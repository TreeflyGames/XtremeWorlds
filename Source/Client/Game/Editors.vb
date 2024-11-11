Imports System.IO
Imports Core

Module Editors

#Region "Animation Editor"

    Friend Sub AnimationEditorInit()
        GameState.EditorIndex = frmEditor_Animation.Instance.lstIndex.SelectedIndex + 1

        With Type.Animation(GameState.EditorIndex)
            If Type.Animation(GameState.EditorIndex).Sound = "" Then
                frmEditor_Animation.Instance.cmbSound.SelectedIndex = 0
            Else
                For i = 0 To frmEditor_Animation.Instance.cmbSound.Items.Count
                    If frmEditor_Animation.Instance.cmbSound.GetItemText(i) = frmEditor_Animation.Instance.cmbSound.SelectedIndex Then
                        frmEditor_Animation.Instance.cmbSound.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
            frmEditor_Animation.Instance.txtName.Text = Trim$(.Name)

            frmEditor_Animation.Instance.nudSprite0.Value = .Sprite(0)
            frmEditor_Animation.Instance.nudFrameCount0.Value = .Frames(0)
            If Type.Animation(GameState.EditorIndex).LoopCount(0) = 0 Then Type.Animation(GameState.EditorIndex).LoopCount(0) = 1
            frmEditor_Animation.Instance.nudLoopCount0.Value = .LoopCount(0)
            If Type.Animation(GameState.EditorIndex).LoopTime(0) = 0 Then Type.Animation(GameState.EditorIndex).LoopTime(0) = 1
            frmEditor_Animation.Instance.nudLoopTime0.Value = .LoopTime(0)

            frmEditor_Animation.Instance.nudSprite1.Value = .Sprite(1)
            frmEditor_Animation.Instance.nudFrameCount1.Value = .Frames(1)
            If Type.Animation(GameState.EditorIndex).LoopCount(1) = 0 Then Type.Animation(GameState.EditorIndex).LoopCount(1) = 1
            frmEditor_Animation.Instance.nudLoopCount1.Value = .LoopCount(1)
            If Type.Animation(GameState.EditorIndex).LoopTime(1) = 0 Then Type.Animation(GameState.EditorIndex).LoopTime(1) = 1
            frmEditor_Animation.Instance.nudLoopTime1.Value = .LoopTime(1)
        End With

        GameState.Animation_Changed(GameState.EditorIndex) = True
    End Sub

    Friend Sub AnimationEditorOk()
        Dim i As Integer

        For i = 1 To MAX_ANIMATIONS
            If GameState.Animation_Changed(i) Then
                SendSaveAnimation(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChanged_Animation()
        SendCloseEditor()
    End Sub

    Friend Sub AnimationEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Animation()
        ClearAnimations()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Animation()
        For i = 1 To MAX_ANIMATIONS
            GameState.Animation_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Npc Editor"

    Friend Sub NpcEditorInit()
        With frmEditor_NPC.Instance
            GameState.EditorIndex = .lstIndex.SelectedIndex + 1

            .cmbDropSlot.SelectedIndex = 0

            .txtName.Text = NPC(GameState.EditorIndex).Name
            .txtAttackSay.Text = NPC(GameState.EditorIndex).AttackSay
            .nudSprite.Value = NPC(GameState.EditorIndex).Sprite
            .nudSpawnSecs.Value = NPC(GameState.EditorIndex).SpawnSecs
            .cmbBehaviour.SelectedIndex = NPC(GameState.EditorIndex).Behaviour
            .cmbFaction.SelectedIndex = NPC(GameState.EditorIndex).Faction
            .nudRange.Value = NPC(GameState.EditorIndex).Range
            .nudChance.Value = NPC(GameState.EditorIndex).DropChance(frmEditor_NPC.Instance.cmbDropSlot.SelectedIndex)
            .cmbItem.SelectedIndex = NPC(GameState.EditorIndex).DropItem(frmEditor_NPC.Instance.cmbDropSlot.SelectedIndex)

            .nudAmount.Value = NPC(GameState.EditorIndex).DropItemValue(frmEditor_NPC.Instance.cmbDropSlot.SelectedIndex)

            .nudHp.Value = NPC(GameState.EditorIndex).HP
            .nudExp.Value = NPC(GameState.EditorIndex).Exp
            .nudLevel.Value = NPC(GameState.EditorIndex).Level
            .nudDamage.Value = NPC(GameState.EditorIndex).Damage

            .cmbSpawnPeriod.SelectedIndex = NPC(GameState.EditorIndex).SpawnTime

            .cmbAnimation.SelectedIndex = NPC(GameState.EditorIndex).Animation

            .nudStrength.Value = NPC(GameState.EditorIndex).Stat(StatType.Strength)
            .nudIntelligence.Value = NPC(GameState.EditorIndex).Stat(StatType.Intelligence)
            .nudSpirit.Value = NPC(GameState.EditorIndex).Stat(StatType.Spirit)
            .nudLuck.Value = NPC(GameState.EditorIndex).Stat(StatType.Luck)
            .nudVitality.Value = NPC(GameState.EditorIndex).Stat(StatType.Vitality)

            .cmbSkill1.SelectedIndex = NPC(GameState.EditorIndex).Skill(1)
            .cmbSkill2.SelectedIndex = NPC(GameState.EditorIndex).Skill(2)
            .cmbSkill3.SelectedIndex = NPC(GameState.EditorIndex).Skill(3)
            .cmbSkill4.SelectedIndex = NPC(GameState.EditorIndex).Skill(4)
            .cmbSkill5.SelectedIndex = NPC(GameState.EditorIndex).Skill(5)
            .cmbSkill6.SelectedIndex = NPC(GameState.EditorIndex).Skill(6)
        End With

        GameClient.EditorNpc_DrawSprite()
        GameState.NPC_Changed(GameState.EditorIndex) = True
    End Sub

    Friend Sub NpcEditorOk()
        Dim i As Integer

        For i = 1 To MAX_NPCS
            If GameState.NPC_Changed(i) Then
                SendSaveNPC(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChanged_NPC()
        SendCloseEditor()
    End Sub

    Friend Sub NpcEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_NPC()
        ClearNPCs()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_NPC()
        For i = 1 To MAX_NPCS
            GameState.NPC_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Resource Editor"
    Friend Sub ClearChanged_Resource()
        ReDim GameState.Resource_Changed(MAX_RESOURCES)
    End Sub

    Friend Sub ResourceEditorInit()
        Dim i As Integer

        GameState.EditorIndex = frmEditor_Resource.Instance.lstIndex.SelectedIndex + 1

        With frmEditor_Resource.Instance
            .txtName.Text = Type.Resource(GameState.EditorIndex).Name
            .txtMessage.Text = Type.Resource(GameState.EditorIndex).SuccessMessage
            .txtMessage2.Text = Type.Resource(GameState.EditorIndex).EmptyMessage
            .cmbType.SelectedIndex = Type.Resource(GameState.EditorIndex).ResourceType
            .nudNormalPic.Value = Type.Resource(GameState.EditorIndex).ResourceImage
            .nudExhaustedPic.Value = Type.Resource(GameState.EditorIndex).ExhaustedImage
            .cmbRewardItem.SelectedIndex = Type.Resource(GameState.EditorIndex).ItemReward
            .nudRewardExp.Value = Type.Resource(GameState.EditorIndex).ExpReward
            .cmbTool.SelectedIndex = Type.Resource(GameState.EditorIndex).ToolRequired
            .nudHealth.Value = Type.Resource(GameState.EditorIndex).Health
            .nudRespawn.Value = Type.Resource(GameState.EditorIndex).RespawnTime
            .cmbAnimation.SelectedIndex = Type.Resource(GameState.EditorIndex).Animation
            .nudLvlReq.Value = Type.Resource(GameState.EditorIndex).LvlRequired
        End With

        frmEditor_Resource.Instance.Visible = True

        GameClient.EditorResource_DrawSprite()

        GameState.Resource_Changed(GameState.EditorIndex) = True
    End Sub

    Friend Sub ResourceEditorOk()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            If GameState.Resource_Changed(i) Then
                SendSaveResource(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChanged_Resource()
        SendCloseEditor()
    End Sub

    Friend Sub ResourceEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Resource()
        ClearResources()
        SendCloseEditor()
    End Sub

#End Region

#Region "Skill Editor"

    Friend Sub SkillEditorInit()
        With frmEditor_Skill.Instance
            GameState.EditorIndex = .lstIndex.SelectedIndex + 1

            .cmbAnimCast.SelectedIndex = 0
            .cmbAnim.SelectedIndex = 0

            ' set values
            .txtName.Text = Trim$(Skill(GameState.EditorIndex).Name)
            .cmbType.SelectedIndex = Skill(GameState.EditorIndex).Type
            .nudMp.Value = Skill(GameState.EditorIndex).MpCost
            .nudLevel.Value = Skill(GameState.EditorIndex).LevelReq
            .cmbAccessReq.SelectedIndex = Skill(GameState.EditorIndex).AccessReq
            .cmbJob.SelectedIndex = Skill(GameState.EditorIndex).JobReq
            .nudCast.Value = Skill(GameState.EditorIndex).CastTime
            .nudCool.Value = Skill(GameState.EditorIndex).CdTime
            .nudIcon.Value = Skill(GameState.EditorIndex).Icon
            .nudMap.Value = Skill(GameState.EditorIndex).Map
            .nudX.Value = Skill(GameState.EditorIndex).X
            .nudY.Value = Skill(GameState.EditorIndex).Y
            .cmbDir.SelectedIndex = Skill(GameState.EditorIndex).Dir
            .nudVital.Value = Skill(GameState.EditorIndex).Vital
            .nudDuration.Value = Skill(GameState.EditorIndex).Duration
            .nudInterval.Value = Skill(GameState.EditorIndex).Interval
            .nudRange.Value = Skill(GameState.EditorIndex).Range

            .chkAoE.Checked = Skill(GameState.EditorIndex).IsAoE

            .nudAoE.Value = Skill(GameState.EditorIndex).AoE
            .cmbAnimCast.SelectedIndex = Skill(GameState.EditorIndex).CastAnim
            .cmbAnim.SelectedIndex = Skill(GameState.EditorIndex).SkillAnim
            .nudStun.Value = Skill(GameState.EditorIndex).StunDuration

            If Skill(GameState.EditorIndex).IsProjectile = 1 Then
                .chkProjectile.Checked = True
            Else
                .chkProjectile.Checked = False
            End If
            .cmbProjectile.SelectedIndex = Skill(GameState.EditorIndex).Projectile

            If Skill(GameState.EditorIndex).KnockBack = 1 Then
                .chkKnockBack.Checked = True
            Else
                .chkKnockBack.Checked = False
            End If
            .cmbKnockBackTiles.SelectedIndex = Skill(GameState.EditorIndex).KnockBackTiles
        End With

        GameState.Skill_Changed(GameState.EditorIndex) = True
        GameClient.EditorSkill_DrawIcon()
    End Sub

    Friend Sub SkillEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            If GameState.Skill_Changed(i) Then
                SendSaveSkill(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChanged_Skill()
        SendCloseEditor()
    End Sub

    Friend Sub SkillEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Skill()
        ClearSkills()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Skill()
        For i = 1 To MAX_SKILLS
            GameState.Skill_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Shop editor"
    Friend Sub ShopEditorInit()
        GameState.EditorIndex = frmEditor_Shop.Instance.lstIndex.SelectedIndex + 1

        With frmEditor_Shop.Instance
            .txtName.Text = Type.Shop(GameState.EditorIndex).Name

            If Type.Shop(GameState.EditorIndex).BuyRate > 0 Then
                .nudBuy.Value = Type.Shop(GameState.EditorIndex).BuyRate
            Else
                .nudBuy.Value = 100
            End If

            .cmbItem.SelectedIndex = 0
            .cmbCostItem.SelectedIndex = 0
        End With

        UpdateShopTrade()
        GameState.Shop_Changed(GameState.EditorIndex) = True
    End Sub

    Friend Sub UpdateShopTrade()
        Dim i As Integer

        frmEditor_Shop.Instance.lstTradeItem.Items.Clear()

        For i = 1 To MAX_TRADES
            With Type.Shop(GameState.EditorIndex).TradeItem(i)
                ' if none, show as none
                If .Item = 0 And .CostItem = 0 Then
                    frmEditor_Shop.Instance.lstTradeItem.Items.Add("Empty Trade Slot")
                Else
                    frmEditor_Shop.Instance.lstTradeItem.Items.Add(i & ": " & .ItemValue & "x " & Type.Item(.Item).Name & " for " & .CostValue & "x " & Type.Item(.CostItem).Name)
                End If
            End With
        Next

        frmEditor_Shop.Instance.lstTradeItem.SelectedIndex = 0
    End Sub

    Friend Sub ShopEditorOk()
        Dim i As Integer

        For i = 1 To MAX_SHOPS
            If GameState.Shop_Changed(i) Then
                SendSaveShop(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChanged_Shop()
        SendCloseEditor()
    End Sub

    Friend Sub ShopEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Shop()
        ClearShops()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Shop()
        For i = 1 To MAX_SHOPS
            GameState.Shop_Changed(i) = False
        Next
    End Sub

#End Region

#Region "Job Editor"
    Friend Sub JobEditorOk()
        For i = 1 To MAX_JOBS
            If GameState.Job_Changed(i) Then
                SendSaveJob(i)
            End If
        Next
        GameState.MyEditorType = -1
        SendCloseEditor()
    End Sub

    Friend Sub JobEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Job()
        ClearJobs()
        SendCloseEditor()
    End Sub

    Friend Sub JobEditorInit()
        Dim i As Integer

        With frmEditor_Job.Instance
            GameState.EditorIndex = .lstIndex.SelectedIndex + 1

            .txtName.Text = Job(GameState.EditorIndex).Name
            .txtDescription.Text = Job(GameState.EditorIndex).Desc

            If Job(GameState.EditorIndex).MaleSprite = 0 Then Job(GameState.EditorIndex).MaleSprite = 1
            .nudMaleSprite.Value = Job(GameState.EditorIndex).MaleSprite
            If Job(GameState.EditorIndex).FemaleSprite = 0 Then Job(GameState.EditorIndex).FemaleSprite = 1
            .nudFemaleSprite.Value = Job(GameState.EditorIndex).FemaleSprite

            .cmbItems.SelectedIndex = 0

            For i = 1 To StatType.Count - 1
                If Job(GameState.EditorIndex).Stat(i) = 0 Then Job(GameState.EditorIndex).Stat(i) = 1
            Next

            .nudStrength.Value = Job(GameState.EditorIndex).Stat(StatType.Strength)
            .nudLuck.Value = Job(GameState.EditorIndex).Stat(StatType.Luck)
            .nudIntelligence.Value = Job(GameState.EditorIndex).Stat(StatType.Intelligence)
            .nudVitality.Value = Job(GameState.EditorIndex).Stat(StatType.Vitality)
            .nudSpirit.Value = Job(GameState.EditorIndex).Stat(StatType.Spirit)
            .nudBaseExp.Value = Job(GameState.EditorIndex).BaseExp

            If Job(GameState.EditorIndex).StartMap = 0 Then Job(GameState.EditorIndex).StartMap = 1
            .nudStartMap.Value = Job(GameState.EditorIndex).StartMap
            .nudStartX.Value = Job(GameState.EditorIndex).StartX
            .nudStartY.Value = Job(GameState.EditorIndex).StartY

            GameState.Job_Changed(GameState.EditorIndex) = True
            .DrawPreview()
        End With
    End Sub

    Friend Sub ClearChanged_Job()
        For i = 1 To MAX_JOBS
            GameState.Job_Changed(i) = False
        Next
    End Sub


#End Region

#Region "Item"

    Friend Sub ItemEditorInit()
        Dim i As Integer

        GameState.EditorIndex = frmEditor_item.Instance.lstIndex.SelectedIndex + 1

        With Type.Item(GameState.EditorIndex)
            frmEditor_item.Instance.txtName.Text = .Name
            frmEditor_item.Instance.txtDescription.Text = .Description

            If .Icon > frmEditor_item.Instance.nudIcon.Maximum Then .Icon = 0
            frmEditor_item.Instance.nudIcon.Value = .Icon
            If .Type > ItemType.Count - 1 Then .Type = 0
            frmEditor_item.Instance.cmbType.SelectedIndex = .Type
            frmEditor_item.Instance.cmbAnimation.SelectedIndex = .Animation

            If .ItemLevel = 0 Then .ItemLevel = 1
            frmEditor_item.Instance.nudItemLvl.Value = .ItemLevel

            ' Type specific settings
            If (frmEditor_item.Instance.cmbType.SelectedIndex = ItemType.Equipment) Then
                frmEditor_item.Instance.fraEquipment.Visible = True
                frmEditor_item.Instance.nudDamage.Value = .Data2
                frmEditor_item.Instance.cmbTool.SelectedIndex = .Data3

                frmEditor_item.Instance.cmbSubType.SelectedIndex = .SubType

                If .Speed < 1000 Then .Speed = 100
                If .Speed > frmEditor_item.Instance.nudSpeed.Maximum Then .Speed = frmEditor_item.Instance.nudSpeed.Maximum
                frmEditor_item.Instance.nudSpeed.Value = .Speed

                frmEditor_item.Instance.nudStrength.Value = .Add_Stat(StatType.Strength)
                frmEditor_item.Instance.nudIntelligence.Value = .Add_Stat(StatType.Intelligence)
                frmEditor_item.Instance.nudVitality.Value = .Add_Stat(StatType.Vitality)
                frmEditor_item.Instance.nudLuck.Value = .Add_Stat(StatType.Luck)
                frmEditor_item.Instance.nudSpirit.Value = .Add_Stat(StatType.Spirit)

                If .KnockBack = 1 Then
                    frmEditor_item.Instance.chkKnockBack.Checked = True
                Else
                    frmEditor_item.Instance.chkKnockBack.Checked = False
                End If
                frmEditor_item.Instance.cmbKnockBackTiles.SelectedIndex = .KnockBackTiles
                frmEditor_item.Instance.nudPaperdoll.Value = .Paperdoll

                If .SubType = EquipmentType.Weapon Then
                    frmEditor_item.Instance.fraProjectile.Visible = True
                Else
                    frmEditor_item.Instance.fraProjectile.Visible = False
                End If
            Else
                frmEditor_item.Instance.fraEquipment.Visible = False
            End If

            If (frmEditor_item.Instance.cmbType.SelectedIndex = ItemType.Consumable) Then
                frmEditor_item.Instance.fraVitals.Visible = True
                frmEditor_item.Instance.nudVitalMod.Value = .Data1
            Else
                frmEditor_item.Instance.fraVitals.Visible = False
            End If

            If (frmEditor_item.Instance.cmbType.SelectedIndex = ItemType.Skill) Then
                frmEditor_item.Instance.fraSkill.Visible = True
                frmEditor_item.Instance.cmbSkills.SelectedIndex = .Data1
            Else
                frmEditor_item.Instance.fraSkill.Visible = False
            End If

            If (frmEditor_item.Instance.cmbType.SelectedIndex = ItemType.Projectile) Then
                frmEditor_item.Instance.fraProjectile.Visible = True
                frmEditor_item.Instance.fraEquipment.Visible = True
            ElseIf .Type <> ItemType.Equipment Then
                frmEditor_item.Instance.fraProjectile.Visible = False
            End If

            If frmEditor_item.Instance.cmbType.SelectedIndex = ItemType.CommonEvent Then
                frmEditor_item.Instance.fraEvents.Visible = True
                frmEditor_item.Instance.nudEvent.Value = .Data1
                frmEditor_item.Instance.nudEventValue.Value = .Data2
            Else
                frmEditor_item.Instance.fraEvents.Visible = False
            End If

            If (frmEditor_item.Instance.cmbType.SelectedIndex = ItemType.Pet) Then
                frmEditor_item.Instance.fraPet.Visible = True
                frmEditor_item.Instance.cmbPet.SelectedIndex = .Data1
            Else
                frmEditor_item.Instance.fraPet.Visible = False
            End If

            ' Projectile
            frmEditor_item.Instance.cmbProjectile.SelectedIndex = .Projectile
            frmEditor_item.Instance.cmbAmmo.SelectedIndex = .Ammo

            ' Basic requirements
            frmEditor_item.Instance.cmbAccessReq.SelectedIndex = .AccessReq
            frmEditor_item.Instance.nudLevelReq.Value = .LevelReq

            frmEditor_item.Instance.nudStrReq.Value = .Stat_Req(StatType.Strength)
            frmEditor_item.Instance.nudVitReq.Value = .Stat_Req(StatType.Vitality)
            frmEditor_item.Instance.nudLuckReq.Value = .Stat_Req(StatType.Luck)
            frmEditor_item.Instance.nudIntReq.Value = .Stat_Req(StatType.Intelligence)
            frmEditor_item.Instance.nudSprReq.Value = .Stat_Req(StatType.Spirit)

            ' Build cmbJobReq
            frmEditor_item.Instance.cmbJobReq.Items.Clear()
            For i = 1 To MAX_JOBS
                frmEditor_item.Instance.cmbJobReq.Items.Add(Job(i).Name)
            Next

            frmEditor_item.Instance.cmbJobReq.SelectedIndex = .JobReq
            ' Info
            frmEditor_item.Instance.nudPrice.Value = .Price
            frmEditor_item.Instance.cmbBind.SelectedIndex = .BindType
            frmEditor_item.Instance.nudRarity.Value = .Rarity

            If .Stackable = 1 Then
                frmEditor_item.Instance.chkStackable.Checked = True
            Else
                frmEditor_item.Instance.chkStackable.Checked = False
            End If
        End With

        GameState.Item_Changed(GameState.EditorIndex) = True
        GameClient.EditorItem_DrawIcon()
        GameClient.EditorItem_DrawPaperdoll()
    End Sub

    Friend Sub ItemEditorCancel()
        GameState.MyEditorType = -1
        ClearChangedItem()
        ClearItems()
        SendCloseEditor()
    End Sub

    Friend Sub ItemEditorOk()
        Dim i As Integer

        For i = 1 To MAX_ITEMS
            If GameState.Item_Changed(i) Then
                SendSaveItem(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChangedItem()
        SendCloseEditor()
    End Sub

#End Region

#Region "Moral Editor"
    Friend Sub MoralEditorOk()
        For i = 1 To MAX_MORALS
            If GameState.Moral_Changed(i) Then
                SendSaveMoral(i)
            End If
        Next
        GameState.MyEditorType = -1
        SendCloseEditor()
    End Sub

    Friend Sub MoralEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Moral()
        ClearMorals()
        SendCloseEditor()
    End Sub

    Friend Sub MoralEditorInit()
        Dim i As Integer

        With frmEditor_Moral.Instance
            GameState.EditorIndex = .lstIndex.SelectedIndex + 1

            .txtName.Text = Type.Moral(GameState.EditorIndex).Name
            .cmbColor.SelectedIndex = Type.Moral(GameState.EditorIndex).Color
            .chkCanCast.Checked = Type.Moral(GameState.EditorIndex).CanCast
            .chkCanPK.Checked = Type.Moral(GameState.EditorIndex).CanPK
            .chkCanPickupItem.Checked = Type.Moral(GameState.EditorIndex).CanPickupItem
            .chkCanDropItem.Checked = Type.Moral(GameState.EditorIndex).CanDropItem
            .chkCanUseItem.Checked = Type.Moral(GameState.EditorIndex).CanUseItem
            .chkDropItems.Checked = Type.Moral(GameState.EditorIndex).DropItems
            .chkLoseExp.Checked = Type.Moral(GameState.EditorIndex).LoseExp
            .chkPlayerBlock.Checked = Type.Moral(GameState.EditorIndex).PlayerBlock
            .chkNPCBlock.Checked = Type.Moral(GameState.EditorIndex).NPCBlock

            GameState.Moral_Changed(GameState.EditorIndex) = True
        End With
    End Sub

    Friend Sub ClearChanged_Moral()
        For i = 1 To MAX_MORALS
            GameState.Moral_Changed(i) = False
        Next
    End Sub
#End Region

#Region "Projectile Editor"
    Friend Sub ProjectileEditorInit()
        GameState.EditorIndex = frmEditor_Projectile.Instance.lstIndex.SelectedIndex + 1

        With Type.Projectile(GameState.EditorIndex)
            frmEditor_Projectile.Instance.txtName.Text = Trim$(.Name)
            frmEditor_Projectile.Instance.nudPic.Value = .Sprite
            frmEditor_Projectile.Instance.nudRange.Value = .Range
            frmEditor_Projectile.Instance.nudSpeed.Value = .Speed
            frmEditor_Projectile.Instance.nudDamage.Value = .Damage
        End With

        GameState.ProjectileChanged(GameState.EditorIndex) = True

    End Sub

    Friend Sub ProjectileEditorOk()
        Dim i As Integer

        For i = 1 To MAX_PROJECTILES
            If GameState.ProjectileChanged(i) Then
                Call SendSaveProjectile(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChanged_Projectile()
        SendCloseEditor()
    End Sub

    Friend Sub ProjectileEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Projectile()
        ClearProjectile()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Projectile()
        Dim i As Integer

        For i = 1 To MAX_PROJECTILES
            GameState.ProjectileChanged(i) = False
        Next

    End Sub

#End Region


#Region "Pet Editor"
    Friend Sub PetEditorInit()
        Dim i As Integer

        GameState.EditorIndex = frmEditor_Pet.Instance.lstIndex.SelectedIndex + 1

        With frmEditor_Pet.Instance
            'populate skill combo's
            .cmbSkill1.Items.Clear()
            .cmbSkill2.Items.Clear()
            .cmbSkill3.Items.Clear()
            .cmbSkill4.Items.Clear()

            For i = 1 To MAX_SKILLS
                .cmbSkill1.Items.Add(i & ": " & Skill(i).Name)
                .cmbSkill2.Items.Add(i & ": " & Skill(i).Name)
                .cmbSkill3.Items.Add(i & ": " & Skill(i).Name)
                .cmbSkill4.Items.Add(i & ": " & Skill(i).Name)
            Next
            .txtName.Text = Type.Pet(GameState.EditorIndex).Name
            If Type.Pet(GameState.EditorIndex).Sprite < 0 Or Type.Pet(GameState.EditorIndex).Sprite > .nudSprite.Maximum Then Type.Pet(GameState.EditorIndex).Sprite = 0

            .nudSprite.Value = Type.Pet(GameState.EditorIndex).Sprite
            .EditorPet_DrawPet()

            .nudRange.Value = Type.Pet(GameState.EditorIndex).Range

            .nudStrength.Value = Type.Pet(GameState.EditorIndex).Stat(StatType.Strength)
            .nudVitality.Value = Type.Pet(GameState.EditorIndex).Stat(StatType.Vitality)
            .nudLuck.Value = Type.Pet(GameState.EditorIndex).Stat(StatType.Luck)
            .nudIntelligence.Value = Type.Pet(GameState.EditorIndex).Stat(StatType.Intelligence)
            .nudSpirit.Value = Type.Pet(GameState.EditorIndex).Stat(StatType.Spirit)
            .nudLevel.Value = Type.Pet(GameState.EditorIndex).Level

            If Type.Pet(GameState.EditorIndex).StatType = 1 Then
                .optCustomStats.Checked = True
                .pnlCustomStats.Visible = True
            Else
                .optAdoptStats.Checked = True
                .pnlCustomStats.Visible = False
            End If

            .nudPetExp.Value = Type.Pet(GameState.EditorIndex).ExpGain

            .nudPetPnts.Value = Type.Pet(GameState.EditorIndex).LevelPnts

            .nudMaxLevel.Value = Type.Pet(GameState.EditorIndex).MaxLevel

            'Set skills
            .cmbSkill1.SelectedIndex = Type.Pet(GameState.EditorIndex).Skill(1)

            .cmbSkill2.SelectedIndex = Type.Pet(GameState.EditorIndex).Skill(2)

            .cmbSkill3.SelectedIndex = Type.Pet(GameState.EditorIndex).Skill(3)

            .cmbSkill4.SelectedIndex = Type.Pet(GameState.EditorIndex).Skill(4)

            If Type.Pet(GameState.EditorIndex).LevelingType = 1 Then
                .optLevel.Checked = True

                .pnlPetlevel.Visible = True
                .pnlPetlevel.BringToFront()
                .nudPetExp.Value = Type.Pet(GameState.EditorIndex).ExpGain
                If Type.Pet(GameState.EditorIndex).MaxLevel > 0 Then .nudMaxLevel.Value = Type.Pet(GameState.EditorIndex).MaxLevel
                .nudPetPnts.Value = Type.Pet(GameState.EditorIndex).LevelPnts
            Else
                .optDoNotLevel.Checked = True

                .pnlPetlevel.Visible = False
                .nudPetExp.Value = Type.Pet(GameState.EditorIndex).ExpGain
                .nudMaxLevel.Value = Type.Pet(GameState.EditorIndex).MaxLevel
                .nudPetPnts.Value = Type.Pet(GameState.EditorIndex).LevelPnts
            End If

            If Type.Pet(GameState.EditorIndex).Evolvable = 1 Then
                .chkEvolve.Checked = True
            Else
                .chkEvolve.Checked = False
            End If

            .nudEvolveLvl.Value = Type.Pet(GameState.EditorIndex).EvolveLevel
            .cmbEvolve.SelectedIndex = Type.Pet(GameState.EditorIndex).EvolveNum

            .EditorPet_DrawPet()
        End With

        ClearChanged_Pet()
        GameState.Pet_Changed(GameState.EditorIndex) = True
    End Sub

    Friend Sub PetEditorOk()
        Dim i As Integer

        For i = 1 To MAX_PETS
            If GameState.Pet_Changed(i) Then
                SendSavePet(i)
            End If
        Next

        GameState.MyEditorType = -1
        ClearChanged_Pet()
        SendCloseEditor()
    End Sub

    Friend Sub PetEditorCancel()
        GameState.MyEditorType = -1
        ClearChanged_Pet()
        ClearPets()
        SendCloseEditor()
    End Sub

    Friend Sub ClearChanged_Pet()
        ReDim GameState.Pet_Changed(MAX_PETS)
    End Sub

#End Region

End Module