﻿using System;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Type;

namespace Client
{

    public class Event
    {

        #region Globals

        // Temp event storage
        public static Core.Type.EventStruct TmpEvent;

        public static bool IsEdit;

        public static int CurPageNum;
        public static int CurCommand;
        public static int GraphicSelX;
        public static int GraphicSelY;
        public static int GraphicSelX2;
        public static int GraphicSelY2;

        public static int EventTileX;
        public static int EventTileY;

        public static int EditorEvent;

        public static int GraphicSelType;
        public static int TempMoveRouteCount;
        public static Core.Type.MoveRouteStruct[] TempMoveRoute;
        public static bool IsMoveRouteCommand;
        public static int[] ListOfEvents;

        public static int EventReplyId;
        public static int EventReplyPage;
        public static int EventChatFace;

        public static int RenameType;
        public static int RenameIndex;
        public static int EventChatTimer;

        public static bool EventChat;
        public static string EventText;
        public static bool ShowEventLbl;
        public static string[] EventChoices = new string[Core.Constant.MAX_EVENT_CHOICES];
        public static bool[] EventChoiceVisible = new bool[Core.Constant.MAX_EVENT_CHOICES];
        public static int EventChatType;
        public static int AnotherChat;

        // constants
        public static string[] Switches = new string[Constant.MAX_SWITCHES];
        public static string[] Variables = new string[Constant.NAX_VARIABLES];

        public static bool EventCopy;
        public static bool EventPaste;
        public static Core.Type.EventListStruct[] EventList;
        public static Core.Type.EventStruct CopyEvent;
        public static Core.Type.EventPageStruct CopyEventPage;

        public static bool InEvent;
        public static bool HoldPlayer;

        public static Core.Type.PictureStruct Picture;

        #endregion
        
        #region Incoming Packets

        public static void Packet_SpawnEvent(ref byte[] data)
        {
            int id;
            var buffer = new ByteStream(data);

            id = buffer.ReadInt32();

            GameState.CurrentEvents = id + 1;
            Array.Resize(ref Core.Type.MapEvents, GameState.CurrentEvents);

            ref var withBlock = ref Core.Type.MapEvents[id];
            withBlock.Name = buffer.ReadString();
            withBlock.Dir = buffer.ReadInt32();
            withBlock.ShowDir = withBlock.Dir;
            withBlock.GraphicType = buffer.ReadByte();
            withBlock.Graphic = buffer.ReadInt32();
            withBlock.GraphicX = buffer.ReadInt32();
            withBlock.GraphicX2 = buffer.ReadInt32();
            withBlock.GraphicY = buffer.ReadInt32();
            withBlock.GraphicY2 = buffer.ReadInt32();
            withBlock.MovementSpeed = buffer.ReadInt32();
            withBlock.Moving = 0;
            withBlock.X = buffer.ReadInt32();
            withBlock.Y = buffer.ReadInt32();
            withBlock.XOffset = 0;
            withBlock.YOffset = 0;
            withBlock.Position = buffer.ReadByte();
            withBlock.Visible = buffer.ReadBoolean();
            withBlock.WalkAnim = buffer.ReadInt32();
            withBlock.DirFix = buffer.ReadInt32();
            withBlock.WalkThrough = buffer.ReadInt32();
            withBlock.ShowName = buffer.ReadInt32();
            

            buffer.Dispose();

        }

