using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.IO.Compression;

namespace Core.Serialization
{
    public class JsonSerializer<InputType> : ISerializer<InputType, string>
    {
        private readonly JsonSerializerSettings serializerSettings;
        private readonly Formatting serializerFormatting;

        public JsonSerializer(JsonSerializerSettings settings = null, Formatting formatting = Formatting.Indented)
        {
            serializerSettings = settings ?? new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = formatting,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            serializerFormatting = serializerSettings.Formatting;
        }

        ### Core Serialization Methods

        public string Serialize(InputType rawObject)
        {
            try
            {
                return JsonConvert.SerializeObject(rawObject, serializerFormatting, serializerSettings);
            }
            catch (JsonException ex)
            {
                throw new SerializationException("Error during serialization", ex);
            }
        }

        public InputType Deserialize(string serializedValue)
        {
            try
            {
                return JsonConvert.DeserializeObject<InputType>(serializedValue, serializerSettings);
            }
            catch (JsonException ex)
            {
                throw new SerializationException("Error during deserialization", ex);
            }
        }

        ### Synchronous File Operations

        public InputType Read(string filename)
        {
            if (!File.Exists(filename))
                return default;

            try
            {
                using (var fileStream = new StreamReader(filename))
                {
                    string fileData = fileStream.ReadToEnd();
                    return Deserialize(fileData);
                }
            }
            catch (IOException ex)
            {
                throw new SerializationException("Error reading file", ex);
            }
        }

        public void Write(string filename, InputType rawObject)
        {
            try
            {
                string directory = Path.GetDirectoryName(filename);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new StreamWriter(filename, false))
                {
                    fileStream.Write(Serialize(rawObject));
                }
            }
            catch (IOException ex)
            {
                throw new SerializationException("Error writing to file", ex);
            }
        }

        ### Asynchronous File Operations

        public async Task<InputType> ReadAsync(string filename)
        {
            if (!File.Exists(filename))
                return default;

            try
            {
                using (var fileStream = new StreamReader(filename))
                {
                    string fileData = await fileStream.ReadToEndAsync();
                    return Deserialize(fileData);
                }
            }
            catch (IOException ex)
            {
                throw new SerializationException("Error reading file", ex);
            }
        }

        public async Task WriteAsync(string filename, InputType rawObject)
        {
            try
            {
                string directory = Path.GetDirectoryName(filename);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new StreamWriter(filename, false))
                {
                    await fileStream.WriteAsync(Serialize(rawObject));
                }
            }
            catch (IOException ex)
            {
                throw new SerializationException("Error writing to file", ex);
            }
        }

        ### Byte Array Support

        public byte[] SerializeToByteArray(InputType rawObject)
        {
            string json = Serialize(rawObject);
            return Encoding.UTF8.GetBytes(json);
        }

        public InputType DeserializeFromByteArray(byte[] byteArray)
        {
            string json = Encoding.UTF8.GetString(byteArray);
            return Deserialize(json);
        }

        ### Custom Serialization Settings

        public string SerializeWithSettings(InputType rawObject, JsonSerializerSettings customSettings)
        {
            try
            {
                return JsonConvert.SerializeObject(rawObject, serializerFormatting, customSettings);
            }
            catch (JsonException ex)
            {
                throw new SerializationException("Error during serialization with custom settings", ex);
            }
        }

        public InputType DeserializeWithSettings(string serializedValue, JsonSerializerSettings customSettings)
        {
            try
            {
                return JsonConvert.DeserializeObject<InputType>(serializedValue, customSettings);
            }
            catch (JsonException ex)
            {
                throw new SerializationException("Error during deserialization with custom settings", ex);
            }
        }

        ### JSON Validation

        public bool IsValidJson(string json)
        {
            try
            {
                JsonConvert.DeserializeObject(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        ### Data Compression

        public byte[] CompressSerializedData(InputType rawObject)
        {
            byte[] jsonBytes = SerializeToByteArray(rawObject);
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
                {
                    deflateStream.Write(jsonBytes, 0, jsonBytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        public InputType DecompressAndDeserialize(byte[] compressedData)
        {
            using (var memoryStream = new MemoryStream(compressedData))
            using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
            using (var reader = new StreamReader(deflateStream))
            {
                string json = reader.ReadToEnd();
                return Deserialize(json);
            }
        }
    }

    ### Custom Exception

    public class SerializationException : Exception
    {
        public SerializationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
