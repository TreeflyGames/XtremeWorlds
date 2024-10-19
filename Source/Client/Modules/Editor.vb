Imports System.IO
Imports Core

Module Editor

#Region "Animation Editor"

    Friend Sub AnimationEditorInit()
        EditorIndex = FrmEditor_Animation.lstIndex.SelectedIndex + 1

        With Type.Animation(EditorIndex)
            If Type.Animation(EditorIndex).Sound = "" Then
                FrmEditor_Animation.cmbSound.SelectedIndex = 0
            Else
                For i = 0 To FrmEditor_Animation.cmbSound.Items.Count
                    If FrmEditor_Animation.cmbSound.GetItemText(i) = FrmEditor_Animation.cmbSound.SelectedIndex Then
                        FrmEditor_Animation.cmbSound.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
            FrmEditor_Animation.txtName.Text = .Name

            FrmEditor_Animation.nudSprite0.Value = .Sprite(0)
            FrmEditor_Animation.nudFrameCount0.Value = .Frames(0)
            If Type.Animation(EditorIndex).LoopCount(0) = 0 Then Type.Animation(EditorIndex).LoopCount(0) = 1
            FrmEditor_Animation.nudLoopCount0.Value = .LoopCount(0)
            If Type.Animation(EditorIndex).LoopTime(0) = 0 Then Type.Animation(EditorIndex).LoopTime(0) = 1
            FrmEditor_Animation.nudLoopTime0.Value = .LoopTime(0)

            FrmEditor_Animation.nudSprite1.Value = .Sprite(1)
            FrmEditor_Animation.nudFrameCount1.Value = .Frames(1)
            If Type.Animation(EditorIndex).LoopCount(1) = 0 Then Type.Animation(EditorIndex).LoopCount(1) = 1
            FrmEditor_Animation.nudLoopCount1.Value = .LoopCount(1)
            If Type.Animation(EditorIndex).LoopTime(1) = 0 Then Type.Animation(EditorIndex).LoopTime(1) = 1
            FrmEditor_Animation.nudLoopTime1.Value = .LoopTime(1)
        End With

        Animation_Changed(EditorIndex) = True
    End Sub

    Friend Sub AnimationEditorOk()
        Dim i As Integer

       For i = 1 To MAX_ANIMATIONS
            If Animation_Changed(i) Then
                SendSaveAnimation(i)
            End If
        Next

        MyEditorType = -1
        ClearChanged_Animation()
        SendCloseEditor()
    End Sub

    Friend Sub AnimationEditorCancel()
        MyEditorType = -1
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
            EditorIndex = .lstIndex.SelectedIndex + 1

            .cmbDropSlot.SelectedIndex = 0

            .txtName.Text = Type.NPC(EditorIndex).Name
            .txtAttackSay.Text = Type.NPC(EditorIndex).AttackSay
            .nudSprite.Value = Type.NPC(EditorIndex).Sprite
            .nudSpawnSecs.Value = Type.NPC(EditorIndex).SpawnSecs
            .cmbBehaviour.SelectedIndex = Type.NPC(EditorIndex).Behaviour
            .cmbFaction.SelectedIndex = Type.NPC(EditorIndex).Faction
            .nudRange.Value = Type.NPC(EditorIndex).Range
            .nudChance.Value = Type.NPC(EditorIndex).DropChance(frmEditor_NPC.cmbDropSlot.SelectedIndex)
            .cmbItem.SelectedIndex = Type.NPC(EditorIndex).DropItem(frmEditor_NPC.cmbDropSlot.SelectedIndex)

            .nudAmount.Value =Type. NPC(EditorIndex).DropItemValue(frmEditor_NPC.cmbDropSlot.SelectedIndex)

            .nudHp.Value = Type.NPC(EditorIndex).HP
            .nudExp.Value = Type.NPC(EditorIndex).Exp
            .nudLevel.Value = Type.NPC(EditorIndex).Level
            .nudDamage.Value = Type.NPC(EditorIndex).Damage

            .cmbSpawnPeriod.SelectedIndex = Type.NPC(EditorIndex).SpawnTime

            .cmbAnimation.SelectedIndex = Type.NPC(EditorIndex).Animation

            .nudStrength.Value = Type.NPC(EditorIndex).Stat(StatType.Strength)
            .nudIntelligence.Value = Type.NPC(EditorIndex).Stat(StatType.Intelligence)
            .nudSpirit.Value = Type.NPC(EditorIndex).Stat(StatType.Spirit)
            .nudLuck.Value = Type.NPC(EditorIndex).Stat(StatType.Luck)
            .nudVitality.Value = Type.NPC(EditorIndex).Stat(StatType.Vitality)

            .cmbSkill1.SelectedIndex = Type.NPC(EditorIndex).Skill(1)
            .cmbSkill2.SelectedIndex = Type.NPC(EditorIndex).Skill(2)
            .cmbSkill3.SelectedIndex = Type.NPC(EditorIndex).Skill(3)
            .cmbSkill4.SelectedIndex = Type.NPC(EditorIndex).Skill(4)
            .cmbSkill5.SelectedIndex = Type.NPC(EditorIndex).Skill(5)
            .cmbSkill6.SelectedIndex = Type.NPC(EditorIndex).Skill(6)
        End With

        Client.EditorNpc_DrawSprite()
        NPC_Changed(EditorIndex) = True
    End Sub

    Friend Sub NPCEditorOk()
        Dim i As Integer

        For i = 1 To MAX_NPCS
            If NPC_Changed(i) Then
                SendSaveNPC(i)
            End If
        Next

        MyEditorType = -1
        ClearChanged_NPC()
        SendCloseEditor()
    End Sub

    Friend Sub NpcEditorCancel()
        MyEditorType = -1
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

        EditorIndex = frmEditor_Resource.lstIndex.SelectedIndex + 1

        With frmEditor_Resource
            .txtName.Text = Type.Resource(EditorIndex).Name
            .txtMessage.Text = Type.Resource(EditorIndex).SuccessMessage
            .txtMessage2.Text = Type.Resource(EditorIndex).EmptyMessage
            .cmbType.SelectedIndex = Type.Resource(EditorIndex).ResourceType
            .nudNormalPic.Value = Type.Resource(EditorIndex).ResourceImage
            .nudExhaustedPic.Value = Type.Resource(EditorIndex).ExhaustedImage
            .cmbRewardItem.SelectedIndex = Type.Resource(EditorIndex).ItemReward
            .nudRewardExp.Value = Type.Resource(EditorIndex).ExpReward
            .cmbTool.SelectedIndex = Type.Resource(EditorIndex).ToolRequired
            .nudHealth.Value = Type.Resource(EditorIndex).Health
            .nudRespawn.Value = Type.Resource(EditorIndex).RespawnTime
            .cmbAnimation.SelectedIndex = Type.Resource(EditorIndex).Animation
            .nudLvlReq.Value = Type.Resource(EditorIndex).LvlRequired
        End With

        frmEditor_Resource.Visible = True

        Client.EditorResource_DrawSprite()

        Resource_Changed(EditorIndex) = True
    End Sub

    Friend Sub ResourceEditorOk()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            If Resource_Changed(i) Then
                SendSaveResource(i)
            End If
        Next

        MyEditorType = -1
        ClearChanged_Resource()
        SendCloseEditor()
    End Sub

    Friend Sub ResourceEditorCancel()
        MyEditorType = -1
        ClearChanged_Resource()
        ClearResources()
        SendCloseEditor()
    End Sub

