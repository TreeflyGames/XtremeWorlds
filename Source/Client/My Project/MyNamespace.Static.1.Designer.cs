' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

#If TARGET = "module" AndAlso _MYTYPE = "" Then
    #Const _MYTYPE = "Empty"
#End If

' Define constants based on project type (_MYTYPE)
#If _MYTYPE = "WindowsForms" Then
    #Const _MYFORMS = True
    #Const _MYWEBSERVICES = True
    #Const _MYUSERTYPE = "Windows"
    #Const _MYCOMPUTERTYPE = "Windows"
    #Const _MYAPPLICATIONTYPE = "WindowsForms"
#ElseIf _MYTYPE = "WindowsFormsWithCustomSubMain" Then
    #Const _MYFORMS = True
    #Const _MYWEBSERVICES = True
    #Const _MYUSERTYPE = "Windows"
    #Const _MYCOMPUTERTYPE = "Windows"
    #Const _MYAPPLICATIONTYPE = "Console"
#ElseIf _MYTYPE = "Windows" OrElse _MYTYPE = "" Then
    #Const _MYWEBSERVICES = True
    #Const _MYUSERTYPE = "Windows"
    #Const _MYCOMPUTERTYPE = "Windows"
    #Const _MYAPPLICATIONTYPE = "Windows"
#ElseIf _MYTYPE = "Console" Then
    #Const _MYWEBSERVICES = True
    #Const _MYUSERTYPE = "Windows"
    #Const _MYCOMPUTERTYPE = "Windows"
    #Const _MYAPPLICATIONTYPE = "Console"
#ElseIf _MYTYPE = "Web" Then
    #Const _MYFORMS = False
    #Const _MYWEBSERVICES = False
    #Const _MYUSERTYPE = "Web"
    #Const _MYCOMPUTERTYPE = "Web"
#ElseIf _MYTYPE = "WebControl" Then
    #Const _MYFORMS = False
    #Const _MYWEBSERVICES = True
    #Const _MYUSERTYPE = "Web"
    #Const _MYCOMPUTERTYPE = "Web"
#ElseIf _MYTYPE = "Custom" Then
    ' Custom project types can define their own constants elsewhere
#ElseIf _MYTYPE <> "Empty" Then
    ' Fallback: Treat unrecognized _MYTYPE values as "Empty"
    #Const _MYTYPE = "Empty"
#End If

#If _MYTYPE <> "Empty" Then
Namespace MergedMyNamespace50E26D7D27174AAEABCA70DEBD52E2FA

    ' Represents the application instance based on project type
#If _MYAPPLICATIONTYPE = "WindowsForms" OrElse _MYAPPLICATIONTYPE = "Windows" OrElse _MYAPPLICATIONTYPE = "Console" Then
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("MyTemplate", "11.0.0.0")> _
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
    Partial Friend Class MyApplication
#If _MYAPPLICATIONTYPE = "WindowsForms" Then
        Inherits Global.Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
