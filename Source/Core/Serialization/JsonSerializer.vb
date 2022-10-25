Imports Newtonsoft.Json
Imports System.IO

Namespace Serialization
    Public Class JsonSerializer(Of InputType)
        Implements ISerializer(Of InputType, String)

        Private ReadOnly serializerSettings As JsonSerializerSettings
        Private ReadOnly serializerFormatting As Formatting

        Public Sub New()
            serializerSettings = New JsonSerializerSettings With {
                .TypeNameHandling = TypeNameHandling.All,
                .Formatting = Formatting.Indented
            }
            serializerFormatting = serializerSettings.Formatting
        End Sub

        Public Function Serialize(rawObject As InputType) As String Implements ISerializer(Of InputType, String).Serialize
            Return JsonConvert.SerializeObject(rawObject, serializerFormatting, serializerSettings)
        End Function

        Public Function Deserialize(serializedValue As String) As InputType Implements ISerializer(Of InputType, String).Deserialize
            Return JsonConvert.DeserializeObject(Of InputType)(serializedValue, serializerSettings)
        End Function

        Public Function Read(filename As String) As InputType Implements ISerializer(Of InputType, String).Read
            If Not Directory.Exists(Path.GetDirectoryName(filename)) Then Return Nothing
            If Not File.Exists(filename) Then Return Nothing

            Dim fileStream As New StreamReader(filename)
            Dim fileData As String = fileStream.ReadToEnd()
            fileStream.Close()

            Return Deserialize(fileData)
        End Function

        Public Sub Write(filename As String, rawObject As InputType) Implements ISerializer(Of InputType, String).Write
            If Not Directory.Exists(Path.GetDirectoryName(filename)) Then Directory.CreateDirectory(Path.GetDirectoryName(filename))

            Dim fileStream As New StreamWriter(filename, False)

            fileStream.Write(Serialize(rawObject))
            fileStream.Close()
        End Sub
    End Class
End Namespace