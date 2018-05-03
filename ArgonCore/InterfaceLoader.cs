using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq.Expressions;

namespace ArgonCore
{
    /// <summary>
    /// Represents a class with a vtable
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct InterfaceContext
    {
        [MarshalAs(UnmanagedType.LPArray)]
        unsafe public IntPtr* vtable;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InterfaceDelegateAttribute : Attribute
    {
        // Name of this interface
        // TODO: this should be got from path + dllname
        public string Name { get; set; }

        // Does this interface require user context
        public bool Contextless { get; set; }
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
            public bool contextless;
            public List<MemberInfo> delegate_types;

            public InterfaceDelegates()
            {
                delegate_types = new List<MemberInfo>();
            }
        }

        public class InterfaceImpl
        {
            public string name;
            public string implements;
            public List<MethodInfo> methods;
            public Type this_type;

            public List<List<Delegate>> stored_delegates;
            public List<IntPtr[]> stored_function_pointers;

            public InterfaceImpl()
            {
                methods = new List<MethodInfo>();
                stored_delegates = new List<List<Delegate>>();
                stored_function_pointers = new List<IntPtr[]>();
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
        public static List<IntPtr[]> Converted { get; private set; }


        public static void Load()
        {
            if (loaded) return;

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
                        var contextless = attribute.Contextless;

                        var new_interface = new Plugin.InterfaceDelegates { name = name, contextless = contextless };

                        var members = t.GetMembers(BindingFlags.Public);

                        foreach (var m in members)
                        {
                            new_interface.delegate_types.Add(m);
                        }

                        // Just assume all members are delegate types
                        p.interface_delegates.Add(new_interface);


                    }
                    else if (t.IsDefined(typeof(InterfaceImplAttribute)))
                    {
                        var attribute = t.GetCustomAttribute<InterfaceImplAttribute>();
                        var name = attribute.Name;
                        var implements = attribute.Implements;

                        var new_interface_impl = new Plugin.InterfaceImpl { name = name };

                        foreach (var m in t.GetMethods(BindingFlags.Public))
                        {
                            new_interface_impl.methods.Add(m);
                        }

                        new_interface_impl.this_type = t;

                        p.interface_impls.Add(new_interface_impl);
                    }
                }
            }

            loaded = true;
            return;
        }

        public static IntPtr CreateInterface(string name)
        {
            foreach (var p in LoadedPlugins)
            {
                foreach (var impl in p.interface_impls)
                {
                    if (impl.name == name)
                    {
                        var instance = Activator.CreateInstance(impl.this_type);

                        var iface = p.interface_delegates.Find(x => x.name.Contains(impl.name));

                        var new_delegates = new List<Delegate>();

                        for (int i = 0; i < impl.methods.Count; i++)
                        {
                            // Find the delegate type that matches the method
                            var mi = impl.methods[i];
                            var type = iface.delegate_types.Find(x => x.Name.Contains(mi.Name));

                            var new_delegate = Delegate.CreateDelegate(type.ReflectedType, instance, mi, true);

                            new_delegates.Add(new_delegate);
                        }

                        impl.stored_delegates.Add(new_delegates);

                        var vtable = new IntPtr[impl.methods.Count];

                        for (int i = 0; i < new_delegates.Count; i++)
                        {
                            vtable[i] = Marshal.GetFunctionPointerForDelegate(new_delegates[i]);
                        }

                        impl.stored_function_pointers.Add(vtable);
                    }
                }
            }

            return IntPtr.Zero;
        }
    }
}
