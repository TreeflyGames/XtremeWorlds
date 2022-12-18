Imports System.Net.Http

Namespace Network

Public Class Api
    Public Sub Auth(username As String, password as String)
        Using client As System.Net.Http.HttpClient = New System.Net.Http.HttpClient()

            Dim params = New Dictionary(Of String, String) From {
                    {"login", username},
                    {"password", password}
                    }

            client.DefaultRequestHeaders.Add("XF-Api-Key", "-5xPMTEPy3151vdLJKzaE04dGTnOu7vu")

            Dim result As HttpResponseMessage = client.PostAsync("https://pokemonblood.com/forums/index.php/api/auth",
                                                                     New FormUrlEncodedContent(params)).Result

            Console.WriteLine(result.Content.ReadAsStringAsync().Result)
            Console.ReadLine()
        End Using
    End Sub
End Class
End NameSpace