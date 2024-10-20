Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports Core
Imports System.IO
Imports SharpDX.Direct2D1
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Data
Imports System.Net.Mime

Public Class GameClient
    Inherits Game

    Public Graphics As GraphicsDeviceManager
    Public SpriteBatch As Graphics.SpriteBatch
    Public TextureCache As New Dictionary(Of String, Texture2D)()
    Public ReadOnly MultiplyBlendState As New BlendState()

    ' Thread-safe queue to hold render commands
    Public RenderQueue As New ConcurrentQueue(Of RenderCommand)()

    ' State tracking variables
    Private currentKeyboardState As KeyboardState
    Private previousKeyboardState As KeyboardState
    Private currentMouseState As MouseState
    Private previousMouseState As MouseState
    Private inGame As Boolean = True
    Private inMenu As Boolean = False
    Private inSmallChat As Boolean = False

    Private activeWindow As Integer = 0 ' Simulated window ID
    Private activeControl As Integer = 1 ' Example control ID

    ' Example control text (replace with actual GUI control management)
    Private controlText As String = ""
    Private controlLocked As Boolean = False
    Private maxTextLength As Integer = 20

    ' Ensure this class exists to store graphic info
    Public Class GraphicInfo
        Public Width As Integer
        Public Height As Integer
    End Class

#Region "Declarations"

    Friend TilesetWindow As RenderTarget2D
    Friend EditorAnimation_Anim1 As RenderTarget2D
    Friend EditorAnimation_Anim2 As RenderTarget2D
    Friend RenderTarget As RenderTarget2D
    Friend screenshotKey As Keys = Keys.F12 ' Key to trigger screenshot

    ' Graphics Declarations
    Friend TilesetTexture() As Texture2D
    Friend CharacterTexture() As Texture2D
    Friend PaperdollTexture() As Texture2D
    Friend ItemTexture() As Texture2D
    Friend ResourceTexture() As Texture2D
    Friend AnimationTexture() As Texture2D
    Friend SkillTexture() As Texture2D
    Friend ProjectileTexture() As Texture2D
    Friend FogTexture() As Texture2D
    Friend EmoteTexture() As Texture2D
    Friend PanoramaTexture() As Texture2D
    Friend ParallaxTexture() As Texture2D
    Friend PictureTexture() As Texture2D

    Friend BloodTexture As Texture2D
    Friend DirectionTexture As Texture2D
    Friend WeatherTexture As Texture2D
    Friend InterfaceTexture() As Texture2D
    Friend DesignTexture() As Texture2D
    Friend GradientTexture() As Texture2D
    Friend TargetTexture As Texture2D
    Friend ChatBubbleTexture As Texture2D
    Friend MapTintTexture As Texture2D
    Friend NightTexture As Texture2D
    Friend LightTexture As Texture2D
    Friend LightDynamicTexture As Texture2D
    Friend CursorTexture As Texture2D
    Friend ShadowTexture As Texture2D
    Friend BarTexture As Texture2D
    Friend PixelTexture As Texture2D
    Friend TransparentTexture As Texture2D

    ' GraphicInfo Declarations
    Friend TilesetGfxInfo() As GraphicInfo
    Friend CharacterGfxInfo() As GraphicInfo
    Friend PaperdollGfxInfo() As GraphicInfo
    Friend ItemGfxInfo() As GraphicInfo
    Friend ResourceGfxInfo() As GraphicInfo
    Friend AnimationGfxInfo() As GraphicInfo
    Friend SkillGfxInfo() As GraphicInfo
    Friend ProjectileGfxInfo() As GraphicInfo
    Friend FogGfxInfo() As GraphicInfo
    Friend EmoteGfxInfo() As GraphicInfo
    Friend PanoramaGfxInfo() As GraphicInfo
    Friend ParallaxGfxInfo() As GraphicInfo
    Friend PictureGfxInfo() As GraphicInfo

    Friend BloodGfxInfo As GraphicInfo
    Friend DirectionGfxInfo As GraphicInfo
    Friend WeatherGfxInfo As GraphicInfo
    Friend InterfaceGfxInfo() As GraphicInfo
    Friend DesignGfxInfo() As GraphicInfo
    Friend GradientGfxInfo() As GraphicInfo
    Friend TargetGfxInfo As GraphicInfo
    Friend ChatBubbleGfxInfo As GraphicInfo
    Friend MapTintGfxInfo As GraphicInfo
    Friend NightGfxInfo As GraphicInfo
    Friend LightGfxInfo As GraphicInfo
    Friend LightDynamicGfxInfo As GraphicInfo
    Friend CursorGfxInfo As GraphicInfo
    Friend ShadowGfxInfo As GraphicInfo
    Friend BarGfxInfo As GraphicInfo

