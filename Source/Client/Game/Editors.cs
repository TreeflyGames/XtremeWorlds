using System;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Client
{

    static class Editors
    {

        #region Animation Editor

        internal static void AnimationEditorInit()
        {
            GameState.EditorIndex = frmEditor_Animation.Instance.lstIndex.SelectedIndex + 1;

            {
                ref var withBlock = ref Core.Type.Animation[GameState.EditorIndex];
                if (string.IsNullOrEmpty(Core.Type.Animation[GameState.EditorIndex].Sound))
                {
                    frmEditor_Animation.Instance.cmbSound.SelectedIndex = 0;
                }
                else
                {
                    for (int i = 0, loopTo = frmEditor_Animation.Instance.cmbSound.Items.Count; i <= loopTo; i++)
                    {
                        if (Conversions.ToDouble(frmEditor_Animation.Instance.cmbSound.GetItemText(i)) == frmEditor_Animation.Instance.cmbSound.SelectedIndex)
                        {
                            frmEditor_Animation.Instance.cmbSound.SelectedIndex = i;
                            break;
                        }
                    }
                }
                frmEditor_Animation.Instance.txtName.Text = Strings.Trim(withBlock.Name);

                frmEditor_Animation.Instance.nudSprite0.Value = withBlock.Sprite[0];
                frmEditor_Animation.Instance.nudFrameCount0.Value = withBlock.Frames[0];
                if (Core.Type.Animation[GameState.EditorIndex].LoopCount[0] == 0)
                    Core.Type.Animation[GameState.EditorIndex].LoopCount[0] = 1;
                frmEditor_Animation.Instance.nudLoopCount0.Value = withBlock.LoopCount[0];
                if (Core.Type.Animation[GameState.EditorIndex].LoopTime[0] == 0)
                    Core.Type.Animation[GameState.EditorIndex].LoopTime[0] = 1;
                frmEditor_Animation.Instance.nudLoopTime0.Value = withBlock.LoopTime[0];

                frmEditor_Animation.Instance.nudSprite1.Value = withBlock.Sprite[1];
                frmEditor_Animation.Instance.nudFrameCount1.Value = withBlock.Frames[1];
                if (Core.Type.Animation[GameState.EditorIndex].LoopCount[1] == 0)
                    Core.Type.Animation[GameState.EditorIndex].LoopCount[1] = 1;
                frmEditor_Animation.Instance.nudLoopCount1.Value = withBlock.LoopCount[1];
                if (Core.Type.Animation[GameState.EditorIndex].LoopTime[1] == 0)
                    Core.Type.Animation[GameState.EditorIndex].LoopTime[1] = 1;
                frmEditor_Animation.Instance.nudLoopTime1.Value = withBlock.LoopTime[1];
            }

            GameState.Animation_Changed[GameState.EditorIndex] = true;
        }

        internal static void AnimationEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_ANIMATIONS - 1; i++)
            {
                if (GameState.Animation_Changed[i])
                {
                    NetworkSend.SendSaveAnimation(i);
                }
            }

            GameState.MyEditorType = -1;
            ClearChanged_Animation();
            NetworkSend.SendCloseEditor();
        }

        internal static void AnimationEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Animation();
            Animation.ClearAnimations();
            NetworkSend.SendCloseEditor();
        }

        internal static void ClearChanged_Animation()
        {
            for (int i = 0; i <= Constant.MAX_ANIMATIONS - 1; i++)
                GameState.Animation_Changed[i] = false;
        }

        #endregion

        #region NPC Editor

        internal static void NPCEditorInit()
        {
            {
                var withBlock = frmEditor_NPC.Instance;
                GameState.EditorIndex = withBlock.lstIndex.SelectedIndex + 1;

                withBlock.cmbDropSlot.SelectedIndex = 0;

                withBlock.txtName.Text = Core.Type.NPC[GameState.EditorIndex].Name;
                withBlock.txtAttackSay.Text = Core.Type.NPC[GameState.EditorIndex].AttackSay;
                withBlock.nudSprite.Value = Core.Type.NPC[GameState.EditorIndex].Sprite;
                withBlock.nudSpawnSecs.Value = Core.Type.NPC[GameState.EditorIndex].SpawnSecs;
                withBlock.cmbBehaviour.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Behaviour;
                withBlock.cmbFaction.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Faction;
                withBlock.nudRange.Value = Core.Type.NPC[GameState.EditorIndex].Range;
                withBlock.nudChance.Value = Core.Type.NPC[GameState.EditorIndex].DropChance[frmEditor_NPC.Instance.cmbDropSlot.SelectedIndex];
                withBlock.cmbItem.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].DropItem[frmEditor_NPC.Instance.cmbDropSlot.SelectedIndex];

                withBlock.nudAmount.Value = Core.Type.NPC[GameState.EditorIndex].DropItemValue[frmEditor_NPC.Instance.cmbDropSlot.SelectedIndex];

                withBlock.nudHp.Value = Core.Type.NPC[GameState.EditorIndex].HP;
                withBlock.nudExp.Value = Core.Type.NPC[GameState.EditorIndex].Exp;
                withBlock.nudLevel.Value = Core.Type.NPC[GameState.EditorIndex].Level;
                withBlock.nudDamage.Value = Core.Type.NPC[GameState.EditorIndex].Damage;

                withBlock.cmbSpawnPeriod.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].SpawnTime;

                withBlock.cmbAnimation.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Animation;

                withBlock.nudStrength.Value = Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Strength];
                withBlock.nudIntelligence.Value = Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Intelligence];
                withBlock.nudSpirit.Value = Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Spirit];
                withBlock.nudLuck.Value = Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Luck];
                withBlock.nudVitality.Value = Core.Type.NPC[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Vitality];

                withBlock.cmbSkill1.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Skill[1];
                withBlock.cmbSkill2.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Skill[2];
                withBlock.cmbSkill3.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Skill[3];
                withBlock.cmbSkill4.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Skill[4];
                withBlock.cmbSkill5.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Skill[5];
                withBlock.cmbSkill6.SelectedIndex = Core.Type.NPC[GameState.EditorIndex].Skill[6];
            }

            GameState.NPC_Changed[GameState.EditorIndex] = true;
        }

        internal static void NPCEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_NPCS - 1; i++)
            {
                if (GameState.NPC_Changed[i])
                {
                    NetworkSend.SendSaveNPC(i);
                }
            }

            GameState.MyEditorType = -1;
            ClearChanged_NPC();
            NetworkSend.SendCloseEditor();
        }

        internal static void NPCEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_NPC();
            Database.ClearNPCs();
            NetworkSend.SendCloseEditor();
        }

        internal static void ClearChanged_NPC()
        {
            for (int i = 0; i <= Constant.MAX_NPCS - 1; i++)
                GameState.NPC_Changed[i] = false;
        }

        #endregion

        #region Resource Editor
        internal static void ClearChanged_Resource()
        {
            GameState.Resource_Changed = new bool[101];
        }

        internal static void ResourceEditorInit()
        {
            int i;

            GameState.EditorIndex = frmEditor_Resource.Instance.lstIndex.SelectedIndex + 1;

            {
                var withBlock = frmEditor_Resource.Instance;
                withBlock.txtName.Text = Core.Type.Resource[GameState.EditorIndex].Name;
                withBlock.txtMessage.Text = Core.Type.Resource[GameState.EditorIndex].SuccessMessage;
                withBlock.txtMessage2.Text = Core.Type.Resource[GameState.EditorIndex].EmptyMessage;
                withBlock.cmbType.SelectedIndex = Core.Type.Resource[GameState.EditorIndex].ResourceType;
                withBlock.nudNormalPic.Value = Core.Type.Resource[GameState.EditorIndex].ResourceImage;
                withBlock.nudExhaustedPic.Value = Core.Type.Resource[GameState.EditorIndex].ExhaustedImage;
                withBlock.cmbRewardItem.SelectedIndex = Core.Type.Resource[GameState.EditorIndex].ItemReward;
                withBlock.nudRewardExp.Value = Core.Type.Resource[GameState.EditorIndex].ExpReward;
                withBlock.cmbTool.SelectedIndex = Core.Type.Resource[GameState.EditorIndex].ToolRequired;
                withBlock.nudHealth.Value = Core.Type.Resource[GameState.EditorIndex].Health;
                withBlock.nudRespawn.Value = Core.Type.Resource[GameState.EditorIndex].RespawnTime;
                withBlock.cmbAnimation.SelectedIndex = Core.Type.Resource[GameState.EditorIndex].Animation;
                withBlock.nudLvlReq.Value = Core.Type.Resource[GameState.EditorIndex].LvlRequired;
            }

            frmEditor_Resource.Instance.Visible = true;

            GameState.Resource_Changed[GameState.EditorIndex] = true;
        }

        internal static void ResourceEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_RESOURCES - 1; i++)
            {
                if (GameState.Resource_Changed[i])
                {
                    NetworkSend.SendSaveResource(i);
                }
            }

            GameState.MyEditorType = -1;
            ClearChanged_Resource();
            NetworkSend.SendCloseEditor();
        }

        internal static void ResourceEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Resource();
            Resource.ClearResources();
            NetworkSend.SendCloseEditor();
        }

        #endregion

        #region Skill Editor

        internal static void SkillEditorInit()
        {
            {
                var withBlock = frmEditor_Skill.Instance;
                GameState.EditorIndex = withBlock.lstIndex.SelectedIndex + 1;

                withBlock.cmbAnimCast.SelectedIndex = 0;
                withBlock.cmbAnim.SelectedIndex = 0;

                // set values
                withBlock.txtName.Text = Strings.Trim(Core.Type.Skill[GameState.EditorIndex].Name);
                withBlock.cmbType.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].Type;
                withBlock.nudMp.Value = Core.Type.Skill[GameState.EditorIndex].MpCost;
                withBlock.nudLevel.Value = Core.Type.Skill[GameState.EditorIndex].LevelReq;
                withBlock.cmbAccessReq.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].AccessReq;
                withBlock.cmbJob.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].JobReq;
                withBlock.nudCast.Value = Core.Type.Skill[GameState.EditorIndex].CastTime;
                withBlock.nudCool.Value = Core.Type.Skill[GameState.EditorIndex].CdTime;
                withBlock.nudIcon.Value = Core.Type.Skill[GameState.EditorIndex].Icon;
                withBlock.nudMap.Value = Core.Type.Skill[GameState.EditorIndex].Map;
                withBlock.nudX.Value = Core.Type.Skill[GameState.EditorIndex].X;
                withBlock.nudY.Value = Core.Type.Skill[GameState.EditorIndex].Y;
                withBlock.cmbDir.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].Dir;
                withBlock.nudVital.Value = Core.Type.Skill[GameState.EditorIndex].Vital;
                withBlock.nudDuration.Value = Core.Type.Skill[GameState.EditorIndex].Duration;
                withBlock.nudInterval.Value = Core.Type.Skill[GameState.EditorIndex].Interval;
                withBlock.nudRange.Value = Core.Type.Skill[GameState.EditorIndex].Range;

                withBlock.chkAoE.Checked = Core.Type.Skill[GameState.EditorIndex].IsAoE;

                withBlock.nudAoE.Value = Core.Type.Skill[GameState.EditorIndex].AoE;
                withBlock.cmbAnimCast.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].CastAnim;
                withBlock.cmbAnim.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].SkillAnim;
                withBlock.nudStun.Value = Core.Type.Skill[GameState.EditorIndex].StunDuration;

                if (Core.Type.Skill[GameState.EditorIndex].IsProjectile == 1)
                {
                    withBlock.chkProjectile.Checked = true;
                }
                else
                {
                    withBlock.chkProjectile.Checked = false;
                }
                withBlock.cmbProjectile.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].Projectile;

                if (Core.Type.Skill[GameState.EditorIndex].KnockBack == 1)
                {
                    withBlock.chkKnockBack.Checked = true;
                }
                else
                {
                    withBlock.chkKnockBack.Checked = false;
                }
                withBlock.cmbKnockBackTiles.SelectedIndex = Core.Type.Skill[GameState.EditorIndex].KnockBackTiles;
            }

            GameState.Skill_Changed[GameState.EditorIndex] = true;
        }

        internal static void SkillEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_SKILLS - 1; i++)
            {
                if (GameState.Skill_Changed[i])
                {
                    NetworkSend.SendSaveSkill(i);
                }
            }

            GameState.MyEditorType = -1;
            ClearChanged_Skill();
            NetworkSend.SendCloseEditor();
        }

        internal static void SkillEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Skill();
            Database.ClearSkills();
            NetworkSend.SendCloseEditor();
        }

        internal static void ClearChanged_Skill()
        {
            for (int i = 0; i <= Constant.MAX_SKILLS - 1; i++)
                GameState.Skill_Changed[i] = false;
        }

        #endregion

        #region Shop editor
        internal static void ShopEditorInit()
        {
            GameState.EditorIndex = frmEditor_Shop.Instance.lstIndex.SelectedIndex + 1;

            {
                var withBlock = frmEditor_Shop.Instance;
                withBlock.txtName.Text = Core.Type.Shop[GameState.EditorIndex].Name;

                if (Core.Type.Shop[GameState.EditorIndex].BuyRate > 0)
                {
                    withBlock.nudBuy.Value = Core.Type.Shop[GameState.EditorIndex].BuyRate;
                }
                else
                {
                    withBlock.nudBuy.Value = 100m;
                }

                withBlock.cmbItem.SelectedIndex = 0;
                withBlock.cmbCostItem.SelectedIndex = 0;
            }

            UpdateShopTrade();
            GameState.Shop_Changed[GameState.EditorIndex] = true;
        }

        internal static void UpdateShopTrade()
        {
            int i;

            frmEditor_Shop.Instance.lstTradeItem.Items.Clear();

            for (i = 0; i <= Constant.MAX_TRADES; i++)
            {
                {
                    ref var withBlock = ref Core.Type.Shop[GameState.EditorIndex].TradeItem[i];
                    // if none, show as none
                    if (withBlock.Item == 0 & withBlock.CostItem == 0)
                    {
                        frmEditor_Shop.Instance.lstTradeItem.Items.Add("Empty Trade Slot");
                    }
                    else
                    {
                        frmEditor_Shop.Instance.lstTradeItem.Items.Add(i + ": " + withBlock.ItemValue + "x " + Core.Type.Item[withBlock.Item].Name + " for " + withBlock.CostValue + "x " + Core.Type.Item[withBlock.CostItem].Name);
                    }
                }
            }

            frmEditor_Shop.Instance.lstTradeItem.SelectedIndex = 0;
        }

        internal static void ShopEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_SHOPS - 1; i++)
            {
                if (GameState.Shop_Changed[i])
                {
                    NetworkSend.SendSaveShop(i);
                }
            }

            GameState.MyEditorType = -1;
            ClearChanged_Shop();
            NetworkSend.SendCloseEditor();
        }

        internal static void ShopEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Shop();
            Shop.ClearShops();
            NetworkSend.SendCloseEditor();
        }

        internal static void ClearChanged_Shop()
        {
            for (int i = 0; i <= Constant.MAX_SHOPS - 1; i++)
                GameState.Shop_Changed[i] = false;
        }

        #endregion

        #region Job Editor
        internal static void JobEditorOk()
        {
            for (int i = 0; i <= Constant.MAX_JOBS - 1; i++)
            {
                if (GameState.Job_Changed[i])
                {
                    NetworkSend.SendSaveJob(i);
                }
            }
            GameState.MyEditorType = -1;
            NetworkSend.SendCloseEditor();
        }

        internal static void JobEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Job();
            Database.ClearJobs();
            NetworkSend.SendCloseEditor();
        }

        internal static void JobEditorInit()
        {
            int i;

            {
                var withBlock = frmEditor_Job.Instance;
                GameState.EditorIndex = withBlock.lstIndex.SelectedIndex + 1;

                withBlock.txtName.Text = Core.Type.Job[GameState.EditorIndex].Name;
                withBlock.txtDescription.Text = Core.Type.Job[GameState.EditorIndex].Desc;

                if (Core.Type.Job[GameState.EditorIndex].MaleSprite == 0)
                    Core.Type.Job[GameState.EditorIndex].MaleSprite = 1;
                withBlock.nudMaleSprite.Value = Core.Type.Job[GameState.EditorIndex].MaleSprite;
                if (Core.Type.Job[GameState.EditorIndex].FemaleSprite == 0)
                    Core.Type.Job[GameState.EditorIndex].FemaleSprite = 1;
                withBlock.nudFemaleSprite.Value = Core.Type.Job[GameState.EditorIndex].FemaleSprite;

                withBlock.cmbItems.SelectedIndex = 0;

                for (i = 0; i <= (int)Core.Enum.StatType.Count - 1; i++)
                {
                    if (Core.Type.Job[GameState.EditorIndex].Stat[i] == 0)
                        Core.Type.Job[GameState.EditorIndex].Stat[i] = 1;
                }

                withBlock.nudStrength.Value = Core.Type.Job[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Strength];
                withBlock.nudLuck.Value = Core.Type.Job[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Luck];
                withBlock.nudIntelligence.Value = Core.Type.Job[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Intelligence];
                withBlock.nudVitality.Value = Core.Type.Job[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Vitality];
                withBlock.nudSpirit.Value = Core.Type.Job[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Spirit];
                withBlock.nudBaseExp.Value = Core.Type.Job[GameState.EditorIndex].BaseExp;

                if (Core.Type.Job[GameState.EditorIndex].StartMap == 0)
                    Core.Type.Job[GameState.EditorIndex].StartMap = 1;
                withBlock.nudStartMap.Value = Core.Type.Job[GameState.EditorIndex].StartMap;
                withBlock.nudStartX.Value = Core.Type.Job[GameState.EditorIndex].StartX;
                withBlock.nudStartY.Value = Core.Type.Job[GameState.EditorIndex].StartY;

                GameState.Job_Changed[GameState.EditorIndex] = true;
                withBlock.DrawPreview();
            }
        }

        internal static void ClearChanged_Job()
        {
            for (int i = 0; i <= Constant.MAX_JOBS - 1; i++)
                GameState.Job_Changed[i] = false;
        }


        #endregion

        #region Item

        internal static void ItemEditorInit()
        {
            int i;

            GameState.EditorIndex = frmEditor_Item.Instance.lstIndex.SelectedIndex + 1;

            {
                ref var withBlock = ref Core.Type.Item[GameState.EditorIndex];
                frmEditor_Item.Instance.txtName.Text = withBlock.Name;
                frmEditor_Item.Instance.txtDescription.Text = withBlock.Description;

                if (withBlock.Icon > frmEditor_Item.Instance.nudIcon.Maximum)
                    withBlock.Icon = 0;
                frmEditor_Item.Instance.nudIcon.Value = withBlock.Icon;
                if (withBlock.Type > (int)Core.Enum.ItemType.Count - 1)
                    withBlock.Type = 0;
                frmEditor_Item.Instance.cmbType.SelectedIndex = withBlock.Type;
                frmEditor_Item.Instance.cmbAnimation.SelectedIndex = withBlock.Animation;

                if (withBlock.ItemLevel == 0)
                    withBlock.ItemLevel = 0;
                frmEditor_Item.Instance.nudItemLvl.Value = withBlock.ItemLevel;

                // Type specific settings
                if (frmEditor_Item.Instance.cmbType.SelectedIndex == (int)Core.Enum.ItemType.Equipment)
                {
                    frmEditor_Item.Instance.fraEquipment.Visible = true;
                    frmEditor_Item.Instance.nudDamage.Value = withBlock.Data2;
                    frmEditor_Item.Instance.cmbTool.SelectedIndex = withBlock.Data3;

                    frmEditor_Item.Instance.cmbSubType.SelectedIndex = withBlock.SubType;

                    if (withBlock.Speed < 1000)
                        withBlock.Speed = 100;
                    if (withBlock.Speed > frmEditor_Item.Instance.nudSpeed.Maximum)
                        withBlock.Speed = (int)Math.Round(frmEditor_Item.Instance.nudSpeed.Maximum);
                    frmEditor_Item.Instance.nudSpeed.Value = withBlock.Speed;

                    frmEditor_Item.Instance.nudStrength.Value = withBlock.Add_Stat[(int)Core.Enum.StatType.Strength];
                    frmEditor_Item.Instance.nudIntelligence.Value = withBlock.Add_Stat[(int)Core.Enum.StatType.Intelligence];
                    frmEditor_Item.Instance.nudVitality.Value = withBlock.Add_Stat[(int)Core.Enum.StatType.Vitality];
                    frmEditor_Item.Instance.nudLuck.Value = withBlock.Add_Stat[(int)Core.Enum.StatType.Luck];
                    frmEditor_Item.Instance.nudSpirit.Value = withBlock.Add_Stat[(int)Core.Enum.StatType.Spirit];

                    if (withBlock.KnockBack == 1)
                    {
                        frmEditor_Item.Instance.chkKnockBack.Checked = true;
                    }
                    else
                    {
                        frmEditor_Item.Instance.chkKnockBack.Checked = false;
                    }
                    frmEditor_Item.Instance.cmbKnockBackTiles.SelectedIndex = withBlock.KnockBackTiles;
                    frmEditor_Item.Instance.nudPaperdoll.Value = withBlock.Paperdoll;

                    if (withBlock.SubType == (byte)Core.Enum.EquipmentType.Weapon)
                    {
                        frmEditor_Item.Instance.fraProjectile.Visible = true;
                    }
                    else
                    {
                        frmEditor_Item.Instance.fraProjectile.Visible = false;
                    }
                }
                else
                {
                    frmEditor_Item.Instance.fraEquipment.Visible = false;
                }

                if (frmEditor_Item.Instance.cmbType.SelectedIndex == (int)Core.Enum.ItemType.Consumable)
                {
                    frmEditor_Item.Instance.fraVitals.Visible = true;
                    frmEditor_Item.Instance.nudVitalMod.Value = withBlock.Data1;
                }
                else
                {
                    frmEditor_Item.Instance.fraVitals.Visible = false;
                }

                if (frmEditor_Item.Instance.cmbType.SelectedIndex == (int)Core.Enum.ItemType.Skill)
                {
                    frmEditor_Item.Instance.fraSkill.Visible = true;
                    frmEditor_Item.Instance.cmbSkills.SelectedIndex = withBlock.Data1;
                }
                else
                {
                    frmEditor_Item.Instance.fraSkill.Visible = false;
                }

                if (frmEditor_Item.Instance.cmbType.SelectedIndex == (int)Core.Enum.ItemType.Projectile)
                {
                    frmEditor_Item.Instance.fraProjectile.Visible = true;
                    frmEditor_Item.Instance.fraEquipment.Visible = true;
                }
                else if (withBlock.Type != (byte)Core.Enum.ItemType.Equipment)
                {
                    frmEditor_Item.Instance.fraProjectile.Visible = false;
                }

                if (frmEditor_Item.Instance.cmbType.SelectedIndex == (int)Core.Enum.ItemType.CommonEvent)
                {
                    frmEditor_Item.Instance.fraEvents.Visible = true;
                    frmEditor_Item.Instance.nudEvent.Value = withBlock.Data1;
                    frmEditor_Item.Instance.nudEventValue.Value = withBlock.Data2;
                }
                else
                {
                    frmEditor_Item.Instance.fraEvents.Visible = false;
                }

                if (frmEditor_Item.Instance.cmbType.SelectedIndex == (int)Core.Enum.ItemType.Pet)
                {
                    frmEditor_Item.Instance.fraPet.Visible = true;
                    frmEditor_Item.Instance.cmbPet.SelectedIndex = withBlock.Data1;
                }
                else
                {
                    frmEditor_Item.Instance.fraPet.Visible = false;
                }

                // Projectile
                frmEditor_Item.Instance.cmbProjectile.SelectedIndex = withBlock.Projectile;
                frmEditor_Item.Instance.cmbAmmo.SelectedIndex = withBlock.Ammo;

                // Basic requirements
                frmEditor_Item.Instance.cmbAccessReq.SelectedIndex = withBlock.AccessReq;
                frmEditor_Item.Instance.nudLevelReq.Value = withBlock.LevelReq;

                frmEditor_Item.Instance.nudStrReq.Value = withBlock.Stat_Req[(int)Core.Enum.StatType.Strength];
                frmEditor_Item.Instance.nudVitReq.Value = withBlock.Stat_Req[(int)Core.Enum.StatType.Vitality];
                frmEditor_Item.Instance.nudLuckReq.Value = withBlock.Stat_Req[(int)Core.Enum.StatType.Luck];
                frmEditor_Item.Instance.nudIntReq.Value = withBlock.Stat_Req[(int)Core.Enum.StatType.Intelligence];
                frmEditor_Item.Instance.nudSprReq.Value = withBlock.Stat_Req[(int)Core.Enum.StatType.Spirit];

                // Build cmbJobReq
                frmEditor_Item.Instance.cmbJobReq.Items.Clear();
                for (i = 0; i <= Constant.MAX_JOBS - 1; i++)
                    frmEditor_Item.Instance.cmbJobReq.Items.Add(Core.Type.Job[i].Name);

                frmEditor_Item.Instance.cmbJobReq.SelectedIndex = withBlock.JobReq;
                // Info
                frmEditor_Item.Instance.nudPrice.Value = withBlock.Price;
                frmEditor_Item.Instance.cmbBind.SelectedIndex = withBlock.BindType;
                frmEditor_Item.Instance.nudRarity.Value = withBlock.Rarity;

                if (withBlock.Stackable == 1)
                {
                    frmEditor_Item.Instance.chkStackable.Checked = true;
                }
                else
                {
                    frmEditor_Item.Instance.chkStackable.Checked = false;
                }
            }

            GameState.Item_Changed[GameState.EditorIndex] = true;
        }

        internal static void ItemEditorCancel()
        {
            GameState.MyEditorType = -1;
            Item.ClearChangedItem();
            Item.ClearItems();
            NetworkSend.SendCloseEditor();
        }

        internal static void ItemEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_ITEMS - 1; i++)
            {
                if (GameState.Item_Changed[i])
                {
                    NetworkSend.SendSaveItem(i);
                }
            }

            GameState.MyEditorType = -1;
            Item.ClearChangedItem();
            NetworkSend.SendCloseEditor();
        }

        #endregion

        #region Moral Editor
        internal static void MoralEditorOk()
        {
            for (int i = 0; i <= Constant.MAX_MORALS; i++)
            {
                if (GameState.Moral_Changed[i])
                {
                    NetworkSend.SendSaveMoral(i);
                }
            }
            GameState.MyEditorType = -1;
            NetworkSend.SendCloseEditor();
        }

        internal static void MoralEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Moral();
            Moral.ClearMorals();
            NetworkSend.SendCloseEditor();
        }

        internal static void MoralEditorInit()
        {
            int i;

            {
                var withBlock = frmEditor_Moral.Instance;
                GameState.EditorIndex = withBlock.lstIndex.SelectedIndex + 1;

                withBlock.txtName.Text = Core.Type.Moral[GameState.EditorIndex].Name;
                withBlock.cmbColor.SelectedIndex = Core.Type.Moral[GameState.EditorIndex].Color;
                withBlock.chkCanCast.Checked = Core.Type.Moral[GameState.EditorIndex].CanCast;
                withBlock.chkCanPK.Checked = Core.Type.Moral[GameState.EditorIndex].CanPK;
                withBlock.chkCanPickupItem.Checked = Core.Type.Moral[GameState.EditorIndex].CanPickupItem;
                withBlock.chkCanDropItem.Checked = Core.Type.Moral[GameState.EditorIndex].CanDropItem;
                withBlock.chkCanUseItem.Checked = Core.Type.Moral[GameState.EditorIndex].CanUseItem;
                withBlock.chkDropItems.Checked = Core.Type.Moral[GameState.EditorIndex].DropItems;
                withBlock.chkLoseExp.Checked = Core.Type.Moral[GameState.EditorIndex].LoseExp;
                withBlock.chkPlayerBlock.Checked = Core.Type.Moral[GameState.EditorIndex].PlayerBlock;
                withBlock.chkNPCBlock.Checked = Core.Type.Moral[GameState.EditorIndex].NPCBlock;

                GameState.Moral_Changed[GameState.EditorIndex] = true;
            }
        }

        internal static void ClearChanged_Moral()
        {
            for (int i = 0; i <= Constant.MAX_MORALS; i++)
                GameState.Moral_Changed[i] = false;
        }
        #endregion

        #region Projectile Editor
        internal static void ProjectileEditorInit()
        {
            GameState.EditorIndex = frmEditor_Projectile.Instance.lstIndex.SelectedIndex + 1;

            {
                ref var withBlock = ref Core.Type.Projectile[GameState.EditorIndex];
                frmEditor_Projectile.Instance.txtName.Text = Strings.Trim(withBlock.Name);
                frmEditor_Projectile.Instance.nudPic.Value = withBlock.Sprite;
                frmEditor_Projectile.Instance.nudRange.Value = withBlock.Range;
                frmEditor_Projectile.Instance.nudSpeed.Value = withBlock.Speed;
                frmEditor_Projectile.Instance.nudDamage.Value = withBlock.Damage;
            }

            GameState.ProjectileChanged[GameState.EditorIndex] = true;

        }

        internal static void ProjectileEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_PROJECTILES - 1;  i++)
            {
                if (GameState.ProjectileChanged[i])
                {
                    Projectile.SendSaveProjectile(i);
                }
            }

            GameState.MyEditorType = -1;
            ClearChanged_Projectile();
            NetworkSend.SendCloseEditor();
        }

        internal static void ProjectileEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Projectile();
            Projectile.ClearProjectile();
            NetworkSend.SendCloseEditor();
        }

        internal static void ClearChanged_Projectile()
        {
            int i;

            for (i = 0; i <= Constant.MAX_PROJECTILES - 1;  i++)
                GameState.ProjectileChanged[i] = false;

        }

        #endregion


        #region Pet Editor
        internal static void PetEditorInit()
        {
            int i;

            GameState.EditorIndex = frmEditor_Pet.Instance.lstIndex.SelectedIndex + 1;

            {
                var withBlock = frmEditor_Pet.Instance;
                // populate skill combo's
                withBlock.cmbSkill1.Items.Clear();
                withBlock.cmbSkill2.Items.Clear();
                withBlock.cmbSkill3.Items.Clear();
                withBlock.cmbSkill4.Items.Clear();

                for (i = 0; i <= Constant.MAX_SKILLS - 1; i++)
                {
                    withBlock.cmbSkill1.Items.Add(i + ": " + Core.Type.Skill[i].Name);
                    withBlock.cmbSkill2.Items.Add(i + ": " + Core.Type.Skill[i].Name);
                    withBlock.cmbSkill3.Items.Add(i + ": " + Core.Type.Skill[i].Name);
                    withBlock.cmbSkill4.Items.Add(i + ": " + Core.Type.Skill[i].Name);
                }
                withBlock.txtName.Text = Core.Type.Pet[GameState.EditorIndex].Name;
                if (Core.Type.Pet[GameState.EditorIndex].Sprite < 0 | Core.Type.Pet[GameState.EditorIndex].Sprite > withBlock.nudSprite.Maximum)
                    Core.Type.Pet[GameState.EditorIndex].Sprite = 0;

                withBlock.nudSprite.Value = Core.Type.Pet[GameState.EditorIndex].Sprite;
                withBlock.EditorPet_DrawPet();

                withBlock.nudRange.Value = Core.Type.Pet[GameState.EditorIndex].Range;

                withBlock.nudStrength.Value = Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Strength];
                withBlock.nudVitality.Value = Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Vitality];
                withBlock.nudLuck.Value = Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Luck];
                withBlock.nudIntelligence.Value = Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Intelligence];
                withBlock.nudSpirit.Value = Core.Type.Pet[GameState.EditorIndex].Stat[(int)Core.Enum.StatType.Spirit];
                withBlock.nudLevel.Value = Core.Type.Pet[GameState.EditorIndex].Level;

                if (Core.Type.Pet[GameState.EditorIndex].StatType == 1)
                {
                    withBlock.optCustomStats.Checked = true;
                    withBlock.pnlCustomStats.Visible = true;
                }
                else
                {
                    withBlock.optAdoptStats.Checked = true;
                    withBlock.pnlCustomStats.Visible = false;
                }

                withBlock.nudPetExp.Value = Core.Type.Pet[GameState.EditorIndex].ExpGain;

                withBlock.nudPetPnts.Value = Core.Type.Pet[GameState.EditorIndex].LevelPnts;

                withBlock.nudMaxLevel.Value = Core.Type.Pet[GameState.EditorIndex].MaxLevel;

                // Set skills
                withBlock.cmbSkill1.SelectedIndex = Core.Type.Pet[GameState.EditorIndex].Skill[1];

                withBlock.cmbSkill2.SelectedIndex = Core.Type.Pet[GameState.EditorIndex].Skill[2];

                withBlock.cmbSkill3.SelectedIndex = Core.Type.Pet[GameState.EditorIndex].Skill[3];

                withBlock.cmbSkill4.SelectedIndex = Core.Type.Pet[GameState.EditorIndex].Skill[4];

                if (Core.Type.Pet[GameState.EditorIndex].LevelingType == 1)
                {
                    withBlock.optLevel.Checked = true;

                    withBlock.pnlPetlevel.Visible = true;
                    withBlock.pnlPetlevel.BringToFront();
                    withBlock.nudPetExp.Value = Core.Type.Pet[GameState.EditorIndex].ExpGain;
                    if (Core.Type.Pet[GameState.EditorIndex].MaxLevel > 0)
                        withBlock.nudMaxLevel.Value = Core.Type.Pet[GameState.EditorIndex].MaxLevel;
                    withBlock.nudPetPnts.Value = Core.Type.Pet[GameState.EditorIndex].LevelPnts;
                }
                else
                {
                    withBlock.optDoNotLevel.Checked = true;

                    withBlock.pnlPetlevel.Visible = false;
                    withBlock.nudPetExp.Value = Core.Type.Pet[GameState.EditorIndex].ExpGain;
                    withBlock.nudMaxLevel.Value = Core.Type.Pet[GameState.EditorIndex].MaxLevel;
                    withBlock.nudPetPnts.Value = Core.Type.Pet[GameState.EditorIndex].LevelPnts;
                }

                if (Core.Type.Pet[GameState.EditorIndex].Evolvable == 1)
                {
                    withBlock.chkEvolve.Checked = true;
                }
                else
                {
                    withBlock.chkEvolve.Checked = false;
                }

                withBlock.nudEvolveLvl.Value = Core.Type.Pet[GameState.EditorIndex].EvolveLevel;
                withBlock.cmbEvolve.SelectedIndex = Core.Type.Pet[GameState.EditorIndex].EvolveNum;

                withBlock.EditorPet_DrawPet();
            }

            ClearChanged_Pet();
            GameState.Pet_Changed[GameState.EditorIndex] = true;
        }

        internal static void PetEditorOk()
        {
            int i;

            for (i = 0; i <= Constant.MAX_PETS; i++)
            {
                if (GameState.Pet_Changed[i])
                {
                    Pet.SendSavePet(i);
                }
            }

            GameState.MyEditorType = -1;
            ClearChanged_Pet();
            NetworkSend.SendCloseEditor();
        }

        internal static void PetEditorCancel()
        {
            GameState.MyEditorType = -1;
            ClearChanged_Pet();
            Pet.ClearPets();
            NetworkSend.SendCloseEditor();
        }

        internal static void ClearChanged_Pet()
        {
            GameState.Pet_Changed = new bool[101];
        }

        #endregion

    }
}