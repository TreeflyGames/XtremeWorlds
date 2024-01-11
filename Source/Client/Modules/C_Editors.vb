Imports System.IO
Imports Core

Module C_Editors

#Region "Animation Editor"

    Friend Sub AnimationEditorInit()
        Editorindex = frmEditor_Animation.lstIndex.SelectedIndex + 1

        With Animation(Editorindex)
            If Trim$(Animation(Editorindex).Sound) = "" Then
                frmEditor_Animation.cmbSound.SelectedIndex = 0
            Else
                For i = 0 To frmEditor_Animation.cmbSound.Items.Count
                    If frmEditor_Animation.cmbSound.GetItemText(i) = frmEditor_Animation.cmbSound.SelectedIndex Then
                        frmEditor_Animation.cmbSound.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
            frmEditor_Animation.txtName.Text = Trim$(.Name)

            frmEditor_Animation.nudSprite0.Value = .Sprite(0)
            frmEditor_Animation.nudFrameCount0.Value = .Frames(0)
            If Animation(Editorindex).LoopCount(0) = 0 Then Animation(Editorindex).LoopCount(0) = 1
            frmEditor_Animation.nudLoopCount0.Value = .LoopCount(0)
            If Animation(Editorindex).LoopTime(0) = 0 Then Animation(Editorindex).LoopTime(0) = 1
            frmEditor_Animation.nudLoopTime0.Value = .LoopTime(0)

            frmEditor_Animation.nudSprite1.Value = .Sprite(1)
            frmEditor_Animation.nudFrameCount1.Value = .Frames(1)
            If Animation(Editorindex).LoopCount(1) = 0 Then Animation(Editorindex).LoopCount(1) = 1
            frmEditor_Animation.nudLoopCount1.Value = .LoopCount(1)
            If Animation(Editorindex).LoopTime(1) = 0 Then Animation(Editorindex).LoopTime(1) = 1
            frmEditor_Animation.nudLoopTime1.Value = .LoopTime(1)
        End With

        Animation_Changed(Editorindex) = True
    End Sub

    Friend Sub AnimationEditorOk()
        Dim i As Integer

       For i = 1 To MAX_ANIMATIONS
            If Animation_Changed(i) Then
                SendSaveAnimation(i)
            End If
        Next

        Editor = -1
        ClearChanged_Animation()
        SendCloseEditor()
    End Sub

    Friend Sub AnimationEditorCancel()
        Editor = -1
        ClearChanged_Animation()
        ClearAnimations()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Animation()
        For i = 1 To MAX_ANIMATIONS
            Animation_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Npc Editor"

    Friend Sub NpcEditorInit()
        With frmEditor_NPC
            Editorindex = .lstIndex.SelectedIndex + 1

            .cmbDropSlot.SelectedIndex = 0

            .txtName.Text = NPC(Editorindex).Name
            .txtAttackSay.Text = NPC(Editorindex).AttackSay
            .nudSprite.Value = NPC(Editorindex).Sprite
            .nudSpawnSecs.Value = NPC(Editorindex).SpawnSecs
            .cmbBehaviour.SelectedIndex = NPC(Editorindex).Behaviour
            .cmbFaction.SelectedIndex = NPC(Editorindex).Faction
            .nudRange.Value = NPC(Editorindex).Range
            .nudChance.Value = NPC(Editorindex).DropChance(frmEditor_NPC.cmbDropSlot.SelectedIndex)
            .cmbItem.SelectedIndex = NPC(Editorindex).DropItem(frmEditor_NPC.cmbDropSlot.SelectedIndex)

            .nudAmount.Value = NPC(Editorindex).DropItemValue(frmEditor_NPC.cmbDropSlot.SelectedIndex)

            .nudHp.Value = NPC(Editorindex).HP
            .nudExp.Value = NPC(Editorindex).Exp
            .nudLevel.Value = NPC(Editorindex).Level
            .nudDamage.Value = NPC(Editorindex).Damage

            .cmbSpawnPeriod.SelectedIndex = NPC(Editorindex).SpawnTime

            .cmbAnimation.SelectedIndex = NPC(Editorindex).Animation

            .nudStrength.Value = NPC(Editorindex).Stat(StatType.Strength)
            .nudEndurance.Value = NPC(Editorindex).Stat(StatType.Endurance)
            .nudIntelligence.Value = NPC(Editorindex).Stat(StatType.Intelligence)
            .nudSpirit.Value = NPC(Editorindex).Stat(StatType.Spirit)
            .nudLuck.Value = NPC(Editorindex).Stat(StatType.Luck)
            .nudVitality.Value = NPC(Editorindex).Stat(StatType.Vitality)

            .cmbSkill1.SelectedIndex = NPC(Editorindex).Skill(1)
            .cmbSkill2.SelectedIndex = NPC(Editorindex).Skill(2)
            .cmbSkill3.SelectedIndex = NPC(Editorindex).Skill(3)
            .cmbSkill4.SelectedIndex = NPC(Editorindex).Skill(4)
            .cmbSkill5.SelectedIndex = NPC(Editorindex).Skill(5)
            .cmbSkill6.SelectedIndex = NPC(Editorindex).Skill(6)
        End With

        EditorNpc_DrawSprite()
        NPC_Changed(Editorindex) = True
    End Sub

    Friend Sub NpcEditorOk()
        Dim i As Integer

        For i = 1 To MAX_NPCS
            If NPC_Changed(i) Then
                SendSaveNpc(i)
            End If
        Next

        Editor = -1
        ClearChanged_NPC()
        SendCloseEditor()
    End Sub

    Friend Sub NpcEditorCancel()
        Editor = -1
        ClearChanged_NPC()
        ClearNpcs()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_NPC()
        For i = 1 To MAX_NPCS
            NPC_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Resource Editor"
    Friend Sub ClearChanged_Resource()
        ReDim Resource_Changed(MAX_RESOURCES)
    End Sub

    Friend Sub ResourceEditorInit()
        Dim i As Integer

        Editorindex = frmEditor_Resource.lstIndex.SelectedIndex + 1

        With frmEditor_Resource
            .txtName.Text = Trim$(Resource(Editorindex).Name)
            .txtMessage.Text = Trim$(Resource(Editorindex).SuccessMessage)
            .txtMessage2.Text = Trim$(Resource(Editorindex).EmptyMessage)
            .cmbType.SelectedIndex = Resource(Editorindex).ResourceType
            .nudNormalPic.Value = Resource(Editorindex).ResourceImage
            .nudExhaustedPic.Value = Resource(Editorindex).ExhaustedImage
            .cmbRewardItem.SelectedIndex = Resource(Editorindex).ItemReward
            .nudRewardExp.Value = Resource(Editorindex).ExpReward
            .cmbTool.SelectedIndex = Resource(Editorindex).ToolRequired
            .nudHealth.Value = Resource(Editorindex).Health
            .nudRespawn.Value = Resource(Editorindex).RespawnTime
            .cmbAnimation.SelectedIndex = Resource(Editorindex).Animation
            .nudLvlReq.Value = Resource(Editorindex).LvlRequired
        End With

        frmEditor_Resource.Visible = True

        EditorResource_DrawSprite()

        Resource_Changed(Editorindex) = True
    End Sub

    Friend Sub ResourceEditorOk()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            If Resource_Changed(i) Then
                SendSaveResource(i)
            End If
        Next

        Editor = -1
        ClearChanged_Resource()
        SendCloseEditor()
    End Sub

    Friend Sub ResourceEditorCancel()
        Editor = -1
        ClearChanged_Resource()
        ClearResources()
        SendCloseEditor()
    End Sub

