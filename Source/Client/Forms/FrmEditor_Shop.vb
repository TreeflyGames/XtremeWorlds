﻿Imports System.IO
Imports System.Windows.Forms
Imports Core

Friend Class frmEditor_Shop
    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        tmpindex = lstIndex.SelectedIndex
        Type.Shop(GameState.EditorIndex).Name = txtName.Text
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Shop(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub ScrlBuy_Scroll(sender As Object, e As EventArgs) Handles nudBuy.ValueChanged
        Type.Shop(GameState.EditorIndex).BuyRate = nudBuy.Value
    End Sub

    Private Sub BtnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim index As Integer

        index = lstTradeItem.SelectedIndex + 1

        With Type.Shop(GameState.EditorIndex).TradeItem(index)
            .Item = cmbItem.SelectedIndex + 1
            .ItemValue = nudItemValue.Value
            .CostItem = cmbCostItem.SelectedIndex + 1
            .CostValue = nudCostValue.Value
        End With
        Call UpdateShopTrade()
    End Sub

    Private Sub BtnDeleteTrade_Click(sender As Object, e As EventArgs) Handles btnDeleteTrade.Click
        Dim index As Integer

        index = lstTradeItem.SelectedIndex + 1
        With Type.Shop(GameState.EditorIndex).TradeItem(index)
            .Item = 0
            .ItemValue = 0
            .CostItem = 0
            .CostValue = 0
        End With
        Call UpdateShopTrade()
    End Sub

    Private Sub lstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        ShopEditorInit()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ShopEditorOk()
        Dispose()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ShopEditorCancel()
        Dispose()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        ClearShop(GameState.EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Shop(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex

        ShopEditorInit()
    End Sub

    Private Sub frmEditor_Shop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstIndex.Items.Clear()

        ' Add the names
        For i = 1 To MAX_SHOPS
            lstIndex.Items.Add(i & ": " & Type.Shop(i).Name)
        Next

        cmbItem.Items.Clear()
        cmbCostItem.Items.Clear()
        cmbItem.Items.Add("None")
        cmbCostItem.Items.Add("None")

        For i = 1 To MAX_ITEMS
            cmbItem.Items.Add(i & ": " & Type.Item(i).Name)
            cmbCostItem.Items.Add(i & ": " & Type.Item(i).Name)
        Next

    End Sub

    Private Sub frmEditor_Shop_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ShopEditorCancel
    End Sub
End Class