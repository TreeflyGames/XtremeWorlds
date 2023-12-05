Imports System.IO
Imports System.Reflection
Imports System.Text
Imports NLua

Public Class LuaScripting
    Private Shared _instance As LuaScripting

    Public Shared Function Instance() As LuaScripting
        If _instance Is Nothing Then
            _instance = New LuaScripting()
        End If
        Return _instance
    End Function

    Public Shared Function ResetInstance() As LuaScripting
        If _instance IsNot Nothing Then
            _instance = Nothing
        End If
        Return Instance()
    End Function

    Private ReadOnly lua As Lua

    Private Sub New()
        lua = New Lua()

        lua.State.Encoding = Encoding.UTF8
    End Sub

    Public Function AddScript(script As String) As Object()
        Try
            Return lua.DoString(script)
        Catch ex As Exception
            Throw New Exception($"Error adding Lua script: {ex.Message}")
        End Try
    End Function

    Public Function AddScriptFromFile(filepath As String) As Object()
        If String.IsNullOrWhiteSpace(filepath) Then
            Throw New Exception($"Error adding Lua script from file, no file path was provided.")
        End If

        If Not File.Exists(filepath) Then
            Throw New Exception($"Error adding Lua script from file, the file '{filepath}' does not exist.")
        End If

        Using fs As New StreamReader(filepath)
            Dim script As String = fs.ReadToEnd()
            fs.Close()

            Return AddScript(script)
        End Using
    End Function

    Public Function ExecuteScript(functionName As String, ParamArray args As Object()) As Object()
        Try
            Return lua.GetFunction(functionName).Call(args)
        Catch ex As Exception
            Throw New Exception($"Error calling Lua function: {ex.Message}")
        End Try
    End Function

    Public Function RegisterFunction(path As String, [function] As MethodBase) As LuaFunction
        Return RegisterFunction(path, Nothing, [function])
    End Function

    Public Function RegisterFunction(path As String, target As Object, [function] As MethodBase) As LuaFunction
        Try
            Return lua.RegisterFunction(path, target, [function])
        Catch ex As Exception
            Throw New Exception($"Error registering Lua function: {ex.Message}")
        End Try
    End Function
End Class
