using System;
using System.Drawing;
using System.Reflection;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Type;
using static Core.Global.Command;
using static Core.Enum;
using static Core.Packets;
using Core.Common;

namespace Server
{

    internal static class EventLogic
    {

        internal static void RemoveDeadEvents()
        {
            int i;
            int mapNum;
            int x;
            int id;
            int page;
            int compare;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (Core.Type.TempPlayer[i].EventMap.CurrentEvents > 0 & Core.Type.TempPlayer[i].GettingMap == false)
                {
                    mapNum = GetPlayerMap(i);
                    var loopTo1 = Core.Type.TempPlayer[i].EventMap.CurrentEvents;
                    for (x = 0; x <= (int)loopTo1; x++)
                    {
                        id = Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID;
                        if (id > Core.Type.TempPlayer[i].EventMap.CurrentEvents)
                            break;
                        page = Core.Type.TempPlayer[i].EventMap.EventPages[x].PageID;

                        if (x < id)
                            continue;
                        if (Map[mapNum].Event[id].PageCount >= page)
                        {
                            // See if there is any reason to delete this event....
                            // In other words, go back through conditions and make sure they all check up.
                            if (Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible == true)
                            {
                                if (Map[mapNum].Event[id].Pages[page].ChkHasItem == 1)
                                {
                                    if (Player.HasItem(i, Map[mapNum].Event[id].Pages[page].HasItemIndex) == 0)
                                    {
                                        Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                    }
                                }

                                if (Map[mapNum].Event[id].Pages[page].ChkSelfSwitch == 1)
                                {
                                    if (Map[mapNum].Event[id].Pages[page].SelfSwitchCompare == 0)
                                    {
                                        compare = 0;
                                    }
                                    else
                                    {
                                        compare = 0;
                                    }
                                    if (Map[mapNum].Event[id].Globals == 1)
                                    {
                                        if (Map[mapNum].Event[id].SelfSwitches[Map[mapNum].Event[id].Pages[page].SelfSwitchIndex] != compare)
                                        {
                                            Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                        }
                                    }
                                    else if (Core.Type.TempPlayer[i].EventMap.EventPages[id].SelfSwitches[Map[mapNum].Event[id].Pages[page].SelfSwitchIndex] != compare)
                                    {
                                        Core.Type.TempPlayer[i].EventMap.EventPages[id].Visible = false;
                                    }

                                }

                                if (Map[mapNum].Event[id].Pages[page].ChkVariable == 1)
                                {
                                    switch (Map[mapNum].Event[id].Pages[page].VariableCompare)
                                    {
                                        case 0:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[page].VariableIndex] != Map[mapNum].Event[id].Pages[page].VariableCondition)
                                                {
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                                }

                                                break;
                                            }
                                        case 1:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[page].VariableIndex] < Map[mapNum].Event[id].Pages[page].VariableCondition)
                                                {
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                                }

                                                break;
                                            }
                                        case 2:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[page].VariableIndex] > Map[mapNum].Event[id].Pages[page].VariableCondition)
                                                {
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                                }

