﻿Imports System.Threading
Imports Core
Imports Microsoft.Xna.Framework

Friend Class FrmGame

#Region "Frm"
    Private Sub FrmMainGame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Task.Run(Sub()
                     Client.Run()
                 End Sub)

        Task.Run(Sub()
                     Startup()
                     BeginInvoke(New MethodInvoker(Sub() Hide()))
                 End Sub)
    End Sub
#End Region

End Class