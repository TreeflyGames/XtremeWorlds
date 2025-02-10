using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Global.Command;

namespace Client
{

    static class GameLogic
    {
        public static void ProcessNPCMovement(double MapNPCNum)
        {
            // Check if NPC is walking, and if so process moving them over
            if (Core.Type.MyMapNPC[(int)MapNPCNum].Moving == (byte)Core.Enum.MovementType.Walking)
            {

                switch (Core.Type.MyMapNPC[(int)MapNPCNum].Dir)
                {
                    case (int)Core.Enum.DirectionType.Up:
                        {
                            Core.Type.MyMapNPC[(int)MapNPCNum].YOffset = (int)Math.Round(Core.Type.MyMapNPC[(int)MapNPCNum].YOffset - GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeY));
                            if (Core.Type.MyMapNPC[(int)MapNPCNum].YOffset < 0)
                                Core.Type.MyMapNPC[(int)MapNPCNum].YOffset = 0;
                            break;
                        }

                    case (int)Core.Enum.DirectionType.Down:
                        {
                            Core.Type.MyMapNPC[(int)MapNPCNum].YOffset = (int)Math.Round(Core.Type.MyMapNPC[(int)MapNPCNum].YOffset + GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeY));
                            if (Core.Type.MyMapNPC[(int)MapNPCNum].YOffset > 0)
                                Core.Type.MyMapNPC[(int)MapNPCNum].YOffset = 0;
                            break;
                        }

                    case (int)Core.Enum.DirectionType.Left:
                        {
                            Core.Type.MyMapNPC[(int)MapNPCNum].XOffset = (int)Math.Round(Core.Type.MyMapNPC[(int)MapNPCNum].XOffset - GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeX));
                            if (Core.Type.MyMapNPC[(int)MapNPCNum].XOffset < 0)
                                Core.Type.MyMapNPC[(int)MapNPCNum].XOffset = 0;
                            break;
                        }

                    case (int)Core.Enum.DirectionType.Right:
                        {
                            Core.Type.MyMapNPC[(int)MapNPCNum].XOffset = (int)Math.Round(Core.Type.MyMapNPC[(int)MapNPCNum].XOffset + GameState.ElapsedTime / 1000d * (GameState.WalkSpeed * GameState.SizeX));
                            if (Core.Type.MyMapNPC[(int)MapNPCNum].XOffset > 0)
                                Core.Type.MyMapNPC[(int)MapNPCNum].XOffset = 0;
                            break;
                        }
                }

