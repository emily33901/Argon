using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ArgonCore;

namespace DelegateGenerator
{
    /// <summary>
    /// <para>This program is for creating a class of delegates from an interface implementation.</para>
    /// <para>The resulting classes will be in the form of the name that the class implements with the correct attributes applied</para>
    /// </summary>
    class Program
    {
        public class FunctionClassDefinition
        {
            public class FunctionDefinition
            {
                public string name;
                public string return_type;

                // stored in the form (type, name)
                public List<(string, string)> args;

                public FunctionDefinition()
                {
                    args = new List<(string, string)>();
                }
            }

            public string containing_namespace;
            public string name;
            public string implements;
            public List<FunctionDefinition> functions;
            public bool server_mapped;

            public FunctionClassDefinition()
            {
                functions = new List<FunctionDefinition>();
            }
        }

        public static List<FunctionClassDefinition> InterfaceImplementations { get; set; }
        public static List<FunctionClassDefinition> InterfaceClasses { get; set; }

        static string GetCorrectTypeName(Type t)
        {
            var original_name = t.FullName;
            var new_name = original_name;

            if (t.IsByRef)
            {
                new_name = "ref " + original_name.Substring(0, original_name.Length - 1);
            }

            switch (t.FullName)
            {
                case "System.Void":
                    new_name = "void";
                    break;
                case "System.Int32":
                    new_name = "int";
                    break;
                case "System.String":
                    new_name = "string";
                    break;
                case "System.UInt32":
                    new_name = "uint";
                    break;
                case "System.Boolean":
                    new_name = "bool";
                    break;
                case "System.IntPtr":
                    new_name = "IntPtr";
                    break;
            }

            return new_name;
        }

