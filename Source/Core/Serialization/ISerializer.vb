Namespace Serialization
    Public Interface ISerializer(Of InputType, OutputType)
        Function Serialize(rawObject As InputType) As OutputType
        Function Deserialize(serializedValue As OutputType) As InputType
        Function Read(filename As String) As InputType
        Sub Write(filename As String, rawObject As InputType)
    End Interface
End Namespace