﻿using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    namespace Mirage.Sharp.Asfw.Network
    {
      public sealed class NetworkServer : IDisposable
      {
        public Dictionary<int, Socket> _socket;
        public List<int> _unsignedIndex;
        private Socket _listener;
        private IAsyncResult _pendingAccept;
        private int _packetCount;
        private int _packetSize;
        public NetworkServer.DataArgs[] PacketID;

        public int BufferLimit { get; set; }

        public int ClientLimit { get; }

        public bool IsListening { get; private set; }

        public int HighIndex { get; private set; }

        public int MinimumIndex { get; set; }

        public List<int> ConnectionID() => this._socket == null ? new List<int>() : new List<int>((IEnumerable<int>) this._socket.Keys);

        public event NetworkServer.AccessArgs AccessCheck;

        public event NetworkServer.ConnectionArgs ConnectionReceived;

        public event NetworkServer.ConnectionArgs ConnectionLost;

        public event NetworkServer.CrashReportArgs CrashReport;

        public event NetworkServer.PacketInfoArgs PacketReceived;

        public event NetworkServer.TrafficInfoArgs TrafficReceived;

        public NetworkServer(int packetCount, int packetSize = 8192, int clientLimit = 0)
        {
          if (this._listener != null || this._socket != null)
            return;

          if (packetSize < 0)
            packetSize = 8192;

          this._listener?.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
          this._socket = new Dictionary<int, Socket>();
          this._unsignedIndex = new List<int>();
          this.ClientLimit = clientLimit;
          this._packetCount = packetCount;
          this._packetSize = packetSize;
          this.PacketID = new NetworkServer.DataArgs[packetCount];
        }

        public void Dispose()
        {
          if (this._socket == null)
            return;
          this.StopListening();
          if (this._socket.Count > 0)
          {
            foreach (int key in this._socket.Keys)
              this.Disconnect(key);
          }
          this._socket.Clear();
          this._socket = (Dictionary<int, Socket>) null;
          this.PacketID = (NetworkServer.DataArgs[]) null;
          this._unsignedIndex.Clear();
          this._unsignedIndex = (List<int>) null;
          this.AccessCheck = (NetworkServer.AccessArgs) null;
          this.ConnectionReceived = (NetworkServer.ConnectionArgs) null;
          this.ConnectionLost = (NetworkServer.ConnectionArgs) null;
          this.CrashReport = (NetworkServer.CrashReportArgs) null;
          this.PacketReceived = (NetworkServer.PacketInfoArgs) null;
          this.TrafficReceived = (NetworkServer.TrafficInfoArgs) null;
          this.PacketID = (NetworkServer.DataArgs[]) null;
        }

        public bool IsConnected(int index)
        {
          if (this._socket == null || !this._socket.ContainsKey(index))
            return false;

          if (this._socket[index].Connected)
            return true;

          this.Disconnect(index);
          return false;
        }

        public string GetIPv4() => Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();

        public string ClientIP(int index) => !this.IsConnected(index) ? "[Null]" : ((IPEndPoint) this._socket[index].RemoteEndPoint).Address.ToString();

        public void Disconnect(int index)
        {
            if (_socket == null || !_socket.ContainsKey(index))
                return;

            // Retrieve the socket from the dictionary
            Socket socket = _socket[index];

            if (socket == null)
            {
                // Remove the entry if the socket is null
                _socket.Remove(index);
                _unsignedIndex.Add(index);
            }
            else
            {
                try
                {
                    // Initiate the asynchronous disconnect
                    socket.BeginDisconnect(false, DoDisconnect, index);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during disconnect: {ex.Message}");

                    // Ensure cleanup if an error occurs
                    ForceDisconnect(index);
                }
            }
        }

        // Callback for handling the end of the disconnect
        private void DoDisconnect(IAsyncResult ar)
        {
            if (_socket is null)
            {
                return;
            }

            int index = (int)ar.AsyncState;

            try
            {
                // Complete the disconnection process
                if (_socket.ContainsKey(index) && _socket[index] != null)
                {
                    _socket[index].EndDisconnect(ar);
                    _socket[index].Close();
                }

                // Remove the socket and mark the index as available
                _socket.Remove(index);
                _unsignedIndex.Add(index);

                // Trigger the ConnectionLost event
                NetworkServer.ConnectionArgs connectionLost = this.ConnectionLost;
                if (connectionLost != null)
                {
                    connectionLost(index);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DoDisconnect: {ex.Message}");
            }
        }

        // Fallback method to forcefully remove the socket if needed
        private void ForceDisconnect(int index)
        {
          if (_socket.ContainsKey(index))
          {
            try
            {
              _socket[index]?.Close();
            }
            catch (Exception ex)
            {
              Console.WriteLine($"Error during force disconnect: {ex.Message}");
            }

            // Remove the socket and mark the index as available
            _socket.Remove(index);
            _unsignedIndex.Add(index);
          }
        }
    
        private int FindEmptySlot(int startIndex)
        {
            using (List<int>.Enumerator enumerator = this._unsignedIndex.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    if (this.HighIndex < current)
                    {
                        this.HighIndex = current;
                    }
                    this._unsignedIndex.Remove(current);
                    return current;
                }
            }

            if (this._socket.Count == 0)
            {
                this.HighIndex = startIndex + 1;
                return startIndex;
            }

            ++this.HighIndex;
            return this.HighIndex;
        }

        public async System.Threading.Tasks.Task StartListeningAsync(int port, int backlog)
        {
            if (this._socket == null || this.IsListening || this._listener != null)
                return;
            this._listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._listener.NoDelay = true;
            this._listener?.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true); // Enable SO_KEEPALIVE
            this._listener.SendTimeout = 0; // No timeout
            this._listener.ReceiveTimeout = 0;
            this._listener.Bind(new IPEndPoint(IPAddress.Any, port));
            this.IsListening = true;
            this._listener.Listen(backlog);
            this.ListenManager();
        }

        public void StopListening()
        {
            if (!this.IsListening || this._socket == null)
            return;
            this.IsListening = false;
            if (this._listener == null)
            return;
            this._listener.Close();
            this._listener.Dispose();
            this._listener = (Socket) null;
        }

        private void DoAcceptClient(IAsyncResult ar)
        {
            try
            {
                Socket socket = this.EndAccept(ar);
                if (socket != null)
                {
                    int emptySlot = this.FindEmptySlot(this.MinimumIndex);
                    this._socket.TryAdd(emptySlot, socket);
                    this._socket[emptySlot].ReceiveBufferSize = this._packetSize;
                    this._socket[emptySlot].SendBufferSize = this._packetSize;
                    this.BeginReceiveData(emptySlot);
                    NetworkServer.ConnectionArgs connectionReceived = this.ConnectionReceived;
                    if (connectionReceived != null)
                        connectionReceived(emptySlot);
                }
            }
            catch
            {
                return;
            }
            this.ListenManager();
        }

        private Socket EndAccept(IAsyncResult ar = null)
        {
            IAsyncResult asyncResult = ar ?? this._pendingAccept;
            if (asyncResult == null || this._listener == null)
            return (Socket) null;
            this._pendingAccept = (IAsyncResult) null;
            return this._listener.EndAccept(asyncResult);
        }

        private void ListenManager()
        {
            if (!this.IsListening || this._listener == null || this._pendingAccept != null || this.ClientLimit > 0 && this.ClientLimit <= this._socket.Count)
            return;
            this._pendingAccept = this._listener.BeginAccept(new AsyncCallback(this.DoAcceptClient), (object) null);
        }

        private void BeginReceiveData(int index)
        {
            NetworkServer.ReceiveState state = new NetworkServer.ReceiveState(index, this._packetSize);
            this._socket[index].BeginReceive(state.Buffer, 0, this._packetSize, SocketFlags.None, new AsyncCallback(this.DoReceive), (object) state);
        }

        private void DoReceive(IAsyncResult ar)
        {
            if (this._socket == null)
                return;

            if (!(ar.AsyncState is NetworkServer.ReceiveState asyncState))
            {
                Console.WriteLine("Error: AsyncState is not of type ReceiveState.");
                return;
            }

            if (!_socket.ContainsKey(asyncState.Index))
            {
                // Do not disconnect here, just return
                return;
            }

            int receivedLength;
            try
            {
                receivedLength = _socket[asyncState.Index].EndReceive(ar);
            }
            catch (SocketException ex)
            {
                // Only disconnect for fatal errors
                if (ex.SocketErrorCode == SocketError.ConnectionReset ||
                    ex.SocketErrorCode == SocketError.ConnectionAborted ||
                    ex.SocketErrorCode == SocketError.Shutdown)
                {
                    HandleSocketError(asyncState, "ConnectionForciblyClosedException", ex);
                    return;
                }
                // For other errors, you may want to log and try to continue
                Console.WriteLine($"Non-fatal SocketException: {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                HandleSocketError(asyncState, "ConnectionForciblyClosedException", ex);
                return;
            }

            // If 0 bytes received, the connection is closed by the client
            if (receivedLength == 0)
            {
                // Do not disconnect immediately, just return
                return;
            }

            this.TrafficReceived?.Invoke(receivedLength, ref asyncState.Buffer);
            asyncState.PacketCount++;

            AppendToRingBuffer(ref asyncState, receivedLength);

            if (this.BufferLimit > 0 && asyncState.RingBuffer.Length > this.BufferLimit)
            {
                DisconnectAndDispose(asyncState.Index, asyncState);
                return;
            }

            if (!_socket[asyncState.Index].Connected)
            {
                DisconnectAndDispose(asyncState.Index, asyncState);
                return;
            }

            this.PacketHandler(ref asyncState);

            asyncState.Buffer = new byte[this._packetSize];

            if (_socket.ContainsKey(asyncState.Index) && _socket[asyncState.Index].Connected)
            {
                _socket[asyncState.Index].BeginReceive(
                    asyncState.Buffer, 0, this._packetSize, SocketFlags.None,
                    new AsyncCallback(this.DoReceive), asyncState);
            }
        
        }

        // Helper to handle socket errors and cleanup
        private void HandleSocketError(NetworkServer.ReceiveState state, string error, Exception ex = null)
        {
            this.CrashReport?.Invoke(state.Index, error);
            if (ex != null) Console.WriteLine($"Exception: {ex.Message}");
            DisconnectAndDispose(state.Index, state);
        }

        // Helper to disconnect and dispose
        private void DisconnectAndDispose(int index, NetworkServer.ReceiveState state)
        {
          this.Disconnect(index);
          state.Dispose();
        }
  
        // Append new data to the ring buffer safely
        private void AppendToRingBuffer(ref NetworkServer.ReceiveState state, int length)
        {
            if (length < 0 || length > this._packetSize)
            {
                HandleSocketError(state, "Invalid packet length");
                return;
            }

            if (state.RingBuffer == null)
            {
                // Initialize RingBuffer if it's null
                state.RingBuffer = new byte[length];
                Buffer.BlockCopy(state.Buffer, 0, state.RingBuffer, 0, length);
            }
            else
            {
                // Expand RingBuffer if it already has data
                int oldLength = state.RingBuffer.Length;
                byte[] newBuffer = new byte[oldLength + length];
                Buffer.BlockCopy(state.RingBuffer, 0, newBuffer, 0, oldLength);
                Buffer.BlockCopy(state.Buffer, 0, newBuffer, oldLength, length);
                state.RingBuffer = newBuffer;
            }
          }

        private void PacketHandler(ref NetworkServer.ReceiveState so)
        {
            int index = so.Index;
            int length1 = so.RingBuffer.Length;
            int num = 0;
            bool flag = false;
            int count;

            while (true)
            {
                count = length1 - num;
                if (count >= 4)
                {
                    int packetLength = BitConverter.ToInt32(so.RingBuffer, num);
                    if (packetLength >= 4)
                    {
                        if (packetLength <= count)
                        {
                            int startIndex = num + 4;
                            int packetId = BitConverter.ToInt32(so.RingBuffer, startIndex);
                            if (packetId >= 0 && packetId < this._packetCount)
                            {
                                if (this.PacketID[packetId] != null)
                                {
                                    if (this.AccessCheck != null)
                                    {
                                        this.AccessCheck(index, packetId);
                                        if (!this._socket.ContainsKey(index))
                                            break;
                                    }
                                    int dataLength = packetLength - 4;
                                    byte[] data = new byte[dataLength];
                                    if (dataLength > 0)
                                        Buffer.BlockCopy(so.RingBuffer, startIndex + 4, data, 0, dataLength);
                                    this.PacketReceived?.Invoke(dataLength, packetId, ref data);
                                    this.PacketID[packetId](index, ref data);
                                    num = startIndex + packetLength;
                                    --so.PacketCount;
                                    flag = true;
                                }
                                else
                                    goto NullReference;
                            }
                            else
                                goto IndexOutofRange;
                        }
                        else
                            goto Overflow;
                    }
                    else
                        goto BrokenPacket;
                }
                else
                    break; // Not enough data for packet size
            }

            // If all data processed, clear buffer, else keep remaining
            if (num == length1)
            {
                so.RingBuffer = null;
                so.PacketCount = 0;
            }
            else if (num > 0)
            {
                int remaining = length1 - num;
                byte[] dst = new byte[remaining];
                Buffer.BlockCopy(so.RingBuffer, num, dst, 0, remaining);
                so.RingBuffer = dst;
                so.PacketCount = flag ? 1 : so.PacketCount;
            }

            return;

        NullReference:
            if (!this._socket.ContainsKey(index))
            {
                so.Dispose();
                return;
            }
            this.CrashReport?.Invoke(index, "NullReferenceException");
            this.Disconnect(index);
            so.Dispose();
            return;
        IndexOutofRange:
            if (!this._socket.ContainsKey(index))
            {
                so.Dispose();
                return;
            }
            this.CrashReport?.Invoke(index, "IndexOutOfRangeException");
            this.Disconnect(index);
            so.Dispose();
            return;
        BrokenPacket:
            if (!this._socket.ContainsKey(index))
            {
                so.Dispose();
                return;
            }
            this.CrashReport?.Invoke(index, "BrokenPacketException");
            this.Disconnect(index);
            so.Dispose();
            return;
        Overflow:
            if (count == 0)
            {
                so.RingBuffer = null;
                so.PacketCount = 0;
            }
            else
            {
                byte[] dst = new byte[count];
                Buffer.BlockCopy(so.RingBuffer, num, dst, 0, count);
                so.RingBuffer = dst;
                if (!flag)
                    return;
                so.PacketCount = 1;
            }
        }

        public void SendDataTo(int index, ReadOnlySpan<byte> data)
        {
            if (this._socket == null)
            {
                // Initialize _socket or handle the null case appropriately
                Console.WriteLine("Socket dictionary is null.");
                return;
            }

            if (!this._socket.ContainsKey(index))
                return;

            if (this._socket[index] == null || !this._socket[index].Connected)
                this.Disconnect(index);
            else
                this._socket[index].BeginSend(data.ToArray(), 0, data.Length, SocketFlags.None, new AsyncCallback(this.DoSend), (object)index);
        }

        public void SendDataTo(int index, ReadOnlySpan<byte> data, int head)
        {
            if (this._socket == null)
            {
                // Initialize _socket or handle the null case appropriately
                Console.WriteLine("Socket dictionary is null.");
                return;
            }

            if (!this._socket.ContainsKey(index))
                return;

            if (this._socket[index] == null || !this._socket[index].Connected)
            {
                this.Disconnect(index);
            }
            else
            {
                byte[] numArray = new byte[head + 4];
                Buffer.BlockCopy((Array)BitConverter.GetBytes(head), 0, (Array)numArray, 0, 4);
                Buffer.BlockCopy((Array)data.ToArray(), 0, (Array)numArray, 4, head);
                try
                {
                    this._socket[index].BeginSend(numArray, 0, head + 4, SocketFlags.None, new AsyncCallback(this.DoSend), (object)index);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"SocketException during send: {ex.Message}");
                    this.Disconnect(index);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during send: {ex.Message}");
                    this.Disconnect(index);
                }
            }
        }

        public void SendDataToAll(ReadOnlySpan<byte> data)
        {
          for (int index = 0; index <= this.HighIndex; ++index)
          {
            if (this._socket.ContainsKey(index))
              this.SendDataTo(index, data);
          }
        }

        public void SendDataToAll(ReadOnlySpan<byte> data, int head)
        {
          byte[] numArray = new byte[head + 4];
          Buffer.BlockCopy((Array) BitConverter.GetBytes(head), 0, (Array) numArray, 0, 4);
          Buffer.BlockCopy((Array) data.ToArray(), 0, (Array) numArray, 4, head);
      
          for (int index = 0; index <= this.HighIndex; ++index)
          {
            if (this._socket.ContainsKey(index))
              this.SendDataTo(index, numArray);
          }
        }

        public void SendDataToAllBut(int index, ref byte[] data)
        {
          for (int index1 = 0; index1 <= this.HighIndex; ++index1)
          {
            if (this._socket.ContainsKey(index1) && index1 != index)
              this.SendDataTo(index1, data);
          }
        }

        public void SendDataToAllBut(int index, ReadOnlySpan<byte> data, int head)
        {
          byte[] numArray = new byte[head + 4];
          Buffer.BlockCopy((Array) BitConverter.GetBytes(head), 0, (Array) numArray, 0, 4);
          Buffer.BlockCopy((Array) data.ToArray(), 0, (Array) numArray, 4, head);
          for (int index1 = 0; index1 <= this.HighIndex; ++index1)
          {
            if (this._socket.ContainsKey(index1) && index1 != index)
              this.SendDataTo(index1, numArray);
          }
        }

        private void DoSend(IAsyncResult ar)
        {
          int asyncState = (int) ar.AsyncState;
          this._socket[asyncState].EndSend(ar);
        }

        private struct ReceiveState : IDisposable
        {
          internal readonly int Index;
          internal int PacketCount;
          internal byte[] Buffer;
          internal byte[] RingBuffer;

          internal ReceiveState(int index, int packetSize)
          {
            this.Index = index;
            this.PacketCount = 0;
            this.Buffer = new byte[packetSize];
            this.RingBuffer = (byte[]) null;
          }

          public void Dispose()
          {
            this.Buffer = (byte[]) null;
            this.RingBuffer = (byte[]) null;
          }
        }

        public delegate void AccessArgs(int index, int packet_id);

        public delegate void ConnectionArgs(int index);

        public delegate void DataArgs(int index, ref byte[] data);

        public delegate void CrashReportArgs(int index, string reason);

        public delegate void PacketInfoArgs(int size, int header, ref byte[] data);

        public delegate void TrafficInfoArgs(int size, ref byte[] data);

        public delegate void NullArgs();
      }
    }
