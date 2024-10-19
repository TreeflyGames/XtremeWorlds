Imports System.IO
Imports Mirage.Sharp.Asfw
Imports Core
Imports Core.Enum
Imports FxResources.System
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports SharpDX.Direct2D1

Public Class frmEditor_Map
#Region "Frm"
    Private Sub frmEditor_Map_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        pnlAttributes.BringToFront()
        pnlAttributes.Visible = False
        pnlAttributes.Left = 4
        pnlAttributes.Top = 28
        optBlocked.Checked = True
        tabpages.SelectedIndex = 0

        DirArrowX(DirectionType.Up) = 12
        DirArrowY(DirectionType.Up) = 0
        DirArrowX(DirectionType.Down) = 12
        DirArrowY(DirectionType.Down) = 23
        DirArrowX(DirectionType.Left) = 0
        DirArrowY(DirectionType.Left) = 12
        DirArrowX(DirectionType.Right) = 23
        DirArrowY(DirectionType.Right) = 12

        scrlFog.Maximum = NumFogs

        TopMost = True
    End Sub

#End Region

#Region "Toolbar"

    Private Sub TsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        Dim X As Integer, x2 As Integer
        Dim Y As Integer, y2 As Integer
        Dim tempArr(,) As TileStruct

        If Not IsNumeric(txtMaxX.Text) Then txtMaxX.Text = MyMap.MaxX
        If Val(txtMaxX.Text) < Type.Setting.CameraWidth Then txtMaxX.Text = Type.Setting.CameraWidth
        If Val(txtMaxX.Text) > Byte.MaxValue Then txtMaxX.Text = Byte.MaxValue
        If Not IsNumeric(txtMaxY.Text) Then txtMaxY.Text = MyMap.MaxY
        If Val(txtMaxY.Text) < Type.Setting.CameraHeight Then txtMaxY.Text = Type.Setting.CameraHeight
        If Val(txtMaxY.Text) > Byte.MaxValue Then txtMaxY.Text = Byte.MaxValue

        With MyMap
            .Name = txtName.Text
            If lstMusic.SelectedIndex >= 0 Then
                .Music = lstMusic.Items(lstMusic.SelectedIndex).ToString
            Else
                .Music = ""
            End If

            If lstShop.SelectedIndex >= 0 Then
                .Shop = lstShop.SelectedIndex
            Else
                .Shop = 0
            End If

            .Up = Val(txtUp.Text)
            .Down = Val(txtDown.Text)
            .Left = Val(txtLeft.Text)
            .Right = Val(txtRight.Text)
            .Moral = lstMoral.SelectedIndex
            .BootMap = Val(txtBootMap.Text)
            .BootX = Val(txtBootX.Text)
            .BootY = Val(txtBootY.Text)

            ' set the data before changing it
            tempArr = MyMap.Tile.Clone

            x2 = MyMap.MaxX
            y2 = MyMap.MaxY

            ' change the data
            .MaxX = Val(txtMaxX.Text)
            .MaxY = Val(txtMaxY.Text)

            ReDim MyMap.Tile(.MaxX, .MaxY)
            ReDim Type.Autotile(.MaxX, .MaxY)

            For i = 0 To MaxTileHistory
                ReDim TileHistory(i).Tile(.MaxX, .MaxY)
            Next

            If x2 > .MaxX Then x2 = .MaxX
            If y2 > .MaxY Then y2 = .MaxY

            For X = 0 To .MaxX
                For Y = 0 To .MaxY
                    ReDim .Tile(X, Y).Layer(LayerType.Count - 1)
                    ReDim Type.Autotile(X, Y).Layer(LayerType.Count - 1)

                    For i = 0 To MaxTileHistory
                        ReDim TileHistory(i).Tile(X, Y).Layer(LayerType.Count - 1)
                    Next

                    If X <= x2 Then
                        If Y <= y2 Then
                            .Tile(X, Y) = tempArr(X, Y)
                        End If
                    End If
                Next
            Next
        End With

        MapEditorSend()
        GettingMap = True
        Dispose()
    End Sub

    Private Sub TsbFill_Click(sender As Object, e As EventArgs) Handles tsbFill.Click
        Dim layer = CType(cmbLayers.SelectedIndex + 1, LayerType)
        MapEditorFillLayer(layer, cmbAutoTile.SelectedIndex + 1, EditorTileX, EditorTileY)
    End Sub

    Private Sub TsbClear_Click(sender As Object, e As EventArgs) Handles tsbClear.Click
        Dim layer = CType(cmbLayers.SelectedIndex + 1, LayerType)
        MapEditorClearLayer(layer)
    End Sub

    Private Sub TsbEyeDropper_Click(sender As Object, e As EventArgs) Handles tsbEyeDropper.Click
        EyeDropper = Not EyeDropper
    End Sub

    Private Sub TsbDiscard_Click(sender As Object, e As EventArgs) Handles tsbDiscard.Click
        MapEditorCancel()
        Dispose()
    End Sub

    Private Sub TsbMapGrid_Click(sender As Object, e As EventArgs) Handles tsbMapGrid.Click
        MapGrid = Not MapGrid
    End Sub

#End Region

#Region "Tiles"
    Private Sub PicBackSelect_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picBackSelect.MouseDown
        MapEditorChooseTile(e.Button, e.X, e.Y)
    End Sub

    Private Sub PicBackSelect_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picBackSelect.MouseMove
        MapEditorDrag(e.Button, e.X, e.Y)
    End Sub

    Private Sub CmbTileSets_Click(sender As Object, e As EventArgs) Handles cmbTileSets.Click
        If cmbTileSets.SelectedIndex > NumTileSets Then
            cmbTileSets.SelectedIndex = 0
        End If

        MyMap.Tileset = cmbTileSets.SelectedIndex + 1

        EditorTileSelStart = New Point(0, 0)
        EditorTileSelEnd = New Point(1, 1)
    End Sub

    Private Sub CmbAutoTile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAutoTile.SelectedIndexChanged
        If cmbAutoTile.SelectedIndex = 0 Then
            EditorTileWidth = 1
            EditorTileHeight = 1
        End If
    End Sub

#End Region

