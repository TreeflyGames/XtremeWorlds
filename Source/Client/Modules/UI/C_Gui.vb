Imports System.Drawing
Imports System.Windows.Forms
Imports Mirage.Sharp.Asfw
Imports Core

Friend Module C_Gui

    Friend Sub CheckGuiMove(x As Integer, y As Integer)
        Dim eqNum As Integer, invNum As Integer, skillslot As Integer
        Dim bankitem As Integer, shopslot As Integer, tradeNum As Integer

        ShowItemDesc = False
        'Charpanel
        If PnlCharacterVisible Then
            If x > CharWindowX AndAlso x < CharWindowX + CharPanelGfxInfo.Width Then
                If y > CharWindowY AndAlso y < CharWindowY + CharPanelGfxInfo.Height Then
                    eqNum = IsEqItem(x, y)
                    If eqNum <> 0 Then
                        UpdateDescWindow(GetPlayerEquipment(Myindex, eqNum), 0, eqNum, 1)
                        LastItemDesc = GetPlayerEquipment(Myindex, eqNum) ' set it so you don't re-set values
                        ShowItemDesc = True
                        Exit Sub
                    Else
                        ShowItemDesc = False
                        LastItemDesc = 0 ' no item was last loaded
                    End If
                End If
            End If
        End If

        'inventory
        If PnlInventoryVisible Then
            If AboveInvpanel(x, y) Then
                InvX = x
                InvY = y

                If DragInvSlotNum > 0 Then
                    If InTrade Then Exit Sub
                    If InBank OrElse InShop Then Exit Sub
                    DrawInventoryItem(x, y)
                    ShowItemDesc = False
                    LastItemDesc = 0 ' no item was last loaded
                Else
                    invNum = IsInvItem(x, y)

                    If invNum <> 0 Then
                        ' exit out if we're offering that item
                        For i = 1 To MAX_INV
                            If TradeYourOffer(i).Num = invNum Then
                                Exit Sub
                            End If
                        Next
                        UpdateDescWindow(GetPlayerInvItemNum(Myindex, invNum), GetPlayerInvItemValue(Myindex, invNum), invNum, 0)
                        LastItemDesc = GetPlayerInvItemNum(Myindex, invNum) ' set it so you don't re-set values
                        ShowItemDesc = True
                        Exit Sub
                    Else
                        ShowItemDesc = False
                        LastItemDesc = 0 ' no item was last loaded
                    End If
                End If
            End If
        End If

        'skills
        If PnlSkillsVisible = True Then
            If AboveSkillpanel(x, y) Then
                SkillX = x
                SkillY = y

                If DragSkillSlotNum > 0 Then
                    If InTrade Then Exit Sub
                    If InBank OrElse InShop Then Exit Sub
                    DrawSkillItem(x, y)
                    LastSkillDesc = 0 ' no item was last loaded
                    ShowSkillDesc = False
                Else
                    skillslot = IsPlayerSkill(x, y)

                    If skillslot <> 0 Then
                        UpdateSkillWindow(Player(Myindex).Skill(skillslot).Num)
                        LastSkillDesc = Player(Myindex).Skill(skillslot).Num
                        ShowSkillDesc = True
                        Exit Sub
                    Else
                        LastSkillDesc = 0
                        ShowSkillDesc = False
                    End If
                End If

            End If
        End If

        'bank
        If PnlBankVisible = True Then
            If AboveBankpanel(x, y) Then
                BankX = x
                BankY = y

                If DragBankSlotNum > 0 Then
                    DrawBankItem(x, y)
                Else
                    bankitem = IsBankItem(x, y)

                    If bankitem <> 0 Then

                        UpdateDescWindow(Bank.Item(bankitem).Num, Bank.Item(bankitem).Value, bankitem, 2)
                        ShowItemDesc = True
                        Exit Sub
                    Else
                        ShowItemDesc = False
                        LastItemDesc = 0 ' no item was last loaded
                    End If
                End If

            End If
        End If

        'shop
        If PnlShopVisible = True Then
            If AboveShoppanel(x, y) Then
                shopslot = IsShopItem(x, y)

                If shopslot <> 0 Then

                    UpdateDescWindow(Shop(InShop).TradeItem(shopslot).Item, Shop(InShop).TradeItem(shopslot).ItemValue, shopslot, 3)
                    LastItemDesc = Shop(InShop).TradeItem(shopslot).Item
                    ShowItemDesc = True
                    Exit Sub
                Else
                    ShowItemDesc = False
                    LastItemDesc = 0
                End If

            End If
        End If

        'trade
        If PnlTradeVisible = True Then
            If AboveTradepanel(x, y) Then
                TradeX = x
                TradeY = y

                'ours
                tradeNum = IsTradeItem(x, y, True)

                If tradeNum <> 0 Then
                    UpdateDescWindow(GetPlayerInvItemNum(Myindex, TradeYourOffer(tradeNum).Num), TradeYourOffer(tradeNum).Value, tradeNum, 4)
                    LastItemDesc = GetPlayerInvItemNum(Myindex, TradeYourOffer(tradeNum).Num) ' set it so you don't re-set values
                    ShowItemDesc = True
                    Exit Sub
                Else
                    ShowItemDesc = False
                    LastItemDesc = 0
                End If

                'theirs
                tradeNum = IsTradeItem(x, y, False)

                If tradeNum <> 0 Then
                    UpdateDescWindow(TradeTheirOffer(tradeNum).Num, TradeTheirOffer(tradeNum).Value, tradeNum, 4)
                    LastItemDesc = TradeTheirOffer(tradeNum).Num ' set it so you don't re-set values
                    ShowItemDesc = True
                    Exit Sub
                Else
                    ShowItemDesc = False
                    LastItemDesc = 0
                End If
            End If
        End If

    End Sub

