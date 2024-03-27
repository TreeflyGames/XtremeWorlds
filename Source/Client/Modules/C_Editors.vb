Imports System.IO
Imports Core

Module C_Editors

#Region "Animation Editor"

    Friend Sub AnimationEditorInit()
        EditorIndex = FrmEditor_Animation.lstIndex.SelectedIndex + 1

        With Animation(EditorIndex)
            If Trim$(Animation(EditorIndex).Sound) = "" Then
                FrmEditor_Animation.cmbSound.SelectedIndex = 0
            Else
                For i = 0 To FrmEditor_Animation.cmbSound.Items.Count
                    If FrmEditor_Animation.cmbSound.GetItemText(i) = FrmEditor_Animation.cmbSound.SelectedIndex Then
                        FrmEditor_Animation.cmbSound.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
            FrmEditor_Animation.txtName.Text = Trim$(.Name)

            FrmEditor_Animation.nudSprite0.Value = .Sprite(0)
            FrmEditor_Animation.nudFrameCount0.Value = .Frames(0)
            If Animation(EditorIndex).LoopCount(0) = 0 Then Animation(EditorIndex).LoopCount(0) = 1
            FrmEditor_Animation.nudLoopCount0.Value = .LoopCount(0)
            If Animation(EditorIndex).LoopTime(0) = 0 Then Animation(EditorIndex).LoopTime(0) = 1
            FrmEditor_Animation.nudLoopTime0.Value = .LoopTime(0)

            FrmEditor_Animation.nudSprite1.Value = .Sprite(1)
            FrmEditor_Animation.nudFrameCount1.Value = .Frames(1)
            If Animation(EditorIndex).LoopCount(1) = 0 Then Animation(EditorIndex).LoopCount(1) = 1
            FrmEditor_Animation.nudLoopCount1.Value = .LoopCount(1)
            If Animation(EditorIndex).LoopTime(1) = 0 Then Animation(EditorIndex).LoopTime(1) = 1
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
        With FrmEditor_NPC
            EditorIndex = .lstIndex.SelectedIndex + 1

            .cmbDropSlot.SelectedIndex = 0

            .txtName.Text = NPC(EditorIndex).Name
            .txtAttackSay.Text = NPC(EditorIndex).AttackSay
            .nudSprite.Value = NPC(EditorIndex).Sprite
            .nudSpawnSecs.Value = NPC(EditorIndex).SpawnSecs
            .cmbBehaviour.SelectedIndex = NPC(EditorIndex).Behaviour
            .cmbFaction.SelectedIndex = NPC(EditorIndex).Faction
            .nudRange.Value = NPC(EditorIndex).Range
            .nudChance.Value = NPC(EditorIndex).DropChance(FrmEditor_NPC.cmbDropSlot.SelectedIndex)
            .cmbItem.SelectedIndex = NPC(EditorIndex).DropItem(FrmEditor_NPC.cmbDropSlot.SelectedIndex)

            .nudAmount.Value = NPC(EditorIndex).DropItemValue(FrmEditor_NPC.cmbDropSlot.SelectedIndex)

            .nudHp.Value = NPC(EditorIndex).HP
            .nudExp.Value = NPC(EditorIndex).Exp
            .nudLevel.Value = NPC(EditorIndex).Level
            .nudDamage.Value = NPC(EditorIndex).Damage

            .cmbSpawnPeriod.SelectedIndex = NPC(EditorIndex).SpawnTime

            .cmbAnimation.SelectedIndex = NPC(EditorIndex).Animation

            .nudStrength.Value = NPC(EditorIndex).Stat(StatType.Strength)
            .nudIntelligence.Value = NPC(EditorIndex).Stat(StatType.Intelligence)
            .nudSpirit.Value = NPC(EditorIndex).Stat(StatType.Spirit)
            .nudLuck.Value = NPC(EditorIndex).Stat(StatType.Luck)
            .nudVitality.Value = NPC(EditorIndex).Stat(StatType.Vitality)

            .cmbSkill1.SelectedIndex = NPC(EditorIndex).Skill(1)
            .cmbSkill2.SelectedIndex = NPC(EditorIndex).Skill(2)
            .cmbSkill3.SelectedIndex = NPC(EditorIndex).Skill(3)
            .cmbSkill4.SelectedIndex = NPC(EditorIndex).Skill(4)
            .cmbSkill5.SelectedIndex = NPC(EditorIndex).Skill(5)
            .cmbSkill6.SelectedIndex = NPC(EditorIndex).Skill(6)
        End With

        EditorNpc_DrawSprite()
        NPC_Changed(EditorIndex) = True
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

        EditorIndex = FrmEditor_Resource.lstIndex.SelectedIndex + 1

        With FrmEditor_Resource
            .txtName.Text = Trim$(Resource(EditorIndex).Name)
            .txtMessage.Text = Trim$(Resource(EditorIndex).SuccessMessage)
            .txtMessage2.Text = Trim$(Resource(EditorIndex).EmptyMessage)
            .cmbType.SelectedIndex = Resource(EditorIndex).ResourceType
            .nudNormalPic.Value = Resource(EditorIndex).ResourceImage
            .nudExhaustedPic.Value = Resource(EditorIndex).ExhaustedImage
            .cmbRewardItem.SelectedIndex = Resource(EditorIndex).ItemReward
            .nudRewardExp.Value = Resource(EditorIndex).ExpReward
            .cmbTool.SelectedIndex = Resource(EditorIndex).ToolRequired
            .nudHealth.Value = Resource(EditorIndex).Health
            .nudRespawn.Value = Resource(EditorIndex).RespawnTime
            .cmbAnimation.SelectedIndex = Resource(EditorIndex).Animation
            .nudLvlReq.Value = Resource(EditorIndex).LvlRequired
        End With

        FrmEditor_Resource.Visible = True

        EditorResource_DrawSprite()

        Resource_Changed(EditorIndex) = True
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
        With FrmEditor_Skill
            EditorIndex = .lstIndex.SelectedIndex + 1

            .cmbAnimCast.SelectedIndex = 0
            .cmbAnim.SelectedIndex = 0

            ' set values
            .txtName.Text = Trim$(Skill(EditorIndex).Name)
            .cmbType.SelectedIndex = Skill(EditorIndex).Type
            .nudMp.Value = Skill(EditorIndex).MpCost
            .nudLevel.Value = Skill(EditorIndex).LevelReq
            .cmbAccessReq.SelectedIndex = Skill(EditorIndex).AccessReq
            .cmbJob.SelectedIndex = Skill(EditorIndex).JobReq
            .nudCast.Value = Skill(EditorIndex).CastTime
            .nudCool.Value = Skill(EditorIndex).CdTime
            .nudIcon.Value = Skill(EditorIndex).Icon
            .nudMap.Value = Skill(EditorIndex).Map
            .nudX.Value = Skill(EditorIndex).X
            .nudY.Value = Skill(EditorIndex).Y
            .cmbDir.SelectedIndex = Skill(EditorIndex).Dir
            .nudVital.Value = Skill(EditorIndex).Vital
            .nudDuration.Value = Skill(EditorIndex).Duration
            .nudInterval.Value = Skill(EditorIndex).Interval
            .nudRange.Value = Skill(EditorIndex).Range

            .chkAoE.Checked = Skill(EditorIndex).IsAoE

            .nudAoE.Value = Skill(EditorIndex).AoE
            .cmbAnimCast.SelectedIndex = Skill(EditorIndex).CastAnim
            .cmbAnim.SelectedIndex = Skill(EditorIndex).SkillAnim
            .nudStun.Value = Skill(EditorIndex).StunDuration

            If Skill(EditorIndex).IsProjectile = 1 Then
                .chkProjectile.Checked = True
            Else
                .chkProjectile.Checked = False
            End If
            .cmbProjectile.SelectedIndex = Skill(EditorIndex).Projectile

            If Skill(EditorIndex).KnockBack = 1 Then
                .chkKnockBack.Checked = True
            Else
                .chkKnockBack.Checked = False
            End If
            .cmbKnockBackTiles.SelectedIndex = Skill(EditorIndex).KnockBackTiles
        End With

        Skill_Changed(EditorIndex) = True
        EditorSkill_DrawIcon()
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
        EditorIndex = FrmEditor_Shop.lstIndex.SelectedIndex + 1

        With FrmEditor_Shop
            .txtName.Text = Trim$(Shop(EditorIndex).Name)

            If Shop(EditorIndex).BuyRate > 0 Then
                .nudBuy.Value = Shop(EditorIndex).BuyRate
            Else
                .nudBuy.Value = 100
            End If

            .nudFace.Value = Shop(EditorIndex).Face
            If File.Exists(Paths.Graphics & "Faces\" & Shop(EditorIndex).Face & GfxExt) Then
                .picFace.BackgroundImage = Drawing.Image.FromFile(Paths.Graphics & "Faces\" & Shop(EditorIndex).Face & GfxExt)
            Else
                .picFace.BackgroundImage = Nothing
            End If

            .cmbItem.SelectedIndex = 0
            .cmbCostItem.SelectedIndex = 0
        End With

        UpdateShopTrade()
        Shop_Changed(EditorIndex) = True
    End Sub

    Friend Sub UpdateShopTrade()
        Dim i As Integer

        FrmEditor_Shop.lstTradeItem.Items.Clear()

        For i = 1 To MAX_TRADES
            With Shop(EditorIndex).TradeItem(i)
                ' if none, show as none
                If .Item = 0 AndAlso .CostItem = 0 Then
                    FrmEditor_Shop.lstTradeItem.Items.Add("Empty Trade Slot")
                Else
                    FrmEditor_Shop.lstTradeItem.Items.Add(i & ": " & .ItemValue & "x " & Trim$(Item(.Item).Name) & " for " & .CostValue & "x " & Trim$(Item(.CostItem).Name))
                End If
            End With
        Next

        FrmEditor_Shop.lstTradeItem.SelectedIndex = 0
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

        With FrmEditor_Job
            EditorIndex = .lstIndex.SelectedIndex + 1

            .txtName.Text = Job(EditorIndex).Name
            .txtDescription.Text = Job(EditorIndex).Desc

            If Job(EditorIndex).MaleSprite = 0 Then Job(EditorIndex).MaleSprite = 1
            .nudMaleSprite.Value = Job(EditorIndex).MaleSprite
            If Job(EditorIndex).FemaleSprite = 0 Then Job(EditorIndex).FemaleSprite = 1
            .nudFemaleSprite.Value = Job(EditorIndex).FemaleSprite

            .cmbItems.SelectedIndex = 0

            For i = 1 To StatType.Count - 1
                If Job(EditorIndex).Stat(i) = 0 Then Job(EditorIndex).Stat(i) = 1
            Next

            .nudStrength.Value = Job(EditorIndex).Stat(StatType.Strength)
            .nudLuck.Value = Job(EditorIndex).Stat(StatType.Luck)
            .nudIntelligence.Value = Job(EditorIndex).Stat(StatType.Intelligence)
            .nudVitality.Value = Job(EditorIndex).Stat(StatType.Vitality)
            .nudSpirit.Value = Job(EditorIndex).Stat(StatType.Spirit)
            .nudBaseExp.Value = Job(EditorIndex).BaseExp

            If Job(EditorIndex).StartMap = 0 Then Job(EditorIndex).StartMap = 1
            .nudStartMap.Value = Job(EditorIndex).StartMap
            .nudStartX.Value = Job(EditorIndex).StartX
            .nudStartY.Value = Job(EditorIndex).StartY

            Job_Changed(EditorIndex) = True
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

        EditorIndex = FrmEditor_Item.lstIndex.SelectedIndex + 1

        With Item(EditorIndex)
            FrmEditor_Item.txtName.Text = Trim$(.Name)
            FrmEditor_Item.txtDescription.Text = Trim$(.Description)

            If .Icon > FrmEditor_Item.nudPic.Maximum Then .Icon = 0
            FrmEditor_Item.nudPic.Value = .Icon
            If .Type > ItemType.Count - 1 Then .Type = 0
            FrmEditor_Item.cmbType.SelectedIndex = .Type
            FrmEditor_Item.cmbAnimation.SelectedIndex = .Animation

            If .ItemLevel = 0 Then .ItemLevel = 1
            FrmEditor_Item.nudItemLvl.Value = .ItemLevel

            ' Type specific settings
            If (FrmEditor_Item.cmbType.SelectedIndex = ItemType.Equipment) Then
                FrmEditor_Item.fraEquipment.Visible = True
                FrmEditor_Item.nudDamage.Value = .Data2
                FrmEditor_Item.cmbTool.SelectedIndex = .Data3

                FrmEditor_Item.cmbSubType.SelectedIndex = .SubType

                If .Speed < 1000 Then .Speed = 100
                If .Speed > FrmEditor_Item.nudSpeed.Maximum Then .Speed = FrmEditor_Item.nudSpeed.Maximum
                FrmEditor_Item.nudSpeed.Value = .Speed

                FrmEditor_Item.nudStrength.Value = .Add_Stat(StatType.Strength)
                FrmEditor_Item.nudIntelligence.Value = .Add_Stat(StatType.Intelligence)
                FrmEditor_Item.nudVitality.Value = .Add_Stat(StatType.Vitality)
                FrmEditor_Item.nudLuck.Value = .Add_Stat(StatType.Luck)
                FrmEditor_Item.nudSpirit.Value = .Add_Stat(StatType.Spirit)

                If .KnockBack = 1 Then
                    FrmEditor_Item.chkKnockBack.Checked = True
                Else
                    FrmEditor_Item.chkKnockBack.Checked = False
                End If
                FrmEditor_Item.cmbKnockBackTiles.SelectedIndex = .KnockBackTiles

                If .Randomize = 1 Then
                    FrmEditor_Item.chkRandomize.Checked = True
                Else
                    FrmEditor_Item.chkRandomize.Checked = False
                End If

                FrmEditor_Item.nudPaperdoll.Value = .Paperdoll

                If .SubType = EquipmentType.Weapon Then
                    FrmEditor_Item.fraProjectile.Visible = True
                Else
                     FrmEditor_Item.fraProjectile.Visible = False
                End If
            Else
                FrmEditor_Item.fraEquipment.Visible = False
            End If

            If (FrmEditor_Item.cmbType.SelectedIndex = ItemType.Consumable) Then
                FrmEditor_Item.fraVitals.Visible = True
                FrmEditor_Item.nudVitalMod.Value = .Data1
            Else
                FrmEditor_Item.fraVitals.Visible = False
            End If

            If (FrmEditor_Item.cmbType.SelectedIndex = ItemType.Skill) Then
                FrmEditor_Item.fraSkill.Visible = True
                FrmEditor_Item.cmbSkills.SelectedIndex = .Data1
            Else
                FrmEditor_Item.fraSkill.Visible = False
            End If

            if (FrmEditor_Item.cmbType.SelectedIndex = ItemType.Projectile) Then
                FrmEditor_Item.fraProjectile.Visible = True
                FrmEditor_Item.fraEquipment.Visible = True
            Elseif .Type <> ItemType.Equipment Then
                FrmEditor_Item.fraProjectile.Visible = False
            End If

            If FrmEditor_Item.cmbType.SelectedIndex = ItemType.CommonEvent Then
                FrmEditor_Item.fraEvents.Visible = True
                FrmEditor_Item.nudEvent.Value = .Data1
                FrmEditor_Item.nudEventValue.Value = .Data2
            Else
                FrmEditor_Item.fraEvents.Visible = False
            End If

            If (FrmEditor_Item.cmbType.SelectedIndex = ItemType.Pet) Then
                FrmEditor_Item.fraPet.Visible = True
                FrmEditor_Item.cmbPet.SelectedIndex = .Data1
            Else
                FrmEditor_Item.fraPet.Visible = False
            End If

            ' Projectile
            FrmEditor_Item.cmbProjectile.SelectedIndex = .Projectile
            FrmEditor_Item.cmbAmmo.SelectedIndex = .Ammo

            ' Basic requirements
            FrmEditor_Item.cmbAccessReq.SelectedIndex = .AccessReq
            FrmEditor_Item.nudLevelReq.Value = .LevelReq

            FrmEditor_Item.nudStrReq.Value = .Stat_Req(StatType.Strength)
            FrmEditor_Item.nudVitReq.Value = .Stat_Req(StatType.Vitality)
            FrmEditor_Item.nudLuckReq.Value = .Stat_Req(StatType.Luck)
            FrmEditor_Item.nudIntReq.Value = .Stat_Req(StatType.Intelligence)
            FrmEditor_Item.nudSprReq.Value = .Stat_Req(StatType.Spirit)

            ' Build cmbJobReq
            FrmEditor_Item.cmbJobReq.Items.Clear()
           For i = 1 To MAX_JOBS
                FrmEditor_Item.cmbJobReq.Items.Add(Job(i).Name)
            Next

            FrmEditor_Item.cmbJobReq.SelectedIndex = .JobReq
            ' Info
            FrmEditor_Item.nudPrice.Value = .Price
            FrmEditor_Item.cmbBind.SelectedIndex = .BindType
            FrmEditor_Item.nudRarity.Value = .Rarity

            If .Stackable = 1 Then
                FrmEditor_Item.chkStackable.Checked = True
            Else
                FrmEditor_Item.chkStackable.Checked = False
            End If
        End With

        Item_Changed(EditorIndex) = True
        EditorItem_DrawIcon()
        EditorItem_DrawPaperdoll()
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