using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

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
    public NetworkServer.DataArgs[] PacketId;

    public int BufferLimit { get; set; }

    public int ClientLimit { get; }

    public bool IsListening { get; private set; }

    public int HighIndex { get; private set; }

    public int MinimumIndex { get; set; }

    public List<int> ConnectionIds() => this._socket == null ? new List<int>() : new List<int>((IEnumerable<int>) this._socket.Keys);

    public int PacketAcceptLimit { get; set; }

    public int PacketDisconnectCount { get; set; }

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

      if (packetSize <= 0)
        packetSize = 8192;
      this._socket = new Dictionary<int, Socket>();
      this._unsignedIndex = new List<int>();
      this.ClientLimit = clientLimit;
      this._packetCount = packetCount;
      this._packetSize = packetSize;
      this.PacketId = new NetworkServer.DataArgs[packetCount];
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
      this.PacketId = (NetworkServer.DataArgs[]) null;
      this._unsignedIndex.Clear();
      this._unsignedIndex = (List<int>) null;
      this.AccessCheck = (NetworkServer.AccessArgs) null;
      this.ConnectionReceived = (NetworkServer.ConnectionArgs) null;
      this.ConnectionLost = (NetworkServer.ConnectionArgs) null;
      this.CrashReport = (NetworkServer.CrashReportArgs) null;
      this.PacketReceived = (NetworkServer.PacketInfoArgs) null;
      this.TrafficReceived = (NetworkServer.TrafficInfoArgs) null;
      this.PacketId = (NetworkServer.DataArgs[]) null;
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

    public string ClientIp(int index) => !this.IsConnected(index) ? "[Null]" : ((IPEndPoint) this._socket[index].RemoteEndPoint).Address.ToString();

    public void Disconnect(int index)
    {
      if (this._socket == null || !this._socket.ContainsKey(index))
        return;
      Socket socket = this._socket[index];
      if (socket == null)
      {
        this._socket.Remove(index);
        this._unsignedIndex.Add(index);
      }
      else
        socket.BeginDisconnect(false, new AsyncCallback(this.DoDisconnect), (object) index);
    }

    private void DoDisconnect(IAsyncResult ar)
    {
      if (this._socket == null)
        return;
      int asyncState = (int) ar.AsyncState;
      try
      {
        this._socket[asyncState].EndDisconnect(ar);
      }
      catch
      {
      }
      if (!this._socket.ContainsKey(asyncState))
        return;
      this._socket[asyncState].Dispose();
      this._socket[asyncState] = (Socket) null;
      this._socket.Remove(asyncState);
      this._unsignedIndex.Add(asyncState);
      int highIndex = this.HighIndex;
      while (!this._socket.ContainsKey(highIndex) && highIndex != this.MinimumIndex)
        --highIndex;
      this.HighIndex = highIndex;
      NetworkServer.ConnectionArgs connectionLost = this.ConnectionLost;
      if (connectionLost == null)
        return;
      connectionLost(asyncState);
    }

    private int FindEmptySlot(int startIndex)
    {
      using (List<int>.Enumerator enumerator = this._unsignedIndex.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          int current = enumerator.Current;
          if (this.HighIndex < current)
            this.HighIndex = current;
          this._unsignedIndex.Remove(current);
          return current;
        }
      }
      if (this._socket.Count == 0)
      {
        this.HighIndex = startIndex;
        return startIndex;
      }
      ++this.HighIndex;
      return this.HighIndex;
    }

    public void StartListening(int port, int backlog)
    {
      if (this._socket == null || this.IsListening || this._listener != null)
        return;
      this._listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      this._listener.NoDelay = true;
      this._listener.Bind((EndPoint) new IPEndPoint(IPAddress.Any, port));
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
          this._socket.Add(emptySlot, socket);
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
      try
      {
        this._socket[index].BeginReceive(state.Buffer, 0, this._packetSize, SocketFlags.None, new AsyncCallback(this.DoReceive), (object) state);
      }
      catch
      {
      }
    }

    private void DoReceive(IAsyncResult ar)
    {
      if (this._socket == null)
        return;
      NetworkServer.ReceiveState asyncState = (NetworkServer.ReceiveState) ar.AsyncState;
      int length1;
      try
      {
        length1 = this._socket[asyncState.Index].EndReceive(ar);
      }
      catch
      {
        NetworkServer.CrashReportArgs crashReport = this.CrashReport;
        if (crashReport != null)
          crashReport(asyncState.Index, "ConnectionForciblyClosedException");
        this.Disconnect(asyncState.Index);
        asyncState.Dispose();
        return;
      }
      if (length1 < 1)
      {
        if (!this._socket.ContainsKey(asyncState.Index))
          asyncState.Dispose();
        else if (this._socket[asyncState.Index] == null)
        {
          asyncState.Dispose();
        }
        else
        {
          NetworkServer.CrashReportArgs crashReport = this.CrashReport;
          if (crashReport != null)
            crashReport(asyncState.Index, "BufferUnderflowException");
          this.Disconnect(asyncState.Index);
          asyncState.Dispose();
        }
      }
      else
      {
        NetworkServer.TrafficInfoArgs trafficReceived = this.TrafficReceived;
        if (trafficReceived != null)
          trafficReceived(length1, ref asyncState.Buffer);
        ++asyncState.PacketCount;
        if (this.PacketDisconnectCount > 0 && asyncState.PacketCount >= this.PacketDisconnectCount)
        {
          NetworkServer.CrashReportArgs crashReport = this.CrashReport;
          if (crashReport != null)
            crashReport(asyncState.Index, "Packet Spamming/DDOS");
          this.Disconnect(asyncState.Index);
          asyncState.Dispose();
        }
        else
        {
          if (this.PacketAcceptLimit == 0 || this.PacketAcceptLimit > asyncState.PacketCount)
          {
            if (asyncState.RingBuffer == null)
            {
              asyncState.RingBuffer = new byte[length1];
              Buffer.BlockCopy((Array) asyncState.Buffer, 0, (Array) asyncState.RingBuffer, 0, length1);
            }
            else
            {
              int length2 = asyncState.RingBuffer.Length;
              byte[] dst = new byte[length2 + length1];
              Buffer.BlockCopy((Array) asyncState.RingBuffer, 0, (Array) dst, 0, length2);
              Buffer.BlockCopy((Array) asyncState.Buffer, 0, (Array) dst, length2, length1);
              asyncState.RingBuffer = dst;
            }
            if (this.BufferLimit > 0 && asyncState.RingBuffer.Length > this.BufferLimit)
            {
              this.Disconnect(asyncState.Index);
              asyncState.Dispose();
              return;
            }
          }
          if (!this._socket.ContainsKey(asyncState.Index))
            asyncState.Dispose();
          else if (this._socket[asyncState.Index] == null || !this._socket[asyncState.Index].Connected)
          {
            this.Disconnect(asyncState.Index);
            asyncState.Dispose();
          }
          else
          {
            this.PacketHandler(ref asyncState);
            asyncState.Buffer = new byte[this._packetSize];
            if (!this._socket.ContainsKey(asyncState.Index))
            {
              asyncState.Dispose();
            }
            else
            {
              try
              {
                this._socket[asyncState.Index].BeginReceive(asyncState.Buffer, 0, this._packetSize, SocketFlags.None, new AsyncCallback(this.DoReceive), (object) asyncState);
              }
              catch
              {
              }
            }
          }
        }
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
          int int32_1 = BitConverter.ToInt32(so.RingBuffer, num);
          if (int32_1 >= 4)
          {
            if (int32_1 <= count)
            {
              int startIndex = num + 4;
              int int32_2 = BitConverter.ToInt32(so.RingBuffer, startIndex);
              if (int32_2 >= 0 && int32_2 < this._packetCount)
              {
                if (this.PacketId[int32_2] != null)
                {
                  if (this.AccessCheck != null)
                  {
                    this.AccessCheck(index, int32_2);
                    if (!this._socket.ContainsKey(index))
                      break;
                  }
                  int length2 = int32_1 - 4;
                  byte[] data = new byte[length2];
                  if (length2 > 0)
                    Buffer.BlockCopy((Array) so.RingBuffer, startIndex + 4, (Array) data, 0, length2);
                  NetworkServer.PacketInfoArgs packetReceived = this.PacketReceived;
                  if (packetReceived != null)
                    packetReceived(length2, int32_2, ref data);
                  this.PacketId[int32_2](index, ref data);
                  num = startIndex + int32_1;
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
          goto Overflow;
      }
      so.Dispose();
      return;
    NullReference:
      if (!this._socket.ContainsKey(index))
      {
        so.Dispose();
        return;
      }
      NetworkServer.CrashReportArgs crashReport1 = this.CrashReport;
      if (crashReport1 != null)
        crashReport1(index, "NullReferenceException");
      this.Disconnect(index);
      so.Dispose();
      return;
    IndexOutofRange:
      if (!this._socket.ContainsKey(index))
      {
        so.Dispose();
        return;
      }
      NetworkServer.CrashReportArgs crashReport2 = this.CrashReport;
      if (crashReport2 != null)
        crashReport2(index, "IndexOutOfRangeException");
      this.Disconnect(index);
      so.Dispose();
      return;
    BrokenPacket:
      if (!this._socket.ContainsKey(index))
      {
        so.Dispose();
        return;
      }
      NetworkServer.CrashReportArgs crashReport3 = this.CrashReport;
      if (crashReport3 != null)
        crashReport3(index, "BrokenPacketException");
      this.Disconnect(index);
      so.Dispose();
      return;
    Overflow:
      if (count == 0)
      {
        so.RingBuffer = (byte[]) null;
        so.PacketCount = 0;
      }
      else
      {
        byte[] dst = new byte[count];
        Buffer.BlockCopy((Array) so.RingBuffer, num, (Array) dst, 0, count);
        so.RingBuffer = dst;
        if (!flag)
          return;
        so.PacketCount = 1;
      }
    }

    public void SendDataTo(int index, byte[] data)
    {
      if (!this._socket.ContainsKey(index))
        return;
      if (this._socket[index] == null || !this._socket[index].Connected)
        this.Disconnect(index);
      else
        this._socket[index].BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(this.DoSend), (object) index);
    }

    public void SendDataTo(int index, byte[] data, int head)
    {
      if (!this._socket.ContainsKey(index))
        return;
      if (this._socket[index] == null || !this._socket[index].Connected)
      {
        this.Disconnect(index);
      }
      else
      {
        byte[] numArray = new byte[head + 4];
        Buffer.BlockCopy((Array) BitConverter.GetBytes(head), 0, (Array) numArray, 0, 4);
        Buffer.BlockCopy((Array) data, 0, (Array) numArray, 4, head);
        this._socket[index].BeginSend(numArray, 0, head + 4, SocketFlags.None, new AsyncCallback(this.DoSend), (object) index);
      }
    }

    public void SendDataToAll(byte[] data)
    {
      for (int index = 0; index <= this.HighIndex; ++index)
      {
        if (this._socket.ContainsKey(index))
          this.SendDataTo(index, data);
      }
    }

    public void SendDataToAll(byte[] data, int head)
    {
      byte[] numArray = new byte[head + 4];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(head), 0, (Array) numArray, 0, 4);
      Buffer.BlockCopy((Array) data, 0, (Array) numArray, 4, head);
      for (int index = 0; index <= this.HighIndex; ++index)
      {
        if (this._socket.ContainsKey(index))
          this.SendDataTo(index, numArray);
      }
    }

    public void SendDataToAllBut(int index, byte[] data)
    {
      for (int index1 = 0; index1 <= this.HighIndex; ++index1)
      {
        if (this._socket.ContainsKey(index1) && index1 != index)
          this.SendDataTo(index1, data);
      }
    }

    public void SendDataToAllBut(int index, byte[] data, int head)
    {
      byte[] numArray = new byte[head + 4];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(head), 0, (Array) numArray, 0, 4);
      Buffer.BlockCopy((Array) data, 0, (Array) numArray, 4, head);
      for (int index1 = 0; index1 <= this.HighIndex; ++index1)
      {
        if (this._socket.ContainsKey(index1) && index1 != index)
          this.SendDataTo(index1, numArray);
      }
    }

    private void DoSend(IAsyncResult ar)
    {
      int asyncState = (int) ar.AsyncState;
      try
      {
        this._socket[asyncState].EndSend(ar);
      }
      catch
      {
        NetworkServer.CrashReportArgs crashReport = this.CrashReport;
        if (crashReport != null)
          crashReport(asyncState, "ConnectionForciblyClosedException");
        this.Disconnect(asyncState);
      }
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
