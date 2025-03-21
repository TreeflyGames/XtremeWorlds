using System.IO;

namespace Mirage.Sharp.Asfw.IO
{
    public class ByteFile
    {
        public static void Load(string src, out byte[] data)
        {
            data = new byte[0];
            if (!File.Exists(src))
                return;
            using (BinaryReader binaryReader = new BinaryReader(File.Open(src, FileMode.Open)))
            {
                data = binaryReader.ReadBytes(binaryReader.ReadInt32());
            }
        }

        public static void Save(string dest, byte[] data)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(dest, FileMode.Create)))
            {
                binaryWriter.Write(data.Length);
                binaryWriter.Write(data);
            }
        }
    }
}
