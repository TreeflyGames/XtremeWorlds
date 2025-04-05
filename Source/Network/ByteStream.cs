using System;
using System.Buffers;
using System.Buffers.Binary; // For BinaryPrimitives
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices; // For MemoryMarshal
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mirage.Sharp.Asfw
{
    public class ByteStream : IDisposable
    {
        private byte[] _buffer;
        private int _writePos; // Position where next write will occur (also the current length of written data)
        private int _readPos;  // Position from where next read will occur
        private bool _disposed;
        private bool _isRented;
        private const int DefaultInitialCapacity = 64; // Define default size

        /// <summary>
        /// Gets a read-only span over the data that has been written to the stream.
        /// </summary>
        public ReadOnlySpan<byte> WrittenData => _buffer.AsSpan(0, _writePos);

        /// <summary>
        /// Gets a read-only span over the data that has not yet been read.
        /// </summary>
        public ReadOnlySpan<byte> UnreadData => _buffer.AsSpan(_readPos, _writePos - _readPos);

        /// <summary>
        /// Gets the current write position (which is also the total number of bytes written).
        /// </summary>
        public int WritePosition => _writePos;

        /// <summary>
        /// Gets or sets the current read position.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is negative or greater than the number of bytes written.</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the stream is disposed.</exception>
        public int ReadPosition
        {
            get
            {
                CheckDisposed();
                return _readPos;
            }
            set
            {
                CheckDisposed();
                if (value < 0 || value > _writePos)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Read position must be between 0 and {nameof(Length)} ({_writePos}).");
                _readPos = value;
            }
        }

        /// <summary>
        /// Gets the number of bytes currently written to the stream.
        /// </summary>
        public int Length => _writePos;

        /// <summary>
        /// Gets the total capacity of the internal buffer.
        /// </summary>
        public int Capacity => _buffer?.Length ?? 0;

        /// <summary>
        /// Gets a value indicating whether the stream has been disposed.
        /// </summary>
        public bool IsDisposed => _disposed;

        /// <summary>
        /// Gets a value indicating whether the read position is at the end of the written data.
        /// </summary>
        public bool EndOfStream => _readPos >= _writePos;

        /// <summary>
        /// Initializes a new ByteStream with a rented buffer of the specified initial capacity.
        /// </summary>
        /// <param name="initialCapacity">The initial capacity of the buffer (default is 64).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if initialCapacity is not positive.</exception>
        public ByteStream(int initialCapacity = DefaultInitialCapacity)
        {
            if (initialCapacity < 1)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity), "Initial capacity must be positive.");

            _buffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
            _writePos = 0;
            _readPos = 0;
            _disposed = false;
            _isRented = true;
        }

        /// <summary>
        /// Initializes a new ByteStream using the provided byte array as the buffer **for reading**.
        /// The stream does NOT take ownership of the buffer (it won't be returned to ArrayPool).
        /// The initial write position and length are set to the array's length, and read position to 0.
        /// </summary>
        /// <param name="bufferToRead">The byte array containing data to be read.</param>
        /// <exception cref="ArgumentNullException">Thrown if bufferToRead is null.</exception>
        public ByteStream(byte[] bufferToRead)
        {
            _buffer = bufferToRead ?? throw new ArgumentNullException(nameof(bufferToRead));
            _writePos = bufferToRead.Length; // Assume buffer is full for reading
            _readPos = 0;
            _disposed = false;
            _isRented = false; // We don't own this buffer
        }

        /// <summary>
        /// Creates a ByteStream from a ReadOnlySpan, copying the data into a new rented buffer.
        /// </summary>
        public static ByteStream FromSpan(ReadOnlySpan<byte> data)
        {
            var stream = new ByteStream(data.Length > 0 ? data.Length : DefaultInitialCapacity);
            stream.WriteBlock(data);
            stream.ReadPosition = 0; // Reset read position to start after writing
            return stream;
        }


        ~ByteStream()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the ByteStream, returning the buffer to the array pool if it was rented.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevent finalizer from running
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (none in this case, but good practice)
                }

                // Free unmanaged resources (the rented buffer)
                if (_isRented && _buffer != null) // No need to check _buffer.Length > 0
                {
                    ArrayPool<byte>.Shared.Return(_buffer);
                    _isRented = false;
                }

                _buffer = null; // Help GC
                _writePos = 0;
                _readPos = 0;
                _disposed = true;
            }
        }

        private void CheckDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ByteStream));
        }

        /// <summary>
        /// Ensures the internal buffer has at least the specified capacity. If not, it resizes the buffer,
        /// renting a new one from the pool and copying existing data.
        /// </summary>
        /// <param name="requiredCapacity">The minimum required capacity.</param>
        private void EnsureCapacity(int requiredCapacity)
        {
            CheckDisposed();
            if (requiredCapacity <= Capacity) // Use Capacity property
                return;

            // Calculate new size (double, but at least the required amount)
            int newSize = Math.Max(Capacity * 2, requiredCapacity);
            byte[] newBuffer = ArrayPool<byte>.Shared.Rent(newSize);

            // Copy existing written data
            if (_writePos > 0)
            {
                Buffer.BlockCopy(_buffer, 0, newBuffer, 0, _writePos);
            }

            // Return old buffer if it was rented
            if (_isRented && _buffer != null)
            {
                ArrayPool<byte>.Shared.Return(_buffer);
            }

            _buffer = newBuffer;
            _isRented = true; // The new buffer is always rented
        }

        /// <summary>
        /// Resets the stream for reuse. Sets both read and write positions to zero.
        /// Does not release or change the underlying buffer.
        /// </summary>
        public void Reset()
        {
            CheckDisposed();
            _writePos = 0;
            _readPos = 0;
        }

        /// <summary>
        /// Returns a new array containing the data currently written to the stream.
        /// </summary>
        public byte[] ToArray()
        {
            CheckDisposed();
            return WrittenData.ToArray(); // Use property which slices correctly
        }

        /// <summary>
        /// Creates a packet (byte array) with a 4-byte little-endian length prefix
        /// followed by the data currently written to the stream.
        /// </summary>
        public byte[] ToPacket()
        {
            CheckDisposed();
            int payloadLength = _writePos;
            byte[] packet = new byte[4 + payloadLength];
            // Use BinaryPrimitives for efficient, endianness-aware writing
            BinaryPrimitives.WriteInt32LittleEndian(packet.AsSpan(0, 4), payloadLength);
            if (payloadLength > 0)
            {
                Buffer.BlockCopy(_buffer, 0, packet, 4, payloadLength);
            }
            return packet;
        }

        /// <summary>
        /// Sends the stream's written data as a length-prefixed packet to the specified socket asynchronously.
        /// </summary>
        /// <exception cref="SocketException">Thrown if the socket is not connected or another socket error occurs.</exception>
        /// <exception cref="ArgumentNullException">Thrown if socket is null.</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the stream is disposed.</exception>
        public async ValueTask SendToSocketAsync(Socket socket, CancellationToken cancellationToken = default)
        {
            CheckDisposed();
            if (socket == null) throw new ArgumentNullException(nameof(socket));
            if (!socket.Connected) throw new SocketException((int)SocketError.NotConnected);

            byte[] packet = ToPacket(); // Creates the length-prefixed packet
            // Send the entire packet as a single memory segment
            await socket.SendAsync(new ReadOnlyMemory<byte>(packet), SocketFlags.None, cancellationToken).ConfigureAwait(false);
        }

        // --- Reading Methods ---

        /// <summary>
        /// Checks if the requested number of bytes can be read from the current position.
        /// </summary>
        /// <param name="count">Number of bytes required.</param>
        /// <exception cref="EndOfStreamException">Thrown if not enough bytes remain.</exception>
        private void EnsureBytesAvailable(int count)
        {
            CheckDisposed();
            if (_readPos + count > _writePos)
            {
                throw new EndOfStreamException($"Cannot read {count} bytes from position {_readPos}, only {_writePos - _readPos} bytes available.");
            }
        }

        /// <summary>
        /// Reads a block of bytes of the specified size from the current read position,
        /// advancing the position.
        /// </summary>
        /// <param name="size">The number of bytes to read.</param>
        /// <returns>A ReadOnlySpan covering the read bytes.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if size is negative.</exception>
        /// <exception cref="EndOfStreamException">Thrown if not enough bytes are available.</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the stream is disposed.</exception>
        public ReadOnlySpan<byte> ReadBlock(int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Size cannot be negative.");
            EnsureBytesAvailable(size);
            var span = _buffer.AsSpan(_readPos, size);
            _readPos += size;
            return span;
        }

        /// <summary>
        /// Reads a 4-byte little-endian length prefix, then reads the specified number of bytes.
        /// Advances the read position past the length prefix and the data.
        /// </summary>
        /// <returns>A ReadOnlySpan covering the read data bytes (excluding the length prefix).</returns>
        /// <exception cref="EndOfStreamException">Thrown if not enough bytes are available for length or data.</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the stream is disposed.</exception>
        public ReadOnlySpan<byte> ReadBytes()
        {
            int length = ReadInt32(); // Reads 4 bytes, advances _readPos
            if (length < 0) throw new IOException("Invalid negative length received for byte array.");
            if (length == 0) return ReadOnlySpan<byte>.Empty;

            return ReadBlock(length); // Reads 'length' bytes, advances _readPos
        }

        /// <summary>
        /// Reads a length-prefixed UTF-8 encoded string from the current read position.
        /// </summary>
        /// <returns>The decoded string.</returns>
        public string ReadString()
        {
            var bytes = ReadBytes(); // Reads length prefix and data
            return bytes.Length > 0 ? Encoding.UTF8.GetString(bytes) : string.Empty;
        }

        /// <summary>
        /// Reads the next byte from the stream without advancing the read position.
        /// </summary>
        /// <returns>The byte at the current read position.</returns>
        /// <exception cref="EndOfStreamException">Thrown if the stream is at the end.</exception>
        public byte PeekByte()
        {
            EnsureBytesAvailable(1);
            return _buffer[_readPos];
        }

        // Optimized Primitive Readers
        public char ReadChar() => BitConverter.ToChar(ReadBlock(sizeof(char))); // Usually 2 bytes
        public byte ReadByte() => ReadBlock(sizeof(byte))[0]; // 1 byte
        public bool ReadBoolean() => ReadBlock(sizeof(bool))[0] != 0; // 1 byte
        public short ReadInt16() { EnsureBytesAvailable(sizeof(short)); var val = BinaryPrimitives.ReadInt16LittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(short); return val; }
        public ushort ReadUInt16() { EnsureBytesAvailable(sizeof(ushort)); var val = BinaryPrimitives.ReadUInt16LittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(ushort); return val; }
        public int ReadInt32() { EnsureBytesAvailable(sizeof(int)); var val = BinaryPrimitives.ReadInt32LittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(int); return val; }
        public uint ReadUInt32() { EnsureBytesAvailable(sizeof(uint)); var val = BinaryPrimitives.ReadUInt32LittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(uint); return val; }
        public float ReadSingle() { EnsureBytesAvailable(sizeof(float)); var val = BinaryPrimitives.ReadSingleLittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(float); return val; }
        public long ReadInt64() { EnsureBytesAvailable(sizeof(long)); var val = BinaryPrimitives.ReadInt64LittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(long); return val; }
        public ulong ReadUInt64() { EnsureBytesAvailable(sizeof(ulong)); var val = BinaryPrimitives.ReadUInt64LittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(ulong); return val; }
        public double ReadDouble() { EnsureBytesAvailable(sizeof(double)); var val = BinaryPrimitives.ReadDoubleLittleEndian(_buffer.AsSpan(_readPos)); _readPos += sizeof(double); return val; }

        // --- Writing Methods ---

        /// <summary>
        /// Writes a block of bytes to the buffer at the current write position, resizing if necessary,
        /// and advances the write position.
        /// </summary>
        /// <param name="bytes">The block of bytes to write.</param>
        /// <exception cref="ObjectDisposedException">Thrown if the stream is disposed.</exception>
        public void WriteBlock(ReadOnlySpan<byte> bytes)
        {
            if (bytes.IsEmpty) return;
            CheckDisposed();
            EnsureCapacity(_writePos + bytes.Length);
            bytes.CopyTo(_buffer.AsSpan(_writePos));
            _writePos += bytes.Length;
        }

        /// <summary>
        /// Writes a 4-byte little-endian length prefix followed by the provided bytes.
        /// </summary>
        /// <param name="value">The bytes to write.</param>
        public void WriteBytes(ReadOnlySpan<byte> value)
        {
            WriteInt32(value.Length); // Write length prefix first
            WriteBlock(value);        // Then write the data
        }

        /// <summary>
        /// Writes the string as a length-prefixed UTF-8 encoded byte sequence.
        /// </summary>
        /// <param name="value">The string to write. Null is written as a zero length.</param>
        public void WriteString(string value)
        {
            CheckDisposed();
            if (string.IsNullOrEmpty(value))
            {
                WriteInt32(0); // Write zero length prefix
                return;
            }

            // Calculate required size for UTF8 bytes + length prefix
            int byteCount = Encoding.UTF8.GetByteCount(value);
            EnsureCapacity(_writePos + 4 + byteCount);

            // Write length prefix
            WriteInt32(byteCount);

            // Encode directly into the buffer
            int bytesWritten = Encoding.UTF8.GetBytes(value, _buffer.AsSpan(_writePos));
            _writePos += bytesWritten; // Advance write position by actual bytes written
        }

        // Optimized Primitive Writers
        public void WriteChar(char value) { EnsureCapacity(_writePos + sizeof(char)); BinaryPrimitives.WriteUInt16LittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(char); }
        public void WriteByte(byte value) { EnsureCapacity(_writePos + sizeof(byte)); _buffer[_writePos++] = value; }
        public void WriteBoolean(bool value) { EnsureCapacity(_writePos + sizeof(bool)); _buffer[_writePos++] = (byte)(value ? 1 : 0); }
        public void WriteInt16(short value) { EnsureCapacity(_writePos + sizeof(short)); BinaryPrimitives.WriteInt16LittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(short); }
        public void WriteUInt16(ushort value) { EnsureCapacity(_writePos + sizeof(ushort)); BinaryPrimitives.WriteUInt16LittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(ushort); }
        public void WriteInt32(int value) { EnsureCapacity(_writePos + sizeof(int)); BinaryPrimitives.WriteInt32LittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(int); }
        public void WriteUInt32(uint value) { EnsureCapacity(_writePos + sizeof(uint)); BinaryPrimitives.WriteUInt32LittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(uint); }
        public void WriteSingle(float value) { EnsureCapacity(_writePos + sizeof(float)); BinaryPrimitives.WriteSingleLittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(float); }
        public void WriteInt64(long value) { EnsureCapacity(_writePos + sizeof(long)); BinaryPrimitives.WriteInt64LittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(long); }
        public void WriteUInt64(ulong value) { EnsureCapacity(_writePos + sizeof(ulong)); BinaryPrimitives.WriteUInt64LittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(ulong); }
        public void WriteDouble(double value) { EnsureCapacity(_writePos + sizeof(double)); BinaryPrimitives.WriteDoubleLittleEndian(_buffer.AsSpan(_writePos), value); _writePos += sizeof(double); }


        /// <summary>
        /// Sets the read position within the stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="origin">A value indicating the reference point used to obtain the new position.</param>
        /// <returns>The new read position within the stream.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the resulting position is invalid.</exception>
        /// <exception cref="ArgumentException">Thrown if origin is invalid.</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the stream is disposed.</exception>
        public int SeekRead(int offset, SeekOrigin origin)
        {
            CheckDisposed();
            int newReadPos;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    newReadPos = offset;
                    break;
                case SeekOrigin.Current:
                    newReadPos = _readPos + offset;
                    break;
                case SeekOrigin.End: // Relative to the end of *written* data
                    newReadPos = _writePos + offset;
                    break;
                default:
                    throw new ArgumentException("Invalid seek origin.", nameof(origin));
            }

            if (newReadPos < 0 || newReadPos > _writePos)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), $"Attempted to seek to position {newReadPos}, which is outside the valid range [0, {_writePos}].");
            }

            _readPos = newReadPos;
            return _readPos;
        }
    }
}