#End Region

#Region "Skill Editor"

    Friend Sub SkillEditorInit()
        With frmEditor_Skill
            EditorIndex = .lstIndex.SelectedIndex + 1

            .cmbAnimCast.SelectedIndex = 0
            .cmbAnim.SelectedIndex = 0

            ' set values
            .txtName.Text = Type.Skill(EditorIndex).Name
            .cmbType.SelectedIndex = Type.Skill(EditorIndex).Type
            .nudMp.Value = Type.Skill(EditorIndex).MpCost
            .nudLevel.Value = Type.Skill(EditorIndex).LevelReq
            .cmbAccessReq.SelectedIndex = Type.Skill(EditorIndex).AccessReq
            .cmbJob.SelectedIndex = Type.Skill(EditorIndex).JobReq
            .nudCast.Value = Type.Skill(EditorIndex).CastTime
            .nudCool.Value = Type.Skill(EditorIndex).CdTime
            .nudIcon.Value = Type.Skill(EditorIndex).Icon
            .nudMap.Value = Type.Skill(EditorIndex).Map
            .nudX.Value = Type.Skill(EditorIndex).X
            .nudY.Value = Type.Skill(EditorIndex).Y
            .cmbDir.SelectedIndex = Type.Skill(EditorIndex).Dir
            .nudVital.Value = Type.Skill(EditorIndex).Vital
            .nudDuration.Value = Type.Skill(EditorIndex).Duration
            .nudInterval.Value = Type.Skill(EditorIndex).Interval
            .nudRange.Value = Type.Skill(EditorIndex).Range

            .chkAoE.Checked = Type.Skill(EditorIndex).IsAoE

            .nudAoE.Value = Type.Skill(EditorIndex).AoE
            .cmbAnimCast.SelectedIndex = Type.Skill(EditorIndex).CastAnim
            .cmbAnim.SelectedIndex = Type.Skill(EditorIndex).SkillAnim
            .nudStun.Value = Type.Skill(EditorIndex).StunDuration

            If Type.Skill(EditorIndex).IsProjectile = 1 Then
                .chkProjectile.Checked = True
            Else
                .chkProjectile.Checked = False
            End If
            .cmbProjectile.SelectedIndex = Type.Skill(EditorIndex).Projectile

            If Type.Skill(EditorIndex).KnockBack = 1 Then
                .chkKnockBack.Checked = True
            Else
                .chkKnockBack.Checked = False
            End If
            .cmbKnockBackTiles.SelectedIndex = Type.Skill(EditorIndex).KnockBackTiles
        End With

        Skill_Changed(EditorIndex) = True
        Client.EditorSkill_DrawIcon()
    End Sub

    Friend Sub SkillEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            If Skill_Changed(i) Then
                SendSaveSkill(i)
            End If
        Next

        MyEditorType = -1
        ClearChanged_Skill()
        SendCloseEditor()
    End Sub

    Friend Sub SkillEditorCancel()
        MyEditorType = -1
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
        EditorIndex = frmEditor_Shop.lstIndex.SelectedIndex + 1

        With frmEditor_Shop
            .txtName.Text = Type.Shop(EditorIndex).Name

            If Type.Shop(EditorIndex).BuyRate > 0 Then
                .nudBuy.Value = Type.Shop(EditorIndex).BuyRate
            Else
                .nudBuy.Value = 100
            End If

            .cmbItem.SelectedIndex = 0
            .cmbCostItem.SelectedIndex = 0
        End With

        UpdateShopTrade()
        Shop_Changed(EditorIndex) = True
    End Sub

    Friend Sub UpdateShopTrade()
        Dim i As Integer

        frmEditor_Shop.lstTradeItem.Items.Clear()

        For i = 1 To MAX_TRADES
            With Type.Shop(EditorIndex).TradeItem(i)
                ' if none, show as none
                If .Item = 0 And .CostItem = 0 Then
                    frmEditor_Shop.lstTradeItem.Items.Add("Empty Trade Slot")
                Else
                    frmEditor_Shop.lstTradeItem.Items.Add(i & ": " & .ItemValue & "x " & Type.Item(.Item).Name & " for " & .CostValue & "x " & Type.Item(.CostItem).Name)
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

        MyEditorType = -1
        ClearChanged_Shop()
        SendCloseEditor()
    End Sub

    Friend Sub ShopEditorCancel()
        MyEditorType = -1
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
        MyEditorType = -1
        SendCloseEditor()
    End Sub

    Friend Sub JobEditorCancel()
        MyEditorType = -1
        ClearChangedJob()
        ClearJobs()
        SendCloseEditor()
    End Sub

    Friend Sub JobEditorInit()
        Dim i As Integer

        With frmEditor_Job
            EditorIndex = .lstIndex.SelectedIndex + 1

            .txtName.Text = Type.Job(EditorIndex).Name
            .txtDescription.Text = Type.Job(EditorIndex).Desc

            If Type.Job(EditorIndex).MaleSprite = 0 Then Type.Job(EditorIndex).MaleSprite = 1
            .nudMaleSprite.Value = Type.Job(EditorIndex).MaleSprite
            If Type.Job(EditorIndex).FemaleSprite = 0 Then Type.Job(EditorIndex).FemaleSprite = 1
            .nudFemaleSprite.Value = Type.Job(EditorIndex).FemaleSprite

            .cmbItems.SelectedIndex = 0

            For i = 1 To StatType.Count - 1
                If Type.Job(EditorIndex).Stat(i) = 0 Then Type.Job(EditorIndex).Stat(i) = 1
            Next

            .nudStrength.Value = Type.Job(EditorIndex).Stat(StatType.Strength)
            .nudLuck.Value = Type.Job(EditorIndex).Stat(StatType.Luck)
            .nudIntelligence.Value = Type.Job(EditorIndex).Stat(StatType.Intelligence)
            .nudVitality.Value = Type.Job(EditorIndex).Stat(StatType.Vitality)
            .nudSpirit.Value = Type.Job(EditorIndex).Stat(StatType.Spirit)
            .nudBaseExp.Value = Type.Job(EditorIndex).BaseExp

            If Type.Job(EditorIndex).StartMap = 0 Then Type.Job(EditorIndex).StartMap = 1
            .nudStartMap.Value = Type.Job(EditorIndex).StartMap
            .nudStartX.Value = Type.Job(EditorIndex).StartX
            .nudStartY.Value = Type.Job(EditorIndex).StartY

            Job_Changed(EditorIndex) = True
            .DrawPreview()
        End With
    End Sub

    Friend Sub ClearChangedJob()
       For i = 1 To MAX_JOBS
            Job_Changed(i) = False
        Next
    End Sub


