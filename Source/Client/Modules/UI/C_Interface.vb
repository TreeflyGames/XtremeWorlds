
Imports Core
Imports Core.Enumerator
Imports Core.Types
Imports SFML.Graphics

Module C_Interface
    ' actual GUI
    Public Windows() As Types.WindowStruct
    Public WindowCount As Long
    Public activeWindow As Long

    ' GUI parts
    Public DragBox As EntityPartStruct

    ' Used for automatically the zOrder
    Private zOrder_Win As Long
    Private zOrder_Con As Long

    Public Sub CreateEntity(winNum As Long, zOrder As Long, name As String, tType As EntityType, ByRef design() As Long, ByRef image() As Long, ByRef callback() as Task, _
       Optional left As Long = 0, Optional top As Long = 0, Optional width As Long = 0, Optional height As Long = 0, Optional visible As Boolean = True, Optional canDrag As Boolean = True, Optional Max As Long = 0, Optional Min As Long = 0, Optional value As Long = 0, Optional text As String = "",
       Optional align As Byte = 0, Optional font As String = "Georgia.ttf", Optional alpha As Long = 255, Optional clickThrough As Boolean = False, Optional xOffset As Long = 0, Optional yOffset As Long = 0, Optional zChange As Byte = 0,
       Optional onDraw As Task = Nothing, Optional isActive As Boolean = True, Optional tooltip As String = "", Optional group As Long = 0)

        Dim i As Long

        ' check if it's a legal number
        If winNum <= 0 Or winNum > WindowCount Then
            Exit Sub
        End If

        ' re-dim the control array
        With Windows(winNum)
            .ControlCount = .ControlCount + 1
            ReDim Preserve .Controls(.ControlCount)
        End With

        ' Set the new control values
        With Windows(winNum).Controls(Windows(winNum).ControlCount)
            .Name = name
            .Type = tType

            ReDim .Design(EntState.State_Count - 1)
            ReDim .Image(EntState.State_Count - 1)
            Redim .callback(EntState.State_Count - 1)

            ' loop through states
            For i = 0 To EntState.State_Count - 1
                .Design(i) = design(i)
                .Image(i) = image(i)
                .callback(i) = callback(i)
            Next

            .Left = left
            .Top = top
            .OrigLeft = left
            .OrigTop = top
            .Width = width
            .Height = height
            .Visible = visible
            .CanDrag = canDrag
            .Max = Max
            .Min = Min
            .Value = value
            .Text = text
            .Align = align
            .Font = font
            .Alpha = alpha
            .ClickThrough = clickThrough
            .xOffset = xOffset
            .yOffset = yOffset
            .zChange = zChange
            .zOrder = zOrder
            .Enabled = True
            .OnDraw = onDraw
            .Tooltip = tooltip
            .Group = group
            ReDim .List(0)
        End With

        ' set the active control
        If isActive Then Windows(winNum).ActiveControl = Windows(winNum).ControlCount

        ' set the zOrder
        zOrder_Con = zOrder_Con + 1
    End Sub

    Public Sub UpdateZOrder(winNum As Long, Optional forced As Boolean = False)
        Dim i As Long
        Dim oldZOrder As Long

        With Windows(winNum).Window

            If Not forced Then If .zChange = 0 Then Exit Sub
            If .zOrder = WindowCount Then Exit Sub
            oldZOrder = .zOrder

            For i = 1 To WindowCount

                If Windows(i).Window.zOrder > oldZOrder Then
                    Windows(i).Window.zOrder = Windows(i).Window.zOrder - 1
                End If

            Next

            .zOrder = WindowCount
        End With

    End Sub

    Public Sub SortWindows()
        Dim tempWindow As WindowStruct
        Dim i As Long, x As Long

        x = 1

        While x <> 0
            x = 0

            For i = 1 To WindowCount - 1

                If Windows(i).Window.zOrder > Windows(i + 1).Window.zOrder Then
                    tempWindow = Windows(i)
                    Windows(i) = Windows(i + 1)
                    Windows(i + 1) = tempWindow
                    x = 1
                End If

            Next

        End While

    End Sub

    Public Sub RenderEntities()
        Dim i As Long, x As Long, curZOrder As Long

        ' don't render anything if we don't have any containers
        If WindowCount = 0 Then Exit Sub

        ' reset zOrder
        curZOrder = 1

        ' loop through windows
        Do While curZOrder <= WindowCount
            For i = 1 To WindowCount
                If curZOrder = Windows(i).Window.zOrder Then
                    ' increment
                    curZOrder = curZOrder + 1
                    ' make sure it's visible
                    If Windows(i).Window.Visible Then
                        ' render container
                        RenderWindow(i)

                        ' render controls
                        For x = 1 To Windows(i).ControlCount
                            If Windows(i).Controls(x).Visible Then
                                RenderEntity(i, x)
                            End If
                        Next
                    End If
                End If
            Next
        Loop
    End Sub

    Public Sub RenderEntity(winNum As Long, entNum As Long)
        Dim xO As Long, yO As Long, hor_centre As Long, ver_centre As Long, height As Long, width As Long, left As Long, sprite As Sprite, xOffset As Long
        Dim callBack As Long, textArray() As String, count As Long, yOffset As Long, i As Long, y As Long, x As Long

        ' check if the window exists
        If winNum <= 0 Or winNum > WindowCount Then
            Exit Sub
        End If

        ' check if the entity exists
        If entNum <= 0 Or entNum > Windows(winNum).ControlCount Then
            Exit Sub
        End If

        ' check the container's position
        xO = Windows(winNum).Window.Left
        yO = Windows(winNum).Window.Top

        With Windows(winNum).Controls(entNum)

            ' find the control type
            Select Case .Type
                ' picture box
                Case EntityType.entPictureBox
                    ' render specific designs
                    If .Design(.State) > 0 Then
                        RenderDesign(.Design(.State), .left + xO, .top + yO, .width, .height, .alpha)
                    End If

                    ' render image
                    If .Image(.State) > 0 Then
                        RenderTexture(InterfaceSprite(.Image(.State)), GameWindow, .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Alpha)
                    End If

                ' textbox
                Case EntityType.entTextBox
                    ' render specific designs
                    If .Design(.State) > 0 Then
                        RenderDesign(.Design(.State), .Left + xO, .Top + yO, .Width, .Height, .Alpha)
                    End If

                    ' render image
                    If .Image(.State) > 0 Then
                        RenderTexture(InterfaceSprite(.Image(.State)), GameWindow, .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Alpha)
                    End If

                    ' render text
                    RenderText(.Text, GameWindow, .Left + xO + .xOffset, .Top + yO + .yOffset, Color.White, Color.White)

                ' buttons
                Case EntityType.entButton
                    ' render specific designs
                    If .Design(.State) > 0 Then
                        If .Design(.State) > 0 Then
                            RenderDesign(.Design(.State), .Left + xO, .Top + yO, .Width, .Height)
                        End If
                    End If

                    ' render image
                    If .Image(.State) > 0 Then
                        If .Image(.State) > 0 Then
                            RenderTexture(InterfaceSprite(.Image(.State)), GameWindow, .Left + xO, .Top + yO, 0, 0, InterfaceGfxInfo(.Image(.State)).Width, InterfaceGfxInfo(.Image(.State)).Height, InterfaceGfxInfo(.Image(.State)).Width, InterfaceGfxInfo(.Image(.State)).Height)
                        End If
                    End If

                    ' render icon
                    RenderTexture(ItemsSprite(.Icon), GameWindow, .Left + xO + .xOffset, .Top + yO + .yOffset, 0, 0, ItemsGfxInfo(.Icon).Width, ItemsGfxInfo(.Icon).Height)
                    
                    ' for changing the text space
                    xOffset = width
                    
                    ' calculate the vertical centre
                    height = GetTextHeight(.Text)
                    If height > .Height Then
                        ver_centre = .Top + yO
                    Else
                        ver_centre = .Top + yO + ((.Height - height) \ 2)
                    End If
                    
                    ' calculate the horizontal centre
                    width = GetTextWidth(.Text)
                    If width > .Width Then
                        hor_centre = .Left + xO + xOffset
                    Else
                        hor_centre = .Left + xO + xOffset + ((.Width - width - xOffset) \ 2)
                    End If

                    RenderText(.Text, GameWindow, hor_centre, ver_centre, Color.White, Color.White)

                ' labels
                Case EntityType.entLabel
                    If Len(.Text) > 0 Then
                        Select Case .Align
                            Case AlignmentType.AlignLeft
                                ' check if need to word wrap
                                If GetTextWidth(.Text) > .Width Then
                                    ' wrap text
                                    WordWrap_Array(.Text, .Width, textArray)
                                    
                                    ' render text
                                    count = UBound(textArray)
                                    
                                    For i = 1 To count
                                        RenderText(textArray(i), GameWindow, .Left + xO, .Top + yO + yOffset, Color.White, Color.White)
                                        yOffset = yOffset + 14
                                    Next
                                Else
                                    ' just one line
                                    RenderText(.Text, GameWindow, .Left + xO, .Top + yO, Color.White, Color.White)
                                End If

                            Case AlignmentType.AlignRight
                                ' check if need to word wrap
                                If GetTextWidth(.Text) > .Width Then
                                    ' wrap text
                                    WordWrap_Array(.Text, .Width, textArray)

                                    ' render text
                                    count = UBound(textArray)
                                    
                                    For i = 1 To count
                                        left = .Left + .Width - GetTextWidth(textArray(i))
                                        RenderText(textArray(i), GameWindow, left + xO, .Top + yO + yOffset, Color.White, Color.White)
                                        yOffset = yOffset + 14
                                    Next
                                Else
                                    ' just one line
                                    left = .Left + .Width - GetTextWidth(.Text)
                                    RenderText(.Text, GameWindow, left + xO, .Top + yO, Color.White, Color.White)
                                End If
                            
                            Case AlignmentType.alignCentre
                                ' check if need to word wrap
                                If GetTextWidth(.Text) > .Width Then
                                    ' wrap text
                                    WordWrap_Array(.Text, .Width, textArray)

                                    ' render text
                                    count = UBound(textArray)

                                    For i = 1 To count
                                        left = .Left + (.Width \ 2) - (GetTextWidth(textArray(i)) \ 2) - 4
                                        RenderText(textArray(i), GameWindow, left + xO, .Top + yO + yOffset, Color.White, Color.White)
                                        yOffset = yOffset + 14
                                    Next
                                Else
                                    ' just one line
                                    left = .Left + (.Width \ 2) - (GetTextWidth(.Text) \ 2) - 4
                                    RenderText(.Text, GameWindow, left + xO, .Top + yO, Color.White, Color.White)
                                End If
                        End Select
                    End If

                ' checkboxes
                Case EntityType.entCheckbox
                    Select Case .Design(0)
                        Case DesignType.ChkNorm
                            ' empty?
                            If .Value = 0 Then sprite = InterfaceSprite(2) Else sprite = InterfaceSprite(3)
                            
                            ' render box
                            RenderTexture(sprite, GameWindow, .Left + xO, .Top + yO, 0, 0, 14, 14, 14, 14)
                            
                            ' find text position
                            Select Case .Align
                                Case AlignmentType.AlignLeft
                                    left = .Left + 18 + xO
                                Case AlignmentType.AlignRight
                                    left = .Left + 18 + (.Width - 18) - GetTextWidth(.Text) + xO
                                Case AlignmentType.AlignCentre
                                    left = .Left + 18 + ((.Width - 18) / 2) - (GetTextWidth(.Text) / 2) + xO
                            End Select
                            
                            ' render text
                            RenderText(.Text, GameWindow, left, .Top + yO, Color.White, Color.White)
                        
                        Case DesignType.ChkChat
                            If .Value = 0 Then .Alpha = 150 Else .Alpha = 255
                            
                            ' render box
                            RenderTexture(InterfaceSprite(51), GameWindow, .Left + xO, .Top + yO, 0, 0, 49, 23)
                            
                            ' render text
                            left = .Left + (49 / 2) - (GetTextWidth(.Text) / 2) + xO
                            
                            ' render text
                            RenderText(.Text, GameWindow, left, .Top + yO + 4, Color.White, Color.White)
                        
                        Case DesignType.ChkCustom_Buying
                            If .Value = 0 Then sprite = InterfaceSprite(58) Else sprite = InterfaceSprite(56)
                            RenderTexture(sprite, GameWindow, .Left + xO, .Top + yO, 0, 0, 49, 20, 49, 20)
                        
                        Case DesignType.ChkCustom_Selling
                            If .Value = 0 Then sprite = InterfaceSprite(59) Else sprite = InterfaceSprite(57)
                            RenderTexture(sprite, GameWindow, .Left + xO, .Top + yO, 0, 0, 49, 20, 49, 20)
                    End Select

                ' comboboxes
                Case EntityType.entCombobox
                    Select Case .Design(0)
                        Case DesignType.ComboNorm
                            ' draw the background
                            RenderDesign(DesignType.TextBlack, .Left + xO, .Top + yO, .Width, .Height)
                            
                            ' render the text
                            If .Value > 0 Then
                                If .Value <= UBound(.List) Then
                                    RenderText(.List(.Value), GameWindow, .Left + xO, .Top + yO, Color.White, Color.White)
                                End If
                            End If

                            ' draw the little arow
                            RenderTexture(InterfaceSprite(66), GameWindow, .Left + xO + .Width, .Top + yO, 0, 0, 5, 4, 5, 4)
                    End Select
            End Select

            If Not .OnDraw Is Nothing Then .OnDraw.Start

        End With

    End Sub

    Public Sub RenderWindow(winNum As Long)
        Dim width As Long, height As Long, x As Long, y As Long, i As Long, left As Long

        ' check if the window exists
        If winNum <= 0 Or winNum > WindowCount Then
            Exit Sub
        End If

        With Windows(winNum).Window
            Select Case .Design(0)
                Case DesignType.ComboMenuNorm
                    RenderTexture(InterfaceSprite(1), GameWindow, .Left, .Top, 0, 0, .Width, .Height, 157, 0, 0, 0)
                    
                    ' text
                    If UBound(.List) > 0 Then
                        y = .Top + 2
                        x = .Left

                        For i = 1 To UBound(.List)
                            ' render select
                            If i = .Value Or i = .Group Then
                                RenderTexture(InterfaceSprite(1), GameWindow, x, y - 1, 0, 0, .Width, 15, 255, 0, 0, 0)
                            End If
                            
                            ' render text
                            left = x + (.Width \ 2) - (GetTextWidth(.List(i)) \ 2)
                            
                            If i = .Value Or i = .Group Then                                                                                                                                                                                                                                
                                RenderText(.List(i), GameWindow, left, y, Color.White, Color.White)
                            Else
                                RenderText(.List(i), GameWindow, left, y, Color.White, Color.White)
                            End If
                            y = y + 16
                        Next
                    End If
                    Exit Sub
            End Select

            Select Case .Design(.State)

                Case DesignType.Win_Black
                    RenderTexture(InterfaceSprite(61), GameWindow, .Left, .Top, 0, 0, .Width, .Height, 190, 255, 255, 255)

                Case DesignType.Win_Norm
                    ' render window
                    RenderDesign(DesignType.Wood, .Left, .Top, .Width, .Height)
                    RenderDesign(DesignType.Green, .Left, .Top, .Width, 21)

                    ' render the icon
                    RenderTexture(ItemsSprite(.icon), GameWindow, .left + .xOffset, .top - (width - 18) + .yOffset, 0, 0, width, height, width, height)

                    ' render the caption
                    RenderText(Trim$(.Text), GameWindow, .Left + height + 4, .Top + 4, Color.White, Color.White)

                Case DesignType.Win_NoBar
                    ' render window
                    RenderDesign(DesignType.Wood, .Left, .Top, .Width, .Height)

                Case DesignType.Win_Empty
                    ' render window
                    RenderDesign(DesignType.Wood_Empty, .Left, .Top, .Width, .Height)
                    RenderDesign(DesignType.Green, .Left, .Top, .Width, 21)

                    ' render the icon
                    RenderTexture(ItemsSprite(.icon), GameWindow, .left + .xOffset, .top - (width - 18) + .yOffset, 0, 0, width, height, width, height)

                    ' render the caption
                    RenderText(Trim$(.Text), GameWindow, .Left + height + 4, .Top + 4, Color.White, Color.White)

                Case DesignType.Win_Desc
                    RenderDesign(DesignType.Win_Desc, .Left, .Top, .Width, .Height)

                Case DesignType.Win_Shadow
                    RenderDesign(DesignType.Win_Shadow, .Left, .Top, .Width, .Height)

                Case DesignType.Win_Party
                    RenderDesign(DesignType.Win_Party, .Left, .Top, .Width, .Height)
            End Select

            If Not .OnDraw Is Nothing Then .OnDraw.Start
        End With

    End Sub

    Public Sub RenderDesign(design As Long, left As Long, top As Long, width As Long, height As Long, Optional alpha As Long = 255)
        Dim bs As Long

        Select Case design
            Case DesignType.MenuHeader
                ' render the header
                RenderTexture(InterfaceSprite(61), GameWindow, left, top, 0, 0, width, height, , , 200, 47, 77, 29)

            Case DesignType.MenuOption
                ' render the option
                RenderTexture(InterfaceSprite(61), GameWindow, left, top, 0, 0, Width, Height, , , 200, 98, 98, 98)

            Case DesignType.Wood
                bs = 4
                ' render the wood box
                RenderEntity_Square(DesignSprite(1), left, top, width, height, bs, alpha)
                
                ' render wood texture
                RenderTexture(InterfaceSprite(1), GameWindow, left, top, width - (bs * 2), height - (bs * 2), width, height, , , alpha)

            Case DesignType.Wood_Small
                bs = 2
                ' render the wood box
                RenderEntity_Square(DesignSprite(8), left, top, width, height, bs, alpha)
                
                ' render wood texture
                RenderTexture(InterfaceSprite(1), GameWindow, left + bs, top + bs, width - (bs * 2), height - (bs * 2), width, height)

            Case DesignType.Wood_Empty
                bs = 4
                ' render the wood box
                RenderEntity_Square(DesignSprite(9), left, top, width, height, alpha)

            Case DesignType.Green
                bs = 2
                ' render the green box
                RenderEntity_Square(DesignSprite(2), left, top, width, height, bs, alpha)
                
                ' render green gradient overlay
                RenderTexture(GradientSprite(1), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Green_Hover
                bs = 2
                ' render the green box
                RenderEntity_Square(DesignSprite(2), left, top, width, height, bs, alpha)
                
                ' render green gradient overlay
                RenderTexture(GradientSprite(2), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Green_Click
                bs = 2
                ' render the green box
                RenderEntity_Square(DesignSprite(2), left, top, width, height, bs, alpha)
                
                ' render green gradient overlay
                RenderTexture(GradientSprite(3), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Red
                bs = 2
                ' render the red box
                RenderEntity_Square(DesignSprite(3), left, top, width, height, bs, alpha)
                
                ' render red gradient overlay
                RenderTexture(GradientSprite(4), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Red_Hover
                bs = 2
                ' render the red box
                RenderEntity_Square(DesignSprite(3), left, top, width, height, bs, alpha)
                
                ' render red gradient overlay
                RenderTexture(GradientSprite(5), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Red_Click
                bs = 2
                ' render the red box
                RenderEntity_Square(DesignSprite(3), left, top, width, height, bs, alpha)
                
                ' render red gradient overlay
                RenderTexture(GradientSprite(6), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Blue
                bs = 2
                ' render the Blue box
                RenderEntity_Square(DesignSprite(14), left, top, width, height, bs, alpha)
                
                ' render Blue gradient overlay
                RenderTexture(GradientSprite(8), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Blue_Hover
                bs = 2
                ' render the Blue box
                RenderEntity_Square(DesignSprite(14), left, top, width, height, bs, alpha)
                
                ' render Blue gradient overlay
                RenderTexture(GradientSprite(9), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Blue_Click
                bs = 2
                ' render the Blue box
                RenderEntity_Square(DesignSprite(14), left, top, width, height, bs, alpha)
                
                ' render Blue gradient overlay
                RenderTexture(GradientSprite(10), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Orange
                bs = 2
                ' render the Orange box
                RenderEntity_Square(DesignSprite(15), left, top, width, height, bs, alpha)
                
                ' render Orange gradient overlay
                RenderTexture(GradientSprite(11), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Orange_Hover
                bs = 2
                ' render the Orange box
                RenderEntity_Square(DesignSprite(15), left, top, width, height, bs, alpha)
                
                ' render Orange gradient overlay
                RenderTexture(GradientSprite(12), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Orange_Click
                bs = 2
                ' render the Orange box
                RenderEntity_Square(DesignSprite(15), left, top, width, height, bs, alpha)
                
                ' render Orange gradient overlay
                RenderTexture(GradientSprite(13), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Grey
                bs = 2
                ' render the Orange box
                RenderEntity_Square(DesignSprite(17), left, top, width, height, bs, alpha)
                
                ' render Orange gradient overlay
                RenderTexture(GradientSprite(14), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Parchment
                bs = 20
                ' render the parchment box
                RenderEntity_Square(DesignSprite(4), left, top, width, height, bs, alpha)

            Case DesignType.BlackOval
                bs = 4
                ' render the black oval
                RenderEntity_Square(DesignSprite(5), left, top, width, height, bs, alpha)

            Case DesignType.TextBlack
                bs = 5
                ' render the black oval
                RenderEntity_Square(DesignSprite(6), left, top, width, height, bs, alpha)

            Case DesignType.TextWhite
                bs = 5
                ' render the black oval
                RenderEntity_Square(DesignSprite(7), left, top, width, height, bs, alpha)

            Case DesignType.TextBlack_Sq
                bs = 4
                ' render the black oval
                RenderEntity_Square(DesignSprite(10), left, top, width, height, bs, alpha)

            Case DesignType.Win_Desc
                bs = 8
                ' render black square
                RenderEntity_Square(DesignSprite(11), left, top, width, height, bs, alpha)

            Case DesignType.DescPic
                bs = 3
                ' render the green box
                RenderEntity_Square(DesignSprite(12), left, top, width, height, bs, alpha)

                ' render green gradient overlay
                RenderTexture(GradientSprite(7), GameWindow, left + bs, top + bs, 0, 0, width - (bs * 2), height - (bs * 2), width, height, alpha)

            Case DesignType.Win_Shadow
                bs = 35
                ' render the green box
                RenderEntity_Square(DesignSprite(13), left - bs, top - bs, width + (bs * 2), height + (bs * 2), bs, alpha)

            Case DesignType.Win_Party
                bs = 12
                ' render black square
                RenderEntity_Square(DesignSprite(16), left, top, width, height, bs, alpha)

            Case DesignType.TileBox
                bs = 4
                ' render box
                RenderEntity_Square(DesignSprite(18), left, top, width, height, bs, alpha)
        End Select

    End Sub

    Public Sub RenderEntity_Square(sprite As Sprite, x As Long, y As Long, width As Long, height As Long, borderSize As Long, Optional alpha As Long = 255)
        Dim bs As Long

        ' Set the border size
        bs = borderSize
        ' Draw centre
        RenderTexture(sprite, GameWindow, X + bs, Y + bs, bs + 1, bs + 1, Width - (bs * 2), Height - (bs * 2), , , alpha)
        ' Draw top side
        RenderTexture(sprite, GameWindow, x + bs, y, bs, 0, width - (bs * 2), bs, 1, bs, alpha)
        ' Draw left side
        RenderTexture(sprite, GameWindow, X, Y + bs, 0, bs, bs, Height - (bs * 2), bs, , alpha)
        ' Draw right side
        RenderTexture(sprite, GameWindow, X + Width - bs, Y + bs, bs + 3, bs, bs, Height - (bs * 2), bs, , alpha)
        ' Draw bottom side
        RenderTexture(sprite, GameWindow, X + bs, Y + Height - bs, bs, bs + 3, Width - (bs * 2), bs, 1, bs, alpha)
        ' Draw top left corner
        RenderTexture(sprite, GameWindow, X, Y, 0, 0, bs, bs, bs, bs, alpha)
        ' Draw top right corner
        RenderTexture(sprite, GameWindow, X + Width - bs, Y, bs + 3, 0, bs, bs, bs, bs, alpha)
        ' Draw bottom left corner
        RenderTexture(sprite, GameWindow, X, Y + Height - bs, 0, bs + 3, bs, bs, bs, bs, alpha)
        ' Draw bottom right corner
        RenderTexture(sprite, GameWindow, X + Width - bs, Y + Height - bs, bs + 3, bs + 3, bs, bs, bs, bs, alpha)
    End Sub
    
    Sub Combobox_AddItem(winIndex As Long, controlIndex As Long, text As String)
        Dim count As Long
        count = UBound(Windows(winIndex).Controls(controlIndex).List)
        ReDim Preserve Windows(winIndex).Controls(controlIndex).List(count + 1)
        Windows(winIndex).Controls(controlIndex).List(count + 1) = text
    End Sub

    Public Sub CreateWindow(name As String, caption As String, zOrder As Long, left As Long, top As Long, width As Long, height As Long, icon As Long, _
       Optional visible As Boolean = True, Optional xOffset As Long = 0, Optional yOffset As Long = 0, Optional design_norm As Long = 0, Optional design_hover As Long = 0, Optional design_mousedown As Long = 0,
       Optional image_norm As Long = 0, Optional image_hover As Long = 0, Optional image_mousedown As Long = 0,
       Optional ByRef callback_norm As Task = Nothing, Optional ByRef callback_hover As Task = Nothing, Optional ByRef callback_mousedown As Task = Nothing, Optional ByRef callback_mousemove As Task = Nothing, Optional ByRef callback_dblclick As Task = Nothing, _
       Optional canDrag As Boolean = True, Optional zChange As Byte = True, Optional onDraw As Task = Nothing, Optional isActive As Boolean = True, Optional clickThrough As Boolean = False)

        Dim i As Long
        Dim design(EntState.State_Count - 1) As Long
        Dim image(EntState.State_Count - 1) As Long
        Dim callback(0 To EntState.State_Count - 1) As Task

        ' fill temp arrays
        design(EntState.Normal) = design_norm
        design(EntState.Hover) = design_hover
        design(EntState.MouseDown) = design_mousedown
        design(EntState.DblClick) = design_norm
        design(EntState.MouseUp) = design_norm
        image(EntState.Normal) = image_norm
        image(EntState.Hover) = image_hover
        image(EntState.MouseDown) = image_mousedown
        image(EntState.DblClick) = image_norm
        image(EntState.MouseUp) = image_norm
        callback(EntState.Normal) = callback_norm
        callback(EntState.Hover) = callback_hover
        callback(EntState.MouseDown) = callback_mousedown
        callback(EntState.MouseMove) = callback_mousemove
        callback(EntState.DblClick) = callback_dblclick

        ' redim the windows
        WindowCount = WindowCount + 1
        ReDim Preserve Windows(WindowCount)

        ' set the properties
        With Windows(WindowCount).Window
            .Name = name
            .Type = EntityType.entWindow

            ReDim .Design(EntState.State_Count - 1)
            ReDim .Image(EntState.State_Count - 1)
            ReDim .Callback(EntState.State_Count - 1)

            ' loop through states
            For i = 0 To EntState.State_Count - 1
                .Design(i) = design(i)
                .Image(i) = image(i)
                .callback(i) = callback(i)
            Next

            .Left = left
            .Top = top
            .OrigLeft = left
            .OrigTop = top
            .Width = width
            .Height = height
            .Visible = visible
            .CanDrag = canDrag
            .Text = caption
            .xOffset = xOffset
            .yOffset = yOffset
            .Icon = icon
            .Enabled = True
            .zChange = zChange
            .zOrder = zOrder
            .OnDraw = onDraw
            .ClickThrough = clickThrough

            ' set active
            If .Visible Then activeWindow = WindowCount
        End With

        ' set the zOrder
        zOrder_Win = zOrder_Win + 1
    End Sub

    Public Sub CreateTextbox(winNum As Long, name As String, left As Long, top As Long, width As Long, height As Long, text As String, _
        Optional font As String = "Georgia.ttf", Optional align As Byte = AlignmentType.AlignLeft, Optional visible As Boolean = True, Optional alpha As Long = 255, Optional isActive As Boolean = True, Optional xOffset As Long = 0, Optional yOffset As Long = 0, Optional image_norm As Long = 0,
        Optional image_hover As Long = 0, Optional image_mousedown As Long = 0, Optional design_norm As Long = 0, Optional design_hover As Long = 0, Optional design_mousedown As Long = 0,
        Optional ByRef callback_norm As Task = Nothing, Optional ByRef callback_hover As Task = Nothing, Optional ByRef callback_mousedown As Task = Nothing, Optional ByRef callback_mousemove As Task = Nothing, Optional ByRef callback_dblclick As Task = Nothing, Optional ByRef callback_enter As Task = Nothing)

        Dim design(EntState.State_Count - 1) As Long
        Dim image(EntState.State_Count - 1) As Long
        Dim callback(EntState.State_Count - 1) as Task

        ' fill temp arrays
        design(EntState.Normal) = design_norm
        design(EntState.Hover) = design_hover
        design(EntState.MouseDown) = design_mousedown
        image(EntState.Normal) = image_norm
        image(EntState.Hover) = image_hover
        image(EntState.MouseDown) = image_mousedown
        callback(EntState.Normal) = callback_norm
        callback(EntState.Hover) = callback_hover
        callback(EntState.MouseDown) = callback_mousedown
        callback(EntState.MouseMove) = callback_mousemove
        callback(EntState.DblClick) = callback_dblclick
        callback(EntState.Enter) = callback_enter

        ' create the textbox
        CreateEntity(winNum, zOrder_Con, name, EntityType.entTextBox, design, image, callback, left, top, width, height, visible, , , , , text, align, font, alpha, , xOffset, yOffset, , , isActive)
    End Sub

    Public Sub CreatePictureBox(winNum As Long, name As String, left As Long, top As Long, width As Long, height As Long,
       Optional visible As Boolean = True, Optional canDrag As Boolean = True, Optional alpha As Long = 255, Optional clickThrough As Boolean = True, Optional image_norm As Long = 0, Optional image_hover As Long = 0, Optional image_mousedown As Long = 0, Optional design_norm As Long = 0, Optional design_hover As Long = 0, Optional design_mousedown As Long = 0,
       Optional ByRef callback_norm As Task = Nothing, Optional ByRef callback_hover As Task = Nothing, Optional ByRef callback_mousedown As Task = Nothing, _
       Optional ByRef callback_mousemove As Task = Nothing, Optional ByRef callback_dblclick As Task = Nothing, Optional ByRef onDraw As Task = Nothing)

        Dim design(EntState.State_Count - 1) As Long
        Dim image(EntState.State_Count - 1) As Long
        Dim callback(EntState.State_Count - 1) As Task

        ' fill temp arrays
        design(EntState.Normal) = design_norm
        design(EntState.Hover) = design_hover
        design(EntState.MouseDown) = design_mousedown
        image(EntState.Normal) = image_norm
        image(EntState.Hover) = image_hover
        image(EntState.MouseDown) = image_mousedown
        callback(EntState.Normal) = callback_norm
        callback(EntState.Hover) = callback_hover
        callback(EntState.MouseDown) = callback_mousedown
        callback(EntState.MouseMove) = callback_mousemove
        callback(EntState.DblClick) = callback_dblclick

        ' create the box
        CreateEntity(winNum, zOrder_Con, name, EntityType.entPictureBox, design, image, callback, left, top, width, height, visible, canDrag, , , , , , , alpha, clickThrough, , , , onDraw)
    End Sub

    Public Sub CreateButton(winNum As Long, name As String, left As Long, top As Long, width As Long, height As Long, text As String, _
       Optional font As String = "Georgia.ttf", Optional image_norm As Long = 0, Optional image_hover As Long = 0, Optional image_mousedown As Long = 0,
       Optional visible As Boolean = True, Optional alpha As Long = 255, Optional design_norm As Long = 0, Optional design_hover As Long = 0, Optional design_mousedown As Long = 0, _
       Optional ByRef callback_norm As Task = Nothing, Optional ByRef callback_hover As Task = Nothing, Optional ByRef callback_mousedown As Task = Nothing, Optional ByRef callback_mousemove As Task = Nothing, Optional ByRef callback_dblclick As Task = Nothing, _
       Optional xOffset As Long = 0, Optional yOffset As Long = 0, Optional tooltip As String = "")

        Dim design(EntState.State_Count - 1) As Long
        Dim image(EntState.State_Count - 1) As Long
        Dim callback(EntState.State_Count - 1) As Task

        ' fill temp arrays
        design(EntState.Normal) = design_norm
        design(EntState.Hover) = design_hover
        design(EntState.MouseDown) = design_mousedown
        image(EntState.Normal) = image_norm
        image(EntState.Hover) = image_hover
        image(EntState.MouseDown) = image_mousedown
        callback(EntState.Normal) = callback_norm
        callback(EntState.Hover) = callback_hover
        callback(EntState.MouseDown) = callback_mousedown
        callback(entState.MouseMove) = callback_mousemove
        callback(EntState.DblClick) = callback_dblclick

        ' create the button 
        CreateEntity(winNum, zOrder_Con, name, EntityType.entButton, design, image, callback, left, top, width, height, visible, , , , , text, , font, alpha, , xOffset, yOffset, , , , tooltip)
    End Sub

    Public Sub CreateLabel(winNum As Long, name As String, left As Long, top As Long, width As Long, height As Long, text As String, font As String, _
       Optional align As Byte = AlignmentType.alignLeft, Optional visible As Boolean = True, Optional alpha As Long = 255, Optional clickThrough As Boolean = False, _
       Optional ByRef callback_norm As Task = Nothing, Optional ByRef callback_hover As Task = Nothing, Optional ByRef callback_mousedown As Task = Nothing, Optional ByRef callback_mousemove As Task = Nothing, Optional ByRef callback_dblclick As Task = Nothing)

        Dim design(EntState.State_Count - 1) As Long
        Dim image(EntState.State_Count - 1) As Long
        Dim callback(EntState.State_Count - 1) As Task

         ' fill temp arrays
        callback(EntState.Normal) = callback_norm
        callback(EntState.Hover) = callback_hover
        callback(EntState.MouseDown) = callback_mousedown
        callback(EntState.MouseMove) = callback_mousemove
        callback(EntState.DblClick) = callback_dblclick

        ' create the label
        CreateEntity(winNum, zOrder_Con, name, EntityType.entLabel, design, image, callback, left, top, width, height, visible, , , , , text, align, font, alpha, clickThrough)
    End Sub

    Public Sub CreateCheckbox(winNum As Long, name As String, left As Long, top As Long, width As Long, text As String, font As String, _
        Optional height As Long = 15, Optional value As Long = 0, Optional align As Byte = AlignmentType.AlignLeft, Optional visible As Boolean = True, Optional alpha As Long = 255,
        Optional theDesign As Long = 0, Optional group As Long = 0, _
        Optional ByRef callback_norm As Task = Nothing, Optional ByRef callback_hover As Task = Nothing, Optional ByRef callback_mousedown As Task = Nothing, Optional ByRef callback_mousemove As Task = Nothing, Optional ByRef callback_dblclick As Task = Nothing)

        Dim design(EntState.State_Count - 1) As Long
        Dim image(EntState.State_Count - 1) As Long
        Dim callback(EntState.State_Count - 1) As Task

        design(0) = theDesign

        ' fill temp arrays
        callback(EntState.Normal) = callback_norm
        callback(EntState.Hover) = callback_hover
        callback(EntState.MouseDown) = callback_mousedown
        callback(EntState.MouseMove) = callback_mousemove
        callback(EntState.DblClick) = callback_dblclick

        ' create the box
        CreateEntity(winNum, zOrder_Con, name, EntityType.entCheckbox, design, image, callback, left, top, width, height, visible, , , , value, text, align, font, alpha, , , , , , , , group)
    End Sub

    Public Sub CreateComboBox(winNum As Long, name As String, left As Long, top As Long, width As Long, height As Long, design As Long)
        Dim theDesign(EntState.state_Count - 1) As Long
        Dim image(EntState.State_Count - 1) As Long
        Dim callback(EntState.State_Count - 1) As Task
        
        theDesign(0) = design
        
        ' create the box
        CreateEntity(winNum, zOrder_Con, name, EntityType.entCombobox, theDesign, image, callback, left, top, width, height)
    End Sub

    Public Function GetWindowIndex(winName As String) As Long
        Dim i As Long

        For i = 1 To WindowCount

            If LCase$(Windows(i).Window.Name) = LCase$(winName) Then
                GetWindowIndex = i
                Exit Function
            End If

        Next

        GetWindowIndex = 0
    End Function
        
    Public Function GetControlIndex(winName As String, controlName As String) As Long
        Dim i As Long, winIndex As Long

        winIndex = GetWindowIndex(winName)

        If Not winIndex > 0 Or Not winIndex <= WindowCount Then Exit Function

        For i = 1 To Windows(winIndex).ControlCount

            If LCase$(Windows(winIndex).Controls(i).Name) = LCase$(controlName) Then
                GetControlIndex = i
                Exit Function
            End If

        Next

        GetControlIndex = 0
    End Function

    Public Function SetActiveControl(curWindow As Long, curControl As Long) As Boolean
        ' make sure it's something which CAN be active
        Select Case Windows(curWindow).Controls(curControl).Type
            Case EntityType.entTextBox
                Windows(curWindow).ActiveControl = curControl
                SetActiveControl = True
        End Select
    End Function

    Public Sub CentralizeWindow(curWindow As Long)
        With Windows(curWindow).Window
            .Left = (Settings.Width / 2) - (.Width / 2)
            .Top = (Settings.Height / 2) - (.Height / 2)
            .OrigLeft = .Left
            .OrigTop = .Top
        End With
    End Sub

    Public Sub HideWindows()
        Dim i As Long

        For i = 1 To WindowCount
            HideWindow(i)
        Next
    End Sub

    Public Sub ShowWindow(curWindow As Long, Optional forced As Boolean = False, Optional resetPosition As Boolean = True)
        Windows(curWindow).Window.Visible = True

        If forced Then
            UpdateZOrder(curWindow, forced)
            activeWindow = curWindow
        ElseIf Windows(curWindow).Window.zChange Then
            UpdateZOrder(curWindow)
            activeWindow = curWindow
        End If

        If resetPosition Then
            With Windows(curWindow).Window
                .Left = .OrigLeft
                .Top = .OrigTop
            End With
        End If
    End Sub

    Public Sub HideWindow(curWindow As Long)
        Dim i As Long

        Windows(curWindow).Window.Visible = False

        ' find next window to set as active
        For i = WindowCount To 1 Step -1
            If Windows(i).Window.Visible And Windows(i).Window.zChange Then
                activeWindow = i
                Exit For
            End If
        Next
    End Sub

    Public Sub CreateWindow_Login()
        ' Create the window
        CreateWindow("winLogin", "Login", zOrder_Win, 0, 0, 276, 182, 45, , 3, 5, DesignType.Win_Norm, DesignType.Win_Norm, DesignType.Win_Norm)

        ' Centralise it
        CentralizeWindow(WindowCount)

        ' Set the index for spawning controls
        zOrder_Con = 1

        ' Parchment
        CreatePictureBox(WindowCount, "picParchment", 6, 26, 264, 150, , , , , , , , DesignType.Parchment, DesignType.Parchment, DesignType.Parchment)
        
        ' Shadows
        CreatePictureBox(WindowCount, "picShadow_1", 67, 43, 142, 9, , ,  , , , , , DesignType.BlackOval, DesignType.BlackOval, DesignType.BlackOval)
        CreatePictureBox(WindowCount, "picShadow_2", 67, 79, 142, 9, , , , , , , , DesignType.BlackOval, DesignType.BlackOval, DesignType.BlackOval)
        
        ' Close button
        CreateButton(WindowCount, "btnClose", Windows(WindowCount).Window.width - 19, 4, 16, 16, "", , 8, 9, 10, , , , , , , , New Task(AddressOf DestroyGame))

        ' Buttons
        CreateButton(WindowCount, "btnAccept", 67, 134, 67, 22, "Accept", , , , , , DesignType.Green, DesignType.Green_Hover, DesignType.Green_Click)
        CreateButton(WindowCount, "btnExit", 142, 134, 67, 22, "Exit", , , , , , DesignType.Red, DesignType.Red_Hover, DesignType.Red_Click, , , , New Task(AddressOf DestroyGame))
        
        ' Labels
        CreateLabel(WindowCount, "lblUsername", 72, 40, 142, 0, "Username", Georgia, AlignmentType.AlignCentre)
        CreateLabel(WindowCount, "lblPassword", 72, 76, 142, 0, "Password", Georgia, AlignmentType.AlignCentre)
        
        ' Textboxes
        CreateTextbox(WindowCount, "txtUser", 67, 55, 142, 19, Settings.Username, , AlignmentType.AlignLeft , , , 5, 3, , , , , DesignType.TextWhite, DesignType.TextWhite, DesignType.TextWhite)
        CreateTextbox(WindowCount, "txtPass", 67, 91, 142, 19, "", , AlignmentType.AlignLeft, , , , 5, 3, , , , DesignType.TextWhite, DesignType.TextWhite, DesignType.TextWhite)
        
        ' Checkbox
        CreateCheckbox(WindowCount, "chkSavePass", 67, 114, 142, "Save Password?", Georgia, ,  , , , , DesignType.ChkNorm)

        ' Set the active control
        If Not Len(Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "txtUser")).Text) > 0 Then
            SetActiveControl(GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtUser"))
        Else
            SetActiveControl(GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtPass"))
        End If
    End Sub

    ' Rendering & Initialisation
    Public Sub InitInterface()
        ' Starter values
        zOrder_Win = 1
        zOrder_Con = 1

        ' Menu
        CreateWindow_Login
    End Sub

    Public Function HandleInterfaceEvents(entState As EntState) As Boolean
        Dim i As Long, curWindow As Long, curControl As Long, callBack As Task, x As Long
    
        ' if hiding gui
        If hideGUI = True Or Editor = EditorType.Map Then Exit Function

        ' Find the container
        For i = 1 To WindowCount
            With Windows(i).Window
                If .enabled And .visible Then
                    If .state <> EntState.MouseDown Then .state = EntState.Normal
                    If CurMouseX >= .left And CurMouseX <= .width + .left Then
                        If CurMouseY >= .top And CurMouseY <= .height + .top Then
                            ' set the combomenu
                            If .design(0) = DesignType.ComboMenuNorm Then
                                ' set the hover menu
                                If entState = EntState.MouseMove Or entState = EntState.Hover Then
                                    'ComboMenu_MouseMove(i)
                                ElseIf entState = EntState.MouseDown Then
                                    'ComboMenu_MouseDown(i)
                                End If
                            End If

                            ' everything else
                            If curWindow = 0 Then curWindow = i
                            If .zOrder > Windows(curWindow).Window.zOrder Then curWindow = i
                        End If
                    End If

                    If entState = EntState.MouseMove Then
                        If .canDrag Then
                            If .state = EntState.MouseDown Then
                                .left = Math.Clamp(.left + ((CurMouseX - .left) - .movedX), 0, Settings.Width - .width)
                                .top = Math.Clamp(.top + ((CurMouseY - .top) - .movedY), 0, Settings.Height - .height)
                            End If
                        End If
                    End If
                End If
            End With
        Next

        ' Handle any controls first
        If curWindow Then
            ' reset /all other/ control mouse events
            For i = 1 To WindowCount
                If i <> curWindow Then
                    For x = 1 To Windows(i).ControlCount
                        Windows(i).Controls(x).state = EntState.Normal
                    Next
                End If
            Next

            For i = 1 To Windows(curWindow).ControlCount
                With Windows(curWindow).Controls(i)
                    If .enabled And .visible Then
                        If .state <> EntState.MouseDown Then .state = EntState.Normal
                        If CurMouseX >= .left + Windows(curWindow).Window.left And CurMouseX <= .left + .width + Windows(curWindow).Window.left Then
                            If CurMouseY >= .top + Windows(curWindow).Window.top And CurMouseY <= .top + .height + Windows(curWindow).Window.top Then
                                If curControl = 0 Then curControl = i
                                If .zOrder > Windows(curWindow).Controls(curControl).zOrder Then curControl = i
                            End If
                        End If

                        If entState = EntState.MouseMove Then
                            If .canDrag Then
                                If .state = EntState.MouseDown Then
                                    .left = Math.Clamp(.left + ((CurMouseX - .left) - .movedX), 0, Windows(curWindow).Window.width - .width)
                                    .top = Math.Clamp(.top + ((CurMouseY - .top) - .movedY), 0, Windows(curWindow).Window.height - .height)
                                End If
                            End If
                        End If
                    End If
                End With
            Next

            ' Handle control
            If curControl Then
                With Windows(curWindow).Controls(curControl)
                    If .state <> EntState.MouseDown Then
                        If entState <> EntState.MouseMove Then
                            .state = entState
                        Else
                            .state = EntState.Hover
                        End If
                    End If

                    If entState = EntState.MouseDown Then
                        If .canDrag Then
                            .movedX = CurMouseX - .left
                            .movedY = CurMouseY - .top
                        End If

                        ' toggle boxes
                        Select Case .Type
                            Case EntityType.entCheckbox
                                ' grouped boxes
                                If .group > 0 Then
                                    If .value = 0 Then
                                        For i = 1 To Windows(curWindow).ControlCount
                                            If Windows(curWindow).Controls(i).Type = EntityType.entCheckbox Then
                                                If Windows(curWindow).Controls(i).group = .group Then
                                                    Windows(curWindow).Controls(i).value = 0
                                                End If
                                            End If
                                        Next
                                        .value = 1
                                    End If
                                Else
                                    If .value = 0 Then
                                        .value = 1
                                    Else
                                        .value = 0
                                    End If
                                End If
                            Case EntityType.entCombobox
                                'ShowComboMenu(curWindow, curControl)
                        End Select

                        ' set active input
                        SetActiveControl(curWindow, curControl)
                    End If
                    callBack = .CallBack(entState)
                End With
            Else
                ' Handle container
                With Windows(curWindow).Window
                    If .state <> EntState.MouseDown Then
                        If entState <> EntState.MouseMove Then
                            .state = entState
                        Else
                            .state = EntState.Hover
                        End If
                    End If

                    If entState = EntState.MouseDown Then
                        If .canDrag Then
                            .movedX = CurMouseX - .left
                            .movedY = CurMouseY - .top
                        End If
                    End If
                    callBack = .CallBack(entState)
                End With
            End If

            ' bring to front
            If entState = EntState.MouseDown Then
                UpdateZOrder(curWindow)
                activeWindow = curWindow
            End If

            ' call back
            If Not callBack Is Nothing Then callBack.Start()
        End If

        ' Reset
        If entState = EntState.MouseUp Then ResetMouseDown
    End Function

    Public Sub ResetInterface()
        Dim i As Long, x As Long

        For i = 1 To WindowCount
            If Windows(i).Window.state <> EntState.MouseDown Then Windows(i).Window.state = EntState.Normal

            For x = 1 To Windows(i).ControlCount
                If Windows(i).Controls(x).state <> EntState.MouseDown Then Windows(i).Controls(x).state = EntState.Normal
            Next
        Next

    End Sub

    Public Sub ResetMouseDown()
        Dim callBack As Task
        Dim i As Long, x As Long

        For i = 1 To WindowCount

            With Windows(i)
                .Window.state = EntState.Normal
                callBack = .Window.CallBack(EntState.Normal)

                If Not callback Is Nothing Then callBack.Start()

                For x = 1 To .ControlCount
                    .Controls(x).state = EntState.Normal
                    callBack = .Controls(x).CallBack(EntState.Normal)

                    If Not callback Is Nothing Then callBack.Start()
                Next

            End With

        Next

    End Sub

    Sub CloseComboMenu()
        HideWindow(GetWindowIndex("winComboMenuBG"))
        HideWindow(GetWindowIndex("winComboMenu"))
    End Sub

    Sub ShowComboMenu()
        ShowWindow(GetWindowIndex("winComboMenuBG"))
        ShowWindow(GetWindowIndex("winComboMenu"))
    End Sub
End Module

