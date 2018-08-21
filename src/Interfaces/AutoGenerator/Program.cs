using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core;

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
                public List<(Type, string)> args;

                public List<Core.Interface.BufferAttribute> buffers;

                public FunctionDefinition()
                {
                    args = new List<(Type, string)>();
                    buffers = new List<Core.Interface.BufferAttribute>();
                }
            }

            public string containing_namespace;
            public string name;
            public string declared_name;
            public List<FunctionDefinition> functions;
            public bool server_mapped;

            public FunctionClassDefinition()
            {
                functions = new List<FunctionDefinition>();
            }
        }

        public static List<FunctionClassDefinition> InterfaceClasses { get; set; }

        static string GetCorrectTypeName(Type t, bool no_ref = false)
        {
            var original_name = t.FullName;
            var new_name = original_name;

            if (t.IsByRef) new_name = original_name.Substring(0, original_name.Length - 1);

            switch (new_name)
            {
                case "System.Void":
                    new_name = "void";
                    break;
                case "System.String":
                    new_name = "string";
                    break;
                case "System.Int32":
                    new_name = "int";
                    break;
                case "System.UInt32":
                    new_name = "uint";
                    break;
                case "System.Int64":
                    new_name = "long";
                    break;
                case "System.UInt64":
                    new_name = "ulong";
                    break;
                case "System.Int16":
                    new_name = "ushort";
                    break;
                case "System.UInt16":
                    new_name = "short";
                    break;
                case "System.Boolean":
                    new_name = "bool";
                    break;
                case "System.IntPtr":
                    new_name = "IntPtr";
                    break;
            }

            if (t.IsByRef && !no_ref)
            {
                new_name = "ref " + new_name;
            }

            return new_name;
        }

        static List<(string, string)> ComputeTypes(FunctionClassDefinition.FunctionDefinition f)
        {
            var computed_types = new List<(string, string)>();
            for (var i = 0; i < f.args.Count; i++)
            {
                var (t, name) = f.args[i];

                computed_types.Add((GetCorrectTypeName(t), name));
            }

            foreach (var b in f.buffers)
            {
                var (old_type, old_name) = computed_types[b.Index];
                computed_types.RemoveAt(b.Index);

                computed_types.Insert(b.NewPointerIndex, ("IntPtr", old_name + "_pointer"));
                computed_types.Insert(b.NewSizeIndex, ("int", old_name + "_length"));
            }

            return computed_types;
        }

        static void FindClasses()
        {
            InterfaceClasses = new List<FunctionClassDefinition>();

            foreach (var a in Core.Interface.Loader.GetInterfaceAssemblies())
            {
                foreach (var type in a.GetTypes())
                {
                    if (Core.Interface.Loader.IsInterfaceImpl(type))
                    {
                        var impl_attribute = type.GetCustomAttribute<Core.Interface.ImplAttribute>();

                        var this_class = new FunctionClassDefinition
                        {
                            name = impl_attribute.Name,
                            containing_namespace = type.Namespace,
                            server_mapped = impl_attribute.ServerMapped,
                            declared_name = type.Name,
                        };
                        var methods = Core.Interface.Loader.InterfaceMethodsForType(type);

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
                                new_function.args.Add((param.ParameterType, param.Name));
                            }

                            foreach (var attribute in mi.GetCustomAttributes())
                            {
                                if (attribute is Core.Interface.BufferAttribute)
                                {
                                    var buffer_attribute = (Core.Interface.BufferAttribute)attribute;

                                    var (arg_t, _) = new_function.args[buffer_attribute.Index];

                                    // Check attribute lines up properly
                                    // These should only reference arguments that are of this type
                                    if (GetCorrectTypeName(arg_t) != "ref Core.Util.Buffer")
                                    {
                                        Console.WriteLine("Parameter at index {0} of function {1} of class {3} does not use Core.Util.Buffer (type was {2})",
                                                          buffer_attribute.Index, new_function.name, GetCorrectTypeName(arg_t), new_function.name);
                                        throw new Exception();
                                    }

                                    new_function.buffers.Add(buffer_attribute);
                                }
                            }

                            new_functions.Add(new_function);
                        }

                        this_class.functions.AddRange(new_functions);

                        InterfaceClasses.Add(this_class);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Finding classes...");
            FindClasses();

            Console.WriteLine("Writing Files...");
            WriteFiles();

            Console.WriteLine("Done!");
            Console.WriteLine("Found {0} interfaces", InterfaceClasses.Count);
            Console.WriteLine("With {0} functions", InterfaceClasses.Aggregate(0, (acc, x) => acc + x.functions.Count));
        }

        static void WriteDelegates()
        {
            const string file_prolog =
@"using System;
using System.Runtime.InteropServices;

// Autogenerated @ {2}
namespace {0}
{{
    /// <summary>
    /// Exports the delegates for all interfaces that implement {1}
    /// </summary>
    [Core.Interface.Delegate(Name = ""{1}"")]
    class {1}_Delegates
    {{";

            const string file_epilog =
@"    }
}";

            foreach (var c in InterfaceClasses)
            {
                var new_file = new StringBuilder();

                // Print file prolog
                new_file.AppendFormat(file_prolog, c.containing_namespace, c.name, DateTime.Now.Date.ToString("dd/MM/yy"));
                new_file.AppendLine();

                foreach (var del in c.functions)
                {
                    new_file.AppendLine("        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]");
                    new_file.Append($"        public delegate {del.return_type} {del.name}(");

                    // Non server mapped classes already have the instance pointer
                    // As a parameter (needed unless we pseudomap them)
                    if (c.server_mapped)
                    {
                        new_file.Append("IntPtr _");
                    }

                    var computed_types = ComputeTypes(del);

                    for (int i = 0; i < computed_types.Count; i++)
                    {
                        var (t, name) = computed_types[i];

                        if (c.server_mapped || (i != 0 && !c.server_mapped)) new_file.Append(", ");

                        new_file.Append($"{t} {name}");
                    }

                    new_file.AppendLine(");");
                }

                new_file.AppendLine(file_epilog);

                Console.WriteLine(new_file.ToString());

                File.WriteAllText($"../InterfaceDelegates/{c.declared_name}Delegates.cs", new_file.ToString());
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
    [Core.Interface.Map(Name = ""{1}"")]
    public class {2}_Map : Core.Interface.IBaseInterfaceMap
    {{";

            const string file_epilog =
@"    }
}";
            foreach (var c in InterfaceClasses)
            {
                if (!c.server_mapped) continue;

                var new_file = new StringBuilder();

                // Print file prolog
                new_file.AppendFormat(file_prolog, c.containing_namespace, c.name, c.declared_name, DateTime.Now.Date.ToString("dd/MM/yy"));
                new_file.AppendLine();

                foreach (var f in c.functions)
                {
                    // Helper
                    var param_names = new StringBuilder();
                    var argument_list = new StringBuilder();

                    var computed_types = ComputeTypes(f);

                    for (int i = 0; i < computed_types.Count; i++)
                    {
                        var (t, name) = computed_types[i];

                        argument_list.Append($", {t} {name}");
                    }

                    // Create param string formats
                    // Do this becuase the ComputedTypes replaces the buffers
                    // with IntPtrs and sizes but we dont want this for the map
                    // generation
                    for (var i = 0; i < f.args.Count; i++)
                    {
                        var (t, name) = f.args[i];

                        if (i != 0)
                            param_names.Append(", ");

                        param_names.Append(name);
                    }

                    new_file.AppendLine(
                        $"        public {f.return_type} {f.name}(IntPtr _{argument_list})");
                    new_file.AppendLine("        {");


                    // Construct needed buffers
                    foreach (var b in f.buffers)
                    {
                        var (t, name) = f.args[b.Index];
                        var (_, ptr_name) = computed_types[b.NewPointerIndex];
                        var (_, size_name) = computed_types[b.NewSizeIndex];

                        new_file.AppendLine($"            var {name} = new Core.Util.Buffer();");
                        new_file.AppendLine($"            {name}.ReadFromPointer({ptr_name}, {size_name});");
                        new_file.AppendLine();
                    }

                    new_file.AppendLine(
                    $@"
            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {{
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = ""{f.name}"",
                Args = new object[] {{{param_names}}},

            }});" + "\n");

                    for (int i = 0; i < f.args.Count; i++)
                    {
                        var (t, name) = f.args[i];

                        if (t.IsByRef)
                            new_file.AppendLine($"            {name} = ({GetCorrectTypeName(t, true)})result.Args[{i}];");
                    }

                    new_file.AppendLine();

                    foreach (var b in f.buffers)
                    {
                        var (t, name) = f.args[b.Index];

                        var (_, pointer_name) = computed_types[b.NewPointerIndex];
                        var (_, size_name) = computed_types[b.NewSizeIndex];

                        new_file.AppendLine($"            {name}.WriteToPointer({pointer_name}, {size_name});");
                    }

                    new_file.AppendLine();


                    if (f.return_type != "void")
                    {
                        new_file.AppendLine($"            return ({f.return_type})result.Result;");
                    }

                    new_file.AppendLine("        }");
                }


                new_file.AppendLine(file_epilog);

                Console.WriteLine(new_file.ToString());

                File.WriteAllText($"../InterfaceMaps/{c.declared_name}Map.cs", new_file.ToString());
            }
        }

        static void WriteFiles()
        {
            WriteDelegates();
            WriteMaps();
        }
    }
}
