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
    public class InterfaceLoader
    {
        private static bool loaded;

        public static List<Plugin> LoadedPlugins { get; private set; }

        public static List<MethodInfo> InterfaceMethodsForType(Type t)
        {
            var all_methods = new List<MethodInfo>(t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            // TODO: maybe it would just be better to look for all methods that have a delegate type and only use those
            all_methods.RemoveAll(x => x.Name.StartsWith("get_") || x.Name.StartsWith("set_"));

            return all_methods;
        }

        public static bool IsInterfaceDelegate(Type t) => t.IsDefined(typeof(InterfaceDelegateAttribute));
        public static bool IsInterfaceImpl(Type t)
        {
            var has_attribute = t.IsDefined(typeof(InterfaceImplAttribute));

            // In order to see whether this class is inherited from IBaseInterface
            // we need to see whether we could assign an IBaseInterface object from it
            if (has_attribute && !typeof(IBaseInterface).IsAssignableFrom(t))
            {
                Console.WriteLine("Class {0} has InterfaceImplAttribute but does not inherit IBaseInterface! IGNORING!", t.Name);
                return false;
            }

            return has_attribute;
        }

        public static List<Assembly> GetInterfaceAssemblies()
        {
            var assemblies = new List<Assembly>();

            var filenames = Directory.EnumerateFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Interface*.dll");

            foreach (var f in filenames)
            {
                assemblies.Add(Assembly.LoadFile(f));
            }

            return assemblies;
        }

        public static void Load()
        {
            if (loaded) return;

            LoadedPlugins = new List<Plugin>();

            foreach (var a in GetInterfaceAssemblies())
            {
                var p = new Plugin { name = a.GetName().Name };

                foreach (var t in a.GetTypes())
                {
                    if (IsInterfaceDelegate(t))
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
                    else if (IsInterfaceImpl(t))
                    {
                        var attribute = t.GetCustomAttribute<InterfaceImplAttribute>();
                        var name = attribute.Name;
                        var implements = attribute.Implements;

                        var new_interface_impl = new Plugin.InterfaceImpl
                        {
                            name = name,
                            implements = implements,
                            this_type = t,
                            methods = InterfaceMethodsForType(t)
                        };

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

                if (type == null)
                {
                    Console.WriteLine("Unable to find delegate for {0} in {1}! (maybe you need to regen autogen?)", mi.Name, iface.name);
                    return (IntPtr.Zero, null);
                }

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
            //   ...

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

        public static Plugin.InterfaceImpl FindInterfaceImpl(string name)
        {
            Load();

            foreach (var p in LoadedPlugins)
            {
                foreach (var impl in p.interface_impls)
                {
                    if (impl.name == name)
                    {
                        return impl;
                    }
                }
            }

            return null;
        }

        public static Plugin.InterfaceDelegates FindInterfaceDelegates(string name)
        {
            Load();

            foreach (var p in LoadedPlugins)
            {
                foreach (var dels in p.interface_delegates)
                {
                    if (dels.name == name)
                    {
                        return dels;
                    }
                }
            }

            return null;
        }

        public static (IntPtr, IBaseInterface) CreateInterface(string name)
        {
            // Ensure that we are loaded before trying to query loaded plugins
            Load();

            var impl = FindInterfaceImpl(name);

            if (impl == null)
            {
                Console.WriteLine("Unable to find implementation for interface {0}", name);
                return (IntPtr.Zero, null);
            }

            var iface = FindInterfaceDelegates(impl.implements);

            if(iface == null)
            {
                Console.WriteLine("Unable to find delegates for interface that implements {0}", impl.implements);
                return (IntPtr.Zero, null);
            }

            // Try to create a new context based on this interface + impl pair
            return CreateContext(iface, impl);
        }
    }
}
