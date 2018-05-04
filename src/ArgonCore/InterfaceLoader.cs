using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ArgonCore
{
    /// <summary>
    /// Used to signal to <see cref="InterfaceLoader"/> that this class is used for interface delegates
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InterfaceDelegateAttribute : Attribute
    {
        /// <summary>
        /// General name of the interfaces that use these delegates
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Interface that all interface implementations must inherit from
    /// </summary>
    public interface IBaseInterface
    {
        /// <summary>
        /// Set by <see cref="ArgonCore.User"/> to allow interfaces to know what user they belong too
        /// </summary>
        int UserId { get; set; }
    }

    /// <summary>
    /// Used to signal to <see cref="InterfaceLoader"/> that this class is used for interface implementations
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InterfaceImplAttribute : Attribute
    {
        /// <summary>
        /// Name that this interface wants to be exported as
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the interface delegates that this class implements
        /// </summary>
        public string Implements { get; set; }
    }

    public class Plugin
    {
        public class InterfaceDelegates
        {
            /// <summary>
            /// Refer to <see cref="InterfaceDelegateAttribute.Name"/>
            /// </summary>
            public string name;

            /// <summary>
            /// Delegate types that were extracted from the class at runtime
            /// </summary>
            public List<Type> delegate_types;

            public InterfaceDelegates()
            {
                delegate_types = new List<Type>();
            }
        }

        public class InterfaceImpl
        {
            /// <summary>
            /// Refer to <see cref="InterfaceImplAttribute.Name"/>
            /// </summary>
            public string name;

            /// <summary>
            /// Refer to <see cref="InterfaceImplAttribute.Implements"/>
            /// </summary>
            public string implements;

            /// <summary>
            /// Methods that were extracted from the class at runtime
            /// </summary>
            public List<MethodInfo> methods;

            /// <summary>
            /// The runtime type of this class (used in <see cref="InterfaceLoader.CreateContext"/> to create an instance of this interface)
            /// </summary>
            public Type this_type;

            /// <summary>
            /// Delegates that are allocated by this interface at runtime
            /// </summary>
            public List<List<Delegate>> stored_delegates;

            /// <summary>
            /// Handles to memory that are used by this interface for storing unmanaged function pointers
            /// </summary>
            public List<IntPtr> stored_function_pointers;

            /// <summary>
            /// Handles to memory that are used by this interface for storing unmanaged context pointers
            /// </summary>
            public List<IntPtr> stored_contexts;

            public InterfaceImpl()
            {
                methods = new List<MethodInfo>();
                stored_delegates = new List<List<Delegate>>();
                stored_function_pointers = new List<IntPtr>();
                stored_contexts = new List<IntPtr>();
            }
        }

        /// <summary>
        /// Name of this plugin
        /// </summary>
        public string name;

        /// <summary>
        /// Interface delegates contained within this plugin
        /// </summary>
        public List<InterfaceDelegates> interface_delegates;

        /// <summary>
        /// Interface implementations contained within this plugin
        /// </summary>
        public List<InterfaceImpl> interface_impls;

        public Plugin()
        {
            interface_delegates = new List<InterfaceDelegates>();
            interface_impls = new List<InterfaceImpl>();
        }
    }

    /// <summary>
    /// Creates interfaces from their respective dlls on disk
    /// </summary>
    class InterfaceLoader
    {
        private static bool loaded;

        public static List<Plugin> LoadedPlugins { get; private set; }

        public static void Load()
        {
            if (loaded) return;

            LoadedPlugins = new List<Plugin>();

            var filenames = Directory.EnumerateFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Interface*.dll");

            foreach (var f in filenames)
            {
                var p = new Plugin { name = f, };

                var assembly = Assembly.LoadFile(f);

                foreach (var t in assembly.GetTypes())
                {
                    if (t.IsDefined(typeof(InterfaceDelegateAttribute)))
                    {
                        var attribute = t.GetCustomAttribute<InterfaceDelegateAttribute>();
                        var name = attribute.Name;

                        var new_interface = new Plugin.InterfaceDelegates { name = name };

                        var types = t.GetNestedTypes(BindingFlags.Public);

                        foreach (var type in types)
                        {
                            // Filter out types that are not delegates
                            if (type.IsSubclassOf(typeof(System.Delegate))) new_interface.delegate_types.Add(type);
                        }

                        // Just assume all members are delegate types
                        p.interface_delegates.Add(new_interface);
                    }
                    else if (t.IsDefined(typeof(InterfaceImplAttribute)))
                    {
                        // In order to see whether this class is inherited from IBaseInterface
                        // we need to see whether we could assign an IBaseInterface object from it
                        if (!typeof(IBaseInterface).IsAssignableFrom(t))
                        {
                            Console.WriteLine("Class {0} has InterfaceImplAttribute but does not inherit IBaseInterface! IGNORING!", t.Name);
                            continue;
                        }

                        var attribute = t.GetCustomAttribute<InterfaceImplAttribute>();
                        var name = attribute.Name;
                        var implements = attribute.Implements;

                        var new_interface_impl = new Plugin.InterfaceImpl { name = name, implements = implements };

                        var all_methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                        // TODO: maybe it would just be better to look for all methods that have a delegate type and only use those
                        new_interface_impl.methods.AddRange(all_methods);
                        new_interface_impl.methods.RemoveAll(x => x.Name.StartsWith("get_") || x.Name.StartsWith("set_"));

                        new_interface_impl.this_type = t;

                        p.interface_impls.Add(new_interface_impl);
                    }
                }

                LoadedPlugins.Add(p);
            }

            loaded = true;
            return;
        }

        private static (IntPtr, IBaseInterface) CreateContext(Plugin.InterfaceDelegates iface, Plugin.InterfaceImpl impl)
        {
            var instance = (IBaseInterface)Activator.CreateInstance(impl.this_type);

            var new_delegates = new List<Delegate>();

            for (var i = 0; i < impl.methods.Count; i++)
            {
                // Find the delegate type that matches the method
                var mi = impl.methods[i];

                var type = iface.delegate_types.Find(x => x.Name.Contains(mi.Name));

                // Create new delegates that are bounded to this instance
                var new_delegate = Delegate.CreateDelegate(type, instance, mi, true);

                new_delegates.Add(new_delegate);
            }

            impl.stored_delegates.Add(new_delegates);


            // Create a new context (class) that mimics what the C++ compiler would generate

            // class:
            //  vtable:
            //   1
            //   2
            //   3
            //   4

            // TODO: Warn if not x86
            var ptr_size = Marshal.SizeOf(typeof(IntPtr));

            // Allocate enough space for the new pointers in local memory
            var vtable = Marshal.AllocHGlobal(impl.methods.Count * ptr_size);

            for (var i = 0; i < new_delegates.Count; i++)
            {
                // Create all function pointers as neccessary
                Marshal.WriteIntPtr(vtable, i * ptr_size, Marshal.GetFunctionPointerForDelegate(new_delegates[i]));
            }

            impl.stored_function_pointers.Add(vtable);

            // create the context
            var new_context = Marshal.AllocHGlobal(ptr_size);

            // Write the pointer to the vtable at the address pointed to by new_context;
            Marshal.WriteIntPtr(new_context, vtable);

            return (new_context, instance);
        }

        public static (IntPtr, IBaseInterface) CreateInterface(string name)
        {
            // Ensure that we are loaded before trying to query loaded plugins
            Load();

            foreach (var p in LoadedPlugins)
            {
                foreach (var impl in p.interface_impls)
                {
                    if (impl.name == name)
                    {
                        var iface = p.interface_delegates.Find(x => x.name == impl.implements);

                        // Try to create a new context based on this interface + impl pair
                        return CreateContext(iface, impl);
                    }
                }
            }

            return (IntPtr.Zero, null);
        }
    }
}
