﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Mirage.Sharp.Asfw.IO
{
  public class Serialization
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
  }
}
