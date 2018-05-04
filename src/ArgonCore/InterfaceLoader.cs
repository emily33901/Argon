using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq.Expressions;

namespace ArgonCore
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InterfaceDelegateAttribute : Attribute
    {
        // Name of this interface
        // TODO: this should be got from path + dllname
        public string Name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InterfaceImplAttribute : Attribute
    {
        // Name of this interface implementation
        public string Name { get; set; }

        // Interface this version / interface implements
        public string Implements { get; set; }
    }

    public class Plugin
    {
        public class InterfaceDelegates
        {
            public string name;
            public List<Type> delegate_types;

            public InterfaceDelegates()
            {
                delegate_types = new List<Type>();
            }
        }

        public class InterfaceImpl
        {
            public string name;
            public string implements;
            public List<MethodInfo> methods;
            public Type this_type;

            // Generated data related to this interface that needs to be cleaned up
            public List<List<Delegate>> stored_delegates;

            // Handles to Marshal allocs
            public List<IntPtr> stored_function_pointers;
            public List<IntPtr> stored_contexts;

            public InterfaceImpl()
            {
                methods = new List<MethodInfo>();
                stored_delegates = new List<List<Delegate>>();
                stored_function_pointers = new List<IntPtr>();
                stored_contexts = new List<IntPtr>();
            }
        }

        public string name;
        public List<InterfaceDelegates> interface_delegates;
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
                var p = new Plugin
                {
                    name = f,
                };

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
                            new_interface.delegate_types.Add(type);
                        }

                        // Just assume all members are delegate types
                        p.interface_delegates.Add(new_interface);
                    }
                    else if (t.IsDefined(typeof(InterfaceImplAttribute)))
                    {
                        var attribute = t.GetCustomAttribute<InterfaceImplAttribute>();
                        var name = attribute.Name;
                        var implements = attribute.Implements;

                        var new_interface_impl = new Plugin.InterfaceImpl { name = name, implements = implements };

                        new_interface_impl.methods.AddRange(t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));

                        new_interface_impl.this_type = t;

                        p.interface_impls.Add(new_interface_impl);
                    }
                }

                LoadedPlugins.Add(p);
            }

            loaded = true;
            return;
        }

        private static IntPtr CreateContext(Plugin.InterfaceDelegates iface, Plugin.InterfaceImpl impl)
        {
            var instance = Activator.CreateInstance(impl.this_type);

            var new_delegates = new List<Delegate>();

            for (int i = 0; i < impl.methods.Count; i++)
            {
                // Find the delegate type that matches the method
                var mi = impl.methods[i];
                var type = iface.delegate_types.Find(x => x.Name.Contains(mi.Name));

                // Create new delegates that are bounded to this instance
                var new_delegate = Delegate.CreateDelegate(type, instance, mi, true);

                new_delegates.Add(new_delegate);
            }

            impl.stored_delegates.Add(new_delegates);


            // TODO: Warn if not x86
            var ptr_size = Marshal.SizeOf(typeof(IntPtr));

            // Allocate enough space for the new pointers in local memory
            var vtable = Marshal.AllocHGlobal(impl.methods.Count * ptr_size);

            for (int i = 0; i < new_delegates.Count; i++)
            {
                // Create all function pointers as neccessary
                Marshal.WriteIntPtr(vtable, i * ptr_size, Marshal.GetFunctionPointerForDelegate(new_delegates[i]));
            }

            impl.stored_function_pointers.Add(vtable);

            var new_context = Marshal.AllocHGlobal(impl.methods.Count);

            Marshal.WriteIntPtr(new_context, vtable);

            return new_context;
        }

        public static IntPtr CreateInterface(string name)
        {
            Load();

            foreach (var p in LoadedPlugins)
            {
                foreach (var impl in p.interface_impls)
                {
                    if (impl.name == name)
                    {
                        var iface = p.interface_delegates.Find(x => x.name == impl.implements);

                        return CreateContext(iface, impl);
                    }
                }
            }

            return IntPtr.Zero;
        }
    }
}
