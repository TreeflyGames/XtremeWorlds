Imports System.Drawing
Imports System.Windows.Forms
Imports Mirage.Sharp.Asfw
Imports Core

Friend Module C_Gui

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
        If IsInvItem(x, y) > 0 Or IsShopItem(x, y) > 0 Or IsBankItem(x, y) Or IsPlayerSkill(x, y) > 0 Or IsEqItem(x, y) > 0 Or IsTradeItem(x, y, False) > 0 Or IsTradeItem(x, y, True) > 0 Then
            Return True
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

#End Region

End Module