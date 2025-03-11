using System;
using Core;
using Core;
using static Core.Global.Command;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Mirage.Sharp.Asfw.IO;

namespace Client
{

    static class NetworkReceive
    {

        public static void PacketRouter()
        {
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SAlertMsg] = Packet_AlertMsg;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SKeyPair] = Packet_KeyPair;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SLoginOK] = Packet_LoginOk;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerChars] = Packet_PlayerChars;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateJob] = Packet_UpdateJob;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SJobData] = Packet_JobData;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SInGame] = Packet_InGame;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerInv] = Packet_PlayerInv;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerInvUpdate] = Packet_PlayerInvUpdate;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerWornEq] = Packet_PlayerWornEquipment;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerHP] = Player.Packet_PlayerHP;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerSP] = Player.Packet_PlayerSP;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerStats] = Player.Packet_PlayerStats;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerData] = Player.Packet_PlayerData;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerMove] = Player.Packet_PlayerMove;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SNPCMove] = Packet_NPCMove;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerDir] = Player.Packet_PlayerDir;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SNPCDir] = Packet_NPCDir;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerXY] = Player.Packet_PlayerXY;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SAttack] = Packet_Attack;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SNPCAttack] = Packet_NPCAttack;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SCheckForMap] = Map.Packet_CheckMap;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapData] = Map.MapData;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapNPCData] = Map.Packet_MapNPCData;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapNPCUpdate] = Map.Packet_MapNPCUpdate;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapDone] = Map.Packet_MapDone;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SGlobalMsg] = Packet_GlobalMsg;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SAdminMsg] = Packet_AdminMsg;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerMsg] = Packet_PlayerMsg;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapMsg] = Packet_MapMsg;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSpawnItem] = Packet_SpawnItem;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateItem] = Item.Packet_UpdateItem;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSpawnNPC] = Packet_SpawnNPC;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SNPCDead] = Packet_NPCDead;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateNPC] = Packet_UpdateNPC;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SEditMap] = Map.Packet_EditMap;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateShop] = Shop.Packet_UpdateShop;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateSkill] = Packet_UpdateSkill;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSkills] = Packet_Skills;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SLeftMap] = Packet_LeftMap;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapResource] = Resource.Packet_MapResource;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateResource] = Resource.Packet_UpdateResource;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSendPing] = Packet_Ping;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SActionMsg] = Packet_ActionMessage;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayerEXP] = Player.Packet_PlayerExp;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SBlood] = Packet_Blood;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateAnimation] = Animation.Packet_UpdateAnimation;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SAnimation] = Animation.Packet_Animation;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapNPCVitals] = Packet_NPCVitals;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SCooldown] = Packet_Cooldown;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SClearSkillBuffer] = Packet_ClearSkillBuffer;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSayMsg] = Packet_SayMessage;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SOpenShop] = Shop.Packet_OpenShop;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SResetShopAction] = Shop.Packet_ResetShopAction;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SStunned] = Packet_Stunned;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapWornEq] = Packet_MapWornEquipment;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SBank] = Bank.Packet_OpenBank;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SLeftGame] = Packet_LeftGame;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.STradeInvite] = Trade.Packet_TradeInvite;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.STrade] = Trade.Packet_Trade;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SCloseTrade] = Trade.Packet_CloseTrade;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.STradeUpdate] = Trade.Packet_TradeUpdate;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.STradeStatus] = Trade.Packet_TradeStatus;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapReport] = Packet_MapReport;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.STarget] = Packet_Target;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SAdmin] = Packet_Admin;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapNames] = Packet_MapNames;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SCritical] = Packet_Critical;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SrClick] = Packet_RClick;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SHotbar] = Packet_Hotbar;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSpawnEvent] = Event.Packet_SpawnEvent;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SEventMove] = Event.Packet_EventMove;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SEventDir] = Event.Packet_EventDir;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SEventChat] = Event.Packet_EventChat;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SEventStart] = Event.Packet_EventStart;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SEventEnd] = Event.Packet_EventEnd;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlayBGM] = Event.Packet_PlayBGM;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPlaySound] = Event.Packet_PlaySound;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SFadeoutBGM] = Event.Packet_FadeOutBGM;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SStopSound] = Event.Packet_StopSound;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSwitchesAndVariables] = Event.Packet_SwitchesAndVariables;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapEventData] = Event.Packet_MapEventData;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SChatBubble] = Packet_ChatBubble;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSpecialEffect] = Event.Packet_SpecialEffect;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPic] = Event.Packet_Picture;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SHoldPlayer] = Event.Packet_HoldPlayer;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateProjectile] = Projectile.HandleUpdateProjectile;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMapProjectile] = Projectile.HandleMapProjectile;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SEmote] = Packet_Emote;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPartyInvite] = Party.Packet_PartyInvite;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPartyUpdate] = Party.Packet_PartyUpdate;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPartyVitals] = Party.Packet_PartyVitals;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdatePet] = Pet.Packet_UpdatePet;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdatePlayerPet] = Pet.Packet_UpdatePlayerPet;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPetMove] = Pet.Packet_PetMove;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPetDir] = Pet.Packet_PetDir;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPetVital] = Pet.Packet_PetVital;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SClearPetSkillBuffer] = Pet.Packet_ClearPetSkillBuffer;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPetAttack] = Pet.Packet_PetAttack;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPetXY] = Pet.Packet_PetXY;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPetExp] = Pet.Packet_PetExperience;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SClock] = Packet_Clock;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.STime] = Packet_Time;

            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SItemEditor] = Packet_EditItem;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SNPCEditor] = Packet_NPCEditor;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SShopEditor] = Packet_EditShop;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SSkillEditor] = Packet_EditSkill;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SResourceEditor] = Packet_ResourceEditor;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SAnimationEditor] = Packet_EditAnimation;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SProjectileEditor] = HandleProjectileEditor;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SJobEditor] = Packet_JobEditor;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SPetEditor] = Packet_PetEditor;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SUpdateMoral] = Packet_UpdateMoral;
            NetworkConfig.Socket.PacketID[(int)Packets.ServerPackets.SMoralEditor] = Packet_EditMoral;

        }

        private static void Packet_AlertMsg(ref byte[] data)
        {
            byte dialogueIndex;
            int menuReset;
            int kick;
            var buffer = new ByteStream(data);

            dialogueIndex = buffer.ReadByte();
            menuReset = buffer.ReadInt32();
            kick = buffer.ReadInt32();

            if (menuReset > 0)
            {
                Gui.HideWindows();

                switch (menuReset)
                {
                    case (int)Core.Enum.MenuType.Login:
                        {
                            Gui.ShowWindow(Gui.GetWindowIndex("winLogin"));
                            break;
                        }
                    case (int)Core.Enum.MenuType.Chars:
                        {
                            Gui.ShowWindow(Gui.GetWindowIndex("winChars"));
                            break;
                        }
                    case (int)Core.Enum.MenuType.Job:
                        {
                            Gui.ShowWindow(Gui.GetWindowIndex("winJobs"));
                            break;
                        }
                    case (int)Core.Enum.MenuType.NewChar:
                        {
                            Gui.ShowWindow(Gui.GetWindowIndex("winNewChar"));
                            break;
                        }
                    case (int)Core.Enum.MenuType.Main:
                        {
                            Gui.ShowWindow(Gui.GetWindowIndex("winLogin"));
                            break;
                        }
                    case (int)Core.Enum.MenuType.Register:
                        {
                            Gui.ShowWindow(Gui.GetWindowIndex("winRegister"));
                            break;
                        }
                }
            }
            else if (kick > 0 | GameState.InGame == true)
            {
                Gui.ShowWindow(Gui.GetWindowIndex("winLogin"));
                NetworkConfig.InitNetwork();
                GameLogic.DialogueAlert(dialogueIndex);
                return;
            }

            GameLogic.DialogueAlert(dialogueIndex);
            buffer.Dispose();
        }

        private static void Packet_KeyPair(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            GameState.EKeyPair.ImportKeyString(buffer.ReadString());
            buffer.Dispose();
        }

        private static void Packet_LoginOk(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            // Now we can receive game data
            GameState.MyIndex = buffer.ReadInt32();

            buffer.Dispose();
        }

        public static void Packet_PlayerChars(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            long I;
            long winNum;
            long conNum;
            var isSlotEmpty = new bool[Constant.MAX_CHARS];
            long x;

            Settings.Instance.Username = Gui.Windows[Gui.GetWindowIndex("winLogin")].Controls[(int)Gui.GetControlIndex("winLogin", "txtUsername")].Text;
            Settings.Save();

            for (var i = 0L; i < Constant.MAX_CHARS; i++)
            {
                GameState.CharName[(int)i] = buffer.ReadString();
                GameState.CharSprite[(int)i] = buffer.ReadInt32();
                GameState.CharAccess[(int)i] = buffer.ReadInt32();
                GameState.CharJob[(int)i] = buffer.ReadInt32();

                // set as empty or not
                if (!(Strings.Len(GameState.CharName[(int)i]) > 0))
                    isSlotEmpty[(int)i] = Conversions.ToBoolean(1);
            }

            buffer.Dispose();

            Gui.HideWindows();
            Gui.ShowWindow(Gui.GetWindowIndex("winChars"));

            // set GUi window up
            winNum = Gui.GetWindowIndex("winChars");
            for (var i = 0L; i < Constant.MAX_CHARS; i++)
            {
                conNum = Gui.GetControlIndex("winChars", "lblCharName_" + (i + 1));
                {
                    var withBlock = Gui.Windows[winNum].Controls[(int)conNum];
                    if (!isSlotEmpty[(int)i])
                    {
                        withBlock.Text = GameState.CharName[(int)i];
                    }
                    else
                    {
                        withBlock.Text = "Blank Slot";
                    }
                }

                // hide/show buttons
                if (isSlotEmpty[(int)i])
                {
                    // create button
                    conNum = Gui.GetControlIndex("winChars", "btnCreateChar_" + (i + 1));
                    Gui.Windows[winNum].Controls[(int)conNum].Visible = true;
                    // select button
                    conNum = Gui.GetControlIndex("winChars", "btnSelectChar_" + (i + 1));
                    Gui.Windows[winNum].Controls[(int)conNum].Visible = false;
                    // delete button
                    conNum = Gui.GetControlIndex("winChars", "btnDelChar_" + (i + 1));
                    Gui.Windows[winNum].Controls[(int)conNum].Visible = false;
                }
                else
                {
                    // create button
                    conNum = Gui.GetControlIndex("winChars", "btnCreateChar_" + (i + 1));
                    Gui.Windows[winNum].Controls[(int)conNum].Visible = false;
                    // select button
                    conNum = Gui.GetControlIndex("winChars", "btnSelectChar_" + (i + 1));
                    Gui.Windows[winNum].Controls[(int)conNum].Visible = true;
                    // delete button
                    conNum = Gui.GetControlIndex("winChars", "btnDelChar_" + (i + 1));
                    Gui.Windows[winNum].Controls[(int)conNum].Visible = true;
                }
            }
        }

        public static void Packet_UpdateJob(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();
            buffer.WriteInt32(i);

            {
                ref var withBlock = ref Core.Type.Job[i];
                withBlock.Name = buffer.ReadString();
                withBlock.Desc = buffer.ReadString();

                withBlock.MaleSprite = buffer.ReadInt32();
                withBlock.FemaleSprite = buffer.ReadInt32();

                for (int q = 0; q < (int)Core.Enum.StatType.Count; q++)
                    withBlock.Stat[q] = buffer.ReadInt32();

                for (int q = 0; q <= 4; q++)
                {
                    withBlock.StartItem[q] = buffer.ReadInt32();
                    withBlock.StartValue[q] = buffer.ReadInt32();
                }

                withBlock.StartMap = buffer.ReadInt32();
                withBlock.StartX = buffer.ReadByte();
                withBlock.StartY = buffer.ReadByte();
                withBlock.BaseExp = buffer.ReadInt32();
            }

            buffer.Dispose();
        }

        public static void Packet_JobData(ref byte[] data)
        {
            int i;
            int x;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_JOBS; i++)
            {          
                ref var withBlock = ref Core.Type.Job[i];
                withBlock.Name = buffer.ReadString();
                withBlock.Desc = buffer.ReadString();

                withBlock.MaleSprite = buffer.ReadInt32();
                withBlock.FemaleSprite = buffer.ReadInt32();

                for (x = 0; x < (int)Core.Enum.StatType.Count; x++)
                    withBlock.Stat[x] = buffer.ReadInt32();

                for (int q = 0; q <= 4; q++)
                {
                    withBlock.StartItem[q] = buffer.ReadInt32();
                    withBlock.StartValue[q] = buffer.ReadInt32();
                }

                withBlock.StartMap = buffer.ReadInt32();
                withBlock.StartX = buffer.ReadByte();
                withBlock.StartY = buffer.ReadByte();
                withBlock.BaseExp = buffer.ReadInt32();
            }

            

            buffer.Dispose();
        }

        private static void Packet_InGame(ref byte[] data)
        {
            GameState.InMenu = false;
            GameState.InGame = true;
            Gui.HideWindows();
            GameState.CanMoveNow = true;
            GameState.MyEditorType = -1;
            GameState.SkillBuffer = -1;
            GameState.InShop = -1;

            // show gui
            Gui.ShowWindow(Gui.GetWindowIndex("winHotbar"), resetPosition: false);
            Gui.ShowWindow(Gui.GetWindowIndex("winMenu"), resetPosition: false);
            Gui.ShowWindow(Gui.GetWindowIndex("winBars"), resetPosition: false);
            Gui.HideChat();

            General.GameInit();
        }

        private static void Packet_PlayerInv(ref byte[] data)
        {
            int i;
            int itemNum;
            int amount;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_INV; i++)
            {
                itemNum = buffer.ReadInt32();
                amount = buffer.ReadInt32();
                SetPlayerInv(GameState.MyIndex, i, itemNum);
                SetPlayerInvValue(GameState.MyIndex, i, amount);
            }

            GameLogic.SetGoldLabel();

            buffer.Dispose();
        }

        private static void Packet_PlayerInvUpdate(ref byte[] data)
        {
            int n;
            int i;
            var buffer = new ByteStream(data);

            n = buffer.ReadInt32();

            SetPlayerInv(GameState.MyIndex, n, buffer.ReadInt32());
            SetPlayerInvValue(GameState.MyIndex, n, buffer.ReadInt32());

            GameLogic.SetGoldLabel();

            buffer.Dispose();
        }

        private static void Packet_PlayerWornEquipment(ref byte[] data)
        {
            int i;
            int n;
            var buffer = new ByteStream(data);

            for (i = 0; i < (int)Core.Enum.EquipmentType.Count; i++)
            {
                n = buffer.ReadInt32();
                SetPlayerEquipment(GameState.MyIndex, n, (Core.Enum.EquipmentType)i);
            }

            buffer.Dispose();
        }

        private static void Packet_NPCMove(ref byte[] data)
        {
            double MapNPCNum;
            int movement;
            int x;
            int y;
            int dir;
            var buffer = new ByteStream(data);

            MapNPCNum = buffer.ReadInt32();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();
            dir = buffer.ReadInt32();
            movement = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.MyMapNPC[(int)MapNPCNum];
                withBlock.X = (byte)x;
                withBlock.Y = (byte)y;
                withBlock.Dir = dir;
                withBlock.XOffset = 0;
                withBlock.YOffset = 0;
                withBlock.Moving = (byte)movement;

                switch (withBlock.Dir)
                {
                    case (int)Core.Enum.DirectionType.Up:
                        {
                            withBlock.YOffset = GameState.PicY;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Down:
                        {
                            withBlock.YOffset = GameState.PicY * -1;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Left:
                        {
                            withBlock.XOffset = GameState.PicX;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Right:
                        {
                            withBlock.XOffset = GameState.PicX * -1;
                            break;
                        }
                }
            }

            buffer.Dispose();
        }

        private static void Packet_NPCDir(ref byte[] data)
        {
            int dir;
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();
            dir = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.MyMapNPC[i];
                withBlock.Dir = dir;
                withBlock.XOffset = 0;
                withBlock.YOffset = 0;
                withBlock.Moving = 0;
            }

            buffer.Dispose();
        }

        private static void Packet_Attack(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();

            // Set player to attacking
            Core.Type.Player[i].Attacking = 1;
            Core.Type.Player[i].AttackTimer = General.GetTickCount();

            buffer.Dispose();
        }

        private static void Packet_NPCAttack(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();

            // Set NPC to attacking
            Core.Type.MyMapNPC[i].Attacking = 1;
            Core.Type.MyMapNPC[i].AttackTimer = General.GetTickCount();

            buffer.Dispose();
        }

        private static void Packet_GlobalMsg(ref byte[] data)
        {
            string msg;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            buffer.Dispose();

            Text.AddText(msg, (int)Core.Enum.ColorType.Yellow, channel: (byte)Core.Enum.ChatChannel.Broadcast);
        }

        private static void Packet_MapMsg(ref byte[] data)
        {
            string msg;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            buffer.Dispose();

            Text.AddText(msg, (int)Core.Enum.ColorType.White, channel: (byte)Core.Enum.ChatChannel.Map);

        }

        private static void Packet_AdminMsg(ref byte[] data)
        {
            string msg;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();

            buffer.Dispose();

            Text.AddText(msg, (int)Core.Enum.ColorType.BrightCyan, channel: (byte)Core.Enum.ChatChannel.Broadcast);
        }

        private static void Packet_PlayerMsg(ref byte[] data)
        {
            string msg;
            int color;
            var buffer = new ByteStream(data);

            msg = buffer.ReadString();
            color = buffer.ReadInt32();

            buffer.Dispose();

            Text.AddText(msg, color, channel: (byte)Core.Enum.ChatChannel.Player);
        }

        private static void Packet_SpawnItem(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.MyMapItem[i];
                withBlock.Num = buffer.ReadInt32();
                withBlock.Value = buffer.ReadInt32();
                withBlock.X = (byte)buffer.ReadInt32();
                withBlock.Y = (byte)buffer.ReadInt32();
            }

            buffer.Dispose();
        }

        private static void Packet_SpawnNPC(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.MyMapNPC[i];
                withBlock.Num = buffer.ReadInt32();
                withBlock.X = (byte)buffer.ReadInt32();
                withBlock.Y = (byte)buffer.ReadInt32();
                withBlock.Dir = buffer.ReadInt32();

                for (i = 0; i < (int)Core.Enum.VitalType.Count; i++)
                    withBlock.Vital[i] = buffer.ReadInt32();
                // Client use only
                withBlock.XOffset = 0;
                withBlock.YOffset = 0;
                withBlock.Moving = 0;
            }

            buffer.Dispose();
        }

        private static void Packet_NPCDead(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();
            Map.ClearMapNPC(i);

            buffer.Dispose();
        }

        private static void Packet_UpdateNPC(ref byte[] data)
        {
            int i;
            int x;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();

            // Update the NPC
            Core.Type.NPC[i].Animation = buffer.ReadInt32();
            Core.Type.NPC[i].AttackSay = buffer.ReadString();
            Core.Type.NPC[i].Behaviour = buffer.ReadByte();

            for (x = 0; x < Constant.MAX_DROP_ITEMS; x++)
            {
                Core.Type.NPC[i].DropChance[x] = buffer.ReadInt32();
                Core.Type.NPC[i].DropItem[x] = buffer.ReadInt32();
                Core.Type.NPC[i].DropItemValue[x] = buffer.ReadInt32();
            }

            Core.Type.NPC[i].Exp = buffer.ReadInt32();
            Core.Type.NPC[i].Faction = buffer.ReadByte();
            Core.Type.NPC[i].HP = buffer.ReadInt32();
            Core.Type.NPC[i].Name = buffer.ReadString();
            Core.Type.NPC[i].Range = buffer.ReadByte();
            Core.Type.NPC[i].SpawnTime = buffer.ReadByte();
            Core.Type.NPC[i].SpawnSecs = buffer.ReadInt32();
            Core.Type.NPC[i].Sprite = buffer.ReadInt32();

            for (x = 0; x < (int)Core.Enum.StatType.Count; x++)
                Core.Type.NPC[i].Stat[x] = buffer.ReadByte();

            for (x = 0; x < Constant.MAX_NPC_SKILLS; x++)
                Core.Type.NPC[i].Skill[x] = buffer.ReadByte();

            Core.Type.NPC[i].Level = buffer.ReadInt32();
            Core.Type.NPC[i].Damage = buffer.ReadInt32();

            buffer.Dispose();
        }

        private static void Packet_UpdateSkill(ref byte[] data)
        {
            int skillNum;
            var buffer = new ByteStream(data);
            skillNum = buffer.ReadInt32();

            Core.Type.Skill[skillNum].AccessReq = buffer.ReadInt32();
            Core.Type.Skill[skillNum].AoE = buffer.ReadInt32();
            Core.Type.Skill[skillNum].CastAnim = buffer.ReadInt32();
            Core.Type.Skill[skillNum].CastTime = buffer.ReadInt32();
            Core.Type.Skill[skillNum].CdTime = buffer.ReadInt32();
            Core.Type.Skill[skillNum].JobReq = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Dir = (byte)buffer.ReadInt32();
            Core.Type.Skill[skillNum].Duration = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Icon = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Interval = buffer.ReadInt32();
            Core.Type.Skill[skillNum].IsAoE = Conversions.ToBoolean(buffer.ReadInt32());
            Core.Type.Skill[skillNum].LevelReq = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Map = buffer.ReadInt32();
            Core.Type.Skill[skillNum].MpCost = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Name = buffer.ReadString();
            Core.Type.Skill[skillNum].Range = buffer.ReadInt32();
            Core.Type.Skill[skillNum].SkillAnim = buffer.ReadInt32();
            Core.Type.Skill[skillNum].StunDuration = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Type = (byte)buffer.ReadInt32();
            Core.Type.Skill[skillNum].Vital = buffer.ReadInt32();
            Core.Type.Skill[skillNum].X = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Y = buffer.ReadInt32();

            Core.Type.Skill[skillNum].IsProjectile = buffer.ReadInt32();
            Core.Type.Skill[skillNum].Projectile = buffer.ReadInt32();

            Core.Type.Skill[skillNum].KnockBack = (byte)buffer.ReadInt32();
            Core.Type.Skill[skillNum].KnockBackTiles = (byte)buffer.ReadInt32();

            buffer.Dispose();

        }

        private static void Packet_Skills(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_PLAYER_SKILLS; i++)
                Core.Type.Player[GameState.MyIndex].Skill[i].Num = buffer.ReadInt32();

            buffer.Dispose();
        }

        private static void Packet_LeftMap(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            Player.ClearPlayer(buffer.ReadInt32());

            buffer.Dispose();
        }

        private static void Packet_Ping(ref byte[] data)
        {
            GameState.PingEnd = General.GetTickCount();
            GameState.Ping = GameState.PingEnd - GameState.PingStart;
        }

        private static void Packet_ActionMessage(ref byte[] data)
        {
            int x;
            int y;
            string message;
            int color;
            int tmpType;
            var buffer = new ByteStream(data);

            message = buffer.ReadString();
            color = buffer.ReadInt32();
            tmpType = buffer.ReadInt32();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();

            buffer.Dispose();

            GameLogic.CreateActionMsg(message, color, (byte)tmpType, x, y);
        }

        private static void Packet_Blood(ref byte[] data)
        {
            int x;
            int y;
            int sprite;
            var buffer = new ByteStream(data);

            x = buffer.ReadInt32();
            y = buffer.ReadInt32();

            // randomise sprite
            sprite = GameLogic.Rand(1, 3);

            GameState.BloodIndex = (byte)(GameState.BloodIndex + 1);
            if (GameState.BloodIndex >= byte.MaxValue)
                GameState.BloodIndex = 1;

            {
                ref var withBlock = ref Core.Type.Blood[GameState.BloodIndex];
                withBlock.X = x;
                withBlock.Y = y;
                withBlock.Sprite = sprite;
                withBlock.Timer = General.GetTickCount();
            }

            buffer.Dispose();
        }
        private static void Packet_NPCVitals(ref byte[] data)
        {
            double MapNPCNum;
            var buffer = new ByteStream(data);

            MapNPCNum = buffer.ReadInt32();
            for (int i = 0; i < (int)Core.Enum.VitalType.Count; i++)
                Core.Type.MyMapNPC[(int)MapNPCNum].Vital[i] = buffer.ReadInt32();

            buffer.Dispose();
        }

        private static void Packet_Cooldown(ref byte[] data)
        {
            int slot;
            var buffer = new ByteStream(data);

            slot = buffer.ReadInt32();
            Core.Type.Player[GameState.MyIndex].Skill[slot].CD = General.GetTickCount();

            buffer.Dispose();
        }

        private static void Packet_ClearSkillBuffer(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            GameState.SkillBuffer = -1;
            GameState.SkillBufferTimer = 0;

            buffer.Dispose();
        }

        private static void Packet_SayMessage(ref byte[] data)
        {
            int access;
            string name;
            string message;
            string header;
            int pk;
            byte channelType;
            byte color;
            var buffer = new ByteStream(data);

            name = buffer.ReadString();
            access = buffer.ReadInt32();
            pk = buffer.ReadInt32();
            message = buffer.ReadString();
            header = buffer.ReadString();

            // Check access level
            switch (access)
            {
                case (int)Core.Enum.AccessType.Player:
                    {
                        color = (byte)Core.Enum.ColorType.White;
                        break;
                    }
                case (int)Core.Enum.AccessType.Moderator:
                    {
                        color = (byte)Core.Enum.ColorType.Cyan;
                        break;
                    }
                case (int)Core.Enum.AccessType.Mapper:
                    {
                        color = (byte)Core.Enum.ColorType.Green;
                        break;
                    }
                case (int)Core.Enum.AccessType.Developer:
                    {
                        color = (byte)Core.Enum.ColorType.BrightBlue;
                        break;
                    }
                case (int)Core.Enum.AccessType.Owner:
                    {
                        color = (byte)Core.Enum.ColorType.Yellow;
                        break;
                    }

                default:
                    {
                        color = (byte)Core.Enum.ColorType.White;
                        break;
                    }
            }

            if (pk > 0)
                color = (byte)Core.Enum.ColorType.BrightRed;

            // find channel
            channelType = 0;
            switch (header ?? "")
            {
                case "[Map]":
                    {
                        channelType = (byte)Core.Enum.ChatChannel.Map;
                        break;
                    }
                case "[Global]":
                    {
                        channelType = (byte)Core.Enum.ChatChannel.Broadcast;
                        break;
                    }
            }

            // add to the chat box
            Text.AddText(header + " " + name + ": " + message, color, channel: channelType);

            buffer.Dispose();
        }

        private static void Packet_Stunned(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            GameState.StunDuration = buffer.ReadInt32();

            buffer.Dispose();
        }

        private static void Packet_MapWornEquipment(ref byte[] data)
        {
            int playernum;
            int n;
            var buffer = new ByteStream(data);

            playernum = buffer.ReadInt32();
            for (int i = 0; i < (int)Core.Enum.EquipmentType.Count; i++)
            {
                n = buffer.ReadInt32();
                SetPlayerEquipment(playernum, n, (Core.Enum.EquipmentType)i);
            }

            buffer.Dispose();
        }

        private static void Packet_Target(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            GameState.MyTarget = buffer.ReadInt32();
            GameState.MyTargetType = buffer.ReadInt32();

            buffer.Dispose();
        }

        private static void Packet_MapReport(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_MAPS; i++)
                GameState.MapNames[i] = buffer.ReadString();

            GameState.InitMapReport = true;

            buffer.Dispose();
        }

        private static void Packet_Admin(ref byte[] data)
        {
            GameState.InitAdminForm = true;
        }

        private static void Packet_MapNames(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);
            for (i = 0; i < Constant.MAX_MAPS; i++)
                GameState.MapNames[i] = buffer.ReadString();

            buffer.Dispose();
        }

        private static void Packet_Critical(ref byte[] data)
        {
            GameState.ShakeTimerEnabled = Conversions.ToBoolean(1);
            GameState.ShakeTimer = General.GetTickCount();
        }

        private static void Packet_RClick(ref byte[] data)
        {

        }

        private static void Packet_Emote(ref byte[] data)
        {
            int index;
            int emote;
            var buffer = new ByteStream(data);
            index = buffer.ReadInt32();
            emote = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.Player[index];
                withBlock.Emote = emote;
                withBlock.EmoteTimer = General.GetTickCount() + 5000;
            }

            buffer.Dispose();

        }

        private static void Packet_ChatBubble(ref byte[] data)
        {
            int targetType;
            int target;
            string message;
            int Color;
            var buffer = new ByteStream(data);

            target = buffer.ReadInt32();
            targetType = buffer.ReadInt32();
            message = buffer.ReadString();
            Color = buffer.ReadInt32();
            GameLogic.AddChatBubble(target, (byte)targetType, message, Color);

            buffer.Dispose();

        }

        private static void Packet_LeftGame(ref byte[] data)
        {
            GameLogic.LogoutGame();
        }

        // *****************
        // ***  EDITORS  ***
        // *****************
        private static void Packet_EditAnimation(ref byte[] data)
        {
            GameState.InitAnimationEditor = true;
        }

        private static void Packet_JobEditor(ref byte[] data)
        {
            GameState.InitJobEditor = true;
        }

        public static void Packet_EditItem(ref byte[] data)
        {
            GameState.InitItemEditor = true;
        }

        private static void Packet_NPCEditor(ref byte[] data)
        {
            GameState.InitNPCEditor = true;
        }

        private static void Packet_ResourceEditor(ref byte[] data)
        {
            GameState.InitResourceEditor = true;
        }

        internal static void Packet_PetEditor(ref byte[] data)
        {
            GameState.InitPetEditor = true;
        }

        internal static void HandleProjectileEditor(ref byte[] data)
        {
            GameState.InitProjectileEditor = true;
        }

        private static void Packet_EditShop(ref byte[] data)
        {
            GameState.InitShopEditor = true;
        }

        private static void Packet_EditSkill(ref byte[] data)
        {
            GameState.InitSkillEditor = true;
        }

        private static void Packet_Clock(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            Clock.Instance.GameSpeed = buffer.ReadInt32();
            Clock.Instance.Time = new DateTime(BitConverter.ToInt64(buffer.ReadBytes(), 0));

            buffer.Dispose();
        }

        private static void Packet_Time(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            Clock.Instance.TimeOfDay = (TimeOfDay)buffer.ReadByte();

            switch (Clock.Instance.TimeOfDay)
            {
                case TimeOfDay.Dawn:
                    {
                        Text.AddText("A chilling, refreshing, breeze has come with the morning.", (int)Core.Enum.ColorType.DarkGray);
                        break;
                    }

                case TimeOfDay.Day:
                    {
                        Text.AddText("Day has dawned in this region.", (int)Core.Enum.ColorType.DarkGray);
                        break;
                    }

                case TimeOfDay.Dusk:
                    {
                        Text.AddText("Dusk has begun darkening the skies...", (int)Core.Enum.ColorType.DarkGray);
                        break;
                    }

                default:
                    {
                        Text.AddText("Night has fallen upon the weary travelers.", (int)Core.Enum.ColorType.DarkGray);
                        break;
                    }
            }

            buffer.Dispose();
        }

        internal static void Packet_Hotbar(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_HOTBAR; i++)
            {
                Core.Type.Player[GameState.MyIndex].Hotbar[i].Slot = buffer.ReadInt32();
                Core.Type.Player[GameState.MyIndex].Hotbar[i].SlotType = (byte)buffer.ReadInt32();
            }

            buffer.Dispose();
        }

        public static void Packet_EditMoral(ref byte[] data)
        {
            GameState.InitMoralEditor = true;
        }

        public static void Packet_UpdateMoral(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            i = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.Moral[i];
                withBlock.Name = buffer.ReadString();
                withBlock.Color = buffer.ReadByte();
                withBlock.NPCBlock = buffer.ReadBoolean();
                withBlock.PlayerBlock = buffer.ReadBoolean();
                withBlock.DropItems = buffer.ReadBoolean();
                withBlock.CanCast = buffer.ReadBoolean();
                withBlock.CanDropItem = buffer.ReadBoolean();
                withBlock.CanPickupItem = buffer.ReadBoolean();
                withBlock.CanPK = buffer.ReadBoolean();
                withBlock.DropItems = buffer.ReadBoolean();
                withBlock.LoseExp = buffer.ReadBoolean();
            }

            buffer.Dispose();
        }

    }
}