#End Region

#Region "Skill Editor"

    Friend Sub SkillEditorInit()
        With frmEditor_Skill
            Editorindex = .lstIndex.SelectedIndex + 1

            .cmbAnimCast.SelectedIndex = 0
            .cmbAnim.SelectedIndex = 0

            ' set values
            .txtName.Text = Trim$(Skill(Editorindex).Name)
            .cmbType.SelectedIndex = Skill(Editorindex).Type
            .nudMp.Value = Skill(Editorindex).MpCost
            .nudLevel.Value = Skill(Editorindex).LevelReq
            .cmbAccessReq.SelectedIndex = Skill(Editorindex).AccessReq
            .cmbJob.SelectedIndex = Skill(Editorindex).JobReq
            .nudCast.Value = Skill(Editorindex).CastTime
            .nudCool.Value = Skill(Editorindex).CdTime
            .nudIcon.Value = Skill(Editorindex).Icon
            .nudMap.Value = Skill(Editorindex).Map
            .nudX.Value = Skill(Editorindex).X
            .nudY.Value = Skill(Editorindex).Y
            .cmbDir.SelectedIndex = Skill(Editorindex).Dir
            .nudVital.Value = Skill(Editorindex).Vital
            .nudDuration.Value = Skill(Editorindex).Duration
            .nudInterval.Value = Skill(Editorindex).Interval
            .nudRange.Value = Skill(Editorindex).Range

            .chkAoE.Checked = Skill(Editorindex).IsAoE

            .nudAoE.Value = Skill(Editorindex).AoE
            .cmbAnimCast.SelectedIndex = Skill(Editorindex).CastAnim
            .cmbAnim.SelectedIndex = Skill(Editorindex).SkillAnim
            .nudStun.Value = Skill(Editorindex).StunDuration

            If Skill(Editorindex).IsProjectile = 1 Then
                .chkProjectile.Checked = True
            Else
                .chkProjectile.Checked = False
            End If
            .cmbProjectile.SelectedIndex = Skill(Editorindex).Projectile

            If Skill(Editorindex).KnockBack = 1 Then
                .chkKnockBack.Checked = True
            Else
                .chkKnockBack.Checked = False
            End If
            .cmbKnockBackTiles.SelectedIndex = Skill(Editorindex).KnockBackTiles
        End With

        EditorSkill_DrawIcon()
        Skill_Changed(Editorindex) = True
    End Sub

    Friend Sub SkillEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            If Skill_Changed(i) Then
                SendSaveSkill(i)
            End If
        Next

        Editor = -1
        ClearChanged_Skill()
        SendCloseEditor()
    End Sub

    Friend Sub SkillEditorCancel()
        Editor = -1
        ClearChanged_Skill()
        ClearSkills()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Skill()
        For i = 1 To MAX_SKILLS
            Skill_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Shop editor"
    Friend Sub ShopEditorInit()
        Editorindex = frmEditor_Shop.lstIndex.SelectedIndex + 1

        With frmEditor_Shop
            .txtName.Text = Trim$(Shop(Editorindex).Name)

            If Shop(Editorindex).BuyRate > 0 Then
                .nudBuy.Value = Shop(Editorindex).BuyRate
            Else
                .nudBuy.Value = 100
            End If

            .nudFace.Value = Shop(Editorindex).Face
            If File.Exists(Paths.Graphics & "Faces\" & Shop(Editorindex).Face & GfxExt) Then
                .picFace.BackgroundImage = Drawing.Image.FromFile(Paths.Graphics & "Faces\" & Shop(Editorindex).Face & GfxExt)
            Else
                .picFace.BackgroundImage = Nothing
            End If

            .cmbItem.SelectedIndex = 0
            .cmbCostItem.SelectedIndex = 0
        End With

        UpdateShopTrade()
        Shop_Changed(Editorindex) = True
    End Sub

    Friend Sub UpdateShopTrade()
        Dim i As Integer

        frmEditor_Shop.lstTradeItem.Items.Clear()

        For i = 1 To MAX_TRADES
            With Shop(Editorindex).TradeItem(i)
                ' if none, show as none
                If .Item = 0 AndAlso .CostItem = 0 Then
                    frmEditor_Shop.lstTradeItem.Items.Add("Empty Trade Slot")
                Else
                    frmEditor_Shop.lstTradeItem.Items.Add(i & ": " & .ItemValue & "x " & Trim$(Item(.Item).Name) & " for " & .CostValue & "x " & Trim$(Item(.CostItem).Name))
                End If
            End With
        Next

        frmEditor_Shop.lstTradeItem.SelectedIndex = 0
    End Sub

    Friend Sub ShopEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SHOPS
            If Shop_Changed(i) Then
                SendSaveShop(i)
            End If
        Next

        Editor = -1
        ClearChanged_Shop()
        SendCloseEditor()
    End Sub

    Friend Sub ShopEditorCancel()
        Editor = -1
        ClearChanged_Shop()
        ClearShops()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Shop()
        For i = 1 To MAX_SHOPS
            Shop_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Job Editor"
    Friend Sub JobEditorOk()
        For i = 1 To MAX_JOBS
            If Job_Changed(i) Then
                SendSaveJob(i)
            End If
        Next
        Editor = -1
        SendCloseEditor()
    End Sub

    Friend Sub JobEditorCancel()
        Editor = -1
        ClearChanged_Job()
        ClearJobs()
        SendCloseEditor()
    End Sub

    Friend Sub JobEditorInit()
        Dim i As Integer

        With frmEditor_Job
            Editorindex = .lstIndex.SelectedIndex + 1

            .txtName.Text = Job(Editorindex).Name
            .txtDescription.Text = Job(Editorindex).Desc

            If Job(Editorindex).MaleSprite = 0 Then Job(Editorindex).MaleSprite = 1
            .nudMaleSprite.Value = Job(Editorindex).MaleSprite
            If Job(Editorindex).FemaleSprite = 0 Then Job(Editorindex).FemaleSprite = 1
            .nudFemaleSprite.Value = Job(Editorindex).FemaleSprite

            .cmbItems.SelectedIndex = 0

            For i = 1 To StatType.Count - 1
                If Job(Editorindex).Stat(i) = 0 Then Job(Editorindex).Stat(i) = 1
            Next

            .nudStrength.Value = Job(Editorindex).Stat(StatType.Strength)
            .nudLuck.Value = Job(Editorindex).Stat(StatType.Luck)
            .nudEndurance.Value = Job(Editorindex).Stat(StatType.Endurance)
            .nudIntelligence.Value = Job(Editorindex).Stat(StatType.Intelligence)
            .nudVitality.Value = Job(Editorindex).Stat(StatType.Vitality)
            .nudSpirit.Value = Job(Editorindex).Stat(StatType.Spirit)
            .nudBaseExp.Value = Job(Editorindex).BaseExp

            If Job(Editorindex).StartMap = 0 Then Job(Editorindex).StartMap = 1
            .nudStartMap.Value = Job(Editorindex).StartMap
            .nudStartX.Value = Job(Editorindex).StartX
            .nudStartY.Value = Job(Editorindex).StartY

            Job_Changed(Editorindex) = True
            .DrawPreview()
        End With
    End Sub

    Friend Sub ClearChanged_Job()
       For i = 1 To MAX_JOBS
            Job_Changed(i) = False
        Next
    End Sub


#End Region

#Region "Item"

    Friend Sub ItemEditorInit()
        Dim i As Integer

        Editorindex = frmEditor_Item.lstIndex.SelectedIndex + 1

        With Item(Editorindex)
            frmEditor_Item.txtName.Text = Trim$(.Name)
            frmEditor_Item.txtDescription.Text = Trim$(.Description)

            If .Pic > frmEditor_Item.nudPic.Maximum Then .Pic = 0
            frmEditor_Item.nudPic.Value = .Pic
            If .Type > ItemType.Count - 1 Then .Type = 0
            frmEditor_Item.cmbType.SelectedIndex = .Type
            frmEditor_Item.cmbAnimation.SelectedIndex = .Animation

            If .ItemLevel = 0 Then .ItemLevel = 1
            frmEditor_Item.nudItemLvl.Value = .ItemLevel

            ' Type specific settings
            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Equipment) Then
                frmEditor_Item.fraEquipment.Visible = True
                frmEditor_Item.nudDamage.Value = .Data2
                frmEditor_Item.cmbTool.SelectedIndex = .Data3

                frmEditor_Item.cmbSubType.SelectedIndex = .SubType

                If .Speed < 1000 Then .Speed = 100
                If .Speed > frmEditor_Item.nudSpeed.Maximum Then .Speed = frmEditor_Item.nudSpeed.Maximum
                frmEditor_Item.nudSpeed.Value = .Speed

                frmEditor_Item.nudStrength.Value = .Add_Stat(StatType.Strength)
                frmEditor_Item.nudEndurance.Value = .Add_Stat(StatType.Endurance)
                frmEditor_Item.nudIntelligence.Value = .Add_Stat(StatType.Intelligence)
                frmEditor_Item.nudVitality.Value = .Add_Stat(StatType.Vitality)
                frmEditor_Item.nudLuck.Value = .Add_Stat(StatType.Luck)
                frmEditor_Item.nudSpirit.Value = .Add_Stat(StatType.Spirit)

                If .KnockBack = 1 Then
                    frmEditor_Item.chkKnockBack.Checked = True
                Else
                    frmEditor_Item.chkKnockBack.Checked = False
                End If
                frmEditor_Item.cmbKnockBackTiles.SelectedIndex = .KnockBackTiles

                If .Randomize = 1 Then
                    frmEditor_Item.chkRandomize.Checked = True
                Else
                    frmEditor_Item.chkRandomize.Checked = False
                End If

                frmEditor_Item.nudPaperdoll.Value = .Paperdoll

                If .SubType = EquipmentType.Weapon Then
                    frmEditor_Item.fraProjectile.Visible = True
                Else
                     frmEditor_Item.fraProjectile.Visible = False
                End If
            Else
                frmEditor_Item.fraEquipment.Visible = False
            End If

            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Consumable) Then
                frmEditor_Item.fraVitals.Visible = True
                frmEditor_Item.nudVitalMod.Value = .Data1
            Else
                frmEditor_Item.fraVitals.Visible = False
            End If

            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Skill) Then
                frmEditor_Item.fraSkill.Visible = True
                frmEditor_Item.cmbSkills.SelectedIndex = .Data1
            Else
                frmEditor_Item.fraSkill.Visible = False
            End If

            if (frmEditor_Item.cmbType.SelectedIndex = ItemType.Projectile) Then
                frmEditor_Item.fraProjectile.Visible = True
                frmEditor_Item.fraEquipment.Visible = True
            Elseif .Type <> ItemType.Equipment Then
                frmEditor_Item.fraProjectile.Visible = False
            End If

            If frmEditor_Item.cmbType.SelectedIndex = ItemType.CommonEvent Then
                frmEditor_Item.fraEvents.Visible = True
                frmEditor_Item.nudEvent.Value = .Data1
                frmEditor_Item.nudEventValue.Value = .Data2
            Else
                frmEditor_Item.fraEvents.Visible = False
            End If

            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Pet) Then
                frmEditor_Item.fraPet.Visible = True
                frmEditor_Item.cmbPet.SelectedIndex = .Data1
            Else
                frmEditor_Item.fraPet.Visible = False
            End If

            ' Projectile
            frmEditor_Item.cmbProjectile.SelectedIndex = .Projectile
            frmEditor_Item.cmbAmmo.SelectedIndex = .Ammo

            ' Basic requirements
            frmEditor_Item.cmbAccessReq.SelectedIndex = .AccessReq
            frmEditor_Item.nudLevelReq.Value = .LevelReq

            frmEditor_Item.nudStrReq.Value = .Stat_Req(StatType.Strength)
            frmEditor_Item.nudVitReq.Value = .Stat_Req(StatType.Vitality)
            frmEditor_Item.nudLuckReq.Value = .Stat_Req(StatType.Luck)
            frmEditor_Item.nudEndReq.Value = .Stat_Req(StatType.Endurance)
            frmEditor_Item.nudIntReq.Value = .Stat_Req(StatType.Intelligence)
            frmEditor_Item.nudSprReq.Value = .Stat_Req(StatType.Spirit)

            ' Build cmbJobReq
            frmEditor_Item.cmbJobReq.Items.Clear()
           For i = 1 To MAX_JOBS
                frmEditor_Item.cmbJobReq.Items.Add(Job(i).Name)
            Next

            frmEditor_Item.cmbJobReq.SelectedIndex = .JobReq
            ' Info
            frmEditor_Item.nudPrice.Value = .Price
            frmEditor_Item.cmbBind.SelectedIndex = .BindType
            frmEditor_Item.nudRarity.Value = .Rarity

            If .Stackable = 1 Then
                frmEditor_Item.chkStackable.Checked = True
            Else
                frmEditor_Item.chkStackable.Checked = False
            End If
        End With

        EditorItem_DrawItem()
        EditorItem_DrawPaperdoll()
        Item_Changed(Editorindex) = True
    End Sub

    Friend Sub ItemEditorCancel()
        Editor = -1
        ClearChangedItem()
        ClearItems()
        SendCloseEditor()
    End Sub

    Friend Sub ItemEditorOk()
        Dim i As Integer

        For i = 1 To MAX_ITEMS
            If Item_Changed(i) Then
                SendSaveItem(i)
            End If
        Next

        Editor = -1
        ClearChangedItem()
        SendCloseEditor()
    End Sub

#End Region
End Module