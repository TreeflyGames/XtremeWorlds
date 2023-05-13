Imports System.Drawing
Imports Core
Imports Mirage.Sharp.Asfw

Module C_Banks

#Region "Globals & Types"

    Friend Bank As Types.BankStruct

    ' Stores the last bank item we showed in desc
    Friend LastBankDesc As Integer

    Friend InBank As Boolean

    ' bank drag + drop
    Friend DragBankSlotNum As Integer

    Friend BankX As Integer
    Friend BankY As Integer

#End Region

#Region "Database"

    Sub ClearBank()
        ReDim Bank.Item(MAX_BANK)
    End Sub

#End Region

#Region "Incoming Packets"

    Friend Sub Packet_OpenBank(ByRef data() As Byte)
        Dim i As Integer, x As Integer
        Dim buffer As New ByteStream(data)

        For i = 1 To MAX_BANK
            Bank.Item(i).Num = buffer.ReadInt32
            Bank.Item(i).Value = buffer.ReadInt32
        Next

        NeedToOpenBank = True

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub DepositItem(invslot As Integer, amount As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CDepositItem)
        buffer.WriteInt32(invslot)
        buffer.WriteInt32(amount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub WithdrawItem(bankslot As Integer, amount As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CWithdrawItem)
        buffer.WriteInt32(bankslot)
        buffer.WriteInt32(amount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub ChangeBankSlots(oldSlot As Integer, newSlot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CChangeBankSlots)
        buffer.WriteInt32(oldSlot)
        buffer.WriteInt32(newSlot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub CloseBank()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CCloseBank)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

        InBank = False
        PnlBankVisible = False
    End Sub

#End Region

#Region "Drawing"

    Sub DrawBank()
        Dim i As Integer, x As Integer, y As Integer, itemnum As Integer
        Dim amount As String
        Dim sRect As Rectangle, dRect As Rectangle
        Dim sprite As Integer, Color As SFML.Graphics.Color

        BankWindowX =  GameWindow.Size.X / 2 - BankPanelGfxInfo.Width / 2
        BankWindowY = GameWindow.Size.Y / 2 - BankPanelGfxInfo.Height / 2

        'first render panel
        RenderTexture(BankPanelSprite, GameWindow, BankWindowX, BankWindowY, 0, 0, BankPanelGfxInfo.Width, BankPanelGfxInfo.Height)

        'Headertext
        RenderText("Your Bank", GameWindow, BankWindowX + 140, BankWindowY + 6, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 15)

        'close
        RenderText("Close Bank", GameWindow, BankWindowX + 140, BankWindowY + BankPanelGfxInfo.Height - 20, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 15)

       For i = 1 To MAX_BANK
            itemnum = GetBankItemNum(i)
            If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then
                StreamItem(itemnum)
                sprite = Item(itemnum).Pic

                If ItemsGfxInfo(sprite).IsLoaded = False Then
                    LoadTexture(sprite, 4)
                End If

                'seeying we still use it, lets update timer
                With ItemsGfxInfo(sprite)
                    .TextureTimer = GetTickCount() + 100000
                End With

                With sRect
                    .Y = 0
                    .Height = PicY
                    .X = 0
                    .Width = PicX
                End With

                With dRect
                    .Y = BankWindowY + BankTop + ((BankOffsetY + 32) * ((i - 1) \ BankColumns))
                    .Height = PicY
                    .X = BankWindowX + BankLeft + ((BankOffsetX + 32) * (((i - 1) Mod BankColumns)))
                    .Width = PicX
                End With

                RenderTexture(ItemsSprite(sprite), GameWindow, dRect.X, dRect.Y, sRect.X, sRect.Y, sRect.Width, sRect.Height)

                ' If item is a stack - draw the amount you have
                If GetBankItemValue(i) > 1 Then
                    y = dRect.Top + 22
                    x = dRect.Left - 4

                    amount = GetBankItemValue(i)
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
        Next

    End Sub

    Friend Sub DrawBankItem(x As Integer, y As Integer)
        Dim rec As Rectangle

        Dim itemnum As Integer
        Dim sprite As Integer

        itemnum = GetBankItemNum(DragBankSlotNum)
        sprite = Item(GetBankItemNum(DragBankSlotNum)).Pic

        If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then

            If ItemsGfxInfo(sprite).IsLoaded = False Then
                LoadTexture(sprite, 4)
            End If

            'seeying we still use it, lets update timer
            With ItemsGfxInfo(sprite)
                .TextureTimer = GetTickCount() + 100000
            End With

            With rec
                .Y = 0
                .Height = PicY
                .X = 0
                .Width = PicX
            End With
        End If

        RenderTexture(ItemsSprite(sprite), GameWindow, x + 16, y + 16, rec.X, rec.Y, rec.Width, rec.Height)

    End Sub

#End Region

End Module