#Region "Attributes"

    Private Sub ScrlMapWarpMap_Scroll(ByVal sender As Object, ByVal e As EventArgs) Handles scrlMapWarpMap.ValueChanged
        lblMapWarpMap.Text = "Map: " & scrlMapWarpMap.Value
    End Sub

    Private Sub ScrlMapWarpX_Scroll(ByVal sender As Object, ByVal e As EventArgs) Handles scrlMapWarpX.ValueChanged
        lblMapWarpX.Text = "X: " & scrlMapWarpX.Value
    End Sub

    Private Sub ScrlMapWarpY_Scroll(ByVal sender As Object, ByVal e As EventArgs) Handles scrlMapWarpY.ValueChanged
        lblMapWarpY.Text = "Y: " & scrlMapWarpY.Value
    End Sub

    Private Sub BtnMapWarp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMapWarp.Click
        EditorWarpMap = scrlMapWarpMap.Value

        EditorWarpX = scrlMapWarpX.Value
        EditorWarpY = scrlMapWarpY.Value
        pnlAttributes.Visible = False
        fraMapWarp.Visible = False
    End Sub

    Private Sub OptWarp_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optWarp.CheckedChanged
        If optWarp.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraMapWarp.Visible = True

        scrlMapWarpMap.Maximum = MAX_MAPS
        scrlMapWarpMap.Value = 1
        scrlMapWarpX.Maximum = Byte.MaxValue
        scrlMapWarpY.Maximum = Byte.MaxValue
        scrlMapWarpX.Value = 0
        scrlMapWarpY.Value = 0
    End Sub

    Private Sub ScrlMapItem_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles scrlMapItem.ValueChanged
        If Type.Item(scrlMapItem.Value).Type = ItemType.Currency Or Type.Item(scrlMapItem.Value).Stackable = 1 Then
            scrlMapItemValue.Enabled = True
        Else
            scrlMapItemValue.Value = 1
            scrlMapItemValue.Enabled = False
        End If

        EditorMap_DrawItem()
        lblMapItem.Text = scrlMapItem.Value & ". " & Type.Item(scrlMapItem.Value).Name & " x" & scrlMapItemValue.Value
    End Sub

    Private Sub ScrlMapItemValue_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles scrlMapItemValue.ValueChanged
        lblMapItem.Text = Type.Item(scrlMapItem.Value).Name & " x" & scrlMapItemValue.Value
    End Sub

    Private Sub BtnMapItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMapItem.Click
        ItemEditorNum = scrlMapItem.Value
        ItemEditorValue = scrlMapItemValue.Value
        pnlAttributes.Visible = False
        fraMapItem.Visible = False
    End Sub

    Private Sub OptItem_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optItem.CheckedChanged
        If optItem.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraMapItem.Visible = True

        scrlMapItem.Maximum = MAX_ITEMS
        scrlMapItem.Value = 1
        lblMapItem.Text = Type.Item(scrlMapItem.Value).Name & " x" & scrlMapItemValue.Value
        EditorMap_DrawItem()
    End Sub

    Private Sub BtnResourceOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResourceOk.Click
        ResourceEditorNum = scrlResource.Value
        pnlAttributes.Visible = False
        fraResource.Visible = False
    End Sub

    Private Sub ScrlResource_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles scrlResource.ValueChanged
        lblResource.Text = "Resource: " & Type.Resource(scrlResource.Value).Name
    End Sub

    Private Sub OptResource_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optResource.CheckedChanged
        If optResource.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraResource.Visible = True
    End Sub

    Private Sub BtnNpcSpawn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNpcSpawn.Click
        SpawnNpcNum = lstNpc.SelectedIndex + 1
        SpawnNpcDir = scrlNpcDir.Value
        pnlAttributes.Visible = False
        fraNpcSpawn.Visible = False
    End Sub

    Private Sub OptNPCSpawn_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optNPCSpawn.CheckedChanged
        Dim n As Integer

        If optNPCSpawn.Checked = False Then Exit Sub

        lstNpc.Items.Clear()

        For n = 1 To MAX_MAP_NPCS
            If MyMap.NPC(n) > 0 Then
                lstNpc.Items.Add(n & ": " & Type.NPC(MyMap.NPC(n)).Name)
            Else
                lstNpc.Items.Add(n)
            End If
        Next n

        scrlNpcDir.Value = 0
        lstNpc.SelectedIndex = 0

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraNpcSpawn.Visible = True
    End Sub

    Private Sub BtnShop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShop.Click
        EditorShop = cmbShop.SelectedIndex + 1
        pnlAttributes.Visible = False
        fraShop.Visible = False
    End Sub

    Private Sub OptShop_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optShop.CheckedChanged
        If optShop.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraShop.Visible = True
    End Sub

    Private Sub BtnHeal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHeal.Click
        MapEditorHealType = cmbHeal.SelectedIndex
        MapEditorHealAmount = scrlHeal.Value
        pnlAttributes.Visible = False
        fraHeal.Visible = False
    End Sub

    Private Sub ScrlHeal_Scroll(ByVal sender As Object, ByVal e As EventArgs) Handles scrlHeal.ValueChanged
        lblHeal.Text = "Amount: " & scrlHeal.Value
    End Sub

    Private Sub OptHeal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optHeal.CheckedChanged
        If optHeal.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraHeal.Visible = True
    End Sub

    Private Sub ScrlTrap_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles scrlTrap.ValueChanged
        lblTrap.Text = "Amount: " & scrlTrap.Value
    End Sub

    Private Sub BtnTrap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTrap.Click
        MapEditorHealAmount = scrlTrap.Value
        pnlAttributes.Visible = False
        fraTrap.Visible = False
    End Sub

    Private Sub OptTrap_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles optTrap.CheckedChanged
        If optTrap.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraTrap.Visible = True
    End Sub

    Private Sub BtnClearAttribute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearAttribute.Click
        Dialogue("Map Editor", "Clear Attributes: ", "Are you sure you wish to clear attributes?", DialogueType.ClearAttributes, DialogueStyle.YesNo)
    End Sub

    Private Sub ScrlNpcDir_Scroll(sender As Object, e As EventArgs) Handles scrlNpcDir.ValueChanged
        Select Case scrlNpcDir.Value
            Case 0
                lblNpcDir.Text = "Direction: Up"
            Case 1
                lblNpcDir.Text = "Direction: Down"
            Case 2
                lblNpcDir.Text = "Direction: Left"
            Case 3
                lblNpcDir.Text = "Direction: Right"
        End Select
    End Sub

    Private Sub OptBlocked_CheckedChanged(sender As Object, e As EventArgs) Handles optBlocked.CheckedChanged
        If optBlocked.Checked Then pnlAttributes.Visible = False
    End Sub
#End Region

#Region "Npc's"

    Private Sub CmbNpcList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNpcList.SelectedIndexChanged
        If lstMapNPC.SelectedIndex > 0 Then
            lstMapNPC.Items.Item(lstMapNPC.SelectedIndex) = lstMapNPC.SelectedIndex & ": " & Type.NPC(cmbNpcList.SelectedIndex).Name
            MyMap.NPC(lstMapNPC.SelectedIndex) = cmbNpcList.SelectedIndex
        End If
    End Sub

#End Region

