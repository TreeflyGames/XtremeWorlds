Imports System.IO
Imports Core
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed. This event is not raised if the application is terminating abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim myFilePath As String = Paths.Logs & "Errors.log"
            Dim directoryPath As String = Path.GetDirectoryName(myFilePath)

            ' Check if the directory exists
            If Not Directory.Exists(directoryPath) Then
                ' Create the directory
                Directory.CreateDirectory(directoryPath)
            End If

            Try
                Using sw As New StreamWriter(File.Open(myFilePath, FileMode.Append))
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    sw.WriteLine("Exception: " & e.Exception.Message)
                    sw.WriteLine("Stack Trace: " & e.Exception.StackTrace)
                    sw.WriteLine("---------------------------------------------------")
                End Using

                MessageBox.Show("An unexpected error occurred. Check the error log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                ' Handle any file I/O errors here
            End Try

            ' It's generally not a good idea to call End, as it terminates the application immediately without proper cleanup.
            ' Consider using Application.Exit() or Environment.Exit() with an error code instead.
        End Sub

    End Class
End Namespace
