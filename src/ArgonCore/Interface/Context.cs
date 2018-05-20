using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ArgonCore.Interface
{
    class Context
    {
        public static object CreateInterfaceInstance(Plugin.InterfaceImpl impl)
        {
            return Activator.CreateInstance(impl.this_type);
        }

        private static (IntPtr, IBaseInterface) Create(Plugin.InterfaceDelegates iface, Plugin.InterfaceImpl impl)
        {
            var instance = CreateInterfaceInstance(impl);

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

            return (new_context, (IBaseInterface)instance);
        }



        public (IntPtr, IBaseInterface) CreateMap()
        {
            return (IntPtr.Zero, null);
        }

        /// <summary>
        /// Get an interface implementation by its exported name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Plugin.InterfaceImpl FindInterfaceImpl(string name)
        {
            Loader.Load();

            foreach (var p in Loader.LoadedPlugins)
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
            Loader.Load();

            foreach (var p in Loader.LoadedPlugins)
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

        public static Plugin.InterfaceMap FindInterfaceMap(string name)
        {
            Loader.Load();

            foreach (var p in Loader.LoadedPlugins)
            {
                foreach (var m in p.interface_maps)
                {
                    if (m.name == name)
                    {
                        return m;
                    }
                }
            }

            return null;
        }

        public static Plugin.InterfaceImpl FindImpl(string name)
        {
            var impl = FindInterfaceImpl(name);

            if (impl == null)
            {
                Console.WriteLine("Unable to find implementation for interface {0}", name);
                return null;
            }

            return impl;
        }

        public static Plugin.InterfaceMap FindMap(string name)
        {
            var map = FindInterfaceMap(name);

            if (map == null)
            {
                Console.WriteLine("Unable to find map for interface {0}", name);
                return null;
            }
            return map;
        }

        public static (IntPtr, IBaseInterface, bool) CreateInterface(string name, bool try_create_map = false)
        {
            // Ensure that we are loaded before trying to query loaded plugins
            Loader.Load();

            Plugin.InterfaceImpl impl = null;

            bool is_map = try_create_map;

            if (try_create_map)
            {
                impl = FindInterfaceMap(name);
            }

            // Not trying to find a map or we couldnt find one (in the case that an interface is not servermapped)
            if (impl == null)
            {
                is_map = false;

                impl = FindImpl(name);
            }

            var iface = FindInterfaceDelegates(impl.implements);

            if (iface == null)
            {
                Console.WriteLine("Unable to find delegates for interface that implements {0}", impl.implements);
                return (IntPtr.Zero, null, false);
            }

            // Try to create a new context based on this interface + impl pair
            var (context, instance) = Create(iface, impl);
            return (context, instance, is_map);
        }
    }
}
