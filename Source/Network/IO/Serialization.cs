using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Mirage.Sharp.Asfw.IO
{
  public static class Serialization
  {
    public static void SaveXml<T>(string path, T obj)
    {
      using (StreamWriter streamWriter = new StreamWriter(path))
        new XmlSerializer(typeof (T)).Serialize((TextWriter) streamWriter, (object) obj);
    }

    public static T LoadXml<T>(string path)
    {
      using (StreamReader streamReader = new StreamReader(path))
        return (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) streamReader);
    }

    public static byte[] FromObject(object obj)
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, obj);
        return serializationStream.GetBuffer();
      }
    }

    public static object ToObject(byte[] bytes)
    {
      using (MemoryStream serializationStream = new MemoryStream(bytes))
        return new BinaryFormatter().Deserialize((Stream) serializationStream);
    }
  }
}
