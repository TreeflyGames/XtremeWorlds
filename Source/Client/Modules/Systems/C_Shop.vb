Imports System.Drawing
Imports Mirage.Sharp.Asfw
Imports Mirage.Basic.Engine

Module C_Shops

#Region "Globals & Types"

    Friend InShop As Integer ' is the player in a shop?
    Friend ShopAction As Byte ' stores the current shop action

#End Region

#Region "Database"

    Sub ClearShop(index As Integer)
        Shop(index) = Nothing
        Shop(index).Name = ""
        ReDim Shop(index).TradeItem(MAX_TRADES)
        For x = 1 To MAX_TRADES
            ReDim Shop(index).TradeItem(x)
        Next
        Shop_Loaded(index) = False
    End Sub

    Sub ClearShops()
        Dim i As Integer

        ReDim Shop(MAX_SHOPS)

       For i = 1 To MAX_SHOPS
            ClearShop(i)
        Next

    End Sub

    Sub StreamShop(shopNum As Integer)
        If shopNum > 0 and Shop(shopNum).Name = "" And Shop_Loaded(shopNum) = False Then
            Shop_Loaded(shopNum) = True
            SendRequestItem(shopNum)
        End If
    End Sub

#End Region

#Region "Incoming Packets"

    Friend Sub Packet_OpenShop(ByRef data() As Byte)
        Dim shopnum As Integer
        Dim buffer As New ByteStream(data)
        shopnum = buffer.ReadInt32

        NeedToOpenShop = True
        NeedToOpenShopNum = shopnum

        buffer.Dispose()
    End Sub

    Friend Sub Packet_ResetShopAction(ByRef data() As Byte)
        ShopAction = 0
    End Sub

    Friend Sub Packet_UpdateShop(ByRef data() As Byte)
        Dim shopnum As Integer
        Dim buffer As New ByteStream(data)
        shopnum = buffer.ReadInt32

        Shop(shopnum).BuyRate = buffer.ReadInt32()
        Shop(shopnum).Name = Trim(buffer.ReadString())
        Shop(shopnum).Face = buffer.ReadInt32()

        For i = 1 To MAX_TRADES
            Shop(shopnum).TradeItem(i).CostItem = buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).CostValue = buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).Item = buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).ItemValue = buffer.ReadInt32()
        Next

        If Shop(shopnum).Name Is Nothing Then Shop(shopnum).Name = ""

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub SendRequestShop(shopNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestShop)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub BuyItem(shopslot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CBuyItem)
        buffer.WriteInt32(shopslot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SellItem(invslot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSellItem)
        buffer.WriteInt32(invslot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

#Region "Drawing"

    Sub DrawShop()
        Dim i As Integer, x As Integer, y As Integer, itemnum As Integer, itempic As Integer
        Dim amount As String
        Dim rec As Rectangle, recPos As Rectangle
        Dim Color As SFML.Graphics.Color

        If Not InGame OrElse PnlShopVisible = False Then Exit Sub

        'first render panel
        RenderTexture(ShopPanelSprite, GameWindow, ShopWindowX, ShopWindowY, 0, 0, ShopPanelGfxInfo.Width, ShopPanelGfxInfo.Height)

        if InShop = 0 Then Exit Sub

        StreamShop(InShop)

        If Shop(InShop).Face > 0 Then
            'render face
            If FacesGfxInfo(Shop(InShop).Face).IsLoaded = False Then
                LoadTexture(Shop(InShop).Face, 7)
            End If

            'seeying we still use it, lets update timer
            With FacesGfxInfo(Shop(InShop).Face)
                .TextureTimer = GetTickCount() + 100000
            End With
            RenderTexture(FacesSprite(Shop(InShop).Face), GameWindow, ShopWindowX + ShopFaceX, ShopWindowY + ShopFaceY, 0, 0, FacesGfxInfo(Shop(InShop).Face).Width, FacesGfxInfo(Shop(InShop).Face).Height)
        End If

        'draw text
        RenderText(Shop(InShop).Name, GameWindow, ShopWindowX + ShopLeft, ShopWindowY + 10, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 15)

        RenderText("Hello, and welcome", GameWindow, ShopWindowX + 10, ShopWindowY + 10, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 15)
        RenderText("to the shop!", GameWindow, ShopWindowX + 10, ShopWindowY + 25, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 15)

        'render buy button
        If CurMouseX > ShopWindowX + ShopButtonBuyX AndAlso CurMouseX < ShopWindowX + ShopButtonBuyX + ButtonGfxInfo.Width And
             CurMouseY > ShopWindowY + ShopButtonBuyY AndAlso CurMouseY < ShopWindowY + ShopButtonBuyY + ButtonGfxInfo.Height Then
            DrawButton("Buy Item", ShopWindowX + ShopButtonBuyX, ShopWindowY + ShopButtonBuyY, 1)
        Else
            DrawButton("Buy Item", ShopWindowX + ShopButtonBuyX, ShopWindowY + ShopButtonBuyY, 0)
        End If

        'render sell button
        If CurMouseX > ShopWindowX + ShopButtonSellX AndAlso CurMouseX < ShopWindowX + ShopButtonSellX + ButtonGfxInfo.Width And
             CurMouseY > ShopWindowY + ShopButtonSellY AndAlso CurMouseY < ShopWindowY + ShopButtonSellY + ButtonGfxInfo.Height Then
            DrawButton("Sell Item", ShopWindowX + ShopButtonSellX, ShopWindowY + ShopButtonSellY, 1)
        Else
            DrawButton("Sell Item", ShopWindowX + ShopButtonSellX, ShopWindowY + ShopButtonSellY, 0)
        End If

        'render close button
        If CurMouseX > ShopWindowX + ShopButtonCloseX AndAlso CurMouseX < ShopWindowX + ShopButtonCloseX + ButtonGfxInfo.Width And
             CurMouseY > ShopWindowY + ShopButtonCloseY AndAlso CurMouseY < ShopWindowY + ShopButtonCloseY + ButtonGfxInfo.Height Then
            DrawButton("Close Shop", ShopWindowX + ShopButtonCloseX, ShopWindowY + ShopButtonCloseY, 1)
        Else
            DrawButton("Close Shop", ShopWindowX + ShopButtonCloseX, ShopWindowY + ShopButtonCloseY, 0)
        End If

       For i = 1 To MAX_TRADES
            itemnum = Shop(InShop).TradeItem(i).Item
            If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then
                StreamItem(itemnum)
                itempic = Item(itemnum).Pic
                If itempic > 0 AndAlso itempic <= NumItems Then

                    If ItemsGfxInfo(itempic).IsLoaded = False Then
                        LoadTexture(itempic, 4)
                    End If

                    'seeying we still use it, lets update timer
                    With ItemsGfxInfo(itempic)
                        .TextureTimer = GetTickCount() + 100000
                    End With

                    With rec
                        .Y = 0
                        .Height = 32
                        .X = 0
                        .Width = 32
                    End With

                    With recPos
                        .Y = ShopWindowY + ShopTop + ((ShopOffsetY + 32) * ((i - 1) \ ShopColumns))
                        .Height = PicY
                        .X = ShopWindowX + ShopLeft + ((ShopOffsetX + 1 + 32) * (((i - 1) Mod ShopColumns)))
                        .Width = PicX
                    End With

                    RenderTexture(ItemsSprite(itempic), GameWindow, recPos.X, recPos.Y, rec.X, rec.Y, rec.Width, rec.Height)

                    ' If item is a stack - draw the amount you have
                    If Shop(InShop).TradeItem(i).ItemValue > 1 Then
                        y = recPos.Top + 22
                        x = recPos.Left - 4
                        amount = Shop(InShop).TradeItem(i).ItemValue
                        Color = SFML.Graphics.Color.White
                        ' Draw currency but with k, m, b etc. using a convertion function
                        If CLng(amount) < 1000000 Then
                            Color = SFML.Graphics.Color.White
                        ElseIf CLng(amount) > 1000000 AndAlso CLng(amount) < 10000000 Then
                            Color = SFML.Graphics.Color.Yellow
                        ElseIf CLng(amount) > 10000000 Then
                            Color = SFML.Graphics.Color.Green
                        End If

                        RenderText(ConvertCurrency(amount), GameWindow, x, y, Color, SFML.Graphics.Color.Black)
                    End If
                End If
            End If
        Next

    End Sub

#End Region

End Module