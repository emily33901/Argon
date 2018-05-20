﻿using System;
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

        public static bool IsInterfaceMap(Type t)
        {
            var has_attribute = t.IsDefined(typeof(MapAttribute));

            if (has_attribute && !typeof(IBaseInterfaceMap).IsAssignableFrom(t))
            {
                Console.WriteLine("Class {0} has InterfaceMapAttribute but does not inherit IBaseInterfaceMap! IGNORING!", t.Name);

                return false;
            }

            return has_attribute;
        }

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
                    else if (IsInterfaceMap(t))
                    {
                        var attribute = t.GetCustomAttribute<MapAttribute>();
                        var name = attribute.Name;
                        var implements = attribute.Implements;

                        var new_interface_map = new Plugin.InterfaceMap
                        {
                            name = name,
                            implements = implements,
                            this_type = t,
                            methods = InterfaceMethodsForType(t),
                        };

                        Console.WriteLine("Found interface map \"{0}\"", name);

                        p.interface_maps.Add(new_interface_map);
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
    }
}