#Region "Setting"

    Private Sub BtnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If lstMusic.SelectedIndex > 0 Then
            Dim selectedFile As String = lstMusic.Items(lstMusic.SelectedIndex).ToString()

            ' If the selected music file is a MIDI file
            If Type.Setting.MusicExt = ".mid" Then
                PlayMidi(Core.Path.Music & selectedFile)
            Else
                PlayMusic(selectedFile)
            End If
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub BtnCopyEvent_Click(sender As Object, e As EventArgs) Handles btnCopyEvent.Click
        If EventCopy = False Then
            EventCopy = True
            lblCopyMode.Text = "CopyMode On"
            EventPaste = False
            lblPasteMode.Text = "PasteMode Off"
        Else
            EventCopy = False
            lblCopyMode.Text = "CopyMode Off"
        End If
    End Sub

    Private Sub BtnPasteEvent_Click(sender As Object, e As EventArgs) Handles btnPasteEvent.Click
        If EventPaste = False Then
            EventPaste = True
            lblPasteMode.Text = "PasteMode On"
            EventCopy = False
            lblCopyMode.Text = "CopyMode Off"
        Else
            EventPaste = False
            lblPasteMode.Text = "PasteMode Off"
        End If
    End Sub

#End Region

#Region "Map Effects"

    Private Sub CmbWeather_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbWeather.SelectedIndexChanged
        MyMap.Weather = cmbWeather.SelectedIndex
    End Sub

    Private Sub ScrlFog_Scroll(sender As Object, e As EventArgs) Handles scrlFog.ValueChanged
        MyMap.Fog = scrlFog.Value
        lblFogIndex.Text = "Fog: " & scrlFog.Value
    End Sub

    Private Sub ScrlIntensity_Scroll(sender As Object, e As EventArgs) Handles scrlIntensity.ValueChanged
        MyMap.WeatherIntensity = scrlIntensity.Value
        lblIntensity.Text = "Intensity: " & scrlIntensity.Value
    End Sub

    Private Sub ScrlFogSpeed_Scroll(sender As Object, e As EventArgs) Handles scrlFogSpeed.ValueChanged
        MyMap.FogSpeed = scrlFogSpeed.Value
        lblFogSpeed.Text = "FogSpeed: " & scrlFogSpeed.Value
    End Sub

    Private Sub ScrlFogOpacity_Scroll(sender As Object, e As EventArgs) Handles scrlFogOpacity.ValueChanged
        MyMap.FogOpacity = scrlFogOpacity.Value
        lblFogOpacity.Text = "Fog Alpha: " & scrlFogOpacity.Value
    End Sub

    Private Sub ChkUseTint_CheckedChanged(sender As Object, e As EventArgs) Handles chkTint.CheckedChanged
        If chkTint.Checked = True Then
            MyMap.MapTint = 1
        Else
            MyMap.MapTint = 0
        End If
    End Sub

    Private Sub ScrlMapRed_Scroll(sender As Object, e As EventArgs) Handles scrlMapRed.ValueChanged
        MyMap.MapTintR = scrlMapRed.Value
        lblMapRed.Text = "Red: " & scrlMapRed.Value
    End Sub

    Private Sub ScrlMapGreen_Scroll(sender As Object, e As EventArgs) Handles scrlMapGreen.ValueChanged
        MyMap.MapTintG = scrlMapGreen.Value
        lblMapGreen.Text = "Green: " & scrlMapGreen.Value
    End Sub

    Private Sub ScrlMapBlue_Scroll(sender As Object, e As EventArgs) Handles scrlMapBlue.ValueChanged
        MyMap.MapTintB = scrlMapBlue.Value
        lblMapBlue.Text = "Blue: " & scrlMapBlue.Value
    End Sub

    Private Sub ScrlMapAlpha_Scroll(sender As Object, e As EventArgs) Handles scrlMapAlpha.ValueChanged
        MyMap.MapTintA = scrlMapAlpha.Value
        lblMapAlpha.Text = "Alpha: " & scrlMapAlpha.Value
    End Sub

    Private Sub CmbPanorama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPanorama.SelectedIndexChanged
        MyMap.Panorama = cmbPanorama.SelectedIndex
    End Sub

    Private Sub CmbParallax_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParallax.SelectedIndexChanged
        MyMap.Parallax = cmbParallax.SelectedIndex
    End Sub

#End Region

