using System;
using System.Drawing;
using System.Threading.Tasks;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Type;
using static Core.Global.Command;
using static Core.Enum;
using static Core.Packets;

namespace Server
{
    public class EventLogic
    {
        // Enum for event command types (expanded with new commands)
        public enum EventType : byte
        {
            AddText = 1, ShowText, ShowChoices, PlayerVar, PlayerSwitch, SelfSwitch, Condition, ExitProcess,
            ChangeItems, RestoreHP, RestoreSP, LevelUp, ChangeLevel, ChangeSkills, ChangeJob, ChangeSprite,
            ChangeSex, ChangePk, WarpPlayer, SetMoveRoute, PlayAnimation, CustomScript, PlayBgm, FadeoutBgm,
            PlaySound, StopSound, SetAccess, OpenShop, OpenBank, GiveExp, ShowChatBubble, Label, GoToLabel,
            SpawnNPC, FadeIn, FadeOut, FlashWhite, SetFog, SetWeather, SetTint, Wait, ShowPicture, HidePicture,
            WaitMovement, HoldPlayer, ReleasePlayer,
            // New command types
            TeleportEvent = 44, ChangeGraphic = 45, SetVariableFromStat = 46, PlayAnimationSequence = 47,
            EventInteraction = 48
        }

        // Constants for readability
        private const int MaxEventsPerMap = 100; // Example limit, adjust as needed
        private const int DefaultMoveTimer = 1000; // Default timer in milliseconds

        #region Core Methods
        public static void RemoveDeadEvents()
        {
            for (int i = 0; i <= NetworkConfig.Socket.HighIndex; i++)
            {
                if (!IsPlayerValid(i)) continue;

                int mapNum = GetPlayerMap(i);
                var eventMap = Core.Type.TempPlayer[i].EventMap;

                for (int x = 0; x < eventMap.CurrentEvents; x++)
                {
                    if (!IsEventValid(eventMap, x, mapNum)) continue;

                    ref var eventPage = ref eventMap.EventPages[x];
                    int id = eventPage.EventID;
                    int page = eventPage.PageID;

                    if (eventPage.Visible && ShouldDespawnEvent(i, mapNum, id, page))
                    {
                        eventPage.Visible = false;
                        UpdateEventVisibility(i, mapNum, id, eventPage);
                    }
                }
            }
        }

        public static void SpawnNewEvents()
        {
            for (int i = 0; i <= NetworkConfig.Socket.HighIndex; i++)
            {
                if (!IsPlayerValid(i)) continue;

                int mapNum = GetPlayerMap(i);
                var eventMap = Core.Type.TempPlayer[i].EventMap;

                for (int x = 0; x < eventMap.CurrentEvents; x++)
                {
                    if (!IsEventValid(eventMap, x, mapNum)) continue;

                    ref var eventPage = ref eventMap.EventPages[x];
                    int id = eventPage.EventID;

                    if (!eventPage.Visible)
                    {
                        int pageId = DetermineSpawnPage(i, mapNum, id);
                        if (pageId >= 0)
                        {
                            SpawnEvent(i, mapNum, id, pageId, ref eventPage);
                        }
                    }
                }
            }
        }

        public static void ProcessEventMovement()
        {
            for (int i = 0; i < Core.Constant.MAX_MAPS; i++)
            {
                if (!PlayersOnMap[i] || Event.TempEventMap[i].EventCount <= 0) continue;

                for (int x = 0; x < Event.TempEventMap[i].EventCount; x++)
                {
                    ref var globalEvent = ref Event.TempEventMap[i].Event[x];
                    if (globalEvent.Active > 0 && globalEvent.MoveTimer <= General.GetTimeMs())
                    {
                        ProcessMovement(i, x, true, ref globalEvent);
                    }
                }
            }
        }

        public static void ProcessLocalEventMovement()
        {
            for (int i = 0; i <= NetworkConfig.Socket.HighIndex; i++)
            {
                if (!IsPlayerValid(i)) continue;

                var eventMap = Core.Type.TempPlayer[i].EventMap;
                for (int x = 0; x < eventMap.CurrentEvents; x++)
                {
                    ref var localEvent = ref eventMap.EventPages[x];
                    if (localEvent.Visible && localEvent.MoveTimer <= General.GetTimeMs())
                    {
                        ProcessMovement(i, x, false, ref localEvent);
                    }
                }
            }
        }

