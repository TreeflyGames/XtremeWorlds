Imports Core.Serialization

Namespace Database
    Public Class CharacterList
        Private names As List(Of String)

        Public Sub New()
            names = New List(Of String)()
        End Sub

        Function Find(Name As String) As Boolean
            Return names.Contains(Trim$(LCase(Name)))
        End Function

        Function Find(Name As String, ByRef RetValue As Boolean) As CharacterList
            RetValue = names.Contains(Trim$(LCase(Name)))
            Return Me
        End Function

        Function Add(Name As String) As CharacterList
            If Find(Name) Then Return Me

            names.Add(Trim$(LCase(Name)))

            Return Me
        End Function

        Function Remove(Name As String) As CharacterList
            If Not Find(Name) Then Return Me

            names.Remove(Trim$(LCase(Name)))

            Return Me
        End Function

        Function Clear() As CharacterList
            If names Is Nothing Then names = New List(Of String)()
            If names.Count <= 0 Then Return Me

            names.Clear()

            Return Me
        End Function

    End Class
End Namespace
