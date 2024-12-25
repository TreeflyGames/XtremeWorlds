
namespace Core.Serialization
{
    public interface ISerializer<InputType, OutputType>
    {
        OutputType Serialize(InputType rawObject);
        InputType Deserialize(OutputType serializedValue);
        InputType Read(string filename);
        void Write(string filename, InputType rawObject);
    }
}