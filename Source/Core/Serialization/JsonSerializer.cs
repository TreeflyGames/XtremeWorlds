using System.IO;
using Newtonsoft.Json;

namespace Core.Serialization
{
    public class JsonSerializer<InputType> : ISerializer<InputType, string>
    {

        private readonly JsonSerializerSettings serializerSettings;
        private readonly Formatting serializerFormatting;

        public JsonSerializer()
        {
            serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            serializerFormatting = serializerSettings.Formatting;
        }

        public string Serialize(InputType rawObject)
        {
            return JsonConvert.SerializeObject(rawObject, serializerFormatting, serializerSettings);
        }

        public InputType Deserialize(string serializedValue)
        {
            return JsonConvert.DeserializeObject<InputType>(serializedValue, serializerSettings);
        }

        public InputType Read(string filename)
        {
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(filename)))
                return default;
            if (!File.Exists(filename))
                return default;

            var fileStream = new StreamReader(filename);
            string fileData = fileStream.ReadToEnd();
            fileStream.Close();

            return Deserialize(fileData);
        }

        public void Write(string filename, InputType rawObject)
        {
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(filename)))
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filename));

            var fileStream = new StreamWriter(filename, false);

            fileStream.Write(Serialize(rawObject));
            fileStream.Close();
        }
    }
}