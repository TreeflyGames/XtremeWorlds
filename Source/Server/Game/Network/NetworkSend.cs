using System;
using System.Linq;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Mirage.Sharp.Asfw.IO;
using static Core.Enum;
using static Core.Packets;
using static Core.Global.Command;
using static Core.Type;
using System.Buffers;

namespace Server
{

    public class NetworkSend
    {

        public static void AlertMsg(int index, byte menuNo, int menuReset = 0, bool kick = true)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SAlertMsg);
            buffer.WriteByte(menuNo);
            buffer.WriteInt32(menuReset);
            buffer.WriteInt32(kick ? 1 : 0);

            Database.ClearAccount((int)index);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void GlobalMsg(string msg)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SGlobalMsg);
            buffer.WriteString(msg);
            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void PlayerMsg(int index, string msg, int color)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerMsg);
            buffer.WriteString(msg);
            buffer.WriteInt32(color);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendPlayerChars(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerChars);

            // loop through each character. clear, load, add. repeat.
            for (int i = 0, loopTo = Core.Constant.MAX_CHARS; i < loopTo; i++)
            {
                Database.LoadCharacter(index, Conversions.ToInteger(i + 1));

                buffer.WriteString(Core.Type.Player[index].Name);
                buffer.WriteInt32(Core.Type.Player[index].Sprite);
                buffer.WriteInt32(Core.Type.Player[index].Access);
                buffer.WriteInt32(Core.Type.Player[index].Job);

                Database.ClearCharacter(index);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateJob(int index, int jobNum)
        {
            int i;
            int n;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SUpdateJob);
            buffer.WriteInt32(jobNum);

            buffer.WriteString(Core.Type.Job[jobNum].Name);
            buffer.WriteString(Core.Type.Job[jobNum].Desc);

            buffer.WriteInt32(Core.Type.Job[jobNum].MaleSprite);
            buffer.WriteInt32(Core.Type.Job[jobNum].FemaleSprite);

            for (int q = 0, loopTo = (int)(StatType.Count); q < loopTo; q++)
                buffer.WriteInt32(Core.Type.Job[jobNum].Stat[Conversions.ToInteger(q)]);

            for (int q = 0; q < Core.Constant.MAX_START_ITEMS; q++)
            {
                buffer.WriteInt32(Core.Type.Job[jobNum].StartItem[q]);
                buffer.WriteInt32(Core.Type.Job[jobNum].StartValue[q]);
            }

            buffer.WriteInt32(Core.Type.Job[jobNum].StartMap);
            buffer.WriteByte(Core.Type.Job[jobNum].StartX);
            buffer.WriteByte(Core.Type.Job[jobNum].StartY);
            buffer.WriteInt32(Core.Type.Job[jobNum].BaseExp);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendCloseTrade(int index)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SCloseTrade);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendExp(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerEXP);
            buffer.WriteInt32(index);
            buffer.WriteInt32(GetPlayerExp(index));
            buffer.WriteInt32(GetPlayerNextLevel(index));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendLoginOK(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SLoginOK);
            buffer.WriteInt32(index);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendInGame(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SInGame);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendJobs(int index)
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SJobData);

            var loopTo = Core.Constant.MAX_JOBS;
            for (i = 0; i < loopTo; i++)
                buffer.WriteBlock(Database.JobData(i));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendJobToAll(int jobNum)
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SJobData);
            buffer.WriteBlock(Database.JobData(jobNum));
            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateJobTo(int index, int jobNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SUpdateJob);

            buffer.WriteBlock(Database.JobData(jobNum));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateJobToAll(int jobNum)
        {
            ByteStream buffer;
            buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SUpdateJob);

            buffer.WriteBlock(Database.JobData(jobNum));

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendInventory(int index)
        {
            int i;
            int n;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerInv);

            var loopTo = Core.Constant.MAX_INV;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(GetPlayerInv(index, i));
                buffer.WriteInt32(GetPlayerInvValue(index, i));
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendLeftMap(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SLeftMap);
            buffer.WriteInt32(index);
            NetworkConfig.SendDataToAllBut(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendLeftGame(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SLeftGame);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendMapEquipment(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SMapWornEq);
            buffer.WriteInt32(index);
            for (int i = 0, loopTo = (int)(EquipmentType.Count); i < loopTo; i++)
                buffer.WriteInt32(GetPlayerEquipment(index, (EquipmentType)i));

            NetworkConfig.SendDataToMap(GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapEquipmentTo(int PlayerNum, int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SMapWornEq);
            buffer.WriteInt32(PlayerNum);
            for (int i = 0, loopTo = (int)(EquipmentType.Count); i < loopTo; i++)
                buffer.WriteInt32(GetPlayerEquipment(PlayerNum, (EquipmentType)i));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendShops(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_SHOPS;
            for (i = 0; i < loopTo; i++)
            {
                if (Core.Type.Shop[i].Name.Length > 0)
                {
                    SendUpdateShopTo(index, i);
                }
            }

        }

        public static void SendUpdateShopTo(int index, int shopNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SUpdateShop);

            buffer.WriteInt32(shopNum);

            buffer.WriteInt32(Core.Type.Shop[shopNum].BuyRate);
            buffer.WriteString(Core.Type.Shop[shopNum].Name);

            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostItem);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostValue);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].Item);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].ItemValue);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateShopToAll(int shopNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SUpdateShop);

            buffer.WriteInt32(shopNum);
            buffer.WriteInt32(Core.Type.Shop[shopNum].BuyRate);
            buffer.WriteString(Core.Type.Shop[shopNum].Name);

            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostItem);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostValue);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].Item);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].ItemValue);
            }

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendSkills(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_SKILLS;
            for (i = 0; i < loopTo; i++)
            {
                if (Core.Type.Skill[i].Name.Length > 0)
                {
                    SendUpdateSkillTo(index, i);
                }
            }
        }

        public static void SendUpdateSkillTo(int index, int skillNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SUpdateSkill);
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

            // projectiles
            buffer.WriteInt32(Core.Type.Skill[skillNum].IsProjectile);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Projectile);

            buffer.WriteInt32(Core.Type.Skill[skillNum].KnockBack);
            buffer.WriteInt32(Core.Type.Skill[skillNum].KnockBackTiles);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendUpdateSkillToAll(int skillNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SUpdateSkill);
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

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendStats(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerStats);
            buffer.WriteInt32(index);

            for (int i = 0, loopTo = (int)(StatType.Count); i < loopTo; i++)
                buffer.WriteInt32(GetPlayerStat(index, (StatType)i));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendVitals(int index)
        {
            for (int i = 0, loopTo = (int)(VitalType.Count); i < loopTo; i++)
                SendVital(index, (VitalType)i);
        }

        public static void SendVital(int index, VitalType vital)
        {
            var buffer = new ByteStream(4);

            // Get our packet type.
            switch (vital)
            {
                case VitalType.HP:
                    {
                        buffer.WriteInt32((int)ServerPackets.SPlayerHP);
                        break;
                    }
                case VitalType.SP:
                    {
                        buffer.WriteInt32((int)ServerPackets.SPlayerSP);
                        break;
                    }
            }

            // Set and send related data.
            buffer.WriteInt32(GetPlayerVital(index, vital));
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

            // send vitals to party if in one
            if (Core.Type.TempPlayer[index].InParty >= 0)
                Party.SendPartyVitals(Core.Type.TempPlayer[index].InParty, index);
        }

        public static void SendWelcome(int index)
        {
            // Send them welcome
            if (SettingsManager.Instance.Welcome.Length > 0)
            {
                PlayerMsg(index, SettingsManager.Instance.Welcome, (int)(int)ColorType.BrightCyan);
            }

            // Send whos online
            SendWhosOnline(index);
        }

        public static void SendWhosOnline(int index)
        {
            string s = "";
            var n = default(int);
            int i;

            if (GetPlayerAccess(index) < (int)AccessType.Moderator)
                return;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i < loopTo; i++)
            {
                if (i != index & GetPlayerName(i) != "")
                {
                    s = s + GetPlayerName(i) + ", ";
                    n = n + 1;
                }
            }

            if (n == 0)
            {
                s = "There are no other players online.";
            }
            else
            {
                s = Strings.Mid(s, 1, Strings.Len(s) - 2);
                s = "There are " + n + " other players online: " + s + ".";
            }

            PlayerMsg(index, s, (int)(int)ColorType.White);
        }

        public static void SendWornEquipment(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerWornEq);

            for (int i = 0, loopTo = (int)(EquipmentType.Count); i < loopTo; i++)
                buffer.WriteInt32(GetPlayerEquipment(index, (EquipmentType)i));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapData(int index, int mapNum, bool SendMap)
        {
            var buffer = new ByteStream(4);
            byte[] data;

            if (SendMap)
            {
                buffer.WriteInt32(1);
                buffer.WriteInt32(mapNum);
                buffer.WriteString(Core.Type.Map[mapNum].Name);
                buffer.WriteString(Core.Type.Map[mapNum].Music);
                buffer.WriteInt32(Core.Type.Map[mapNum].Revision);
                buffer.WriteInt32(Core.Type.Map[mapNum].Moral);
                buffer.WriteInt32(Core.Type.Map[mapNum].Tileset);
                buffer.WriteInt32(Core.Type.Map[mapNum].Up);
                buffer.WriteInt32(Core.Type.Map[mapNum].Down);
                buffer.WriteInt32(Core.Type.Map[mapNum].Left);
                buffer.WriteInt32(Core.Type.Map[mapNum].Right);
                buffer.WriteInt32(Core.Type.Map[mapNum].BootMap);
                buffer.WriteInt32(Core.Type.Map[mapNum].BootX);
                buffer.WriteInt32(Core.Type.Map[mapNum].BootY);
                buffer.WriteInt32(Core.Type.Map[mapNum].MaxX);
                buffer.WriteInt32(Core.Type.Map[mapNum].MaxY);
                buffer.WriteInt32(Core.Type.Map[mapNum].Weather);
                buffer.WriteInt32(Core.Type.Map[mapNum].Fog);
                buffer.WriteInt32(Core.Type.Map[mapNum].WeatherIntensity);
                buffer.WriteInt32(Core.Type.Map[mapNum].FogOpacity);
                buffer.WriteInt32(Core.Type.Map[mapNum].FogSpeed);
                buffer.WriteInt32(Conversions.ToInteger(Core.Type.Map[mapNum].MapTint));
                buffer.WriteInt32(Core.Type.Map[mapNum].MapTintR);
                buffer.WriteInt32(Core.Type.Map[mapNum].MapTintG);
                buffer.WriteInt32(Core.Type.Map[mapNum].MapTintB);
                buffer.WriteInt32(Core.Type.Map[mapNum].MapTintA);
                buffer.WriteByte(Core.Type.Map[mapNum].Panorama);
                buffer.WriteByte(Core.Type.Map[mapNum].Parallax);
                buffer.WriteByte(Core.Type.Map[mapNum].Brightness);
                buffer.WriteInt32(Conversions.ToInteger(Core.Type.Map[mapNum].NoRespawn));
                buffer.WriteInt32(Conversions.ToInteger(Core.Type.Map[mapNum].Indoors));
                buffer.WriteInt32(Core.Type.Map[mapNum].Shop);

                for (int i = 0, loopTo = Core.Constant.MAX_MAP_NPCS; i < loopTo; i++)
                    buffer.WriteInt32(Core.Type.Map[mapNum].NPC[i]);

                for (int x = 0, loopTo1 = Core.Type.Map[mapNum].MaxX; x < (int)loopTo1; x++)
                {
                    for (int y = 0, loopTo2 = Core.Type.Map[mapNum].MaxY; y < (int)loopTo2; y++)
                    {
                        buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Data1);
                        buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Data2);
                        buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Data3);
                        buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Data1_2);
                        buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Data2_2);
                        buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Data3_2);
                        buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].DirBlock);
                        for (int i = 0, loopTo3 = (int)(LayerType.Count); i < loopTo3; i++)
                        {
                            buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Layer[i].Tileset);
                            buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Layer[i].X);
                            buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Layer[i].Y);
                            buffer.WriteInt32(Core.Type.Map[mapNum].Tile[x, y].Layer[i].AutoTile);
                        }
                        buffer.WriteInt32((int)Core.Type.Map[mapNum].Tile[x, y].Type);
                        buffer.WriteInt32((int)Core.Type.Map[mapNum].Tile[x, y].Type2);
                    }
                }

                buffer.WriteInt32(Core.Type.Map[mapNum].EventCount);

                if (Core.Type.Map[mapNum].EventCount > 0)
                {
                    for (int i = 0, loopTo4 = Core.Type.Map[mapNum].EventCount; i < loopTo4; i++)
                    {
                        {
                            ref var withBlock = ref Core.Type.Map[mapNum].Event[i];
                            buffer.WriteString(withBlock.Name);
                            buffer.WriteByte(withBlock.Globals);
                            buffer.WriteInt32(withBlock.X);
                            buffer.WriteInt32(withBlock.Y);
                            buffer.WriteInt32(withBlock.PageCount);
                        }

                        if (Core.Type.Map[mapNum].Event[i].PageCount > 0)
                        {
                            for (int X = 0, loopTo5 = Core.Type.Map[mapNum].Event[i].PageCount; X < loopTo5; X++)
                            {
                                {
                                    ref var withBlock1 = ref Core.Type.Map[mapNum].Event[i].Pages[X];
                                    buffer.WriteInt32(withBlock1.ChkVariable);
                                    buffer.WriteInt32(withBlock1.VariableIndex);
                                    buffer.WriteInt32(withBlock1.VariableCondition);
                                    buffer.WriteInt32(withBlock1.VariableCompare);
                                    buffer.WriteInt32(withBlock1.ChkSwitch);
                                    buffer.WriteInt32(withBlock1.SwitchIndex);
                                    buffer.WriteInt32(withBlock1.SwitchCompare);
                                    buffer.WriteInt32(withBlock1.ChkHasItem);
                                    buffer.WriteInt32(withBlock1.HasItemIndex);
                                    buffer.WriteInt32(withBlock1.HasItemAmount);
                                    buffer.WriteInt32(withBlock1.ChkSelfSwitch);
                                    buffer.WriteInt32(withBlock1.SelfSwitchIndex);
                                    buffer.WriteInt32(withBlock1.SelfSwitchCompare);
                                    buffer.WriteByte(withBlock1.GraphicType);
                                    buffer.WriteInt32(withBlock1.Graphic);
                                    buffer.WriteInt32(withBlock1.GraphicX);
                                    buffer.WriteInt32(withBlock1.GraphicY);
                                    buffer.WriteInt32(withBlock1.GraphicX2);
                                    buffer.WriteInt32(withBlock1.GraphicY2);
                                    buffer.WriteByte(withBlock1.MoveType);
                                    buffer.WriteByte(withBlock1.MoveSpeed);
                                    buffer.WriteByte(withBlock1.MoveFreq);
                                    buffer.WriteInt32(withBlock1.MoveRouteCount);
                                    buffer.WriteInt32(withBlock1.IgnoreMoveRoute);
                                    buffer.WriteInt32(withBlock1.RepeatMoveRoute);

                                    if (withBlock1.MoveRouteCount > 0)
                                    {
                                        for (int Y = 0, loopTo6 = withBlock1.MoveRouteCount; Y < (int)loopTo6; Y++)
                                        {
                                            buffer.WriteInt32(withBlock1.MoveRoute[Y].Index);
                                            buffer.WriteInt32(withBlock1.MoveRoute[Y].Data1);
                                            buffer.WriteInt32(withBlock1.MoveRoute[Y].Data2);
                                            buffer.WriteInt32(withBlock1.MoveRoute[Y].Data3);
                                            buffer.WriteInt32(withBlock1.MoveRoute[Y].Data4);
                                            buffer.WriteInt32(withBlock1.MoveRoute[Y].Data5);
                                            buffer.WriteInt32(withBlock1.MoveRoute[Y].Data6);
                                        }
                                    }

                                    buffer.WriteInt32(withBlock1.WalkAnim);
                                    buffer.WriteInt32(withBlock1.DirFix);
                                    buffer.WriteInt32(withBlock1.WalkThrough);
                                    buffer.WriteInt32(withBlock1.ShowName);
                                    buffer.WriteByte(withBlock1.Trigger);
                                    buffer.WriteInt32(withBlock1.CommandListCount);
                                    buffer.WriteByte(withBlock1.Position);
                                }

                                if (Core.Type.Map[mapNum].Event[i].Pages[X].CommandListCount > 0)
                                {
                                    for (int Y = 0, loopTo7 = Core.Type.Map[mapNum].Event[i].Pages[X].CommandListCount; Y < loopTo7; Y++)
                                    {
                                        buffer.WriteInt32(Core.Type.Map[mapNum].Event[i].Pages[X].CommandList[Y].CommandCount);
                                        buffer.WriteInt32(Core.Type.Map[mapNum].Event[i].Pages[X].CommandList[Y].ParentList);
                                        if (Core.Type.Map[mapNum].Event[i].Pages[X].CommandList[Y].CommandCount > 0)
                                        {
                                            for (int z = 0, loopTo8 = Core.Type.Map[mapNum].Event[i].Pages[X].CommandList[Y].CommandCount; z < loopTo8; z++)
                                            {
                                                {
                                                    ref var withBlock2 = ref Core.Type.Map[mapNum].Event[i].Pages[X].CommandList[Y].Commands[z];
                                                    buffer.WriteInt32(withBlock2.Index);
                                                    buffer.WriteString(withBlock2.Text1);
                                                    buffer.WriteString(withBlock2.Text2);
                                                    buffer.WriteString(withBlock2.Text3);
                                                    buffer.WriteString(withBlock2.Text4);
                                                    buffer.WriteString(withBlock2.Text5);
                                                    buffer.WriteInt32(withBlock2.Data1);
                                                    buffer.WriteInt32(withBlock2.Data2);
                                                    buffer.WriteInt32(withBlock2.Data3);
                                                    buffer.WriteInt32(withBlock2.Data4);
                                                    buffer.WriteInt32(withBlock2.Data5);
                                                    buffer.WriteInt32(withBlock2.Data6);
                                                    buffer.WriteInt32(withBlock2.ConditionalBranch.CommandList);
                                                    buffer.WriteInt32(withBlock2.ConditionalBranch.Condition);
                                                    buffer.WriteInt32(withBlock2.ConditionalBranch.Data1);
                                                    buffer.WriteInt32(withBlock2.ConditionalBranch.Data2);
                                                    buffer.WriteInt32(withBlock2.ConditionalBranch.Data3);
                                                    buffer.WriteInt32(withBlock2.ConditionalBranch.ElseCommandList);
                                                    buffer.WriteInt32(withBlock2.MoveRouteCount);
                                                    if (withBlock2.MoveRouteCount > 0)
                                                    {
                                                        for (int w = 0, loopTo9 = withBlock2.MoveRouteCount; w < loopTo9; w++)
                                                        {
                                                            buffer.WriteInt32(withBlock2.MoveRoute[w].Index);
                                                            buffer.WriteInt32(withBlock2.MoveRoute[w].Data1);
                                                            buffer.WriteInt32(withBlock2.MoveRoute[w].Data2);
                                                            buffer.WriteInt32(withBlock2.MoveRoute[w].Data3);
                                                            buffer.WriteInt32(withBlock2.MoveRoute[w].Data4);
                                                            buffer.WriteInt32(withBlock2.MoveRoute[w].Data5);
                                                            buffer.WriteInt32(withBlock2.MoveRoute[w].Data6);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                buffer.WriteInt32(0);
            }

            for (int i = 0, loopTo10 = Core.Constant.MAX_MAP_ITEMS; i < loopTo10; i++)
            {
                buffer.WriteInt32((int)Core.Type.MapItem[mapNum, i].Num);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].Value);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].X);
                buffer.WriteInt32(Core.Type.MapItem[mapNum, i].Y);
            }

            for (int i = 0, loopTo11 = Core.Constant.MAX_MAP_NPCS; i < loopTo11; i++)
            {
                buffer.WriteInt32((int)Core.Type.MapNPC[mapNum].NPC[i].Num);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].X);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Y);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Dir);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(int)VitalType.HP]);
                buffer.WriteInt32(Core.Type.MapNPC[mapNum].NPC[i].Vital[(int)VitalType.SP]);
            }

            if (Core.Type.MapResource[GetPlayerMap(index)].ResourceCount > 0)
            {
                buffer.WriteInt32(1);
                buffer.WriteInt32(Core.Type.MapResource[GetPlayerMap(index)].ResourceCount);

                for (int i = 0, loopTo12 = Core.Type.MapResource[GetPlayerMap(index)].ResourceCount; i < loopTo12; i++)
                {
                    buffer.WriteByte(Core.Type.MapResource[GetPlayerMap(index)].ResourceData[i].State);
                    buffer.WriteInt32(Core.Type.MapResource[GetPlayerMap(index)].ResourceData[i].X);
                    buffer.WriteInt32(Core.Type.MapResource[GetPlayerMap(index)].ResourceData[i].Y);
                }
            }
            else
            {
                buffer.WriteInt32(0);
            }

            data = Compression.CompressBytes(buffer.ToArray());
            buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SMapData);
            buffer.WriteBlock(data);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendJoinMap(int index)
        {
            int i;
            byte[] data;
            int dataSize;
            int mapNum = GetPlayerMap(index);

            // Send all players on current map to index
            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i < loopTo; i++)
            {
                if (IsPlaying(i))
                {
                    if (i != index)
                    {
                        if (GetPlayerMap(i) == mapNum)
                        {
                            data = PlayerData(i);
                            dataSize = data.Length;
                            NetworkConfig.Socket.SendDataTo(index, data, dataSize);
                            SendPlayerXYTo(index, i);
                            NetworkSend.SendMapEquipmentTo(index, i);
                        }
                    }
                }
            }

            EventLogic.SpawnMapEventsFor(index, GetPlayerMap(index));

            // Send index's player data to everyone on the map including himself
            data = PlayerData(index);
            NetworkConfig.SendDataToMap(mapNum, data, data.Length);
            SendPlayerXYToMap(index);
            NetworkSend.SendMapEquipment(index);
            NetworkSend.SendVitals(index);
        }

        public static byte[] PlayerData(int index)
        {
            byte[] PlayerDataRet = default;
            var buffer = new ByteStream(4);
            int i;

            buffer.WriteInt32((int)ServerPackets.SPlayerData);
            buffer.WriteInt32(index);
            buffer.WriteString(GetPlayerName(index));
            buffer.WriteInt32(GetPlayerJob(index));
            buffer.WriteInt32(GetPlayerLevel(index));
            buffer.WriteInt32(GetPlayerPoints(index));
            buffer.WriteInt32(GetPlayerSprite(index));
            buffer.WriteInt32(GetPlayerMap(index));
            buffer.WriteInt32(GetPlayerAccess(index));
            buffer.WriteBoolean(GetPlayerPK(index));

            var loopTo = (int)StatType.Count;
            for (i = 0; i < loopTo; i++)
                buffer.WriteInt32(GetPlayerStat(index, (StatType)i));

            var loopTo1 = (int)ResourceType.Count;
            for (i = 0; i < loopTo1; i++)
            {
                buffer.WriteInt32(GetPlayerGatherSkillLvl(index, i));
                buffer.WriteInt32(GetPlayerGatherSkillExp(index, i));
                buffer.WriteInt32(GetPlayerGatherSkillMaxExp(index, i));
            }

            PlayerDataRet = buffer.ToArray();

            buffer.Dispose();
            return PlayerDataRet;
        }

        public static void SendPlayerXY(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerXY);
            buffer.WriteInt32(index);
            buffer.WriteInt32(GetPlayerX(index));
            buffer.WriteInt32(GetPlayerY(index));
            buffer.WriteInt32(GetPlayerDir(index));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendPlayerXYTo(int index, int playerNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerXY);
            buffer.WriteInt32(playerNum);
            buffer.WriteInt32(GetPlayerX(playerNum));
            buffer.WriteInt32(GetPlayerY(playerNum));
            buffer.WriteInt32(GetPlayerDir(playerNum));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendPlayerXYToMap(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerXY);
            buffer.WriteInt32(index);
            buffer.WriteInt32(GetPlayerX(index));
            buffer.WriteInt32(GetPlayerY(index));
            buffer.WriteInt32(GetPlayerDir(index));

            NetworkConfig.SendDataToMap(GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendPlayerMove(int index, int Movement)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SPlayerMove);
            buffer.WriteInt32(index);
            buffer.WriteInt32(GetPlayerX(index));
            buffer.WriteInt32(GetPlayerY(index));
            buffer.WriteInt32(GetPlayerDir(index));
            buffer.WriteInt32(Movement);

            NetworkConfig.SendDataToMap(GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void MapMsg(int mapNum, string Msg, byte Color)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SMapMsg);
            buffer.WriteString(Msg);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void AdminMsg(int mapNum, string Msg, byte Color)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SAdminMsg);
            buffer.WriteString(Msg);

            for (int i = 0, loopTo = NetworkConfig.Socket.HighIndex; i < loopTo; i++)
            {
                if (GetPlayerAccess(i) >= (int)AccessType.Moderator)
                {
                    NetworkConfig.SendDataTo(i, buffer.UnreadData, buffer.WritePosition);
                }
            }

            buffer.Dispose();
        }

        public static void SendActionMsg(int mapNum, string Message, int Color, int MsgType, int X, int Y, int PlayerOnlyNum = -1)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SActionMsg);
            buffer.WriteString(Message);
            buffer.WriteInt32(Color);
            buffer.WriteInt32(MsgType);
            buffer.WriteInt32(X);
            buffer.WriteInt32(Y);

            if (PlayerOnlyNum >= 0)
            {
                NetworkConfig.Socket.SendDataTo(PlayerOnlyNum, buffer.UnreadData, buffer.WritePosition);
            }
            else
            {
                NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            }

            buffer.Dispose();
        }

        public static void SayMsg_Map(int mapNum, int index, string Message, int SayColor)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SSayMsg);
            buffer.WriteString(GetPlayerName(index));
            buffer.WriteInt32(GetPlayerAccess(index));
            buffer.WriteBoolean(GetPlayerPK(index));
            buffer.WriteString(Message);
            buffer.WriteString("[Map]:");
            buffer.WriteInt32(SayColor);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SayMsg_Global(int index, string Message, int SayColor)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SSayMsg);
            buffer.WriteString(GetPlayerName(index));
            buffer.WriteInt32(GetPlayerAccess(index));
            buffer.WriteBoolean(GetPlayerPK(index));
            buffer.WriteString(Message);
            buffer.WriteString("[Global]:");
            buffer.WriteInt32(SayColor);

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendPlayerData(int index)
        {
            byte[] data = PlayerData(index);
            NetworkConfig.SendDataToMap(GetPlayerMap(index), data, data.Length);
        }

        public static void SendInventoryUpdate(int index, int InvSlot)
        {
            var buffer = new ByteStream(4);
            int n;

            buffer.WriteInt32((int)ServerPackets.SPlayerInvUpdate);

            buffer.WriteInt32(InvSlot);

            buffer.WriteInt32(GetPlayerInv(index, InvSlot));
            buffer.WriteInt32(GetPlayerInvValue(index, InvSlot));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendOpenShop(int index, int ShopNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SOpenShop);
            buffer.WriteInt32(ShopNum);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void ResetShopAction(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SResetShopAction);

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendBank(int index)
        {
            var buffer = new ByteStream(4);
            int i;

            buffer.WriteInt32((int)ServerPackets.SBank);

            var loopTo = Core.Constant.MAX_BANK;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32((int)Bank[index].Item[i].Num);
                buffer.WriteInt32(Bank[index].Item[i].Value);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendClearSkillBuffer(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SClearSkillBuffer);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendTradeInvite(int index, int Tradeindex)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.STradeInvite);
            buffer.WriteInt32(Tradeindex);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendTrade(int index, int TradeTarget)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.STrade);
            buffer.WriteInt32(TradeTarget);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendTradeUpdate(int index, byte DataType)
        {
            var buffer = new ByteStream(4);
            int i;
            double tradeTarget;
            var totalWorth = default(int);

            tradeTarget = Core.Type.TempPlayer[index].InTrade;

            if (tradeTarget == -1)
                return;

            buffer.WriteInt32((int)ServerPackets.STradeUpdate);
            buffer.WriteInt32(DataType);

            if (DataType == 0) // own inventory
            {
                var loopTo = Core.Constant.MAX_INV;
                for (i = 0; i < loopTo; i++)
                {
                    if (Core.Type.TempPlayer[index].TradeOffer[i].Num >= 0)
                    {
                        buffer.WriteInt32((int)Core.Type.TempPlayer[index].TradeOffer[i].Num);
                        buffer.WriteInt32(Core.Type.TempPlayer[index].TradeOffer[i].Value);

                        // add total worth
                        // currency?
                        if (Core.Type.Item[(int)Core.Type.TempPlayer[index].TradeOffer[i].Num].Type == (int)ItemType.Currency || Core.Type.Item[(int)Core.Type.TempPlayer[index].TradeOffer[i].Num].Stackable == 1)
                        {
                            if (Core.Type.TempPlayer[index].TradeOffer[i].Value == 0)
                                Core.Type.TempPlayer[index].TradeOffer[i].Value = 0;
                            totalWorth = totalWorth + Core.Type.Item[GetPlayerInv(index, (int)Core.Type.TempPlayer[index].TradeOffer[i].Num)].Price * Core.Type.TempPlayer[index].TradeOffer[i].Value;
                        }
                        else
                        {
                            totalWorth = totalWorth + Core.Type.Item[GetPlayerInv(index, (int)Core.Type.TempPlayer[index].TradeOffer[i].Num)].Price;
                        }
                    }
                    else
                    {
                        buffer.WriteInt32(-1);
                        buffer.WriteInt32(0);
                    }
                }
            }
            else if (DataType == 1) // other inventory
            {
                var loopTo1 = Core.Constant.MAX_INV;
                for (i = 0; i < loopTo1; i++)
                {
                    if (Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Num >= 0)
                    {
                        buffer.WriteInt32(GetPlayerInv((int)tradeTarget, (int)Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Num));
                        buffer.WriteInt32(Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Value);

                        // add total worth
                        if (GetPlayerInv((int)tradeTarget, (int)Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Num) >= 0)
                        {
                            // currency?
                            if (Core.Type.Item[GetPlayerInv((int)tradeTarget, (int)Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Num)].Type == (int)ItemType.Currency || Core.Type.Item[GetPlayerInv((int)tradeTarget, (int)Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Num)].Stackable == 1)
                            {
                                if (Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Value == 0)
                                    Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Value = 0;
                                totalWorth = totalWorth + Core.Type.Item[GetPlayerInv((int)tradeTarget, (int)Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Num)].Price * Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Value;
                            }
                            else
                            {
                                totalWorth = totalWorth + Core.Type.Item[GetPlayerInv((int)tradeTarget, (int)Core.Type.TempPlayer[(int)tradeTarget].TradeOffer[i].Num)].Price;
                            }
                        }
                    }
                    else
                    {
                        buffer.WriteInt32(-1);
                        buffer.WriteInt32(0);
                    }
                }
            }

            // send total worth of trade
            buffer.WriteInt32(totalWorth);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendTradeStatus(int index, byte Status)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.STradeStatus);
            buffer.WriteInt32(Status);
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendStunned(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SStunned);
            buffer.WriteInt32(Core.Type.TempPlayer[index].StunDuration);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendBlood(int mapNum, int X, int Y)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SBlood);
            buffer.WriteInt32(X);
            buffer.WriteInt32(Y);

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendPlayerSkills(int index)
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SSkills);

            var loopTo = Core.Constant.MAX_PLAYER_SKILLS;
            for (i = 0; i < loopTo; i++)
                buffer.WriteInt32(GetPlayerSkill(index, i));

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendCooldown(int index, int Slot)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SCooldown);
            buffer.WriteInt32(Slot);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendTarget(int index, int Target, int TargetType)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.STarget);
            buffer.WriteInt32(Target);
            buffer.WriteInt32(TargetType);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapReport(int index)
        {
            var buffer = new ByteStream(4);
            int I;

            buffer.WriteInt32((int)ServerPackets.SMapReport);

            var loopTo = Core.Constant.MAX_MAPS;
            for (var i = 0; i < loopTo; i++)
                buffer.WriteString(Core.Type.Map[i].Name);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendAdminPanel(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SAdmin);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendMapNames(int index)
        {
            var buffer = new ByteStream(4);
            int I;

            buffer.WriteInt32((int)ServerPackets.SMapNames);

            var loopTo = Core.Constant.MAX_MAPS;
            for (var i = 0; i < loopTo; i++)
                buffer.WriteString(Core.Type.Map[i].Name);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendHotbar(int index)
        {
            var buffer = new ByteStream(4);
            int i;

            buffer.WriteInt32((int)ServerPackets.SHotbar);

            var loopTo = Core.Constant.MAX_HOTBAR;
            for (i = 0; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.Player[index].Hotbar[i].Slot);
                buffer.WriteInt32(Core.Type.Player[index].Hotbar[i].SlotType);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendCritical(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SCritical);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendKeyPair(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SKeyPair);
            buffer.WriteString(Global.EKeyPair.ExportKeyString(false));
            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendRightClick(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SrClick);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendJobEditor(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SJobEditor);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendEmote(int index, int Emote)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SEmote);

            buffer.WriteInt32(index);
            buffer.WriteInt32(Emote);

            NetworkConfig.SendDataToMap(GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendChatBubble(int mapNum, int Target, int TargetType, string Message, int Color)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SChatBubble);

            buffer.WriteInt32(Target);
            buffer.WriteInt32(TargetType);
            buffer.WriteString(Message);
            buffer.WriteInt32(Color);
            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void SendPlayerAttack(int index)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SAttack);
            buffer.WriteInt32(index);
            NetworkConfig.SendDataToMapBut(index, GetPlayerMap(index), buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        public static void SendNPCAttack(int mapNum, int npcIndex)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SNPCAttack);
            buffer.WriteInt32(npcIndex);
            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }
    }
}