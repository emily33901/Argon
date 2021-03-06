using System;

// Autogenerated @ 21/08/18
namespace InterfaceCore
{
    /// <summary>
    /// Implements the map for interface MappedTest001
    /// </summary>
    [Core.Interface.Map(Name = "MappedTest001")]
    public class MappedTest001_Map : Core.Interface.IBaseInterfaceMap
    {
        public int PointerTest(IntPtr _, ref int a, ref int b, ref int c)
        {

            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "PointerTest",
                Args = new object[] {a, b, c},

            });

            a = (int)result.Args[0];
            b = (int)result.Args[1];
            c = (int)result.Args[2];


            return (int)result.Result;
        }
        public int BufferTest(IntPtr _, IntPtr b_pointer, int b_length)
        {
            var b = new Core.Util.Buffer();
            b.ReadFromPointer(b_pointer, b_length);


            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "BufferTest",
                Args = new object[] {b},

            });

            b = (Core.Util.Buffer)result.Args[0];

            b.WriteToPointer(b_pointer, b_length);

            return (int)result.Result;
        }
        public void StructTest(IntPtr _, IntPtr b_pointer, int b_length)
        {
            var b = new Core.Util.Buffer();
            b.ReadFromPointer(b_pointer, b_length);


            var result = Client.ClientPipe.CallSerializedFunction(PipeId, new Core.IPC.SerializedFunction()
            {
                ClientId = ClientId,
                InterfaceId = InterfaceId,
                Name = "StructTest",
                Args = new object[] {b},

            });

            b = (Core.Util.Buffer)result.Args[0];

            b.WriteToPointer(b_pointer, b_length);

        }
    }
}