#End Region

#Region "Item"

    Friend Sub ItemEditorInit()
        Dim i As Integer

        EditorIndex = frmEditor_Item.lstIndex.SelectedIndex + 1

        With Type.Item(EditorIndex)
            frmEditor_Item.txtName.Text = .Name
            frmEditor_Item.txtDescription.Text = .Description

            If .Icon > frmEditor_Item.nudIcon.Maximum Then .Icon = 0
            frmEditor_Item.nudIcon.Value = .Icon
            If .Type > ItemType.Count - 1 Then .Type = 0
            frmEditor_Item.cmbType.SelectedIndex = .Type
            frmEditor_Item.cmbAnimation.SelectedIndex = .Animation

            If .ItemLevel = 0 Then .ItemLevel = 1
            frmEditor_Item.nudItemLvl.Value = .ItemLevel

            ' Type specific Setting
            If (frmEditor_Item.cmbType.SelectedIndex = ItemType.Equipment) Then
                frmEditor_Item.fraEquipment.Visible = True
                frmEditor_Item.nudDamage.Value = .Data2
                frmEditor_Item.cmbTool.SelectedIndex = .Data3

                frmEditor_Item.cmbSubType.SelectedIndex = .SubType

                If .Speed < 1000 Then .Speed = 100
                If .Speed > frmEditor_Item.nudSpeed.Maximum Then .Speed = frmEditor_Item.nudSpeed.Maximum
                frmEditor_Item.nudSpeed.Value = .Speed

                frmEditor_Item.nudStrength.Value = .Add_Stat(StatType.Strength)
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
            frmEditor_Item.nudIntReq.Value = .Stat_Req(StatType.Intelligence)
            frmEditor_Item.nudSprReq.Value = .Stat_Req(StatType.Spirit)

            ' Build cmbJobReq
            frmEditor_Item.cmbJobReq.Items.Clear()
           For i = 1 To MAX_JOBS
                frmEditor_Item.cmbJobReq.Items.Add(Type.Job(i).Name)
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

        Item_Changed(EditorIndex) = True
        Client.EditorItem_DrawIcon()
        Client.EditorItem_DrawPaperdoll()
    End Sub

    Friend Sub ItemEditorCancel()
        MyEditorType = -1
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

        MyEditorType = -1
        ClearChangedItem()
        SendCloseEditor()
    End Sub

