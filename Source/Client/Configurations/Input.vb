Imports System.IO
Imports System.Xml.Serialization
Imports Core
Imports Microsoft.Xna.Framework.Input

Public Class Input
    Public Shared Inputs As Input

    Public Class Input
        Public MoveUp As Keys = Keys.W
        Public MoveDown As Keys = Keys.S
        Public MoveLeft As Keys = Keys.A
        Public MoveRight As Keys = Keys.D
        Public MoveUp2 As Keys = Keys.Up
        Public MoveLeft2 As Keys = Keys.Left
        Public MoveRight2 As Keys = Keys.Right
        Public MoveDown2 As Keys = Keys.Down

        Public Attack As Keys = Keys.Z
        Public Run As Keys = Keys.LeftShift
        Public Loot As Keys = Keys.Space

        Public Hotbar1 As Keys = Keys.NumPad0
        Public Hotbar2 As Keys = Keys.NumPad1
        Public Hotbar3 As Keys = Keys.NumPad2
        Public Hotbar4 As Keys = Keys.NumPad3
        Public Hotbar5 As Keys = Keys.NumPad4
        Public Hotbar6 As Keys = Keys.NumPad5
        Public Hotbar7 As Keys = Keys.NumPad6

        Public Inventory As Keys = Keys.I
        Public Character As Keys = Keys.C
        Public Skill As Keys = Keys.K
        Public Setting As Keys = Keys.N

        Public HudToggle As Keys = Keys.F10
        Public Admin As Keys = Keys.Insert
    End Class

    Public Shared Primary As New Input
    Public Shared Secondary As New Input

    ' Functions to check for specific input states
    Public Shared Function MoveUp(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.MoveUp) Or state.IsKeyDown(Secondary.MoveUp) Or
               state.IsKeyDown(Primary.MoveUp2) Or state.IsKeyDown(Secondary.MoveUp2)
    End Function

    Public Shared Function MoveDown(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.MoveDown) Or state.IsKeyDown(Secondary.MoveDown) Or
               state.IsKeyDown(Primary.MoveDown2) Or state.IsKeyDown(Secondary.MoveDown2)
    End Function

    Public Shared Function MoveLeft(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.MoveLeft) Or state.IsKeyDown(Secondary.MoveLeft) Or
               state.IsKeyDown(Primary.MoveLeft2) Or state.IsKeyDown(Secondary.MoveLeft2)
    End Function

    Public Shared Function MoveRight(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.MoveRight) Or state.IsKeyDown(Secondary.MoveRight) Or
               state.IsKeyDown(Primary.MoveRight2) Or state.IsKeyDown(Secondary.MoveRight2)
    End Function

    Public Shared Function Attack(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Attack) Or state.IsKeyDown(Secondary.Attack)
    End Function

    Public Shared Function Run(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Run) Or state.IsKeyDown(Secondary.Run)
    End Function

    Public Shared Function Loot(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Loot) Or state.IsKeyDown(Secondary.Loot)
    End Function

    Public Shared Function Inventory(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Inventory) Or state.IsKeyDown(Secondary.Inventory)
    End Function

    Public Shared Function Character(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Character) Or state.IsKeyDown(Secondary.Character)
    End Function

    Public Shared Function Skill(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Skill) Or state.IsKeyDown(Secondary.Skill)
    End Function

    Public Shared Function Setting(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Setting) Or state.IsKeyDown(Secondary.Setting)
    End Function

    Public Shared Function HudToggle(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.HudToggle) Or state.IsKeyDown(Secondary.HudToggle)
    End Function

    Public Shared Function Admin(state As KeyboardState) As Boolean
        Return state.IsKeyDown(Primary.Admin) Or state.IsKeyDown(Secondary.Admin)
    End Function

    ' Load input configuration from XML
    Public Shared Sub Load()
        Try
            Dim configPath As String = Core.Path.Config()
            Directory.CreateDirectory(configPath) ' Ensure directory exists

            Dim configFile As String = IO.Path.Combine(configPath, "Inputs.xml")

            If Not File.Exists(configFile) Then
                ' Create a new file and serialize the default Inputs
                Using writer As New StreamWriter(File.Create(configFile))
                    Dim serializer As New XmlSerializer(GetType(Input), New XmlRootAttribute("Inputs"))
                    serializer.Serialize(writer, Inputs)
                End Using
            Else
                ' Deserialize the existing configuration
                Using reader As New StreamReader(configFile)
                    Dim serializer As New XmlSerializer(GetType(Input), New XmlRootAttribute("Inputs"))
                    Inputs = CType(serializer.Deserialize(reader), Input)
                End Using
            End If
        Catch ex As Exception
            Console.WriteLine("Error loading input configuration: " & ex.Message)
        End Try
    End Sub

    ' Save input configuration to XML
    Public Shared Sub Save()
        Try
            Dim configPath As String = Core.Path.Config()
            Directory.CreateDirectory(configPath) ' Ensure directory exists

            Dim configFile As String = IO.Path.Combine(configPath, "Inputs.xml")

            Using writer As New StreamWriter(configFile)
                Dim serializer As New XmlSerializer(GetType(Input), New XmlRootAttribute("Inputs"))
                serializer.Serialize(writer, Inputs)
            End Using
        Catch ex As Exception
            Console.WriteLine("Error saving input configuration: " & ex.Message)
        End Try
    End Sub
End Class
