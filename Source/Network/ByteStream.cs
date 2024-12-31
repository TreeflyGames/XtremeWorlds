using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Mirage.Sharp.Asfw
{
    public struct ByteStream : IDisposable
    {
        public byte[] Data;
        public int Head;

        public ByteStream(int initialSize = 4)
        {
            if (initialSize < 1)
                initialSize = 4;

            Data = new byte[initialSize];
            Head = 0;
        }

        public ByteStream(byte[] bytes)
        {
            Data = bytes;
            Head = 0;
        }

        public void Dispose()
        {
            Data = null;
            Head = 0;
        }

        public byte[] ToArray()
        {
            byte[] dst = new byte[Head];
            Buffer.BlockCopy(Data, 0, dst, 0, Head);
            return dst;
        }

        public byte[] ToPacket()
        {
            byte[] dst = new byte[4 + Head];
            Buffer.BlockCopy(BitConverter.GetBytes(Head), 0, dst, 0, 4);
            Buffer.BlockCopy(Data, 0, dst, 4, Head);
            return dst;
        }

        private void CheckSize(int length)
        {
            int num = Data.Length;
            if (length + Head < num)
                return;

            if (num < 4)
                num = 4;

            int length1 = num * 2;
            while (length + Head >= length1)
                length1 *= 2;

            byte[] dst = new byte[length1];
            Buffer.BlockCopy(Data, 0, dst, 0, Head);
            Data = dst;
        }

        public byte[] ReadBlock(int size)
        {
            if (size < 0 || Head + size > Data.Length)
                return new byte[0];

            byte[] dst = new byte[size];
            Buffer.BlockCopy(Data, Head, dst, 0, size);
            Head += size;
            return dst;
        }

        public byte[] ReadBytes()
        {
            if (Head + 4 > Data.Length)
                return new byte[0];

            int int32 = BitConverter.ToInt32(Data, Head);
            Head += 4;

            if (int32 < 0 || Head + int32 > Data.Length)
                return new byte[0];

            byte[] dst = new byte[int32];
            Buffer.BlockCopy(Data, Head, dst, 0, int32);
            Head += int32;
            return dst;
        }

        public string ReadString()
        {
            if (Head + 4 > Data.Length)
                return "";

            int int32 = BitConverter.ToInt32(Data, Head);
            Head += 4;

            if (int32 < 0 || Head + int32 > Data.Length)
                return "";

            string str = Encoding.UTF8.GetString(Data, Head, int32);
            Head += int32;
            return str;
        }

        public char ReadChar()
        {
            if (Head + 2 > Data.Length)
                return char.MinValue;

            char result = BitConverter.ToChar(Data, Head);
            Head += 2;
            return result;
        }

        public byte ReadByte()
        {
            if (Head + 1 > Data.Length)
                return 0;

            byte result = Data[Head];
            Head++;
            return result;
        }

        public bool ReadBoolean()
        {
            if (Head + 1 > Data.Length)
                return false;

            bool result = Data[Head] != 0;
            Head++;
            return result;
        }

        public short ReadInt16()
        {
            if (Head + 2 > Data.Length)
                return 0;

            short result = BitConverter.ToInt16(Data, Head);
            Head += 2;
            return result;
        }

        public ushort ReadUInt16()
        {
            if (Head + 2 > Data.Length)
                return 0;

            ushort result = BitConverter.ToUInt16(Data, Head);
            Head += 2;
            return result;
        }

        public int ReadInt32()
        {
            if (Head + 4 > Data.Length)
                return 0;

            int result = BitConverter.ToInt32(Data, Head);
            Head += 4;
            return result;
        }

        public uint ReadUInt32()
        {
            if (Head + 4 > Data.Length)
                return 0;

            uint result = BitConverter.ToUInt32(Data, Head);
            Head += 4;
            return result;
        }

        public float ReadSingle()
        {
            if (Head + 4 > Data.Length)
                return 0.0f;

            float result = BitConverter.ToSingle(Data, Head);
            Head += 4;
            return result;
        }

        public long ReadInt64()
        {
            if (Head + 8 > Data.Length)
                return 0;

            long result = BitConverter.ToInt64(Data, Head);
            Head += 8;
            return result;
        }

        public ulong ReadUInt64()
        {
            if (Head + 8 > Data.Length)
                return 0;

            ulong result = BitConverter.ToUInt64(Data, Head);
            Head += 8;
            return result;
        }

        public double ReadDouble()
        {
            if (Head + 8 > Data.Length)
                return 0.0;

            double result = BitConverter.ToDouble(Data, Head);
            Head += 8;
            return result;
        }

        public void WriteBlock(byte[] bytes)
        {
            CheckSize(bytes.Length);
            Buffer.BlockCopy(bytes, 0, Data, Head, bytes.Length);
            Head += bytes.Length;
        }

        public void WriteBlock(byte[] bytes, int offset, int size)
        {
            CheckSize(size);
            Buffer.BlockCopy(bytes, offset, Data, Head, size);
            Head += size;
        }

        public void WriteBytes(byte[] value, int offset, int size)
        {
            WriteBlock(BitConverter.GetBytes(size));
            WriteBlock(value, offset, size);
        }

        public void WriteBytes(byte[] value)
        {
            WriteBlock(BitConverter.GetBytes(value.Length));
            WriteBlock(value);
        }

        public void WriteString(string value)
        {
            if (value == null)
            {
                WriteBlock(BitConverter.GetBytes(0));
            }
            else
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                WriteBlock(BitConverter.GetBytes(bytes.Length));
                WriteBlock(bytes);
            }
        }

        public void WriteChar(char value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteByte(byte value)
        {
            CheckSize(1);
            Data[Head] = value;
            Head++;
        }

        public void WriteBoolean(bool value)
        {
            CheckSize(1);
            Data[Head] = (byte)(value ? 1 : 0);
            Head++;
        }

        public void WriteInt16(short value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteUInt16(ushort value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteInt32(int value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteUInt32(uint value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteSingle(float value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteInt64(long value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteUInt64(ulong value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }

        public void WriteDouble(double value)
        {
            WriteBlock(BitConverter.GetBytes(value));
        }
    }
}