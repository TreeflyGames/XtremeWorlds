using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;
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
        private const int DefaultInitialSize = 32; // Larger for socket packets
        private const int MinBufferSize = 4;

        // Public accessors for socket compatibility
        public byte[] Data
        {
            get => _data ?? Array.Empty<byte>();
            set
            {
                CheckDisposed();
                _data = value ?? throw new ArgumentNullException(nameof(value));
                _capacity = _data.Length;
                _head = Math.Min(_head, _capacity); // Clamp head to new capacity
            }
        }

        public int Head
        {
            get => _head;
            set
            {
                CheckDisposed();
                if (value < 0 || value > _capacity)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _head = value;
            }
        }

        // Properties for control and debugging
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

        // Optimized packet generation for sockets
        public byte[] ToPacket()
        {
            CheckDisposed();
            byte[] result = new byte[4 + _head];
            BinaryPrimitives.WriteInt32LittleEndian(result.AsSpan(0, 4), _head);
            _data.AsSpan(0, _head).CopyTo(result.AsSpan(4));
            return result;
        }

        // Reset for reuse (socket-friendly)
        public void Reset()
        {
            CheckDisposed();
            _head = 0;
        }

        // Dynamic resizing with socket-aware growth
        private void EnsureCapacity(int additionalLength)
        {
            CheckDisposed();
            int required = _head + additionalLength;
            if (required <= _capacity) return;

            int newCapacity = Math.Max(_capacity * 2, required);
            byte[] newData = new byte[newCapacity];
            _data.AsSpan(0, _head).CopyTo(newData);
            _data = newData;
            _capacity = newCapacity;
        }

        // Read methods with Span
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
            return length <= 0 ? string.Empty : encoding.GetString(ReadBlock(length));
        }

        public byte ReadByte() => _head < _capacity ? _data[_head++] : (byte)0;
        public bool ReadBoolean() => ReadByte() != 0;
        public char ReadChar() => BinaryPrimitives.ReadInt16LittleEndian(ReadBlock(2));
        public short ReadInt16() => BinaryPrimitives.ReadInt16LittleEndian(ReadBlock(2));
        public ushort ReadUInt16() => BinaryPrimitives.ReadUInt16LittleEndian(ReadBlock(2));
        public int ReadInt32() => BinaryPrimitives.ReadInt32LittleEndian(ReadBlock(4));
        public uint ReadUInt32() => BinaryPrimitives.ReadUInt32LittleEndian(ReadBlock(4));
        public float ReadSingle() => BinaryPrimitives.ReadSingleLittleEndian(ReadBlock(4));
        public long ReadInt64() => BinaryPrimitives.ReadInt64LittleEndian(ReadBlock(8));
        public ulong ReadUInt64() => BinaryPrimitives.ReadUInt64LittleEndian(ReadBlock(8));
        public double ReadDouble() => BinaryPrimitives.ReadDoubleLittleEndian(ReadBlock(8));

        // Write methods with Span
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
            WriteBytes(encoding.GetBytes(value));
        }

        public void WriteByte(byte value) { EnsureCapacity(1); _data[_head++] = value; }
        public void WriteBoolean(bool value) => WriteByte((byte)(value ? 1 : 0));
        public void WriteChar(char value) => BinaryPrimitives.WriteInt16LittleEndian(WriteSpan(2), value);
        public void WriteInt16(short value) => BinaryPrimitives.WriteInt16LittleEndian(WriteSpan(2), value);
        public void WriteUInt16(ushort value) => BinaryPrimitives.WriteUInt16LittleEndian(WriteSpan(2), value);
        public void WriteInt32(int value) => BinaryPrimitives.WriteInt32LittleEndian(WriteSpan(4), value);
        public void WriteUInt32(uint value) => BinaryPrimitives.WriteUInt32LittleEndian(WriteSpan(4), value);
        public void WriteSingle(float value) => BinaryPrimitives.WriteSingleLittleEndian(WriteSpan(4), value);
        public void WriteInt64(long value) => BinaryPrimitives.WriteInt64LittleEndian(WriteSpan(8), value);
        public void WriteUInt64(ulong value) => BinaryPrimitives.WriteUInt64LittleEndian(WriteSpan(8), value);
        public void WriteDouble(double value) => BinaryPrimitives.WriteDoubleLittleEndian(WriteSpan(8), value);

        private Span<byte> WriteSpan(int size)
        {
            EnsureCapacity(size);
            var span = _data.AsSpan(_head, size);
            _head += size;
            return span;
        }

        // Enhanced Feature: Socket Packet Parsing
        public bool TryParsePacket(ReadOnlySpan<byte> packet, out ByteStream parsed)
        {
            parsed = default;
            if (packet.Length < 4) return false;

            int length = BinaryPrimitives.ReadInt32LittleEndian(packet);
            if (length < 0 || length > packet.Length - 4) return false;

            parsed = new ByteStream(packet.Slice(4, length).ToArray());
            return true;
        }

        // New Feature: Batch Write for Socket Efficiency
        public void WriteBatch<T>(ReadOnlySpan<T> values, Action<ByteStream, T> writer)
        {
            CheckDisposed();
            WriteInt32(values.Length);
            foreach (var value in values)
                writer(this, value);
        }

        // New Feature: Async Socket Send/Receive
        public async Task SendToSocketAsync(System.Net.Sockets.Socket socket)
        {
            CheckDisposed();
            byte[] packet = ToPacket();
            await socket.SendAsync(packet.AsMemory(), System.Net.Sockets.SocketFlags.None);
        }

        public static async Task<ByteStream> ReceiveFromSocketAsync(System.Net.Sockets.Socket socket, int bufferSize = 1024)
        {
            byte[] header = new byte[4];
            int received = await socket.ReceiveAsync(header.AsMemory(), System.Net.Sockets.SocketFlags.None);
            if (received < 4) return new ByteStream();

            int length = BinaryPrimitives.ReadInt32LittleEndian(header);
            if (length <= 0) return new ByteStream();

            byte[] data = new byte[length];
            int totalReceived = 0;
            while (totalReceived < length)
            {
                int bytesRead = await socket.ReceiveAsync(data.AsMemory(totalReceived, length - totalReceived), System.Net.Sockets.SocketFlags.None);
                if (bytesRead == 0) break; // Connection closed
                totalReceived += bytesRead;
            }
            return new ByteStream(data);
        }

        // New Feature: Packet Validation
        public bool ValidateIntegrity()
        {
            CheckDisposed();
            return _head <= _capacity && _data != null;
        }

        // Enhanced Feature: Compression with Socket Prep
        public void CompressForSocket(System.IO.Compression.CompressionLevel level = System.IO.Compression.CompressionLevel.Fastest)
        {
            CheckDisposed();
            using var ms = new MemoryStream();
            ms.Write(BitConverter.GetBytes(_head)); // Store original length
            using (var gzip = new System.IO.Compression.GZipStream(ms, level))
            {
                gzip.Write(_data, 0, _head);
            }
            _data = ms.ToArray();
            _capacity = _data.Length;
            _head = _capacity;
        }

        public void DecompressFromSocket()
        {
            CheckDisposed();
            using var ms = new MemoryStream(_data);
            int originalLength = BinaryPrimitives.ReadInt32LittleEndian(ms.ReadBytes(4));
            using var gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
            _data = new byte[originalLength];
            gzip.Read(_data, 0, originalLength);
            _capacity = originalLength;
            _head = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ByteStream));
        }
    }
}
