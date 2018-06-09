using System;
using System.IO;


namespace ArgonCore.Util
{
    public class Buffer
    {
        MemoryStream stream;
        BinaryWriter writer;

        public Buffer()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public byte[] GetBuffer()
        {
            return stream.GetBuffer();
        }

        public void Write(ulong value) => writer.Write(value);
        public void Write(uint value) => writer.Write(value);
        public void Write(ushort value) => writer.Write(value);
        public void Write(string value) => writer.Write(value);
        public void Write(float value) => writer.Write(value);
        public void Write(sbyte value) => writer.Write(value);
        public void Write(long value) => writer.Write(value);
        public void Write(int value) => writer.Write(value);
        public void Write(short value) => writer.Write(value);
        public void Write(decimal value) => writer.Write(value);
        public void Write(char[] chars, int index, int count)
            => writer.Write(chars, index, count);
        public void Write(char[] chars)
            => writer.Write(chars);
        public void Write(byte[] buffer, int index, int count)
            => writer.Write(buffer, index, count);
        public void Write(byte[] buffer)
            => writer.Write(buffer);
        public void Write(byte value) => writer.Write(value);
        public void Write(bool value) => writer.Write(value);
        public void Write(double value) => writer.Write(value);
        public void Write(char ch) => writer.Write(ch);

        public void Write(object o)
        {
            if (o.GetType() == typeof(ulong))
            {
                Write((ulong)o);
            }
            else if (o.GetType() == typeof(uint))
            {
                Write((uint)o);
            }
            else if (o.GetType() == typeof(ushort))
            {
                Write((ushort)o);
            }
            else if (o.GetType() == typeof(string))
            {
                Write((string)o);
            }
            else if (o.GetType() == typeof(float))
            {
                Write((float)o);
            }
            else if (o.GetType() == typeof(sbyte))
            {
                Write((sbyte)o);
            }
            else if (o.GetType() == typeof(long))
            {
                Write((long)o);
            }
            else if (o.GetType() == typeof(int))
            {
                Write((int)o);
            }
            else if (o.GetType() == typeof(short))
            {
                Write((short)o);
            }
            else if (o.GetType() == typeof(decimal))
            {
                Write((decimal)o);
            }
            else if (o.GetType() == typeof(byte))
            {
                Write((byte)o);
            }
            else if (o.GetType() == typeof(ulong))
            {
                Write((ulong)o);
            }
            else if (o.GetType() == typeof(bool))
            {
                Write((bool)o);
            }
            else if (o.GetType() == typeof(double))
            {
                Write((double)o);
            }
            else if (o.GetType() == typeof(char))
            {
                Write((char)o);
            }
        }

        public void Write(params object[] args)
        {
            foreach (var o in args)
            {
                Write(o);
            }
        }
    }
}