#End Region

    Public Sub New()
        Settings.Load()
        GetResolutionSize(Setting.Resolution, ResolutionWidth, ResolutionHeight)

        Graphics = New GraphicsDeviceManager(Me)

        ' Set the desired window size
        graphics.PreferredBackBufferWidth = ResolutionWidth
        graphics.PreferredBackBufferHeight = ResolutionHeight

        ' Apply changes to ensure the window resizes
        graphics.ApplyChanges()

        Content.RootDirectory = "Content"

        ' Hook into the Exiting event to handle window close
        AddHandler Me.Exiting, AddressOf OnWindowClosed
    End Sub

    Protected Overrides Sub Initialize()
        ' Create the RenderTarget2D with the same size as the screen
        RenderTarget = New RenderTarget2D(
            GraphicsDevice,
            GraphicsDevice.PresentationParameters.BackBufferWidth,
            GraphicsDevice.PresentationParameters.BackBufferHeight,
            False,
            GraphicsDevice.PresentationParameters.BackBufferFormat,
            DepthFormat.Depth24)

        InitializeMultiplyBlendState()

        ' Optional: Set the title of the window
        Window.Title = Setting.GameName

        MyBase.Initialize()
    End Sub

    Sub InitializeMultiplyBlendState()
        With MultiplyBlendState
            .ColorSourceBlend = Microsoft.Xna.Framework.Graphics.Blend.DestinationColor
            .ColorDestinationBlend = Microsoft.Xna.Framework.Graphics.Blend.Zero
            .ColorBlendFunction = BlendFunction.Add
            .AlphaSourceBlend = Microsoft.Xna.Framework.Graphics.Blend.One
            .AlphaDestinationBlend = Microsoft.Xna.Framework.Graphics.Blend.Zero
            .AlphaBlendFunction = BlendFunction.Add
        End With
    End Sub

    Public Class RenderCommand
        Public Property Type As Byte
        Public Property Path As String
        Public Property Font As FontType
        Public Property Text As String
        Public Property sRect As Rectangle
        Public Property dRect As Rectangle
        Public Property X As Integer
        Public Property Y As Integer
        Public Property Color As Color
        Public Property Color2 As Color
    End Class

    Public Sub RenderTexture(ByRef texture As Texture2D, dX As Integer, dY As Integer,
                         sX As Integer, sY As Integer, dW As Integer, dH As Integer,
                         Optional sW As Integer = 1, Optional sH As Integer = 1,
                         Optional alpha As Byte = 255, Optional red As Byte = 255,
                         Optional green As Byte = 255, Optional blue As Byte = 255)

        ' If texture is still nothing, exit to avoid null reference exception
        If texture Is Nothing Then
            Console.WriteLine("Texture not found or failed to load.")
            Exit Sub
        End If

        ' Create source and destination rectangles
        Dim sourceRect As New Rectangle(sX, sY, sW, sH)
        Dim destinationRect As New Rectangle(dX, dY, dW, dH)

        ' Create a color with specified alpha and RGB values
        Dim color As New Color(red, green, blue, alpha)

        ' Draw the texture using SpriteBatch
        SpriteBatch.Begin()
        SpriteBatch.Draw(texture, destinationRect, sourceRect, color)
        SpriteBatch.End()
    End Sub

    Private Sub LoadAnimations()
        Dim count As Integer = GetFileCount(Core.Path.Animations)
        ReDim Client.AnimationTexture(count - 1)
        LoadTextures(Client.AnimationTexture, Core.Path.Animations)
    End Sub

    Private Sub LoadCharacters()
        Dim count As Integer = GetFileCount(Core.Path.Characters)
        ReDim Client.CharacterTexture(count - 1)
        LoadTextures(Client.CharacterTexture, Core.Path.Characters)
    End Sub

    Private Sub LoadEmotes()
        Dim count As Integer = GetFileCount(Core.Path.Emotes)
        ReDim Client.EmoteTexture(count - 1)
        LoadTextures(Client.EmoteTexture, Core.Path.Emotes)
    End Sub

    Private Sub LoadTilesets()
        Dim count As Integer = GetFileCount(Core.Path.Tilesets)
        ReDim Client.TilesetTexture(count - 1)
        LoadTextures(Client.TilesetTexture, Core.Path.Tilesets)
    End Sub

    Private Sub LoadFogs()
        Dim count As Integer = GetFileCount(Core.Path.Fogs)
        ReDim Client.FogTexture(count - 1)
        LoadTextures(Client.FogTexture, Core.Path.Fogs)
    End Sub

    Private Sub LoadItems()
        Dim count As Integer = GetFileCount(Core.Path.Items)
        ReDim Client.ItemTexture(count - 1)
        LoadTextures(Client.ItemTexture, Core.Path.Items)
    End Sub

    Private Sub LoadPanoramas()
        Dim count As Integer = GetFileCount(Core.Path.Panoramas)
        ReDim Client.PanoramaTexture(count - 1)
        LoadTextures(Client.PanoramaTexture, Core.Path.Panoramas)
    End Sub

    Private Sub LoadPaperdolls()
        Dim count As Integer = GetFileCount(Core.Path.Paperdolls)
        ReDim Client.PaperdollTexture(count - 1)
        LoadTextures(Client.PaperdollTexture, Core.Path.Paperdolls)
    End Sub

    Private Sub LoadParallax()
        Dim count As Integer = GetFileCount(Core.Path.Parallax)
        ReDim Client.ParallaxTexture(count - 1)
        LoadTextures(Client.ParallaxTexture, Core.Path.Parallax)
    End Sub

    Private Sub LoadPictures()
        Dim count As Integer = GetFileCount(Core.Path.Pictures)
        ReDim Client.PictureTexture(count - 1)
        LoadTextures(Client.PictureTexture, Core.Path.Pictures)
    End Sub

    Private Sub LoadProjectile()
        Dim count As Integer = GetFileCount(Core.Path.Projectiles)
        ReDim Client.ProjectileTexture(count - 1)
        LoadTextures(Client.ProjectileTexture, Core.Path.Projectiles)
    End Sub

    Private Sub LoadResources()
        Dim count As Integer = GetFileCount(Core.Path.Resources)
        ReDim Client.ResourceTexture(count - 1)
        LoadTextures(Client.ResourceTexture, Core.Path.Resources)
    End Sub

    Private Sub LoadSkills()
        Dim count As Integer = GetFileCount(Core.Path.Skills)
        ReDim Client.SkillTexture(count - 1)
        LoadTextures(Client.SkillTexture, Core.Path.Skills)
    End Sub

    Private Sub LoadInterface()
        Dim count As Integer = GetFileCount(Core.Path.Gui)
        ReDim Client.InterfaceTexture(count - 1)
        LoadTextures(Client.InterfaceTexture, Core.Path.Gui)
    End Sub

    Private Sub LoadGradients()
        Dim count As Integer = GetFileCount(Core.Path.Gradients)
        ReDim Client.GradientTexture(count - 1)
        LoadTextures(Client.GradientTexture, Core.Path.Gradients)
    End Sub

    Private Sub LoadDesigns()
        Dim count As Integer = GetFileCount(Core.Path.Designs)
        ReDim Client.DesignTexture(count - 1)
        LoadTextures(DesignTexture, Core.Path.Designs)
    End Sub

    Private Sub LoadFonts()
        For i = 1 To FontType.Count - 1
            Fonts(i) = LoadFont(Core.Path.Fonts, i)
        Next
    End Sub

    Protected Overrides Sub LoadContent()
        SpriteBatch = New Graphics.SpriteBatch(GraphicsDevice)

        TransparentTexture = New Texture2D(GraphicsDevice, 1, 1)
        TransparentTexture.SetData(New Color() {Color.White})

        LoadFonts()
        LoadAnimations()
        LoadCharacters()
        LoadEmotes()
        LoadTilesets()
        LoadFogs()
        LoadItems()
        LoadPanoramas()
        LoadPaperdolls()
        LoadParallax()
        LoadPictures()
        LoadProjectile()
        LoadResources()
        LoadSkills()
        LoadInterface()
        LoadGradients()
        LoadDesigns()
    End Sub

    Public Function LoadFont(path As String, font As FontType) As SpriteFont
        Return Content.Load(Of SpriteFont)(IO.Path.Combine(path, font))
    End Function

    Public Sub LoadTextures(ByRef texture() As Texture2D, path As String)
        If Not Directory.Exists(path) Then Exit Sub

        Dim files = Directory.GetFiles(path, "*" & GfxExt) ' Adjust for other formats if necessary
        For i As Integer = 0 To files.Length - 1
            Using stream As New FileStream(files(i), FileMode.Open)
                texture(i) = Texture2D.FromStream(GraphicsDevice, stream)
            End Using
        Next
    End Sub

    Public Sub EnqueueText(ByRef text As String, path As String, x As Integer, y As Integer, font As FontType, frontColor As Color, backColor As Color)
        ' Create the render command and enqueue it
        Dim command As New RenderCommand With {
            .Type = RenderType.Font,
            .Font = font,
            .Path = path,
            .Text = text,
            .X = x,
            .Y = y,
            .Color = frontColor,
            .Color2 = backColor
        }

        RenderQueue.Enqueue(command)
    End Sub

    Public Sub EnqueueTexture(ByRef path As String, dX As Integer, dY As Integer,
                          sX As Integer, sY As Integer, dW As Integer, dH As Integer,
                          Optional sW As Integer = 1, Optional sH As Integer = 1,
                          Optional alpha As Byte = 255, Optional red As Byte = 255,
                          Optional green As Byte = 255, Optional blue As Byte = 255)

        ' Create the destination and source rectangles
        Dim dRect As New Rectangle(dX, dY, dW, dH)
        Dim sRect As New Rectangle(sX, sY, sW, sH)

        ' Create the color with the specified alpha and RGB values
        Dim color As New Color(red, green, blue, alpha)

        ' Enqueue the render command
        Dim command As New RenderCommand With {
            .Type = RenderType.Texture,
            .Path = path,
            .dRect = dRect,
            .sRect = sRect,
            .Color = color
        }

        RenderQueue.Enqueue(command)
    End Sub

    ' Load or retrieve a texture from the cache
    Public Function GetTexture(path As String) As Texture2D
        If textureCache.ContainsKey(path) Then
            ' Return the already loaded texture
            Return textureCache(path)
        End If

        ' Load the texture and add it to the cache
        Try
            Using stream As New FileStream(path, FileMode.Open)
                Dim texture As Texture2D = Texture2D.FromStream(graphicsDevice, stream)
                textureCache(path) = texture
                Return texture
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error loading texture from {path}: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Protected Overrides Sub Draw(gameTime As GameTime)
        GraphicsDevice.Clear(Color.CornflowerBlue)

        SpriteBatch.Begin()

        ' Dequeue and process all render commands
        Dim command As RenderCommand

        While RenderQueue.TryDequeue(command)
            Select Case command.Type
                    Case RenderType.Texture
                        SpriteBatch.Draw(GetTexture(command.path), command.dRect, command.sRect, command.Color)
                    
                    Case RenderType.Font
                        ' Calculate the shadow position
                        Dim shadowPosition As New Vector2(command.X + 1, command.Y + 1)

                        ' Draw the shadow (backString equivalent)
                        Client.SpriteBatch.DrawString(Fonts(command.Font), command.Text, shadowPosition, command.Color2,
                                               0.0F, Vector2.Zero, 10 / 16.0F, SpriteEffects.None, 0.0F)

                        ' Draw the main text (frontString equivalent)
                        Client.SpriteBatch.DrawString(Fonts(command.Font), command.Text, New Vector2(command.X, command.Y), command.Color,
                                               0.0F, Vector2.Zero, 10 / 16.0F, SpriteEffects.None, 0.0F)
            End Select
        End While

        SpriteBatch.End()

        MyBase.Draw(gameTime)
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        Dim mouseButton As MouseButton

        MyBase.Update(gameTime)

        ' Poll the current keyboard state
        currentKeyboardState = Keyboard.GetState()
        currentMouseState = Mouse.GetState()

        ' Convert adjusted coordinates to game world coordinates
        CurX = TileView.Left + Math.Floor((currentMouseState.X + Camera.Left) / PicX)
        CurY = TileView.Top + Math.Floor((currentMouseState.Y + Camera.Top) / PicY)

        ' Store raw mouse coordinates for interface interactions
        CurMouseX = currentMouseState.X
        CurMouseY = currentMouseState.Y

        If currentMouseState.Position <> previousMouseState.Position Then
            HandleInterfaceEvents(EntState.MouseMove)
        End If

        ' Handle character input if a window and control are active
        If activeWindow > 0 AndAlso activeControl > 0 AndAlso Not controlLocked Then
            HandleTextInput()
        End If

        ' Capture screenshot when the screenshot key is pressed
        If currentKeyboardState.IsKeyDown(screenshotKey) Then
            TakeScreenshot()
        End If

        ' Process key inputs
        If inGame Then
            ' Handle mouse wheel scrolling
            HandleMouseWheelScrolling()

            If currentMouseState.LeftButton = ButtonState.Released Or currentMouseState.RightButton = ButtonState.Released Or currentMouseState.MiddleButton = ButtonState.Released Then
                HandleInterfaceEvents(EntState.MouseUp)
            End If

            If currentMouseState.LeftButton = ButtonState.Pressed Then
                Dim currentTime As Integer = Environment.TickCount

                mouseButton = MouseButton.Left

                ' Double-click detection
                If currentTime - LastLeftClickTime <= DoubleClickTImer Then
                    HandleInterfaceEvents(EntState.DblClick)
                    LastLeftClickTime = 0 ' Reset to avoid triple-clicks
                Else
                    HandleInterfaceEvents(EntState.MouseDown)
                    LastLeftClickTime = currentTime
                End If

                If inGame Then
                    If PetAlive(MyIndex) AndAlso IsInBounds() Then
                        PetMove(CurX, CurY)
                    End If
                    CheckAttack(True)
                    PlayerSearch(CurX, CurY, 0)
                End If
            ElseIf currentMouseState.RightButton = ButtonState.Pressed Then
                mouseButton = MouseButton.Right

                If inGame Then
                    If Keyboard.GetState().IsKeyDown(Keys.LeftShift) Then
                        ' Admin warp if pressing Shift and right-clicking
                        If GetPlayerAccess(MyIndex) >= AccessType.Moderator Then
                            AdminWarp(CurX, CurY)
                        End If
                    Else
                        ' Show right-click menu
                        For i = 1 To MAX_PLAYERS
                            If IsPlaying(i) AndAlso GetPlayerMap(i) = GetPlayerMap(MyIndex) AndAlso
                               GetPlayerX(i) = CurX AndAlso GetPlayerY(i) = CurY Then
                                ShowPlayerMenu(i, currentMouseState.X, currentMouseState.Y)
                            End If
                        Next

                        PlayerSearch(CurX, CurY, 1)
                    End If
                End If
            ElseIf currentMouseState.MiddleButton = ButtonState.Pressed Then
                mouseButton = MouseButton.Middle
            End If

            If mouseButton <> MouseButton.None Then
                If MyEditorType = EditorType.Map Then
                    frmEditor_Map.MapEditorMouseDown(mouseButton, currentMouseState.X, currentMouseState.Y, False)
                End If
            End If

            HandleMovement()

            If IsKeyPressed(Keys.Escape) Then
                If inMenu Then Exit Sub
                ToggleMenu()
            End If

            If IsKeyPressed(Keys.Enter) Then
                If inSmallChat Then
                    ShowChat()
                    inSmallChat = False
                    Exit Sub
                End If
                HandlePressEnter()
            End If

            If IsKeyPressed(Keys.Space) Then
                CheckMapGetItem()
            End If

            If IsKeyPressed(Keys.I) Then
                ' hide/show inventory
                If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Inv
            End If

            If IsKeyPressed(Keys.C) Then
                ' hide/show char
                If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Char
            End If

            If IsKeyPressed(Keys.K) Then
                ' hide/show skills
                If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Skills
            End If

            If IsKeyPressed(Keys.Up) Then VbKeyUp = False
            If IsKeyPressed(Keys.Down) Then VbKeyDown = False
            If IsKeyPressed(Keys.Left) Then VbKeyLeft = False
            If IsKeyPressed(Keys.Right) Then VbKeyRight = False
            If IsKeyPressed(Keys.LeftControl) Then VbKeyControl = False
            If IsKeyPressed(Keys.LeftShift) Then VbKeyShift = False

            HandleHotbarInput()
        End If

        If activeWindow > 0 Then HandleActiveWindowInput()

        ' Save the current state as the previous state for the next frame
        previousKeyboardState = currentKeyboardState
        previousMouseState = currentMouseState

        MyBase.Update(gameTime)
    End Sub

    Private Sub HandleTextInput()
        ' Get all pressed keys from the current frame
        Dim pressedKeys = currentKeyboardState.GetPressedKeys()

        For Each key In pressedKeys
            ' Check if this key was newly pressed
            If previousKeyboardState.IsKeyUp(key) Then
                Dim character As Char = ConvertKeyToChar(key)

                ' Ignore special control characters
                If character = ChrW(8) OrElse character = ChrW(13) OrElse
                   character = ChrW(9) OrElse character = ChrW(27) Then
                    Continue For
                End If

                ' Ensure text length doesn't exceed the max length
                If controlText.Length < maxTextLength Then
                    controlText &= character ' Append the character to control text
                    Console.WriteLine("Updated Control Text: " & controlText)
                End If
            End If
        Next
    End Sub

    Private Function ConvertKeyToChar(key As Keys) As Char
        ' Convert MonoGame Keys to corresponding Char (basic conversion)
        Select Case key
            Case Keys.A To Keys.Z
                ' Handle letters (convert to lowercase for simplicity)
                Return ChrW(key - Keys.A + AscW("a"c))
            Case Keys.D0 To Keys.D9
                ' Handle number keys
                Return ChrW(key - Keys.D0 + AscW("0"c))
            Case Keys.Space
                Return " "c
            Case Keys.OemPeriod
                Return "."c
            Case Else
                ' Default to empty character for unsupported keys
                Return ChrW(0)
        End Select
    End Function

    Private Sub HandleMouseWheelScrolling()
        ' Check if the scroll wheel has moved
        Dim scrollDelta As Integer = currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue

        If scrollDelta <> 0 Then
            Console.WriteLine($"Mouse Wheel Scrolled: {scrollDelta}")

            If MyEditorType = EditorType.Map Then
                If scrollDelta > 0 Then ' Scrolling up
                    If Keyboard.GetState().IsKeyDown(Keys.LeftShift) Then
                        If frmEditor_Map.cmbLayers.SelectedIndex + 1 < LayerType.Count - 1 Then
                            frmEditor_Map.cmbLayers.SelectedIndex += 1
                        End If
                    Else
                        If frmEditor_Map.cmbTileSets.SelectedIndex > 0 Then
                            frmEditor_Map.cmbTileSets.SelectedIndex -= 1
                        End If
                    End If
                Else ' Scrolling down
                    If Keyboard.GetState().IsKeyDown(Keys.LeftShift) Then
                        If frmEditor_Map.cmbLayers.SelectedIndex > 0 Then
                            frmEditor_Map.cmbLayers.SelectedIndex -= 1
                        End If
                    Else
                        If frmEditor_Map.cmbTileSets.SelectedIndex + 1 < NumTileSets Then
                            frmEditor_Map.cmbTileSets.SelectedIndex += 1
                        End If
                    End If
                End If
            End If

            ' Scroll chat box based on the scroll direction
            If scrollDelta > 0 Then
                ScrollChatBox(0)
            Else
                ScrollChatBox(1)
            End If

            ' Handle interface events
            HandleInterfaceEvents(EntState.MouseScroll)
        End If
    End Sub

    Private Sub HandleMovement()
        If currentKeyboardState.IsKeyDown(Keys.W) OrElse currentKeyboardState.IsKeyDown(Keys.Up) Then
            VbKeyUp = True
        End If

        If currentKeyboardState.IsKeyDown(Keys.S) OrElse currentKeyboardState.IsKeyDown(Keys.Down) Then
            VbKeyDown = True
        End If

        If currentKeyboardState.IsKeyDown(Keys.A) OrElse currentKeyboardState.IsKeyDown(Keys.Left) Then
            VbKeyLeft = True
        End If

        If currentKeyboardState.IsKeyDown(Keys.D) OrElse currentKeyboardState.IsKeyDown(Keys.Right) Then
            VbKeyRight = True
        End If
    End Sub

    Private Sub ToggleMenu()
        If Windows(GetWindowIndex("winOptions")).Window.Visible Then
            HideWindow(GetWindowIndex("winOptions"))
            CloseComboMenu()
        ElseIf Windows(GetWindowIndex("winChat")).Window.Visible Then
            HideChat()
        ElseIf Windows(GetWindowIndex("winEscMenu")).Window.Visible Then
            HideWindow(GetWindowIndex("winEscMenu"))
        Else
            ShowWindow(GetWindowIndex("winEscMenu"), True)
        End If
    End Sub

    Private Sub HandleHotbarInput()
        If inSmallChat Then
            For i = 1 To MAX_Hotbar - 1
                If IsKeyPressed(DirectCast(26 + i, Keys)) Then
                    SendUseHotbarSlot(i)
                End If
            Next

            If IsKeyPressed(Keys.NumPad0) Then
                SendUseHotbarSlot(MAX_Hotbar)
            End If
        End If
    End Sub

    Private Sub HandleActiveWindowInput()
        If Windows(activeWindow).Window.Visible Then
            If Windows(activeWindow).ActiveControl > 0 Then
                If IsKeyPressed(Keys.Insert) Then
                    SendRequestAdmin()
                ElseIf IsKeyPressed(Keys.Back) Then
                    HandleBackspaceInput()
                ElseIf IsKeyPressed(Keys.Enter) Then
                    ActivateControl()
                End If
            End If
        End If
    End Sub

    Private Sub HandleBackspaceInput()
        Dim activeControl = Windows(activeWindow).Controls(Windows(activeWindow).ActiveControl)
        If activeControl.Text.Length > 0 Then
            activeControl.Text = activeControl.Text.Substring(0, activeControl.Text.Length - 1)
        End If
    End Sub

    ' Utility function to detect single key press (current frame)
    Private Function IsKeyPressed(key As Keys) As Boolean
        Return currentKeyboardState.IsKeyDown(key) AndAlso previousKeyboardState.IsKeyUp(key)
    End Function

    Private Sub OnWindowClosed(ByVal sender As Object, ByVal e As EventArgs)
        ' Handle any cleanup logic before the game exits
        Console.WriteLine("Window Closed")
        DestroyGame()
        Environment.Exit(0)
    End Sub

    Public Sub TakeScreenshot()
        ' Set the render target to our RenderTarget2D
        Client.GraphicsDevice.SetRenderTarget(Client.RenderTarget)
        Client.GraphicsDevice.Clear(Color.Transparent) ' Clear with transparency

        ' Draw everything onto the render target
        Draw(New GameTime()) ' Redraw the scene to the render target

        ' Reset the render target to the back buffer
        Client.GraphicsDevice.SetRenderTarget(Nothing)

        ' Save the screenshot to a PNG file
        Using stream As FileStream = New FileStream($"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png", FileMode.Create)
            Client.RenderTarget.SaveAsPng(stream, Client.RenderTarget.Width, Client.RenderTarget.Height)
        End Using
    End Sub

    ' Draw a filled rectangle with an optional outline
    Public Sub DrawRectangle(position As Vector2, size As Vector2, fillColor As Color, outlineColor As Color, outlineThickness As Single)
        ' Create a 1x1 white texture for drawing
        Dim whiteTexture As New Texture2D(SpriteBatch.GraphicsDevice, 1, 1)

        whiteTexture.SetData(New Color() {Color.White})

        ' Draw the filled rectangle
        SpriteBatch.Draw(whiteTexture, New Rectangle(position.ToPoint(), size.ToPoint()), fillColor)