        public static void Packet_EventMove(ref byte[] data)
        {
            int id;
            int x;
            int y;
            int dir;
            int showDir;
            int movementSpeed;
            var buffer = new ByteStream(data);

            id = buffer.ReadInt32();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();
            dir = buffer.ReadInt32();
            showDir = buffer.ReadInt32();
            movementSpeed = buffer.ReadInt32();

            if (id > GameState.CurrentEvents)
                return;

            {
                ref var withBlock = ref Core.Type.MapEvents[id];
                withBlock.X = x;
                withBlock.Y = y;
                withBlock.Dir = dir;
                withBlock.XOffset = 0;
                withBlock.YOffset = 0;
                withBlock.Moving = 1;
                withBlock.ShowDir = showDir;
                withBlock.MovementSpeed = movementSpeed;

                switch (dir)
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

        }

        public static void Packet_EventDir(ref byte[] data)
        {
            int i;
            byte dir;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();
            dir = (byte)buffer.ReadInt32();

            if (i > GameState.CurrentEvents)
                return;

            {
                ref var withBlock = ref Core.Type.MapEvents[i];
                withBlock.Dir = dir;
                withBlock.ShowDir = dir;
                withBlock.XOffset = 0;
                withBlock.YOffset = 0;
                withBlock.Moving = 0;
            }

        }

        public static void Packet_SwitchesAndVariables(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);

            for (i = 0; i < Constant.MAX_SWITCHES; i++)
                Switches[i] = buffer.ReadString();

            for (i = 0; i < Constant.NAX_VARIABLES; i++)
                Variables[i] = buffer.ReadString();

            buffer.Dispose();

        }

