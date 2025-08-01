﻿using Core;
using Core.Localization;
using Microsoft.Toolkit.HighPerformance;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using System;
using System.Data.Common;
using static Core.Global.Command;
using Color = Core.Color;

namespace Client
{
    public class GameLogic
    {
        public static void ProcessNpcMovement(double MapNpcNum)
        {        
            if (MapNpcNum < 0 || MapNpcNum > Constant.MAX_MAP_NPCS)
            {
                return;
            }

            // Check if Npc is walking, and if so process moving them over
            if (Data.MyMapNpc[(int)MapNpcNum].Moving == (byte)MovementState.Walking)
            {
                switch (Data.MyMapNpc[(int)MapNpcNum].Dir)
                {
                    case (int)Direction.Up:
                        {
                            Core.Data.MyMapNpc[(int)MapNpcNum].Y -= 1;

                            break;
                        }
                    case (int)Direction.Down:
                        {
                            Core.Data.MyMapNpc[(int)MapNpcNum].Y += 1;
                            break;
                        }
                    case (int)Direction.Left:
                        {
                            Core.Data.MyMapNpc[(int)MapNpcNum].X -= 1;
                            break;
                        }
                    case (int)Direction.Right:
                        {
                            Core.Data.MyMapNpc[(int)MapNpcNum].X += 1;
                            break;
                        }
                }
            }
        }

        public static bool IsInBounds()
        {
            bool IsInBoundsRet = false;

            if (GameState.CurX >= 0 & GameState.CurX <= Data.MyMap.MaxX)
            {
                if (GameState.CurY >= 0 & GameState.CurY <= Data.MyMap.MaxY)
                {
                    IsInBoundsRet = true;
                }
            }

            return IsInBoundsRet;

        }

        public static bool GameStarted()
        {
            bool GameStartedRet = false;

            if (GameState.InGame == false || GameState.MapData == false || GameState.PlayerData == false)
                return GameStartedRet;

            GameStartedRet = true;
            return GameStartedRet;
        }

        public static void CreateActionMsg(string message, int color, byte msgType, int x, int y)
        {

            GameState.ActionMsgIndex = (byte)(GameState.ActionMsgIndex + 1);
            if (GameState.ActionMsgIndex >= byte.MaxValue)
                GameState.ActionMsgIndex = 1;
            
            ref var withBlock = ref Data.ActionMsg[GameState.ActionMsgIndex];
            withBlock.Message = message;
            withBlock.Color = color;
            withBlock.Type = msgType;
            withBlock.Created = General.GetTickCount();
            withBlock.Scroll = 0;
            withBlock.X = x;
            withBlock.Y = y;        

            if (Data.ActionMsg[GameState.ActionMsgIndex].Type == (int)Core.ActionMessageType.Scroll)
            {
                Data.ActionMsg[GameState.ActionMsgIndex].Y = Data.ActionMsg[GameState.ActionMsgIndex].Y + Rand(-2, 6);
                Data.ActionMsg[GameState.ActionMsgIndex].X = Data.ActionMsg[GameState.ActionMsgIndex].X + Rand(-8, 8);
            }

        }

        public static int Rand(int maxNumber, int minNumber = 0)
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
        public static void SetDirBlock(ref byte blockvar, ref byte dir, bool block)
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

        public static bool IsDirBlocked(ref byte blockvar, ref byte dir)
        {
            return Conversions.ToBoolean(blockvar & (long)Math.Round(Math.Pow(2d, dir)));
        }

        public static string ConvertCurrency(int amount)
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
                    Text.AddText(LocalesManager.Get("PlayerMsg"), (int)Core.Color.Yellow);
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
                                Text.AddText(LocalesManager.Get("Emote"), (int)Core.Color.Yellow);
                                goto Continue1;
                            }