#If TARGET = "winexe" Then
        ''' <summary>
        ''' Entry point for Windows Forms applications.
        ''' </summary>
        <Global.System.STAThread(), Global.System.Diagnostics.DebuggerHidden(), Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Shared Sub Main(ByVal Args As String())
            Try
                ' Enable high DPI support for modern displays
                Global.System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware)
                Global.System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(MyApplication.UseCompatibleTextRendering())
            Finally
                My.Application.Run(Args)
            End Try
        End Sub
#End If
#ElseIf _MYAPPLICATIONTYPE = "Windows" Then
        Inherits Global.Microsoft.VisualBasic.ApplicationServices.ApplicationBase
#ElseIf _MYAPPLICATIONTYPE = "Console" Then
        Inherits Global.Microsoft.VisualBasic.ApplicationServices.ConsoleApplicationBase
#End If
    End Class
#End If

    ' Provides access to computer-related functionality
#If _MYCOMPUTERTYPE <> "" Then
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("MyTemplate", "11.0.0.0")> _
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
    Partial Friend Class MyComputer
#If _MYCOMPUTERTYPE = "Windows" Then
        Inherits Global.Microsoft.VisualBasic.Devices.Computer
#ElseIf _MYCOMPUTERTYPE = "Web" Then
        Inherits Global.Microsoft.VisualBasic.Devices.ServerComputer
#End If
        <Global.System.Diagnostics.DebuggerHidden()> _
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
        Public Sub New()
            MyBase.New()
        End Sub
    End Class
#End If

    <Global.Microsoft.VisualBasic.HideModuleName()> _
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("MyTemplate", "11.0.0.0")> _
    Friend Module MyProject
#If _MYCOMPUTERTYPE <> "" Then
        ''' <summary>
        ''' Gets the current computer instance.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.Computer")> _
        Friend ReadOnly Property Computer() As MyComputer
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return m_ComputerObjectProvider.GetInstance()
            End Get
        End Property
        Private ReadOnly m_ComputerObjectProvider As New ThreadSafeObjectProvider(Of MyComputer)
#End If

#If _MYAPPLICATIONTYPE = "Windows" Or _MYAPPLICATIONTYPE = "WindowsForms" Or _MYAPPLICATIONTYPE = "Console" Then
        ''' <summary>
        ''' Gets the current application instance.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.Application")> _
        Friend ReadOnly Property Application() As MyApplication
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return m_AppObjectProvider.GetInstance()
            End Get
        End Property
        Private ReadOnly m_AppObjectProvider As New ThreadSafeObjectProvider(Of MyApplication)
#End If

#If _MYUSERTYPE = "Windows" Then
        ''' <summary>
        ''' Gets the current Windows user instance.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.User")> _
        Friend ReadOnly Property User() As Global.Microsoft.VisualBasic.ApplicationServices.User
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return m_UserObjectProvider.GetInstance()
            End Get
        End Property
        Private ReadOnly m_UserObjectProvider As New ThreadSafeObjectProvider(Of Global.Microsoft.VisualBasic.ApplicationServices.User)
#ElseIf _MYUSERTYPE = "Web" Then
        ''' <summary>
        ''' Gets the current web user instance.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.User")> _
        Friend ReadOnly Property User() As Global.Microsoft.VisualBasic.ApplicationServices.WebUser
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return m_UserObjectProvider.GetInstance()
            End Get
        End Property
        Private ReadOnly m_UserObjectProvider As New ThreadSafeObjectProvider(Of Global.Microsoft.VisualBasic.ApplicationServices.WebUser)
#End If

#If _MYFORMS = True Then
    #Const STARTUP_MY_FORM_FACTORY = "My.MyProject.Forms"
        ''' <summary>
        ''' Gets the forms collection for the application.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.Forms")> _
        Friend ReadOnly Property Forms() As MyForms
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return m_MyFormsObjectProvider.GetInstance()
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
        <Global.Microsoft.VisualBasic.MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")> _
        Friend NotInheritable Class MyForms
            <Global.System.Diagnostics.DebuggerHidden()> _
            Private Shared Function Create__Instance__(Of T As {New, Global.System.Windows.Forms.Form})(ByVal Instance As T) As T
                If Instance Is Nothing OrElse Instance.IsDisposed Then
                    If m_FormBeingCreated?.ContainsKey(GetType(T)) = True Then
                        Throw New Global.System.InvalidOperationException(Global.Microsoft.VisualBasic.CompilerServices.Utils.GetResourceString("WinForms_RecursiveFormCreate"))
                    End If
                    m_FormBeingCreated = If(m_FormBeingCreated, New Global.System.Collections.Hashtable())
                    m_FormBeingCreated.Add(GetType(T), Nothing)
                    Try
                        Return New T()
                    Catch ex As Global.System.Reflection.TargetInvocationException
                        ' Enhanced error handling: Provide a fallback message if InnerException is unexpectedly null
                        Dim innerMessage As String = ex.InnerException?.Message ?? "An unknown error occurred during form creation."
                        Dim betterMessage As String = Global.Microsoft.VisualBasic.CompilerServices.Utils.GetResourceString("WinForms_SeeInnerException", innerMessage)
                        Throw New Global.System.InvalidOperationException(betterMessage, ex.InnerException)
                    Finally
                        m_FormBeingCreated.Remove(GetType(T))
                    End Try
                Else
                    Return Instance
                End If
            End Function

            <Global.System.Diagnostics.DebuggerHidden()> _
            Private Sub Dispose__Instance__(Of T As Global.System.Windows.Forms.Form)(ByRef instance As T)
                instance.Dispose()
                instance = Nothing
            End Sub

            <Global.System.Diagnostics.DebuggerHidden()> _
            <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
            Public Sub New()
                MyBase.New()
            End Sub

            <Global.System.ThreadStatic()> Private Shared m_FormBeingCreated As Global.System.Collections.Hashtable

            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> Public Overrides Function Equals(ByVal o As Object) As Boolean
                Return MyBase.Equals(o)
            End Function
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> Public Overrides Function GetHashCode() As Integer
                Return MyBase.GetHashCode
            End Function
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> _
            Friend Overloads Function [GetType]() As Global.System.Type
                Return GetType(MyForms)
            End Function
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never)> Public Overrides Function ToString() As String
                Return MyBase.ToString
            End Function
        End Class

        Private m_MyFormsObjectProvider As New ThreadSafeObjectProvider(Of MyForms)
#End If

#If _MYWEBSERVICES = True Then
        ''' <summary>
        ''' Gets the web services collection for the application.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.WebServices")> _
        Friend ReadOnly Property WebServices() As MyWebServices
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return m_MyWebServicesObjectProvider.GetInstance()
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
        <Global.Microsoft.VisualBasic.MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")> _
        Friend NotInheritable Class MyWebServices
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _
            Public Overrides Function Equals(ByVal o As Object) As Boolean
                Return MyBase.Equals(o)
            End Function
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _
            Public Overrides Function GetHashCode() As Integer
                Return MyBase.GetHashCode
            End Function
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _
            Friend Overloads Function [GetType]() As Global.System.Type
                Return GetType(MyWebServices)
            End Function
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _
            Public Overrides Function ToString() As String
                Return MyBase.ToString
            End Function

            <Global.System.Diagnostics.DebuggerHidden()> _
            Private Shared Function Create__Instance__(Of T As {New})(ByVal instance As T) As T
                Return If(instance, New T())
            End Function

            <Global.System.Diagnostics.DebuggerHidden()> _
            Private Sub Dispose__Instance__(Of T)(ByRef instance As T)
                instance = Nothing
            End Sub

            <Global.System.Diagnostics.DebuggerHidden()> _
            <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
            Public Sub New()
                MyBase.New()
            End Sub
        End Class

        Private ReadOnly m_MyWebServicesObjectProvider As New ThreadSafeObjectProvider(Of MyWebServices)
#End If

#If _MYTYPE = "Web" Then
        ''' <summary>
        ''' Gets the current HTTP request, or Nothing if unavailable.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.Request")> _
        Friend ReadOnly Property Request() As Global.System.Web.HttpRequest
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return Global.System.Web.HttpContext.Current?.Request
            End Get
        End Property

        ''' <summary>
        ''' Gets the current HTTP response, or Nothing if unavailable.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.Response")> _
        Friend ReadOnly Property Response() As Global.System.Web.HttpResponse
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return Global.System.Web.HttpContext.Current?.Response
            End Get
        End Property

        ''' <summary>
        ''' Gets the logging instance for the web application.
        ''' </summary>
        <Global.System.ComponentModel.Design.HelpKeyword("My.Application.Log")> _
        Friend ReadOnly Property Log() As Global.Microsoft.VisualBasic.Logging.AspLog
            <Global.System.Diagnostics.DebuggerHidden()> _
            Get
                Return m_LogObjectProvider.GetInstance()
            End Get
        End Property

        Private ReadOnly m_LogObjectProvider As New ThreadSafeObjectProvider(Of Global.Microsoft.VisualBasic.Logging.AspLog)