        public static void Packet_MapEventData(ref byte[] data)
        {
            int i;
            int x;
            int y;
            int z;
            int w;
            var buffer = new ByteStream(data);

            Core.Type.MyMap.EventCount = buffer.ReadInt32();

            if (Core.Type.MyMap.EventCount > 0)
            {
                Core.Type.MyMap.Event = new Core.Type.EventStruct[Core.Type.MyMap.EventCount];
                var loopTo = Core.Type.MyMap.EventCount;
                for (i = 0; i < loopTo; i++)
                {
                    {
                        ref var withBlock = ref Core.Type.MyMap.Event[i];
                        withBlock.Name = buffer.ReadString();
                        withBlock.Globals = buffer.ReadByte();
                        withBlock.X = buffer.ReadInt32();
                        withBlock.Y = buffer.ReadInt32();
                        withBlock.PageCount = buffer.ReadInt32();
                    }

                    if (Core.Type.MyMap.Event[i].PageCount > 0)
                    {
                        Core.Type.MyMap.Event[i].Pages = new Core.Type.EventPageStruct[Core.Type.MyMap.Event[i].PageCount];
                        var loopTo1 = Core.Type.MyMap.Event[i].PageCount;
                        for (x = 0; x < loopTo1; x++)
                        {
                            {
                                ref var withBlock1 = ref Core.Type.MyMap.Event[i].Pages[x];
                                withBlock1.ChkVariable = buffer.ReadInt32();
                                withBlock1.VariableIndex = buffer.ReadInt32();
                                withBlock1.VariableCondition = buffer.ReadInt32();
                                withBlock1.VariableCompare = buffer.ReadInt32();
                                withBlock1.ChkSwitch = buffer.ReadInt32();
                                withBlock1.SwitchIndex = buffer.ReadInt32();
                                withBlock1.SwitchCompare = buffer.ReadInt32();
                                withBlock1.ChkHasItem = buffer.ReadInt32();
                                withBlock1.HasItemIndex = buffer.ReadInt32();
                                withBlock1.HasItemAmount = buffer.ReadInt32();
                                withBlock1.ChkSelfSwitch = buffer.ReadInt32();
                                withBlock1.SelfSwitchIndex = buffer.ReadInt32();
                                withBlock1.SelfSwitchCompare = buffer.ReadInt32();
                                withBlock1.GraphicType = buffer.ReadByte();
                                withBlock1.Graphic = buffer.ReadInt32();
                                withBlock1.GraphicX = buffer.ReadInt32();
                                withBlock1.GraphicY = buffer.ReadInt32();
                                withBlock1.GraphicX2 = buffer.ReadInt32();
                                withBlock1.GraphicY2 = buffer.ReadInt32();

                                withBlock1.MoveType = buffer.ReadByte();
                                withBlock1.MoveSpeed = buffer.ReadByte();
                                withBlock1.MoveFreq = buffer.ReadByte();
                                withBlock1.MoveRouteCount = buffer.ReadInt32();
                                withBlock1.IgnoreMoveRoute = buffer.ReadInt32();
                                withBlock1.RepeatMoveRoute = buffer.ReadInt32();

                                if (withBlock1.MoveRouteCount > 0)
                                {
                                    Core.Type.MyMap.Event[i].Pages[x].MoveRoute = new Core.Type.MoveRouteStruct[withBlock1.MoveRouteCount];
                                    var loopTo2 = withBlock1.MoveRouteCount;
                                    for (y = 0; y < loopTo2; y++)
                                    {
                                        withBlock1.MoveRoute[y].Index = buffer.ReadInt32();
                                        withBlock1.MoveRoute[y].Data1 = buffer.ReadInt32();
                                        withBlock1.MoveRoute[y].Data2 = buffer.ReadInt32();
                                        withBlock1.MoveRoute[y].Data3 = buffer.ReadInt32();
                                        withBlock1.MoveRoute[y].Data4 = buffer.ReadInt32();
                                        withBlock1.MoveRoute[y].Data5 = buffer.ReadInt32();
                                        withBlock1.MoveRoute[y].Data6 = buffer.ReadInt32();
                                    }
                                }

                                withBlock1.WalkAnim = buffer.ReadInt32();
                                withBlock1.DirFix = buffer.ReadInt32();
                                withBlock1.WalkThrough = buffer.ReadInt32();
                                withBlock1.ShowName = buffer.ReadInt32();
                                withBlock1.Trigger = buffer.ReadByte();
                                withBlock1.CommandListCount = buffer.ReadInt32();
                                withBlock1.Position = buffer.ReadByte();
                            }

                            if (Core.Type.MyMap.Event[i].Pages[x].CommandListCount > 0)
                            {
                                Core.Type.MyMap.Event[i].Pages[x].CommandList = new Core.Type.CommandListStruct[Core.Type.MyMap.Event[i].Pages[x].CommandListCount];
                                var loopTo3 = Core.Type.MyMap.Event[i].Pages[x].CommandListCount;
                                for (y = 0; y < loopTo3; y++)
                                {
                                    Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount = buffer.ReadInt32();
                                    Core.Type.MyMap.Event[i].Pages[x].CommandList[y].ParentList = buffer.ReadInt32();
                                    if (Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        Core.Type.MyMap.Event[i].Pages[x].CommandList[y].Commands = new Core.Type.EventCommandStruct[Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount];
                                        var loopTo4 = Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount;
                                        for (z = 0; z < loopTo4; z++)
                                        {
                                            {
                                                ref var withBlock2 = ref Core.Type.MyMap.Event[i].Pages[x].CommandList[y].Commands[z];
                                                withBlock2.Index = buffer.ReadInt32();
                                                withBlock2.Text1 = buffer.ReadString();
                                                withBlock2.Text2 = buffer.ReadString();
                                                withBlock2.Text3 = buffer.ReadString();
                                                withBlock2.Text4 = buffer.ReadString();
                                                withBlock2.Text5 = buffer.ReadString();
                                                withBlock2.Data1 = buffer.ReadInt32();
                                                withBlock2.Data2 = buffer.ReadInt32();
                                                withBlock2.Data3 = buffer.ReadInt32();
                                                withBlock2.Data4 = buffer.ReadInt32();
                                                withBlock2.Data5 = buffer.ReadInt32();
                                                withBlock2.Data6 = buffer.ReadInt32();
                                                withBlock2.ConditionalBranch.CommandList = buffer.ReadInt32();
                                                withBlock2.ConditionalBranch.Condition = buffer.ReadInt32();
                                                withBlock2.ConditionalBranch.Data1 = buffer.ReadInt32();
                                                withBlock2.ConditionalBranch.Data2 = buffer.ReadInt32();
                                                withBlock2.ConditionalBranch.Data3 = buffer.ReadInt32();
                                                withBlock2.ConditionalBranch.ElseCommandList = buffer.ReadInt32();
                                                withBlock2.MoveRouteCount = buffer.ReadInt32();

                                                if (withBlock2.MoveRouteCount > 0)
                                                {
                                                    withBlock2.MoveRoute = new Core.Type.MoveRouteStruct[withBlock2.MoveRouteCount];
                                                    var loopTo5 = withBlock2.MoveRouteCount;
                                                    for (w = 0; w < loopTo5; w++)
                                                    {
                                                        withBlock2.MoveRoute[w].Index = buffer.ReadInt32();
                                                        withBlock2.MoveRoute[w].Data1 = buffer.ReadInt32();
                                                        withBlock2.MoveRoute[w].Data2 = buffer.ReadInt32();
                                                        withBlock2.MoveRoute[w].Data3 = buffer.ReadInt32();
                                                        withBlock2.MoveRoute[w].Data4 = buffer.ReadInt32();
                                                        withBlock2.MoveRoute[w].Data5 = buffer.ReadInt32();
                                                        withBlock2.MoveRoute[w].Data6 = buffer.ReadInt32();
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

            buffer.Dispose();

        }

        public static void Packet_EventChat(ref byte[] data)
        {
            int i;
            int choices;
            var buffer = new ByteStream(data);
            EventReplyId = buffer.ReadInt32();
            EventReplyPage = buffer.ReadInt32();
            EventChatFace = buffer.ReadInt32();
            EventText = buffer.ReadString();
            if (string.IsNullOrEmpty(EventText))
                EventText = " ";
            EventChat = Conversions.ToBoolean(1);
            ShowEventLbl = Conversions.ToBoolean(1);
            choices = buffer.ReadInt32();
            InEvent = Conversions.ToBoolean(1);
            for (i = 0; i < Core.Constant.MAX_EVENT_CHOICES; i++)
            {
                EventChoices[i] = "";
                EventChoiceVisible[i] = Conversions.ToBoolean(0);
            }
            EventChatType = 0;
            if (choices == 0)
            {
            }
            else
            {
                EventChatType = 1;
                var loopTo = choices;
                for (i = 0; i < loopTo; i++)
                {
                    EventChoices[i] = buffer.ReadString();
                    EventChoiceVisible[i] = Conversions.ToBoolean(1);
                }
            }
            AnotherChat = buffer.ReadInt32();

            buffer.Dispose();

        }

        public static void Packet_EventStart(ref byte[] data)
        {
            InEvent = Conversions.ToBoolean(1);
        }

        public static void Packet_EventEnd(ref byte[] data)
        {
            InEvent = Conversions.ToBoolean(0);
        }

        public static void Packet_Picture(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int picIndex;
            int spriteType;
            int xOffset;
            int yOffset;
            int eventid;

            eventid = buffer.ReadInt32();
            picIndex = buffer.ReadByte();

            if (picIndex == 0)
            {
                Picture.Index = 0;
                Picture.EventId = 0;
                Picture.SpriteType = 0;
                Picture.xOffset = 0;
                Picture.yOffset = 0;
                return;
            }

            spriteType = buffer.ReadByte();
            xOffset = buffer.ReadByte();
            yOffset = buffer.ReadByte();

            Picture.Index = (byte)picIndex;
            Picture.EventId = eventid;
            Picture.SpriteType = (byte)spriteType;
            Picture.xOffset = (byte)xOffset;
            Picture.yOffset = (byte)yOffset;

            buffer.Dispose();

        }

        public static void Packet_HidePicture(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            Picture = default;
        }

        public static void Packet_HoldPlayer(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            if (buffer.ReadInt32() == 0)
            {
                HoldPlayer = Conversions.ToBoolean(1);
            }
            else
            {
                HoldPlayer = Conversions.ToBoolean(0);
            }

            buffer.Dispose();

        }

        public static void Packet_PlayBGM(ref byte[] data)
        {
            string music;
            var buffer = new ByteStream(data);

            music = buffer.ReadString();
            Core.Type.MyMap.Music = music;

            buffer.Dispose();
        }

        public static void Packet_FadeOutBGM(ref byte[] data)
        {
            Sound.CurrentMusic = "";
            Sound.FadeOutSwitch = Conversions.ToBoolean(1);
        }

        public static void Packet_PlaySound(ref byte[] data)
        {
            string sound;
            var buffer = new ByteStream(data);
            int x;
            int y;

            sound = buffer.ReadString();
            x = buffer.ReadInt32();
            y = buffer.ReadInt32();

            Sound.PlaySound(sound, x, y);

            buffer.Dispose();
        }

        public static void Packet_StopSound(ref byte[] data)
        {
            Sound.StopSound();
        }

        public static void Packet_SpecialEffect(ref byte[] data)
        {
            int effectType;
            var buffer = new ByteStream(data);
            effectType = buffer.ReadInt32();

            switch (effectType)
            {
                case GameState.EffectTypeFadein:
                    {
                        GameState.UseFade = Conversions.ToBoolean(1);
                        GameState.FadeType = 1;
                        GameState.FadeAmount = 0;
                        break;
                    }
                case GameState.EffectTypeFadeout:
                    {
                        GameState.UseFade = Conversions.ToBoolean(1);
                        GameState.FadeType = 0;
                        GameState.FadeAmount = 255;
                        break;
                    }
                case GameState.EffectTypeFlash:
                    {
                        GameState.FlashTimer = General.GetTickCount() + 150;
                        break;
                    }
                case GameState.EffectTypeFog:
                    {
                        GameState.CurrentFog = buffer.ReadInt32();
                        GameState.CurrentFogSpeed = buffer.ReadInt32();
                        GameState.CurrentFogOpacity = buffer.ReadInt32();
                        break;
                    }
                case GameState.EffectTypeWeather:
                    {
                        GameState.CurrentWeather = buffer.ReadInt32();
                        GameState.CurrentWeatherIntensity = buffer.ReadInt32();
                        break;
                    }
                case GameState.EffectTypeTint:
                    {
                        Core.Type.MyMap.MapTint = Conversions.ToBoolean(1);
                        GameState.CurrentTintR = buffer.ReadInt32();
                        GameState.CurrentTintG = buffer.ReadInt32();
                        GameState.CurrentTintB = buffer.ReadInt32();
                        GameState.CurrentTintA = buffer.ReadInt32();
                        break;
                    }
            }

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        public static void RequestSwitchesAndVariables()
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestSwitchesAndVariables);
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void SendSwitchesAndVariables()
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSwitchesAndVariables);

            for (i = 0; i < Constant.MAX_SWITCHES; i++)
                buffer.WriteString(Switches[i]);

            for (i = 0; i < Constant.NAX_VARIABLES; i++)
                buffer.WriteString(Variables[i]);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        #endregion

        #region Misc

        public static void ProcessEventMovement(int id)
        {
            if (GameState.MyEditorType == (int)Core.Enum.EditorType.Map)
                return;

            if (id >= Core.Type.MyMap.EventCount)
                return;

            if (id >= Core.Type.MapEvents.Length)
                return;

            if (Core.Type.MapEvents[id].Moving == 1)
            {
                switch (Core.Type.MapEvents[id].Dir)
                {
                    case (int)Core.Enum.DirectionType.Up:
                        {
                            Core.Type.MapEvents[id].YOffset = (int)Math.Round(Core.Type.MapEvents[id].YOffset - GameState.ElapsedTime / 1000d * (Core.Type.MapEvents[id].MovementSpeed * GameState.SizeY));
                            if (Core.Type.MapEvents[id].YOffset < 0)
                                Core.Type.MapEvents[id].YOffset = 0;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Down:
                        {
                            Core.Type.MapEvents[id].YOffset = (int)Math.Round(Core.Type.MapEvents[id].YOffset + GameState.ElapsedTime / 1000d * (Core.Type.MapEvents[id].MovementSpeed * GameState.SizeY));
                            if (Core.Type.MapEvents[id].YOffset > 0)
                                Core.Type.MapEvents[id].YOffset = 0;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Left:
                        {
                            Core.Type.MapEvents[id].XOffset = (int)Math.Round(Core.Type.MapEvents[id].XOffset - GameState.ElapsedTime / 1000d * (Core.Type.MapEvents[id].MovementSpeed * GameState.SizeX));
                            if (Core.Type.MapEvents[id].XOffset < 0)
                                Core.Type.MapEvents[id].XOffset = 0;
                            break;
                        }
                    case (int)Core.Enum.DirectionType.Right:
                        {
                            Core.Type.MapEvents[id].XOffset = (int)Math.Round(Core.Type.MapEvents[id].XOffset + GameState.ElapsedTime / 1000d * (Core.Type.MapEvents[id].MovementSpeed * GameState.SizeX));
                            if (Core.Type.MapEvents[id].XOffset > 0)
                                Core.Type.MapEvents[id].XOffset = 0;
                            break;
                        }
                }

                // Check if completed walking over to the next tile
                if (Core.Type.MapEvents[id].Moving > 0)
                {
                    if (Core.Type.MapEvents[id].Dir == (int)Core.Enum.DirectionType.Right | Core.Type.MapEvents[id].Dir == (int)Core.Enum.DirectionType.Down)
                    {
                        if (Core.Type.MapEvents[id].XOffset >= 0 & Core.Type.MapEvents[id].YOffset >= 0)
                        {
                            Core.Type.MapEvents[id].Moving = 0;
                            if (Core.Type.MapEvents[id].Steps == 1)
                            {
                                Core.Type.MapEvents[id].Steps = 3;
                            }
                            else
                            {
                                Core.Type.MapEvents[id].Steps = 1;
                            }
                        }
                    }
                    else if (Core.Type.MapEvents[id].XOffset <= 0 & Core.Type.MapEvents[id].YOffset <= 0)
                    {
                        Core.Type.MapEvents[id].Moving = 0;
                        if (Core.Type.MapEvents[id].Steps == 1)
                        {
                            Core.Type.MapEvents[id].Steps = 3;
                        }
                        else
                        {
                            Core.Type.MapEvents[id].Steps = 1;
                        }
                    }
                }
            }

        }

        public static object GetColorString(int color)
        {
            object GetColorStringRet = default;

            switch (color)
            {
                case 0:
                    {
                        GetColorStringRet = "Black";
                        break;
                    }
                case 1:
                    {
                        GetColorStringRet = "Blue";
                        break;
                    }
                case 2:
                    {
                        GetColorStringRet = "Green";
                        break;
                    }
                case 3:
                    {
                        GetColorStringRet = "Cyan";
                        break;
                    }
                case 4:
                    {
                        GetColorStringRet = "Red";
                        break;
                    }
                case 5:
                    {
                        GetColorStringRet = "Magenta";
                        break;
                    }
                case 6:
                    {
                        GetColorStringRet = "Brown";
                        break;
                    }
                case 7:
                    {
                        GetColorStringRet = "Grey";
                        break;
                    }
                case 8:
                    {
                        GetColorStringRet = "Dark Grey";
                        break;
                    }
                case 9:
                    {
                        GetColorStringRet = "Bright Blue";
                        break;
                    }
                case 10:
                    {
                        GetColorStringRet = "Bright Green";
                        break;
                    }
                case 11:
                    {
                        GetColorStringRet = "Bright Cyan";
                        break;
                    }
                case 12:
                    {
                        GetColorStringRet = "Bright Red";
                        break;
                    }
                case 13:
                    {
                        GetColorStringRet = "Pink";
                        break;
                    }
                case 14:
                    {
                        GetColorStringRet = "Yellow";
                        break;
                    }
                case 15:
                    {
                        GetColorStringRet = "White";
                        break;
                    }

                default:
                    {
                        GetColorStringRet = "Black";
                        break;
                    }
            }

            return GetColorStringRet;

        }

        public static void ClearEventChat()
        {
            int i;

            if (AnotherChat == 1)
            {
                for (i = 0; i < Core.Constant.MAX_EVENT_CHOICES; i++)
                    EventChoiceVisible[i] = Conversions.ToBoolean(0);
                EventText = "";
                EventChatType = 1;
                EventChatTimer = General.GetTickCount() + 100;
            }
            else if (AnotherChat == 2)
            {
                for (i = 0; i < Core.Constant.MAX_EVENT_CHOICES; i++)
                    EventChoiceVisible[i] = Conversions.ToBoolean(0);
                EventText = "";
                EventChatType = 1;
                EventChatTimer = General.GetTickCount() + 100;
            }
            else
            {
                EventChat = Conversions.ToBoolean(0);
            }
        }

        #endregion

    }
}