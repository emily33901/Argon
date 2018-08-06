using System;

using ArgonCore.Interface;
using ArgonCore.IPC;

namespace InterfaceArgonCore
{
    /// <summary>
    /// Series of tests for the mapping code
    /// </summary>
    [Impl(Name = "MappedTest001", ServerMapped = true)]
    public class MappedTest001 : IBaseInterface
    {
        public int PointerTest(ref int a, ref int b, ref int c)
        {
            c = a + b;
            b = 5;
            a = 4;
            return a + b + c;
        }

        [Buffer(Index = 0, NewPointerIndex = 0, NewSizeIndex = 1)]
        public int BufferTest(ref ArgonCore.Util.Buffer b)
        {
            // Reset the buffer
            b.Reset();

            // Write raw chars here as that is what the test expects
            // Writing typeof(string) will write the length of the string first
            // (which is useful for deserialization but isnt what this is testing for)
            b.Write("13 characters".ToCharArray());

            return b.Length();
        }
    }
}
