﻿using System.Reflection;
using Core;
using static Core.Global.Command;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

using Mirage.Sharp.Asfw;

namespace Client
{

    public class NetworkSend
    {
        public static void SendAddChar(string name, int sexNum, int jobNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAddChar);
            buffer.WriteByte(GameState.CharNum);
            buffer.WriteString(name);
            buffer.WriteInt32(sexNum);
            buffer.WriteInt32(jobNum);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendUseChar(byte slot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CUseChar);
            buffer.WriteByte(slot);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendDelChar(byte slot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CDelChar);
            buffer.WriteByte(slot);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendLogin(string name, string pass)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CLogin);
            buffer.WriteString(GameState.EKeyPair.EncryptString(name));
            buffer.WriteString(GameState.EKeyPair.EncryptString(pass));

            // Get the current executing assembly
            var @assembly = Assembly.GetExecutingAssembly();

            // Retrieve the version information
            var version = assembly.GetName().Version;
            buffer.WriteString(GameState.EKeyPair.EncryptString(version.ToString()));
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendRegister(string name, string pass)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRegister);
            buffer.WriteString(GameState.EKeyPair.EncryptString(name));
            buffer.WriteString(GameState.EKeyPair.EncryptString(pass));

            // Get the current executing assembly
            var @assembly = Assembly.GetExecutingAssembly();

            // Retrieve the version information
            var version = assembly.GetName().Version;
            buffer.WriteString(GameState.EKeyPair.EncryptString(version.ToString()));
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void btnLogin_Click()
        {
            string user;
            string pass;

            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winLogin")];
                user = withBlock.Controls[(int)Gui.GetControlIndex("winLogin", "txtUsername")].Text;
                pass = withBlock.Controls[(int)Gui.GetControlIndex("winLogin", "txtPassword")].Text;

                if (NetworkConfig.Socket?.IsConnected == true)
                {
                    SendLogin(user, pass);
                }
                else
                {
                    GameLogic.Dialogue("Invalid Connection", "Cannot connect to game server.", "Please try again.", (byte)Core.Enum.DialogueType.Alert);
                }
            }
        }

        public static void GetPing()
        {
            var buffer = new ByteStream(4);
            GameState.PingStart = General.GetTickCount();

            buffer.WriteInt32((int)Packets.ClientPackets.CCheckPing);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendPlayerMove()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPlayerMove);
            buffer.WriteInt32(GetPlayerDir(GameState.MyIndex));
            buffer.WriteInt32(Core.Type.Player[GameState.MyIndex].Moving);
            buffer.WriteInt32(Core.Type.Player[GameState.MyIndex].X);
            buffer.WriteInt32(Core.Type.Player[GameState.MyIndex].Y);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SayMsg(string text)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSayMsg);
            buffer.WriteString(text);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendKick(string name)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CKickPlayer);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendBan(string name)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CBanPlayer);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void WarpMeTo(string name)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CWarpMeTo);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void WarpToMe(string name)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CWarpToMe);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void WarpTo(int mapNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CWarpTo);
            buffer.WriteInt32(mapNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestLevelUp()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestLevelUp);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSpawnItem(int tmpItem, int tmpAmount)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSpawnItem);
            buffer.WriteInt32(tmpItem);
            buffer.WriteInt32(tmpAmount);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSetSprite(int spriteNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSetSprite);
            buffer.WriteInt32(spriteNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSetAccess(string name, byte access)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSetAccess);
            buffer.WriteString(name);
            buffer.WriteInt32(access);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendAttack()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAttack);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendPlayerDir()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPlayerDir);
            buffer.WriteInt32(GetPlayerDir(GameState.MyIndex));

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestNPC(int NPCNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestNPC);
            buffer.WriteDouble(NPCNum);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestSkill(int skillNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestSkill);
            buffer.WriteInt32(skillNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendTrainStat(byte statNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CTrainStat);
            buffer.WriteInt32(statNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestPlayerData()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPlayerData);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void BroadcastMsg(string text)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CBroadcastMsg);
            buffer.WriteString(text);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void PlayerMsg(string text, string msgTo)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPlayerMsg);
            buffer.WriteString(msgTo);
            buffer.WriteString(text);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void AdminMsg(string text)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAdminMsg);
            buffer.WriteString(text);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendWhosOnline()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CWhosOnline);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendPlayerInfo(string name)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CPlayerInfoRequest);
            buffer.WriteString(name);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendMotdChange(string welcome)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSetMotd);
            buffer.WriteString(welcome);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendBanList()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CBanList);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendBanDestroy()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CBanDestroy);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendChangeInvSlots(int oldSlot, int newSlot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSwapInvSlots);
            buffer.WriteInt32(oldSlot);
            buffer.WriteInt32(newSlot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendChangeSkillSlots(int oldSlot, int newSlot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSwapSkillSlots);
            buffer.WriteInt32(oldSlot);
            buffer.WriteInt32(newSlot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendUseItem(int invNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CUseItem);
            buffer.WriteInt32(invNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendDropItem(int invNum, int amount)
        {
            var buffer = new ByteStream(4);

            if (GameState.InBank | GameState.InShop >= 0)
                return;

            // do basic checks
            if (invNum < 0 | invNum > Constant.MAX_INV)
                return;

            if (Core.Type.Player[GameState.MyIndex].Inv[invNum].Num < 0 | Core.Type.Player[GameState.MyIndex].Inv[invNum].Num > Constant.MAX_ITEMS)
                return;

            if (Core.Type.Item[(int)GetPlayerInv(GameState.MyIndex, invNum)].Type == (byte)Core.Enum.ItemType.Currency | Core.Type.Item[(int)GetPlayerInv(GameState.MyIndex, invNum)].Stackable == 1)
            {
                if (amount < 0 | amount > Core.Type.Player[GameState.MyIndex].Inv[invNum].Value)
                    return;
            }

            buffer.WriteInt32((int)Packets.ClientPackets.CMapDropItem);
            buffer.WriteInt32(invNum);
            buffer.WriteInt32(amount);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void PlayerSearch(int CurX, int CurY, byte rClick)
        {
            var buffer = new ByteStream(4);

            if (GameLogic.IsInBounds())
            {
                buffer.WriteInt32((int)Packets.ClientPackets.CSearch);
                buffer.WriteInt32(GameState.CurX);
                buffer.WriteInt32(GameState.CurY);
                buffer.WriteInt32(rClick);
                NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            }

            buffer.Dispose();
        }

        public static void AdminWarp(int x, int y)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAdminWarp);
            buffer.WriteInt32(x);
            buffer.WriteInt32(y);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendLeaveGame()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CQuit);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendUnequip(int eqNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CUnequip);
            buffer.WriteInt32(eqNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void ForgetSkill(int skillSlot)
        {
            var buffer = new ByteStream(4);

            // Check for subscript out of range
            if (skillSlot < 0 | skillSlot > Constant.MAX_PLAYER_SKILLS)
                return;

            // dont let them forget a skill which is in CD
            if (Core.Type.Player[GameState.MyIndex].Skill[skillSlot].CD > 0)
            {
                Text.AddText("Cannot forget a skill which is cooling down!", (int)Core.Enum.ColorType.Red);
                return;
            }

            // dont let them forget a skill which is buffered
            if (GameState.SkillBuffer == skillSlot)
            {
                Text.AddText("Cannot forget a skill which you are casting!", (int)Core.Enum.ColorType.Red);
                return;
            }

            if (Core.Type.Player[GameState.MyIndex].Skill[skillSlot].Num >= 0)
            {
                buffer.WriteInt32((int)Packets.ClientPackets.CForgetSkill);
                buffer.WriteInt32(skillSlot);
                NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            }
            else
            {
                Text.AddText("No skill found.", (int)Core.Enum.ColorType.Red);
            }

            buffer.Dispose();
        }

        public static void SendRequestMapReport()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CMapReport);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestAdmin()
        {
            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Moderator)
                return;

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CAdmin);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendUseEmote(int emote)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CEmote);
            buffer.WriteInt32(emote);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestEditResource()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditResource);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSaveResource(int ResourceNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveResource);

            buffer.WriteInt32(ResourceNum);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].Animation);
            buffer.WriteString(Core.Type.Resource[ResourceNum].EmptyMessage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ExhaustedImage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].Health);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ExpReward);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ItemReward);
            buffer.WriteString(Core.Type.Resource[ResourceNum].Name);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ResourceImage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ResourceType);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].RespawnTime);
            buffer.WriteString(Core.Type.Resource[ResourceNum].SuccessMessage);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].LvlRequired);
            buffer.WriteInt32(Core.Type.Resource[ResourceNum].ToolRequired);
            buffer.WriteInt32(Conversions.ToInteger(Core.Type.Resource[ResourceNum].Walkthrough));

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestEditNPC()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditNPC);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSaveNPC(int NPCNum)
        {
            var buffer = new ByteStream(4);
            int i;

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveNPC);
            buffer.WriteInt32(NPCNum);

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Animation);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].AttackSay);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Behaviour);

            for (i = 0; i < Constant.MAX_DROP_ITEMS; i++)
            {
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropChance[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItem[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Exp);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Faction);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].HP);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].Name);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Range);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].SpawnTime);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].SpawnSecs);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Sprite);

            for (i = 0; i < (int)Core.Enum.StatType.Count; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Stat[i]);

            for (i = 0; i < Constant.MAX_NPC_SKILLS; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Skill[i]);

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Level);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Damage);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestEditSkill()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditSkill);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSaveSkill(int skillNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveSkill);
            buffer.WriteInt32(skillNum);

            buffer.WriteInt32(Core.Type.Skill[skillNum].AccessReq);
            buffer.WriteInt32(Core.Type.Skill[skillNum].AoE);
            buffer.WriteInt32(Core.Type.Skill[skillNum].CastAnim);
            buffer.WriteInt32(Core.Type.Skill[skillNum].CastTime);
            buffer.WriteInt32(Core.Type.Skill[skillNum].CdTime);
            buffer.WriteInt32(Core.Type.Skill[skillNum].JobReq);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Dir);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Duration);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Icon);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Interval);
            buffer.WriteInt32(Conversions.ToInteger(Core.Type.Skill[skillNum].IsAoE));
            buffer.WriteInt32(Core.Type.Skill[skillNum].LevelReq);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Map);
            buffer.WriteInt32(Core.Type.Skill[skillNum].MpCost);
            buffer.WriteString(Core.Type.Skill[skillNum].Name);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Range);
            buffer.WriteInt32(Core.Type.Skill[skillNum].SkillAnim);
            buffer.WriteInt32(Core.Type.Skill[skillNum].StunDuration);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Type);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Vital);
            buffer.WriteInt32(Core.Type.Skill[skillNum].X);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Y);

            buffer.WriteInt32(Core.Type.Skill[skillNum].IsProjectile);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Projectile);

            buffer.WriteInt32(Core.Type.Skill[skillNum].KnockBack);
            buffer.WriteInt32(Core.Type.Skill[skillNum].KnockBackTiles);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendSaveShop(int shopNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveShop);
            buffer.WriteInt32(shopNum);

            buffer.WriteInt32(Core.Type.Shop[shopNum].BuyRate);
            buffer.WriteString(Core.Type.Shop[shopNum].Name);

            for (int i = 0; i < Constant.MAX_TRADES; i++)
            {
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostItem);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostValue);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].Item);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].ItemValue);
            }

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendRequestEditShop()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditShop);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSaveAnimation(int animationNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveAnimation);
            buffer.WriteInt32(animationNum);

            for (int i = 0, loopTo = Information.UBound(Core.Type.Animation[animationNum].Frames); i < loopTo; i++)
                buffer.WriteInt32(Core.Type.Animation[animationNum].Frames[i]);

            for (int i = 0, loopTo1 = Information.UBound(Core.Type.Animation[animationNum].LoopCount); i < loopTo1; i++)
                buffer.WriteInt32(Core.Type.Animation[animationNum].LoopCount[i]);

            for (int i = 0, loopTo2 = Information.UBound(Core.Type.Animation[animationNum].LoopTime); i < loopTo2; i++)
                buffer.WriteInt32(Core.Type.Animation[animationNum].LoopTime[i]);

            buffer.WriteString(Core.Type.Animation[animationNum].Name);
            buffer.WriteString(Core.Type.Animation[animationNum].Sound);

            for (int i = 0, loopTo3 = Information.UBound(Core.Type.Animation[animationNum].Sprite); i < loopTo3; i++)
                buffer.WriteInt32(Core.Type.Animation[animationNum].Sprite[i]);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestEditAnimation()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditAnimation);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestEditJob()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditJob);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSaveJob(int jobNum)
        {
            int i;
            int q;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveJob);

            buffer.WriteInt32(jobNum);

            buffer.WriteString(Core.Type.Job[jobNum].Name);
            buffer.WriteString(Core.Type.Job[jobNum].Desc);

            buffer.WriteInt32(Core.Type.Job[jobNum].MaleSprite);
            buffer.WriteInt32(Core.Type.Job[jobNum].FemaleSprite);

            for (i = 0; i < (int)Core.Enum.StatType.Count; i++)
                buffer.WriteInt32(Core.Type.Job[jobNum].Stat[i]);

            for (q = 0; q < Core.Constant.MAX_START_ITEMS; q++)
            {
                buffer.WriteInt32(Core.Type.Job[jobNum].StartItem[q]);
                buffer.WriteInt32(Core.Type.Job[jobNum].StartValue[q]);
            }

            buffer.WriteInt32(Core.Type.Job[jobNum].StartMap);
            buffer.WriteByte(Core.Type.Job[jobNum].StartX);
            buffer.WriteByte(Core.Type.Job[jobNum].StartY);

            buffer.WriteInt32(Core.Type.Job[jobNum].BaseExp);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSaveItem(int itemNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveItem);
            buffer.WriteInt32(itemNum);
            buffer.WriteInt32(Core.Type.Item[itemNum].AccessReq);

            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                buffer.WriteInt32(Core.Type.Item[itemNum].Add_Stat[i]);

            buffer.WriteInt32(Core.Type.Item[itemNum].Animation);
            buffer.WriteInt32(Core.Type.Item[itemNum].BindType);
            buffer.WriteInt32(Core.Type.Item[itemNum].JobReq);
            buffer.WriteInt32(Core.Type.Item[itemNum].Data1);
            buffer.WriteInt32(Core.Type.Item[itemNum].Data2);
            buffer.WriteInt32(Core.Type.Item[itemNum].Data3);
            buffer.WriteInt32(Core.Type.Item[itemNum].LevelReq);
            buffer.WriteInt32(Core.Type.Item[itemNum].Mastery);
            buffer.WriteString(Core.Type.Item[itemNum].Name);
            buffer.WriteInt32(Core.Type.Item[itemNum].Paperdoll);
            buffer.WriteInt32(Core.Type.Item[itemNum].Icon);
            buffer.WriteInt32(Core.Type.Item[itemNum].Price);
            buffer.WriteInt32(Core.Type.Item[itemNum].Rarity);
            buffer.WriteInt32(Core.Type.Item[itemNum].Speed);

            buffer.WriteInt32(Core.Type.Item[itemNum].Stackable);
            buffer.WriteString(Core.Type.Item[itemNum].Description);

            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                buffer.WriteInt32(Core.Type.Item[itemNum].Stat_Req[i]);

            buffer.WriteInt32(Core.Type.Item[itemNum].Type);
            buffer.WriteInt32(Core.Type.Item[itemNum].SubType);

            buffer.WriteInt32(Core.Type.Item[itemNum].ItemLevel);

            buffer.WriteInt32(Core.Type.Item[itemNum].KnockBack);
            buffer.WriteInt32(Core.Type.Item[itemNum].KnockBackTiles);

            buffer.WriteInt32(Core.Type.Item[itemNum].Projectile);
            buffer.WriteInt32(Core.Type.Item[itemNum].Ammo);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestEditItem()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditItem);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendCloseEditor()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CCloseEditor);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSetHotbarSlot(int @type, int newSlot, int oldSlot, int num)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSetHotbarSlot);

            buffer.WriteInt32(type);
            buffer.WriteInt32(newSlot);
            buffer.WriteInt32(oldSlot);
            buffer.WriteInt32(num);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendDeleteHotbar(int slot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CDeleteHotbarSlot);

            buffer.WriteInt32(slot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendUseHotbarSlot(int slot)
        {
            switch (Core.Type.Player[GameState.MyIndex].Hotbar[slot].SlotType)
            {
                case (byte)Core.Enum.PartType.Skill:
                    {
                        Player.PlayerCastSkill(Player.FindSkill((int)Core.Type.Player[GameState.MyIndex].Hotbar[slot].Slot));
                        return;
                    }
            }

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CUseHotbarSlot);

            buffer.WriteInt32(slot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendLearnSkill(int tmpSkill)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSkillLearn);
            buffer.WriteInt32(tmpSkill);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendCast(int skillSlot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CCast);
            buffer.WriteInt32(skillSlot);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

            GameState.SkillBuffer = skillSlot;
            GameState.SkillBufferTimer = General.GetTickCount();
        }

        public static void SendRequestMoral(int moralNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestMoral);
            buffer.WriteInt32(moralNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendRequestEditMoral()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditMoral);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendSaveMoral(int moralNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveMoral);
            buffer.WriteInt32(moralNum);

            {
                ref var withBlock = ref Core.Type.Moral[moralNum];
                buffer.WriteString(withBlock.Name);
                buffer.WriteByte(withBlock.Color);
                buffer.WriteBoolean(withBlock.CanCast);
                buffer.WriteBoolean(withBlock.CanPK);
                buffer.WriteBoolean(withBlock.CanDropItem);
                buffer.WriteBoolean(withBlock.CanPickupItem);
                buffer.WriteBoolean(withBlock.CanUseItem);
                buffer.WriteBoolean(withBlock.DropItems);
                buffer.WriteBoolean(withBlock.LoseExp);
                buffer.WriteBoolean(withBlock.PlayerBlock);
                buffer.WriteBoolean(withBlock.NPCBlock);
            }

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendCloseShop()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CCloseShop);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

    }
}