#Region "Support Functions"

    Function IsEqItem(x As Single, y As Single) As Integer
        Dim tempRec As RectStruct
        Dim i As Integer
        IsEqItem = 0

        For i = 1 To EquipmentType.Count - 1

            If GetPlayerEquipment(Myindex, i) > 0 AndAlso GetPlayerEquipment(Myindex, i) <= MAX_ITEMS Then

                With tempRec
                    .Top = CharWindowY + EqTop + ((EqOffsetY + 32) * ((i - 1) \ EqColumns))
                    .Bottom = .Top + PicY
                    .Left = CharWindowX + EqLeft + ((EqOffsetX + 32) * (((i - 1) Mod EqColumns)))
                    .Right = .Left + PicX
                End With

                If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                    If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                        IsEqItem = i
                        Exit Function
                    End If
                End If
            End If

        Next

    End Function

    Function IsInvItem(x As Single, y As Single) As Integer
        Dim tempRec As RectStruct
        Dim i As Integer

       For i = 1 To MAX_INV

            If GetPlayerInvItemNum(Myindex, i) > 0 AndAlso GetPlayerInvItemNum(Myindex, i) <= MAX_ITEMS Then

                With tempRec
                    .Top = InvWindowY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                    .Bottom = .Top + PicY
                    .Left = InvWindowX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                    .Right = .Left + PicX
                End With

                If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                    If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                        IsInvItem = i
                        Exit Function
                    End If
                End If
            End If

        Next

    End Function

    Function IsPlayerSkill(x As Single, y As Single) As Integer
        Dim tempRec As RectStruct
        Dim i As Integer

       For i = 1 To MAX_PLAYER_SKILLS

            If Player(Myindex).Skill(i).Num > 0 AndAlso Player(Myindex).Skill(i).Num <= MAX_PLAYER_SKILLS Then

                With tempRec
                    .Top = SkillWindowY + SkillTop + ((SkillOffsetY + 32) * ((i - 1) \ SkillColumns))
                    .Bottom = .Top + PicY
                    .Left = SkillWindowX + SkillLeft + ((SkillOffsetX + 32) * (((i - 1) Mod SkillColumns)))
                    .Right = .Left + PicX
                End With

                If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                    If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                        IsPlayerSkill = i
                        Exit Function
                    End If
                End If
            End If

        Next

    End Function

    Function IsBankItem(x As Single, y As Single) As Integer
        Dim tempRec As RectStruct
        Dim i As Integer

       For i = 1 To MAX_BANK
            If GetBankItemNum(i) > 0 AndAlso GetBankItemNum(i) <= MAX_ITEMS Then

                With tempRec
                    .Top = BankWindowY + BankTop + ((BankOffsetY + 32) * ((i - 1) \ BankColumns))
                    .Bottom = .Top + PicY
                    .Left = BankWindowX + BankLeft + ((BankOffsetX + 32) * (((i - 1) Mod BankColumns)))
                    .Right = .Left + PicX
                End With

                If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                    If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then

                        IsBankItem = i
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Function IsShopItem(x As Single, y As Single) As Integer
        Dim tempRec As Rectangle
        Dim i As Integer

        If InShop <= 0 Or InShop > MAX_SHOPS Then Exit Function

        For i = 1 To MAX_TRADES

            If Shop(InShop).TradeItem(i).Item > 0 AndAlso Shop(InShop).TradeItem(i).Item <= MAX_ITEMS Then
                With tempRec
                    .Y = ShopWindowY + ShopTop + ((ShopOffsetY + 32) * ((i - 1) \ ShopColumns))
                    .Height = PicY
                    .X = ShopWindowX + ShopLeft + ((ShopOffsetX + 32) * (((i - 1) Mod ShopColumns)))
                    .Width = PicX
                End With

                If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                    If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                        IsShopItem = i
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Function IsTradeItem(x As Single, y As Single, yours As Boolean) As Integer
        Dim tempRec As RectStruct
        Dim i As Integer
        Dim itemnum As Integer

       For i = 1 To MAX_INV

            If yours Then
                itemnum = GetPlayerInvItemNum(Myindex, TradeYourOffer(i).Num)

                With tempRec
                    .Top = TradeWindowY + OurTradeY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                    .Bottom = .Top + PicY
                    .Left = TradeWindowX + OurTradeX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                    .Right = .Left + PicX
                End With
            Else
                itemnum = TradeTheirOffer(i).Num

                With tempRec
                    .Top = TradeWindowY + TheirTradeY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                    .Bottom = .Top + PicY
                    .Left = TradeWindowX + TheirTradeX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                    .Right = .Left + PicX
                End With
            End If

            If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then

                If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                    If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                        IsTradeItem = i
                        Exit Function
                    End If
                End If

            End If

        Next

    End Function

    Function IsDescWindowActive(x As Integer, y As Integer) As Boolean
        If IsInvItem(x, y) > 0 Or IsShopItem(x, y) > 0 Or IsBankItem(x,y) Or IsPlayerSkill(x,y) > 0 Or IsEqItem(x,y) > 0 Or IsTradeItem(x,y,false) > 0 Or IsTradeItem(x,y,true) > 0 Then
            Return True
        End If
    End Function

    Function AboveActionPanel(x As Single, y As Single) As Boolean
        AboveActionPanel = False

        If x > ActionPanelX AndAlso x < ActionPanelX + ActionPanelGfxInfo.Width Then
            If y > ActionPanelY AndAlso y < ActionPanelY + ActionPanelGfxInfo.Height Then
                AboveActionPanel = True
            End If
        End If
    End Function

    Function AboveHotbar(x As Single, y As Single) As Boolean
        AboveHotbar = False

        If x > HotbarX AndAlso x < HotbarX + HotBarGfxInfo.Width Then
            If y > HotbarY AndAlso y < HotbarY + HotBarGfxInfo.Height Then
                AboveHotbar = True
            End If
        End If
    End Function

    Function AbovePetbar(x As Single, y As Single) As Boolean
        AbovePetbar = False

        If x > PetbarX AndAlso x < PetbarX + PetbarGfxInfo.Width Then
            If y > PetbarY AndAlso y < PetbarY + HotBarGfxInfo.Height Then
                AbovePetbar = True
            End If
        End If
    End Function

    Function AboveInvpanel(x As Single, y As Single) As Boolean
        AboveInvpanel = False

        If x > InvWindowX AndAlso x < InvWindowX + InvPanelGfxInfo.Width Then
            If y > InvWindowY AndAlso y < InvWindowY + InvPanelGfxInfo.Height Then
                AboveInvpanel = True
            End If
        End If
    End Function

    Function AboveCharpanel(x As Single, y As Single) As Boolean
        AboveCharpanel = False

        If x > CharWindowX AndAlso x < CharWindowX + CharPanelGfxInfo.Width Then
            If y > CharWindowY AndAlso y < CharWindowY + CharPanelGfxInfo.Height Then
                AboveCharpanel = True
            End If
        End If
    End Function

    Function AboveSkillpanel(x As Single, y As Single) As Boolean
        AboveSkillpanel = False

        If x > SkillWindowX AndAlso x < SkillWindowX + SkillPanelGfxInfo.Width Then
            If y > SkillWindowY AndAlso y < SkillWindowY + SkillPanelGfxInfo.Height Then
                AboveSkillpanel = True
            End If
        End If
    End Function

    Function AboveBankpanel(x As Single, y As Single) As Boolean
        AboveBankpanel = False

        If x > BankWindowX AndAlso x < BankWindowX + BankPanelGfxInfo.Width Then
            If y > BankWindowY AndAlso y < BankWindowY + BankPanelGfxInfo.Height Then
                AboveBankpanel = True
            End If
        End If
    End Function

    Function AboveShoppanel(x As Single, y As Single) As Boolean
        AboveShoppanel = False

        If x > ShopWindowX AndAlso x < ShopWindowX + ShopPanelGfxInfo.Width Then
            If y > ShopWindowY AndAlso y < ShopWindowY + ShopPanelGfxInfo.Height Then
                AboveShoppanel = True
            End If
        End If
    End Function

    Function AboveTradepanel(x As Single, y As Single) As Boolean
        AboveTradepanel = False

        If x > TradeWindowX AndAlso x < TradeWindowX + TradePanelGfxInfo.Width Then
            If y > TradeWindowY AndAlso y < TradeWindowY + TradePanelGfxInfo.Height Then
                AboveTradepanel = True
            End If
        End If
    End Function

    Function AboveEventChat(x As Single, y As Single) As Boolean
        AboveEventChat = False

        If x > EventChatX AndAlso x < EventChatX + EventChatGfxInfo.Width Then
            If y > EventChatY AndAlso y < EventChatY + EventChatGfxInfo.Height Then
                AboveEventChat = True
            End If
        End If
    End Function

    Function AboveChatScrollUp(x As Single, y As Single) As Boolean
        AboveChatScrollUp = False

        If x > ChatWindowX + ChatWindowGfxInfo.Width - 24 AndAlso x < ChatWindowX + ChatWindowGfxInfo.Width Then
            If y > ChatWindowY AndAlso y < ChatWindowY + 56 Then 'ChatWindowGFXInfo.height Then
                AboveChatScrollUp = True
            End If
        End If
    End Function

    Function AboveChatScrollDown(x As Single, y As Single) As Boolean
        AboveChatScrollDown = False

        If x > ChatWindowX + ChatWindowGfxInfo.Width - 24 AndAlso x < ChatWindowX + ChatWindowGfxInfo.Width Then
            If y > ChatWindowY + ChatWindowGfxInfo.Height - 72 AndAlso y < ChatWindowY + ChatWindowGfxInfo.Height Then
                AboveChatScrollDown = True
            End If
        End If
    End Function

    Function AboveRClickPanel(x As Single, y As Single) As Boolean
        AboveRClickPanel = False

        If x > RClickX AndAlso x < RClickX + RClickGfxInfo.Width Then
            If y > RClickY AndAlso y < RClickY + RClickGfxInfo.Height Then
                AboveRClickPanel = True
            End If
        End If
    End Function

    Function AboveCraftPanel(x As Single, y As Single) As Boolean
        AboveCraftPanel = False

        If x > CraftPanelX AndAlso x < CraftPanelX + CraftGfxInfo.Width Then
            If y > CraftPanelY AndAlso y < CraftPanelY + CraftGfxInfo.Height Then
                AboveCraftPanel = True
            End If
        End If
    End Function

    Friend Function IsHotBarSlot(x As Single, y As Single) As Integer
        Dim tempRec As RectStruct
        Dim i As Integer

        For i = 1 To MAX_HOTBAR
            With tempRec
                .Top = HotbarY + HotbarTop
                .Bottom = .Top + PicY
                .Left = HotbarX + HotbarLeft + ((HotbarOffsetX + 32) * (((i - 1) Mod MAX_HOTBAR)))
                .Right = .Left + PicX
            End With

            If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                    IsHotBarSlot = i
                    Exit Function
                End If
            End If
        Next

    End Function

    Friend Sub RePositionGui(width As Integer, height As Integer)        
        ScreenMapx = (width / 32) - 1
        ScreenMapy = (height / 32) - 1

        'then the window
        FrmGame.ClientSize = New Drawing.Size((ScreenMapx) * PicX + PicX, (ScreenMapy) * PicY + PicY)
        FrmGame.picscreen.Width = (ScreenMapx) * PicX + PicX
        FrmGame.picscreen.Height = (ScreenMapy) * PicY + PicY

        HalfX = ((ScreenMapx) \ 2) * PicX
        HalfY = ((ScreenMapy) \ 2) * PicY
        ScreenX = (ScreenMapx) * PicX
        ScreenY = (ScreenMapy) * PicY

        GameWindow.SetView(New SFML.Graphics.View(New SFML.Graphics.FloatRect(0, 0, (ScreenMapx) * PicX + PicX, (ScreenMapy) * PicY + PicY)))

        'Then we can recalculate the positions

        'chatwindow
        ChatWindowX = 1
        ChatWindowY = FrmGame.Height - ChatWindowGfxInfo.Height - 65

        MyChatX = 24
        MyChatY = FrmGame.Height - 60

        'hotbar
        'If Settings.ScreenSize = 0 Then
        HotbarX = HudWindowX + HudPanelGfxInfo.Width + 20
        HotbarY = 5

        'petbar
        PetbarX = HotbarX
        PetbarY = HotbarY + 34
        'Else
        '    HotbarX = ChatWindowX + MyChatWindowGfxInfo.Width + 75
        '    HotbarY = FrmGame.Height - HotBarGfxInfo.Height - 45

        'petbar
        '    PetbarX = HotbarX
        '    PetbarY = HotbarY - 34
        'End If

        'action panel
        ActionPanelX = FrmGame.Width - ActionPanelGfxInfo.Width - 25
        ActionPanelY = FrmGame.Height - ActionPanelGfxInfo.Height - 45

        'Char Window
        CharWindowX = FrmGame.Width - CharPanelGfxInfo.Width - 26
        CharWindowY = FrmGame.Height - CharPanelGfxInfo.Height - ActionPanelGfxInfo.Height - 50

        'inv Window
        InvWindowX = FrmGame.Width - InvPanelGfxInfo.Width - 26
        InvWindowY = FrmGame.Height - InvPanelGfxInfo.Height - ActionPanelGfxInfo.Height - 50

        'skill window
        SkillWindowX = FrmGame.Width - SkillPanelGfxInfo.Width - 26
        SkillWindowY = FrmGame.Height - SkillPanelGfxInfo.Height - ActionPanelGfxInfo.Height - 50

        'petstat window
        PetStatX = PetbarX
        PetStatY = PetbarY - PetStatsGfxInfo.Height - 10
    End Sub

#End Region

End Module