        public static void ProcessEventCommands()
        {
            for (int i = 0; i <= NetworkConfig.Socket.HighIndex; i++)
            {
                if (!IsPlayerValid(i) || Core.Type.TempPlayer[i].EventProcessingCount <= 0) continue;

                var eventProcessing = Core.Type.TempPlayer[i].EventProcessing;
                for (int x = 0; x < eventProcessing.Length; x++)
                {
                    ref var process = ref eventProcessing[x];
                    if (process.Active == 1 && process.ActionTimer <= General.GetTimeMs() && process.WaitingForResponse == 0)
                    {
                        ExecuteCommand(i, x, ref process);
                    }
                }
            }
        }

        public static void UpdateEventLogic()
        {
            RemoveDeadEvents();
            SpawnNewEvents();
            ProcessEventMovement();
            ProcessLocalEventMovement();
            ProcessEventCommands();
        }
        #endregion

        #region Helper Methods
        private static bool IsPlayerValid(int index)
        {
            return index >= 0 && index <= NetworkConfig.Socket.HighIndex &&
                   Core.Type.TempPlayer[index].EventMap.CurrentEvents > 0 &&
                   !Core.Type.TempPlayer[index].GettingMap;
        }

        private static bool IsEventValid(Core.Type.EventMapStruct eventMap, int index, int mapNum)
        {
            return index < eventMap.EventPages.Length &&
                   eventMap.EventPages[index].EventID <= Map[mapNum].Event.Length;
        }

        private static bool ShouldDespawnEvent(int playerIndex, int mapNum, int eventID, int pageID)
        {
            var eventData = Map[mapNum].Event[eventID];
            var page = eventData.Pages[pageID];
            return (page.ChkHasItem == 1 && Player.HasItem(playerIndex, page.HasItemIndex) == 0) ||
                   (page.ChkSelfSwitch == 1 && !CheckSelfSwitch(eventID, eventData, page)) ||
                   (page.ChkVariable == 1 && !CheckVariable(playerIndex, page)) ||
                   (page.ChkSwitch == 1 && !CheckSwitch(playerIndex, page));
        }

