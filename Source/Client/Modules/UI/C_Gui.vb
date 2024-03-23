Imports System.Drawing
Imports System.Windows.Forms
Imports Mirage.Sharp.Asfw
Imports Core

Friend Module C_Gui

#Region "Support Functions"

   Public Function IsEqItem(StartX As Long, StartY As Long) As Long
        Dim tempRec As RectStruct
        Dim i As Long

        For i = 1 To EquipmentType.Count - 1
            If GetPlayerEquipment(MyIndex, i) Then
                With tempRec
                .Top = StartY + EqTop + (32 * ((i - 1) \ EqColumns))
                .bottom = .Top + PicY
                .Left = StartX + EqLeft + ((EqOffsetX + 32) * (((i - 1) Mod EqColumns)))
                .Right = .Left + PicX
                End With

                If CurMouseX >= tempRec.Left And CurMouseX <= tempRec.Right Then
                    If CurMouseY >= tempRec.Top And CurMouseY <= tempRec.bottom Then
                        IsEqItem = i
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Public Function IsInv(StartX As Long, StartY As Long) As Long
        Dim tempRec As RectStruct
        Dim i As Long

        For i = 1 To MAX_INV
            If GetPlayerInvItemNum(MyIndex, i) Then
                With tempRec
                    .Top = StartY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                    .bottom = .Top + PicY
                    .Left = StartX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                    .Right = .Left + PicX
                End With

                If CurMouseX >= tempRec.Left And CurMouseX <= tempRec.Right Then
                    If CurMouseY >= tempRec.Top And CurMouseY <= tempRec.bottom Then
                        IsInv = i
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Public Function IsSkill(StartX As Long, StartY As Long) As Long
        Dim tempRec As RectStruct
        Dim i As Long

        For i = 1 To MAX_PLAYER_SKILLS
            If Player(Myindex).Skill(i).Num Then
                With tempRec
                    .Top = StartY + SkillTop + ((SkillOffsetY + 32) * ((i - 1) \ SkillColumns))
                    .bottom = .Top + PicY
                    .Left = StartX + SkillLeft + ((SkillOffsetX + 32) * (((i - 1) Mod SkillColumns)))
                    .Right = .Left + PicX
                End With

                If CurMouseX >= tempRec.Left And CurMouseX <= tempRec.Right Then
                    If CurMouseY >= tempRec.Top And CurMouseY <= tempRec.bottom Then
                        IsSkill = i
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Public Function IsBankItem(StartX As Long, StartY As Long) As Long
        Dim tempRec As RectStruct
        Dim i As Long

        For i = 1 To MAX_BANK
            If Bank.Item(i).num > 0 Then
                With tempRec
                    .Top = StartY + BankTop + ((BankOffsetY + 32) * ((i - 1) \ BankColumns))
                    .bottom = .Top + PicY
                    .Left = StartX + BankLeft + ((BankOffsetX + 32) * (((i - 1) Mod BankColumns)))
                    .Right = .Left + PicX
                End With

                If CurMouseX >= tempRec.Left And CurMouseX <= tempRec.Right Then
                    If CurMouseY >= tempRec.Top And CurMouseY <= tempRec.bottom Then
                        IsBankItem = i
                        Exit Function
                    End If
                End If
            End If
        
        Next

    End Function

  Public Function IsShop(StartX As Long, StartY As Long) As Long
        Dim tempRec As RectStruct
        Dim i As Long

        For i = 1 To MAX_TRADES
            With tempRec
                .Top = StartY + ShopTop + ((ShopOffsetY + 32) * ((i - 1) \ ShopColumns))
                .bottom = .Top + PicY
                .Left = StartX + ShopLeft + ((ShopOffsetX + 32) * (((i - 1) Mod ShopColumns)))
                .Right = .Left + PicX
            End With

            If CurMouseX >= tempRec.Left And CurMouseX <= tempRec.Right Then
                If CurMouseY >= tempRec.Top And CurMouseY <= tempRec.bottom Then
                    IsShop = i
                    Exit Function
                End If
            End If
        Next
    End Function

    Public Function IsTrade(StartX As Long, StartY As Long) As Long
        Dim tempRec As RectStruct
        Dim i As Long

        For i = 1 To MAX_INV
            With tempRec
                .Top = StartY + TradeTop + ((TradeOffsetY + 32) * ((i - 1) \ TradeColumns))
                .bottom = .Top + PicY
                .Left = StartX + TradeLeft + ((TradeOffsetX + 32) * (((i - 1) Mod TradeColumns)))
                .Right = .Left + PicX
            End With

            If CurMouseX >= tempRec.Left And CurMouseX <= tempRec.Right Then
                If CurMouseY >= tempRec.Top And CurMouseY <= tempRec.bottom Then
                    IsTrade = i
                    Exit Function
                End If
            End If
        Next
    End Function

#End Region

End Module