                                                break;
                                            }
                                        case 3:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[page].VariableIndex] <= Map[mapNum].Event[id].Pages[page].VariableCondition)
                                                {
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                                }

                                                break;
                                            }
                                        case 4:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[page].VariableIndex] >= Map[mapNum].Event[id].Pages[page].VariableCondition)
                                                {
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                                }

                                                break;
                                            }
                                        case 5:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[page].VariableIndex] == Map[mapNum].Event[id].Pages[page].VariableCondition)
                                                {
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                                }

                                                break;
                                            }
                                    }
                                }

                                if (Map[mapNum].Event[id].Pages[page].ChkSwitch == 1)
                                {
                                    if (Map[mapNum].Event[id].Pages[page].SwitchCompare == 1) // we expect true
                                    {
                                        if (Core.Type.Player[i].Switches[Map[mapNum].Event[id].Pages[page].SwitchIndex] == 0) // we see false so we despawn the event
                                        {
                                            Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                        }
                                    }
                                    else if (Core.Type.Player[i].Switches[Map[mapNum].Event[id].Pages[page].SwitchIndex] == 1) // we expect false and we see true so we despawn the event
                                    {
                                        Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible = false;
                                    }
                                }

                                if (Map[mapNum].Event[id].Globals == 1 & Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible == false)
                                    Event.TempEventMap[mapNum].Event[id].Active = 0;

                                if (Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible == false & id > 0)
                                {
                                    var buffer = new ByteStream(4);
                                    buffer.WriteInt32((int)Packets.ServerPackets.SSpawnEvent);
                                    buffer.WriteInt32(id);
                                    {
                                        var withBlock = Core.Type.TempPlayer[i].EventMap.EventPages[x];
                                        buffer.WriteString(Map[GetPlayerMap(i)].Event[withBlock.EventID].Name);
                                        buffer.WriteInt32(withBlock.Dir);
                                        buffer.WriteByte(withBlock.GraphicType);
                                        buffer.WriteInt32(withBlock.Graphic);
                                        buffer.WriteInt32(withBlock.GraphicX);
                                        buffer.WriteInt32(withBlock.GraphicX2);
                                        buffer.WriteInt32(withBlock.GraphicY);
                                        buffer.WriteInt32(withBlock.GraphicY2);
                                        buffer.WriteInt32(withBlock.MovementSpeed);
                                        buffer.WriteInt32(withBlock.X);
                                        buffer.WriteInt32(withBlock.Y);
                                        buffer.WriteByte(withBlock.Position);
                                        buffer.WriteBoolean(withBlock.Visible);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[page].WalkAnim);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[page].DirFix);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[page].WalkThrough);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[page].ShowName);
                                        buffer.WriteInt32(withBlock.QuestNum);
                                    }

                                    NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);
                                    buffer.Dispose();
                                }
                            }
                        }
                    }
                }
            }

        }

        internal static void SpawnNewEvents()
        {
            int PageID;
            int id;
            int compare;
            int i;
            int mapNum;
            int n;
            int x;
            int z;
            bool spawnevent;
            int p;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (Core.Type.TempPlayer[i].EventMap.CurrentEvents > 0)
                {
                    mapNum = GetPlayerMap(i);
                    var loopTo1 = Core.Type.TempPlayer[i].EventMap.CurrentEvents;
                    for (x = 0; x <= (int)loopTo1; x++)
                    {
                        id = Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID;
                        if (id > 0 & id <= Core.Type.TempPlayer[i].EventMap.CurrentEvents)
                        {
                            PageID = Core.Type.TempPlayer[i].EventMap.EventPages[x].PageID;

                            if (Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible == false)
                                PageID = 0;

                            if (x < id)
                                continue;
                            for (z = Map[mapNum].Event[id].PageCount; z >= 0; z -= 1)
                            {
                                spawnevent = Conversions.ToBoolean(1);

                                if (Map[mapNum].Event[id].Pages[z].ChkHasItem == 1)
                                {
                                    if (Player.HasItem(i, Map[mapNum].Event[id].Pages[z].HasItemIndex) == 0)
                                    {
                                        spawnevent = Conversions.ToBoolean(0);
                                    }
                                }

                                if (Map[mapNum].Event[id].Pages[z].ChkSelfSwitch == 1)
                                {
                                    if (Map[mapNum].Event[id].Pages[z].SelfSwitchCompare == 0)
                                    {
                                        compare = 0;
                                    }
                                    else
                                    {
                                        compare = 0;
                                    }
                                    if (Map[mapNum].Event[id].Globals == 1)
                                    {
                                        if (Map[mapNum].Event[id].SelfSwitches[Map[mapNum].Event[id].Pages[z].SelfSwitchIndex] != compare)
                                        {
                                            spawnevent = Conversions.ToBoolean(0);
                                        }
                                    }
                                    else if (Core.Type.TempPlayer[i].EventMap.EventPages[id].SelfSwitches[Map[mapNum].Event[id].Pages[z].SelfSwitchIndex] != compare)
                                    {
                                        spawnevent = Conversions.ToBoolean(0);
                                    }
                                }

                                if (Map[mapNum].Event[id].Pages[z].ChkVariable == 1)
                                {
                                    switch (Map[mapNum].Event[id].Pages[z].VariableCompare)
                                    {
                                        case 0:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[z].VariableIndex] != Map[mapNum].Event[id].Pages[z].VariableCondition)
                                                {
                                                    spawnevent = Conversions.ToBoolean(0);
                                                }

                                                break;
                                            }
                                        case 1:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[z].VariableIndex] < Map[mapNum].Event[id].Pages[z].VariableCondition)
                                                {
                                                    spawnevent = Conversions.ToBoolean(0);
                                                }

                                                break;
                                            }
                                        case 2:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[z].VariableIndex] > Map[mapNum].Event[id].Pages[z].VariableCondition)
                                                {
                                                    spawnevent = Conversions.ToBoolean(0);
                                                }

                                                break;
                                            }
                                        case 3:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[z].VariableIndex] <= Map[mapNum].Event[id].Pages[z].VariableCondition)
                                                {
                                                    spawnevent = Conversions.ToBoolean(0);
                                                }

                                                break;
                                            }
                                        case 4:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[z].VariableIndex] >= Map[mapNum].Event[id].Pages[z].VariableCondition)
                                                {
                                                    spawnevent = Conversions.ToBoolean(0);
                                                }

                                                break;
                                            }
                                        case 5:
                                            {
                                                if (Core.Type.Player[i].Variables[Map[mapNum].Event[id].Pages[z].VariableIndex] == Map[mapNum].Event[id].Pages[z].VariableCondition)
                                                {
                                                    spawnevent = Conversions.ToBoolean(0);
                                                }

                                                break;
                                            }
                                    }
                                }

                                if (Map[mapNum].Event[id].Pages[z].ChkSwitch == 1)
                                {
                                    if (Map[mapNum].Event[id].Pages[z].SwitchCompare == 0) // we want false
                                    {
                                        if (Core.Type.Player[i].Switches[Map[mapNum].Event[id].Pages[z].SwitchIndex] == 1) // and switch is true
                                        {
                                            spawnevent = Conversions.ToBoolean(0); // do not spawn
                                        }
                                    }
                                    else if (Core.Type.Player[i].Switches[Map[mapNum].Event[id].Pages[z].SwitchIndex] == 0) // else we want true and the switch is false
                                    {
                                        spawnevent = Conversions.ToBoolean(0);
                                    }
                                }

                                if (Conversions.ToInteger(spawnevent) == 1)
                                {
                                    if (Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible == true)
                                    {
                                        if (z <= PageID)
                                        {
                                            spawnevent = Conversions.ToBoolean(0);
                                        }
                                    }
                                }

                                if (Conversions.ToInteger(spawnevent) == 1)
                                {

                                    if (Core.Type.TempPlayer[i].EventProcessingCount > 0)
                                    {
                                        var loopTo2 = Information.UBound(Core.Type.TempPlayer[i].EventProcessing);
                                        for (n = 0; n <= (int)loopTo2; n++)
                                        {
                                            if (Core.Type.TempPlayer[i].EventProcessing[n].EventID == id)
                                            {
                                                Core.Type.TempPlayer[i].EventProcessing[n].Active = 0;
                                            }
                                        }
                                    }

                                    {
                                        var withBlock = Core.Type.TempPlayer[i].EventMap.EventPages[id];
                                        if (Map[mapNum].Event[id].Pages[z].GraphicType == 1)
                                        {
                                            switch (Map[mapNum].Event[id].Pages[z].GraphicY)
                                            {
                                                case 0:
                                                    {
                                                        withBlock.Dir = (int)DirectionType.Down;
                                                        break;
                                                    }
                                                case 1:
                                                    {
                                                        withBlock.Dir = (int)DirectionType.Left;
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        withBlock.Dir = (int)DirectionType.Right;
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        withBlock.Dir = (int)DirectionType.Up;
                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            withBlock.Dir = 0;
                                        }
                                        withBlock.Graphic = Map[mapNum].Event[id].Pages[z].Graphic;
                                        withBlock.GraphicType = Map[mapNum].Event[id].Pages[z].GraphicType;
                                        withBlock.GraphicX = Map[mapNum].Event[id].Pages[z].GraphicX;
                                        withBlock.GraphicY = Map[mapNum].Event[id].Pages[z].GraphicY;
                                        withBlock.GraphicX2 = Map[mapNum].Event[id].Pages[z].GraphicX2;
                                        withBlock.GraphicY2 = Map[mapNum].Event[id].Pages[z].GraphicY2;
                                        switch (Map[mapNum].Event[id].Pages[z].MoveSpeed)
                                        {
                                            case 0:
                                                {
                                                    withBlock.MovementSpeed = 2;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    withBlock.MovementSpeed = 3;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    withBlock.MovementSpeed = 4;
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    withBlock.MovementSpeed = 6;
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    withBlock.MovementSpeed = 12;
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    withBlock.MovementSpeed = 24;
                                                    break;
                                                }
                                        }
                                        withBlock.Position = Map[mapNum].Event[id].Pages[z].Position;
                                        withBlock.EventID = id;
                                        withBlock.PageID = z;
                                        withBlock.Visible = true;

                                        withBlock.MoveType = Map[mapNum].Event[id].Pages[z].MoveType;
                                        if (withBlock.MoveType == 2)
                                        {
                                            withBlock.MoveRouteCount = Map[mapNum].Event[id].Pages[z].MoveRouteCount;
                                            if (withBlock.MoveRouteCount > 0)
                                            {
                                                ;

                                                if (Map[mapNum].Event[id].Pages[z].MoveRouteCount > 0)
                                                {
                                                    Array.Resize(ref Core.Type.TempPlayer[i].EventMap.EventPages[i].MoveRoute, Map[mapNum].Event[id].Pages[z].MoveRouteCount);
                                                    for (p = 0; p < Map[mapNum].Event[id].Pages[z].MoveRouteCount; p++)
                                                    {
                                                        Core.Type.TempPlayer[i].EventMap.EventPages[i].MoveRoute[p] = Map[mapNum].Event[id].Pages[z].MoveRoute[p];
                                                    }
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[i].MoveRouteComplete = 0;
                                                }
                                                else
                                                {
                                                    Core.Type.TempPlayer[i].EventMap.EventPages[i].MoveRouteComplete = 0;
                                                }

                                                var loopTo3 = Map[mapNum].Event[id].Pages[z].MoveRouteCount;
                                                for (p = 0; p <= (int)loopTo3; p++)
                                                    withBlock.MoveRoute[p] = Map[mapNum].Event[id].Pages[z].MoveRoute[p];
                                                withBlock.MoveRouteComplete = 0;
                                            }
                                            else
                                            {
                                                withBlock.MoveRouteComplete = 0;
                                            }
                                        }
                                        else
                                        {
                                            withBlock.MoveRouteComplete = 0;
                                        }

                                        withBlock.RepeatMoveRoute = Map[mapNum].Event[id].Pages[z].RepeatMoveRoute;
                                        withBlock.IgnoreIfCannotMove = Map[mapNum].Event[id].Pages[z].IgnoreMoveRoute;

                                        withBlock.MoveFreq = Map[mapNum].Event[id].Pages[z].MoveFreq;
                                        withBlock.MoveSpeed = Map[mapNum].Event[id].Pages[z].MoveSpeed;

                                        withBlock.WalkThrough = Map[mapNum].Event[id].Pages[z].WalkThrough;
                                        withBlock.ShowName = Map[mapNum].Event[id].Pages[z].ShowName;
                                        withBlock.WalkingAnim = Map[mapNum].Event[id].Pages[z].WalkAnim;
                                        withBlock.FixedDir = Map[mapNum].Event[id].Pages[z].DirFix;

                                    }

                                    if (Map[mapNum].Event[id].Globals == 1)
                                    {
                                        if (spawnevent)
                                        {
                                            Event.TempEventMap[mapNum].Event[id].Active = z;
                                            Event.TempEventMap[mapNum].Event[id].Position = Map[mapNum].Event[id].Pages[z].Position;
                                        }
                                    }

                                    var buffer = new ByteStream(4);
                                    buffer.WriteInt32((int) ServerPackets.SSpawnEvent);
                                    buffer.WriteInt32(id);
                                    {
                                        var withBlock1 = Core.Type.TempPlayer[i].EventMap.EventPages[x];
                                        buffer.WriteString(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Name);
                                        buffer.WriteInt32(withBlock1.Dir);
                                        buffer.WriteByte(withBlock1.GraphicType);
                                        buffer.WriteInt32(withBlock1.Graphic);
                                        buffer.WriteInt32(withBlock1.GraphicX);
                                        buffer.WriteInt32(withBlock1.GraphicX2);
                                        buffer.WriteInt32(withBlock1.GraphicY);
                                        buffer.WriteInt32(withBlock1.GraphicY2);
                                        buffer.WriteInt32(withBlock1.MovementSpeed);
                                        buffer.WriteInt32(withBlock1.X);
                                        buffer.WriteInt32(withBlock1.Y);
                                        buffer.WriteByte(withBlock1.Position);
                                        buffer.WriteBoolean(withBlock1.Visible);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[z].WalkAnim);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[z].DirFix);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[z].WalkThrough);
                                        buffer.WriteInt32(Map[mapNum].Event[id].Pages[z].ShowName);
                                        buffer.WriteInt32(withBlock1.QuestNum);
                                    }
                                    NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);

                                    buffer.Dispose();
                                    z = 0;
                                }
                            }
                        }
                    }
                }
            }

        }

        internal static void ProcessEventMovement()
        {
            int rand;
            int x;
            int i;
            int playerID;
            int EventID;
            int WalkThrough;
            bool IsGlobal;
            int mapNum;
            var actualmovespeed = default(int);
            ByteStream buffer;
            var z = default(int);
            var sendupdate = default(bool);
            var donotprocessmoveroute = default(bool);
            int pageNum;

            // Process Movement if needed for each player/each map/each event....
            var loopTo = Core.Constant.MAX_MAPS - 1;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (PlayersOnMap[i])
                {
                    // Manage Global Events First, then all the others.....
                    if (Event.TempEventMap[i].EventCount > 0)
                    {
                        var loopTo1 = Event.TempEventMap[i].EventCount;
                        for (x = 0; x <= (int)loopTo1; x++)
                        {
                            if (Event.TempEventMap[i].Event[x].Active > 0)
                            {
                                pageNum = 0;
                                if (Event.TempEventMap[i].Event[x].MoveTimer <= General.GetTimeMs())
                                {
                                    // Real event! Lets process it!
                                    switch (Event.TempEventMap[i].Event[x].MoveType)
                                    {
                                        case 0:
                                            {
                                                break;
                                            }
                                        // Nothing, fixed position
                                        case 1: // Random, move randomly if possible...
                                            {
                                                rand = (int)Math.Round(General.Random.NextDouble(0d, 3d));
                                                if (Event.CanEventMove(0, i, Event.TempEventMap[i].Event[x].X, Event.TempEventMap[i].Event[x].Y, x, Event.TempEventMap[i].Event[x].WalkThrough, (byte)rand, true))
                                                {
                                                    switch (Event.TempEventMap[i].Event[x].MoveSpeed)
                                                    {
                                                        case 0:
                                                            {
                                                                Event.EventMove(0, i, x, rand, 2, true);
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                Event.EventMove(0, i, x, rand, 3, true);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Event.EventMove(0, i, x, rand, 4, true);
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Event.EventMove(0, i, x, rand, 6, true);
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                Event.EventMove(0, i, x, rand, 12, true);
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                Event.EventMove(0, i, x, rand, 24, true);
                                                                break;
                                                            }
                                                    }
                                                }
                                                else
                                                {
                                                    Event.EventDir(0, i, x, rand, true);
                                                }

                                                break;
                                            }
                                        case 2: // Move Route
                                            {
                                                {
                                                    var withBlock = Event.TempEventMap[i].Event[x];
                                                    IsGlobal = Conversions.ToBoolean(1);
                                                    mapNum = i;
                                                    playerID = 0;
                                                    EventID = x;
                                                    WalkThrough = Event.TempEventMap[i].Event[x].WalkThrough;
                                                    if (withBlock.MoveRouteCount > 0)
                                                    {
                                                        if (withBlock.MoveRouteStep >= withBlock.MoveRouteCount & withBlock.RepeatMoveRoute == 1)
                                                        {
                                                            withBlock.MoveRouteStep = 0;
                                                            withBlock.MoveRouteComplete = 0;
                                                        }
                                                        else if (withBlock.MoveRouteStep >= withBlock.MoveRouteCount & withBlock.RepeatMoveRoute == 0)
                                                        {
                                                            donotprocessmoveroute = Conversions.ToBoolean(1);
                                                            withBlock.MoveRouteComplete = 0;
                                                        }
                                                        else
                                                        {
                                                            withBlock.MoveRouteComplete = 0;
                                                        }
                                                        if (Conversions.ToInteger(donotprocessmoveroute) == 0)
                                                        {
                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep + 1;
                                                            switch (withBlock.MoveSpeed)
                                                            {
                                                                case 0:
                                                                    {
                                                                        actualmovespeed = 2;
                                                                        break;
                                                                    }
                                                                case 1:
                                                                    {
                                                                        actualmovespeed = 3;
                                                                        break;
                                                                    }
                                                                case 2:
                                                                    {
                                                                        actualmovespeed = 4;
                                                                        break;
                                                                    }
                                                                case 3:
                                                                    {
                                                                        actualmovespeed = 6;
                                                                        break;
                                                                    }
                                                                case 4:
                                                                    {
                                                                        actualmovespeed = 12;
                                                                        break;
                                                                    }
                                                                case 5:
                                                                    {
                                                                        actualmovespeed = 24;
                                                                        break;
                                                                    }
                                                            }
                                                            switch (withBlock.MoveRoute[withBlock.MoveRouteStep].Index)
                                                            {
                                                                case 1:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Up, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Up, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 2:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Down, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Down, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 3:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Left, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Left, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 4:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Right, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Right, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 5:
                                                                    {
                                                                        z = (int)Math.Round(General.Random.NextDouble(0d, 3d));
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 6:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            if (Conversions.ToInteger(Event.IsOneBlockAway(withBlock.X, withBlock.Y, GetPlayerX(playerID), GetPlayerY(playerID))) == 1)
                                                                            {
                                                                                Event.EventDir(playerID, GetPlayerMap(playerID), EventID, Event.GetDirToPlayer(playerID, GetPlayerMap(playerID), EventID), false);
                                                                                if (withBlock.IgnoreIfCannotMove == 0)
                                                                                {
                                                                                    withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                z = Event.CanEventMoveTowardsPlayer(playerID, mapNum, EventID);
                                                                                if (z >= 4)
                                                                                {
                                                                                    // No
                                                                                    if (withBlock.IgnoreIfCannotMove == 0)
                                                                                    {
                                                                                        withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                                    }
                                                                                }
                                                                                // i is the direct, lets go...
                                                                                else if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                                {
                                                                                    Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                                }
                                                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                                                {
                                                                                    withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                                }
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case 7:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            z = Event.CanEventMoveAwayFromPlayer(playerID, mapNum, EventID);
                                                                            if (z >= 5)
                                                                            {
                                                                            }
                                                                            // No
                                                                            // i is the direct, lets go...
                                                                            else if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                            {
                                                                                Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                            }
                                                                            else if (withBlock.IgnoreIfCannotMove == 0)
                                                                            {
                                                                                withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case 8:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)withBlock.Dir, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, withBlock.Dir, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 9:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (int)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 10:
                                                                    {
                                                                        withBlock.MoveTimer = General.GetTimeMs() + 100;
                                                                        break;
                                                                    }
                                                                case 11:
                                                                    {
                                                                        withBlock.MoveTimer = General.GetTimeMs() + 500;
                                                                        break;
                                                                    }
                                                                case 12:
                                                                    {
                                                                        withBlock.MoveTimer = General.GetTimeMs() + 1000;
                                                                        break;
                                                                    }
                                                                case 13:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Up, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 14:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Down, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 15:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Left, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 16:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Right, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 17:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 18:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 19:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (byte)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 20:
                                                                    {
                                                                        z = (int)Math.Round(General.Random.NextDouble(0d, 3d));
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 21:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            z = Event.GetDirToPlayer(playerID, mapNum, EventID);
                                                                            Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        }

                                                                        break;
                                                                    }
                                                                case 22:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            z = Event.GetDirAwayFromPlayer(playerID, mapNum, EventID);
                                                                            Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        }

                                                                        break;
                                                                    }
                                                                case 23:
                                                                    {
                                                                        withBlock.MoveSpeed = 0;
                                                                        break;
                                                                    }
                                                                case 24:
                                                                    {
                                                                        withBlock.MoveSpeed = 0;
                                                                        break;
                                                                    }
                                                                case 25:
                                                                    {
                                                                        withBlock.MoveSpeed = 2;
                                                                        break;
                                                                    }
                                                                case 26:
                                                                    {
                                                                        withBlock.MoveSpeed = 3;
                                                                        break;
                                                                    }
                                                                case 27:
                                                                    {
                                                                        withBlock.MoveSpeed = 4;
                                                                        break;
                                                                    }
                                                                case 28:
                                                                    {
                                                                        withBlock.MoveSpeed = 5;
                                                                        break;
                                                                    }
                                                                case 29:
                                                                    {
                                                                        withBlock.MoveFreq = 0;
                                                                        break;
                                                                    }
                                                                case 30:
                                                                    {
                                                                        withBlock.MoveFreq = 0;
                                                                        break;
                                                                    }
                                                                case 31:
                                                                    {
                                                                        withBlock.MoveFreq = 2;
                                                                        break;
                                                                    }
                                                                case 32:
                                                                    {
                                                                        withBlock.MoveFreq = 3;
                                                                        break;
                                                                    }
                                                                case 33:
                                                                    {
                                                                        withBlock.MoveFreq = 4;
                                                                        break;
                                                                    }
                                                                case 34:
                                                                    {
                                                                        withBlock.WalkingAnim = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 35:
                                                                    {
                                                                        withBlock.WalkingAnim = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 36:
                                                                    {
                                                                        withBlock.FixedDir = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 37:
                                                                    {
                                                                        withBlock.FixedDir = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 38:
                                                                    {
                                                                        withBlock.WalkThrough = 0;
                                                                        break;
                                                                    }
                                                                case 39:
                                                                    {
                                                                        withBlock.WalkThrough = 0;
                                                                        break;
                                                                    }
                                                                case 40:
                                                                    {
                                                                        withBlock.Position = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 41:
                                                                    {
                                                                        withBlock.Position = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 42:
                                                                    {
                                                                        withBlock.Position = 2;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 43:
                                                                    {
                                                                        withBlock.GraphicType = (byte)withBlock.MoveRoute[withBlock.MoveRouteStep].Data1;
                                                                        withBlock.Graphic = withBlock.MoveRoute[withBlock.MoveRouteStep].Data2;
                                                                        withBlock.GraphicX = withBlock.MoveRoute[withBlock.MoveRouteStep].Data3;
                                                                        withBlock.GraphicX2 = withBlock.MoveRoute[withBlock.MoveRouteStep].Data4;
                                                                        withBlock.GraphicY = withBlock.MoveRoute[withBlock.MoveRouteStep].Data5;
                                                                        withBlock.GraphicY2 = withBlock.MoveRoute[withBlock.MoveRouteStep].Data6;
                                                                        if (withBlock.GraphicType == 1)
                                                                        {
                                                                            switch (withBlock.GraphicY)
                                                                            {
                                                                                case 0:
                                                                                    {
                                                                                        withBlock.Dir = (byte)DirectionType.Down;
                                                                                        break;
                                                                                    }
                                                                                case 1:
                                                                                    {
                                                                                        withBlock.Dir = (byte)DirectionType.Left;
                                                                                        break;
                                                                                    }
                                                                                case 2:
                                                                                    {
                                                                                        withBlock.Dir = (byte)DirectionType.Right;
                                                                                        break;
                                                                                    }
                                                                                case 3:
                                                                                    {
                                                                                        withBlock.Dir = (byte)DirectionType.Up;
                                                                                        break;
                                                                                    }
                                                                            }
                                                                        }
                                                                        // Need to Send Update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                            }

                                                            if (sendupdate)
                                                            {
                                                                buffer = new ByteStream(4);
                                                                buffer.WriteInt32((int) ServerPackets.SSpawnEvent);
                                                                buffer.WriteInt32(EventID);
                                                                {
                                                                    var withBlock1 = Event.TempEventMap[i].Event[x];
                                                                    buffer.WriteString(Map[i].Event[x].Name);
                                                                    buffer.WriteInt32(withBlock1.Dir);
                                                                    buffer.WriteByte(withBlock1.GraphicType);
                                                                    buffer.WriteInt32(withBlock1.Graphic);
                                                                    buffer.WriteInt32(withBlock1.GraphicX);
                                                                    buffer.WriteInt32(withBlock1.GraphicX2);
                                                                    buffer.WriteInt32(withBlock1.GraphicY);
                                                                    buffer.WriteInt32(withBlock1.GraphicY2);
                                                                    buffer.WriteByte(withBlock1.MoveSpeed);
                                                                    buffer.WriteInt32(withBlock1.X);
                                                                    buffer.WriteInt32(withBlock1.Y);
                                                                    buffer.WriteByte(withBlock1.Position);
                                                                    buffer.WriteInt32(withBlock1.Active);
                                                                    buffer.WriteInt32(withBlock1.WalkingAnim);
                                                                    buffer.WriteInt32(withBlock1.FixedDir);
                                                                    buffer.WriteInt32(withBlock1.WalkThrough);
                                                                    buffer.WriteInt32(withBlock1.ShowName);
                                                                }
                                                                NetworkConfig.SendDataToMap(i, ref buffer.Data, buffer.Head);
                                                                buffer.Dispose();
                                                            }
                                                        }
                                                        donotprocessmoveroute = Conversions.ToBoolean(0);
                                                    }
                                                }

                                                break;
                                            }
                                    }

                                    switch (Event.TempEventMap[i].Event[x].MoveFreq)
                                    {
                                        case 0:
                                            {
                                                Event.TempEventMap[i].Event[x].MoveTimer = General.GetTimeMs() + 4000;
                                                break;
                                            }
                                        case 1:
                                            {
                                                Event.TempEventMap[i].Event[x].MoveTimer = General.GetTimeMs() + 2000;
                                                break;
                                            }
                                        case 2:
                                            {
                                                Event.TempEventMap[i].Event[x].MoveTimer = General.GetTimeMs() + 1000;
                                                break;
                                            }
                                        case 3:
                                            {
                                                Event.TempEventMap[i].Event[x].MoveTimer = General.GetTimeMs() + 500;
                                                break;
                                            }
                                        case 4:
                                            {
                                                Event.TempEventMap[i].Event[x].MoveTimer = General.GetTimeMs() + 250;
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        internal static void ProcessLocalEventMovement()
        {
            int rand;
            int x;
            int i;
            int playerID;
            int EventID;
            int WalkThrough;
            bool IsGlobal;
            int mapNum;
            var actualmovespeed = default(int);
            ByteStream buffer;
            var z = default(int);
            bool sendupdate;
            var donotprocessmoveroute = default(bool);

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
            {
                playerID = i;
                if (Core.Type.TempPlayer[i].EventMap.CurrentEvents > 0)
                {
                    var loopTo1 = Core.Type.TempPlayer[i].EventMap.CurrentEvents;
                    for (x = 0; x <= (int)loopTo1; x++)
                    {
                        if (Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID > Information.UBound(Map[GetPlayerMap(i)].Event))
                            break;
                        if ((int)Map[GetPlayerMap(i)].Event[Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID].Globals == 0)
                        {
                            if (Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible == true)
                            {
                                if (Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveTimer <= General.GetTimeMs())
                                {
                                    // Real event! Lets process it!
                                    switch (Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveType)
                                    {
                                        case 0:
                                            {
                                                break;
                                            }
                                        // Nothing, fixed position
                                        case 1: // Random, move randomly if possible...
                                            {
                                                rand = (int)Math.Round(General.Random.NextDouble(0d, 3d));
                                                playerID = i;
                                                if (Event.CanEventMove(i, GetPlayerMap(i), Core.Type.TempPlayer[i].EventMap.EventPages[x].X, Core.Type.TempPlayer[i].EventMap.EventPages[x].Y, x, Core.Type.TempPlayer[i].EventMap.EventPages[x].WalkThrough, (byte)rand, false))
                                                {
                                                    switch (Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveSpeed)
                                                    {
                                                        case 0:
                                                            {
                                                                Event.EventMove(i, GetPlayerMap(i), x, rand, 2, false);
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                Event.EventMove(i, GetPlayerMap(i), x, rand, 3, false);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Event.EventMove(i, GetPlayerMap(i), x, rand, 4, false);
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Event.EventMove(i, GetPlayerMap(i), x, rand, 6, false);
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                Event.EventMove(i, GetPlayerMap(i), x, rand, 12, false);
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                Event.EventMove(i, GetPlayerMap(i), x, rand, 24, false);
                                                                break;
                                                            }
                                                    }
                                                }
                                                else
                                                {
                                                    Event.EventDir(0, GetPlayerMap(i), x, rand, true);
                                                }

                                                break;
                                            }
                                        case 2: // Move Route
                                            {
                                                {
                                                    var withBlock = Core.Type.TempPlayer[i].EventMap.EventPages[x];
                                                    IsGlobal = Conversions.ToBoolean(0);
                                                    sendupdate = Conversions.ToBoolean(0);
                                                    mapNum = GetPlayerMap(i);
                                                    playerID = i;
                                                    EventID = x;
                                                    WalkThrough = withBlock.WalkThrough;
                                                    if (Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveRouteCount > 0)
                                                    {
                                                        if (Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveRouteStep >= Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveRouteCount & Core.Type.TempPlayer[i].EventMap.EventPages[x].RepeatMoveRoute == 1)
                                                        {
                                                            withBlock.MoveRouteStep = 0;
                                                            withBlock.MoveRouteComplete = 0;
                                                        }
                                                        else if (withBlock.MoveRouteStep >= withBlock.MoveRouteCount & withBlock.RepeatMoveRoute == 0)
                                                        {
                                                            donotprocessmoveroute = Conversions.ToBoolean(1);
                                                            withBlock.MoveRouteComplete = 0;
                                                        }
                                                        else
                                                        {
                                                            withBlock.MoveRouteComplete = 0;
                                                        }
                                                        if (Conversions.ToInteger(donotprocessmoveroute) == 0)
                                                        {

                                                            switch (Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveSpeed)
                                                            {
                                                                case 0:
                                                                    {
                                                                        actualmovespeed = 2;
                                                                        break;
                                                                    }
                                                                case 1:
                                                                    {
                                                                        actualmovespeed = 3;
                                                                        break;
                                                                    }
                                                                case 2:
                                                                    {
                                                                        actualmovespeed = 4;
                                                                        break;
                                                                    }
                                                                case 3:
                                                                    {
                                                                        actualmovespeed = 6;
                                                                        break;
                                                                    }
                                                                case 4:
                                                                    {
                                                                        actualmovespeed = 12;
                                                                        break;
                                                                    }
                                                                case 5:
                                                                    {
                                                                        actualmovespeed = 24;
                                                                        break;
                                                                    }
                                                            }
                                                            Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveRouteStep = Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveRouteStep + 1;
                                                            switch (Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveRoute[Core.Type.TempPlayer[i].EventMap.EventPages[x].MoveRouteStep].Index)
                                                            {
                                                                case 1:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Up, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Up, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (Core.Type.TempPlayer[i].EventMap.EventPages[x].IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 2:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Down, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Down, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 3:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Left, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Left, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 4:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Right, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Right, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 5:
                                                                    {
                                                                        z = (int)Math.Round(General.Random.NextDouble(0d, 3d));
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 6:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            if (Conversions.ToInteger(Event.IsOneBlockAway(withBlock.X, withBlock.Y, GetPlayerX(playerID), GetPlayerY(playerID))) == 1)
                                                                            {
                                                                                Event.EventDir(playerID, GetPlayerMap(playerID), EventID, Event.GetDirToPlayer(playerID, GetPlayerMap(playerID), EventID), false);
                                                                                // Lets do cool stuff!
                                                                                if ((int)Map[GetPlayerMap(playerID)].Event[EventID].Pages[Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].PageID].Trigger == 1)
                                                                                {
                                                                                    if (Map[mapNum].Event[EventID].Pages[Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].PageID].CommandListCount > 0)
                                                                                    {
                                                                                        Core.Type.TempPlayer[playerID].EventProcessing[EventID].Active = 0;
                                                                                        Core.Type.TempPlayer[playerID].EventProcessing[EventID].ActionTimer = General.GetTimeMs();
                                                                                        Core.Type.TempPlayer[playerID].EventProcessing[EventID].CurList = 0;
                                                                                        Core.Type.TempPlayer[playerID].EventProcessing[EventID].CurSlot = 0;
                                                                                        Core.Type.TempPlayer[playerID].EventProcessing[EventID].EventID = EventID;
                                                                                        Core.Type.TempPlayer[playerID].EventProcessing[EventID].PageID = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].PageID;
                                                                                        Core.Type.TempPlayer[playerID].EventProcessing[EventID].WaitingForResponse = 0;
                                                                                        ;

                                                                                    }
                                                                                }
                                                                                if (withBlock.IgnoreIfCannotMove == 0)
                                                                                {
                                                                                    withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                z = Event.CanEventMoveTowardsPlayer(playerID, mapNum, EventID);
                                                                                if (z >= 4)
                                                                                {
                                                                                    // No
                                                                                    if (withBlock.IgnoreIfCannotMove == 0)
                                                                                    {
                                                                                        withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                                    }
                                                                                }
                                                                                // i is the direct, lets go...
                                                                                else if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                                {
                                                                                    Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                                }
                                                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                                                {
                                                                                    withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                                }
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case 7:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            z = Event.CanEventMoveAwayFromPlayer(playerID, mapNum, EventID);
                                                                            if (z >= 5)
                                                                            {
                                                                            }
                                                                            // No
                                                                            // i is the direct, lets go...
                                                                            else if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                            {
                                                                                Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                            }
                                                                            else if (withBlock.IgnoreIfCannotMove == 0)
                                                                            {
                                                                                withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case 8:
                                                                    {
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)withBlock.Dir, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, withBlock.Dir, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 9:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (byte)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                        {
                                                                            Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                        }
                                                                        else if (withBlock.IgnoreIfCannotMove == 0)
                                                                        {
                                                                            withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                                        }

                                                                        break;
                                                                    }
                                                                case 10:
                                                                    {
                                                                        withBlock.MoveTimer = General.GetTimeMs() + 100;
                                                                        break;
                                                                    }
                                                                case 11:
                                                                    {
                                                                        withBlock.MoveTimer = General.GetTimeMs() + 500;
                                                                        break;
                                                                    }
                                                                case 12:
                                                                    {
                                                                        withBlock.MoveTimer = General.GetTimeMs() + 1000;
                                                                        break;
                                                                    }
                                                                case 13:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Up, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 14:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Down, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 15:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Left, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 16:
                                                                    {
                                                                        Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Right, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 17:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 18:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 19:
                                                                    {
                                                                        switch (withBlock.Dir)
                                                                        {
                                                                            case (byte)DirectionType.Up:
                                                                                {
                                                                                    z = (byte)DirectionType.Down;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Right:
                                                                                {
                                                                                    z = (byte)DirectionType.Left;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Left:
                                                                                {
                                                                                    z = (byte)DirectionType.Right;
                                                                                    break;
                                                                                }
                                                                            case (byte)DirectionType.Down:
                                                                                {
                                                                                    z = (byte)DirectionType.Up;
                                                                                    break;
                                                                                }
                                                                        }
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 20:
                                                                    {
                                                                        z = (int)Math.Round(General.Random.NextDouble(0d, 3d));
                                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        break;
                                                                    }
                                                                case 21:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            z = Event.GetDirToPlayer(playerID, mapNum, EventID);
                                                                            Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        }

                                                                        break;
                                                                    }
                                                                case 22:
                                                                    {
                                                                        if (Conversions.ToInteger(IsGlobal) == 0)
                                                                        {
                                                                            z = Event.GetDirAwayFromPlayer(playerID, mapNum, EventID);
                                                                            Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                                        }

                                                                        break;
                                                                    }
                                                                case 23:
                                                                    {
                                                                        withBlock.MoveSpeed = 0;
                                                                        break;
                                                                    }
                                                                case 24:
                                                                    {
                                                                        withBlock.MoveSpeed = 0;
                                                                        break;
                                                                    }
                                                                case 25:
                                                                    {
                                                                        withBlock.MoveSpeed = 2;
                                                                        break;
                                                                    }
                                                                case 26:
                                                                    {
                                                                        withBlock.MoveSpeed = 3;
                                                                        break;
                                                                    }
                                                                case 27:
                                                                    {
                                                                        withBlock.MoveSpeed = 4;
                                                                        break;
                                                                    }
                                                                case 28:
                                                                    {
                                                                        withBlock.MoveSpeed = 5;
                                                                        break;
                                                                    }
                                                                case 29:
                                                                    {
                                                                        withBlock.MoveFreq = 0;
                                                                        break;
                                                                    }
                                                                case 30:
                                                                    {
                                                                        withBlock.MoveFreq = 0;
                                                                        break;
                                                                    }
                                                                case 31:
                                                                    {
                                                                        withBlock.MoveFreq = 2;
                                                                        break;
                                                                    }
                                                                case 32:
                                                                    {
                                                                        withBlock.MoveFreq = 3;
                                                                        break;
                                                                    }
                                                                case 33:
                                                                    {
                                                                        withBlock.MoveFreq = 4;
                                                                        break;
                                                                    }
                                                                case 34:
                                                                    {
                                                                        withBlock.WalkingAnim = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 35:
                                                                    {
                                                                        withBlock.WalkingAnim = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 36:
                                                                    {
                                                                        withBlock.FixedDir = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 37:
                                                                    {
                                                                        withBlock.FixedDir = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 38:
                                                                    {
                                                                        withBlock.WalkThrough = 0;
                                                                        break;
                                                                    }
                                                                case 39:
                                                                    {
                                                                        withBlock.WalkThrough = 0;
                                                                        break;
                                                                    }
                                                                case 40:
                                                                    {
                                                                        withBlock.Position = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 41:
                                                                    {
                                                                        withBlock.Position = 0;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 42:
                                                                    {
                                                                        withBlock.Position = 2;
                                                                        // Need to send update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case 43:
                                                                    {
                                                                        withBlock.GraphicType = (byte)withBlock.MoveRoute[withBlock.MoveRouteStep].Data1;
                                                                        withBlock.GraphicX = withBlock.MoveRoute[withBlock.MoveRouteStep].Data3;
                                                                        withBlock.GraphicX2 = withBlock.MoveRoute[withBlock.MoveRouteStep].Data4;
                                                                        withBlock.GraphicY = withBlock.MoveRoute[withBlock.MoveRouteStep].Data5;
                                                                        withBlock.GraphicY2 = withBlock.MoveRoute[withBlock.MoveRouteStep].Data6;
                                                                        if (withBlock.GraphicType == 1)
                                                                        {
                                                                            switch (withBlock.GraphicY)
                                                                            {
                                                                                case 0:
                                                                                    {
                                                                                        withBlock.Dir = (int)DirectionType.Down;
                                                                                        break;
                                                                                    }
                                                                                case 1:
                                                                                    {
                                                                                        withBlock.Dir = (int)DirectionType.Left;
                                                                                        break;
                                                                                    }
                                                                                case 2:
                                                                                    {
                                                                                        withBlock.Dir = (int)DirectionType.Right;
                                                                                        break;
                                                                                    }
                                                                                case 3:
                                                                                    {
                                                                                        withBlock.Dir = (int)DirectionType.Up;
                                                                                        break;
                                                                                    }
                                                                            }
                                                                        }
                                                                        // Need to Send Update to client
                                                                        sendupdate = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                            }

                                                            if (sendupdate & Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].EventID > 0)
                                                            {
                                                                buffer = new ByteStream(4);
                                                                buffer.WriteInt32((int) ServerPackets.SSpawnEvent);
                                                                buffer.WriteInt32(Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].EventID);
                                                                {
                                                                    var withBlock1 = Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID];
                                                                    buffer.WriteString(Map[GetPlayerMap(playerID)].Event[Core.Type.TempPlayer[playerID].EventMap.EventPages[EventID].EventID].Name);
                                                                    buffer.WriteInt32(withBlock1.Dir);
                                                                    buffer.WriteByte(withBlock1.GraphicType);
                                                                    buffer.WriteInt32(withBlock1.Graphic);
                                                                    buffer.WriteInt32(withBlock1.GraphicX);
                                                                    buffer.WriteInt32(withBlock1.GraphicX2);
                                                                    buffer.WriteInt32(withBlock1.GraphicY);
                                                                    buffer.WriteInt32(withBlock1.GraphicY2);
                                                                    buffer.WriteInt32(withBlock1.MoveSpeed);
                                                                    buffer.WriteInt32(withBlock1.X);
                                                                    buffer.WriteInt32(withBlock1.Y);
                                                                    buffer.WriteByte(withBlock1.Position);
                                                                    buffer.WriteBoolean(withBlock1.Visible);
                                                                    buffer.WriteInt32(withBlock1.WalkingAnim);
                                                                    buffer.WriteInt32(withBlock1.FixedDir);
                                                                    buffer.WriteInt32(withBlock1.WalkThrough);
                                                                    buffer.WriteInt32(withBlock1.ShowName);
                                                                    buffer.WriteInt32(withBlock1.QuestNum);
                                                                }
                                                                NetworkConfig.Socket.SendDataTo(ref playerID, ref buffer.Data, ref buffer.Head);
                                                                buffer.Dispose();
                                                            }
                                                        }
                                                        donotprocessmoveroute = Conversions.ToBoolean(0);
                                                    }
                                                }

                                                break;
                                            }
                                    }
                                    switch (Core.Type.TempPlayer[playerID].EventMap.EventPages[x].MoveFreq)
                                    {
                                        case 0:
                                            {
                                                Core.Type.TempPlayer[playerID].EventMap.EventPages[x].MoveTimer = General.GetTimeMs() + 4000;
                                                break;
                                            }
                                        case 1:
                                            {
                                                Core.Type.TempPlayer[playerID].EventMap.EventPages[x].MoveTimer = General.GetTimeMs() + 2000;
                                                break;
                                            }
                                        case 2:
                                            {
                                                Core.Type.TempPlayer[playerID].EventMap.EventPages[x].MoveTimer = General.GetTimeMs() + 1000;
                                                break;
                                            }
                                        case 3:
                                            {
                                                Core.Type.TempPlayer[playerID].EventMap.EventPages[x].MoveTimer = General.GetTimeMs() + 500;
                                                break;
                                            }
                                        case 4:
                                            {
                                                Core.Type.TempPlayer[playerID].EventMap.EventPages[x].MoveTimer = General.GetTimeMs() + 250;
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        internal static void ProcessEventCommands()
        {
            var buffer = new ByteStream(4);
            int i;
            int x;
            var removeEventProcess = default(bool);
            var w = default(int);
            int v;
            int p;
            bool restartlist;
            bool restartloop;
            bool endprocess;

            // Now, we process the damn things for commands :P
            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    if (Core.Type.TempPlayer[i].GettingMap == false)
                    {
                        if (Core.Type.TempPlayer[i].EventMap.CurrentEvents > 0)
                        {
                            var loopTo1 = Core.Type.TempPlayer[i].EventMap.CurrentEvents;
                            for (x = 0; x <= (int)loopTo1; x++)
                            {
                                if (Core.Type.TempPlayer[i].EventProcessingCount > 0)
                                {
                                    if (Core.Type.TempPlayer[i].EventMap.EventPages[x].Visible == true)
                                    {
                                        if (x < Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID)
                                            continue;
                                        if (Map[Core.Type.Player[i].Map].Event[Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID].Pages[Core.Type.TempPlayer[i].EventMap.EventPages[x].PageID].Trigger == 2) // Parallel Process baby!
                                        {

                                            if (Core.Type.TempPlayer[i].EventProcessing[x].Active == 0)
                                            {
                                                if (Map[GetPlayerMap(i)].Event[Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID].Pages[Core.Type.TempPlayer[i].EventMap.EventPages[x].PageID].CommandListCount > 0)
                                                {
                                                    // start new event processing
                                                    Core.Type.TempPlayer[i].EventProcessing[Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID].Active = 0;
                                                    {
                                                        var withBlock = Core.Type.TempPlayer[i].EventProcessing[Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID];
                                                        withBlock.ActionTimer = General.GetTimeMs();
                                                        withBlock.CurList = 0;
                                                        withBlock.CurSlot = 0;
                                                        withBlock.EventID = Core.Type.TempPlayer[i].EventMap.EventPages[x].EventID;
                                                        withBlock.PageID = Core.Type.TempPlayer[i].EventMap.EventPages[x].PageID;
                                                        withBlock.WaitingForResponse = 0;
                                                        ;

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

            // That is it for starting parallel processes :D now we just have to make the code that actually processes the events to their fullest
            var loopTo2 = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= (int)loopTo2; i++)
            {
                if (NetworkConfig.IsPlaying(i))
                {
                    if (Core.Type.TempPlayer[i].EventProcessingCount > 0)
                    {
                        if (Core.Type.TempPlayer[i].GettingMap == false)
                        {
                            restartloop = Conversions.ToBoolean(1);
                            while (Conversions.ToInteger(restartloop) == 1)
                            {
                                restartloop = Conversions.ToBoolean(0);
                                var loopTo3 = Core.Type.TempPlayer[i].EventProcessingCount;
                                for (x = 0; x <= (int)loopTo3; x++)
                                {
                                    if (Core.Type.TempPlayer[i].EventProcessing[x].Active == 1)
                                    {
                                        {
                                            var withBlock1 = Core.Type.TempPlayer[i].EventProcessing[x];
                                            if (Core.Type.TempPlayer[i].EventProcessingCount == 0)
                                                return;
                                            removeEventProcess = Conversions.ToBoolean(0);
                                            if (withBlock1.WaitingForResponse == 2)
                                            {
                                                if (Core.Type.TempPlayer[i].InShop == 0)
                                                {
                                                    withBlock1.WaitingForResponse = 0;
                                                }
                                            }
                                            if (withBlock1.WaitingForResponse == 3)
                                            {
                                                if (Core.Type.TempPlayer[i].InBank == false)
                                                {
                                                    withBlock1.WaitingForResponse = 0;
                                                }
                                            }
                                            if (withBlock1.WaitingForResponse == 4)
                                            {
                                                // waiting for eventmovement to complete
                                                if (withBlock1.EventMovingType == 0)
                                                {
                                                    if (Core.Type.TempPlayer[i].EventMap.EventPages[withBlock1.EventMovingId].MoveRouteComplete == 1)
                                                    {
                                                        withBlock1.WaitingForResponse = 0;
                                                    }
                                                }
                                                else if (Event.TempEventMap[GetPlayerMap(i)].Event[withBlock1.EventMovingId].MoveRouteComplete == 1)
                                                {
                                                    withBlock1.WaitingForResponse = 0;
                                                }
                                            }

                                            if (withBlock1.WaitingForResponse == 0)
                                            {
                                                if (withBlock1.ActionTimer <= General.GetTimeMs())
                                                {
                                                    restartlist = Conversions.ToBoolean(1);
                                                    endprocess = Conversions.ToBoolean(0);
                                                    while (Conversions.ToInteger(restartlist) == 1 & Conversions.ToInteger(endprocess) == 0 & withBlock1.WaitingForResponse == 0)
                                                    {
                                                        restartlist = Conversions.ToBoolean(0);
                                                        if (withBlock1.ListLeftOff[withBlock1.CurList] > 0)
                                                        {
                                                            withBlock1.CurSlot = withBlock1.ListLeftOff[withBlock1.CurList] + 1;
                                                            withBlock1.ListLeftOff[withBlock1.CurList] = 0;
                                                        }

                                                        if (withBlock1.CurList > Map[Core.Type.Player[i].Map].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandListCount)
                                                        {
                                                            // Get rid of this event, it is bad
                                                            removeEventProcess = Conversions.ToBoolean(1);
                                                            endprocess = Conversions.ToBoolean(1);
                                                        }

                                                        if (withBlock1.CurSlot > Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].CommandCount)
                                                        {
                                                            if (withBlock1.CurList == 1)
                                                            {
                                                                // Get rid of this event, it is bad
                                                                removeEventProcess = Conversions.ToBoolean(1);
                                                                endprocess = Conversions.ToBoolean(1);
                                                            }
                                                            else
                                                            {
                                                                withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].ParentList;
                                                                withBlock1.CurSlot = 0;
                                                                restartlist = Conversions.ToBoolean(1);
                                                            }
                                                        }

                                                        if (Conversions.ToInteger(restartlist) == 0 & Conversions.ToInteger(endprocess) == 0)
                                                        {
                                                            // If we are still here, then we are good to process shit :D
                                                            switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Index)
                                                            {
                                                                case (byte)EventType.AddText:
                                                                    {
                                                                        switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2)
                                                                        {
                                                                            case 0:
                                                                                {
                                                                                    NetworkSend.PlayerMsg(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                                    break;
                                                                                }
                                                                            case 1:
                                                                                {
                                                                                    NetworkSend.MapMsg(GetPlayerMap(i), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1, (byte)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                                    break;
                                                                                }
                                                                            case 2:
                                                                                {
                                                                                    NetworkSend.GlobalMsg(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1); // Type.Map(GetPlayerMap(i)).Events(.EventID].Pages(.PageID).CommandList(.CurList].Commands(.CurSlot).Data1)
                                                                                    break;
                                                                                }
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.ShowText:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SEventChat);
                                                                        buffer.WriteInt32(withBlock1.EventID);
                                                                        buffer.WriteInt32(withBlock1.PageID);
                                                                        buffer.WriteInt32(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                        buffer.WriteString(ParseEventText(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1));
                                                                        buffer.WriteInt32(0);

                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].CommandCount > withBlock1.CurSlot)
                                                                        {
                                                                            if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index == (byte)EventType.ShowText | Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index == (byte)EventType.ShowChoices)
                                                                            {
                                                                                buffer.WriteInt32(1);
                                                                            }
                                                                            else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index == (byte)EventType.Condition)
                                                                            {
                                                                                buffer.WriteInt32(2);
                                                                            }
                                                                            else
                                                                            {
                                                                                buffer.WriteInt32(0);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            buffer.WriteInt32(2);
                                                                        }
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);
                                                                        buffer.Dispose();
                                                                        withBlock1.WaitingForResponse = 0;
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ShowChoices:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SEventChat);
                                                                        buffer.WriteInt32(withBlock1.EventID);
                                                                        buffer.WriteInt32(withBlock1.PageID);
                                                                        buffer.WriteInt32(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data5);
                                                                        buffer.WriteString(ParseEventText(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1));

                                                                        if ((Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text2.Length) > 0)
                                                                        {
                                                                            w = 0;
                                                                            if ((Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text3.Length) > 0)
                                                                            {
                                                                                w = 2;
                                                                                if ((Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text4.Length) > 0)
                                                                                {
                                                                                    w = 3;
                                                                                    if ((Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text5.Length) > 0)
                                                                                    {
                                                                                        w = 4;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        buffer.WriteInt32(w);
                                                                        var loopTo4 = w;
                                                                        for (v = 0; v <= (int)loopTo4; v++)
                                                                        {
                                                                            switch (v)
                                                                            {
                                                                                case 1:
                                                                                    {
                                                                                        buffer.WriteString(ParseEventText(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text2));
                                                                                        break;
                                                                                    }
                                                                                case 2:
                                                                                    {
                                                                                        buffer.WriteString(ParseEventText(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text3));
                                                                                        break;
                                                                                    }
                                                                                case 3:
                                                                                    {
                                                                                        buffer.WriteString(ParseEventText(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text4));
                                                                                        break;
                                                                                    }
                                                                                case 4:
                                                                                    {
                                                                                        buffer.WriteString(ParseEventText(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text5));
                                                                                        break;
                                                                                    }
                                                                            }
                                                                        }
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].CommandCount > withBlock1.CurSlot)
                                                                        {
                                                                            if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index == (byte)EventType.ShowText | Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index == (byte)EventType.ShowChoices)
                                                                            {
                                                                                buffer.WriteInt32(1);
                                                                            }
                                                                            else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index == (byte)EventType.Condition)
                                                                            {
                                                                                buffer.WriteInt32(2);
                                                                            }
                                                                            else
                                                                            {
                                                                                buffer.WriteInt32(0);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            buffer.WriteInt32(2);
                                                                        }
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);
                                                                        buffer.Dispose();
                                                                        withBlock1.WaitingForResponse = 0;
                                                                        break;
                                                                    }
                                                                case (byte)EventType.PlayerVar:
                                                                    {
                                                                        switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2)
                                                                        {
                                                                            case 0:
                                                                                {
                                                                                    Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3;
                                                                                    break;
                                                                                }
                                                                            case 1:
                                                                                {
                                                                                    Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] = Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] + Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3;
                                                                                    break;
                                                                                }
                                                                            case 2:
                                                                                {
                                                                                    Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] = Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] - Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3;
                                                                                    break;
                                                                                }
                                                                            case 3:
                                                                                {
                                                                                    Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] = (int)General.Random.NextDouble(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data4);
                                                                                    break;
                                                                                }
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.PlayerSwitch:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 0)
                                                                        {
                                                                            Core.Type.Player[i].Switches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] = 0;
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 1)
                                                                        {
                                                                            Core.Type.Player[i].Switches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] = 0;
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.SelfSwitch:
                                                                    {
                                                                        if ((int)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Globals == 1)
                                                                        {
                                                                            if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 0)
                                                                            {
                                                                                Map[GetPlayerMap(i)].Event[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 + 1] = 0;
                                                                            }
                                                                            else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 1)
                                                                            {
                                                                                Map[GetPlayerMap(i)].Event[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 + 1] = 0;
                                                                            }
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 0)
                                                                        {
                                                                            Core.Type.TempPlayer[i].EventMap.EventPages[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 + 1] = 0;
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 1)
                                                                        {
                                                                            Core.Type.TempPlayer[i].EventMap.EventPages[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 + 1] = 0;
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.Condition:
                                                                    {
                                                                        switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Condition)
                                                                        {
                                                                            case 0:
                                                                                {
                                                                                    switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data2)
                                                                                    {
                                                                                        case 0:
                                                                                            {
                                                                                                if (Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] == Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data3)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 1:
                                                                                            {
                                                                                                if (Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] >= Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data3)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 2:
                                                                                            {
                                                                                                if (Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] <= Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data3)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 3:
                                                                                            {
                                                                                                if (Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] > Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data3)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 4:
                                                                                            {
                                                                                                if (Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] < Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data3)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 5:
                                                                                            {
                                                                                                if (Core.Type.Player[i].Variables[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] != Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data3)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 1:
                                                                                {
                                                                                    switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data2)
                                                                                    {
                                                                                        case 0:
                                                                                            {
                                                                                                if ((int)Core.Type.Player[i].Switches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] == 1)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 1:
                                                                                            {
                                                                                                if ((int)Core.Type.Player[i].Switches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1] == 0)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 2:
                                                                                {
                                                                                    if (Player.HasItem(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1) >= Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data2)
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 3:
                                                                                {
                                                                                    if ((int)Core.Type.Player[i].Job == Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 4:
                                                                                {
                                                                                    if (HasSkill(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1) == true)
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 5:
                                                                                {
                                                                                    switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data2)
                                                                                    {
                                                                                        case 0:
                                                                                            {
                                                                                                if (GetPlayerLevel(i) == Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 1:
                                                                                            {
                                                                                                if (GetPlayerLevel(i) >= Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 2:
                                                                                            {
                                                                                                if (GetPlayerLevel(i) <= Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 3:
                                                                                            {
                                                                                                if (GetPlayerLevel(i) > Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 4:
                                                                                            {
                                                                                                if (GetPlayerLevel(i) < Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case 5:
                                                                                            {
                                                                                                if (GetPlayerLevel(i) != Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                    withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                    withBlock1.CurSlot = 0;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 6:
                                                                                {
                                                                                    if ((int)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Globals == 1)
                                                                                    {
                                                                                        switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data2)
                                                                                        {
                                                                                            case 0: // Self Switch is true
                                                                                                {
                                                                                                    if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1 + 1] == 1)
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }

                                                                                                    break;
                                                                                                }
                                                                                            case 1:  // self switch is false
                                                                                                {
                                                                                                    if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1 + 1] == 0)
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }

                                                                                                    break;
                                                                                                }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data2)
                                                                                        {
                                                                                            case 0: // Self Switch is true
                                                                                                {
                                                                                                    if (Core.Type.TempPlayer[i].EventMap.EventPages[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1 + 1] == 1)
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }

                                                                                                    break;
                                                                                                }
                                                                                            case 1:  // self switch is false
                                                                                                {
                                                                                                    if (Core.Type.TempPlayer[i].EventMap.EventPages[withBlock1.EventID].SelfSwitches[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1 + 1] == 0)
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                                        withBlock1.CurSlot = 0;
                                                                                                    }

                                                                                                    break;
                                                                                                }
                                                                                        }
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 7:
                                                                                {
                                                                                    break;
                                                                                }

                                                                            case 8:
                                                                                {
                                                                                    if ((int)Core.Type.Player[i].Sex == Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }

                                                                                    break;
                                                                                }
                                                                            case 9:
                                                                                {
                                                                                    if (TimeType.Instance.TimeOfDay == (TimeOfDay)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.Data1)
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.CommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                                                        withBlock1.CurList = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].ConditionalBranch.ElseCommandList;
                                                                                        withBlock1.CurSlot = 0;
                                                                                    }

                                                                                    break;
                                                                                }
                                                                        }
                                                                        endprocess = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ExitProcess:
                                                                    {
                                                                        removeEventProcess = Conversions.ToBoolean(1);
                                                                        endprocess = Conversions.ToBoolean(1);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ChangeItems:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 0)
                                                                        {
                                                                            if (Player.HasItem(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1) > 0)
                                                                            {
                                                                                SetPlayerInvValue(i, Player.FindItemSlot(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3);
                                                                            }
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 1)
                                                                        {
                                                                            Player.GiveInv(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3, true);
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 2)
                                                                        {
                                                                            int itemAmount;
                                                                            itemAmount = Player.HasItem(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                            // Check Amount
                                                                            if (itemAmount >= Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3)
                                                                            {
                                                                                Player.TakeInv(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3);
                                                                            }
                                                                        }
                                                                        NetworkSend.SendInventory(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.RestoreHP:
                                                                    {
                                                                        SetPlayerVital(i, VitalType.HP, GetPlayerMaxVital(i, VitalType.HP));
                                                                        NetworkSend.SendVital(i, VitalType.HP);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.RestoreSP:
                                                                    {
                                                                        SetPlayerVital(i, VitalType.SP, GetPlayerMaxVital(i, VitalType.SP));
                                                                        NetworkSend.SendVital(i, VitalType.SP);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.LevelUp:
                                                                    {
                                                                        SetPlayerExp(i, GetPlayerNextLevel(i));
                                                                        Player.CheckPlayerLevelUp(i);
                                                                        NetworkSend.SendExp(i);
                                                                        NetworkSend.SendPlayerData(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ChangeLevel:
                                                                    {
                                                                        SetPlayerLevel(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                        SetPlayerExp(i, 0);
                                                                        NetworkSend.SendExp(i);
                                                                        NetworkSend.SendPlayerData(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ChangeSkills:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 0)
                                                                        {
                                                                            if (FindOpenSkill(i) > 0)
                                                                            {
                                                                                if (HasSkill(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1) == false)
                                                                                {
                                                                                    SetPlayerSkill(i, FindOpenSkill(i), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                                }
                                                                                else
                                                                                {
                                                                                    // Error, already knows skill
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                // Error, no room for skills
                                                                            }
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 1)
                                                                        {
                                                                            if (HasSkill(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1) == true)
                                                                            {
                                                                                var loopTo5 = Core.Constant.MAX_PLAYER_SKILLS - 1;
                                                                                for (p = 0; p <= (int)loopTo5; p++)
                                                                                {
                                                                                    if (Core.Type.Player[i].Skill[p].Num == Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1)
                                                                                    {
                                                                                        SetPlayerSkill(i, p, 0);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        NetworkSend.SendPlayerSkills(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ChangeJob:
                                                                    {
                                                                        Core.Type.Player[i].Job = (byte)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1;
                                                                        NetworkSend.SendPlayerData(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ChangeSprite:
                                                                    {
                                                                        SetPlayerSprite(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                        NetworkSend.SendPlayerData(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ChangeSex:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 == 0)
                                                                        {
                                                                            Core.Type.Player[i].Sex = (byte)SexType.Male;
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 == 1)
                                                                        {
                                                                            Core.Type.Player[i].Sex = (byte)SexType.Female;
                                                                        }
                                                                        NetworkSend.SendPlayerData(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ChangePk:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 == 0)
                                                                        {
                                                                            Core.Type.Player[i].Pk = 0;
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 == 1)
                                                                        {
                                                                            Core.Type.Player[i].Pk = 0;
                                                                        }
                                                                        NetworkSend.SendPlayerData(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.WarpPlayer:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data4 == 0)
                                                                        {
                                                                            Player.PlayerWarp(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3);
                                                                        }
                                                                        else
                                                                        {
                                                                            Core.Type.Player[i].Dir = (byte)(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data4 - 1);
                                                                            Player.PlayerWarp(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3);
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.SetMoveRoute:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 <= Map[GetPlayerMap(i)].EventCount)
                                                                        {
                                                                            if ((int)Map[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].Globals == 1)
                                                                            {
                                                                                Event.TempEventMap[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveType = 2;
                                                                                Event.TempEventMap[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].IgnoreIfCannotMove = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2;
                                                                                Event.TempEventMap[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].RepeatMoveRoute = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3;
                                                                                Event.TempEventMap[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRouteCount = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].MoveRouteCount;
                                                                                Event.TempEventMap[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRoute = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].MoveRoute;
                                                                                Event.TempEventMap[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRouteStep = 0;
                                                                                Event.TempEventMap[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRouteComplete = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveType = 2;
                                                                                Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].IgnoreIfCannotMove = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2;
                                                                                Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].RepeatMoveRoute = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3;
                                                                                Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRouteCount = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].MoveRouteCount;
                                                                                Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRoute = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].MoveRoute;
                                                                                Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRouteStep = 0;
                                                                                Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].MoveRouteComplete = 0;
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.PlayAnimation:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 0)
                                                                        {
                                                                            Animation.SendAnimation(GetPlayerMap(i), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, GetPlayerX(i), GetPlayerY(i), (byte)TargetType.Player, i);
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 1)
                                                                        {
                                                                            if ((int)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Globals == 1)
                                                                            {
                                                                                Animation.SendAnimation(GetPlayerMap(i), Map[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].X, Map[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3].Y);
                                                                            }
                                                                            else
                                                                            {
                                                                                Animation.SendAnimation(GetPlayerMap(i), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3].X, Core.Type.TempPlayer[i].EventMap.EventPages[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3].Y, 0, 0);
                                                                            }
                                                                        }
                                                                        else if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2 == 2)
                                                                        {
                                                                            Animation.SendAnimation(GetPlayerMap(i), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data4, 0, 0);
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.CustomScript:
                                                                    {
                                                                        // Runs Through Cases for a script
                                                                        Event.CustomScript(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, GetPlayerMap(i), withBlock1.EventID);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.PlayBgm:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SPlayBGM);
                                                                        buffer.WriteString(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);
                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                                case (byte)EventType.FadeoutBgm:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SFadeoutBGM);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);
                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                                case (byte)EventType.PlaySound:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SPlaySound);
                                                                        buffer.WriteString(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1);
                                                                        buffer.WriteInt32(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].X);
                                                                        buffer.WriteInt32(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].Y);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);
                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                                case (byte)EventType.StopSound:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SStopSound);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);
                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                                case (byte)EventType.SetAccess:
                                                                    {
                                                                        SetPlayerAccess(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                        NetworkSend.SendPlayerData(i);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.OpenShop:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 > 0) // shop exists?
                                                                        {
                                                                            if ((Shop[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].Name.Length) > 0) // name exists?
                                                                            {
                                                                                NetworkSend.SendOpenShop(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                                Core.Type.TempPlayer[i].InShop = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1; // stops movement and the like
                                                                                withBlock1.WaitingForResponse = 2;
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.OpenBank:
                                                                    {
                                                                        NetworkSend.SendBank(i);
                                                                        Core.Type.TempPlayer[i].InBank = true;
                                                                        withBlock1.WaitingForResponse = 3;
                                                                        break;
                                                                    }
                                                                case (byte)EventType.GiveExp:
                                                                    {
                                                                        Event.GivePlayerExp(i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ShowChatBubble:
                                                                    {
                                                                        switch (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1)
                                                                        {
                                                                            case (byte)TargetType.Player:
                                                                                {
                                                                                    NetworkSend.SendChatBubble(GetPlayerMap(i), i, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1, (int) ColorType.Blue);
                                                                                    break;
                                                                                }
                                                                            case (byte)TargetType.NPC:
                                                                                {
                                                                                    NetworkSend.SendChatBubble(GetPlayerMap(i), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1, (int) ColorType.Blue);
                                                                                    break;
                                                                                }
                                                                            case (byte)TargetType.Event:
                                                                                {
                                                                                    NetworkSend.SendChatBubble(GetPlayerMap(i), Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1, (int) ColorType.Blue);
                                                                                    break;
                                                                                }
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.Label:
                                                                    {
                                                                        break;
                                                                    }
                                                                // Do nothing, just a label
                                                                case (byte)EventType.GotoLabel:
                                                                    {
                                                                        // Find the label's list of commands and slot
                                                                        FindEventLabel(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Text1, GetPlayerMap(i), withBlock1.EventID, withBlock1.PageID, ref withBlock1.CurSlot, ref withBlock1.CurList, ref withBlock1.ListLeftOff);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.SpawnNPC:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].NPC[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1] > 0)
                                                                        {
                                                                            NPC.SpawnNPC(Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, GetPlayerMap(i));
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.FadeIn:
                                                                    {
                                                                        Event.SendSpecialEffect(i, Event.EffectTypeFadein);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.FadeOut:
                                                                    {
                                                                        Event.SendSpecialEffect(i, Event.EffectTypeFadeout);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.FlashWhite:
                                                                    {
                                                                        Event.SendSpecialEffect(i, Event.EffectTypeFlash);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.SetFog:
                                                                    {
                                                                        Event.SendSpecialEffect(i, Event.EffectTypeFog, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.SetWeather:
                                                                    {
                                                                        Event.SendSpecialEffect(i, Event.EffectTypeWeather, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.SetTint:
                                                                    {
                                                                        Event.SendSpecialEffect(i, Event.EffectTypeTint, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3, Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data4);
                                                                        break;
                                                                    }
                                                                case (byte)EventType.Wait:
                                                                    {
                                                                        withBlock1.ActionTimer = General.GetTimeMs() + Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1;
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ShowPicture:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SPic);
                                                                        buffer.WriteInt32(withBlock1.EventID);
                                                                        buffer.WriteByte((byte)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1);
                                                                        buffer.WriteByte((byte)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data2);
                                                                        buffer.WriteByte((byte)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data3);
                                                                        buffer.WriteByte((byte)Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data4);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);

                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                                case (byte)EventType.HidePicture:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SPic);
                                                                        buffer.WriteByte(0);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);

                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                                case (byte)EventType.WaitMovement:
                                                                    {
                                                                        if (Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1 <= Map[GetPlayerMap(i)].EventCount)
                                                                        {
                                                                            if ((int)Map[GetPlayerMap(i)].Event[Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1].Globals == 1)
                                                                            {
                                                                                withBlock1.WaitingForResponse = 4;
                                                                                withBlock1.EventMovingId = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1;
                                                                                withBlock1.EventMovingType = 0;
                                                                            }
                                                                            else
                                                                            {
                                                                                withBlock1.WaitingForResponse = 4;
                                                                                withBlock1.EventMovingId = Map[GetPlayerMap(i)].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList[withBlock1.CurList].Commands[withBlock1.CurSlot].Data1;
                                                                                withBlock1.EventMovingType = 0;
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case (byte)EventType.HoldPlayer:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SHoldPlayer);
                                                                        buffer.WriteInt32(0);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);

                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                                case (byte)EventType.ReleasePlayer:
                                                                    {
                                                                        buffer = new ByteStream(4);
                                                                        buffer.WriteInt32((int) ServerPackets.SHoldPlayer);
                                                                        buffer.WriteInt32(1);
                                                                        NetworkConfig.Socket.SendDataTo(ref i, ref buffer.Data, ref buffer.Head);

                                                                        buffer.Dispose();
                                                                        break;
                                                                    }
                                                            }
                                                        }
                                                    }
                                                    if (Conversions.ToInteger(endprocess) == 0)
                                                    {
                                                        withBlock1.CurSlot = withBlock1.CurSlot + 1;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Conversions.ToInteger(removeEventProcess) == 1)
                                    {
                                        Core.Type.TempPlayer[i].EventProcessing[x].Active = 0;
                                        restartloop = Conversions.ToBoolean(1);
                                        removeEventProcess = Conversions.ToBoolean(0);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        internal static void UpdateEventLogic()
        {
            RemoveDeadEvents();
            SpawnNewEvents();
            ProcessEventMovement();
            ProcessLocalEventMovement();
            ProcessEventCommands();
        }

        public static string ParseEventText(int index, string txt)
        {
            string ParseEventTextRet = default;
            int i;
            int x;
            string newtxt;
            string parsestring;
            int z;

            txt = Strings.Replace(txt, "/name", Core.Type.Player[index].Name);
            txt = Strings.Replace(txt, "/p", Core.Type.Player[index].Name);
            txt = Strings.Replace(txt, "$playername$", Core.Type.Player[index].Name);
            txt = Strings.Replace(txt, "$playerclass$", Job[Core.Type.Player[index].Job].Name);
            while (Strings.InStr(1, txt, "/v") > 0)
            {
                x = Strings.InStr(1, txt, "/v");
                if (x > 0)
                {
                    i = 0;
                    while (Conversions.ToInteger(Information.IsNumeric(Strings.Mid(txt, x + 2 + i, 1))) != 0)
                        i = i + 1;
                    newtxt = Strings.Mid(txt, 1, x - 1);
                    parsestring = Strings.Mid(txt, x + 2, i);
                    z = Core.Type.Player[index].Variables[(int)Math.Round(Conversion.Val(parsestring))];
                    newtxt = newtxt + z.ToString();
                    newtxt = newtxt + Strings.Mid(txt, x + 2 + i, Strings.Len(txt) - (x + i));
                    txt = newtxt;
                }
            }
            ParseEventTextRet = txt;
            return ParseEventTextRet;

        }

        public static void FindEventLabel(string Label, int mapNum, int EventID, int PageID, ref int CurSlot, ref int CurList, ref int[] ListLeftOff)
        {
            int tmpCurSlot;
            int tmpCurList;
            int[] CurrentListOption;
            var removeEventProcess = default(bool);
            int[] tmpListLeftOff;
            var restartlist = default(bool);
            var w = default(int);

            // Store the Old data, just in case

            tmpCurSlot = CurSlot;
            tmpCurList = CurList;
            tmpListLeftOff = ListLeftOff;

            ListLeftOff = new int[Map[mapNum].Event[EventID].Pages[PageID].CommandListCount];
            CurrentListOption = new int[Map[mapNum].Event[EventID].Pages[PageID].CommandListCount];
            CurList = 0;
            CurSlot = 0;

            while (Conversions.ToInteger(removeEventProcess) != 1)
            {
                if (ListLeftOff[CurList] > 0)
                {
                    CurSlot = ListLeftOff[CurList];
                    ListLeftOff[CurList] = 0;
                }
                if (CurList > Map[mapNum].Event[EventID].Pages[PageID].CommandListCount)
                {
                    // Get rid of this event, it is bad
                    removeEventProcess = Conversions.ToBoolean(1);
                }

                if (CurSlot > Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].CommandCount)
                {
                    if (CurList == 1)
                    {
                        removeEventProcess = Conversions.ToBoolean(1);
                    }
                    else
                    {
                        CurList = Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].ParentList;
                        CurSlot = 0;
                        restartlist = Conversions.ToBoolean(1);
                    }
                }

                if (Conversions.ToInteger(restartlist) == 0)
                {
                    if (Conversions.ToInteger(removeEventProcess) == 0)
                    {
                        // If we are still here, then we are good to process shit :D
                        switch (Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Index)
                        {
                            case (byte)EventType.ShowChoices:
                                {
                                    if (Strings.Len(Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Text2) > 0)
                                    {
                                        w = 0;
                                        if (Strings.Len(Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Text3) > 0)
                                        {
                                            w = 2;
                                            if (Strings.Len(Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Text4) > 0)
                                            {
                                                w = 3;
                                                if (Strings.Len(Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Text5) > 0)
                                                {
                                                    w = 4;
                                                }
                                            }
                                        }
                                    }
                                    if (w > 0)
                                    {
                                        if (CurrentListOption[CurList] < w)
                                        {
                                            CurrentListOption[CurList] = CurrentListOption[CurList] + 1;
                                            // Process
                                            ListLeftOff[CurList] = CurSlot;
                                            switch (CurrentListOption[CurList])
                                            {
                                                case 1:
                                                    {
                                                        CurList = Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Data1;
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        CurList = Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Data2;
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        CurList = Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Data3;
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        CurList = Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Data4;
                                                        break;
                                                    }
                                            }
                                            CurSlot = 0;
                                        }
                                        else
                                        {
                                            CurrentListOption[CurList] = 0;
                                            // continue on
                                        }
                                    }
                                    w = 0;
                                    break;
                                }
                            case (byte)EventType.Condition:
                                {
                                    if (CurrentListOption[CurList] == 0)
                                    {
                                        CurrentListOption[CurList] = 0;
                                        ListLeftOff[CurList] = CurSlot;
                                        CurList = Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].ConditionalBranch.CommandList;
                                        CurSlot = 0;
                                    }
                                    else if (CurrentListOption[CurList] == 1)
                                    {
                                        CurrentListOption[CurList] = 2;
                                        ListLeftOff[CurList] = CurSlot;
                                        CurList = Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].ConditionalBranch.ElseCommandList;
                                        CurSlot = 0;
                                    }
                                    else if (CurrentListOption[CurList] == 2)
                                    {
                                        CurrentListOption[CurList] = 0;
                                    }

                                    break;
                                }
                            case (byte)EventType.Label:
                                {
                                    // Do nothing, just a label
                                    if ((Map[mapNum].Event[EventID].Pages[PageID].CommandList[CurList].Commands[CurSlot].Text1 ?? "") == (Label ?? ""))
                                    {
                                        return;
                                    }

                                    break;
                                }
                        }
                        CurSlot = CurSlot + 1;
                    }
                }
                restartlist = Conversions.ToBoolean(0);
            }

            ListLeftOff = tmpListLeftOff;
            CurList = tmpCurList;
            CurSlot = tmpCurSlot;

        }

        public static int FindNPCPath(int MapNum, int MapNPCNum, int targetx, int targety)
        {
            int FindNPCPathRet = default;
            int tim;
            int sX;
            int sY;
            int[,] pos;
            bool reachable;
            int j;
            var LastSum = default(int);
            int Sum;
            int FX;
            int FY;
            int i;
            Point[] path;
            int LastX;
            int LastY;
            bool did;

            // Initialization phase

            tim = 0;

            sX = Core.Type.MapNPC[MapNum].NPC[MapNPCNum].X;
            sY = Core.Type.MapNPC[MapNum].NPC[MapNPCNum].Y;

            FX = targetx;
            FY = targety;

            if (FX == -1)
                FX = 0;
            if (FY == -1)
                FY = 0;

            pos = new int[(Map[MapNum].MaxX + 1), (Map[MapNum].MaxY + 1)];
            // pos = MapBlocks(MapNum).Blocks

            pos[sX, sY] = 100 + tim;
            pos[FX, FY] = 2;

            // reset reachable
            reachable = Conversions.ToBoolean(0);

            // Do while reachable is false... if its set true in progress, we jump out
            // If the path is decided unreachable in process, we will use exit sub. Not proper,
            // but faster ;-)
            while (Conversions.ToInteger(reachable) == 0)
            {
                // we loop through all squares
                var loopTo = (int)Map[MapNum].MaxY;
                for (j = 0; j <= (int)loopTo; j++)
                {
                    var loopTo1 = (int)Map[MapNum].MaxX;
                    for (i = 0; i <= (int)loopTo1; i++)
                    {
                        // If j = 10 And i = 0 Then MsgBox "hi!"
                        // If they are to be extended, the pointer TIM is on them
                        if (pos[i, j] == 100 + tim)
                        {
                            // The part is to be extended, so do it
                            // We have to make sure that there is a pos(i+1,j) BEFORE we actually use it,
                            // because then we get error... If the square is on side, we dont test for this one!
                            if (i < Map[MapNum].MaxX)
                            {
                                // If there isnt a wall, or any other... thing
                                if (pos[i + 1, j] == 0)
                                {
                                    // Expand it, and make its pos equal to tim+1, so the next time we make this loop,
                                    // It will exapand that square too! This is crucial part of the program
                                    pos[i + 1, j] = 100 + tim + 1;
                                }
                                else if (pos[i + 1, j] == 2)
                                {
                                    // If the position is no 0 but its 2 (FINISH) then Reachable = 1!!! We found end
                                    reachable = Conversions.ToBoolean(1);
                                }
                            }

                            // This is the same as the last one, as i said a lot of copy paste work and editing that
                            // This is simply another side that we have to test for... so instead of i+1 we have i-1
                            // Its actually pretty same then... i wont comment it therefore, because its only repeating
                            // same thing with minor changes to check sides
                            if (i > 0)
                            {
                                if (pos[i - 1, j] == 0)
                                {
                                    pos[i - 1, j] = 100 + tim + 1;
                                }
                                else if (pos[i - 1, j] == 2)
                                {
                                    reachable = Conversions.ToBoolean(1);
                                }
                            }

                            if (j < Map[MapNum].MaxY)
                            {
                                if (pos[i, j + 1] == 0)
                                {
                                    pos[i, j + 1] = 100 + tim + 1;
                                }
                                else if (pos[i, j + 1] == 2)
                                {
                                    reachable = Conversions.ToBoolean(1);
                                }
                            }

                            if (j > 0)
                            {
                                if (pos[i, j - 1] == 0)
                                {
                                    pos[i, j - 1] = 100 + tim + 1;
                                }
                                else if (pos[i, j - 1] == 2)
                                {
                                    reachable = Conversions.ToBoolean(1);
                                }
                            }
                        }
                    }
                }

                // If the reachable is STILL false, then
                if (Conversions.ToInteger(reachable) == 0)
                {
                    // reset sum
                    Sum = 0;
                    var loopTo2 = (int)Map[MapNum].MaxY;
                    for (j = 0; j <= (int)loopTo2; j++)
                    {
                        var loopTo3 = (int)Map[MapNum].MaxX;
                        for (i = 0; i <= (int)loopTo3; i++)
                            // we add up ALL the squares
                            Sum = Sum + pos[i, j];
                    }

                    // Now if the sum is euqal to the last sum, its not reachable, if it isnt, then we store
                    // sum to lastsum
                    if (Sum == LastSum)
                    {
                        FindNPCPathRet = 4;
                        return FindNPCPathRet;
                    }
                    else
                    {
                        LastSum = Sum;
                    }
                }

                // we increase the pointer to point to the next squares to be expanded
                tim = tim + 1;
            }

            // We work backwards to find the way...
            LastX = FX;
            LastY = FY;

            path = new Point[tim + 1 + 1];

            // The following code may be a little bit confusing but ill try my best to explain it.
            // We are working backwards to find ONE of the shortest ways back to Start.
            // So we repeat the loop until the LastX and LastY arent in start. Look in the code to see
            // how LastX and LasY change
            while (LastX != sX | LastY != sY)
            {
                // We decrease tim by one, and then we are finding any adjacent square to the final one, that
                // has that value. So lets say the tim would be 5, because it takes 5 steps to get to the target.
                // Now everytime we decrease that, so we make it 4, and we look for any adjacent square that has
                // that value. When we find it, we just color it yellow as for the solution
                tim = tim - 1;
                // reset did to false
                did = Conversions.ToBoolean(0);

                // If we arent on edge
                if (LastX < Map[MapNum].MaxX)
                {
                    // check the square on the right of the solution. Is it a tim-1 one? or just a blank one
                    if (pos[LastX + 1, LastY] == 100 + tim)
                    {
                        // if it, then make it yellow, and change did to true
                        LastX = LastX + 1;
                        did = Conversions.ToBoolean(1);
                    }
                }

                // This will then only work if the previous part didnt execute, and did is still false. THen
                // we want to check another square, the on left. Is it a tim-1 one ?
                if (Conversions.ToInteger(did) == 0)
                {
                    if (LastX > 0)
                    {
                        if (pos[LastX - 1, LastY] == 100 + tim)
                        {
                            LastX = LastX - 1;
                            did = Conversions.ToBoolean(1);
                        }
                    }
                }

                // We check the one below it
                if (Conversions.ToInteger(did) == 0)
                {
                    if (LastY < Map[MapNum].MaxY)
                    {
                        if (pos[LastX, LastY + 1] == 100 + tim)
                        {
                            LastY = LastY + 1;
                            did = Conversions.ToBoolean(1);
                        }
                    }
                }

                // And above it. One of these have to be it, since we have found the solution, we know that already
                // there is a way back.
                if (Conversions.ToInteger(did) == 0)
                {
                    if (LastY > 0)
                    {
                        if (pos[LastX, LastY - 1] == 100 + tim)
                        {
                            LastY = LastY - 1;
                        }
                    }
                }

                path[tim].X = LastX;
                path[tim].Y = LastY;
            }

            // Ok we got a Core.Path. Now, lets look at the first step and see what direction we should take.
            if (path[1].X > LastX)
            {
                FindNPCPathRet = (byte)DirectionType.Right;
            }
            else if (path[1].Y > LastY)
            {
                FindNPCPathRet = (byte)DirectionType.Down;
            }
            else if (path[1].Y < LastY)
            {
                FindNPCPathRet = (byte)DirectionType.Up;
            }
            else if (path[1].X < LastX)
            {
                FindNPCPathRet = (byte)DirectionType.Left;
            }

            return FindNPCPathRet;

        }

        public static void SpawnAllMapGlobalEvents()
        {
            int i;

            var loopTo = Core.Constant.MAX_MAPS - 1;
            for (i = 0; i <= (int)loopTo; i++)
                SpawnGlobalEvents(i);

        }

        public static void SpawnGlobalEvents(int MapNum)
        {
            int i;
            int z;

            if (Map[MapNum].EventCount > 0)
            {
                Event.TempEventMap[MapNum].EventCount = 0;
                ;
                Array.Resize(ref Event.TempEventMap[MapNum].Event, 1);

                var loopTo = Map[MapNum].EventCount;
                for (i = 0; i <= (int)loopTo; i++)
                {
                    Event.TempEventMap[MapNum].EventCount = Event.TempEventMap[MapNum].EventCount;
                    Array.Resize(ref Event.TempEventMap[MapNum].Event, Event.TempEventMap[MapNum].EventCount);
                    if (Map[MapNum].Event[i].PageCount > 0)
                    {
                        if (Map[MapNum].Event[i].Globals == 1)
                        {
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].X = Map[MapNum].Event[i].X;
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].Y = Map[MapNum].Event[i].Y;
                            if (Map[MapNum].Event[i].Pages[1].GraphicType == 1)
                            {
                                switch (Map[MapNum].Event[i].Pages[1].GraphicY)
                                {
                                    case 0:
                                        {
                                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].Dir = (int)DirectionType.Down;
                                            break;
                                        }
                                    case 1:
                                        {
                                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].Dir = (int)DirectionType.Left;
                                            break;
                                        }
                                    case 2:
                                        {
                                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].Dir = (int)DirectionType.Right;
                                            break;
                                        }
                                    case 3:
                                        {
                                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].Dir = (int)DirectionType.Up;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].Dir = (int)DirectionType.Down;
                            }
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].Active = 0;

                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveType = Map[MapNum].Event[i].Pages[1].MoveType;

                            if (Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveType == 2)
                            {
                                Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveRouteCount = Map[MapNum].Event[i].Pages[1].MoveRouteCount;
                                ;

                                int eventCount = Event.TempEventMap[MapNum].EventCount;
                                int moveRouteCount = Map[MapNum].Event[i].Pages[1].MoveRouteCount;

                                Array.Resize(ref Event.TempEventMap[MapNum].Event[eventCount].MoveRoute, moveRouteCount);
                                for (int j = 0; j < moveRouteCount; j++)
                                {
                                    Event.TempEventMap[MapNum].Event[eventCount].MoveRoute[j] = Map[MapNum].Event[i].Pages[1].MoveRoute[j];
                                }

                                var loopTo1 = Map[MapNum].Event[i].Pages[1].MoveRouteCount;
                                for (z = 0; z <= (int)loopTo1; z++)
                                    Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveRoute[z] = Map[MapNum].Event[i].Pages[1].MoveRoute[z];
                                Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveRouteComplete = 0;
                            }
                            else
                            {
                                Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveRouteComplete = 0;
                            }

                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].RepeatMoveRoute = Map[MapNum].Event[i].Pages[1].RepeatMoveRoute;
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].IgnoreIfCannotMove = Map[MapNum].Event[i].Pages[1].IgnoreMoveRoute;

                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveFreq = Map[MapNum].Event[i].Pages[1].MoveFreq;
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].MoveSpeed = Map[MapNum].Event[i].Pages[1].MoveSpeed;

                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].WalkThrough = Map[MapNum].Event[i].Pages[1].WalkThrough;
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].FixedDir = Map[MapNum].Event[i].Pages[1].DirFix;
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].WalkingAnim = Map[MapNum].Event[i].Pages[1].WalkAnim;
                            Event.TempEventMap[MapNum].Event[Event.TempEventMap[MapNum].EventCount].ShowName = Map[MapNum].Event[i].Pages[1].ShowName;
                        }
                    }
                }
            }

        }

        internal static void SpawnMapEventsFor(int index, int mapNum)
        {
            int i;
            int z;
            bool spawncurrentevent;
            int p;
            int compare;

            Core.Type.TempPlayer[index].EventMap.CurrentEvents = 0;
            ;

            Array.Resize(ref Core.Type.TempPlayer[index].EventMap.EventPages, 1);


            if (Map[mapNum].EventCount > 0)
            {
                ;
                Array.Resize(ref Core.Type.TempPlayer[index].EventProcessing, Map[mapNum].EventCount);

                Core.Type.TempPlayer[index].EventProcessingCount = Map[mapNum].EventCount;
            }
            else
            {
                ;
                Array.Resize(ref Core.Type.TempPlayer[index].EventProcessing, 1);

                Core.Type.TempPlayer[index].EventProcessingCount = 0;
            }

            if (Map[mapNum].EventCount <= 0)
                return;
            var loopTo = Map[mapNum].EventCount;
            for (i = 0; i <= (int)loopTo; i++)
            {
                if (Map[mapNum].Event[i].PageCount > 0)
                {
                    for (z = Map[mapNum].Event[i].PageCount; z >= 0; z -= 1)
                    {
                        {
                            ref var withBlock = ref Map[mapNum].Event[i].Pages[z];
                            spawncurrentevent = Conversions.ToBoolean(1);

                            if (withBlock.ChkVariable == 1)
                            {
                                switch (withBlock.VariableCompare)
                                {
                                    case 0:
                                        {
                                            if (Core.Type.Player[index].Variables[withBlock.VariableIndex] != withBlock.VariableCondition)
                                            {
                                                spawncurrentevent = Conversions.ToBoolean(0);
                                            }

                                            break;
                                        }
                                    case 1:
                                        {
                                            if (Core.Type.Player[index].Variables[withBlock.VariableIndex] < withBlock.VariableCondition)
                                            {
                                                spawncurrentevent = Conversions.ToBoolean(0);
                                            }

                                            break;
                                        }
                                    case 2:
                                        {
                                            if (Core.Type.Player[index].Variables[withBlock.VariableIndex] > withBlock.VariableCondition)
                                            {
                                                spawncurrentevent = Conversions.ToBoolean(0);
                                            }

                                            break;
                                        }
                                    case 3:
                                        {
                                            if (Core.Type.Player[index].Variables[withBlock.VariableIndex] <= withBlock.VariableCondition)
                                            {
                                                spawncurrentevent = Conversions.ToBoolean(0);
                                            }

                                            break;
                                        }
                                    case 4:
                                        {
                                            if (Core.Type.Player[index].Variables[withBlock.VariableIndex] >= withBlock.VariableCondition)
                                            {
                                                spawncurrentevent = Conversions.ToBoolean(0);
                                            }

                                            break;
                                        }
                                    case 5:
                                        {
                                            if (Core.Type.Player[index].Variables[withBlock.VariableIndex] == withBlock.VariableCondition)
                                            {
                                                spawncurrentevent = Conversions.ToBoolean(0);
                                            }

                                            break;
                                        }
                                }
                            }

                            // we are assuming the event will spawn, and are looking for ways to stop it
                            if (withBlock.ChkSwitch == 1)
                            {
                                if (withBlock.SwitchCompare == 1) // we want true
                                {
                                    if (Core.Type.Player[index].Switches[withBlock.SwitchIndex] == 0) // it is false, so we stop the spawn
                                    {
                                        spawncurrentevent = Conversions.ToBoolean(0);
                                    }
                                }
                                else if (Core.Type.Player[index].Switches[withBlock.SwitchIndex] == 1) // we want false and it is true so we stop the spawn
                                {
                                    spawncurrentevent = Conversions.ToBoolean(0);
                                }
                            }

                            if (withBlock.ChkHasItem == 1)
                            {
                                if (Player.HasItem(index, withBlock.HasItemIndex) == 0)
                                {
                                    spawncurrentevent = Conversions.ToBoolean(0);
                                }
                            }

                            if (withBlock.ChkSelfSwitch == 1)
                            {
                                if (withBlock.SelfSwitchCompare == 0)
                                {
                                    compare = 0;
                                }
                                else
                                {
                                    compare = 0;
                                }
                                if (Map[mapNum].Event[i].Globals == 1)
                                {
                                    if (Map[mapNum].Event[i].SelfSwitches[withBlock.SelfSwitchIndex] != compare)
                                    {
                                        spawncurrentevent = Conversions.ToBoolean(0);
                                    }
                                }
                                else if (compare == 1)
                                {
                                    spawncurrentevent = Conversions.ToBoolean(0);
                                }
                            }

                            if (Conversions.ToInteger(spawncurrentevent) == 1 | Conversions.ToInteger(spawncurrentevent) == 0 & z == 1)
                            {
                                // spawn the event... send data to player
                                Core.Type.TempPlayer[index].EventMap.CurrentEvents = Core.Type.TempPlayer[index].EventMap.CurrentEvents + 1;
                                Array.Resize(ref Core.Type.TempPlayer[index].EventMap.EventPages, Core.Type.TempPlayer[index].EventMap.CurrentEvents + 1);
                                {
                                    var withBlock1 = Core.Type.TempPlayer[index].EventMap.EventPages[Core.Type.TempPlayer[index].EventMap.CurrentEvents];
                                    if (Map[mapNum].Event[i].Pages[z].GraphicType == 1)
                                    {
                                        switch (Map[mapNum].Event[i].Pages[z].GraphicY)
                                        {
                                            case 0:
                                                {
                                                    withBlock1.Dir = (int)DirectionType.Down;
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    withBlock1.Dir = (int)DirectionType.Left;
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    withBlock1.Dir = (int)DirectionType.Right;
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    withBlock1.Dir = (int)DirectionType.Up;
                                                    break;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        withBlock1.Dir = 0;
                                    }
                                    withBlock1.Graphic = Map[mapNum].Event[i].Pages[z].Graphic;
                                    withBlock1.GraphicType = Map[mapNum].Event[i].Pages[z].GraphicType;
                                    withBlock1.GraphicX = Map[mapNum].Event[i].Pages[z].GraphicX;
                                    withBlock1.GraphicY = Map[mapNum].Event[i].Pages[z].GraphicY;
                                    withBlock1.GraphicX2 = Map[mapNum].Event[i].Pages[z].GraphicX2;
                                    withBlock1.GraphicY2 = Map[mapNum].Event[i].Pages[z].GraphicY2;
                                    switch (Map[mapNum].Event[i].Pages[z].MoveSpeed)
                                    {
                                        case 0:
                                            {
                                                withBlock1.MovementSpeed = 2;
                                                break;
                                            }
                                        case 1:
                                            {
                                                withBlock1.MovementSpeed = 3;
                                                break;
                                            }
                                        case 2:
                                            {
                                                withBlock1.MovementSpeed = 4;
                                                break;
                                            }
                                        case 3:
                                            {
                                                withBlock1.MovementSpeed = 6;
                                                break;
                                            }
                                        case 4:
                                            {
                                                withBlock1.MovementSpeed = 12;
                                                break;
                                            }
                                        case 5:
                                            {
                                                withBlock1.MovementSpeed = 24;
                                                break;
                                            }
                                    }
                                    if (Conversions.ToBoolean(Map[mapNum].Event[i].Globals))
                                    {
                                        withBlock1.X = Event.TempEventMap[mapNum].Event[i].X;
                                        withBlock1.Y = Event.TempEventMap[mapNum].Event[i].Y;
                                        withBlock1.Dir = Event.TempEventMap[mapNum].Event[i].Dir;
                                        withBlock1.MoveRouteStep = Event.TempEventMap[mapNum].Event[i].MoveRouteStep;
                                    }
                                    else
                                    {
                                        withBlock1.X = Map[mapNum].Event[i].X;
                                        withBlock1.Y = Map[mapNum].Event[i].Y;
                                        withBlock1.MoveRouteStep = 0;
                                    }
                                    withBlock1.Position = Map[mapNum].Event[i].Pages[z].Position;
                                    withBlock1.EventID = i;
                                    withBlock1.PageID = z;
                                    if (Conversions.ToInteger(spawncurrentevent) == 1)
                                    {
                                        withBlock1.Visible = true;
                                    }
                                    else
                                    {
                                        withBlock1.Visible = false;
                                    }

                                    withBlock1.MoveType = Map[mapNum].Event[i].Pages[z].MoveType;
                                    if (withBlock1.MoveType == 2)
                                    {
                                        withBlock1.MoveRouteCount = Map[mapNum].Event[i].Pages[z].MoveRouteCount;
                                        ;
                                        if (Core.Type.Map[mapNum].Event[i].Pages[z].MoveRouteCount > 0)
                                        {
                                            Array.Resize(ref withBlock.MoveRoute, Core.Type.Map[mapNum].Event[i].Pages[z].MoveRouteCount);
                                            for (p = 0; p < Core.Type.Map[mapNum].Event[i].Pages[z].MoveRouteCount; p++)
                                            {
                                                withBlock.MoveRoute[p] = Core.Type.Map[mapNum].Event[i].Pages[z].MoveRoute[p];
                                            }
                                            withBlock1.MoveRouteComplete = 0;
                                        }
                                        else
                                        {
                                            withBlock1.MoveRouteComplete = 0;
                                        }

                                        if (Map[mapNum].Event[i].Pages[z].MoveRouteCount > 0)
                                        {
                                            var loopTo1 = Map[mapNum].Event[i].Pages[z].MoveRouteCount;
                                            for (p = 0; p <= (int)loopTo1; p++)
                                                withBlock1.MoveRoute[p] = Map[mapNum].Event[i].Pages[z].MoveRoute[p];
                                        }
                                        withBlock1.MoveRouteComplete = 0;
                                    }
                                    else
                                    {
                                        withBlock1.MoveRouteComplete = 0;
                                    }

                                    withBlock1.RepeatMoveRoute = Map[mapNum].Event[i].Pages[z].RepeatMoveRoute;
                                    withBlock1.IgnoreIfCannotMove = Map[mapNum].Event[i].Pages[z].IgnoreMoveRoute;

                                    withBlock1.MoveFreq = Map[mapNum].Event[i].Pages[z].MoveFreq;
                                    withBlock1.MoveSpeed = Map[mapNum].Event[i].Pages[z].MoveSpeed;

                                    withBlock1.WalkingAnim = Map[mapNum].Event[i].Pages[z].WalkAnim;
                                    withBlock1.WalkThrough = Map[mapNum].Event[i].Pages[z].WalkThrough;
                                    withBlock1.ShowName = Map[mapNum].Event[i].Pages[z].ShowName;
                                    withBlock1.FixedDir = Map[mapNum].Event[i].Pages[z].DirFix;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            ByteStream buffer;
            if (Core.Type.TempPlayer[index].EventMap.CurrentEvents > 0)
            {
                var loopTo2 = Core.Type.TempPlayer[index].EventMap.CurrentEvents;
                for (i = 0; i <= (int)loopTo2; i++)
                {
                    if (Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID > 0)
                    {
                        buffer = new ByteStream(4);
                        buffer.WriteInt32((int)(int) ServerPackets.SSpawnEvent);
                        buffer.WriteInt32(Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID);
                        {
                            var withBlock2 = Core.Type.TempPlayer[index].EventMap.EventPages[i];
                            buffer.WriteString(Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Name);
                            buffer.WriteInt32(withBlock2.Dir);
                            buffer.WriteByte(withBlock2.GraphicType);
                            buffer.WriteInt32(withBlock2.Graphic);
                            buffer.WriteInt32(withBlock2.GraphicX);
                            buffer.WriteInt32(withBlock2.GraphicX2);
                            buffer.WriteInt32(withBlock2.GraphicY);
                            buffer.WriteInt32(withBlock2.GraphicY2);
                            buffer.WriteInt32(withBlock2.MovementSpeed);
                            buffer.WriteInt32(withBlock2.X);
                            buffer.WriteInt32(withBlock2.Y);
                            buffer.WriteByte(withBlock2.Position);
                            buffer.WriteBoolean(withBlock2.Visible);
                            buffer.WriteInt32(Map[mapNum].Event[withBlock2.EventID].Pages[withBlock2.PageID].WalkAnim);
                            buffer.WriteInt32(Map[mapNum].Event[withBlock2.EventID].Pages[withBlock2.PageID].DirFix);
                            buffer.WriteInt32(Map[mapNum].Event[withBlock2.EventID].Pages[withBlock2.PageID].WalkThrough);
                            buffer.WriteInt32(Map[mapNum].Event[withBlock2.EventID].Pages[withBlock2.PageID].ShowName);
                        }
                        NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);

                        buffer.Dispose();
                    }
                }
            }

        }

        public static object TriggerEvent(int index, int i, byte triggerType, int x, int y)
        {
            object TriggerEventRet = default;

            // Check if there are any current events
            if (Core.Type.TempPlayer[index].EventMap.CurrentEvents > 0)
            {
                for (int z = 0; z <= Core.Type.TempPlayer[index].EventMap.CurrentEvents; z++)
                {
                    if (Core.Type.TempPlayer[index].EventMap.EventPages[z].EventID == i)
                    {
                        i = z;
                        break;
                    }
                }
            }

            if (Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID == 0)
            {
                return TriggerEventRet;
            }

            if (Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID]
                .Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID].Trigger != triggerType)
            {
                return TriggerEventRet;
            }

            // Determine new x, y based on player direction
            switch (GetPlayerDir(index))
            {
                case (byte)DirectionType.Up:
                    if (GetPlayerY(index) == 0) return TriggerEventRet;
                    x = GetPlayerX(index);
                    y = GetPlayerY(index) - 1;
                    break;

                case (byte)DirectionType.Down:
                    if (GetPlayerY(index) == Map[GetPlayerMap(index)].MaxY) return TriggerEventRet;
                    x = GetPlayerX(index);
                    y = GetPlayerY(index) + 1;
                    break;

                case (byte)DirectionType.Left:
                    if (GetPlayerX(index) == 0) return TriggerEventRet;
                    x = GetPlayerX(index) - 1;
                    y = GetPlayerY(index);
                    break;

                case (byte)DirectionType.Right:
                    if (GetPlayerX(index) == Map[GetPlayerMap(index)].MaxX) return TriggerEventRet;
                    x = GetPlayerX(index) + 1;
                    y = GetPlayerY(index);
                    break;

                case (byte)DirectionType.UpRight:
                    if (GetPlayerX(index) == Map[GetPlayerMap(index)].MaxX || GetPlayerY(index) == 0) return TriggerEventRet;
                    x = GetPlayerX(index) + 1;
                    y = GetPlayerY(index) - 1;
                    break;

                case (byte)DirectionType.UpLeft:
                    if (GetPlayerX(index) == 0 || GetPlayerY(index) == 0) return TriggerEventRet;
                    x = GetPlayerX(index) - 1;
                    y = GetPlayerY(index) - 1;
                    break;

                case (byte)DirectionType.DownLeft:
                    if (GetPlayerX(index) == 0 || GetPlayerY(index) == Map[GetPlayerMap(index)].MaxY) return TriggerEventRet;
                    x = GetPlayerX(index) - 1;
                    y = GetPlayerY(index) + 1;
                    break;

                case (byte)DirectionType.DownRight:
                    if (GetPlayerX(index) == Map[GetPlayerMap(index)].MaxX || GetPlayerY(index) == Map[GetPlayerMap(index)].MaxY) return TriggerEventRet;
                    x = GetPlayerX(index) + 1;
                    y = GetPlayerY(index) + 1;
                    break;
            }

            if (x != Core.Type.TempPlayer[index].EventMap.EventPages[i].X ||
                y != Core.Type.TempPlayer[index].EventMap.EventPages[i].Y)
            {
                return TriggerEventRet;
            }

            // Check if there are any commands to process
            if (Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID]
                .Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID].CommandListCount > 0)
            {
                if (Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID].Active == 0)
                {
                    var eventProcessing = Core.Type.TempPlayer[index].EventProcessing[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID];
                    eventProcessing.Active = 0;
                    eventProcessing.ActionTimer = General.GetTimeMs();
                    eventProcessing.CurList = 0;
                    eventProcessing.CurSlot = 0;
                    eventProcessing.EventID = Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID;
                    eventProcessing.PageID = Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID;
                    eventProcessing.WaitingForResponse = 0;

                    // Handle ReDim (resize array)
                    int commandListCount = Map[GetPlayerMap(index)].Event[Core.Type.TempPlayer[index].EventMap.EventPages[i].EventID]
                        .Pages[Core.Type.TempPlayer[index].EventMap.EventPages[i].PageID].CommandListCount;

                    eventProcessing.ListLeftOff = new int[commandListCount];
                    return TriggerEventRet;
                }
            }

            TriggerEventRet = 0;
            return TriggerEventRet;
        }
    }
}