#Region "Map Editor"

    Public Sub MapPropertiesInit()
        Dim X As Integer, Y As Integer, i As Integer

        txtName.Text = MyMap.Name

        ' find the music we have set
        lstMusic.Items.Clear()
        lstMusic.Items.Add("None")
        lstMusic.SelectedIndex = 0

        CacheMusic()

        For i = 1 To UBound(MusicCache)
            lstMusic.Items.Add(MusicCache(i))
        Next

        For i = 1 To lstMusic.Items.Count - 1
            If lstMusic.Items(i).ToString = MyMap.Music Then
                lstMusic.SelectedIndex = i
                Exit For
            End If
        Next

        ' find the shop we have set
        lstShop.Items.Clear()
        lstShop.Items.Add("None")
        lstShop.SelectedIndex = 0

        For i = 1 To MAX_SHOPS
            lstShop.Items.Add(Type.Shop(i).Name)
        Next

        For i = 1 To lstShop.Items.Count - 1
            If lstShop.Items(i).ToString = Type.Shop(MyMap.Shop).Name Then
                lstShop.SelectedIndex = i
                Exit For
            End If
        Next

        ' find the shop we have set
        lstMoral.Items.Clear()
        lstMoral.Items.Add("None")
        lstMoral.SelectedIndex = 0

        For i = 1 To MAX_MORALS
            lstMoral.Items.Add(Type.Moral(i).Name)
        Next

        For i = 1 To lstMoral.Items.Count - 1
            If lstMoral.Items(i).ToString = Type.Moral(MyMap.Moral).Name Then
                lstMoral.SelectedIndex = i
                Exit For
            End If
        Next

        chkTint.Checked = MyMap.MapTint
        chkNoMapRespawn.Checked = MyMap.NoRespawn
        chkIndoors.Checked = MyMap.Indoors

        ' rest of it
        txtUp.Text = MyMap.Up
        txtDown.Text = MyMap.Down
        txtLeft.Text = MyMap.Left
        txtRight.Text = MyMap.Right

        txtBootMap.Text = MyMap.BootMap
        txtBootX.Text = MyMap.BootX
        txtBootY.Text = MyMap.BootY

        lstMapNPC.Items.Clear()
        lstMapNPC.Items.Add("None")
        lstMapNPC.SelectedIndex = 0

        For X = 1 To MAX_MAP_NPCS
            lstMapNPC.Items.Add(X & ": " & Type.NPC(MyMap.NPC(X)).Name)
        Next

        cmbNpcList.Items.Add("None")
        cmbNpcList.SelectedIndex = 0

        For Y = 1 To MAX_NPCS
            cmbNpcList.Items.Add(Y & ": " & Type.NPC(Y).Name)
        Next

        cmbAnimation.Items.Clear()
        cmbAnimation.Items.Add("None")
        cmbAnimation.SelectedIndex = 0

        For Y = 1 To MAX_ANIMATIONS
            cmbAnimation.Items.Add(Y & ": " & Type.Animation(Y).Name)
        Next

        lblMap.Text = "Map: "
        txtMaxX.Text = MyMap.MaxX
        txtMaxY.Text = MyMap.MaxY

        cmbWeather.SelectedIndex = MyMap.Weather
        scrlFog.Value = MyMap.Fog
        lblFogIndex.Text = "Fog: " & scrlFog.Value
        scrlIntensity.Value = MyMap.WeatherIntensity
        lblIntensity.Text = "Intensity: " & scrlIntensity.Value
        scrlFogOpacity.Value = MyMap.FogOpacity
        scrlFogSpeed.Value = MyMap.FogSpeed

        cmbPanorama.Items.Clear()
        cmbPanorama.Items.Add("None")

        For i = 1 To NumPanoramas
            cmbPanorama.Items.Add(i)
        Next

        cmbPanorama.SelectedIndex = MyMap.Panorama

        cmbParallax.Items.Clear()
        cmbParallax.Items.Add("None")

        For i = 1 To NumParallax
            cmbParallax.Items.Add(i)
        Next

        cmbParallax.SelectedIndex = MyMap.Parallax

        tabpages.SelectedIndex = 0
        scrlMapBrightness.Value = MyMap.Brightness
        chkTint.Checked = MyMap.MapTint
        scrlMapRed.Value = MyMap.MapTintR
        scrlMapGreen.Value = MyMap.MapTintG
        scrlMapBlue.Value = MyMap.MapTintB
        scrlMapAlpha.Value = MyMap.MapTintA

        ' show the form
        Visible = True

    End Sub

    Public Sub MapEditorInit()
        ' set the scrolly bars
        If MyMap.Tileset = 0 Then MyMap.Tileset = 1
        If MyMap.Tileset > NumTileSets Then MyMap.Tileset = 1

        EditorTileSelStart = New Point(0, 0)
        EditorTileSelEnd = New Point(1, 1)

        ' set shops for the shop attribute
        For i = 1 To MAX_SHOPS
            cmbShop.Items.Add(i & ": " & Type.Shop(i).Name)
        Next

        ' we're not in a shop
        cmbShop.SelectedIndex = 0
        cmbAttribute.SelectedIndex = 0

        optBlocked.Checked = True

        cmbTileSets.Items.Clear()
        For i = 1 To NumTileSets
            cmbTileSets.Items.Add(i)
        Next

        cmbTileSets.SelectedIndex = 0
        cmbLayers.SelectedIndex = 0
        cmbAutoTile.SelectedIndex = 0

        MapPropertiesInit()

        If MapData = True Then GettingMap = False
    End Sub

    Public Sub MapEditorChooseTile(ByVal Button As Integer, ByVal X As Single, ByVal Y As Single)
        If Button = MouseButtons.Left Then 'Left Mouse Button
            EditorTileWidth = 1
            EditorTileHeight = 1

            If cmbAutoTile.SelectedIndex > 0 Then
                Select Case cmbAutoTile.SelectedIndex
                    Case 1 ' autotile
                        EditorTileWidth = 2
                        EditorTileHeight = 3
                    Case 2 ' fake autotile
                        EditorTileWidth = 1
                        EditorTileHeight = 1
                    Case 3 ' animated
                        EditorTileWidth = 6
                        EditorTileHeight = 3
                    Case 4 ' cliff
                        EditorTileWidth = 2
                        EditorTileHeight = 2
                    Case 5 ' waterfall
                        EditorTileWidth = 2
                        EditorTileHeight = 3
                End Select
            End If

            EditorTileX = X \ PicX
            EditorTileY = Y \ PicY

            EditorTileSelStart = New Point(EditorTileX, EditorTileY)
            EditorTileSelEnd = New Point(EditorTileX + EditorTileWidth, EditorTileY + EditorTileHeight)

            DrawTileset()
        End If
    End Sub

    Public Sub MapEditorDrag(ByVal Button As Integer, ByVal X As Single, ByVal Y As Single)
        If Button = MouseButtons.Left Then 'Left Mouse Button
            ' convert the pixel number to tile number
            X = (X \ PicX) + 1
            Y = (Y \ PicY) + 1

            ' check it's not out of bounds
            If X < 0 Then X = 0
            If X > picBackSelect.Width / PicX Then X = picBackSelect.Width / PicX
            If Y < 0 Then Y = 0
            If Y > picBackSelect.Height / PicY Then Y = picBackSelect.Height / PicY

            ' find out what to set the width + height of map editor to
            If X > EditorTileX Then ' drag right
                EditorTileWidth = X - EditorTileX
            End If

            If Y > EditorTileY Then ' drag down
                EditorTileHeight = Y - EditorTileY
            End If

            EditorTileSelStart = New Point(EditorTileX, EditorTileY)
            EditorTileSelEnd = New Point(EditorTileWidth, EditorTileHeight)

            DrawTileset()
        End If

    End Sub

    Public Sub MapEditorMouseDown(ByVal Button As ButtonState, ByVal X As Integer, ByVal Y As Integer, Optional ByVal movedMouse As Boolean = True)
        Dim i As Integer
        Dim CurLayer As Integer
        Dim tileChanged As Boolean

        CurLayer = cmbLayers.SelectedIndex + 1

        If EyeDropper Then
            MapEditorEyeDropper()
            Exit Sub
        End If

        For x2 = 0 To MyMap.MaxX
            For y2 = 0 To MyMap.MaxY
                With MyMap.Tile(x2, y2)
                    If .Layer(CurLayer).Tileset > 0 Then
                        If Not tileChanged Then
                            MapEditorHistory()
                            tileChanged = True
                        End If

                        TileHistory(HistoryIndex).Tile(x2, y2).Data1 = .Data1
                        TileHistory(HistoryIndex).Tile(x2, y2).Data2 = .Data2
                        TileHistory(HistoryIndex).Tile(x2, y2).Data3 = .Data3
                        TileHistory(HistoryIndex).Tile(x2, y2).Type = .Type
                        TileHistory(HistoryIndex).Tile(x2, y2).DirBlock = .DirBlock

                        TileHistory(HistoryIndex).Tile(x2, y2).Layer(CurLayer).X = .Layer(CurLayer).X
                        TileHistory(HistoryIndex).Tile(x2, y2).Layer(CurLayer).Y = .Layer(CurLayer).Y
                        TileHistory(HistoryIndex).Tile(x2, y2).Layer(CurLayer).Tileset = .Layer(CurLayer).Tileset
                        TileHistory(HistoryIndex).Tile(x2, y2).Layer(CurLayer).AutoTile = .Layer(CurLayer).AutoTile
                    End If
                End With
            Next
        Next

        If Not IsInBounds() Then Exit Sub
        If cmbAutoTile.SelectedIndex = -1 Then Exit Sub

        If Button = MouseButton.Left Then
            If optInfo.Checked Then
                Select Case MyMap.Tile(CurX, CurY).Type
                    Case TileType.Warp
                        AddText("Map: " + MyMap.Tile(CurX, CurY).Data1.ToString() + " X: " + MyMap.Tile(CurX, CurY).Data2.ToString() + " Y:" + MyMap.Tile(CurX, CurY).Data3.ToString(), ColorType.Gray)
                End Select

                Select Case MyMap.Tile(CurX, CurY).Type2
                    Case TileType.Warp
                        AddText("Map: " + MyMap.Tile(CurX, CurY).Data1_2.ToString() + " X: " + MyMap.Tile(CurX, CurY).Data2_2.ToString() + " Y:" + MyMap.Tile(CurX, CurY).Data3_2.ToString(), ColorType.Gray)
                End Select
            End If

            If tabpages.SelectedTab Is tpTiles Then
                If EditorTileWidth = 1 And EditorTileHeight = 1 Then 'single tile
                    MapEditorSetTile(CurX, CurY, CurLayer, False, cmbAutoTile.SelectedIndex)
                Else ' multi tile!
                    If cmbAutoTile.SelectedIndex = 0 Then
                        MapEditorSetTile(CurX, CurY, CurLayer, True)
                    Else
                        MapEditorSetTile(CurX, CurY, CurLayer, True, cmbAutoTile.SelectedIndex)
                    End If
                End If
            ElseIf tabpages.SelectedTab Is tpAttributes Then
                With MyMap.Tile(CurX, CurY)
                    ' blocked tile
                    If optBlocked.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Blocked
                        Else
                            .Type2 = TileType.Blocked
                        End If
                    End If

                    ' warp tile
                    If optWarp.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Warp
                            .Data1 = EditorWarpMap
                            .Data2 = EditorWarpX
                            .Data3 = EditorWarpY
                        Else
                            .Type2 = TileType.Warp
                            .Data1_2 = EditorWarpMap
                            .Data2_2 = EditorWarpX
                            .Data3_2 = EditorWarpY
                        End If
                    End If

                    ' item spawn
                    If optItem.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Item
                            .Data1 = ItemEditorNum
                            .Data2 = ItemEditorValue
                            .Data3 = 0
                        Else
                            .Type2 = TileType.Item
                            .Data1_2 = ItemEditorNum
                            .Data2_2 = ItemEditorValue
                            .Data3_2 = 0
                        End If
                    End If

                    ' npc avoid
                    If optNPCAvoid.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.NPCAvoid
                            .Data1 = 0
                            .Data2 = 0
                            .Data3 = 0
                        Else
                            .Type2 = TileType.NPCAvoid
                            .Data1_2 = 0
                            .Data2_2 = 0
                            .Data3_2 = 0
                        End If
                    End If

                    ' resource
                    If optResource.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Resource
                            .Data1 = ResourceEditorNum
                            .Data2 = 0
                            .Data3 = 0
                        Else
                            .Type2 = TileType.Resource
                            .Data1_2 = ResourceEditorNum
                            .Data2_2 = 0
                            .Data3_2 = 0
                        End If
                    End If

                    ' npc spawn
                    If optNPCSpawn.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.NPCSpawn
                            .Data1 = SpawnNpcNum
                            .Data2 = SpawnNpcDir
                            .Data3 = 0
                        Else
                            .Type2 = TileType.NPCSpawn
                            .Data1_2 = SpawnNpcNum
                            .Data2_2 = SpawnNpcDir
                            .Data3_2 = 0
                        End If
                    End If

                    ' shop
                    If optShop.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Shop
                            .Data1 = EditorShop
                            .Data2 = 0
                            .Data3 = 0
                        Else
                            .Type2 = TileType.Shop
                            .Data1_2 = EditorShop
                            .Data2_2 = 0
                            .Data3_2 = 0
                        End If
                    End If

                    ' bank
                    If optBank.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Bank
                            .Data1 = 0
                            .Data2 = 0
                            .Data3 = 0
                        Else
                            .Type2 = TileType.Bank
                            .Data1_2 = 0
                            .Data2_2 = 0
                            .Data3_2 = 0
                        End If
                    End If

                    ' heal
                    If optHeal.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Heal
                            .Data1 = MapEditorHealType
                            .Data2 = MapEditorHealAmount
                            .Data3 = 0
                        Else
                            .Type2 = TileType.Heal
                            .Data1_2 = MapEditorHealType
                            .Data2_2 = MapEditorHealAmount
                            .Data3_2 = 0
                        End If
                    End If

                    ' trap
                    If optTrap.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Trap
                            .Data1 = MapEditorHealAmount
                            .Data2 = 0
                            .Data3 = 0
                        Else
                            .Type2 = TileType.Trap
                            .Data1_2 = MapEditorHealAmount
                            .Data2_2 = 0
                            .Data3_2 = 0
                        End If
                    End If

                    ' light
                    If optLight.Checked Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Light
                            .Data1 = EditorLight
                            .Data2 = EditorFlicker
                            .Data3 = EditorShadow
                        Else
                            .Type2 = TileType.Light
                            .Data1_2 = EditorLight
                            .Data2_2 = EditorFlicker
                            .Data3_2 = EditorShadow
                        End If
                    End If

                    ' Animation
                    If optAnimation.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.Animation
                            .Data1 = EditorAnimation
                            .Data2 = 0
                            .Data3 = 0
                        Else
                            .Type2 = TileType.Animation
                            .Data1_2 = EditorAnimation
                            .Data2_2 = 0
                            .Data3_2 = 0
                        End If
                    End If

                    ' No Xing
                    If optNoXing.Checked = True Then
                        If EditorAttribute = 1 Then
                            .Type = TileType.NoXing
                            .Data1 = 0
                            .Data2 = 0
                            .Data3 = 0
                        Else
                            .Type2 = TileType.NoXing
                            .Data1_2 = 0
                            .Data2_2 = 0
                            .Data3_2 = 0
                        End If
                    End If
                End With
            ElseIf tabpages.SelectedTab Is tpDirBlock Then
                ' find what tile it is
                X -= ((X \ PicX) * PicX)
                Y -= ((Y \ PicY) * PicY)

                ' see if it hits an arrow
                For i = 1 To 4
                    ' flip the value.
                    If X >= DirArrowX(i) And X <= DirArrowX(i) + 8 Then
                        If Y >= DirArrowY(i) And Y <= DirArrowY(i) + 8 Then
                            ' flip the value.
                            SetDirBlock(MyMap.Tile(CurX, CurY).DirBlock, CByte(i), Not IsDirBlocked(MyMap.Tile(CurX, CurY).DirBlock, CByte(i)))
                            Exit For
                        End If
                    End If
                Next
            ElseIf tabpages.SelectedTab Is tpEvents Then
                If frmEditor_Event.Visible = False Then
                    If EventCopy Then
                        CopyEvent_Map(CurX, CurY)
                    ElseIf EventPaste Then
                        PasteEvent_Map(CurX, CurY)
                    Else
                        AddEvent(CurX, CurY)
                    End If
                End If
            End If
        End If

        If Button = MouseButton.Right Then
            If tabpages.SelectedTab Is tpTiles Then
                If EditorTileWidth = 1 And EditorTileHeight = 1 Then 'single tile
                    MapEditorSetTile(CurX, CurY, CurLayer, False, cmbAutoTile.SelectedIndex, 1)
                Else ' multi tile!
                    If cmbAutoTile.SelectedIndex = 0 Then
                        MapEditorSetTile(CurX, CurY, CurLayer, True, 0, 1)
                    Else
                        MapEditorSetTile(CurX, CurY, CurLayer, True, cmbAutoTile.SelectedIndex, 1)
                    End If
                End If
            ElseIf tabpages.SelectedTab Is tpAttributes Then
                With MyMap.Tile(CurX, CurY)
                    ' clear attribute
                    .Type = 0
                    .Data1 = 0
                    .Data2 = 0
                    .Data3 = 0
                End With
            ElseIf tabpages.SelectedTab Is tpEvents Then
                DeleteEvent(CurX, CurY)
            End If
        End If
    End Sub

    Public Sub MapEditorCancel()
        Dim Buffer As ByteStream

        If MyEditorType <> EditorType.Map Then Exit Sub

        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ClientPackets.CNeedMap)
        Buffer.WriteInt32(1)
        Socket?.SendData(Buffer.Data, Buffer.Head)
        MyEditorType = -1
        GettingMap = True
        SendCloseEditor()

        frmEditor_Event.Dispose()
    End Sub

    Public Sub MapEditorSend()
        SendMap()
        MyEditorType = -1
        GettingMap = True
        SendCloseEditor()
    End Sub

    Public Sub MapEditorSetTile(ByVal X As Integer, ByVal Y As Integer, ByVal CurLayer As Integer, Optional ByVal multitile As Boolean = False, Optional ByVal theAutotile As Byte = 0, Optional eraseTile As Byte = 0)
        Dim x2 As Integer, y2 As Integer, newTileX As Integer, newTileY As Integer

        newTileX = EditorTileX
        newTileY = EditorTileY

        If eraseTile Then
            newTileX = 0
            newTileY = 0
        End If

        If theAutotile > 0 Then
            With MyMap.Tile(X, Y)
                ' set layer
                .Layer(CurLayer).X = newTileX
                .Layer(CurLayer).Y = newTileY
                If eraseTile Then
                    .Layer(CurLayer).Tileset = 0
                Else
                    .Layer(CurLayer).Tileset = cmbTileSets.SelectedIndex + 1
                End If
                .Layer(CurLayer).AutoTile = theAutotile
                CacheRenderState(X, Y, CurLayer)
            End With

            ' do a re-init so we can see our changes
            InitAutotiles()
            Exit Sub
        End If

        If Not multitile Then ' single
            With MyMap.Tile(X, Y)
                ' set layer
                .Layer(CurLayer).X = newTileX
                .Layer(CurLayer).Y = newTileY
                If eraseTile Then
                    .Layer(CurLayer).Tileset = 0
                Else
                    .Layer(CurLayer).Tileset = cmbTileSets.SelectedIndex + 1
                End If
                .Layer(CurLayer).AutoTile = 0
                CacheRenderState(X, Y, CurLayer)
            End With
        Else ' multitile
            y2 = 0 ' starting tile for y axis
            For Y = CurY To CurY + EditorTileHeight - 1
                x2 = 0 ' re-set x count every y loop
                For X = CurX To CurX + EditorTileWidth - 1
                    If X >= 0 And X <= MyMap.MaxX Then
                        If Y >= 0 And Y <= MyMap.MaxY Then
                            With MyMap.Tile(X, Y)
                                .Layer(CurLayer).X = newTileX + x2
                                .Layer(CurLayer).Y = newTileY + y2
                                If eraseTile Then
                                    .Layer(CurLayer).Tileset = 0
                                Else
                                    .Layer(CurLayer).Tileset = cmbTileSets.SelectedIndex + 1
                                End If
                                .Layer(CurLayer).AutoTile = 0
                                CacheRenderState(X, Y, CurLayer)
                            End With
                        End If
                    End If
                    x2 += 1
                Next
                y2 += 1
            Next
        End If
    End Sub

    Public Sub MapEditorHistory()
        Dim i As Long

        If HistoryIndex = MaxTileHistory Then
            For i = 1 To MaxTileHistory - 1
                TileHistory(i) = TileHistory(i + 1)
                TileHistoryHighIndex = TileHistoryHighIndex - 1
            Next
        Else
            HistoryIndex = HistoryIndex + 1
            TileHistoryHighIndex = TileHistoryHighIndex + 1
            If TileHistoryHighIndex > HistoryIndex Then
                TileHistoryHighIndex = HistoryIndex
            End If
        End If

    End Sub

    Public Sub MapEditorClearLayer(ByVal layer As LayerType)
        Dialogue("Map Editor", "Clear Layer: " & layer.ToString, "Are you sure you wish to clear this layer?", DialogueType.ClearLayer, DialogueStyle.YesNo, cmbLayers.SelectedIndex + 1, cmbAutoTile.SelectedIndex + 1)
    End Sub

    Public Sub MapEditorFillLayer(ByVal layer As LayerType, Optional ByVal theAutotile As Byte = 0, Optional ByVal tileX As Byte = 0, Optional ByVal tileY As Byte = 0)
        Dialogue("Map Editor", "Fill Layer: " & layer.ToString, "Are you sure you wish to fill this layer?", DialogueType.FillLayer, DialogueStyle.YesNo, cmbLayers.SelectedIndex + 1, cmbAutoTile.SelectedIndex + 1, tileX, tileY, cmbTileSets.SelectedIndex + 1)
    End Sub

    Public Sub MapEditorEyeDropper()
        Dim CurLayer As Integer

        CurLayer = cmbLayers.SelectedIndex + 1

        With MyMap.Tile(CurX, CurY)
            If .Layer(CurLayer).Tileset > 0 Then
                cmbTileSets.SelectedIndex = .Layer(CurLayer).Tileset - 1
            Else
                cmbTileSets.SelectedIndex = 1
            End If
            MapEditorChooseTile(MouseButtons.Left, .Layer(CurLayer).X * PicX, .Layer(CurLayer).Y * PicY)
            EyeDropper = Not EyeDropper
        End With
    End Sub

    Public Sub MapEditorUndo()
        Dim tileChanged As Boolean

        If HistoryIndex = 0 Then Exit Sub

        HistoryIndex = HistoryIndex - 1

        For x = 0 To MyMap.MaxX
            For y = 0 To MyMap.MaxY
                For i = 1 To LayerType.Count - 1
                    With MyMap.Tile(x, y)
                        If Not (MyMap.Tile(x, y).Type = TileHistory(HistoryIndex).Tile(x, y).Type) Or (Not .Layer(i).X = TileHistory(HistoryIndex).Tile(x, y).Layer(i).X Or Not .Layer(i).Y = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Y Or Not .Layer(i).Tileset = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Tileset) Then
                            tileChanged = True
                        End If

                        .Data1 = TileHistory(HistoryIndex).Tile(x, y).Data1
                        .Data2 = TileHistory(HistoryIndex).Tile(x, y).Data2
                        .Data3 = TileHistory(HistoryIndex).Tile(x, y).Data3
                        .Data1_2 = TileHistory(HistoryIndex).Tile(x, y).Data1_2
                        .Data2_2 = TileHistory(HistoryIndex).Tile(x, y).Data2_2
                        .Data3_2 = TileHistory(HistoryIndex).Tile(x, y).Data3_2
                        .Type = TileHistory(HistoryIndex).Tile(x, y).Type
                        .Type2 = TileHistory(HistoryIndex).Tile(x, y).Type2
                        .DirBlock = TileHistory(HistoryIndex).Tile(x, y).DirBlock
                        .Layer(i).X = TileHistory(HistoryIndex).Tile(x, y).Layer(i).X
                        .Layer(i).Y = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Y
                        .Layer(i).Tileset = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Tileset
                        .Layer(i).AutoTile = TileHistory(HistoryIndex).Tile(x, y).Layer(i).AutoTile
                        CacheRenderState(x, y, i)
                    End With
                Next
            Next
        Next

        If Not tileChanged Then
            MapEditorUndo()
        End If
    End Sub

    Public Sub MapEditorRedo()
        Dim tileChanged As Boolean

        If TileHistoryHighIndex > 0 And (TileHistoryHighIndex = HistoryIndex Or HistoryIndex = MaxTileHistory) Then
            Exit Sub
        End If

        If HistoryIndex = MaxTileHistory Then Exit Sub

        HistoryIndex = HistoryIndex + 1

        For x = 0 To MyMap.MaxX
            For y = 0 To MyMap.MaxY
                For i = 1 To LayerType.Count - 1
                    With MyMap.Tile(x, y)
                        If Not (MyMap.Tile(x, y).Type = TileHistory(HistoryIndex).Tile(x, y).Type) Or (Not .Layer(i).X = TileHistory(HistoryIndex).Tile(x, y).Layer(i).X Or Not .Layer(i).Y = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Y Or Not .Layer(i).Tileset = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Tileset) Then
                            tileChanged = True
                        End If

                        .Data1 = TileHistory(HistoryIndex).Tile(x, y).Data1
                        .Data2 = TileHistory(HistoryIndex).Tile(x, y).Data2
                        .Data3 = TileHistory(HistoryIndex).Tile(x, y).Data3
                        .Type = TileHistory(HistoryIndex).Tile(x, y).Type
                        .DirBlock = TileHistory(HistoryIndex).Tile(x, y).DirBlock

                        .Layer(i).X = TileHistory(HistoryIndex).Tile(x, y).Layer(i).X
                        .Layer(i).Y = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Y
                        .Layer(i).Tileset = TileHistory(HistoryIndex).Tile(x, y).Layer(i).Tileset
                        .Layer(i).AutoTile = TileHistory(HistoryIndex).Tile(x, y).Layer(i).AutoTile
                        CacheRenderState(x, y, i)
                    End With
                Next
            Next
        Next

        If Not tileChanged Then
            MapEditorRedo()
        End If
    End Sub

    Public Sub ClearAttributeDialogue()
        fraNpcSpawn.Visible = False
        fraResource.Visible = False
        fraMapItem.Visible = False
        fraMapWarp.Visible = False
        fraShop.Visible = False
        fraHeal.Visible = False
        fraTrap.Visible = False
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        MyMap.Name = txtName.Text
    End Sub

    Private Sub frmEditor_Map_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MapEditorCancel()
    End Sub

    Private Sub scrMapBrightness_Scroll(sender As Object, e As ScrollEventArgs) Handles scrlMapBrightness.Scroll
        MyMap.Brightness = scrlMapBrightness.Value
        lblMapBrightness.Text = "Brightness: " & scrlMapBrightness.Value
    End Sub