'        Draw the outline if thickness > 0
        If outlineThickness > 0 Then
            ' Create the four sides of the outline
            Dim left As New Rectangle(position.ToPoint(), New Point(CInt(outlineThickness), CInt(size.Y)))
            Dim top As New Rectangle(position.ToPoint(), New Point(CInt(size.X), CInt(outlineThickness)))
            Dim right As New Rectangle(New Point(CInt(position.X + size.X - outlineThickness), CInt(position.Y)), New Point(CInt(outlineThickness), CInt(size.Y)))
            Dim bottom As New Rectangle(New Point(CInt(position.X), CInt(position.Y + size.Y - outlineThickness)), New Point(CInt(size.X), CInt(outlineThickness)))

            ' Draw the outline rectangles
            SpriteBatch.Draw(whiteTexture, left, outlineColor)
            SpriteBatch.Draw(whiteTexture, top, outlineColor)
            SpriteBatch.Draw(whiteTexture, right, outlineColor)
            SpriteBatch.Draw(whiteTexture, bottom, outlineColor)
        End If

        ' Dispose the texture to free memory
        whiteTexture.Dispose()
    End Sub

    ''' <summary>
    ''' Draws a rectangle with a fill color and an outline.
    ''' </summary>
    ''' <param name="rect">The Rectangle to be drawn.</param>
    ''' <param name="fillColor">The color to fill the rectangle.</param>
    ''' <param name="outlineColor">The color of the outline.</param>
    ''' <param name="outlineThickness">The thickness of the outline.</param>
    Public Sub DrawRectangleWithOutline(
        rect As Rectangle,
        fillColor As Color,
        outlineColor As Color,
        outlineThickness As Single)

        ' Create a 1x1 white texture
        Dim whiteTexture As New Texture2D(SpriteBatch.GraphicsDevice, 1, 1)
        whiteTexture.SetData(New Color() {Color.White})

        ' Draw the filled rectangle
        SpriteBatch.Draw(whiteTexture, rect, fillColor)

        ' Draw the outline if thickness > 0
        If outlineThickness > 0 Then
            ' Define outline rectangles (left, top, right, bottom)
            Dim left As New Rectangle(rect.Left, rect.Top, CInt(outlineThickness), rect.Height)
            Dim top As New Rectangle(rect.Left, rect.Top, rect.Width, CInt(outlineThickness))
            Dim right As New Rectangle(rect.Right - CInt(outlineThickness), rect.Top, CInt(outlineThickness), rect.Height)
            Dim bottom As New Rectangle(rect.Left, rect.Bottom - CInt(outlineThickness), rect.Width, CInt(outlineThickness))

            ' Draw the outline rectangles
            SpriteBatch.Draw(whiteTexture, left, outlineColor)
            SpriteBatch.Draw(whiteTexture, top, outlineColor)
            SpriteBatch.Draw(whiteTexture, right, outlineColor)
            SpriteBatch.Draw(whiteTexture, bottom, outlineColor)
        End If

        ' Dispose the texture after use
        whiteTexture.Dispose()
    End Sub

    Public Sub DrawSelectionRectangle()
        Dim selectionRect As New Rectangle(
            EditorTileSelStart.X * PicX, EditorTileSelStart.Y * PicY,
            EditorTileWidth * PicX, EditorTileHeight * PicY
        )

        ' Begin the sprite batch and draw a semi-transparent overlay (optional)
        SpriteBatch.Begin()
        SpriteBatch.Draw(TilesetWindow, selectionRect, Color.Red * 0.4F)
        SpriteBatch.End()
    End Sub

    Private Sub DrawOutlineRectangle(x As Integer, y As Integer, width As Integer, height As Integer, color As Color, thickness As Single)
        Dim whiteTexture As New Texture2D(SpriteBatch.GraphicsDevice, 1, 1)

        SpriteBatch.Begin()

        ' Define four rectangles for the outline
        Dim left As New Rectangle(x, y, thickness, height)
        Dim top As New Rectangle(x, y, width, thickness)
        Dim right As New Rectangle(x + width - thickness, y, thickness, height)
        Dim bottom As New Rectangle(x, y + height - thickness, width, thickness)

        ' Draw the outline
        SpriteBatch.Draw(whiteTexture, left, color)
        SpriteBatch.Draw(whiteTexture, top, color)
        SpriteBatch.Draw(whiteTexture, right, color)
        SpriteBatch.Draw(whiteTexture, bottom, color)
        SpriteBatch.End()
    End Sub

    Public Function QbColorToXnaColor(qbColor As Integer) As Color
        Select Case qbColor
            Case ColorType.Black
                Return Color.Black
            Case ColorType.Blue
                Return Color.Blue
            Case ColorType.Green
                Return Color.Green
            Case ColorType.Cyan
                Return Color.Cyan
            Case ColorType.Red
                Return Color.Red
            Case ColorType.Magenta
                Return Color.Magenta
            Case ColorType.Brown
                Return Color.Brown
            Case ColorType.Gray
                Return Color.LightGray
            Case ColorType.DarkGray
                Return Color.Gray
            Case ColorType.BrightBlue
                Return Color.LightBlue
            Case ColorType.BrightGreen
                Return Color.LightGreen
            Case ColorType.BrightCyan
                Return Color.LightCyan
            Case ColorType.BrightRed
                Return Color.LightCoral
            Case ColorType.Pink
                Return Color.Orchid
            Case ColorType.Yellow
                Return Color.Yellow
            Case ColorType.White
                Return Color.White
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(qbColor), "Invalid QbColor value.")
        End Select
    End Function

    Friend Sub DrawEmote(x2 As Integer, y2 As Integer, sprite As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer, anim As Integer

        If sprite < 1 Or sprite > NumEmotes Then Exit Sub
            If ShowAnimLayers = True Then
            anim = 1
        Else
            anim = 0
        End If

        With rec
            .Y = 0
            .Height = PicX
            .X = anim * (EmoteGfxInfo(sprite).Width / 2)
            .Width = (EmoteGfxInfo(sprite).Width / 2)
        End With

        x = ConvertMapX(x2)
        y = ConvertMapY(y2) - (PicY + 16)

        RenderTexture(EmoteTexture(sprite), x, y, rec.X, rec.Y, rec.Width, rec.Height)
    End Sub

    Friend Sub DrawDirections(x As Integer, y As Integer)
        Dim rec As Rectangle, i As Integer

        ' render grid
        rec.Y = 24
        rec.X = 0
        rec.Width = 32
        rec.Height = 32

        RenderTexture(DirectionTexture, ConvertMapX(x * PicX), ConvertMapY(y * PicY), rec.X, rec.Y, rec.Width,
                     rec.Height, rec.Width, rec.Height)

        ' render dir blobs
        For i = 1 To 4
            rec.X = (i - 1) * 8
            rec.Width = 8

            ' find out whether render blocked or not
            If Not IsDirBlocked(MyMap.Tile(x, y).DirBlock, CByte(i)) Then
                rec.Y = 8
            Else
                rec.Y = 16
            End If
            rec.Height = 8

            RenderTexture(DirectionTexture, ConvertMapX(x * PicX) + DirArrowX(i), ConvertMapY(y * PicY) + DirArrowY(i), rec.X, rec.Y, rec.Width, rec.Height, rec.Width, rec.Height)
        Next
    End Sub

    Friend Sub DrawPaperdoll(x2 As Integer, y2 As Integer, sprite As Integer, anim As Integer, spritetop As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer
        Dim width As Integer, height As Integer

        If sprite < 1 Or sprite > NumPaperdolls Then Exit Sub

        With rec
            .Y = spritetop * (PaperdollGfxInfo(sprite).Height / 4)
            .Height = (PaperdollGfxInfo(sprite).Height / 4)
            .X = anim * (PaperdollGfxInfo(sprite).Width / 4)
            .Width = (PaperdollGfxInfo(sprite).Width / 4)
        End With

        x = ConvertMapX(x2)
        y = ConvertMapY(y2)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        RenderTexture(PaperdollTexture(sprite), x, y, rec.X, rec.Y, rec.Width, rec.Height)
    End Sub

    Friend Sub DrawNPC(MapNpcNum As Integer)
        Dim anim As Byte
        Dim x As Integer
        Dim y As Integer
        Dim sprite As Integer, spriteLeft As Integer
        Dim rect As Rectangle
        Dim attackSpeed As Integer = 1000

        ' Check if NPC exists
        If MyMapNPC(MapNPCNum).Num = 0 Then Exit Sub

        ' Ensure NPC is within the tile view range
        If MyMapNPC(MapNPCNum).X < TileView.Left Or MyMapNPC(MapNPCNum).X > TileView.Right Then Exit Sub
        If MyMapNPC(MapNPCNum).Y < TileView.Top Or MyMapNPC(MapNPCNum).Y > TileView.Bottom Then Exit Sub

        ' Stream NPC if not yet loaded
        StreamNpc(MyMapNPC(MapNPCNum).Num)

        ' Get the sprite of the NPC
        sprite = Type.NPC(MyMapNPC(MapNPCNum).Num).Sprite

        ' Validate sprite
        If sprite < 1 Or sprite > NumCharacters Then Exit Sub

        ' Reset animation frame
        anim = 0

        ' Check for attacking animation
        If MyMapNPC(MapNPCNum).AttackTimer + (attackSpeed / 2) > GetTickCount() AndAlso MyMapNPC(MapNPCNum).Attacking = 1 Then
            anim = 3
        Else
            ' Walking animation based on direction
            Select Case MyMapNPC(MapNPCNum).Dir
                Case DirectionType.Up
                    If MyMapNPC(MapNPCNum).YOffset > 8 Then anim = MyMapNPC(MapNPCNum).Steps
                Case DirectionType.Down
                    If MyMapNPC(MapNPCNum).YOffset < -8 Then anim = MyMapNPC(MapNPCNum).Steps
                Case DirectionType.Left
                    If MyMapNPC(MapNPCNum).XOffset > 8 Then anim = MyMapNPC(MapNPCNum).Steps
                Case DirectionType.Right
                    If MyMapNPC(MapNPCNum).XOffset < -8 Then anim = MyMapNPC(MapNPCNum).Steps
            End Select
        End If

        ' Reset attacking state if attack timer has passed
        With MyMapNPC(MapNPCNum)
            If .AttackTimer + attackSpeed < GetTickCount() Then
                .Attacking = 0
                .AttackTimer = 0
            End If
        End With

        ' Set sprite sheet position based on direction
        Select Case MyMapNPC(MapNPCNum).Dir
            Case DirectionType.Up
                spriteLeft = 3
            Case DirectionType.Right
                spriteLeft = 2
            Case DirectionType.Down
                spriteLeft = 0
            Case DirectionType.Left
                spriteLeft = 1
        End Select

        ' Create the rectangle for rendering the sprite
        rect = New Rectangle(anim * (CharacterGfxInfo(sprite).Width / 4), spriteLeft * (CharacterGfxInfo(sprite).Height / 4),
                             CharacterGfxInfo(sprite).Width / 4, CharacterGfxInfo(sprite).Height / 4)

        ' Calculate X and Y coordinates for rendering
        x = MyMapNPC(MapNPCNum).X * PicX + MyMapNPC(MapNPCNum).XOffset - ((CharacterGfxInfo(sprite).Width / 4 - 32) / 2)

        If CharacterGfxInfo(sprite).Height / 4 > 32 Then
            ' Larger sprites need an offset for height adjustment
            y = MyMapNPC(MapNPCNum).Y * PicY + MyMapNPC(MapNPCNum).YOffset - (CharacterGfxInfo(sprite).Height / 4 - 32)
        Else
            ' Normal sprite height
            y = MyMapNPC(MapNPCNum).Y * PicY + MyMapNPC(MapNPCNum).YOffset
        End If

        ' Draw shadow and NPC sprite
        DrawShadow(x, y + 16)
        DrawCharacterSprite(sprite, x, y, rect)
    End Sub

    Friend Sub DrawMapItem(itemNum As Integer)
        Dim srcrec As Rectangle, destrec As Rectangle
        Dim picNum As Integer
        Dim x As Integer, y As Integer
        StreamItem(MyMapItem(itemNum).Num)

        picNum = Type.Item(MyMapItem(itemNum).Num).Icon

        If picNum < 1 Or picNum > NumItems Then Exit Sub

        With MyMapItem(itemNum)
            If .X < TileView.Left Or .X > TileView.Right Then Exit Sub
            If .Y < TileView.Top Or .Y > TileView.Bottom Then Exit Sub
        End With

        srcrec = New Rectangle(0, 0, PicX, PicY)
        destrec = New Rectangle(ConvertMapX(MyMapItem(itemNum).X * PicX), ConvertMapY(MyMapItem(itemNum).Y * PicY), PicX, PicY)

        x = ConvertMapX(MyMapItem(itemNum).X * PicX)
        y = ConvertMapY(MyMapItem(itemNum).Y * PicY)

        RenderTexture(ItemTexture(picNum), x, y, srcrec.X, srcrec.Y, srcrec.Width, srcrec.Height, srcrec.Width, srcrec.Height)
    End Sub

    Friend Sub DrawCharacterSprite(sprite As Integer, x2 As Integer, y2 As Integer, sRECT As Rectangle)
        Dim x As Integer
        Dim y As Integer

        If sprite < 1 Or sprite > NumCharacters Then Exit Sub

        x = ConvertMapX(x2)
        y = ConvertMapY(y2)

        RenderTexture(CharacterTexture(sprite), x, y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height, sRECT.Width, sRECT.Height)
    End Sub

    Friend Sub DrawShadow(x2 As Integer, y2 As Integer)
        Dim x As Integer
        Dim y As Integer
        Dim srcrec As Rectangle
        Dim destrec As Rectangle

        If Type.Setting.Shadow = 0 Then Exit Sub

        x = ConvertMapX(x2)
        y = ConvertMapY(y2)
        srcrec = New Rectangle(0, 0, PicX, PicY)
        destrec = New Rectangle(ConvertMapX(x * PicX), ConvertMapY(y * PicY), PicX, PicY)

        RenderTexture(ShadowTexture, x, y, srcrec.X, srcrec.Y, destrec.Width, destrec.Height, destrec.Width, destrec.Height)
    End Sub

    Friend Sub DrawBlood(index As Integer)
        Dim srcrec As Rectangle
        Dim destrec As Rectangle
        Dim x As Integer
        Dim y As Integer

        With Blood(index)
            If .X < TileView.Left Or .X > TileView.Right Then Exit Sub
            If .Y < TileView.Top Or .Y > TileView.Bottom Then Exit Sub

            ' check if we should be seeing it
            If .Timer + 20000 < GetTickCount() Then Exit Sub

            x = ConvertMapX(Blood(index).X * PicX)
            y = ConvertMapY(Blood(index).Y * PicY)

            srcrec = New Rectangle((.Sprite - 1) * PicX, 0, PicX, PicY)
            destrec = New Rectangle(ConvertMapX(.X * PicX), ConvertMapY(.Y * PicY), PicX, PicY)

            RenderTexture(BloodTexture, x, y, srcrec.X, srcrec.Y, srcrec.Width, srcrec.Height)

        End With
    End Sub

    Public Sub DrawBars()
        Dim Left As Long, Top As Long, Width As Long, Height As Long
        Dim tmpX As Long, tmpY As Long, barWidth As Long, i As Long, NpcNum As Long

        ' dynamic bar calculations
        Width = BarGfxInfo.Width
        Height = BarGfxInfo.Height / 4

        ' render npc health bars
        For i = 1 To MAX_MAP_NPCS
            NpcNum = Type.MyMapNPC(i).Num
            ' exists?
            If NpcNum > 0 Then
                ' alive?
                If Type.MyMapNPC(i).Vital(VitalType.HP) > 0 And Type.MyMapNPC(i).Vital(VitalType.HP) < Type.NPC(NPCNum).HP Then
                    ' lock to npc
                    tmpX = Type.MyMapNPC(i).X * PicX + Type.MyMapNPC(i).XOffset + 16 - (Width / 2)
                    tmpY = Type.MyMapNPC(i).Y * PicY + Type.MyMapNPC(i).YOffset + 35

                    ' calculate the width to fill
                    If Width > 0 Then BarWidth_NpcHP_Max(i) = ((Type.MyMapNPC(i).Vital(VitalType.HP) / Width) / (Type.NPC(NPCNum).HP / Width)) * Width

                    ' draw bar background
                    Top = Height * 3 ' HP bar background
                    Left = 0
                    RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, Width, Height, Width, Height)

                    ' draw the bar proper
                    Top = 0 ' HP bar
                    Left = 0
                    RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, BarWidth_NpcHP(i), Height, BarWidth_NpcHP(i), Height)
                End If
            End If
        Next

        For i = 1 To MAX_PLAYERS
            If GetPlayerMap(i) = GetPlayerMap(i) Then
                If GetPlayerVital(i, VitalType.HP) > 0 And GetPlayerVital(i, VitalType.HP) < GetPlayerMaxVital(i, VitalType.HP) Then
                    ' lock to Player
                    tmpX = GetPlayerX(i) * PicX + Type.Player(i).XOffset + 16 - (Width / 2)
                    tmpY = GetPlayerY(i) * PicY + Type.Player(i).YOffset + 35

                    ' calculate the width to fill
                    If Width > 0 Then BarWidth_PlayerHP_Max(i) = ((GetPlayerVital(i, VitalType.HP) / Width) / (GetPlayerMaxVital(i, VitalType.HP) / Width)) * Width

                    ' draw bar background
                    Top = Height * 3 ' HP bar background
                    Left = 0
                    RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, Width, Height, Width, Height)

                    ' draw the bar proper
                    Top = 0 ' HP bar
                    Left = 0
                    RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, BarWidth_PlayerHP(i), Height, BarWidth_PlayerHP(i), Height)
                End If

                If GetPlayerVital(i, VitalType.SP) > 0 And GetPlayerVital(i, VitalType.SP) < GetPlayerMaxVital(i, VitalType.SP) Then
                    ' lock to Player
                    tmpX = GetPlayerX(i) * PicX + Type.Player(i).XOffset + 16 - (Width / 2)
                    tmpY = GetPlayerY(i) * PicY + Type.Player(i).YOffset + 35 + Height

                    ' calculate the width to fill
                    If Width > 0 Then BarWidth_PlayerSP_Max(i) = ((GetPlayerVital(i, VitalType.SP) / Width) / (GetPlayerMaxVital(i, VitalType.SP) / Width)) * Width

                    ' draw bar background
                    Top = Height * 3 ' SP bar background
                    Left = 0
                    RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, Width, Height, Width, Height)

                    ' draw the bar proper
                    Top = Height * 1 ' SP bar
                    Left = 0
                    RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, BarWidth_PlayerSP(i), Height, BarWidth_PlayerSP(i), Height)
                End If

                If SkillBuffer > 0 Then
                    If Type.Skill(Type.Player(i).Skill(SkillBuffer).Num).CastTime > 0 Then
                        ' lock to player
                        tmpX = GetPlayerX(i) * PicX + Type.Player(i).XOffset + 16 - (Width / 2)
                        tmpY = GetPlayerY(i) * PicY + Type.Player(i).YOffset + 35 + Height

                        ' calculate the width to fill
                        If Width > 0 Then barWidth = (GetTickCount - SkillBufferTimer) / ((Type.Skill(Type.Player(i).Skill(SkillBuffer).Num).CastTime * 1000)) * Width

                        ' draw bar background
                        Top = Height * 3 ' cooldown bar background
                        Left = 0
                        RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, Width, Height, Width, Height)

                        ' draw the bar proper
                        Top = Height * 2 ' cooldown bar
                        Left = 0
                        RenderTexture(BarTexture, ConvertMapX(tmpX), ConvertMapY(tmpY), Left, Top, barWidth, Height, barWidth, Height)
                    End If
                End If
            End If
        Next
    End Sub

    Friend Sub DrawEyeDropper()
        SpriteBatch.Begin()

        ' Define rectangle parameters.
        Dim position As New Vector2(ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY))
        Dim size As New Vector2(PicX, PicX)
        Dim fillColor As Color = Color.Transparent  ' No fill
        Dim outlineColor As Color = Color.Cyan      ' Cyan outline
        Dim outlineThickness As Integer = 1         ' Thickness of outline

        ' Draw the rectangle with an outline.
        DrawRectangle(position, size, fillColor, outlineColor, outlineThickness)
        SpriteBatch.End()
    End Sub

    Friend Sub DrawGrid()
        ' Use a single Begin/End pair to improve performance
        SpriteBatch.Begin()

        ' Iterate over the tiles in the visible range
        For x = TileView.Left - 1 To TileView.Right
            For y = TileView.Top - 1 To TileView.Bottom
                If IsValidMapPoint(x, y) Then
                    ' Calculate the tile position and size
                    Dim posX As Integer = ConvertMapX((x - 1) * PicX)
                    Dim posY As Integer = ConvertMapY((y - 1) * PicY)
                    Dim rectWidth As Integer = PicX
                    Dim rectHeight As Integer = PicY

                    ' Draw the transparent rectangle as the tile background
                    SpriteBatch.Draw(TransparentTexture, New Rectangle(posX, posY, rectWidth, rectHeight), Color.Transparent)

                    ' Define the outline color and thickness
                    Dim outlineColor As Color = Color.White
                    Dim thickness As Integer = 1

                    ' Draw the tile outline (top, bottom, left, right)
                    SpriteBatch.Draw(TransparentTexture, New Rectangle(posX, posY, rectWidth, thickness), outlineColor) ' Top
                    SpriteBatch.Draw(TransparentTexture, New Rectangle(posX, posY + rectHeight - thickness, rectWidth, thickness), outlineColor) ' Bottom
                    SpriteBatch.Draw(TransparentTexture, New Rectangle(posX, posY, thickness, rectHeight), outlineColor) ' Left
                    SpriteBatch.Draw(TransparentTexture, New Rectangle(posX + rectWidth - thickness, posY, thickness, rectHeight), outlineColor) ' Right
                End If
            Next
        Next

        SpriteBatch.End()
    End Sub

    Friend Sub DrawTileOutline()
        ' Begin the sprite batch for drawing
        SpriteBatch.Begin()

        ' Example rectangle (replace with your actual logic)
        Dim rect As New Rectangle(100, 100, 200, 100)
        Dim fillColor As Color = Color.Transparent
        Dim outlineColor As Color = Color.Blue
        Dim outlineThickness As Integer = 2

        ' Draw the outlined rectangle
        DrawRectangleWithOutline(rect, fillColor, outlineColor, outlineThickness)

        ' Render the tileset texture if in the correct editor tab
        If frmEditor_Map.tabpages.SelectedTab Is frmEditor_Map.tpAttributes Then
            ' No specific rendering here; only setting size for attributes
        Else
            Dim selectedTileTexture = TilesetTexture(frmEditor_Map.cmbTileSets.SelectedIndex + 1)
            Dim rec2 As New Rectangle()

            If EditorTileWidth = 1 AndAlso EditorTileHeight = 1 Then
                RenderTexture(selectedTileTexture,
                              ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY),
                              EditorTileSelStart.X * PicX, EditorTileSelStart.Y * PicY,
                              PicX, PicY)
            ElseIf frmEditor_Map.cmbAutoTile.SelectedIndex > 0 Then
                RenderTexture(selectedTileTexture,
                              ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY),
                              EditorTileSelStart.X * PicX, EditorTileSelStart.Y * PicY,
                              EditorTileSelEnd.X * PicX, EditorTileSelEnd.Y * PicY)
            End If

            ' Draw a filled rectangle for the tile selection
            Dim position As New Vector2(ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY))
            SpriteBatch.Draw(PixelTexture, New Rectangle(CInt(position.X), CInt(position.Y), CInt(rec2.Width), CInt(rec2.Height)), Color.White)
        End If

        ' End the sprite batch
        SpriteBatch.End()
    End Sub

    Friend Sub DrawTarget(x2 As Integer, y2 As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer
        Dim width As Integer, height As Integer

        With rec
            .Y = 0
            .Height = TargetGfxInfo.Height
            .X = 0
            .Width = TargetGfxInfo.Width / 2
        End With
        x = ConvertMapX(x2 + 4)
        y = ConvertMapY(y2 - 32)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        RenderTexture(TargetTexture, x, y, rec.X, rec.Y, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Friend Sub DrawHover(x2 As Integer, y2 As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer
        Dim width As Integer, height As Integer

        With rec
            .Y = 0
            .Height = TargetGfxInfo.Height
            .X = TargetGfxInfo.Width / 2
            .Width = TargetGfxInfo.Width / 2 + TargetGfxInfo.Width / 2
        End With

        x = ConvertMapX(x2 + 4)
        y = ConvertMapY(y2 - 32)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        RenderTexture(TargetTexture, x, y, rec.X, rec.Y, rec.Width, rec.Height, rec.Width, rec.Height)
    End Sub

    Friend Sub EditorItem_DrawIcon()
        Dim itemnum As Integer
        itemnum = frmEditor_Item.nudIcon.Value

        If itemnum < 1 Or itemnum > NumItems Then
            frmEditor_Item.picItem.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Core.Path.Graphics & "items\" & itemnum & GfxExt) Then
            frmEditor_Item.picItem.BackgroundImage = Drawing.Image.FromFile(Core.Path.Graphics & "items\" & itemnum & GfxExt)
        Else
            frmEditor_Item.picItem.BackgroundImage = Nothing
        End If
    End Sub

    Friend Sub EditorItem_DrawPaperdoll()
        Dim Sprite As Integer

        Sprite = frmEditor_Item.nudPaperdoll.Value

        If Sprite < 1 Or Sprite > NumPaperdolls Then
            frmEditor_Item.picPaperdoll.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Core.Path.Graphics & "paperdolls\" & Sprite & GfxExt) Then
            frmEditor_Item.picPaperdoll.BackgroundImage =
                Drawing.Image.FromFile(Core.Path.Graphics & "paperdolls\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorNPC_DrawSprite()
        Dim Sprite As Integer

        Sprite = frmEditor_NPC.nudSprite.Value

        If Sprite < 1 Or Sprite > NumCharacters Then
            frmEditor_NPC.picSprite.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Core.Path.Graphics & "characters\" & Sprite & GfxExt) Then
            frmEditor_NPC.picSprite.Width =
                Drawing.Image.FromFile(Core.Path.Graphics & "characters\" & Sprite & GfxExt).Width / 4
            frmEditor_NPC.picSprite.Height =
                Drawing.Image.FromFile(Core.Path.Graphics & "characters\" & Sprite & GfxExt).Height / 4
            frmEditor_NPC.picSprite.BackgroundImage =
                Drawing.Image.FromFile(Core.Path.Graphics & "characters\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorResource_DrawSprite()
        Dim Sprite As Integer

        ' normal sprite
        Sprite = frmEditor_Resource.nudNormalPic.Value

        If Sprite < 1 Or Sprite > NumResources Then
            frmEditor_Resource.picNormalpic.BackgroundImage = Nothing
        Else
            If File.Exists(Core.Path.Graphics & "resources\" & Sprite & GfxExt) Then
                frmEditor_Resource.picNormalpic.BackgroundImage =
                    Drawing.Image.FromFile(Core.Path.Graphics & "resources\" & Sprite & GfxExt)
            End If
        End If

        ' exhausted sprite
        Sprite = frmEditor_Resource.nudExhaustedPic.Value

        If Sprite < 1 Or Sprite > NumResources Then
            frmEditor_Resource.picExhaustedPic.BackgroundImage = Nothing
        Else
            If File.Exists(Core.Path.Graphics & "resources\" & Sprite & GfxExt) Then
                frmEditor_Resource.picExhaustedPic.BackgroundImage =
                    Drawing.Image.FromFile(Core.Path.Graphics & "resources\" & Sprite & GfxExt)
            End If
        End If
    End Sub

    Friend Sub EditorEvent_DrawPicture()
        Dim Sprite As Integer

        Sprite = frmEditor_Event.nudShowPicture.Value

        If Sprite < 1 Or Sprite > NumPictures Then
            frmEditor_Event.picShowPic.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Core.Path.Graphics & "pictures\" & Sprite & GfxExt) Then
            frmEditor_Event.picShowPic.Width =
                Drawing.Image.FromFile(Core.Path.Graphics & "pictures\" & Sprite & GfxExt).Width
            frmEditor_Event.picShowPic.Height =
                Drawing.Image.FromFile(Core.Path.Graphics & "pictures\" & Sprite & GfxExt).Height
            frmEditor_Event.picShowPic.BackgroundImage =
                Drawing.Image.FromFile(Core.Path.Graphics & "pictures\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorSkill_DrawIcon()
        Dim skillNum As Integer
        skillNum = frmEditor_Skill.nudIcon.Value

        If skillNum < 1 Or skillNum > NumItems Then
            frmEditor_Skill.picSprite.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Core.Path.Graphics & "Skills\" & skillNum & GfxExt) Then
            frmEditor_Skill.picSprite.BackgroundImage = Drawing.Image.FromFile(Core.Path.Graphics & "Skills\" & skillNum & GfxExt)
        Else
            frmEditor_Skill.picSprite.BackgroundImage = Nothing
        End If
    End Sub

    Friend Sub EditorAnim_DrawSprite()
        With frmEditor_Animation
            ProcessAnimation(.nudSprite0, .nudFrameCount0, .nudLoopTime0, 0, EditorAnimation_Anim1, .picSprite0)
            ProcessAnimation(.nudSprite1, .nudFrameCount1, .nudLoopTime1, 1, EditorAnimation_Anim2, .picSprite1)
        End With
    End Sub

    Public Sub ProcessAnimation(animationControl As NumericUpDown,
                            frameCountControl As NumericUpDown,
                            loopCountControl As NumericUpDown,
                            animationTimerIndex As Integer,
                            animationDisplay As RenderTarget2D,
                            backgroundColorControl As PictureBox)

        ' Get the animation number and validate its range
        Dim animationNum As Integer = animationControl.Value
        If animationNum <= 0 OrElse animationNum > NumAnimations Then Exit Sub

        ' Retrieve animation texture dimensions
        Dim totalWidth As Integer = AnimationGfxInfo(animationNum).Width
        Dim totalHeight As Integer = AnimationGfxInfo(animationNum).Height

        ' Get the number of columns from the control
        Dim columns As Integer = frameCountControl.Value
        If columns <= 0 Then Exit Sub ' Avoid division by zero

        ' Calculate frame dimensions (assuming square frames)
        Dim frameWidth As Integer = totalWidth \ columns
        Dim frameHeight As Integer = frameWidth ' Adjust if frames are not square

        ' Calculate the number of rows and total frames
        Dim rows As Integer = If(frameHeight > 0, totalHeight \ frameHeight, 1)
        Dim frameCount As Integer = rows * columns

        ' Get loop time from the control
        Dim looptime As Integer = loopCountControl.Value

        ' Check if it's time to update the frame
        If AnimEditorTimer(animationTimerIndex) + looptime <= GetTickCount() Then
            If AnimEditorFrame(animationTimerIndex) >= frameCount Then
                AnimEditorFrame(animationTimerIndex) = 1 ' Loop back to the first frame
            Else
                AnimEditorFrame(animationTimerIndex) += 1
            End If
            AnimEditorTimer(animationTimerIndex) = GetTickCount()
        End If

        ' Render the current frame to the RenderTarget2D
        GraphicsDevice.SetRenderTarget(animationDisplay)
        GraphicsDevice.Clear(Color.Black) ' Clear with background color

        ' Begin SpriteBatch for rendering
        SpriteBatch.Begin()

        ' Calculate the current frame index and its position in the texture
        Dim frameIndex As Integer = AnimEditorFrame(animationTimerIndex) - 1
        Dim column As Integer = frameIndex Mod columns
        Dim row As Integer = frameIndex \ columns
        Dim sourceRect As New Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight)

        ' Render the texture to the target
        SpriteBatch.Draw(AnimationTexture(animationNum), 
                         New Rectangle(0, 0, frameWidth, frameHeight), 
                         sourceRect, 
                         Color.White)

        SpriteBatch.End()

        ' Reset to the default back buffer
        GraphicsDevice.SetRenderTarget(Nothing)
    End Sub

    Public Sub DrawChatBubble(ByVal Index As Long)
        Dim theArray() As String, x As Long, y As Long, i As Long, MaxWidth As Long, x2 As Long, y2 As Long, Color As Integer, tmpNum As Long

        With ChatBubble(Index)
            ' exit out early
            If .Target = 0 Then Exit Sub

            Color = .Color

            ' calculate position
            Select Case .TargetType
                Case TargetType.Player
                    ' it's a player
                    If Not GetPlayerMap(.Target) = GetPlayerMap(MyIndex) Then Exit Sub

                    ' it's on our map - get co-ords
                    x = ConvertMapX((Type.Player(.Target).X * 32) + Type.Player(.Target).XOffset) + 16
                    y = ConvertMapY((Type.Player(.Target).Y * 32) + Type.Player(.Target).YOffset) - 32
                Case TargetType.Event
                    x = ConvertMapX(MyMap.Event(.Target).X * 32) + 16
                    y = ConvertMapY(MyMap.Event(.Target).Y * 32) - 16
                Case Else
                    Exit Sub
            End Select

            ' word wrap
            WordWrap(.Msg, ChatBubbleWidth, theArray)

            ' find max width
            tmpNum = UBound(theArray)

            For i = 1 To tmpNum
                If TextWidth(theArray(i), 15) > MaxWidth Then MaxWidth = TextWidth(theArray(i), 15)
            Next

            ' calculate the new position
            x2 = x - (MaxWidth \ 2)
            y2 = y - (UBound(theArray) * 12)

            ' render bubble - top left
            RenderTexture(InterfaceTexture(33), x2 - 9, y2 - 5, 0, 0, 9, 5, 9, 5)

            ' top right
            RenderTexture(InterfaceTexture(33), x2 + MaxWidth, y2 - 5, 119, 0, 9, 5, 9, 5)

            ' top
            RenderTexture(InterfaceTexture(33), x2, y2 - 5, 9, 0, MaxWidth, 5, 5, 5)

            ' bottom left
            RenderTexture(InterfaceTexture(33), x2 - 9, y, 0, 19, 9, 6, 9, 6)

            ' bottom right
            RenderTexture(InterfaceTexture(33), x2 + MaxWidth, y, 119, 19, 9, 6, 9, 6)

            ' bottom - left half
            RenderTexture(InterfaceTexture(33), x2, y, 9, 19, (MaxWidth \ 2) - 5, 6, 6, 6)

            ' bottom - right half
            RenderTexture(InterfaceTexture(33), x2 + (MaxWidth \ 2) + 6, y, 9, 19, (MaxWidth \ 2) - 5, 6, 9, 6)

            ' left
            RenderTexture(InterfaceTexture(33), x2 - 9, y2, 0, 6, 9, (UBound(theArray) * 12), 9, 6)

            ' right
            RenderTexture(InterfaceTexture(33), x2 + MaxWidth, y2, 119, 6, 9, (UBound(theArray) * 12), 9, 6)

            ' center
            RenderTexture(InterfaceTexture(33), x2, y2, 9, 5, MaxWidth, (UBound(theArray) * 12), 9, 5)

            ' little pointy bit
            RenderTexture(InterfaceTexture(33), x - 5, y, 58, 19, 11, 11, 11, 11)

            ' render each line centralized
            tmpNum = UBound(theArray)

            For i = 1 To tmpNum
                RenderText(theArray(i), x - (theArray(i).Length / 2) - (TextWidth(theArray(i)) / 2), y2, QbColorToXnaColor(.Color), Microsoft.Xna.Framework.Color.Black)
                y2 = y2 + 12
            Next

            ' check if it's timed out - close it if so
            If .Timer + 5000 < GetTickCount Then
                .Active = False
            End If
        End With
    End Sub

    Friend Sub DrawPlayer(index As Integer)
        Dim anim As Byte, x As Integer, y As Integer
        Dim spritenum As Integer, spriteleft As Integer
        Dim attackspeed As Integer
        Dim rect As Rectangle

        spritenum = GetPlayerSprite(index)

        If index < 1 Or index > MAX_PLAYERS Then Exit Sub
        If spritenum <= 0 Or spritenum > NumCharacters Then Exit Sub

        ' speed from weapon
        If GetPlayerEquipment(index, EquipmentType.Weapon) > 0 Then
            attackspeed = Type.Item(GetPlayerEquipment(index, EquipmentType.Weapon)).Speed
        Else
            attackspeed = 1000
        End If

        ' Reset frame
        anim = 0

        ' Check for attacking animation
        If Type.Player(index).AttackTimer + (attackspeed / 2) > GetTickCount() Then
            If Type.Player(index).Attacking = 1 Then
                anim = 3
            End If
        Else
            ' If not attacking, walk normally
            Select Case GetPlayerDir(index)
                Case DirectionType.Up

                    If (Type.Player(index).YOffset > 8) Then anim = Type.Player(index).Steps
                Case DirectionType.Down

                    If (Type.Player(index).YOffset < -8) Then anim = Type.Player(index).Steps
                Case DirectionType.Left

                    If (Type.Player(index).XOffset > 8) Then anim = Type.Player(index).Steps
                Case DirectionType.Right

                    If (Type.Player(index).XOffset < -8) Then anim = Type.Player(index).Steps
                Case DirectionType.UpRight
                    If (Type.Player(index).XOffset < -8) Then anim = Type.Player(index).Steps
                    If (Type.Player(index).YOffset > 8) Then anim = Type.Player(index).Steps

                Case DirectionType.UpLeft
                    If (Type.Player(index).XOffset > 8) Then anim = Type.Player(index).Steps
                    If (Type.Player(index).YOffset > 8) Then anim = Type.Player(index).Steps

                Case DirectionType.DownRight
                    If (Type.Player(index).XOffset < -8) Then anim = Type.Player(index).Steps
                    If (Type.Player(index).YOffset < -8) Then anim = Type.Player(index).Steps

                Case DirectionType.DownLeft
                    If (Type.Player(index).XOffset > 8) Then anim = Type.Player(index).Steps
                    If (Type.Player(index).YOffset < -8) Then anim = Type.Player(index).Steps

            End Select

        End If

        ' Check to see if we want to stop making him attack
        With Type.Player(index)
            If .AttackTimer + attackspeed < GetTickCount() Then
                .Attacking = 0
                .AttackTimer = 0
            End If

        End With

        ' Set the left
        Select Case GetPlayerDir(index)
            Case DirectionType.Up
                spriteleft = 3
            Case DirectionType.Right
                spriteleft = 2
            Case DirectionType.Down
                spriteleft = 0
            Case DirectionType.Left
                spriteleft = 1
            Case DirectionType.UpRight
                spriteleft = 2
            Case DirectionType.UpLeft
                spriteleft = 1
            Case DirectionType.DownLeft
                spriteleft = 1
            Case DirectionType.DownRight
                spriteleft = 2
        End Select

        ' Calculate the X
        x = Type.Player(index).X * PicX + Type.Player(index).XOffset - ((CharacterGfxInfo(spritenum).Width / 4 - 32) / 2)

        ' Is the player's height more than 32..?
        If (CharacterGfxInfo(spritenum).Height) > 32 Then
            ' Create a 32 pixel offset for larger sprites
            y = GetPlayerY(index) * PicY + Type.Player(index).YOffset - ((CharacterGfxInfo(spritenum).Height / 4) - 32)
        Else
            ' Proceed as normal
            y = GetPlayerY(index) * PicY + Type.Player(index).YOffset
        End If

        rect = New Rectangle((anim) * (CharacterGfxInfo(spritenum).Width / 4), spriteleft * (CharacterGfxInfo(spritenum).Height / 4),
                               (CharacterGfxInfo(spritenum).Width / 4), (CharacterGfxInfo(spritenum).Height / 4))

        ' render the actual sprite
        DrawShadow(x, y + 16)
        DrawCharacterSprite(spritenum, x, y, rect)

        'check for paperdolling
        For i = 1 To EquipmentType.Count - 1
            If GetPlayerEquipment(index, i) > 0 Then
                If Type.Item(GetPlayerEquipment(index, i)).Paperdoll > 0 Then
                    DrawPaperdoll(x, y, Type.Item(GetPlayerEquipment(index, i)).Paperdoll, anim, spriteleft)
                End If
            End If
        Next

        ' Check to see if we want to stop showing emote
        With Type.Player(index)
            If .EmoteTimer < GetTickCount() Then
                .Emote = 0
                .EmoteTimer = 0
            End If
        End With

        'check for emotes
        If Type.Player(MyIndex).Emote > 0 Then
            DrawEmote(x, y, Type.Player(MyIndex).Emote)
        End If
    End Sub

    Public Sub EditorEvent_DrawGraphic()
        If Not frmEditor_Event.picGraphicSel.Visible Then Exit Sub

        Select Case frmEditor_Event.cmbGraphic.SelectedIndex
            Case 0 ' None
                frmEditor_Event.picGraphicSel.BackgroundImage = Nothing

            Case 1 ' Character Graphic
                If frmEditor_Event.nudGraphic.Value > 0 And frmEditor_Event.nudGraphic.Value <= NumCharacters Then
                    RenderToPictureBox(frmEditor_Event.picGraphic, GetTexture(IO.Path.Combine(Core.Path.Characters, frmEditor_Event.nudGraphic.Value & GfxExt)))
                Else
                    frmEditor_Event.picGraphic.BackgroundImage = Nothing
                End If

            Case 2 ' Tileset Graphic
                If frmEditor_Event.nudGraphic.Value > 0 And frmEditor_Event.nudGraphic.Value <= NumTileSets Then
                    Dim texture As Texture2D = GetTexture(IO.Path.Combine(Core.Path.Tilesets, frmEditor_Event.nudGraphic.Value & GfxExt))
                    RenderToPictureBox(frmEditor_Event.picGraphic, texture)
                    RenderToPictureBox(frmEditor_Event.picGraphicSel, texture)
                Else
                    frmEditor_Event.picGraphicSel.BackgroundImage = Nothing
                End If
        End Select
    End Sub

    Public Sub RenderToPictureBox(pictureBox As PictureBox, texture As Texture2D)
        ' Create a new RenderTarget2D matching the PictureBox dimensions
        Dim renderTarget As New RenderTarget2D(GraphicsDevice, pictureBox.Width, pictureBox.Height)

        ' Set the render target and clear it
        GraphicsDevice.SetRenderTarget(renderTarget)
        GraphicsDevice.Clear(Color.CornflowerBlue)

        ' Begin SpriteBatch and render the texture
        SpriteBatch.Begin()
        SpriteBatch.Draw(texture, New Rectangle(0, 0, pictureBox.Width, pictureBox.Height), Color.White)
        SpriteBatch.End()

        ' Reset to the back buffer
        GraphicsDevice.SetRenderTarget(Nothing)

        ' Save RenderTarget2D to a Bitmap
        Dim bitmap As Drawing.Bitmap = RenderTargetToBitmap(renderTarget)

        ' Display the bitmap in the PictureBox
        pictureBox.Image = bitmap

        ' Dispose of resources
        renderTarget.Dispose()
    End Sub

    ' Convert RenderTarget2D to Bitmap
    Private Function RenderTargetToBitmap(renderTarget As RenderTarget2D) As Drawing.Bitmap
        ' Get the pixel data from RenderTarget2D
        Dim data(renderTarget.Width * renderTarget.Height - 1) As Color
        renderTarget.GetData(data)

        ' Create a new Bitmap
        Dim bitmap As New Drawing.Bitmap(renderTarget.Width, renderTarget.Height, Imaging.PixelFormat.Format32bppArgb)

        ' Copy the pixel data to the Bitmap
        For y As Integer = 0 To renderTarget.Height - 1
            For x As Integer = 0 To renderTarget.Width - 1
                Dim color As Color = data(y * renderTarget.Width + x)
                bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B))
            Next
        Next

        Return bitmap
    End Function

    Friend Sub DrawEvents()
        If MyMap.EventCount <= 0 Then Exit Sub ' Exit early if no events

        For i As Integer = 1 To MyMap.EventCount
            Dim x = ConvertMapX(MyMap.Event(i).X * PicX)
            Dim y = ConvertMapY(MyMap.Event(i).Y * PicY)

            ' Skip event if there are no pages
            If MyMap.Event(i).PageCount <= 0 Then
                DrawOutlineRectangle(x, y, PicX, PicY, Color.Blue, 0.6F)
                Continue For
            End If

            ' Render event based on its graphic type
            Select Case MyMap.Event(i).Pages(1).GraphicType
                Case 0 ' Text Event
                    Dim tX = x + (PicX \ 2) - 4
                    Dim tY = y + (PicY \ 2) - 7
                    RenderText("E", tX, tY, Color.Green, Color.Black)

                Case 1 ' Character Graphic
                    RenderCharacterGraphic(MyMap.Event(i), x, y)

                Case 2 ' Tileset Graphic
                    RenderTilesetGraphic(MyMap.Event(i), x, y)

                Case Else
                    ' Draw fallback outline rectangle if graphic type is unknown
                    DrawOutlineRectangle(x, y, PicX, PicY, Color.Blue, 0.6F)
            End Select
        Next
    End Sub

    Public Sub RenderCharacterGraphic(eventData As EventStruct, x As Integer, y As Integer)
        ' Get the graphic index from the event's first page
        Dim gfxIndex As Integer = eventData.Pages(1).Graphic

        ' Validate the graphic index to ensure it’s within range
        If gfxIndex <= 0 OrElse gfxIndex > NumCharacters Then Exit Sub 

        ' Get animation details (frame index and columns) from the event
        Dim frameIndex As Integer = eventData.Pages(1).GraphicX ' Example frame index
        Dim columns As Integer = eventData.Pages(1).GraphicY ' Example column count

        ' Calculate the frame size (assuming square frames for simplicity)
        Dim frameWidth As Integer = CharacterTexture(gfxIndex).Width \ columns
        Dim frameHeight As Integer = frameWidth ' Adjust if non-square frames

        ' Calculate the source rectangle for the current frame
        Dim column As Integer = frameIndex Mod columns
        Dim row As Integer = frameIndex \ columns
        Dim sourceRect As New Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight)

        ' Define the position on the map where the graphic will be drawn
        Dim position As New Vector2(x, y)

        ' Render the graphic using SpriteBatch
        SpriteBatch.Begin()

        SpriteBatch.Draw(CharacterTexture(gfxIndex), 
                         position, 
                         sourceRect, 
                         Color.White)

        SpriteBatch.End()
    End Sub

    Private Sub RenderTilesetGraphic(eventData As EventStruct, x As Integer, y As Integer)
        Dim gfxIndex = eventData.Pages(1).Graphic

        If gfxIndex > 0 AndAlso gfxIndex <= NumTileSets Then
            ' Define source rectangle from tileset graphics
            Dim srcRect As New Rectangle(
                eventData.Pages(1).GraphicX * 32,
                eventData.Pages(1).GraphicY * 32,
                eventData.Pages(1).GraphicX2 * 32,
                eventData.Pages(1).GraphicY2 * 32
            )

            ' Adjust position if the tile is larger than 32x32
            If srcRect.Height > 32 Then y -= PicY

            ' Define destination rectangle
            Dim destRect As New Rectangle(x, y, srcRect.Width, srcRect.Height)

            RenderTexture(TilesetTexture(gfxIndex), destRect.X, destRect.Y, srcRect.X, srcRect.Y, destRect.Width, destRect.Height, srcRect.Width, srcRect.Height)
        Else
            ' Draw fallback outline if the tileset graphic is invalid
            DrawOutlineRectangle(x, y, PicX, PicY, Color.Blue, 0.6F)
        End If
    End Sub

    Friend Sub DrawEvent(id As Integer) ' draw on map, outside the editor
        Dim x As Integer, y As Integer, width As Integer, height As Integer, sRect As Rectangle, anim As Integer, spritetop As Integer

       If MapEvents(id).Visible = 0 Then Exit Sub

        Select Case MapEvents(id).GraphicType
            Case 0
                Exit Sub
            Case 1
               If MapEvents(id).Graphic <= 0 Or MapEvents(id).Graphic > NumCharacters Then Exit Sub

                ' Reset frame
               If MapEvents(id).Steps = 3 Then
                    anim = 0
                ElseIf MapEvents(id).Steps = 1 Then
                    anim = 2
                End If

                Select Case MapEvents(id).Dir
                    Case DirectionType.Up
                        If (MapEvents(id).YOffset > 8) Then anim = MapEvents(id).Steps
                    Case DirectionType.Down
                        If (MapEvents(id).YOffset < -8) Then anim = MapEvents(id).Steps
                    Case DirectionType.Left
                        If (MapEvents(id).XOffset > 8) Then anim = MapEvents(id).Steps
                    Case DirectionType.Right
                        If (MapEvents(id).XOffset < -8) Then anim = MapEvents(id).Steps
                End Select

                ' Set the left
                Select Case MapEvents(id).ShowDir
                    Case DirectionType.Up
                        spritetop = 3
                    Case DirectionType.Right
                        spritetop = 2
                    Case DirectionType.Down
                        spritetop = 0
                    Case DirectionType.Left
                        spritetop = 1
                End Select

               If MapEvents(id).WalkAnim = 1 Then anim = 0
               If MapEvents(id).Moving = 0 Then anim = MapEvents(id).GraphicX

                width = CharacterGfxInfo(MapEvents(id).Graphic).Width / 4
                height = CharacterGfxInfo(MapEvents(id).Graphic).Height / 4

                sRect = New Rectangle((anim) * (CharacterGfxInfo(MapEvents(id).Graphic).Width / 4), spritetop * (CharacterGfxInfo(MapEvents(id).Graphic).Height / 4), (CharacterGfxInfo(MapEvents(id).Graphic).Width / 4), (CharacterGfxInfo(MapEvents(id).Graphic).Height / 4))
                ' Calculate the X
                x = MapEvents(id).X * PicX + MapEvents(id).XOffset - ((CharacterGfxInfo(MapEvents(id).Graphic).Width / 4 - 32) / 2)

                ' Is the player's height more than 32..?
                If (CharacterGfxInfo(MapEvents(id).Graphic).Height * 4) > 32 Then
                    ' Create a 32 pixel offset for larger sprites
                    y = MapEvents(id).Y * PicY + MapEvents(id).YOffset - ((CharacterGfxInfo(MapEvents(id).Graphic).Height / 4) - 32)
                Else
                    ' Proceed as normal
                    y = MapEvents(id).Y * PicY + MapEvents(id).YOffset
                End If
                ' render the actual sprite
                DrawCharacterSprite(MapEvents(id).Graphic, x, y, sRect)
            Case 2
               If MapEvents(id).Graphic < 1 Or MapEvents(id).Graphic > NumTileSets Then Exit Sub
               If MapEvents(id).GraphicY2 > 0 Or MapEvents(id).GraphicX2 > 0 Then
                    With sRect
                        .X = MapEvents(id).GraphicX * 32
                        .Y = MapEvents(id).GraphicY * 32
                        .Width = MapEvents(id).GraphicX2 * 32
                        .Height = MapEvents(id).GraphicY2 * 32
                    End With
                Else
                    With sRect
                        .X = MapEvents(id).GraphicY * 32
                        .Height = .Top + 32
                        .Y = MapEvents(id).GraphicX * 32
                        .Width = .Left + 32
                    End With
                End If
                x = MapEvents(id).X * 32
                y = MapEvents(id).Y * 32
                x = x - ((sRect.Right - sRect.Left) / 2)
                y = y - (sRect.Bottom - sRect.Top) + 32

               If MapEvents(id).GraphicY2 > 1 Then
                    RenderTexture(TilesetTexture(MapEvents(id).Graphic), ConvertMapX(MapEvents(id).X * PicX), ConvertMapY(MapEvents(id).Y * PicY) - PicY, sRect.Left, sRect.Top, sRect.Width, sRect.Height)
                Else
                    RenderTexture(TilesetTexture(MapEvents(id).Graphic), ConvertMapX(MapEvents(id).X * PicX), ConvertMapY(MapEvents(id).Y * PicY), sRect.Left, sRect.Top, sRect.Width, sRect.Height)
                End If
        End Select

    End Sub

End Class
