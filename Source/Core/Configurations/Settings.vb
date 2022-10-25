Imports System.IO
Imports System.Xml.Serialization
Imports Mirage.Basic.Engine

Public Class SettingsDef

    Public Language As String = "English"

    Public Username As String = ""
    Public Password As String = ""
    Public SavePass As Boolean = False

    Public MenuMusic As String = ""
    Public Music As Boolean = True
    Public Sound As Boolean = True
    Public Volume As Single = 100.0F

    Public ScreenSize As String = "1024x768"
    Public Vsync As Byte = 1
    Public ShowNpcBar As Byte = 1
    Public CameraType As Byte = 0
    Public Fullscreen As Byte = 1
    Public OpenAdminPanelOnLogin As Byte = 1
    Public DynamicLightRendering As Byte = 1

    Public Ip As String = "127.0.0.1"
    Public Port As Integer = 7001

    <XmlIgnore()> Public GameName As String = "MirageBasic"
    <XmlIgnore()> Public Website As String = "https://miragebasic.net/"

    <XmlIgnore()> Public Version As String = "1.5.3"

    Public Welcome As String = "Welcome to MirageBasic, enjoy your stay!"

    Public TimeSpeed As Integer = 1
End Class

Public Module modSettings
    Public Settings As New SettingsDef

    Public Sub LoadSettings()
        Dim cf As String = Paths.Config()
        Dim x As New XmlSerializer(GetType(SettingsDef), New XmlRootAttribute("Settings"))

        Directory.CreateDirectory(cf)
        cf += "Settings.xml"

        If Not File.Exists(cf) Then
            File.Create(cf).Dispose()
            Dim writer = New StreamWriter(cf)
            x.Serialize(writer, Settings)
            writer.Close
        End If

        Dim reader = New StreamReader(cf)
        Settings = x.Deserialize(reader)
        reader.Close
    End Sub

    Public Sub SaveSettings()
        Dim cf As String = Paths.Config()

        Directory.CreateDirectory(cf)
        cf += "Settings.xml"

        Dim x As New XmlSerializer(GetType(SettingsDef), New XmlRootAttribute("Settings"))
        Dim writer = New StreamWriter(cf)
        
        x.Serialize(writer, Settings)
        writer.Close
    End Sub

End Module