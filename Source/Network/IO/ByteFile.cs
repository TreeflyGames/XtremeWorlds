using System.IO;

namespace Mirage.Sharp.Asfw.IO
{
  public static class ByteFile
  {
    public static void Load(string src, ref ByteStream stream)
    {
      if (!File.Exists(src))
        return;
      BinaryReader binaryReader = new BinaryReader((Stream) File.Open(src, FileMode.Open));
      stream.Data = binaryReader.ReadBytes(binaryReader.ReadInt32());
      stream.Head = 0;
      binaryReader.Close();
    }

    public static void Save(string dest, ref ByteStream stream)
    {
      BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(dest, FileMode.Create));
      binaryWriter.Write(stream.Head);
      binaryWriter.Write(stream.ToArray());
      binaryWriter.Close();
    }

    public static byte[] Load(string src)
    {
      if (!File.Exists(src))
        return new byte[0];
      BinaryReader binaryReader = new BinaryReader((Stream) File.Open(src, FileMode.Open));
      byte[] numArray = binaryReader.ReadBytes(binaryReader.ReadInt32());
      binaryReader.Close();
      return numArray;
    }

    public static void Save(string dest, ref byte[] data)
    {
      BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Open(dest, FileMode.Create));
      binaryWriter.Write(data.Length);
      binaryWriter.Write(data);
      binaryWriter.Close();
    }
  }
}
