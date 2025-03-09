using System;
using System.Buffers;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mirage.Sharp.Asfw
{
    public struct ByteStream : IDisposable
    {
        private byte[] _data;
        private int _head;
        private bool _disposed;

        public ReadOnlySpan<byte> Data => _data.AsSpan(0, _head);
        public int Length => _head;
        public int Capacity => _data.Length;
        public bool IsDisposed => _disposed;

        public ByteStream(int initialSize = 64) // Increased default size for better performance
        {
            if (initialSize < 1)
                throw new ArgumentException("Initial size must be positive", nameof(initialSize));

            _data = ArrayPool<byte>.Shared.Rent(initialSize);
            _head = 0;
            _disposed = false;
        }

        public ByteStream(byte[] bytes)
        {
            _data = bytes ?? throw new ArgumentNullException(nameof(bytes));
            _head = bytes.Length;
            _disposed = false;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_data != null && _data.Length > 0)
                {
                    ArrayPool<byte>.Shared.Return(_data);
                }
                _data = null;
                _head = 0;
                _disposed = true;
            }
        }

        public byte[] ToArray()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ByteStream));
            return _data.AsSpan(0, _head).ToArray();
        }

        public byte[] ToPacket()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ByteStream));
            byte[] packet = new byte[4 + _head];
            BitConverter.TryWriteBytes(packet.AsSpan(0, 4), _head);
            Buffer.BlockCopy(_data, 0, packet, 4, _head);
            return packet;
        }

        public async ValueTask SendToSocketAsync(Socket socket, CancellationToken cancellationToken = default)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ByteStream));
            if (socket == null) throw new ArgumentNullException(nameof(socket));
            if (!socket.Connected) throw new SocketException((int)SocketError.NotConnected);

            byte[] packet = ToPacket();
            await socket.SendAsync(packet, SocketFlags.None, cancellationToken).ConfigureAwait(false);
        }

        private void EnsureCapacity(int requiredSize)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ByteStream));
            if (requiredSize <= _data.Length)
                return;

            int newSize = Math.Max(_data.Length * 2, requiredSize);
            byte[] newBuffer = ArrayPool<byte>.Shared.Rent(newSize);
            Buffer.BlockCopy(_data, 0, newBuffer, 0, _head);
            ArrayPool<byte>.Shared.Return(_data);
            _data = newBuffer;
        }

        public ReadOnlySpan<byte> ReadBlock(int size)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ByteStream));
            if (size < 0 || _head + size > _data.Length)
                return ReadOnlySpan<byte>.Empty;

            var span = _data.AsSpan(_head, size);
            _head += size;
            return span;
        }

        public ReadOnlySpan<byte> ReadBytes()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ByteStream));
            if (_head + 4 > _data.Length)
                return ReadOnlySpan<byte>.Empty;

            int length = BitConverter.ToInt32(_data, _head);
            _head += 4;

            if (length < 0 || _head + length > _data.Length)
                return ReadOnlySpan<byte>.Empty;

            var span = _data.AsSpan(_head, length);
            _head += length;
            return span;
        }

        public string ReadString()
        {
            var bytes = ReadBytes();
            return bytes.Length > 0 ? Encoding.UTF8.GetString(bytes) : string.Empty;
        }

        public char ReadChar() => BitConverter.ToChar(ReadBlock(2));
        public byte ReadByte() => ReadBlock(1)[0];
        public bool ReadBoolean() => ReadBlock(1)[0] != 0;
        public short ReadInt16() => BitConverter.ToInt16(ReadBlock(2));
        public ushort ReadUInt16() => BitConverter.ToUInt16(ReadBlock(2));
        public int ReadInt32() => BitConverter.ToInt32(ReadBlock(4));
        public uint ReadUInt32() => BitConverter.ToUInt32(ReadBlock(4));
        public float ReadSingle() => BitConverter.ToSingle(ReadBlock(4));
        public long ReadInt64() => BitConverter.ToInt64(ReadBlock(8));
        public ulong ReadUInt64() => BitConverter.ToUInt64(ReadBlock(8));
        public double ReadDouble() => BitConverter.ToDouble(ReadBlock(8));

        public void WriteBlock(ReadOnlySpan<byte> bytes)
        {
            EnsureCapacity(_head + bytes.Length);
            bytes.CopyTo(_data.AsSpan(_head));
            _head += bytes.Length;
        }

        public void WriteBytes(ReadOnlySpan<byte> value)
        {
            WriteBlock(BitConverter.GetBytes(value.Length));
            WriteBlock(value);
        }

        public void WriteString(string value)
        {
            if (value == null)
            {
                WriteBlock(BitConverter.GetBytes(0));
                return;
            }

            WriteBytes(Encoding.UTF8.GetBytes(value));
        }

        public void WriteChar(char value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteByte(byte value) => WriteBlock(new[] { value });
        public void WriteBoolean(bool value) => WriteBlock(new[] { (byte)(value ? 1 : 0) });
        public void WriteInt16(short value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteUInt16(ushort value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteInt32(int value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteUInt32(uint value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteSingle(float value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteInt64(long value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteUInt64(ulong value) => WriteBlock(BitConverter.GetBytes(value));
        public void WriteDouble(double value) => WriteBlock(BitConverter.GetBytes(value));
    }
}