#End If

        ' Provides thread-safe instances of type T
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
        <Global.System.Runtime.InteropServices.ComVisible(False)> _
        Friend NotInheritable Class ThreadSafeObjectProvider(Of T As New)
            Friend ReadOnly Property GetInstance() As T
#If TARGET = "library" Then
                <Global.System.Diagnostics.DebuggerHidden()> _
                Get
                    Dim value As T = m_Context.Value
                    If value Is Nothing Then
                        value = New T()
                        m_Context.Value = value
                    End If
                    Return value
                End Get
#Else
                <Global.System.Diagnostics.DebuggerHidden()> _
                Get
                    If m_ThreadStaticValue Is Nothing Then m_ThreadStaticValue = New T()
                    Return m_ThreadStaticValue
                End Get
#End If
            End Property

            <Global.System.Diagnostics.DebuggerHidden()> _
            <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _
            Public Sub New()
                MyBase.New()
            End Sub

#If TARGET = "library" Then
            Private ReadOnly m_Context As New Global.Microsoft.VisualBasic.MyServices.Internal.ContextValue(Of T)
#Else
            <Global.System.Runtime.CompilerServices.CompilerGenerated(), Global.System.ThreadStatic()> Private Shared m_ThreadStaticValue As T
#End If
        End Class
    End Module
End Namespace
#End If