                            NetworkSend.SendUseEmote(Conversions.ToInteger(command[1]));
                            break;
                        }

                    case "/help":
                        {
                            Text.AddText(LocalesManager.Get("Help1"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("Help2"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("Help3"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("Help4"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("Help5"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("Help6"), (int)Core.Color.Yellow);
                            break;
                        }

                    case "/info":
                        {
                            if (GameState.MyTarget >= 0)
                            {
                                if (GameState.MyTargetType == (int)TargetType.Player)
                                {
                                    NetworkSend.SendPlayerInfo(GetPlayerName(GameState.MyTarget));
                                    goto Continue1;
                                }
                            }

                            // Checks to make sure we have more than one string in the array
                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("Info"), (int)Core.Color.Yellow);
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
                            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
                            buffer.Dispose();
                            break;
                        }

                    case "/party":
                        {
                            if (GameState.MyTarget >= 0)
                            {
                                if (GameState.MyTargetType == (int)TargetType.Player)
                                {
                                    Party.SendPartyRequest(GetPlayerName(GameState.MyTarget));
                                    goto Continue1;
                                }
                            }

                            // Make sure they are actually sending something
                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("Party"), (int)Core.Color.BrightRed);
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
                            if (GameState.MyTarget >= 0)
                            {
                                if (GameState.MyTargetType == (int)TargetType.Player)
                                {
                                    Trade.SendTradeRequest(GetPlayerName(GameState.MyTarget));
                                    goto Continue1;
                                }
                            }

                            // Make sure they are actually sending something
                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("Trade"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            Trade.SendTradeRequest(command[1]);
                            break;
                        }

                    // // Moderator Admin Commands //
                    // Admin Help
                    case "/admin":
                        {
                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Moderator)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            Text.AddText(LocalesManager.Get("Admin1"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("Admin2"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("AdminGblMsg"), (int)Core.Color.Yellow);
                            Text.AddText(LocalesManager.Get("AdminPvtMsg"), (int)Core.Color.Yellow);
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

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Moderator)
                            {
                                Text.AddText(LocalesManager.Get("AccessAlert"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("Kick"), (int)Core.Color.Yellow);
                                goto Continue1;
                            }

                            NetworkSend.SendKick(command[1]);
                            break;
                        }

                    // // Mapper Admin Commands //
                    // Location
                    case "/loc":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            GameState.BLoc = !GameState.BLoc;
                            break;
                        }

                    // Warping to a player
                    case "/warpmeto":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("WarpMeTo"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.WarpMeTo(command[1]);
                            break;
                        }

                    // Warping a player to you
                    case "/warptome":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("WarpToMe"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.WarpToMe(command[1]);
                            break;
                        }

                    // Warping to a map
                    case "/warpto":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || !Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("WarpTo"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            n = Conversions.ToInteger(command[1]);

                            // Check to make sure its a valid map #
                            if (n >= 0 & n < Constant.MAX_MAPS)
                            {
                                NetworkSend.WarpTo(n);
                            }
                            else
                            {
                                Text.AddText(LocalesManager.Get("InvalidMap"), (int)Core.Color.BrightRed);
                            }

                            break;
                        }

                    // Setting sprite
                    case "/sprite":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1 || !Information.IsNumeric(command[1]))
                            {
                                Text.AddText(LocalesManager.Get("Sprite"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendSetSprite(Conversions.ToInteger(command[1]));
                            break;
                        }

                    // Map report
                    case "/mapreport":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestMapReport();
                            break;
                        }

                    // Respawn request
                    case "/respawn":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            Map.SendMapRespawn();
                            break;
                        }

                    case "/editmap":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Mapper)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            Map.SendRequestEditMap();
                            break;
                        }

                    case "/editscript":
                        {
                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Owner)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                            }

                            Script.SendRequestEditScript();
                            break;
                        }

                    // // Moderator Commands //
                    // Welcome change
                    case "/welcome":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Moderator)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1)
                            {
                                Text.AddText(LocalesManager.Get("Welcome"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendMotdChange(Strings.Right(chatText, Strings.Len(chatText) - 5));
                            break;
                        }

                    // Check the ban list
                    case "/banlist":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Moderator)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendBanList();
                            break;
                        }

                    // Banning a player
                    case "/ban":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Moderator)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if (Information.UBound(command) < 1)
                            {
                                Text.AddText(LocalesManager.Get("Ban"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendBan(command[1]);
                            break;
                        }

                    // // Owner Admin Commands //
                    // Giving another player access
                    case "/bandestroy":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Owner)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendBanDestroy();
                            break;
                        }

                    case "/access":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Owner)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            if ((Information.UBound(command) < 2 || Information.IsNumeric(command[1])) | !Information.IsNumeric(command[2]))
                            {
                                Text.AddText(LocalesManager.Get("Access"), (int)Core.Color.Yellow);
                                goto Continue1;
                            }

                            NetworkSend.SendSetAccess(command[1], (byte)Conversions.ToLong(command[2]));
                            break;
                        }

                    // // Developer Admin Commands //
                    case "/editresource":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditResource();
                            break;
                        }

                    case "/editanimation":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditAnimation();
                            break;
                        }

                    case "/edititem":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditItem();
                            break;
                        }

                    case "/editprojectile":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            Projectile.SendRequestEditProjectiles();
                            break;
                        }

                    case "/editnpc":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditNpc();
                            break;
                        }

                    case "/editjob":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditJob();
                            break;
                        }

                    case "/editskill":
                        {

                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditSkill();
                            break;
                        }

                    case "/editshop":
                        {
                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditShop();
                            break;
                        }

                    case "/editmoral":
                        {
                            if (GetPlayerAccess(GameState.MyIndex) < (int)AccessLevel.Developer)
                            {
                                Text.AddText(LocalesManager.Get("AccessDenied"), (int)Core.Color.BrightRed);
                                goto Continue1;
                            }

                            NetworkSend.SendRequestEditMoral();
                            break;
                        }

                    case "":
                        {
                            break;
                        }

                    default:
                        {
                            Text.AddText(LocalesManager.Get("InvalidCmd"), (int)Core.Color.BrightRed);
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

            if (General.GetTickCount() > Core.Data.Player[GameState.MyIndex].MapGetTimer + 250)
            {
                Core.Data.Player[GameState.MyIndex].MapGetTimer = General.GetTickCount();
                buffer.WriteInt32((int)Packets.ClientPackets.CMapGetItem);
                NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            }

            buffer.Dispose();
        }

        public static void ClearActionMsg(byte index)
        {
            Data.ActionMsg[index].Message = "";
            Data.ActionMsg[index].Created = 0;
            Data.ActionMsg[index].Type = 0;
            Data.ActionMsg[index].Color = 0;
            Data.ActionMsg[index].Scroll = 0;
            Data.ActionMsg[index].X = 0;
            Data.ActionMsg[index].Y = 0;
        }

        public static void UpdateDrawMapName()
        {
            if (Data.MyMap.Moral > 0)
            {
                GameState.DrawMapNameColor = GameClient.QbColorToXnaColor(Data.Moral[Data.MyMap.Moral].Color);
            }
        }

        public static void AddChatBubble(int target, byte targetType, string msg, int Color)
        {
            int i;
            int index;

            // Set the global index
            GameState.ChatBubbleindex = GameState.ChatBubbleindex + 1;
            if (GameState.ChatBubbleindex < 1 | GameState.ChatBubbleindex > byte.MaxValue)
                GameState.ChatBubbleindex = 1;

            // Default to new bubble
            index = GameState.ChatBubbleindex;

            // Loop through and see if that player/Npc already has a chat bubble
            for (i = 0; i < byte.MaxValue; i++)
            {
                if (Data.ChatBubble[i].TargetType == targetType)
                {
                    if (Data.ChatBubble[i].Target == target)
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
                ref var withBlock = ref Data.ChatBubble[index];
                withBlock.Target = target;
                withBlock.TargetType = targetType;
                withBlock.Msg = msg;
                withBlock.Color = Color;
                withBlock.Timer = General.GetTickCount();
                withBlock.Active = true;
            }

        }

        public static void RemoveChatBubbles()
        {
            // Loop through and see if that player/Npc already has a chat bubble
            for (int i = 0; i <= GameState.ChatBubbleindex; i++)
            {
                ref var withBlock = ref Data.ChatBubble[i];
                withBlock.Target = 0;
                withBlock.TargetType = 0;
                withBlock.Msg = "";
                withBlock.Color = 0;
                withBlock.Timer = 0;
                withBlock.Active = false;

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
                case (byte)SystemMessage.Connection:
                    {
                        header = "Invalid Connection";
                        body = "You lost connection to the game server.";
                        body2 = "Please try again later.";

                        NetworkConfig.InitNetwork();
                        GameState.InGame = false;
                        break;
                    }

                case (byte)SystemMessage.Banned:
                    {
                        header = "Banned";
                        body = "You have been banned, have a nice day!";
                        body2 = "Please send all ban appeals to an administrator.";
                        GameState.InGame = false;
                        break;
                    }

                case (byte)SystemMessage.Kicked:
                    {
                        header = "Kicked";
                        body = "You have been kicked.";
                        body2 = "Please try and behave.";
                        GameState.InGame = false;
                        break;
                    }

                case (byte)SystemMessage.ClientOutdated:
                    {
                        header = "Wrong Version";
                        body = "Your game client is the wrong version.";
                        body2 = "Please try updating.";
                        break;
                    }

                case (byte)SystemMessage.ServerMaintenance:
                    {
                        header = "Connection Refused";
                        body = "The server is currently going under maintenance.";
                        body2 = "Please try again soon.";
                        break;
                    }

                case (byte)SystemMessage.NameTaken:
                    {
                        header = "Invalid Name";
                        body = "This name is already in use.";
                        body2 = "Please try another name.";
                        break;
                    }

                case (byte)SystemMessage.NameLengthInvalid:
                    {
                        header = "Invalid Name";
                        body = "This name is too short or too long.";
                        body2 = "Please try another name.";
                        break;
                    }

                case (byte)SystemMessage.NameContainsIllegalChars:
                    {
                        header = "Invalid Name";
                        body = "This name contains illegal characters.";
                        body2 = "Please try another name.";
                        break;
                    }

                case (byte)SystemMessage.DatabaseError:
                    {
                        header = "Invalid Connection";
                        body = "Cannot connect to database.";
                        body2 = "Please try again later.";
                        break;
                    }

                case (byte)SystemMessage.WrongPassword:
                    {
                        header = "Invalid Login";
                        body = "Invalid username or password.";
                        body2 = "Please try again.";
                        Gui.ClearPasswordTexts();
                        break;
                    }

                case (byte)SystemMessage.AccountActivationRequired:
                    {
                        header = "Inactive Account";
                        body = "Your account is not activated.";
                        body2 = "Please activate your account then try again.";
                        break;
                    }

                case (byte)SystemMessage.MaxCharactersReached:
                    {
                        header = "Cannot Merge";
                        body = "You cannot merge a full account.";
                        body2 = "Please clear a character slot.";
                        break;
                    }

                case (byte)SystemMessage.ConfirmCharacterDeletion:
                    {
                        header = "Deleted Character";
                        body = "Your character was successfully deleted.";
                        body2 = "Please log on to continue playing.";
                        break;
                    }

                case (byte)SystemMessage.CreateAccount:
                    {
                        header = "Account Created";
                        body = "Your account was successfully created.";
                        body2 = "Now, you can play!";
                        break;
                    }

                case (byte)SystemMessage.MultipleAccountsNotAllowed:
                    {
                        header = "Multiple Accounts";
                        body = "Multiple accounts are not authorized.";
                        body2 = "Please logout and try again!";
                        break;
                    }

                case (byte)SystemMessage.Login:
                    {
                        header = "Cannot Login";
                        body = "This account does not exist.";
                        body2 = "Please try registering the account.";
                        break;
                    }

                case (byte)SystemMessage.Crashed:
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
            Dialogue(header, body, body2, (byte)DialogueType.Alert);
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
                if (style == (int)DialogueStyle.YesNo)
                {
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnYes")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnNo")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnOkay")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "txtInput")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblBody_2")].Visible = true;
                }
                else if (style == (int)DialogueStyle.Okay)
                {
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnYes")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnNo")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "btnOkay")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "txtInput")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDialogue", "lblBody_2")].Visible = true;
                }
                else if (style == (int)DialogueStyle.Input)
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

            try
            {
                General.SetWindowFocus(General.Client.Window.Handle);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
                    case (long)DialogueType.TradeAmount:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            Trade.TradeItem((int)GameState.diaData1, (int)value);
                            break;
                        }

                    case (long)DialogueType.DepositItem:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            Bank.DepositItem((int)GameState.diaData1, (int)value);
                            break;
                        }

                    case (long)DialogueType.WithdrawItem:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            Bank.WithdrawItem((byte)(int)GameState.diaData1, (int)value);
                            break;
                        }

                    case (long)DialogueType.DropItem:
                        {
                            value = (long)Math.Round(Conversion.Val(diaInput));
                            NetworkSend.SendDropItem((int)GameState.diaData1, (int)value);
                            break;
                        }

                    case (long)DialogueType.Information:
                        {
                            GameState.Info = true;
                            break;
                        }
                }
            }

            else if (index == 2L) // Yes button
            {
                // Dialogue index
                switch (GameState.diaIndex)
                {
                    case (long)DialogueType.Trade:
                        {
                            Trade.SendHandleTradeInvite(1);
                            break;
                        }

                    case (long)DialogueType.ForgetSkill:
                        {
                            NetworkSend.ForgetSkill((int)GameState.diaData1);
                            break;
                        }

                    case (long)DialogueType.PartyInvite:
                        {
                            Party.SendAcceptParty();
                            break;
                        }

                    case (long)DialogueType.LootConfirmation:
                        {
                            CheckMapGetItem();
                            break;
                        }

                    case (long)DialogueType.DeleteCharacter:
                        {
                            NetworkSend.SendDelChar((byte)GameState.diaData1);
                            break;
                        }

                    case (long)DialogueType.FillLayer:
                        {
                            if (GameState.diaData2 > 0L)
                            {
                                var loopTo = (int)Data.MyMap.MaxX;
                                for (x = 0; x < loopTo; x++)
                                {
                                    var loopTo1 = (int)Data.MyMap.MaxY;
                                    for (y = 0; y < loopTo1; y++)
                                    {
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].X = (int)GameState.diaData3;
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Y = (int)GameState.diaData4;
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Tileset = (int)GameState.diaData5;
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].AutoTile = (byte)GameState.diaData2;
                                        Autotile.CacheRenderState(x, y, (int)GameState.diaData1);
                                    }
                                }

                                // do a re-init so we can see our changes
                                Autotile.InitAutotiles();
                            }
                            else
                            {
                                var loopTo2 = (int)Data.MyMap.MaxX;
                                for (x = 0; x < loopTo2; x++)
                                {
                                    var loopTo3 = (int)Data.MyMap.MaxY;
                                    for (y = 0; y < loopTo3; y++)
                                    {
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].X = (int)GameState.diaData3;
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Y = (int)GameState.diaData4;
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].Tileset = (int)GameState.diaData5;
                                        Data.MyMap.Tile[x, y].Layer[(int)GameState.diaData1].AutoTile = 0;
                                        Autotile.CacheRenderState(x, y, (int)GameState.diaData1);
                                    }
                                }
                            }

                            break;
                        }

                    case (long)DialogueType.ClearLayer:
                        {
                            var loopTo4 = (int)Data.MyMap.MaxX;
                            for (x = 0; x < loopTo4; x++)
                            {
                                var loopTo5 = (int)Data.MyMap.MaxY;
                                for (y = 0; y < loopTo5; y++)
                                {
                                    {
                                        ref var withBlock = ref Data.MyMap.Tile[x, y];
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

                    case (long)DialogueType.ClearAttributes:
                        {
                            var loopTo6 = (int)Data.MyMap.MaxX;
                            for (x = 0; x < loopTo6; x++)
                            {
                                var loopTo7 = (int)Data.MyMap.MaxY;
                                for (y = 0; y < loopTo7; y++)
                                {
                                    Data.MyMap.Tile[x, y].Type = 0;
                                    Data.MyMap.Tile[x, y].Type2 = 0;
                                }
                            }

                            break;
                        }

                    case (long)DialogueType.FillAttributes:
                        {
                            TileType type = TileType.None;
                            var loopTo6 = (int)Data.MyMap.MaxX;
                            for (x = 0; x < loopTo6; x++)
                            {
                                var loopTo7 = (int)Data.MyMap.MaxY;
                                for (y = 0; y < loopTo7; y++)
                                {
                                    // blocked tile
                                    if (frmEditor_Map.Instance.optBlocked.Checked == true)
                                    {
                                        type = TileType.Blocked;
                                    }

                                    // warp tile
                                    if (frmEditor_Map.Instance.optWarp.Checked == true)
                                    {
                                        type = TileType.Warp;
                                    }

                                    // item spawn
                                    if (frmEditor_Map.Instance.optItem.Checked == true)
                                    {
                                        type = TileType.Item;
                                    }

                                    // Npc avoid
                                    if (frmEditor_Map.Instance.optNpcAvoid.Checked == true)
                                    {
                                        type = TileType.NpcAvoid;
                                    }

                                    // resource
                                    if (frmEditor_Map.Instance.optResource.Checked == true)
                                    {
                                        type = TileType.Resource;
                                    }

                                    // Npc spawn
                                    if (frmEditor_Map.Instance.optNpcSpawn.Checked == true)
                                    {
                                        type = TileType.NpcSpawn;
                                    }

                                    // shop
                                    if (frmEditor_Map.Instance.optShop.Checked == true)
                                    {
                                        type = TileType.Shop;
                                    }

                                    // bank
                                    if (frmEditor_Map.Instance.optBank.Checked == true)
                                    {
                                        type = TileType.Bank;
                                    }

                                    // heal
                                    if (frmEditor_Map.Instance.optHeal.Checked == true)
                                    {
                                        type = TileType.Heal;
                                    }

                                    // trap
                                    if (frmEditor_Map.Instance.optTrap.Checked == true)
                                    {
                                        type = TileType.Trap;
                                    }

                                    // Animation
                                    if (frmEditor_Map.Instance.optAnimation.Checked == true)
                                    {
                                        type = TileType.Animation;
                                    }

                                    // No Xing
                                    if (frmEditor_Map.Instance.optNoCrossing.Checked == true)
                                    {
                                        type = TileType.NoCrossing;
                                    }

                                    if (frmEditor_Map.Instance.cmbAttribute.InvokeRequired)
                                    {
                                        int selectedIndex = (int)frmEditor_Map.Instance.cmbAttribute.Invoke(
                                            new Func<int>(() => frmEditor_Map.Instance.cmbAttribute.SelectedIndex));

                                        if (selectedIndex == 1)
                                        {
                                            Data.MyMap.Tile[x, y].Type = type;
                                        }
                                        else
                                        {
                                            Data.MyMap.Tile[x, y].Type2 = type;
                                        }
                                    }
                                }
                            }

                            break;
                        }

                    case (long)DialogueType.ClearMap:
                        Map.ClearMap();
                        Map.ClearMapNpcs();
                        Map.ClearMapItems();
                        break;
                }
            }

            else if (index == 3L) // No button
            {
                // Dialogue index
                switch (GameState.diaIndex)
                {
                    case (long)DialogueType.Trade:
                        {
                            Trade.SendHandleTradeInvite(0);
                            break;
                        }

                    case (long)DialogueType.PartyInvite:
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
            GameState.NewCnarGender = (long)Sex.Male;
            Gui.Windows[Gui.GetWindowIndex("winJobs")].Controls[(int)Gui.GetControlIndex("winJobs", "lblJobName")].Text = Data.Job[(int)GameState.NewCharJob].Name;
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
                Dialogue("Invalid Connection", "Cannot connect to game server.", "Please try again.", (byte)DialogueType.Alert);
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
                if (Strings.Len(Data.Chat[(int)(GameState.ChatScroll + 7L)].Text) > 0)
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
            Core.Type.Rectangle tempRec;
            long i;

            for (i = 0L; i < Constant.MAX_HOTBAR; i++)
            {
                tempRec.Top = (int)(StartY + GameState.HotbarTop);
                tempRec.Left = (int)(StartX + i * GameState.HotbarOffsetX);
                tempRec.Right = tempRec.Left + GameState.SizeX;
                tempRec.Bottom = tempRec.Top + GameState.SizeY;

                if (Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot >= 0)
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

            return -1;
        }

        public static void ShowInvDesc(long x, long y, long invNum)
        {
            bool soulBound;

            if (invNum < 0L | invNum > Constant.MAX_INV)
                return;

            // show
            if (GetPlayerInv(GameState.MyIndex, (int)invNum) >= 0)
            {
                if (Core.Data.Item[GetPlayerInv(GameState.MyIndex, (int)invNum)].BindType > 0 & Core.Data.Player[GameState.MyIndex].Inv[(int)invNum].Bound > 0)
                    soulBound = true;
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
            GameState.descType = (byte)DraggablePartType.Item; // inventory
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
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "lblJob")].Visible = true;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "lblLevel")].Visible = true;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = false;

            // set variables
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winDescription")];
                // name
                // If Not soulBound Then
                theName = Core.Data.Item[(int)itemNum].Name;
                // Else
                // theName = "(SB) " & Item(itemNum).Name)
                // End If
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblName")].Text = theName;
                switch (Core.Data.Item[(int)itemNum].Rarity)
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
                if (Core.Data.Item[(int)itemNum].JobReq > 0)
                {
                    jobName = Data.Job[Core.Data.Item[(int)itemNum].JobReq].Name;
                    // do we match it?
                    if (GetPlayerJob(GameState.MyIndex) == Core.Data.Item[(int)itemNum].JobReq)
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

                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblJob")].Text = jobName;
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblJob")].Color = Color;

                // level
                if (Core.Data.Item[(int)itemNum].LevelReq > 0)
                {
                    levelTxt = "Level " + Core.Data.Item[(int)itemNum].LevelReq;
                    // do we match it?
                    if (GetPlayerLevel(GameState.MyIndex) >= Core.Data.Item[(int)itemNum].LevelReq)
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
            GameState.Description = new Core.Type.Text[2];

            // go through the rest of the text
            switch (Core.Data.Item[(int)itemNum].Type)
            {
                case (byte)ItemCategory.Equipment:
                    {
                        switch (Core.Data.Item[(int)itemNum].SubType)
                        {
                            case (byte)ItemSubCategory.Weapon:
                                {
                                    AddDescInfo("Weapon", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)ItemSubCategory.Armor:
                                {
                                    AddDescInfo("Armor", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)ItemSubCategory.Helmet:
                                {
                                    AddDescInfo("Helmet", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)ItemSubCategory.Shield:
                                {
                                    AddDescInfo("Shield", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)ItemSubCategory.Shoes:
                                {
                                    AddDescInfo("Shoes", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                            case (byte)ItemSubCategory.Gloves:
                                {
                                    AddDescInfo("Gloves", Microsoft.Xna.Framework.Color.White);
                                    break;
                                }
                        }

                        break;
                    }
                case (byte)ItemCategory.Consumable:
                    {
                        AddDescInfo("Consumable", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)ItemCategory.Currency:
                    {
                        AddDescInfo("Currency", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)ItemCategory.Skill:
                    {
                        AddDescInfo("Skill", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)ItemCategory.Projectile:
                    {
                        AddDescInfo("Projectile", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)ItemCategory.Pet:
                    {
                        AddDescInfo("Pet", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
            }

            // more info
            switch (Core.Data.Item[(int)itemNum].Type)
            {
                case (byte)ItemCategory.Currency:
                    {
                        // binding
                        if (Core.Data.Item[(int)itemNum].BindType == 1)
                        {
                            AddDescInfo("Bind on Pickup", Microsoft.Xna.Framework.Color.White);
                        }
                        else if (Core.Data.Item[(int)itemNum].BindType == 2)
                        {
                            AddDescInfo("Bind on Equip", Microsoft.Xna.Framework.Color.White);
                        }

                        AddDescInfo("Value: " + Core.Data.Item[(int)itemNum].Price + " g", Microsoft.Xna.Framework.Color.Yellow);
                        break;
                    }
                case (byte)ItemCategory.Equipment:
                    {
                        // Damage/defense
                        if (Core.Data.Item[(int)itemNum].SubType == (byte)Equipment.Weapon)
                        {
                            AddDescInfo("Damage: " + Core.Data.Item[(int)itemNum].Data2, Microsoft.Xna.Framework.Color.White);
                            AddDescInfo("Speed: " + Core.Data.Item[(int)itemNum].Speed / 1000d + "s", Microsoft.Xna.Framework.Color.White);
                        }
                        else if (Core.Data.Item[(int)itemNum].Data2 > 0)
                        {
                            AddDescInfo("Defense: " + Core.Data.Item[(int)itemNum].Data2, Microsoft.Xna.Framework.Color.White);
                        }

                        // binding
                        if (Core.Data.Item[(int)itemNum].BindType == 1)
                        {
                            AddDescInfo("Bind on Pickup", Microsoft.Xna.Framework.Color.White);
                        }
                        else if (Core.Data.Item[(int)itemNum].BindType == 2)
                        {
                            AddDescInfo("Bind on Equip", Microsoft.Xna.Framework.Color.White);
                        }

                        AddDescInfo("Value: " + Core.Data.Item[(int)itemNum].Price + " G", Microsoft.Xna.Framework.Color.Yellow);

                        // stat bonuses
                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Strength] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Strength] + " Str", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] + " End", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Spirit] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Spirit] + " Spi", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] + " Luc", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Intelligence] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Intelligence] + " Int", Microsoft.Xna.Framework.Color.White);
                        }

                        break;
                    }
                case (byte)ItemCategory.Consumable:
                    {
                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Strength] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Strength] + " Str", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] + " End", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Spirit] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Spirit] + " Spi", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Luck] + " Luc", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Intelligence] > 0)
                        {
                            AddDescInfo("+" + Core.Data.Item[(int)itemNum].Add_Stat[(int)Core.Stat.Intelligence] + " Int", Microsoft.Xna.Framework.Color.White);
                        }

                        if (Core.Data.Item[(int)itemNum].Data1 > 0)
                        {
                            switch (Core.Data.Item[(int)itemNum].SubType)
                            {
                                case (byte)ItemSubCategory.RestoresHealth:
                                    {
                                        AddDescInfo("+" + Core.Data.Item[(int)itemNum].Data1 + " HP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                                case (byte)ItemSubCategory.RestoresMana:
                                    {
                                        AddDescInfo("+" + Core.Data.Item[(int)itemNum].Data1 + " MP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                                case (byte)ItemSubCategory.RestoresStamina:
                                    {
                                        AddDescInfo("+" + Core.Data.Item[(int)itemNum].Data1 + " SP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                                case (byte)ItemSubCategory.GrantsExperience:
                                    {
                                        AddDescInfo("+" + Core.Data.Item[(int)itemNum].Data1 + " EXP", Microsoft.Xna.Framework.Color.White);
                                        break;
                                    }
                            }

                        }

                        AddDescInfo("Value: " + Core.Data.Item[(int)itemNum].Price + " G", Microsoft.Xna.Framework.Color.Yellow);
                        break;
                    }
                case (byte)ItemCategory.Skill:
                    {
                        AddDescInfo("Value: " + Core.Data.Item[(int)itemNum].Price + " G", Microsoft.Xna.Framework.Color.Yellow);
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
            GameState.Description = new Core.Type.Text[2];

            // hide req. labels
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "lblLevel")].Visible = false;
            Gui.Windows[Gui.GetWindowIndex("winDescription")].Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = true;

            // set variables
            {
                var withBlock = Gui.Windows[Gui.GetWindowIndex("winDescription")];
                // set name
                withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblName")].Text = Data.Skill[(int)skillNum].Name;
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
                    Color = (long)Core.Color.White;
                    // sUse = "Uses: " & PlayerSkills(SkillSlot).Uses & "/" & Type.Skill(skillNum).NextUses
                    // If PlayerSkills(SkillSlot).Uses = Type.Skill(skillNum).NextUses Then
                    // If Not GetPlayerLevel(GameState.MyIndex) >= Skill(Type.Skill(skillNum).NextRank).LevelReq Then
                    // Color = BrightRed
                    // sUse = "Lvl " & Skill(Type.Skill(skillNum).NextRank).LevelReq & " req."
                    // End If
                    // End If
                    // Else
                    Color = (long)Core.Color.Gray;
                    sUse = "Max Rank";
                    // End If
                    // show controls
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblJob")].Visible = true;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = true;
                    // set vals
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblJob")].Text = sUse;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblJob")].Color = Microsoft.Xna.Framework.Color.White;
                }
                else
                {
                    // hide some controls
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "lblJob")].Visible = false;
                    withBlock.Controls[(int)Gui.GetControlIndex("winDescription", "picBar")].Visible = false;
                }
            }

            switch (Data.Skill[(int)skillNum].Type)
            {
                case (byte)SkillEffect.DamageHealth:
                    {
                        AddDescInfo("Damage HP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)SkillEffect.DamageMana:
                    {
                        AddDescInfo("Damage SP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)SkillEffect.HealHealth:
                    {
                        AddDescInfo("Heal HP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)SkillEffect.HealMana:
                    {
                        AddDescInfo("Heal SP", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
                case (byte)SkillEffect.Warp:
                    {
                        AddDescInfo("Warp", Microsoft.Xna.Framework.Color.White);
                        break;
                    }
            }

            // more info
            switch (Data.Skill[(int)skillNum].Type)
            {
                case (byte)SkillEffect.DamageHealth:
                case (byte)SkillEffect.DamageMana:
                case (byte)SkillEffect.HealHealth:
                case (byte)SkillEffect.HealMana:
                    {
                        // damage
                        AddDescInfo("Vital: " + Data.Skill[(int)skillNum].Vital, Microsoft.Xna.Framework.Color.White);

                        // mp cost
                        AddDescInfo("Cost: " + Data.Skill[(int)skillNum].MpCost + " SP", Microsoft.Xna.Framework.Color.White);

                        // cast time
                        AddDescInfo("Cast Time: " + Data.Skill[(int)skillNum].CastTime + "s", Microsoft.Xna.Framework.Color.White);

                        // cd time
                        AddDescInfo("Cooldown: " + Data.Skill[(int)skillNum].CdTime + "s", Microsoft.Xna.Framework.Color.White);

                        // aoe
                        if (Data.Skill[(int)skillNum].AoE > 0)
                        {
                            AddDescInfo("AoE: " + Data.Skill[(int)skillNum].AoE, Microsoft.Xna.Framework.Color.White);
                        }

                        // stun
                        if (Data.Skill[(int)skillNum].StunDuration > 0)
                        {
                            AddDescInfo("Stun: " + Data.Skill[(int)skillNum].StunDuration + "s", Microsoft.Xna.Framework.Color.White);
                        }

                        // dot
                        if (Data.Skill[(int)skillNum].Duration > 0 & Data.Skill[(int)skillNum].Interval > 0)
                        {
                            AddDescInfo("DoT: " + Data.Skill[(int)skillNum].Duration / (double)Data.Skill[(int)skillNum].Interval + " tick", Microsoft.Xna.Framework.Color.White);
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

            var equipmentCount = System.Enum.GetValues(typeof(Equipment)).Length;

            if (eqNum < 0L || eqNum >= equipmentCount)
                return;

            if (Core.Data.Player[GameState.MyIndex].Equipment[(int)eqNum] < 0 || Core.Data.Player[GameState.MyIndex].Equipment[(int)eqNum] > Constant.MAX_ITEMS)
                return;

            // show
            if (Conversions.ToBoolean(Core.Data.Player[GameState.MyIndex].Equipment[(int)eqNum]))
            {
                if (Core.Data.Item[(int)Core.Data.Player[GameState.MyIndex].Equipment[(int)eqNum]].BindType > 0)
                    soulBound = true;
                ShowItemDesc(x, y, (long)Core.Data.Player[GameState.MyIndex].Equipment[(int)eqNum]);
            }
        }

        public static void AddDescInfo(string text, Microsoft.Xna.Framework.Color color)
        {
            long count;
            count = Information.UBound(GameState.Description);
            Array.Resize(ref GameState.Description, (int)(count));
            GameState.Description[(int)(count)].Caption = text;
            GameState.Description[(int)(count)].Color = GameClient.ToDrawingColor(color);
        }

        public static void LogoutGame()
        {
            GameState.InMenu = true;
            GameState.InGame = false;

            General.ClearGameData();
            NetworkConfig.DestroyNetwork();
            NetworkConfig.InitNetwork();
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
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkMusic")].Value = Conversions.ToLong(SettingsManager.Instance.Music);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkSound")].Value = Conversions.ToLong(SettingsManager.Instance.Sound);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkAutotile")].Value = Conversions.ToLong(SettingsManager.Instance.Autotile);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "chkFullscreen")].Value = Conversions.ToLong(SettingsManager.Instance.Fullscreen);
                withBlock.Controls[(int)Gui.GetControlIndex("winOptions", "cmbRes")].Value = SettingsManager.Instance.Resolution;
            }
        }

        public static void OpenShop(long shopNum)
        {
            // set globals
            GameState.InShop = (int)shopNum;
            GameState.shopSelectedSlot = 0L;
            GameState.shopSelectedItem = Data.Shop[GameState.InShop].TradeItem[1].Item;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "CheckboxSelling")].Value = 0L;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "CheckboxBuying")].Value = 0L;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "btnSell")].Visible = false;
            Gui.Windows[Gui.GetWindowIndex("winShop")].Controls[(int)Gui.GetControlIndex("winShop", "btnBuy")].Visible = true;
            GameState.shopIsSelling = false;

            // set the current item
            Gui.UpdateShop();

            // show the window
            Gui.ShowWindow(Gui.GetWindowIndex("winShop"));
        }

        public static void UpdatePartyBars()
        {
            long i;
            long pIndex;
            long barWidth;
            long Width;

            // unload it if we're not in a party
            if (Data.MyParty.Leader == 0)
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
                                if (GetPlayerVital((int)pIndex, Core.Vital.Health) > 0 & GetPlayerMaxVital((int)pIndex, Core.Vital.Health) > 0)
                                {
                                    Width = (long)Math.Round(GetPlayerVital((int)pIndex, Core.Vital.Health) / (double)barWidth / (GetPlayerMaxVital((int)pIndex, Core.Vital.Health) / (double)barWidth) * barWidth);
                                    withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picBar_HP" + i)].Width = Width;
                                }
                                else
                                {
                                    withBlock.Controls[(int)Gui.GetControlIndex("winParty", "picBar_HP" + i)].Width = 0L;
                                }
                                // get their spirit
                                if (GetPlayerVital((int)pIndex, Core.Vital.Stamina) > 0 & GetPlayerMaxVital((int)pIndex, Core.Vital.Stamina) > 0)
                                {
                                    Width = (long)Math.Round(GetPlayerVital((int)pIndex, Core.Vital.Stamina) / (double)barWidth / (GetPlayerMaxVital((int)pIndex, Core.Vital.Stamina) / (double)barWidth) * barWidth);
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

            if (MaxWidth <  Width)
            {
                // find out the amount to increase per loop
                barDifference = (long)Math.Round((double)(Width - MaxWidth) / 100L) * 10L;

                // if it's less than 1 then default to 1
                if (barDifference < 0L)
                    barDifference = 0L;
                
                if (Width != MaxWidth && barDifference == 0L)
                {
                    barDifference = Math.Clamp(Width - MaxWidth, 1, Width);
                }
                
                // set the width
                Width -= barDifference;
            }
            else if (MaxWidth > Width)
            {
                // find out the amount to increase per loop
                barDifference = (long)Math.Round((double)(MaxWidth - Width) / 100) * 10L;

                // if it's less than 1 then default to 1
                if (barDifference < 0L)
                    barDifference = 0L;

                if (MaxWidth != Width && barDifference == 0L)
                {
                    barDifference = Math.Clamp(MaxWidth - Width, 1, MaxWidth);
                }

                // set the width
                Width += barDifference;
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
            ConvertMapXRet = (int)Math.Round(x - GameState.TileView.Left - GameState.Camera.Left);
            return ConvertMapXRet;
        }

        public static int ConvertMapY(int y)
        {
            int ConvertMapYRet = default;
            ConvertMapYRet = (int)Math.Round(y - GameState.TileView.Top - GameState.Camera.Top);
            return ConvertMapYRet;
        }

        public static bool IsValidMapPoint(int x, int y)
        {
            if (x < 0)
                return default;
            if (y < 0)
                return default;
            if (x > Data.Map[GetPlayerMap(GameState.MyIndex)].MaxX - 1)
                return default;
            if (y > Data.Map[GetPlayerMap(GameState.MyIndex)].MaxY - 1)
                return default;

            return true;
        }

        public static List<Microsoft.Xna.Framework.Vector2> GetCellsInSquare(int xCenter, int yCenter, int distance)
        {
            int xMin = Math.Max(0, xCenter - distance);
            int xMax = Math.Min(Data.MyMap.MaxX, xCenter + distance);
            int yMin = Math.Max(0, yCenter - distance);
            int yMax = Math.Min(Data.MyMap.MaxY, yCenter + distance);

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
            int xMax = Math.Min(Data.MyMap.MaxX, xCenter + distance);
            int yMin = Math.Max(0, yCenter - distance);
            int yMax = Math.Min(Data.MyMap.MaxY, yCenter + distance);

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

        private static void PostProcessFovQuadrant(ref List<Microsoft.Xna.Framework.Vector2> _inFov, int x, int y, Core.Quadrant quadrant)
        {
            int x1 = x;
            int y1 = y;
            int x2 = x;
            int y2 = y;
            var pos = new Microsoft.Xna.Framework.Vector2(x, y); // Use Vector2i for integer-based coordinates

            // Adjust coordinates based on the quadrant
            switch (quadrant)
            {
                case Core.Quadrant.Northeast:
                    {
                        y1 = y + 1;
                        x2 = x - 1;
                        break;
                    }
                case Core.Quadrant.Southeast:
                    {
                        y1 = y - 1;
                        x2 = x - 1;
                        break;
                    }
                case Core.Quadrant.Southwest:
                    {
                        y1 = y - 1;
                        x2 = x + 1;
                        break;
                    }
                case Core.Quadrant.Northwest:
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
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Quadrant.Southeast);
                        }
                        else if (cell.Y < yOrigin)
                        {
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Quadrant.Northeast);
                        }
                    }
                    else if (cell.X < xOrigin)
                    {
                        if (cell.Y > yOrigin)
                        {
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Quadrant.Southwest);
                        }
                        else if (cell.Y < yOrigin)
                        {
                            PostProcessFovQuadrant(ref inFov, (int)Math.Round(cell.X), (int)Math.Round(cell.Y), Core.Quadrant.Northwest);
                        }
                    }
                }
            }

            return inFov;
        }

        private static bool IsTransparent(int x, int y)
        {
            if (Data.MyMap.Tile[x, y].Type == TileType.Blocked | Data.MyMap.Tile[x, y].Type2 == TileType.Blocked)
            {
                return false;
            }

            return true;
        }

        public static void UpdateCamera()
        {
            float targetCameraX;
            float targetCameraY;

            // Calculate the target camera position based on the player's pixel position  
            targetCameraX = GetPlayerRawX(GameState.MyIndex) - (GameState.ResolutionWidth / 2f);
            targetCameraY = GetPlayerRawY(GameState.MyIndex) - (GameState.ResolutionHeight / 2f);

            // Directly set the camera position to match the player's position for better sync  
            GameState.Camera.Left = (long)Math.Round(targetCameraX);
            GameState.Camera.Top = (long)Math.Round(targetCameraY);

            // Clamp the camera position to the map edges (in pixels)  
            long mapWidth = Data.MyMap.MaxX;
            long mapHeight = Data.MyMap.MaxY;

            GameState.Camera.Left = Math.Max(0, Math.Min(GameState.Camera.Left, mapWidth - GameState.ResolutionWidth));
            GameState.Camera.Top = Math.Max(0, Math.Min(GameState.Camera.Top, mapHeight - GameState.ResolutionHeight));

            // Calculate the visible tile range (in tiles)  
            long StartX = Math.Max(0, Math.Min((long)Math.Floor(GameState.Camera.Left), Data.MyMap.MaxX - 1));
            long StartY = Math.Max(0, Math.Min((long)Math.Floor(GameState.Camera.Top), Data.MyMap.MaxY - 1));
            long EndX = Math.Min(Data.MyMap.MaxX, StartX + (long)(GameState.ResolutionWidth) + 1);
            long EndY = Math.Min(Data.MyMap.MaxY, StartY + (long)(GameState.ResolutionHeight) + 1);

            // Update the tile view  
            ref var withBlock = ref GameState.TileView;
            withBlock.Top = StartY;
            withBlock.Bottom = EndY;
            withBlock.Left = StartX;
            withBlock.Right = EndX;

            // Update the camera bounds  
            ref var withBlock1 = ref GameState.Camera;
            withBlock1.Right = withBlock1.Left;
            withBlock1.Bottom = withBlock1.Top;

            // Optional: Update the map name display  
            UpdateDrawMapName();
        }

    }
}