#End Region

#Region "Moral Editor"
    Friend Sub MoralEditorOk()
        For i = 1 To MAX_MORALS
            If Moral_Changed(i) Then
                SendSaveMoral(i)
            End If
        Next
        MyEditorType = -1
        SendCloseEditor()
    End Sub

    Friend Sub MoralEditorCancel()
        MyEditorType = -1
        ClearChanged_Moral()
        ClearMorals()
        SendCloseEditor()
    End Sub

    Friend Sub MoralEditorInit()
        Dim i As Integer

        With frmEditor_Moral
            EditorIndex = .lstIndex.SelectedIndex + 1

            .txtName.Text = Type.Moral(EditorIndex).Name
            .cmbColor.SelectedIndex = Type.Moral(EditorIndex).Color
            .chkCanCast.Checked = Type.Moral(EditorIndex).CanCast
            .chkCanPK.Checked = Type.Moral(EditorIndex).CanPK
            .chkCanPickupItem.Checked = Type.Moral(EditorIndex).CanPickupItem
            .chkCanDropItem.Checked = Type.Moral(EditorIndex).CanDropItem
            .chkCanUseItem.Checked = Type.Moral(EditorIndex).CanUseItem
            .chkDropItems.Checked = Type.Moral(EditorIndex).DropItems
            .chkLoseExp.Checked = Type.Moral(EditorIndex).LoseExp
            .chkPlayerBlock.Checked = Type.Moral(EditorIndex).PlayerBlock
            .chkNPCBlock.Checked = Type.Moral(EditorIndex).NPCBlock
        
            Moral_Changed(EditorIndex) = True
        End With
    End Sub

    Friend Sub ClearChanged_Moral()
       For i = 1 To MAX_MORALS
            Moral_Changed(i) = False
        Next
    End Sub
#End Region
End Module