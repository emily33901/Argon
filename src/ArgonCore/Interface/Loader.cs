using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ArgonCore.Interface
{
    /// <summary>
    /// Creates interfaces from their respective dlls on disk
    /// </summary>
    public class Loader
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

        public static bool IsInterfaceDelegate(Type t) => t.IsDefined(typeof(DelegateAttribute));
        public static bool IsInterfaceImpl(Type t)
        {
            var has_attribute = t.IsDefined(typeof(ImplAttribute));

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
                        var attribute = t.GetCustomAttribute<DelegateAttribute>();
                        var name = attribute.Name;

                        var new_interface = new Plugin.InterfaceDelegates { name = name };

                        Console.WriteLine("Found interface delegates \"{0}\"", name);

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
                        var attribute = t.GetCustomAttribute<ImplAttribute>();
                        var name = attribute.Name;
                        var implements = attribute.Implements;

                        var new_interface_impl = new Plugin.InterfaceImpl
                        {
                            name = name,
                            implements = implements,
                            this_type = t,
                            methods = InterfaceMethodsForType(t)
                        };

                        Console.WriteLine("Found interface impl \"{0}\"", name);


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

        /// <summary>
        /// Get an interface implementation by its exported name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get interface delegates by their exported name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

            if (iface == null)
            {
                Console.WriteLine("Unable to find delegates for interface that implements {0}", impl.implements);
                return (IntPtr.Zero, null);
            }

            // Try to create a new context based on this interface + impl pair
            return CreateContext(iface, impl);
        }
    }
}
