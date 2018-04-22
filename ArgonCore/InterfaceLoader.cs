using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ArgonCore
{
    /// <summary>
    /// Represents a class with a vtable
    /// </summary>
    public struct VtableClass
    {
        unsafe public IntPtr* vtable;

        public bool contextless;


    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InterfaceAttribute : Attribute
    {
        // Name of this interface
        // TODO: this should be got from path + dllname
        public string Name { get; set; }

        // Does this interface require user context
        public bool Contextless { get; set; }
    }

    public struct Plugin
    {
        public class Interface
        {
            public string name;
            public bool contextless;
            public List<Delegate> delagates;
            public IntPtr[] vtable;

            public Interface()
            {
                delagates = new List<Delegate>();
            }
        }

        public string name;
        public List<Interface> interfaces;
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
                    interfaces = new List<Plugin.Interface>()
                };

                var assembly = Assembly.LoadFile(f);

                foreach (var t in assembly.GetTypes())
                {
                    if (t.IsDefined(typeof(InterfaceAttribute)))
                    {
                        var attribute = t.GetCustomAttribute<InterfaceAttribute>();
                        var name = attribute.Name;
                        var contextless = attribute.Contextless;

                        var new_interface = new Plugin.Interface { name = name, contextless = contextless };

                        var methods = t.GetMethods(BindingFlags.Static | BindingFlags.Public);

                        var new_vtable = new IntPtr[methods.Length];

                        foreach (var m in methods)
                        {
                            var d = m.CreateDelegate(m.ReflectedType);

                            // TODO: handle contextful delegates
                            if (contextless)
                            {
                                new_interface.delagates.Add(d);
                            }
                        }

                        for (int i = 0; i < new_interface.delagates.Count; i++)
                        {
                            var d = new_interface.delagates[i];
                            new_vtable[i] = Marshal.GetFunctionPointerForDelegate(d);
                        }

                        new_interface.vtable = new_vtable;

                        p.interfaces.Add(new_interface);
                    }
                }
            }

            loaded = true;
            return;
        }
    }
}