        private static void UpdateEventVisibility(int playerIndex, int mapNum, int eventId, Core.Type.MapEventStruct eventPage)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
            buffer.WriteInt32(eventId);
            WriteEventData(buffer, mapNum, eventId, eventPage);
            NetworkConfig.Socket.SendDataTo(playerIndex, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        private static int DetermineSpawnPage(int playerIndex, int mapNum, int eventID)
        {
            for (int z = 0; z < Map[mapNum].Event[eventID].PageCount; z++)
            {
                var eventData = Map[mapNum].Event[eventID];
                var page = Map[mapNum].Event[eventID].Pages[z];
                bool shouldSpawn = true;

                if (page.ChkHasItem == 1 && Player.HasItem(playerIndex, page.HasItemIndex) == 0) shouldSpawn = false;
                if (page.ChkSelfSwitch == 1 && !CheckSelfSwitch(eventID, eventData, page)) shouldSpawn = false;
                if (page.ChkVariable == 1 && !CheckVariable(playerIndex, page)) shouldSpawn = false;
                if (page.ChkSwitch == 1 && !CheckSwitch(playerIndex, page)) shouldSpawn = false;

                if (shouldSpawn) return z;
            }
            return -1;
        }

        private static void SpawnEvent(int playerIndex, int mapNum, int eventId, int pageId, ref Core.Type.MapEventStruct eventPage)
        {
            eventPage = new Core.Type.MapEventStruct
            {
                EventID = eventId,
                PageID = pageId,
                X = Map[mapNum].Event[eventId].X,
                Y = Map[mapNum].Event[eventId].Y,
                Visible = true,
                // Initialize other properties as needed
            };
            ConfigureEventProperties(ref eventPage, Map[mapNum].Event[eventId].Pages[pageId]);
            UpdateEventVisibility(playerIndex, mapNum, eventId, eventPage);
        }

        private static void ProcessMovement(int index, int eventId, bool isGlobal, ref dynamic eventRef)
        {
            switch (eventRef.MoveType)
            {
                case 1: // Random movement
                    int direction = (int)Math.Round(General.GetRandom.NextDouble(0d, 3d));
                    MoveEventRandomly(index, eventId, direction, isGlobal, ref eventRef);
                    break;
                case 2: // Move route
                    ProcessMoveRoute(index, eventId, isGlobal, ref eventRef);
                    break;
            }
            UpdateMoveTimer(ref eventRef);
        }

        private static void ExecuteCommand(int playerIndex, int eventIndex, ref EventProcessing process)
        {
            int mapNum = GetPlayerMap(playerIndex);
            var command = Map[mapNum].Event[process.EventID].Pages[process.PageID]
                .CommandList[process.CurList].Commands[process.CurSlot];

            switch ((EventType)command.Index)
            {
                // Existing commands (simplified)
                case EventType.ShowText:
                    HandleShowText(playerIndex, mapNum, process, command);
                    break;
                case EventType.WarpPlayer:
                    Player.PlayerWarp(playerIndex, command.Data1, command.Data2, command.Data3);
                    break;

                // New commands
                case EventType.TeleportEvent:
                    TeleportEvent(playerIndex, mapNum, process.EventID, command.Data1, command.Data2, command.Data3);
                    break;
                case EventType.ChangeGraphic:
                    ChangeEventGraphic(playerIndex, mapNum, process.EventID, command);
                    break;
                case EventType.SetVariableFromStat:
                    SetVariableFromStat(playerIndex, command.Data1, command.Data2);
                    break;
                case EventType.PlayAnimationSequence:
                    PlayAnimationSequence(playerIndex, mapNum, process.EventID, command.Data1);
                    break;
                case EventType.EventInteraction:
                    TriggerEventInteraction(playerIndex, mapNum, process.EventID, command.Data1);
                    break;
            }

            process.CurSlot++;
            if (process.CurSlot >= Map[mapNum].Event[process.EventID].Pages[process.PageID]
                .CommandList[process.CurList].CommandCount)
            {
                process.Active = 0; // End processing
            }
        }
        #endregion

        #region New Command Implementations
        private static void TeleportEvent(int playerIndex, int mapNum, int eventId, int targetMap, int targetX, int targetY)
        {
            if (Map[mapNum].Event[eventId].Globals == 1)
            {
                Event.TempEventMap[mapNum].Event[eventId].X = targetX;
                Event.TempEventMap[mapNum].Event[eventId].Y = targetY;
                SendEventUpdateToMap(mapNum, eventId);
            }
            else
            {
                Core.Type.TempPlayer[playerIndex].EventMap.EventPages[eventId].X = targetX;
                Core.Type.TempPlayer[playerIndex].EventMap.EventPages[eventId].Y = targetY;
                SendEventUpdateToPlayer(playerIndex, eventId);
            }
        }

        private static void ChangeEventGraphic(int playerIndex, int mapNum, int eventId, CommandInstance command)
        {
            if (Map[mapNum].Event[eventId].Globals == 1)
            {
                ref var globalEvent = ref Event.TempEventMap[mapNum].Event[eventId];
                UpdateGraphic(ref globalEvent, command);
                SendEventGraphicUpdateToMap(mapNum, eventId);
            }
            else
            {
                ref var localEvent = ref Core.Type.TempPlayer[playerIndex].EventMap.EventPages[eventId];
                UpdateGraphic(ref localEvent, command);
                SendEventGraphicUpdateToPlayer(playerIndex, eventId);
            }
        }

        private static void SetVariableFromStat(int playerIndex, int varIndex, int statType)
        {
            switch (statType)
            {
                case 0: // HP
                    Core.Type.Player[playerIndex].Variables[varIndex] = GetPlayerVital(playerIndex, VitalType.HP);
                    break;
                case 1: // Level
                    Core.Type.Player[playerIndex].Variables[varIndex] = GetPlayerLevel(playerIndex);
                    break;
            }
        }

        private static void PlayAnimationSequence(int playerIndex, int mapNum, int eventId, int sequenceId)
        {
            // Placeholder for animation sequence logic
            Animation.SendAnimation(mapNum, sequenceId, Core.Type.TempPlayer[playerIndex].EventMap.EventPages[eventId].X,
                Core.Type.TempPlayer[playerIndex].EventMap.EventPages[eventId].Y);
        }

        private static void TriggerEventInteraction(int playerIndex, int mapNum, int eventId, int targetEventId)
        {
            // Placeholder for event-to-event interaction logic
            // Example: Trigger another event's command list
        }
        #endregion

        #region Utility Methods
        private static void UpdateGraphic(ref dynamic eventRef, CommandInstance command)
        {
            eventRef.GraphicType = (byte)command.Data1;
            eventRef.Graphic = command.Data2;
            eventRef.GraphicX = command.Data3;
            eventRef.GraphicY = command.Data4;
            eventRef.GraphicX2 = command.Data5;
            eventRef.GraphicY2 = command.Data6;
        }

        private static void SendEventUpdateToMap(int mapNum, int eventId)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
            buffer.WriteInt32(eventId);
            WriteEventData(buffer, mapNum, eventId, Event.TempEventMap[mapNum].Event[eventId]);
            NetworkConfig.SendDataToMap(mapNum, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        private static void SendEventGraphicUpdateToPlayer(int playerIndex, int eventId)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
            buffer.WriteInt32(eventId);
            WriteEventData(buffer, GetPlayerMap(playerIndex), eventId, Core.Type.TempPlayer[playerIndex].EventMap.EventPages[eventId]);
            NetworkConfig.Socket.SendDataTo(playerIndex, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        private static void WriteEventData(ByteStream buffer, int mapNum, int eventId, dynamic eventData)
        {
            buffer.WriteString(Map[mapNum].Event[eventId].Name);
            buffer.WriteInt32(eventData.Dir);
            buffer.WriteByte(eventData.GraphicType);
            buffer.WriteInt32(eventData.Graphic);
            buffer.WriteInt32(eventData.GraphicX);
            buffer.WriteInt32(eventData.GraphicX2);
            buffer.WriteInt32(eventData.GraphicY);
            buffer.WriteInt32(eventData.GraphicY2);
            buffer.WriteInt32(eventData.MovementSpeed);
            buffer.WriteInt32(eventData.X);
            buffer.WriteInt32(eventData.Y);
            buffer.WriteByte(eventData.Position);
            buffer.WriteBoolean(eventData.Visible);
            buffer.WriteInt32(Map[mapNum].Event[eventId].Pages[eventData.PageID].WalkAnim);
            buffer.WriteInt32(Map[mapNum].Event[eventId].Pages[eventData.PageID].DirFix);
            buffer.WriteInt32(Map[mapNum].Event[eventId].Pages[eventData.PageID].WalkThrough);
            buffer.WriteInt32(Map[mapNum].Event[eventId].Pages[eventData.PageID].ShowName);
        }

        private static bool CheckSwitch(int playerIndex, EventPageStruct page)
        {
            return Core.Type.Player[playerIndex].Switches[page.SwitchIndex] == page.SwitchCompare;
        }

        private static bool CheckSelfSwitch(int eventIndex, EventStruct eventData, EventPageStruct page)
        {
            return eventData.SelfSwitches[eventIndex] == page.SwitchCompare;
        }

        private static bool CheckVariable(int playerIndex, EventPageStruct page)
        {
            int playerVariable = Core.Type.Player[playerIndex].Variables[page.VariableIndex];
            switch (page.VariableCondition)
            {
                case 0: // Equal to
                    return playerVariable == page.VariableCompare;
                case 1: // Greater than or equal to
                    return playerVariable >= page.VariableCompare;
                case 2: // Less than or equal to
                    return playerVariable <= page.VariableCompare;
                case 3: // Greater than
                    return playerVariable > page.VariableCompare;
                case 4: // Less than
                    return playerVariable < page.VariableCompare;
                default:
                    return false;
            }
        }
        #endregion
    }
}
