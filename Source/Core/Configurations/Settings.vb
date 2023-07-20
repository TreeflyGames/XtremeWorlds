Imports System.IO
Imports System.Xml.Serialization

Public Class Settings
    Public Language As String = "English"

    Public Username As String = ""
    Public Password As String = ""
    Public RememberPassword As Boolean = False

    Public MenuMusic As String = "title.ogg"
    Public Music As Boolean = True
    Public Sound As Boolean = True
    Public Volume As Single = 100.0F

    Public Width As String = "1024"
    Public Height As String = "768"
    Public Vsync As Byte = 1
    Public ShowNpcBar As Byte = 1
    Public CameraType As Byte = 0
    Public Fullscreen As Byte = 1
    Public OpenAdminPanelOnLogin As Byte = 1
    Public DynamicLightRendering As Byte = 1

    Public Ip As String = "127.0.0.1"
    Public Port As Integer = 7001

    <XmlIgnore()> Public GameName As String = "MirageWorlds"
    <XmlIgnore()> Public Website As String = "https://treeflygames.com/"

    <XmlIgnore()> Public Version As String = "1.6.3"

    Public Welcome As String = "Welcome to MirageWorlds, enjoy your stay!"

    Public TimeSpeed As Integer = 1

    Public MaxFps As Integer = 60
End Class

Public Module SettingsManager
    Public Sub Load()
        Dim cf As String = Paths.Config()
        Dim x As New XmlSerializer(GetType(Settings), New XmlRootAttribute("Settings"))

        Directory.CreateDirectory(cf)
        cf += "Settings.xml"

        If Not File.Exists(cf) Then
            File.Create(cf).Dispose()
            Dim writer = New StreamWriter(cf)
            x.Serialize(writer, New Settings)
            writer.Close
        End If

        Dim reader = New StreamReader(cf)
        Types.Settings = x.Deserialize(reader)
        reader.Close
    End Sub

    Public Sub Save()
        Dim cf As String = Paths.Config()

        Directory.CreateDirectory(cf)
        cf += "Settings.xml"

        Dim x As New XmlSerializer(GetType(Settings), New XmlRootAttribute("Settings"))
        Dim writer = New StreamWriter(cf)
        
        x.Serialize(writer, Types.Settings)
        writer.Close
    End Sub

End Module