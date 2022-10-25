Imports Mirage.Basic.Engine
Imports SFML.System

Module C_Types
    Friend ActionMsg(Byte.MaxValue) As ActionMsgStruct

    Friend Blood(Byte.MaxValue) As BloodStruct

    Friend Chat As New List(Of ChatStruct)

    Public tempTileLights As List(Of LightTileStruct)

    Friend MapNames(MAX_MAPS) As String

    Public Structure ChatStruct
        Dim Text As String
        Dim Color As Integer
        Dim Y As Byte
    End Structure

    Public Structure SkillAnimStruct
        Dim Skillnum As Integer
        Dim Timer As Integer
        Dim FramePointer As Integer
    End Structure

    Public Structure ChatBubbleStruct
        Dim Msg As String
        Dim Colour As Integer
        Dim Target As Integer
        Dim TargetType As Byte
        Dim Timer As Integer
        Dim Active As Boolean
    End Structure

    Public Structure MapResourceStruct
        Dim X As Integer
        Dim Y As Integer
        Dim State As Byte
    End Structure

    Public Structure ActionMsgStruct
        Dim Message As String
        Dim Created As Integer
        Dim Type As Integer
        Dim Color As Integer
        Dim Scroll As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim Timer As Integer
    End Structure

    Public Structure BloodStruct
        Dim Sprite As Integer
        Dim Timer As Integer
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Public Structure LightTileStruct
        Public tiles As List(Of Vector2i)
        Public isFlicker As Boolean
        Public isSmooth As Boolean
        Public scale As Vector2f
    End Structure

End Module