        static void FindClasses()
        {
            InterfaceImplementations = new List<FunctionClassDefinition>();
            InterfaceClasses = new List<FunctionClassDefinition>();

            foreach (var a in ArgonCore.Interface.Loader.GetInterfaceAssemblies())
            {
                foreach (var type in a.GetTypes())
                {
                    if (ArgonCore.Interface.Loader.IsInterfaceImpl(type))
                    {
                        var impl_attribute = type.GetCustomAttribute<ArgonCore.Interface.ImplAttribute>();

                        var this_class = new FunctionClassDefinition
                        {
                            name = impl_attribute.Name,
                            implements = impl_attribute.Implements,
                            containing_namespace = type.Namespace,
                            server_mapped = impl_attribute.ServerMapped
                        };
                        var methods = ArgonCore.Interface.Loader.InterfaceMethodsForType(type);

                        var new_functions = new List<FunctionClassDefinition.FunctionDefinition>();
                        foreach (var mi in methods)
                        {
                            var new_function = new FunctionClassDefinition.FunctionDefinition
                            {
                                name = mi.Name,
                                return_type = GetCorrectTypeName(mi.ReturnType),
                            };

                            foreach (var param in mi.GetParameters())
                            {
                                new_function.args.Add((GetCorrectTypeName(param.ParameterType), param.Name));
                            }

                            new_functions.Add(new_function);
                        }
                        this_class.functions.AddRange(new_functions);

                        InterfaceClasses.Add(this_class);

                        // Add these new functions to their implementation classes
                        var implemented_found = true;

                        // See whether we have already added some methods for this delegate class
                        var implemented_class = InterfaceImplementations.Find(x => x.name == impl_attribute.Implements);
                        if (implemented_class == null)
                        {
                            implemented_found = false;
                            implemented_class = new FunctionClassDefinition { name = impl_attribute.Implements, containing_namespace = type.Namespace };
                        }

                        foreach (var f in new_functions)
                        {
                            // See whether we have already got this delegate
                            if (implemented_class.functions.Find(x => x.name == f.name) != null) continue;

                            implemented_class.functions.Add(f);
                        }

                        if (!implemented_found) InterfaceImplementations.Add(implemented_class);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            FindClasses();

            WriteFiles();

            Console.ReadLine();
        }

        static void WriteDelegates()
        {
            const string file_prolog =
@"using System;

// Autogenerated @ {2}
namespace {0}
{{
    /// <summary>
    /// Exports the delegates for all interfaces that implement {1}
    /// </summary>
    [ArgonCore.Interface.Delegate(Name = ""{1}"")]
    class {1}_Delegates
    {{";

            const string file_epilog =
    @"    }
}";

            foreach (var c in InterfaceImplementations)
            {
                var new_file = new StringBuilder();

                // Print file prolog
                new_file.AppendFormat(file_prolog, c.containing_namespace, c.name, DateTime.Now.Date.ToString("dd/MM/yy"));
                new_file.AppendLine();

                foreach (var del in c.functions)
                {
                    new_file.AppendFormat("        public delegate {0} {1}(", del.return_type, del.name + "Delegate");
                    for (var i = 0; i < del.args.Count; i++)
                    {
                        var (t, name) = del.args[i];

                        new_file.AppendFormat("{0} {1}", t, name);

                        if (i != del.args.Count - 1)
                        {
                            new_file.Append(", ");
                        }
                    }

                    new_file.AppendLine(");");
                }

                new_file.AppendLine(file_epilog);

                Console.WriteLine(new_file.ToString());

                File.WriteAllText(String.Format("../InterfaceAutogen/Delegates/{0}Delegates.cs", c.name), new_file.ToString());
            }
        }

        static void WriteMaps()
        {
            const string file_prolog =
@"using System;

// Autogenerated @ {3}
namespace {0}
{{
    /// <summary>
    /// Implements the map for interface {1}
    /// </summary>
    [ArgonCore.Interface.Map(Name = ""{1}"", Implements = ""{2}"")]
    public class {1}_Map : ArgonCore.Interface.IBaseInterfaceMap
    {{";

            const string file_epilog =
    @"    }
}";
            foreach (var c in InterfaceClasses)
            {
                if (!c.server_mapped) continue;

                var new_file = new StringBuilder();

                // Print file prolog
                new_file.AppendFormat(file_prolog, c.containing_namespace, c.name, c.implements, DateTime.Now.Date.ToString("dd/MM/yy"));
                new_file.AppendLine();

                foreach (var f in c.functions)
                {
                    // Helper
                    var param_names = new StringBuilder();

                    new_file.AppendFormat("        public {0} {1}(", f.return_type, f.name + "");

                    // Create param data
                    for (var i = 0; i < f.args.Count; i++)
                    {
                        var (t, name) = f.args[i];

                        param_names.Append(name);

                        new_file.AppendFormat("{0} {1}", t, name);

                        if (i != f.args.Count - 1)
                        {
                            new_file.Append(", ");
                            param_names.Append(", ");
                        }
                    }

                    new_file.AppendLine(")");
                    new_file.AppendLine("        {");

                    // build the body of the function
                    new_file.AppendLine("            var sf = new ArgonCore.IPC.SerializedFunction");
                    new_file.AppendLine("            {");
                    new_file.AppendLine("               InterfaceId = InterfaceId,");
                    new_file.AppendLine(
                        String.Format("               Name = \"{0}\",", f.name));
                    new_file.Append("               Args = new object[] {"); new_file.Append(param_names); new_file.AppendLine("},");
                    new_file.AppendLine("            };");

                    if (f.return_type != "void")
                    {
                        new_file.Append("            return ");
                    }
                    else
                    {
                        new_file.Append("            ");
                    }

                    new_file.AppendFormat("ArgonCore.IPC.Client.CallSerializedFunction");
                    if (f.return_type != "void")
                    {
                        new_file.AppendFormat("<{0}>", f.return_type);
                    }

                    new_file.AppendLine("(sf);");
                    new_file.AppendLine("        }");
                }

                new_file.AppendLine(file_epilog);

                Console.WriteLine(new_file.ToString());

                File.WriteAllText(String.Format("../InterfaceAutogen/Maps/{0}Map.cs", c.name), new_file.ToString());
            }
        }

        static void WriteFiles()
        {
            WriteDelegates();
            WriteMaps();
        }
    }
}