#End Region

#Region "Drawing"
    Public Sub DrawTileset()
        Dim tileset As Integer = cmbTileSets.SelectedIndex + 1

        ' Set the render target to the TilesetWindow and clear it
        Client.GraphicsDevice.SetRenderTarget(Client.TilesetWindow)
        Client.GraphicsDevice.Clear(Color.Transparent) ' Clear with transparency

        ' Set the appropriate autotile dimensions
        SetAutoTileDimensions(cmbAutoTile.SelectedIndex)

        ' Define the source and destination rectangles for rendering
        Dim srcRect As New Rectangle(0, 0, Client.TilesetGfxInfo(tileset).Width, Client.TilesetGfxInfo(tileset).Height)
        Dim destRect As New Rectangle(0, 0, Math.Min(srcRect.Width, picBackSelect.Width), Math.Min(srcRect.Height, picBackSelect.Height))

        ' Begin the sprite batch and draw the tileset texture
        Client.SpriteBatch.Begin()
        Client.SpriteBatch.Draw(
            Client.TilesetTexture(tileset), destRect, srcRect, Color.White
        )
        Client.SpriteBatch.End()

        ' Draw the editor animation overlay (optional)
        Client.SpriteBatch.Begin()
        Client.SpriteBatch.Draw(Client.EditorAnimation_Anim1, Vector2.Zero, Color.White)
        Client.SpriteBatch.End()

        ' Draw the selection rectangle
        Client.DrawSelectionRectangle()

        ' Reset the render target back to the screen
        Client.GraphicsDevice.SetRenderTarget(Nothing)

        ' Display the tileset on the screen by drawing the render target
        Client.SpriteBatch.Begin()
        Client.SpriteBatch.Draw(Client.TilesetWindow, Vector2.Zero, Color.White)
        Client.SpriteBatch.End()
    End Sub

    ' Helper function to set tile dimensions based on the selected autotile type
    Private Sub SetAutoTileDimensions(selectedIndex As Integer)
        Select Case selectedIndex
            Case 1 ' Autotile
                EditorTileWidth = 2
                EditorTileHeight = 3
            Case 2 ' Fake Autotile
                EditorTileWidth = 1
                EditorTileHeight = 1
            Case 3 ' Animated
                EditorTileWidth = 6
                EditorTileHeight = 3
            Case 4 ' Cliff
                EditorTileWidth = 2
                EditorTileHeight = 2
            Case 5 ' Waterfall
                EditorTileWidth = 2
                EditorTileHeight = 3
            Case Else
                EditorTileWidth = 1
                EditorTileHeight = 1
        End Select
    End Sub

    Public Sub EditorMap_DrawItem()
        Dim itemnum As Integer

        itemnum = Type.Item(Me.scrlMapItem.Value).Icon

        If itemnum <= 0 Or itemnum > NumItems Then
            Me.picMapItem.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Core.Path.Graphics & "items\" & itemnum & GfxExt) Then
            Me.picMapItem.BackgroundImage = Drawing.Image.FromFile(Core.Path.Graphics & "items\" & itemnum & GfxExt)
        End If

    End Sub

    Private Sub tsbCopyMap_Click(sender As Object, e As EventArgs) Handles tsbCopyMap.Click
        Dim i As Integer, X As Integer, Y As Integer

        If CopyMap = False Then
            ReDim Tile(MyMap.MaxX, MyMap.MaxY)
            TmpMaxX = MyMap.MaxX
            TmpMaxY = MyMap.MaxY

            For X = 0 To MyMap.MaxX
                For Y = 0 To MyMap.MaxY
                    With MyMap.Tile(X, Y)
                        ReDim Tile(X, Y).Layer(LayerType.Count - 1)

                        Tile(X, Y).Data1 = .Data1
                        Tile(X, Y).Data2 = .Data2
                        Tile(X, Y).Data3 = .Data3
                        Tile(X, Y).Type = .Type
                        Tile(X, Y).DirBlock = .DirBlock

                        For i = 1 To LayerType.Count - 1
                            Tile(X, Y).Layer(i).X = .Layer(i).X
                            Tile(X, Y).Layer(i).Y = .Layer(i).Y
                            Tile(X, Y).Layer(i).Tileset = .Layer(i).Tileset
                            Tile(X, Y).Layer(i).AutoTile = .Layer(i).AutoTile
                        Next
                    End With
                Next
            Next

            CopyMap = True
            MsgBox("Map copied. Go to another map to paste it.")
        Else
            ReDim MyMap.Tile(TmpMaxX, TmpMaxY)
            ReDim Type.Autotile(TmpMaxX, TmpMaxY)
            MyMap.MaxX = TmpMaxX
            MyMap.MaxY = TmpMaxY

            For X = 0 To MyMap.MaxX
                For Y = 0 To MyMap.MaxY
                    With MyMap.Tile(X, Y)
                        ReDim Preserve MyMap.Tile(X, Y).Layer(LayerType.Count - 1)
                        ReDim Preserve Type.Autotile(X, Y).Layer(LayerType.Count - 1)

                        .Data1 = Tile(X, Y).Data1
                        .Data2 = Tile(X, Y).Data2
                        .Data3 = Tile(X, Y).Data3
                        .Type = Tile(X, Y).Type
                        .DirBlock = Tile(X, Y).DirBlock

                        For i = 1 To LayerType.Count - 1
                            .Layer(i).X = Tile(X, Y).Layer(i).X
                            .Layer(i).Y = Tile(X, Y).Layer(i).Y
                            .Layer(i).Tileset = Tile(X, Y).Layer(i).Tileset
                            .Layer(i).AutoTile = Tile(X, Y).Layer(i).AutoTile
                            CacheRenderState(X, Y, i)
                        Next
                    End With
                Next
            Next
            CopyMap = False
        End If
    End Sub

    Private Sub tsbUndo_Click(sender As Object, e As EventArgs) Handles tsbUndo.Click
        MapEditorUndo()
    End Sub

    Private Sub tsbRedo_Click(sender As Object, e As EventArgs) Handles tsbRedo.Click
        MapEditorRedo()
    End Sub

    Private Sub tsbOpacity_Click(sender As Object, e As EventArgs) Handles tsbOpacity.Click
        HideLayers = Not HideLayers
    End Sub

    Private Sub tsbLight_Click(sender As Object, e As EventArgs) Handles tsbLight.Click
        Night = Not Night
    End Sub

    Private Sub tsbScreenshot_Click(sender As Object, e As EventArgs) Handles tsbScreenshot.Click
        If MyMap.Name = "" Then
            MsgBox("Screenshot is done onlyIf Type.Map has a valid name.")
            Exit Sub
        End If

        Client.TakeScreenshot()

    End Sub

    Private Sub optAnimation_CheckedChanged(sender As Object, e As EventArgs) Handles optAnimation.CheckedChanged
        If optAnimation.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraAnimation.Visible = True
    End Sub

    Private Sub brnAnimation_Click(sender As Object, e As EventArgs) Handles brnAnimation.Click
        EditorAnimation = cmbAnimation.SelectedIndex
        pnlAttributes.Visible = False
        fraAnimation.Visible = False
    End Sub

    Private Sub btnLight_Click(sender As Object, e As EventArgs) Handles btnLight.Click
        EditorLight = scrlLight.Value
        If chkFlicker.Checked Then
            EditorFlicker = 1
        Else
            EditorFlicker = 0
        End If

        If chkShadow.Checked Then
            EditorShadow = 1
        Else
            EditorShadow = 0
        End If

        pnlAttributes.Visible = False
        fraMapLight.Visible = False
    End Sub

    Private Sub optLight_CheckedChanged(sender As Object, e As EventArgs) Handles optLight.CheckedChanged
        If optLight.Checked = False Then Exit Sub

        ClearAttributeDialogue()
        pnlAttributes.Visible = True
        fraMapLight.Visible = True
    End Sub

    Private Sub scrlLight_ValueChanged(sender As Object, e As EventArgs) Handles scrlLight.ValueChanged
        lblRadius.Text = "Radius: " & scrlLight.Value
    End Sub

    Private Sub chkRespawn_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoMapRespawn.CheckedChanged
        If chkNoMapRespawn.Checked = True Then
            MyMap.NoRespawn = 1
        Else
            MyMap.NoRespawn = 0
        End If
    End Sub

    Private Sub chkIndoors_CheckedChanged(sender As Object, e As EventArgs) Handles chkIndoors.CheckedChanged
        If chkIndoors.Checked = True Then
            MyMap.Indoors = 1
        Else
            MyMap.Indoors = 0
        End If
    End Sub

    Private Sub cmbAttribute_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAttribute.SelectedIndexChanged
        EditorAttribute = cmbAttribute.SelectedIndex + 1
    End Sub

    Private Sub tsbDeleteMap_Click(sender As Object, e As EventArgs) Handles tsbDeleteMap.Click
        ClearMap
    End Sub

#End Region

End Class