using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mirage.Sharp.Asfw
{
    public struct ByteStream : IDisposable
    {
        private byte[] _data;
        private int _head;
        private int _capacity;
        private bool _isDisposed;

        // Constants for optimization
        private const int DefaultInitialSize = 16; // Larger default for modern use
        private const int MinBufferSize = 4;

        // Properties for better control and debugging
        public readonly int Position => _head;
        public readonly int Capacity => _capacity;
        public readonly int Remaining => _capacity - _head;
        public readonly bool IsAtEnd => _head >= _capacity;

        // Constructors
        public ByteStream(int initialSize = DefaultInitialSize)
        {
            if (initialSize < MinBufferSize)
                initialSize = MinBufferSize;

            _data = new byte[initialSize];
            _capacity = initialSize;
            _head = 0;
            _isDisposed = false;
        }

        public ByteStream(byte[] bytes)
        {
            _data = bytes ?? throw new ArgumentNullException(nameof(bytes));
            _capacity = bytes.Length;
            _head = 0;
            _isDisposed = false;
        }

        // Dispose pattern
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _data = null;
                _capacity = 0;
                _head = 0;
                _isDisposed = true;
            }
        }

        // Enhanced array output with bounds checking
        public byte[] ToArray()
        {
            CheckDisposed();
            if (_head == 0) return Array.Empty<byte>();
            byte[] result = new byte[_head];
            Buffer.BlockCopy(_data, 0, result, 0, _head);
            return result;
        }

        // Packet with length prefix (optimized with Span)
        public byte[] ToPacket()
        {
            CheckDisposed();
            byte[] result = new byte[4 + _head];
            BinaryPrimitives.WriteInt32LittleEndian(result.AsSpan(0, 4), _head);
            Buffer.BlockCopy(_data, 0, result, 4, _head);
            return result;
        }

        // Dynamic resizing with exponential growth
        private void EnsureCapacity(int additionalLength)
        {
            CheckDisposed();
            int required = _head + additionalLength;
            if (required <= _capacity) return;

            int newCapacity = Math.Max(_capacity * 2, required);
            byte[] newData = new byte[newCapacity];
            Buffer.BlockCopy(_data, 0, newData, 0, _head);
            _data = newData;
            _capacity = newCapacity;
        }

        // Read methods with Span for performance
        public ReadOnlySpan<byte> ReadBlock(int size)
        {
            CheckDisposed();
            if (size < 0 || _head + size > _capacity)
                return ReadOnlySpan<byte>.Empty;

            var span = _data.AsSpan(_head, size);
            _head += size;
            return span;
        }

        public byte[] ReadBytes()
        {
            int length = ReadInt32();
            return length <= 0 ? Array.Empty<byte>() : ReadBlock(length).ToArray();
        }

        public string ReadString(Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            int length = ReadInt32();
            if (length <= 0) return string.Empty;
            return encoding.GetString(ReadBlock(length));
        }

        public char ReadChar() => BinaryPrimitives.ReadInt16LittleEndian(ReadBlock(2));
        public byte ReadByte() => _head < _capacity ? _data[_head++] : (byte)0;
        public bool ReadBoolean() => ReadByte() != 0;
        public short ReadInt16() => BinaryPrimitives.ReadInt16LittleEndian(ReadBlock(2));
        public ushort ReadUInt16() => BinaryPrimitives.ReadUInt16LittleEndian(ReadBlock(2));
        public int ReadInt32() => BinaryPrimitives.ReadInt32LittleEndian(ReadBlock(4));
        public uint ReadUInt32() => BinaryPrimitives.ReadUInt32LittleEndian(ReadBlock(4));
        public float ReadSingle() => BinaryPrimitives.ReadSingleLittleEndian(ReadBlock(4));
        public long ReadInt64() => BinaryPrimitives.ReadInt64LittleEndian(ReadBlock(8));
        public ulong ReadUInt64() => BinaryPrimitives.ReadUInt64LittleEndian(ReadBlock(8));
        public double ReadDouble() => BinaryPrimitives.ReadDoubleLittleEndian(ReadBlock(8));

        // Write methods with Span optimizations
        public void WriteBlock(ReadOnlySpan<byte> bytes)
        {
            EnsureCapacity(bytes.Length);
            bytes.CopyTo(_data.AsSpan(_head));
            _head += bytes.Length;
        }

        public void WriteBytes(ReadOnlySpan<byte> value)
        {
            WriteInt32(value.Length);
            WriteBlock(value);
        }

        public void WriteString(string value, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            if (string.IsNullOrEmpty(value))
            {
                WriteInt32(0);
                return;
            }
            byte[] bytes = encoding.GetBytes(value);
            WriteBytes(bytes);
        }

        public void WriteChar(char value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteByte(byte value) { EnsureCapacity(1); _data[_head++] = value; }
        public void WriteBoolean(bool value) => WriteByte((byte)(value ? 1 : 0));
        public void WriteInt16(short value) => BinaryPrimitives.WriteInt16LittleEndian(WriteSpan(2), value);
        public void WriteUInt16(ushort value) => BinaryPrimitives.WriteUInt16LittleEndian(WriteSpan(2), value);
        public void WriteInt32(int value) => BinaryPrimitives.WriteInt32LittleEndian(WriteSpan(4), value);
        public void WriteUInt32(uint value) => BinaryPrimitives.WriteUInt32LittleEndian(WriteSpan(4), value);
        public void WriteSingle(float value) => BinaryPrimitives.WriteSingleLittleEndian(WriteSpan(4), value);
        public void WriteInt64(long value) => BinaryPrimitives.WriteInt64LittleEndian(WriteSpan(8), value);
        public void WriteUInt64(ulong value) => BinaryPrimitives.WriteUInt64LittleEndian(WriteSpan(8), value);
        public void WriteDouble(double value) => BinaryPrimitives.WriteDoubleLittleEndian(WriteSpan(8), value);

        // Helper for write operations
        private Span<byte> WriteSpan(int size)
        {
            EnsureCapacity(size);
            var span = _data.AsSpan(_head, size);
            _head += size;
            return span;
        }

        // New Feature: Object Serialization
        public void WriteObject<T>(T obj) where T : class
        {
            CheckDisposed();
            if (obj == null)
            {
                WriteInt32(0);
                return;
            }
            using MemoryStream ms = new();
            System.Text.Json.JsonSerializer.Serialize(ms, obj);
            byte[] bytes = ms.ToArray();
            WriteBytes(bytes);
        }

        public T ReadObject<T>() where T : class
        {
            CheckDisposed();
            byte[] bytes = ReadBytes();
            if (bytes.Length == 0) return null;
            return System.Text.Json.JsonSerializer.Deserialize<T>(bytes);
        }

        // New Feature: Async Read/Write from Stream
        public async Task ReadFromStreamAsync(Stream stream, int length)
        {
            CheckDisposed();
            EnsureCapacity(length);
            _head += await stream.ReadAsync(_data.AsMemory(_head, length));
        }

        public async Task WriteToStreamAsync(Stream stream)
        {
            CheckDisposed();
            await stream.WriteAsync(_data.AsMemory(0, _head));
        }

        // New Feature: Peek without advancing
        public int PeekInt32()
        {
            CheckDisposed();
            if (_head + 4 > _capacity) return 0;
            return BinaryPrimitives.ReadInt32LittleEndian(_data.AsSpan(_head));
        }

        // New Feature: Compression
        public void Compress(System.IO.Compression.CompressionLevel level = System.IO.Compression.CompressionLevel.Optimal)
        {
            CheckDisposed();
            using var ms = new MemoryStream();
            using (var gzip = new System.IO.Compression.GZipStream(ms, level))
            {
                gzip.Write(_data, 0, _head);
            }
            _data = ms.ToArray();
            _capacity = _data.Length;
            _head = _capacity;
        }

        public void Decompress()
        {
            CheckDisposed();
            using var ms = new MemoryStream(_data, 0, _head);
            using var gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
            using var result = new MemoryStream();
            gzip.CopyTo(result);
            _data = result.ToArray();
            _capacity = _data.Length;
            _head = 0; // Reset head for reading
        }

        // Utility to check disposal state
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ByteStream));
        }
    }
}
