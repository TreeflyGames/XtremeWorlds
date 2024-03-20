Imports System.IO
Imports System.Runtime
Imports System.Xml.Serialization

Public Class GameSettings
    Public Shared Language As String = "English"

    Public Shared Username As String = ""
    Public Shared SaveUsername As Boolean = False

    Public Shared MenuMusic As String = "menu.mid"
    Public Shared Music As Boolean = True
    Public Shared Sound As Boolean = True
    Public Shared Volume As Single = 100.0F

    Public Shared MusicExt As String = ".mid"
    Public Shared SoundExt As String = ".wav"

    Public Shared ScreenWidth As String = "1024"
    Public Shared ScreenHeight As String = "768"
    Public Shared Vsync As Byte = 1
    Public Shared ShowNpcBar As Byte = 1
    Public Shared CameraType As Byte = 0
    Public Shared Fullscreen As Byte = 1
    Public Shared CameraWidth As Byte = 32
    Public Shared CameraHeight As Byte = 24
    Public Shared OpenAdminPanelOnLogin As Byte = 1
    Public Shared DynamicLightRendering As Byte = 1
    Public Shared ChannelState(ChatChannel.Count - 1) As Byte

    Public Shared Ip As String = "127.0.0.1"
    Public Shared Port As Integer = 7001

    <XmlIgnore()> Public Shared GameName As String = "MirageWorlds"
    <XmlIgnore()> Public Shared Website As String = "https://giamon.com/"

    <XmlIgnore()> Public Shared Version As String = "1.6.2"

    Public Shared Welcome As String = "Welcome to MirageWorlds, enjoy your stay!"

    Public Shared TimeSpeed As Integer = 1

    Public Shared MaxFps As Integer = 60

    Public Shared Autotile As Boolean = True

    Public Shared Instance As GameSettings

    ' Configuration file path
    Private Shared ReadOnly Property ConfigPath As String
        Get
            Dim cf As String = Paths.Config()
            Directory.CreateDirectory(cf)
            Return Path.Combine(cf, "GameSettings.xml")
        End Get
    End Property

    ' Load settings from file
    Public Shared Function Load() As GameSettings
        If File.Exists(ConfigPath) Then
            Try
                Using reader As New StreamReader(ConfigPath)
                    Dim serializer As New XmlSerializer(GetType(GameSettings))
                    Return CType(serializer.Deserialize(reader), GameSettings)
                End Using
            Catch ex As Exception
                Return New GameSettings() ' In case of error, return default settings
            End Try
        Else
            Return New GameSettings() ' If file doesn't exist, return default settings
        End If
    End Function

    ' Save current settings to file
    Public Shared Sub Save()
        Using writer As New StreamWriter(ConfigPath)
            Dim serializer As New XmlSerializer(GetType(GameSettings))
            serializer.Serialize(writer, Instance)
        End Using
    End Sub
End Class