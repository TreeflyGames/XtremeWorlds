using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mirage.Sharp.Asfw
{
    public class ByteStream : IDisposable
    {
        private byte[] _data;
        private int _head;
        private int _capacity;
        private bool _isDisposed;

        // Constants for optimization and chaos scaling
        private const int DefaultInitialSize = 64; // Larger default for socket chaos
        private const int MinBufferSize = 4;
        private const int MaxPacketSize = 65536; // Cap for sanity—#GlowCoup precision

        // Public accessors with ref returns for socket compatibility
        public ref byte[] DataRef => ref _data; // Direct ref for socket sends
        public ref int HeadRef => ref _head;    // Direct ref for socket head

        // Properties for control, debugging, and chaos tracking
        public int Capacity => _capacity;
        public int Remaining => _capacity - _head;
        public bool IsAtEnd => _head >= _capacity;
        public int Position => _head;           // Alias for chaos navigation
        public bool IsEmpty => _head == 0;

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

        // ToArray for socket use—optimized with pooling
        public byte[] ToArray()
        {
            CheckDisposed();
            if (_head == 0) return Array.Empty<byte>();
            byte[] result = new byte[_head];
            _data.AsSpan(0, _head).CopyTo(result);
            return result;
        }

        // Optimized packet generation for sockets with header
        public byte[] ToPacket()
        {
            CheckDisposed();
            byte[] result = new byte[4 + _head];
            BinaryPrimitives.WriteInt32LittleEndian(result.AsSpan(0, 4), _head);
            _data.AsSpan(0, _head).CopyTo(result.AsSpan(4));
            return result;
        }

        // Reset for reuse—option to clear data
        public void Reset(bool clearData = false)
        {
            CheckDisposed();
            _head = 0;
            if (clearData) Array.Clear(_data, 0, _capacity);
        }

        // Dynamic resizing with chaos-aware growth
        private void EnsureCapacity(int additionalLength)
        {
            CheckDisposed();
            int required = _head + additionalLength;
            if (required <= _capacity) return;

            int newCapacity = Math.Max(_capacity * 2, required);
            if (newCapacity > MaxPacketSize) newCapacity = MaxPacketSize; // Cap chaos
            byte[] newData = new byte[newCapacity];
            _data.CopyTo(newData, 0); // Full copy for class safety
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
            CheckDisposed();
            int length = ReadInt32();
            if (length <= 0 || _head + length > _capacity)
                return Array.Empty<byte>();

            byte[] result = new byte[length];
            _data.AsSpan(_head, length).CopyTo(result);
            _head += length;
            return result;
        }

        public string ReadString(Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return encoding.GetString(ReadBytes());
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
        public void WriteChar(char value) => WriteInt16((short)value); // Explicit cast
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

        // Enhanced Socket Packet Parsing with Robustness
        public bool TryParsePacket(ReadOnlySpan<byte> packet, out ByteStream parsed)
        {
            parsed = null;
            if (packet.Length < 4) return false;

            int length = BinaryPrimitives.ReadInt32LittleEndian(packet);
            if (length < 0 || length > packet.Length - 4 || length > MaxPacketSize) return false;

            parsed = new ByteStream(packet.Slice(4, length).ToArray());
            return true;
        }

        // Enhanced Batch Write with Ref Support and Chaos
        public void WriteBatch<T>(IEnumerable<T> values, Action<ByteStream, T> writer)
        {
            CheckDisposed();
            int startPos = _head;
            WriteInt32(0); // Placeholder for count
            int count = 0;
            foreach (var value in values)
            {
                writer(this, value);
                count++;
            }
            int endPos = _head;
            _head = startPos;
            WriteInt32(count); // Update count
            _head = endPos;
        }

        // Enhanced Async Socket Send/Receive with Cancellation
        public async Task SendToSocketAsync(System.Net.Sockets.Socket socket, CancellationToken token = default)
        {
            CheckDisposed();
            token.ThrowIfCancellationRequested();
            byte[] array = ToArray();
            await socket.SendAsync(array, System.Net.Sockets.SocketFlags.None, token).ConfigureAwait(false);
        }

        public static async Task<ByteStream> ReceiveFromSocketAsync(System.Net.Sockets.Socket socket, int bufferSize = 1024, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            byte[] header = new byte[4];
            int received = await socket.ReceiveAsync(header, System.Net.Sockets.SocketFlags.None, token).ConfigureAwait(false);
            if (received < 4) return new ByteStream();

            int length = BinaryPrimitives.ReadInt32LittleEndian(header);
            if (length <= 0 || length > MaxPacketSize) return new ByteStream();

            byte[] data = new byte[length];
            int totalReceived = 0;
            while (totalReceived < length)
            {
                token.ThrowIfCancellationRequested();
                int bytesRead = await socket.ReceiveAsync(data.AsMemory(totalReceived, length - totalReceived), System.Net.Sockets.SocketFlags.None, token).ConfigureAwait(false);
                if (bytesRead == 0) break; // Connection closed
                totalReceived += bytesRead;
            }
            return new ByteStream(data);
        }

        // Direct Ref Socket Send with Cancellation
        public async Task SendToSocketRefAsync(System.Net.Sockets.Socket socket, CancellationToken token = default)
        {
            CheckDisposed();
            token.ThrowIfCancellationRequested();
            await socket.SendAsync(_data.AsMemory(0, _head), System.Net.Sockets.SocketFlags.None, token).ConfigureAwait(false);
        }

        // Enhanced Packet Validation with Chaos Check
        public bool ValidateIntegrity()
        {
            CheckDisposed();
            return _head <= _capacity && _head >= 0 && _data != null && _capacity <= MaxPacketSize;
        }

        // New Feature: Async Packet Splitting
        public async IAsyncEnumerable<ByteStream> SplitPacketAsync(int maxSize, CancellationToken token = default)
        {
            CheckDisposed();
            if (maxSize < 8) throw new ArgumentException("Max size must allow header + data.", nameof(maxSize));

            int dataLength = _head;
            int offset = 0;
            while (offset < dataLength)
            {
                token.ThrowIfCancellationRequested();
                int chunkSize = Math.Min(maxSize - 4, dataLength - offset);
                var stream = new ByteStream(chunkSize + 4);
                stream.WriteInt32(chunkSize);
                stream.WriteBlock(_data.AsSpan(offset, chunkSize));
                await Task.Yield(); // Ensure async yield
                yield return stream;
                offset += chunkSize;
            }
        }

        // New Feature: Async Packet Reassembly
        public static async Task<ByteStream> ReassemblePacketsAsync(IAsyncEnumerable<ByteStream> packets, CancellationToken token = default)
        {
            using var ms = new MemoryStream();
            await foreach (var packet in packets.ConfigureAwait(false).WithCancellation(token))
            {
                int length = packet.ReadInt32();
                ms.Write(packet.ReadBlock(length).ToArray());
            }
            return new ByteStream(ms.ToArray());
        }

        // New Feature: Packet Peek with Offset
        public byte PeekByte(int offset = 0)
        {
            CheckDisposed();
            int peekPos = _head + offset;
            return peekPos < _capacity ? _data[peekPos] : (byte)0;
        }

        // New Feature: Resize with Chaos Preservation
        public void Resize(int newSize, bool preserveData = true)
        {
            CheckDisposed();
            if (newSize < MinBufferSize) newSize = MinBufferSize;
            if (newSize == _capacity) return;

            byte[] newData = new byte[newSize];
            if (preserveData && _data != null)
            {
                int copyLength = Math.Min(_head, newSize);
                _data.AsSpan(0, copyLength).CopyTo(newData);
            }
            _data = newData;
            _capacity = newSize;
            _head = Math.Min(_head, _capacity);
        }

        // Enhanced Compression with Async Option
        public async Task CompressForSocketAsync(System.IO.Compression.CompressionLevel level = System.IO.Compression.CompressionLevel.Fastest, CancellationToken token = default)
        {
            CheckDisposed();
            token.ThrowIfCancellationRequested();
            byte[] originalData = ToArray();
            using var ms = new MemoryStream();
            await ms.WriteAsync(BitConverter.GetBytes(originalData.Length), token).ConfigureAwait(false);
            using (var gzip = new System.IO.Compression.GZipStream(ms, level, leaveOpen: true))
            {
                await gzip.WriteAsync(originalData, token).ConfigureAwait(false);
            }
            _data = ms.ToArray();
            _capacity = _data.Length;
            _head = _capacity;
        }

        public async Task DecompressFromSocketAsync(CancellationToken token = default)
        {
            CheckDisposed();
            token.ThrowIfCancellationRequested();
            byte[] compressedData = ToArray();
            using var ms = new MemoryStream(compressedData);
            byte[] lengthBytes = new byte[4];
            await ms.ReadAsync(lengthBytes, token).ConfigureAwait(false);
            int originalLength = BinaryPrimitives.ReadInt32LittleEndian(lengthBytes);
            using var gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
            _data = new byte[originalLength];
            await gzip.ReadAsync(_data, token).ConfigureAwait(false);
            _capacity = originalLength;
            _head = 0;
        }

        // Chaos Check
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(ByteStream));
        }
    }
}