                // Check if completed walking over to the next tile
                if (Core.Type.MyMapNPC[(int)MapNPCNum].Moving > 0)
                {
                    if (Core.Type.MyMapNPC[(int)MapNPCNum].Dir == (int)Core.Enum.DirectionType.Right | Core.Type.MyMapNPC[(int)MapNPCNum].Dir == (int)Core.Enum.DirectionType.Down)
                    {
                        if (Core.Type.MyMapNPC[(int)MapNPCNum].XOffset >= 0 & Core.Type.MyMapNPC[(int)MapNPCNum].YOffset >= 0)
                        {
                            Core.Type.MyMapNPC[(int)MapNPCNum].Moving = 0;
                            if (Core.Type.MyMapNPC[(int)MapNPCNum].Steps == 1)
                            {
                                Core.Type.MyMapNPC[(int)MapNPCNum].Steps = 3;
                            }
                            else
                            {
                                Core.Type.MyMapNPC[(int)MapNPCNum].Steps = 1;
                            }
                        }
                    }
                    else if (Core.Type.MyMapNPC[(int)MapNPCNum].XOffset <= 0 & Core.Type.MyMapNPC[(int)MapNPCNum].YOffset <= 0)
                    {
                        Core.Type.MyMapNPC[(int)MapNPCNum].Moving = 0;
                        if (Core.Type.MyMapNPC[(int)MapNPCNum].Steps == 1)
                        {
                            Core.Type.MyMapNPC[(int)MapNPCNum].Steps = 3;
                        }
                        else
                        {
                            Core.Type.MyMapNPC[(int)MapNPCNum].Steps = 1;
                        }
                    }
                }
            }
        }

        internal static bool IsInBounds()
        {
            bool IsInBoundsRet = false;

            if (GameState.CurX >= 0 & GameState.CurX <= Core.Type.MyMap.MaxX)
            {
                if (GameState.CurY >= 0 & GameState.CurY <= Core.Type.MyMap.MaxY)
                {
                    IsInBoundsRet = true;
                }
            }

            return IsInBoundsRet;

        }

        public static bool GameStarted()
        {
            bool GameStartedRet = default;
            GameStartedRet = false;
            if (GameState.InGame == false)
                return GameStartedRet;
            if (GameState.MapData == false)
                return GameStartedRet;
            if (GameState.PlayerData == false)
                return GameStartedRet;
            GameStartedRet = true;
            return GameStartedRet;
        }

        internal static void CreateActionMsg(string message, int color, byte msgType, int x, int y)
        {

            GameState.ActionMsgIndex = (byte)(GameState.ActionMsgIndex + 1);
            if (GameState.ActionMsgIndex >= byte.MaxValue)
                GameState.ActionMsgIndex = 1;

            {
                ref var withBlock = ref Core.Type.ActionMsg[GameState.ActionMsgIndex];
                withBlock.Message = message;
                withBlock.Color = color;
                withBlock.Type = msgType;
                withBlock.Created = General.GetTickCount();
                withBlock.Scroll = 0;
                withBlock.X = x;
                withBlock.Y = y;
            }

            if (Core.Type.ActionMsg[GameState.ActionMsgIndex].Type == (int)Core.Enum.ActionMsgType.Scroll)
            {
                Core.Type.ActionMsg[GameState.ActionMsgIndex].Y = Core.Type.ActionMsg[GameState.ActionMsgIndex].Y + Rand(-2, 6);
                Core.Type.ActionMsg[GameState.ActionMsgIndex].X = Core.Type.ActionMsg[GameState.ActionMsgIndex].X + Rand(-8, 8);
            }

        }

        internal static int Rand(int maxNumber, int minNumber = 0)
        {
            if (minNumber > maxNumber)
            {
                int t = minNumber;
                minNumber = maxNumber;
                maxNumber = t;
            }

            return (int)Math.Round(General.Random.NextDouble(minNumber, maxNumber));
        }

        // BitWise Operators for directional blocking
        internal static void SetDirBlock(ref byte blockvar, ref byte dir, bool block)
        {
            if (block)
            {
                blockvar = (byte)(blockvar | (long)Math.Round(Math.Pow(2d, dir)));
            }
            else
            {
                blockvar = (byte)(blockvar & ~(byte)Math.Pow(2d, dir));
            }
        }

        internal static bool IsDirBlocked(ref byte blockvar, ref byte dir)
        {
            return Conversions.ToBoolean(blockvar & (long)Math.Round(Math.Pow(2d, dir)));
        }

        internal static string ConvertCurrency(int amount)
        {
            string ConvertCurrencyRet = default;

            if (Conversion.Int(amount) < 10000)
            {
                ConvertCurrencyRet = amount.ToString();
            }
            else if (Conversion.Int(amount) < 999999)
            {
                ConvertCurrencyRet = Conversion.Int(amount / 1000d) + "k";
            }
            else if (Conversion.Int(amount) < 999999999)
            {
                ConvertCurrencyRet = Conversion.Int(amount / 1000000d) + "m";
            }
            else
            {
                ConvertCurrencyRet = Conversion.Int(amount / 1000000000d) + "b";
            }

            return ConvertCurrencyRet;

        }

        public static void HandlePressEnter()
        {
            var chatText = default(string);
            string name;
            int i;
            int n;
            string[] command;
            ByteStream buffer;

            if (GameState.InGame)
            {
                chatText = Gui.Windows[Gui.GetWindowIndex("winChat")].Controls[(int)Gui.GetControlIndex("winChat", "txtChat")].Text;
            }

            chatText = chatText.Replace("\0", string.Empty);

            // hide/show chat window
            if (string.IsNullOrEmpty(chatText))
            {
                if (Gui.Windows[Gui.GetWindowIndex("winChat")].Visible == true)
                {
                    Gui.Windows[Gui.GetWindowIndex("winChat")].Controls[(int)Gui.GetControlIndex("winChat", "txtChat")].Text = "";
                    Gui.HideChat();
                    return;
                }
            }

            // Admin message
            if (Strings.Left(chatText, 1) == "@")
            {
                chatText = Strings.Mid(chatText, 2, Strings.Len(chatText) - 1);

                if (Strings.Len(chatText) > 0)
                {
                    NetworkSend.AdminMsg(chatText);
                }

                Gui.Windows[Gui.GetWindowIndex("winChat")].Controls[(int)Gui.GetControlIndex("winChat", "txtChat")].Text = "";
                return;
            }

            // Broadcast message
            if (Strings.Left(chatText, 1) == "'")
            {
                chatText = Strings.Mid(chatText, 2, Strings.Len(chatText) - 1);

                if (Strings.Len(chatText) > 0)
                {
                    NetworkSend.BroadcastMsg(chatText);
                }

                Gui.Windows[Gui.GetWindowIndex("winChat")].Controls[(int)Gui.GetControlIndex("winChat", "txtChat")].Text = "";
                return;
            }

            // party message
            if (Strings.Left(chatText, 1) == "-")
            {
                chatText = Strings.Mid(chatText, 2, Strings.Len(chatText) - 1);

                if (Strings.Len(chatText) > 0)
                {
                    Party.SendPartyChatMsg(chatText);
                }

                Gui.Windows[Gui.GetWindowIndex("winChat")].Controls[(int)Gui.GetControlIndex("winChat", "txtChat")].Text = "";
                return;
            }

            // Player message
            if (Strings.Left(chatText, 1) == "!")
            {
                chatText = Strings.Mid(chatText, 2, Strings.Len(chatText) - 1);
                name = "";

                // Get the desired player from the user text
                var loopTo = Strings.Len(chatText);
                for (i = 0; i < loopTo; i++)
                {

                    if ((Strings.Mid(chatText, i, 1) ?? "") != (Strings.Space(1) ?? ""))
                    {
                        name = name + Strings.Mid(chatText, i, 1);
                    }
                    else
                    {
                        break;
                    }

                }

                chatText = Strings.Mid(chatText, i, Strings.Len(chatText) - 1);

                // Make sure they are actually sending something
                if (Strings.Len(chatText) > 0)
                {
                    // Send the message to the player
                    NetworkSend.PlayerMsg(chatText, name);
                }
                else
                {
                    Text.AddText(Languages.Language.Chat.PlayerMsg, (int)Core.Enum.ColorType.Yellow);
                }

                goto Continue1;
            }

            if (Strings.Left(chatText, 1) == "/")
            {
                command = Strings.Split(chatText, Strings.Space(1));

                switch (command[0] ?? "")
                {
                    case "/emote":
                        {
                            // Checks to make sure we have more than one string in the array
                            if (Information.UBound(command) < 1 || !Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.Emote, (int)Core.Enum.ColorType.Yellow);
                                goto Continue1;
                            }

                            NetworkSend.SendUseEmote(Conversions.ToInteger(command[1]));
                            break;
                        }

                    case "/help":
                        {
                            Text.AddText(Languages.Language.Chat.Help1, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.Help2, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.Help3, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.Help4, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.Help5, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.Help6, (int)Core.Enum.ColorType.Yellow);
                            break;
                        }

                    case "/info":
                        {
                            if (GameState.MyTarget > 0)
                            {
                                if (GameState.MyTargetType == (int)Core.Enum.TargetType.Player)
                                {
                                    NetworkSend.SendPlayerInfo(GetPlayerName(GameState.MyTarget));
                                    goto Continue1;
                                }
                            }

                            // Checks to make sure we have more than one string in the array
                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.Info, (int)Core.Enum.ColorType.Yellow);
                                goto Continue1;
                            }

                            NetworkSend.SendPlayerInfo(command[1]);
                            break;
                        }

                    // Whos Online
                    case "/who":
                        {
                            NetworkSend.SendWhosOnline();
                            break;
                        }

                    // Requets level up
                    case "/levelup":
                        {
                            NetworkSend.SendRequestLevelUp();
                            break;
                        }

                    // Checking fps
                    case "/fps":
                        {
                            GameState.Bfps = !GameState.Bfps;
                            break;
                        }

                    case "/lps":
                        {
                            GameState.Blps = !GameState.Blps;
                            break;
                        }

                    // Request stats
                    case "/stats":
                        {
                            buffer = new ByteStream(4);
                            buffer.WriteInt32((int)Packets.ClientPackets.CGetStats);
                            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
                            buffer.Dispose();
                            break;
                        }

                    case "/party":
                        {
                            if (GameState.MyTarget > 0)
                            {
                                if (GameState.MyTargetType == (int)Core.Enum.TargetType.Player)
                                {
                                    Party.SendPartyRequest(GetPlayerName(GameState.MyTarget));
                                    goto Continue1;
                                }
                            }

                            // Make sure they are actually sending something
                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.Party, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            Party.SendPartyRequest(command[1]);
                            break;
                        }

                    // Join party
                    case "/join":
                        {
                            Party.SendAcceptParty();
                            break;
                        }

                    // Leave party
                    case "/leave":
                        {
                            Party.SendLeaveParty();
                            break;
                        }

                    // Trade
                    case "/trade":
                        {
                            if (GameState.MyTarget > 0)
                            {
                                if (GameState.MyTargetType == (int)Core.Enum.TargetType.Player)
                                {
                                    Trade.SendTradeRequest(GetPlayerName(GameState.MyTarget));
                                    goto Continue1;
                                }
                            }

                            // Make sure they are actually sending something
                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.Trade, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            Trade.SendTradeRequest(command[1]);
                            break;
                        }

                    // // Moderator Admin Commands //
                    // Admin Help
                    case "/admin":
                        {
                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Moderator)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            Text.AddText(Languages.Language.Chat.Admin1, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.Admin2, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.AdminGblMsg, (int)Core.Enum.ColorType.Yellow);
                            Text.AddText(Languages.Language.Chat.AdminPvtMsg, (int)Core.Enum.ColorType.Yellow);
                            break;
                        }

                    case "/acp":
                        {
                            NetworkSend.SendRequestAdmin();
                            break;
                        }

                    // Kicking a player
                    case "/kick":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Moderator)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.Kick, (int)Core.Enum.ColorType.Yellow);
                                goto Continue1;
                            }

                            NetworkSend.SendKick(command[1]);
                            break;
                        }

                    // // Mapper Admin Commands //
                    // Location
                    case "/loc":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            GameState.BLoc = !GameState.BLoc;
                            break;
                        }

                    // Warping to a player
                    case "/warpmeto":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.WarpMeTo, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.WarpMeTo(command[1]);
                            break;
                        }

                    // Warping a player to you
                    case "/warptome":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.WarpToMe, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.WarpToMe(command[1]);
                            break;
                        }

                    // Warping to a map
                    case "/warpto":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || !Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.WarpTo, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            n = Conversions.ToInteger(command[1]);

                            // Check to make sure its a valid map #
                            if (n > 0 & n < Constant.MAX_MAPS)
                            {
                                NetworkSend.WarpTo(n);
                            }
                            else
                            {
                                Text.AddText(Languages.Language.Chat.InvalidMap, (int)Core.Enum.ColorType.BrightRed);
                            }

                            break;
                        }

                    // Setting sprite
                    case "/sprite":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || !Information.IsNumeric(command[1]))
                            {
                                Text.AddText(Languages.Language.Chat.Sprite, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendSetSprite(Conversions.ToInteger(command[1]));
                            break;
                        }

                    // Map report
                    case "/mapreport":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestMapReport();
                            break;
                        }

                    // Respawn request
                    case "/respawn":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            Map.SendMapRespawn();
                            break;
                        }

                    case "/editmap":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Mapper)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            Map.SendRequestEditMap();
                            break;
                        }

                    // // Moderator Commands //
                    // Welcome change
                    case "/welcome":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Moderator)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1)
                            {
                                Text.AddText(Languages.Language.Chat.Welcome, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendMotdChange(Strings.Right(chatText, Strings.Len(chatText) - 5));
                            break;
                        }

                    // Check the ban list
                    case "/banlist":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Moderator)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendBanList();
                            break;
                        }

                    // Banning a player
                    case "/ban":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Moderator)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1)
                            {
                                Text.AddText(Languages.Language.Chat.Ban, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendBan(command[1]);
                            break;
                        }

                    // // Owner Admin Commands //
                    // Giving another player access
                    case "/bandestroy":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Owner)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendBanDestroy();
                            break;
                        }

                    case "/access":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Owner)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            if ((Information.UBound(command) < 2 || Information.IsNumeric(command[1])) | !Information.IsNumeric(command[2]))
                            {
                                Text.AddText(Languages.Language.Chat.Access, (int)Core.Enum.ColorType.Yellow);
                                goto Continue1;
                            }

                            NetworkSend.SendSetAccess(command[1], (byte)Conversions.ToLong(command[2]));
                            break;
                        }

                    // // Developer Admin Commands //
                    case "/editresource":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditResource();
                            break;
                        }

                    case "/editanimation":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditAnimation();
                            break;
                        }

                    case "/editpet":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            Pet.SendRequestEditPet();
                            break;
                        }

                    case "/edititem":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditItem();
                            break;
                        }

                    case "/editprojectile":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            Projectile.SendRequestEditProjectiles();
                            break;
                        }

                    case "/editnpc":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditNPC();
                            break;
                        }

                    case "/editjob":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditJob();
                            break;
                        }

                    case "/editskill":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditSkill();
                            break;
                        }

                    case "/editshop":
                        {
                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditShop();
                            break;
                        }

                    case "/editmoral":
                        {
                            if (GetPlayerAccess(GameState.MyIndex) < (int)Core.Enum.AccessType.Developer)
                            {
                                Text.AddText(Languages.Language.Chat.AccessAlert, (int)Core.Enum.ColorType.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditMoral();
                            break;
                        }

                    case var @case when @case == "":
                        {
                            break;
                        }

                    default:
                        {
                            Text.AddText(Languages.Language.Chat.InvalidCmd, (int)Core.Enum.ColorType.BrightRed);
                            break;
                        }
                }
            }

            else if (Strings.Len(chatText) > 0) // Say message
            {
                NetworkSend.SayMsg(chatText);
            }

        Continue1:
            ;

            Gui.Windows[Gui.GetWindowIndex("winChat")].Controls[(int)Gui.GetControlIndex("winChat", "txtChat")].Text = "";
        }

        public static void CheckMapGetItem()
        {
            var buffer = new ByteStream(4);
            buffer = new ByteStream(4);

            if (General.GetTickCount() > Core.Type.Player[GameState.MyIndex].MapGetTimer + 250)
            {
                Core.Type.Player[GameState.MyIndex].MapGetTimer = General.GetTickCount();
                buffer.WriteInt32((int)Packets.ClientPackets.CMapGetItem);
                NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            }

            buffer.Dispose();
        }

        internal static void ClearActionMsg(byte index)
        {
            Core.Type.ActionMsg[index].Message = "";
            Core.Type.ActionMsg[index].Created = 0;
            Core.Type.ActionMsg[index].Type = 0;
            Core.Type.ActionMsg[index].Color = 0;
            Core.Type.ActionMsg[index].Scroll = 0;
            Core.Type.ActionMsg[index].X = 0;
            Core.Type.ActionMsg[index].Y = 0;
        }

        internal static void UpdateDrawMapName()
        {
            if (Core.Type.MyMap.Moral > 0)
            {
                GameState.DrawMapNameColor = GameClient.QbColorToXnaColor(Core.Type.Moral[Core.Type.MyMap.Moral].Color);
            }
        }

        internal static void AddChatBubble(int target, byte targetType, string msg, int Color)
        {
            int i;
            int index;

            // Set the global index
            GameState.ChatBubbleindex = GameState.ChatBubbleindex + 1;
            if (GameState.ChatBubbleindex < 1 | GameState.ChatBubbleindex > byte.MaxValue)
                GameState.ChatBubbleindex = 1;

            // Default to new bubble
            index = GameState.ChatBubbleindex;

            // Loop through and see if that player/NPC already has a chat bubble
            for (i = 0; i < byte.MaxValue; i++)
            {
                if (Core.Type.ChatBubble[i].TargetType == targetType)
                {
                    if (Core.Type.ChatBubble[i].Target == target)
                    {
                        // Reset master index
                        if (GameState.ChatBubbleindex > 1)
                            GameState.ChatBubbleindex = GameState.ChatBubbleindex - 1;

                        // We use this one now, yes?
                        index = i;
                        break;
                    }
                }
            }

            // Set the bubble up
            {
                ref var withBlock = ref Core.Type.ChatBubble[index];
                withBlock.Target = target;
                withBlock.TargetType = targetType;
                withBlock.Msg = msg;
                withBlock.Color = Color;
                withBlock.Timer = General.GetTickCount();
                withBlock.Active = Conversions.ToBoolean(1);
            }

        }

        public static void DialogueAlert(byte Index)
        {
            var header = default(string);
            var body = default(string);
            var body2 = default(string);

            // find the body/header
            switch (Index)
            {
                case (byte)Core.Enum.DialogueMsg.Connection:
                    {
                        header = "Invalid Connection";
                        body = "You lost connection to the game server.";
                        body2 = "Please try again later.";
                        GameState.InGame = false;
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Banned:
                    {
                        header = "Banned";
                        body = "You have been banned, have a nice day!";
                        body2 = "Please send all ban appeals to an administrator.";
                        GameState.InGame = false;
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Kicked:
                    {
                        header = "Kicked";
                        body = "You have been kicked.";
                        body2 = "Please try and behave.";
                        GameState.InGame = false;
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Outdated:
                    {
                        header = "Wrong Version";
                        body = "Your game client is the wrong version.";
                        body2 = "Please try updating.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Maintenance:
                    {
                        header = "Connection Refused";
                        body = "The server is currently going under maintenance.";
                        body2 = "Please try again soon.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.NameTaken:
                    {
                        header = "Invalid Name";
                        body = "This name is already in use.";
                        body2 = "Please try another name.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.NameLength:
                    {
                        header = "Invalid Name";
                        body = "This name is too short or too long.";
                        body2 = "Please try another name.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.NameIllegal:
                    {
                        header = "Invalid Name";
                        body = "This name contains illegal characters.";
                        body2 = "Please try another name.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Database:
                    {
                        header = "Invalid Connection";
                        body = "Cannot connect to database.";
                        body2 = "Please try again later.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.WrongPass:
                    {
                        header = "Invalid Login";
                        body = "Invalid username or password.";
                        body2 = "Please try again.";
                        Gui.ClearPasswordTexts();
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Activate:
                    {
                        header = "Inactive Account";
                        body = "Your account is not activated.";
                        body2 = "Please activate your account then try again.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.MaxChar:
                    {
                        header = "Cannot Merge";
                        body = "You cannot merge a full account.";
                        body2 = "Please clear a character slot.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.DelChar:
                    {
                        header = "Deleted Character";
                        body = "Your character was successfully deleted.";
                        body2 = "Please log on to continue playing.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.CreateAccount:
                    {
                        header = "Account Created";
                        body = "Your account was successfully created.";
                        body2 = "Now, you can play!";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.MultiAccount:
                    {
                        header = "Multiple Accounts";
                        body = "Multiple accounts are not authorized.";
                        body2 = "Please logout and try again!";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Login:
                    {
                        header = "Cannot Login";
                        body = "This account does not exist.";
                        body2 = "Please try registering the account.";
                        break;
                    }

                case (byte)Core.Enum.DialogueMsg.Crash:
                    {
                        header = "Error";
                        body = "There was a network error.";
                        body2 = "Check logs folder for details.";

                        Gui.HideWindows();
                        Gui.ShowWindow(Gui.GetWindowIndex("winLogin"));
                        break;
                    }
            }

            // set the dialogue up!
            Dialogue(header, body, body2, (byte)Core.Enum.DialogueType.Alert);
        }

        public static void CloseDialogue()
        {
            Gui.HideWindow(Gui.GetWindowIndex("winDialogue"));
        }

        public static void Dialogue(string header, string body, string body2, byte Index, byte style = 0, long Data1 = 0L, long Data2 = 0L, long Data3 = 0L, long Data4 = 0L, long Data5 = 0L)
        {
            if (Gui.Windows[Gui.GetWindowIndex("winDialogue")].Visible == true)
                return;

            // set buttons
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winDialogue")];
                if (style == (int)Core.Enum.DialogueStyle.YesNo)
                {
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnYes")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnNo")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnOkay")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "txtInput")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblBody_2")].Visible = true;
                }
                else if (style == (int)Core.Enum.DialogueStyle.Okay)
                {
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnYes")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnNo")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnOkay")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "txtInput")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblBody_2")].Visible = true;
                }
                else if (style == (int)Core.Enum.DialogueStyle.Input)
                {
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnYes")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnNo")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnOkay")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "txtInput")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblBody_2")].Visible = false;
                }

                // set labels
                withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblHeader")].Text = header;
                withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblBody_1")].Text = body;
                withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblBody_2")].Text = body2;
                withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "txtInput")].Text = "";
            }

            // set it all up
            GameState.diaIndex = Index;
            GameState.diaData1 = Data1;
            GameState.diaData2 = Data2;
            GameState.diaData3 = Data3;
            GameState.diaData4 = Data4;
            GameState.diaData5 = Data5;
            GameState.diaStyle = style;

            // make the Gui.Windows visible
            Gui.ShowWindow(Gui.GetWindowIndex("winDialogue"), true);
        }

        public static void DialogueHandler(long index)
        {
            long value;
            string diaInput;
            int x;
            int y;

            diaInput = Gui.Windows[Gui.GetWindowIndex("winDialogue")].Controls[(int)Gui.GetControlIndex("winDialogue", "txtInput")].Text;

            // Find out which button
            if (index == 1L) // Okay button
            {
                // Dialogue index
                switch (GameState.diaIndex)
                {
                    case (long)Core.Enum.DialogueType.TradeAmount:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            Trade.TradeItem((int)GameState.diaData1, (int)value);
                            break;
                        }

                    case (long)Core.Enum.DialogueType.DepositItem:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            Bank.DepositItem((int)GameState.diaData1, (int)value);
                            break;
                        }

                    case (long)Core.Enum.DialogueType.WithdrawItem:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            Bank.WithdrawItem((int)GameState.diaData1, (int)value);
                            break;
                        }

                    case (long)Core.Enum.DialogueType.DropItem:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            NetworkSend.SendDropItem((int)GameState.diaData1, (int)value);
                            break;
                        }
                }
            }

            else if (index == 2L) // Yes button
            {
                // Dialogue index
                switch (GameState.diaIndex)
                {
                    case (long)Core.Enum.DialogueType.Trade:
                        {
                            Trade.SendHandleTradeInvite(1);
                            break;
                        }

                    case (long)Core.Enum.DialogueType.Forget:
                        {
                            NetworkSend.ForgetSkill((int)GameState.diaData1);
                            break;
                        }

                    case (long)Core.Enum.DialogueType.Party:
                        {
                            Party.SendAcceptParty();
                            break;
                        }

                    case (long)Core.Enum.DialogueType.LootItem:
                        {
                            CheckMapGetItem();
                            break;
                        }

                    case (long)Core.Enum.DialogueType.DelChar:
                        {
                            NetworkSend.SendDelChar((byte)GameState.diaData1);
                            break;
                        }

                    case (long)Core.Enum.DialogueType.FillLayer:
                        {
                            if (GameState.diaData2 > 0L)
                            {
                                var loopTo = (int)Core.Type.MyMap.MaxX;
                                for (x = 0; x < loopTo; x++)
                                {
                                    var loopTo1 = (int)Core.Type.MyMap.MaxY;
                                    for (y = 0; y < loopTo1; y++)
                                    {
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].X = (int)GameState.diaData3;
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Y = (int)GameState.diaData4;
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Tileset = (int)GameState.diaData5;
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].AutoTile = (byte)GameState.diaData2;
                                        Autotile.CacheRenderState(x, y, (int)GameState.diaData1);
                                    }
                                }

                                // do a re-init so we can see our changes
                                Autotile.InitAutotiles();
                            }
                            else
                            {
                                var loopTo2 = (int)Core.Type.MyMap.MaxX;
                                for (x = 0; x < loopTo2; x++)
                                {
                                    var loopTo3 = (int)Core.Type.MyMap.MaxY;
                                    for (y = 0; y < loopTo3; y++)
                                    {
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].X = (int)GameState.diaData3;
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Y = (int)GameState.diaData4;
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Tileset = (int)GameState.diaData5;
                                        Core.Type.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].AutoTile = 0;
                                        Autotile.CacheRenderState(x, y, (int)GameState.diaData1);
                                    }
                                }
                            }

                            break;
                        }

                    case (long)Core.Enum.DialogueType.ClearLayer:
                        {
                            var loopTo4 = (int)Core.Type.MyMap.MaxX;
                            for (x = 0; x < loopTo4; x++)
                            {
                                var loopTo5 = (int)Core.Type.MyMap.MaxY;
                                for (y = 0; y < loopTo5; y++)
                                {
                                    {
                                        ref var withBlock = ref Core.Type.MyMap.Tile[x, y];
                                        withBlock.Layer[(int)GameState.diaData1].X = 0;
                                        withBlock.Layer[(int)GameState.diaData1].Y = 0;
                                        withBlock.Layer[(int)GameState.diaData1].Tileset = 0;
                                        withBlock.Layer[(int)GameState.diaData1].AutoTile = 0;
                                        Autotile.CacheRenderState(x, y, (int)GameState.diaData1);
                                    }
                                }
                            }

                            break;
                        }

                    case (long)Core.Enum.DialogueType.ClearAttributes:
                        {
                            var loopTo6 = (int)Core.Type.MyMap.MaxX;
                            for (x = 0; x < loopTo6; x++)
                            {
                                var loopTo7 = (int)Core.Type.MyMap.MaxY;
                                for (y = 0; y < loopTo7; y++)
                                {
                                    Core.Type.MyMap.Tile[x, y].Type = 0;
                                    Core.Type.MyMap.Tile[x, y].Type2 = 0;
                                }
                            }

                            break;
                        }
                }
            }

            else if (index == 3L) // No button
            {
                // Dialogue index
                switch (GameState.diaIndex)
                {
                    case (long)Core.Enum.DialogueType.Trade:
                        {
                            Trade.SendHandleTradeInvite(0);
                            break;
                        }

                    case (long)Core.Enum.DialogueType.Party:
                        {
                            Party.SendDeclineParty();
                            break;
                        }
                }
            }

            CloseDialogue();
            GameState.diaIndex = 0L;
            diaInput = "";
        }

        public static void ShowJobs()
        {
            Gui.HideWindows();
            GameState.NewCharJob = 0L;
            GameState.NewCharSprite = 1L;
            GameState.NewCnarGender = (long)Core.Enum.SexType.Male;
            Gui.Windows[Gui.GetWindowIndex("winJobs")].Controls[(int)Gui.GetControlIndex("winJobs", "lblClassName")].Text = Core.Type.Job[(int)GameState.NewCharJob].Name;
            Gui.ShowWindow(Gui.GetWindowIndex("winJobs"));
        }

        public static void AddChar(string name, int sex, int job, int sprite)
        {
            if (NetworkConfig.Socket?.IsConnected == true)
            {
                NetworkSend.SendAddChar(name, sex, job);
            }
            else
            {
                Dialogue("Invalid Connection", "Cannot connect to game server.", "Please try again.", (byte)Core.Enum.DialogueType.Alert);
            }
        }

        public static void SetChatHeight(long Height)
        {
            GameState.actChatHeight = Height;
        }

        public static void SetChatWidth(long Width)
        {
            GameState.actChatWidth = Width;
        }

        public static void ScrollChatBox(byte direction)
        {
            if (direction == 0) // up
            {
                if (Strings.Len(Core.Type.Chat[(int)(GameState.ChatScroll + 7L)].Text) > 0)
                {
                    if (GameState.ChatScroll < Constant.CHAT_LINES)
                    {
                        GameState.ChatScroll = GameState.ChatScroll + 1L;
                    }
                }
            }
            else if (GameState.ChatScroll > 0L)
            {
                GameState.ChatScroll = GameState.ChatScroll - 1L;
            }
        }

        public static long IsHotbar(long StartX, long StartY)
        {
            long IsHotbarRet = default;
            Core.Type.RectangleStruct tempRec;
            long i;

            for (i = 0L; i < Constant.MAX_HOTBAR; i++)
            {
                tempRec.Top = (int)(StartY + GameState.HotbarTop);
                tempRec.Left = (int)(StartX + i * GameState.HotbarOffsetX);
                tempRec.Right = tempRec.Left + GameState.PicX;
                tempRec.Bottom = tempRec.Top + GameState.PicY;

                if (Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot >= 0)
                {
                    if (GameState.CurMouseX >= tempRec.Left & GameState.CurMouseX <= tempRec.Right)
                    {
                        if (GameState.CurMouseY >= tempRec.Top & GameState.CurMouseY <= tempRec.Bottom)
                        {
                            IsHotbarRet = i;
                            return IsHotbarRet;
                        }
                    }
                }
            }

            return IsHotbarRet;
        }

        public static void ShowInvDesc(long x, long y, long invNum)
        {
            bool soulBound;

            if (invNum < 0L | invNum > Constant.MAX_INV)
                return;

            // show
            if (GetPlayerInv(GameState.MyIndex, (int)invNum) >= 0)
            {
                if (Core.Type.Item[GetPlayerInv(GameState.MyIndex, (int)invNum)].BindType > 0 & Core.Type.Player[GameState.MyIndex].Inv[(int)invNum].Bound > 0)
                    soulBound = Conversions.ToBoolean(1);
                ShowItemDesc(x, y, (long)GetPlayerInv(GameState.MyIndex, (int)invNum));
            }
        }

        public static void ShowItemDesc(long x, long y, long itemNum)
        {
            var Color = default(Microsoft.Xna.Framework.Color);
            string theName;
            string jobName;
            string levelTxt;
            long i;

            // set globals
            GameState.descType = (byte)Core.Enum.PartType.Item; // inventory
            GameState.descItem = itemNum;

            // set position
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Left = x;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Top = y;

            // show the window
            Gui.ShowWindow(Gui.GetWindowIndex("winDescription"), resetPosition: false);

            // exit out early if last is same
            if (GameState.descLastType == GameState.descType & GameState.descLastItem == GameState.descItem)
                return;

            // set last to this
            GameState.descLastType = GameState.descType;
            GameState.descLastItem = GameState.descItem;

            // show req. labels
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "lblClass")].Visible = true;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "lblLevel")].Visible = true;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = false;

            // set variables
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winDescription")];
                // name
                // If Not soulBound Then
                theName = Core.Type.Item[(int)itemNum].Name;
                // Else
                // theName = "(SB) " & Item(itemNum).Name)
                // End If
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblName")].Text = theName;
                switch (Core.Type.Item[(int)itemNum].Rarity)
                {
                    case 0: // white
                        {
                            Color = Microsoft.Xna.Framework.Color.White;
                            break;
                        }
                    case 1: // green
                        {
                            Color = Microsoft.Xna.Framework.Color.Green;
                            break;
                        }
                    case 2: // blue
                        {
                            Color = Microsoft.Xna.Framework.Color.Blue;
                            break;
                        }
                    case 3: // maroon
                        {
                            Color = Microsoft.Xna.Framework.Color.Red;
                            break;
                        }
                    case 4: // purple
                        {
                            Color = Microsoft.Xna.Framework.Color.Magenta;
                            break;
                        }
                    case 5: // cyan
                        {
                            Color = Microsoft.Xna.Framework.Color.Cyan;
                            break;
                        }
                }
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblName")].Color = Color;

                // class req
                if (Core.Type.Item[(int)itemNum].JobReq > 0)
                {
                    jobName = Core.Type.Job[Core.Type.Item[(int)itemNum].JobReq].Name;
                    // do we match it?
                    if (GetPlayerJob(GameState.MyIndex) == Core.Type.Item[(int)itemNum].JobReq)
                    {
                        Color = Microsoft.Xna.Framework.Color.Green;
                    }
                    else
                    {
                        Color = Microsoft.Xna.Framework.Color.Red;
                    }
                }
                else
                {
                    jobName = "No Job Req.";
                    Color = Microsoft.Xna.Framework.Color.Green;
                }

                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblClass")].Text = jobName;
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblClass")].Color = Color;

                // level
                if (Core.Type.Item[(int)itemNum].LevelReq > 0)
                {
                    levelTxt = "Level " + Core.Type.Item[(int)itemNum].LevelReq;
                    // do we match it?
                    if (GetPlayerLevel(GameState.MyIndex) >= Core.Type.Item[(int)itemNum].LevelReq)
                    {
                        Color = Microsoft.Xna.Framework.Color.Green;
                    }
                    else
                    {
                        Color = Microsoft.Xna.Framework.Color.Red;
                    }
                }
                else
                {
                    levelTxt = "No Level Req.";
                    Color = Microsoft.Xna.Framework.Color.Green;
                }
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblLevel")].Text = levelTxt;
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblLevel")].Color = Color;
            }

            // clear
            GameState.descText = new Core.Type.TextStruct[2];

            // go through the rest of the text
            switch (Core.Type.Item[(int)itemNum].Type)
            {
                case (byte)Core.Enum.ItemType.Equipment:
                    {
                        switch (Core.Type.Item[(int)itemNum].SubType)
                        {
                            case (byte)Core.Enum.ItemSubType.Weapon:
                                {
                                    AddDescInfo("Weapon", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)Core.Enum.ItemSubType.Armor:
                                {
                                    AddDescInfo("Armor", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)Core.Enum.ItemSubType.Helmet:
                                {
                                    AddDescInfo("Helmet", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)Core.Enum.ItemSubType.Shield:
                                {
                                    AddDescInfo("Shield", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)Core.Enum.ItemSubType.Shoes:
                                {
                                    AddDescInfo("Shoes", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)Core.Enum.ItemSubType.Gloves:
                                {
                                    AddDescInfo("Gloves", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                        }

                        break;
                    }
                case (byte)Core.Enum.ItemType.Consumable:
                    {
                        AddDescInfo("Consumable", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.ItemType.Currency:
                    {
                        AddDescInfo("Currency", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.ItemType.Skill:
                    {
                        AddDescInfo("Skill", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.ItemType.Projectile:
                    {
                        AddDescInfo("Projectile", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.ItemType.Pet:
                    {
                        AddDescInfo("Pet", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
            }

            // more info
            switch (Core.Type.Item[(int)itemNum].Type)
            {
                case (byte)Core.Enum.ItemType.Currency:
                    {
                        // binding
                        if (Core.Type.Item[(int)itemNum].BindType == 1)
                        {
                            AddDescInfo("Bind on Pickup", Microsoft.Xna.Framework.Color.White);
                        }
                        else if (Core.Type.Item[(int)itemNum].BindType == 2)
                        {
                            AddDescInfo("Bind on Equip", Microsoft.Xna.Framework.Color.White);
                        }

                        AddDescInfo("Value: " + Core.Type.Item[(int)itemNum].Price + " g", Microsoft.Xna.Framework.Color.Yellow);
                        break;
                    }
                case (byte)Core.Enum.ItemType.Equipment:
                    {
                        // Damage/defense
                        if (Core.Type.Item[(int)itemNum].SubType == (byte)Core.Enum.EquipmentType.Weapon)
                        {
                            AddDescInfo("Damage: " + Core.Type.Item[(int)itemNum].Data2, Microsoft.Xna.Framework.Color.White);
                            AddDescInfo("Speed: " + Core.Type.Item[(int)itemNum].Speed / 1000d + "s", Microsoft.Xna.Framework.Color.White);
                        }
                        else if (Core.Type.Item[(int)itemNum].Data2 > 0)
                        {
                            AddDescInfo("Defense: " + Core.Type.Item[(int)itemNum].Data2, Microsoft.Xna.Framework.Color.White);
                        }

                        // binding
                        if (Core.Type.Item[(int)itemNum].BindType == 1)
                        {
                            AddDescInfo("Bind on Pickup", Microsoft.Xna.Framework.Color.White);
                        }
                        else if (Core.Type.Item[(int)itemNum].BindType == 2)
                        {
                            AddDescInfo("Bind on Equip", Microsoft.Xna.Framework.Color.White);
                        }

                        AddDescInfo("Value: " + Core.Type.Item[(int)itemNum].Price + " G", Microsoft.Xna.Framework.Color.Yellow);

                        // stat bonuses
                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Strength] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Strength] + " Str", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] + " End", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Spirit] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Spirit] + " Spi", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] + " Luc", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Intelligence] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Intelligence] + " Int", Microsoft.Xna.Framework.Color.White);
                        }

                        break;
                    }
                case (byte)Core.Enum.ItemType.Consumable:
                    {
                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Strength] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Strength] + " Str", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] + " End", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Spirit] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Spirit] + " Spi", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Luck] + " Luc", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Intelligence] > 0)
                        {
                            AddDescInfo("+" + Core.Type.Item[(int)itemNum].Add_Stat[(int)Core.Enum.StatType.Intelligence] + " Int", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Type.Item[(int)itemNum].Data1 > 0)
                        {
                            switch (Core.Type.Item[(int)itemNum].SubType)
                            {
                                case (byte)Core.Enum.ItemSubType.AddHP:
                                    {
                                        AddDescInfo("+" + Core.Type.Item[(int)itemNum].Data1 + " HP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                                case (byte)Core.Enum.ItemSubType.AddMP:
                                    {
                                        AddDescInfo("+" + Core.Type.Item[(int)itemNum].Data1 + " MP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                                case (byte)Core.Enum.ItemSubType.AddSP:
                                    {
                                        AddDescInfo("+" + Core.Type.Item[(int)itemNum].Data1 + " SP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                                case (byte)Core.Enum.ItemSubType.Exp:
                                    {
                                        AddDescInfo("+" + Core.Type.Item[(int)itemNum].Data1 + " EXP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                            }

                        }

                        AddDescInfo("Value: " + Core.Type.Item[(int)itemNum].Price + " G", Microsoft.Xna.Framework.Color.Yellow);
                        break;
                    }
                case (byte)Core.Enum.ItemType.Skill:
                    {
                        AddDescInfo("Value: " + Core.Type.Item[(int)itemNum].Price + " G", Microsoft.Xna.Framework.Color.Yellow);
                        break;
                    }
            }
        }

        public static void ShowSkillDesc(long x, long y, long skillNum, long SkillSlot)
        {
            long Color;
            string theName;
            string sUse;
            long i;
            long barWidth;
            long tmpWidth;

            if (skillNum < 0 || skillNum > Core.Constant.MAX_SKILLS)
                return;

            // set globals
            GameState.descType = 2; // Skill
            GameState.descItem = skillNum;

            // set position
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Left = x;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Top = y;

            // show the window
            Gui.ShowWindow(Gui.GetWindowIndex("winDescription"), resetPosition: false);

            // exit out early if last is same
            if (GameState.descLastType == GameState.descType & GameState.descLastItem == GameState.descItem)
                return;

            // clear
            GameState.descText = new Core.Type.TextStruct[2];

            // hide req. labels
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "lblLevel")].Visible = false;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = true;

            // set variables
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winDescription")];
                // set name
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblName")].Text = Core.Type.Skill[(int)skillNum].Name;
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblName")].Color = Microsoft.Xna.Framework.Color.White;

                // find ranks
                if (SkillSlot >= 0L)
                {
                    // draw the rank bar
                    barWidth = 66L;
                    // If Type.Skill(skillNum).rank > 0 Then
                    // tmpWidth = ((PlayerSkills(SkillSlot).Uses / barWidth) / (Type.Skill(skillNum).NextUses / barWidth)) * barWidth
                    // Else
                    tmpWidth = 66L;
                    // End If
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Value = tmpWidth;
                    // does it rank up?
                    // If Type.Skill(skillNum).NextRank > 0 Then
                    Color = (long)Core.Enum.ColorType.White;
                    // sUse = "Uses: " & PlayerSkills(SkillSlot).Uses & "/" & Type.Skill(skillNum).NextUses
                    // If PlayerSkills(SkillSlot).Uses = Type.Skill(skillNum).NextUses Then
                    // If Not GetPlayerLevel(GameState.MyIndex) >= Skill(Type.Skill(skillNum).NextRank).LevelReq Then
                    // Color = BrightRed
                    // sUse = "Lvl " & Skill(Type.Skill(skillNum).NextRank).LevelReq & " req."
                    // End If
                    // End If
                    // Else
                    Color = (long)Core.Enum.ColorType.Gray;
                    sUse = "Max Rank";
                    // End If
                    // show controls
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblClass")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = true;
                    // set vals
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblClass")].Text = sUse;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblClass")].Color = Microsoft.Xna.Framework.Color.White;
                }
                else
                {
                    // hide some controls
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblClass")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = false;
                }
            }

            switch (Core.Type.Skill[(int)skillNum].Type)
            {
                case (byte)Core.Enum.SkillType.DamageHp:
                    {
                        AddDescInfo("Damage HP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.SkillType.DamageMp:
                    {
                        AddDescInfo("Damage SP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.SkillType.HealHp:
                    {
                        AddDescInfo("Heal HP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.SkillType.HealMp:
                    {
                        AddDescInfo("Heal SP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)Core.Enum.SkillType.Warp:
                    {
                        AddDescInfo("Warp", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
            }

            // more info
            switch (Core.Type.Skill[(int)skillNum].Type)
            {
                case (byte)Core.Enum.SkillType.DamageHp:
                case (byte)Core.Enum.SkillType.DamageMp:
                case (byte)Core.Enum.SkillType.HealHp:
                case (byte)Core.Enum.SkillType.HealMp:
                    {
                        // damage
                        AddDescInfo("Vital: " + Core.Type.Skill[(int)skillNum].Vital, Microsoft.Xna.Framework.Color.White);

                        // mp cost
                        AddDescInfo("Cost: " + Core.Type.Skill[(int)skillNum].MpCost + " SP", Microsoft.Xna.Framework.Color.White);

                        // cast time
                        AddDescInfo("Cast Time: " + Core.Type.Skill[(int)skillNum].CastTime + "s", Microsoft.Xna.Framework.Color.White);

                        // cd time
                        AddDescInfo("Cooldown: " + Core.Type.Skill[(int)skillNum].CdTime + "s", Microsoft.Xna.Framework.Color.White);

                        // aoe
                        if (Core.Type.Skill[(int)skillNum].AoE > 0)
                        {
                            AddDescInfo("AoE: " + Core.Type.Skill[(int)skillNum].AoE, Microsoft.Xna.Framework.Color.White);
                        }

                        // stun
                        if (Core.Type.Skill[(int)skillNum].StunDuration > 0)
                        {
                            AddDescInfo("Stun: " + Core.Type.Skill[(int)skillNum].StunDuration + "s", Microsoft.Xna.Framework.Color.White);
                        }

                        // dot
                        if (Core.Type.Skill[(int)skillNum].Duration > 0 & Core.Type.Skill[(int)skillNum].Interval > 0)
                        {
                            AddDescInfo("DoT: " + Core.Type.Skill[(int)skillNum].Duration / (double)Core.Type.Skill[(int)skillNum].Interval + " tick", Microsoft.Xna.Framework.Color.White);
                        }

                        break;
                    }
            }
        }

        public static void ShowShopDesc(long x, long y, long itemNum)
        {
            if (itemNum < 0L | itemNum > Constant.MAX_ITEMS)
                return;
            // show
            ShowItemDesc(x, y, itemNum);
        }

        public static void ShowEqDesc(long x, long y, long eqNum)
        {
            bool soulBound;

            // rte9
            if (eqNum < 0L | eqNum > (int)Core.Enum.EquipmentType.Count)
                return;

            if (Core.Type.Player[GameState.MyIndex].Equipment[(int)eqNum] < 0 || Core.Type.Player[GameState.MyIndex].Equipment[(int)eqNum] > Constant.MAX_ITEMS)
                return;

            // show
            if (Conversions.ToBoolean(Core.Type.Player[GameState.MyIndex].Equipment[(int)eqNum]))
            {
                if (Core.Type.Item[(int)Core.Type.Player[GameState.MyIndex].Equipment[(int)eqNum]].BindType > 0)
                    soulBound = Conversions.ToBoolean(1);
                ShowItemDesc(x, y, (long)Core.Type.Player[GameState.MyIndex].Equipment[(int)eqNum]);
            }
        }

        public static void AddDescInfo(string text, Microsoft.Xna.Framework.Color color)
        {
            long count;
            count = Information.UBound(GameState.descText);
            Array.Resize(ref GameState.descText, (int)(count + 1L));
            GameState.descText[(int)(count)].Text = text;
            GameState.descText[(int)(count)].Color = GameClient.ToDrawingColor(color);
        }

        public static void LogoutGame()
        {
            GameState.InMenu = true;
            GameState.InGame = false;
            
            NetworkConfig.DestroyNetwork();
            NetworkConfig.InitNetwork();
            General.ClearGameData();
        }

        public static void SetOptionsScreen()
        {
            // Resolutions
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1920x1080");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1680x1050");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1600x900");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1440x900");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1440x1050");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1366x768");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1360x1024");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1360x768");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1280x1024");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1280x800");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1280x768");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1280x720");
            Gui.Combobox_AddItem(Gui.GetWindowIndex("winOptions").ToString(), Gui.GetControlIndex("winOptions", "cmbRes"), "1120x864");

            // fill the options screen
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winOptions")];
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkMusic")].Value = Conversions.ToLong(Settings.Instance.Music);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkSound")].Value = Conversions.ToLong(Settings.Instance.Sound);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkAutotile")].Value = Conversions.ToLong(Settings.Instance.Autotile);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkFullscreen")].Value = Conversions.ToLong(Settings.Instance.Fullscreen);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "cmbRes")].Value = Settings.Instance.Resolution;
            }
        }

        public static void OpenShop(long shopNum)
        {
            // set globals
            GameState.InShop = (int)shopNum;
            GameState.shopSelectedSlot = 0L;
            GameState.shopSelectedItem = Core.Type.Shop[GameState.InShop].TradeItem[1].Item;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "chkSelling")].Value = 0L;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "chkBuying")].Value = 0L;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "btnSell")].Visible = false;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "btnBuy")].Visible = true;
            GameState.shopIsSelling = Conversions.ToBoolean(0);

            // set the current item
            Gui.UpdateShop();

            // show the window
            Gui.ShowWindow(Gui.GetWindowIndex("winShop"));
        }

        public static void CloseShop()
        {
            NetworkSend.SendCloseShop();
            Gui.HideWindow(Gui.GetWindowIndex("winShop"));
            GameState.shopSelectedSlot = 0L;
            GameState.shopSelectedItem = 0L;
            GameState.shopIsSelling = Conversions.ToBoolean(0);
            GameState.InShop = 0;
        }

        public static void UpdatePartyBars()
        {
            long i;
            long pIndex;
            long barWidth;
            long Width;

            // unload it if we're not in a party
            if (Core.Type.Party.Leader == 0)
            {
                return;
            }

            // max bar width
            barWidth = 173L;

            // make sure we're in a party
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winParty")];
                for (i = 0L; i <= 3L; i++)
                {
                    // get the pIndex from the control
                    if (withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picChar" + i)].Visible == true)
                    {
                        pIndex = withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picChar" + i)].Value;
                        // make sure they exist
                        if (pIndex > 0L)
                        {
                            if (IsPlaying((int)pIndex))
                            {
                                // get their health
                                if (GetPlayerVital((int)pIndex, Core.Enum.VitalType.HP) > 0 & GetPlayerMaxVital((int)pIndex, Core.Enum.VitalType.HP) > 0)
                                {
                                    Width = (long)Math.Round(GetPlayerVital((int)pIndex, Core.Enum.VitalType.HP) / (double)barWidth / (GetPlayerMaxVital((int)pIndex, Core.Enum.VitalType.HP) / (double)barWidth) * barWidth);
                                    withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picBar_HP" + i)].Width = Width;
                                }
                                else
                                {
                                    withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picBar_HP" + i)].Width = 0L;
                                }
                                // get their spirit
                                if (GetPlayerVital((int)pIndex, Core.Enum.VitalType.SP) > 0 & GetPlayerMaxVital((int)pIndex, Core.Enum.VitalType.SP) > 0)
                                {
                                    Width = (long)Math.Round(GetPlayerVital((int)pIndex, Core.Enum.VitalType.SP) / (double)barWidth / (GetPlayerMaxVital((int)pIndex, Core.Enum.VitalType.SP) / (double)barWidth) * barWidth);
                                    withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picBar_SP" + i)].Width = Width;
                                }
                                else
                                {
                                    withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picBar_SP" + i)].Width = 0L;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ShowTrade()
        {
            // show the window
            Gui.ShowWindow(Gui.GetWindowIndex("winTrade"));

            // set the controls up
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winTrade")];
                withBlock.Text = "Trading with " + GetPlayerName(Trade.InTrade);
                withBlock.Controls[(int)Gui.GetControlIndex("winTrade", "lblYourTrade")].Text = GetPlayerName(GameState.MyIndex) + "'s Offer";
                withBlock.Controls[(int)Gui.GetControlIndex("winTrade", "lblTheirTrade")].Text = GetPlayerName(Trade.InTrade) + "'s Offer";
                withBlock.Controls[(int)Gui.GetControlIndex("winTrade", "lblYourValue")].Text = "0g";
                withBlock.Controls[(int)Gui.GetControlIndex("winTrade", "lblTheirValue")].Text = "0g";
                withBlock.Controls[(int)Gui.GetControlIndex("winTrade", "lblStatus")].Text = "Choose items to offer.";
            }
        }

        public static void ShowPlayerMenu(long Index, long x, long y)
        {
            GameState.PlayerMenuIndex = Index;
            if (GameState.PlayerMenuIndex == 0L | GameState.PlayerMenuIndex == GameState.MyIndex)
                return;
            Gui.Windows[Gui.GetWindowIndex("winPlayerMenu")].Left = x - 5L;
            Gui.Windows[Gui.GetWindowIndex("winPlayerMenu")].Top = y - 5L;
            Gui.Windows[Gui.GetWindowIndex("winPlayerMenu")].Controls[(int)Gui.GetControlIndex("winPlayerMenu", "btnName")].Text = GetPlayerName((int)GameState.PlayerMenuIndex);
            Gui.ShowWindow(Gui.GetWindowIndex("winRightClickBG"));
            Gui.ShowWindow(Gui.GetWindowIndex("winPlayerMenu"));
        }

        public static void SetBarWidth(ref long MaxWidth, ref long Width)
        {
            long barDifference;

            if (MaxWidth < Width)
            {
                // find out the amount to increase per loop
                barDifference = (long)Math.Round((Width - MaxWidth) / 100d * 10d);

                // if it's less than 1 then default to 1
                if (barDifference < 0L)
                    barDifference = 0L;
                // set the width
                Width = Width - barDifference;
            }
            else if (MaxWidth > Width)
            {
                // find out the amount to increase per loop
                barDifference = (long)Math.Round((MaxWidth - Width) / 100d * 10d);

                // if it's less than 1 then default to 1
                if (barDifference < 0L)
                    barDifference = 0L;
                // set the width
                Width = Width + barDifference;
            }
        }

        public static void SetGoldLabel()
        {
            long i;
            var Amount = default(long);

            for (i = 0L; i < Constant.MAX_INV; i++)
            {
                if (GetPlayerInv(GameState.MyIndex, (int)i) == 1)
                {
                    Amount = GetPlayerInvValue(GameState.MyIndex, (int)i);
                }
            }
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "lblGold")].Text = Strings.Format(Amount, "#,###,###,###") + "g";
            Gui.Windows[Gui.GetWindowIndex("winInventory")].Controls[(int)Gui.GetControlIndex("winInventory", "lblGold")].Text = Strings.Format(Amount, "#,###,###,###") + "g";
        }

        public static int Clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static int ConvertMapX(int x)
        {
            int ConvertMapXRet = default;
            ConvertMapXRet = (int)Math.Round(x - GameState.TileView.Left * GameState.PicX - GameState.Camera.Left);
            return ConvertMapXRet;
        }

        public static int ConvertMapY(int y)
        {
            int ConvertMapYRet = default;
            ConvertMapYRet = (int)Math.Round(y - GameState.TileView.Top * GameState.PicY - GameState.Camera.Top);
            return ConvertMapYRet;
        }

        public static bool IsValidMapPoint(int x, int y)
        {
            if (x < 0)
                return default;
            if (y < 0)
                return default;
            if (x > Core.Type.Map[GetPlayerMap(GameState.MyIndex)].MaxX - 1)
                return default;
            if (y > Core.Type.Map[GetPlayerMap(GameState.MyIndex)].MaxY - 1)
                return default;

            return true;
        }

        public static List<Microsoft.Xna.Framework.Vector2> GetCellsInSquare(int xCenter, int yCenter, int distance)
        {
            int xMin = Math.Max(0, xCenter - distance);
            int xMax = Math.Min(Core.Type.MyMap.MaxX, xCenter + distance);
            int yMin = Math.Max(0, yCenter - distance);
            int yMax = Math.Min(Core.Type.MyMap.MaxY, yCenter + distance);

            var cells = new List<Microsoft.Xna.Framework.Vector2>();
            for (int y = yMin, loopTo = yMax; y < loopTo; y++)
            {
                for (int x = xMin, loopTo1 = xMax; x < loopTo1; x++)
                    cells.Add(new Microsoft.Xna.Framework.Vector2(x, y));
            }
            return cells;
        }

        public static List<Microsoft.Xna.Framework.Vector2> GetBorderCellsInSquare(int xCenter, int yCenter, int distance)
        {
            int xMin = Math.Max(0, xCenter - distance);
            int xMax = Math.Min(Core.Type.MyMap.MaxX, xCenter + distance);
            int yMin = Math.Max(0, yCenter - distance);
            int yMax = Math.Min(Core.Type.MyMap.MaxY, yCenter + distance);

            var borderCells = new List<Microsoft.Xna.Framework.Vector2>();

            // Top and bottom border
            for (int x = xMin, loopTo = xMax; x < loopTo; x++)
            {
                borderCells.Add(new Microsoft.Xna.Framework.Vector2(x, yMin));
                borderCells.Add(new Microsoft.Xna.Framework.Vector2(x, yMax));
            }

            // Left and right border
            for (int y = yMin + 1, loopTo1 = yMax - 1; y < loopTo1; y++)
            {
                borderCells.Add(new Microsoft.Xna.Framework.Vector2(xMin, y));
                borderCells.Add(new Microsoft.Xna.Framework.Vector2(xMax, y));
            }

            borderCells.Remove(new Microsoft.Xna.Framework.Vector2(xCenter, yCenter));
            return borderCells;
        }

        private static List<Microsoft.Xna.Framework.Vector2> Line(int x, int y, int xDestination, int yDestination)
        {
            var discovered = new HashSet<Microsoft.Xna.Framework.Vector2>();
            var litTiles = new List<Microsoft.Xna.Framework.Vector2>();

            int dx = Math.Abs(xDestination - x);
            int dy = Math.Abs(yDestination - y);
            int sx = x < xDestination ? 1 : -1;
            int sy = y < yDestination ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                var pos = new Microsoft.Xna.Framework.Vector2(x, y);
                if (discovered.Add(pos))
                    litTiles.Add(pos);

                if (x == xDestination && y == yDestination)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }

            return litTiles;
        }

        private static void PostProcessFovQuadrant(ref List<Microsoft.Xna.Framework.Vector2> _inFov, int x, int y, Core.Enum.QuadrantType quadrant)
        {
            int x1 = x;
            int y1 = y;
            int x2 = x;
            int y2 = y;
            var pos = new Microsoft.Xna.Framework.Vector2(x, y); // Use Vector2i for integer-based coordinates

            // Adjust coordinates based on the quadrant
            switch (quadrant)
            {
                case Core.Enum.QuadrantType.NE:
                    {
                        y1 = y + 1;
                        x2 = x - 1;
                        break;
                    }
                case Core.Enum.QuadrantType.SE:
                    {
                        y1 = y - 1;
                        x2 = x - 1;
                        break;
                    }
                case Core.Enum.QuadrantType.SW:
                    {
                        y1 = y - 1;
                        x2 = x + 1;
                        break;
                    }
                case Core.Enum.QuadrantType.NW:
                    {
                        y1 = y + 1;
                        x2 = x + 1;
                        break;
                    }
            }

            // Check if the position is already in the field of view and is not transparent
            if (!_inFov.Contains(pos) && !IsTransparent(x, y))
            {
                // Check neighboring cells to determine visibility
                if (IsTransparent(x1, y1) && _inFov.Contains(new Microsoft.Xna.Framework.Vector2(x1, y1)) || IsTransparent(x2, y2) && _inFov.Contains(new Microsoft.Xna.Framework.Vector2(x2, y2)) || IsTransparent(x2, y1) && _inFov.Contains(new Microsoft.Xna.Framework.Vector2(x2, y1)))
                {
                    _inFov.Add(pos);
                }
            }
        }

        public static List<Microsoft.Xna.Framework.Vector2> AppendFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
        {
            var inFov = new List<Microsoft.Xna.Framework.Vector2>();

            // Get all the border cells in a square around the origin within the given radius
            foreach (Microsoft.Xna.Framework.Vector2 borderCell in GetBorderCellsInSquare(xOrigin, yOrigin, radius))
            {
                // Trace a line from the origin to the border cell
                foreach (Microsoft.Xna.Framework.Vector2 cell in Line(xOrigin, yOrigin, (int)Math.Round(borderCell.X), (int)Math.Round(borderCell.Y)))
                {
                    // Stop if the cell is outside the radius
                    if (Math.Abs(cell.X - xOrigin) + Math.Abs(cell.Y - yOrigin) > radius)
                        break;

                    // Add the cell to the FOV list if it's transparent or light walls is true
                    if (IsTransparent((int)Math.Round(cell.X), (int)Math.Round(cell.Y)))
                    {
                        inFov.Add(cell);
                    }
                    else
                    {
                        if (lightWalls)
                            inFov.Add(cell);
                        break;
                    } // Stop the line if a non-transparent wall is encountered
                }
            }

            // Optional: Post-process the FOV for specific quadrants
            if (lightWalls)
            {
                foreach (Microsoft.Xna.Framework.Vector2 cell in GetCellsInSquare(xOrigin, yOrigin, radius))
                {
                    // Check the relative position to the origin and post-process based on quadrant
                    if (cell.X > xOrigin)
                    {
                        if (cell.Y > yOrigin)
                        {
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Enum.QuadrantType.SE);
                        }
                        else if (cell.Y < yOrigin)
                        {
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Enum.QuadrantType.NE);
                        }
                    }
                    else if (cell.X < xOrigin)
                    {
                        if (cell.Y > yOrigin)
                        {
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Enum.QuadrantType.SW);
                        }
                        else if (cell.Y < yOrigin)
                        {
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Enum.QuadrantType.NW);
                        }
                    }
                }
            }

            return inFov;
        }

        private static bool IsTransparent(int x, int y)
        {
            if (Core.Type.MyMap.Tile[x, y].Type == Core.Enum.TileType.Blocked | Core.Type.MyMap.Tile[x, y].Type2 == Core.Enum.TileType.Blocked)
            {
                return false;
            }

            return true;
        }

        public static void UpdateCamera()
        {
            long offsetX;
            long offsetY;
            long StartX;
            long StartY;
            long EndX;
            long EndY;
            long tileHeight;
            long tileWidth;
            long ScreenX;
            long ScreenY;

            tileWidth = (long)Math.Round(GameState.ResolutionWidth / 32d);
            tileHeight = (long)Math.Round(GameState.ResolutionHeight / 32d);

            ScreenX = (tileWidth + 1L) * GameState.PicX;
            ScreenY = (tileHeight + 1L) * GameState.PicY;

            offsetX = Core.Type.Player[GameState.MyIndex].XOffset + GameState.PicX;
            offsetY = Core.Type.Player[GameState.MyIndex].YOffset + GameState.PicY;
            StartX = GetPlayerX(GameState.MyIndex) - (tileWidth + 1L) / 2L - 1L;
            StartY = GetPlayerY(GameState.MyIndex) - (tileHeight + 1L) / 2L - 1L;

            // Ensure StartX and StartY do not go below 0
            if (StartX < 0)
            {
                StartX = 0;
                offsetX = 0; // Prevent shifting beyond the first tile
            }
            if (StartY < 0)
            {
                StartY = 0;
                offsetY = 0; // Prevent shifting beyond the first tile
            }

            if (tileWidth + 1L <= Core.Type.MyMap.MaxX)
            {
                if (StartX < 0L)
                {
                    offsetX = 0L;

                    if (StartX == -1)
                    {
                        if (Core.Type.Player[GameState.MyIndex].XOffset > 0)
                        {
                            offsetX = Core.Type.Player[GameState.MyIndex].XOffset;
                        }
                    }

                    StartX = 0L;
                }

                EndX = StartX + tileWidth + 1L + 1L;

                if (EndX > Core.Type.MyMap.MaxX)
                {
                    offsetX = 32L;

                    if (EndX == Core.Type.MyMap.MaxX)
                    {
                        if (Core.Type.Player[GameState.MyIndex].XOffset < 0)
                        {
                            offsetX = Core.Type.Player[GameState.MyIndex].XOffset + GameState.PicX;
                        }
                    }

                    EndX = Core.Type.MyMap.MaxX;
                    StartX = EndX - tileWidth - 1L;
                }
            }
            else
            {
                EndX = StartX + tileWidth + 1L + 1L;
            }

            if (tileHeight + 1L <= Core.Type.MyMap.MaxY)
            {
                if (StartY < 0L)
                {
                    offsetY = 0L;

                    if (StartY == -1)
                    {
                        if (Core.Type.Player[GameState.MyIndex].YOffset > 0)
                        {
                            offsetY = Core.Type.Player[GameState.MyIndex].YOffset;
                        }
                    }

                    StartY = 0L;
                }

                EndY = StartY + tileHeight + 1L + 1L;

                if (EndY > Core.Type.MyMap.MaxY)
                {
                    offsetY = 32L;

                    if (EndY == Core.Type.MyMap.MaxY)
                    {
                        if (Core.Type.Player[GameState.MyIndex].YOffset < 0)
                        {
                            offsetY = Core.Type.Player[GameState.MyIndex].YOffset + GameState.PicY;
                        }
                    }

                    EndY = Core.Type.MyMap.MaxY;
                    StartY = EndY - tileHeight - 1L;
                }
            }
            else
            {
                EndY = StartY + tileHeight + 1L + 1L;
            }

            if (tileWidth + 1L == Core.Type.MyMap.MaxX)
            {
                offsetX = 0L;
            }

            if (tileHeight + 1L == Core.Type.MyMap.MaxY)
            {
                offsetY = 0L;
            }

            ref var withBlock = ref GameState.TileView;
            withBlock.Top = StartY;
            withBlock.Bottom = EndY;
            withBlock.Left = StartX;
            withBlock.Right = EndX;

            ref var withBlock1 = ref GameState.Camera;
            withBlock1.Top = offsetY;
            withBlock1.Bottom = withBlock1.Top + ScreenY;
            withBlock1.Left = offsetX;
            withBlock1.Right = withBlock1.Left + ScreenX;
            
            // Optional: Update the map name display
            UpdateDrawMapName();
        }

    }
}