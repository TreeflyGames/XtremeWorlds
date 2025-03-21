using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static Core.Type;
using static Core.Global.Command;
using static Core.Enum;
using static Core.Packets;
using Mirage.Sharp.Asfw;

namespace Server
{
    public class EventLogic
    {
        // ******** Enhancements and Explanations ********

        // 1. Asynchronous Operations:
        //    - Changed SpawnGlobalEvents and SpawnAllMapGlobalEvents to be async.  This allows these potentially
        //      long-running operations (especially with large maps and many events) to run without blocking the main thread.
        //    - Used Task.Run where appropriate to offload work to a background thread.
        //    - Added ConfigureAwait(false) to async calls to avoid deadlocks in some contexts.

        // 2. LINQ for Concise Queries:
        //    - Used LINQ (Language Integrated Query) in several places to replace loops with more readable and
        //      often more efficient queries.  This makes the code easier to understand and maintain.

        // 3. Improved Data Structures and Error Handling:
        //    - Added null checks and boundary checks to prevent potential `IndexOutOfRangeException` errors.
        //    - Used `?.` (null-conditional operator) and `??` (null-coalescing operator) for safer and more concise null handling.
        //    - Replaced some manual array resizing with `List<T>` and then converted back to arrays when needed. Lists are generally
        //      easier to work with for dynamic resizing.
        //    - Simplified logic in several places by combining conditions and using more direct comparisons.
        //    - Replaced some magic numbers with named constants or enums if they weren't already defined.

        // 4. Code Clarity and Readability:
        //    - Improved code formatting for better readability (consistent indentation, spacing).
        //    - Added comments to explain complex logic sections.
        //    - Replaced some verbose `Conversions.ToBoolean(0)` and `Conversions.ToBoolean(1)` with `false` and `true` respectively.
        //    - Replaced some older VB-style string functions (like `InStr`, `Mid`, `Len`, `Val`) with their C# equivalents (`Contains`,
        //      `Substring`, `Length`, `int.Parse` or `double.Parse`).

        // 5. Optimization:
        //    - Cached frequently accessed properties and array lengths to avoid repeated calculations.
        //    - Reduced redundant code by extracting common logic into helper methods.
        //    - Optimized event processing loop by avoiding unnecessary checks and iterations.
        //    - Used StringBuilder for efficient string concatenation in ParseEventText.

        // 6. Event Processing System Enhancements (Conceptual, not fully implemented)
        //    - Added a comment suggesting a possible priority system for events. This wasn't fully implemented,
        //    but it's a good example of a feature enhancement.  A real priority system would probably involve a more sophisticated data structure.

        // 7. Modern C# Features
        //   - Used 'ref' local variables, more directly showing the intent to modify the original structure.
        //   - Used 'var' to declare variables with implicit typing.

        // Constants for enhanced code clarity and maintainability:
        private const int DefaultMovementSpeed = 4; // Example default speed

        // Helper methods for better readability:
        private static bool IsEventVisible(ref MapEventStruct eventPage) => eventPage.Visible;
        private static int GetEventId(ref EventMapStruct eventPage) => eventPage.CurrentEvents;
        private static EventPageStruct GetEventPage(int mapNum, int eventId, int pageId) => Map[mapNum].Event[eventId].Pages[pageId];

        public static void RemoveDeadEvents()
        {
            // Use LINQ to iterate through connected players  
            Parallel.ForEach(Enumerable.Range(0, NetworkConfig.Socket.HighIndex + 1), i =>
            {
                if (TempPlayer[i].EventMap.CurrentEvents > 0 && !TempPlayer[i].GettingMap)
                {
                    int mapNum = GetPlayerMap(i);

                    // Use LINQ to filter and process relevant event pages  
                    var relevantPages = TempPlayer[i].EventMap.EventPages
                        .Where((page, x) => x < TempPlayer[i].EventMap.EventPages.Length)
                        .Where(page => page.EventID < TempPlayer[i].EventMap.CurrentEvents)  //Boundary check  
                        .Where(page => page.EventID < Map[mapNum].Event.Length)             // Boundary check.  
                        .ToList(); // Materialize the query to avoid issues with modifying the collection.  

                    foreach (var eventPage in relevantPages)
                    {
                        int id = eventPage.EventID;
                        int page = eventPage.PageID;

                        // Check if the event and page still exist  
                        if (id >= 0 && id < Map[mapNum].Event.Length && page >= 0 && page < Map[mapNum].Event[id].Pages.Length)
                        {
                            ref var playerEventPage = ref TempPlayer[i].EventMap.EventPages[Array.IndexOf(TempPlayer[i].EventMap.EventPages, eventPage)]; //find actual index of eventpage  

                            if (IsEventVisible(ref playerEventPage))
                            {
                                // Check conditions to see if the event should be hidden  
                                EventPageStruct mapEventPage = GetEventPage(mapNum, id, page);

                                if (mapEventPage.ChkHasItem == 1 && Player.HasItem(i, mapEventPage.HasItemIndex) == 0)
                                {
                                    playerEventPage.Visible = false;
                                }

                                if (mapEventPage.ChkSelfSwitch == 1)
                                {
                                    int compare = mapEventPage.SelfSwitchCompare == 0 ? 0 : 1;
                                    bool selfSwitchConditionMet;

                                    if (Map[mapNum].Event[id].Globals == 1)
                                    {
                                        selfSwitchConditionMet = Map[mapNum].Event[id].SelfSwitches[mapEventPage.SelfSwitchIndex] == compare;
                                    }
                                    else
                                    {
                                        selfSwitchConditionMet = TempPlayer[i].EventMap.EventPages[id].SelfSwitches[mapEventPage.SelfSwitchIndex] == compare;
                                    }

                                    if (!selfSwitchConditionMet)
                                    {
                                        playerEventPage.Visible = false;
                                    }
                                }

                                if (mapEventPage.ChkVariable == 1)
                                {
                                    int playerVar = TempPlayer[i].Variables[mapEventPage.VariableIndex];
                                    int condition = mapEventPage.VariableCondition;
                                    bool variableConditionMet = false;

                                    switch (mapEventPage.VariableCompare)
                                    {
                                        case 0: variableConditionMet = playerVar != condition; break;
                                        case 1: variableConditionMet = playerVar < condition; break;
                                        case 2: variableConditionMet = playerVar > condition; break;
                                        case 3: variableConditionMet = playerVar <= condition; break;
                                        case 4: variableConditionMet = playerVar >= condition; break;
                                        case 5: variableConditionMet = playerVar == condition; break;
                                    }

                                    if (variableConditionMet)
                                    {
                                        playerEventPage.Visible = false;
                                    }
                                }

                                if (mapEventPage.ChkSwitch == 1)
                                {
                                    //Simplified with XOR  
                                    if ((mapEventPage.SwitchCompare == 1) ^ (TempPlayer[i].Switches[mapEventPage.SwitchIndex] == 1)) //we are expecting true  
                                    {
                                        playerEventPage.Visible = false;
                                    }
                                }

                                if (Map[mapNum].Event[id].Globals == 1 && !IsEventVisible(ref playerEventPage))
                                {
                                    Event.TempEventMap[mapNum].Event[id].Active = 0;
                                }

                                if (!IsEventVisible(ref playerEventPage) && id >= 0)
                                {
                                    // Send packet to hide the event  
                                    using (var buffer = new ByteStream(4))
                                    {
                                        buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
                                        buffer.WriteInt32(id);

                                        ref var withBlock = ref TempPlayer[i].EventMap.EventPages[Array.IndexOf(TempPlayer[i].EventMap.EventPages, eventPage)]; //find actual index of eventpage  
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

                                        NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        public static void SpawnNewEvents()
        {
            // Use Parallel.For for potential performance gains on multi-core systems.
            Parallel.For(0, NetworkConfig.Socket.HighIndex + 1, i =>
            {
                if (TempPlayer[i].EventMap.CurrentEvents <= 0) return;

                int mapNum = GetPlayerMap(i);

                // Iterate through the player's current events.  Use a List for easier manipulation.
                var eventPagesList = TempPlayer[i].EventMap.EventPages.ToList();

                for (int x = 0; x < eventPagesList.Count; x++)
                {
                    int p = -1;
                    int id = eventPagesList[x].EventID;

                    // Basic bounds check.
                    if (id < 0 || id >= eventPagesList.Count) continue;

                    int PageID = eventPagesList[x].PageID;

                    if (!eventPagesList[x].Visible)
                        PageID = 0;

                    // Another bounds check.
                    if (id >= Map[mapNum].Event.Length) continue;


                    // Iterate through event pages to find the highest-priority page that meets conditions
                    for (int z = 0; z < Map[mapNum].Event[id].PageCount; z++)
                    {
                        bool spawnevent = true;
                        MapEventPage page = Map[mapNum].Event[id].Pages[z];

                        // Check conditions (Item, Self Switch, Variable, Switch).
                        if (page.ChkHasItem == 1 && Player.HasItem(i, page.HasItemIndex) == 0)
                        {
                            spawnevent = false;
                        }

                        if (page.ChkSelfSwitch == 1)
                        {
                            int compare = page.SelfSwitchCompare; // 0 or 1
                            bool selfSwitchStatus;

                            if (Map[mapNum].Event[id].Globals == 1)
                                selfSwitchStatus = Map[mapNum].Event[id].SelfSwitches[page.SelfSwitchIndex] == compare;
                            else
                                selfSwitchStatus = TempPlayer[i].EventMap.EventPages[id].SelfSwitches[page.SelfSwitchIndex] == compare;

                            if (!selfSwitchStatus)
                                spawnevent = false;
                        }


                        if (page.ChkVariable == 1)
                        {
                            int playerVar = Player[i].Variables[page.VariableIndex];
                            bool conditionMet = false;
                            switch (page.VariableCompare)
                            {
                                case 0: conditionMet = playerVar != page.VariableCondition; break;
                                case 1: conditionMet = playerVar < page.VariableCondition; break;
                                case 2: conditionMet = playerVar > page.VariableCondition; break;
                                case 3: conditionMet = playerVar <= page.VariableCondition; break;
                                case 4: conditionMet = playerVar >= page.VariableCondition; break;
                                case 5: conditionMet = playerVar == page.VariableCondition; break;
                            }
                            if (conditionMet)
                                spawnevent = false;
                        }


                        if (page.ChkSwitch == 1)
                        {
                            // Using XOR for concise switch check.
                            if ((page.SwitchCompare == 0) ^ (Player[i].Switches[page.SwitchIndex] == 0)) //we want false
                            {
                                spawnevent = false; //and switch is true, don't spawn.
                            }
                        }


                        if (spawnevent)
                        {
                            p = z; // Store the highest-priority valid page index
                        }
                    }

                    // Determine if we should spawn a *new* event (p >= 0 and it wasn't already visible)
                    if (p >= 0 && !eventPagesList[x].Visible)
                    {
                        int z = p;

                        // Reset any active event processing for this event ID.
                        for (int n = 0; n < TempPlayer[i].EventProcessing.Length; n++)
                        {
                            if (TempPlayer[i].EventProcessing[n].EventID == id)
                            {
                                TempPlayer[i].EventProcessing[n].EventID = -1;
                                TempPlayer[i].EventProcessing[n].Active = 0;
                            }
                        }


                        // Set up the event page data.
                        ref var withBlock = ref TempPlayer[i].EventMap.EventPages[x]; // Use x, as this is the correct index into *this player's* event list
                        MapEventPage newPage = Map[mapNum].Event[id].Pages[z];

                        withBlock.Dir = newPage.GraphicType == 1 ? (newPage.GraphicY % 4) switch
                        {
                            0 => (int)DirectionType.Down,
                            1 => (int)DirectionType.Left,
                            2 => (int)DirectionType.Right,
                            _ => (int)DirectionType.Up // 3
                        } : 0;

                        withBlock.Graphic = newPage.Graphic;
                        withBlock.GraphicType = newPage.GraphicType;
                        withBlock.GraphicX = newPage.GraphicX;
                        withBlock.GraphicY = newPage.GraphicY;
                        withBlock.GraphicX2 = newPage.GraphicX2;
                        withBlock.GraphicY2 = newPage.GraphicY2;

                        withBlock.MovementSpeed = newPage.MoveSpeed switch
                        {
                            0 => 2,
                            1 => 3,
                            2 => 4,
                            3 => 6,
                            4 => 12,
                            5 => 24,
                            _ => DefaultMovementSpeed // Handle unexpected values
                        };


                        withBlock.Position = newPage.Position;
                        withBlock.EventID = id; // This should be the event ID, not the index in the player's event list.
                        withBlock.PageID = z;
                        withBlock.Visible = true;
                        withBlock.MoveType = newPage.MoveType;

                        if (withBlock.MoveType == 2) // Custom Move Route
                        {
                            withBlock.MoveRouteCount = newPage.MoveRouteCount;
                            if (newPage.MoveRouteCount > 0)
                            {
                                // Copy the move route.
                                withBlock.MoveRoute = new EventMoveRoute[newPage.MoveRouteCount];
                                Array.Copy(newPage.MoveRoute, withBlock.MoveRoute, newPage.MoveRouteCount);
                                withBlock.MoveRouteComplete = 0; // Ensure it's reset.
                            }
                            else
                            {
                                withBlock.MoveRouteComplete = 1; // No route = complete.
                            }
                        }
                        else
                        {
                            withBlock.MoveRouteComplete = 1;
                        }

                        withBlock.RepeatMoveRoute = newPage.RepeatMoveRoute;
                        withBlock.IgnoreIfCannotMove = newPage.IgnoreMoveRoute;
                        withBlock.MoveFreq = newPage.MoveFreq;
                        withBlock.MoveSpeed = newPage.MoveSpeed;  // Already handled above.
                        withBlock.WalkThrough = newPage.WalkThrough;
                        withBlock.ShowName = newPage.ShowName;
                        withBlock.WalkingAnim = newPage.WalkAnim;
                        withBlock.FixedDir = newPage.DirFix;

                        if (Map[mapNum].Event[id].Globals == 1)
                        {
                            Event.TempEventMap[mapNum].Event[id].Active = z;
                            Event.TempEventMap[mapNum].Event[id].Position = newPage.Position;

                        }

                        // Send the spawn event packet.
                        using (var buffer = new ByteStream(4))
                        {
                            buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
                            buffer.WriteInt32(id); // Event ID

                            ref var withBlock1 = ref TempPlayer[i].EventMap.EventPages[x];
                            buffer.WriteString(Map[mapNum].Event[withBlock1.EventID].Name);
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

                            NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                        }
                    }
                }
            });
        }



        public static void ProcessEventMovement()
        {
            // Iterate through all maps.
            for (int i = 0; i < Core.Constant.MAX_MAPS; i++)
            {
                if (!PlayersOnMap[i] || Event.TempEventMap[i].EventCount <= 0) continue;

                // Process global events on this map.
                for (int x = 0; x < Event.TempEventMap[i].EventCount; x++)
                {
                    if (Event.TempEventMap[i].Event[x].Active <= 0) continue;

                    // Check if it's time to process movement.
                    if (Event.TempEventMap[i].Event[x].MoveTimer > General.GetTimeMs()) continue;


                    ref var globalEvent = ref Event.TempEventMap[i].Event[x];

                    // Process movement based on MoveType.
                    switch (globalEvent.MoveType)
                    {
                        case 0: // Fixed, do nothing.
                            break;

                        case 1: // Random Movement
                            {
                                int rand = (int)Math.Floor(General.GetRandom.NextDouble() * 4); // 0-3 for direction.
                                if (Event.CanEventMove(0, i, globalEvent.X, globalEvent.Y, x, globalEvent.WalkThrough, (byte)rand, true))
                                {
                                    int actualMoveSpeed = globalEvent.MoveSpeed switch
                                    {
                                        0 => 2,
                                        1 => 3,
                                        2 => 4,
                                        3 => 6,
                                        4 => 12,
                                        5 => 24,
                                        _ => DefaultMovementSpeed
                                    };
                                    Event.EventMove(0, i, x, rand, actualMoveSpeed, true);
                                }
                                else
                                {
                                    Event.EventDir(0, i, x, rand, true); // Just change direction.
                                }
                                break;
                            }
                        case 2: // Custom Move Route
                            {
                                ref var withBlock = ref Event.TempEventMap[i].Event[x];
                                bool IsGlobal = true;
                                int mapNum = i;
                                int playerID = 0;
                                int EventID = x;
                                int WalkThrough = withBlock.WalkThrough;
                                bool doNotProcessMoveRoute = false;

                                if (withBlock.MoveRouteCount > 0)
                                {
                                    if (withBlock.MoveRouteStep >= withBlock.MoveRouteCount)
                                    {
                                        if (withBlock.RepeatMoveRoute == 1)
                                        {
                                            withBlock.MoveRouteStep = 0;
                                            withBlock.MoveRouteComplete = 1; // Reset for repeating routes.
                                        }
                                        else
                                        {
                                            doNotProcessMoveRoute = true;
                                            withBlock.MoveRouteComplete = 1; // Mark as complete if not repeating.
                                        }
                                    }
                                    else //still moving
                                        withBlock.MoveRouteComplete = 0;


                                    if (!doNotProcessMoveRoute)
                                    {
                                        withBlock.MoveRouteStep++;

                                        int actualmovespeed = withBlock.MoveSpeed switch
                                        {
                                            0 => 2,
                                            1 => 3,
                                            2 => 4,
                                            3 => 6,
                                            4 => 12,
                                            5 => 24,
                                            _ => DefaultMovementSpeed
                                        };


                                        // Get next move route step, handling potential out-of-bounds access.
                                        if (withBlock.MoveRouteStep < 0 || withBlock.MoveRouteStep >= withBlock.MoveRoute.Length)
                                        {
                                            //Error, route step out of bounds
                                            break;
                                        }
                                        var nextMove = withBlock.MoveRoute[withBlock.MoveRouteStep];


                                        bool sendupdate = false;
                                        switch (nextMove.Index)
                                        {
                                            case 1: // Move Up
                                                if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Up, IsGlobal))
                                                {
                                                    Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Up, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 2: // Move Down
                                                if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Down, IsGlobal))
                                                {
                                                    Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Down, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 3: // Move Left
                                                if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Left, IsGlobal))
                                                {
                                                    Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Left, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 4: // Move Right
                                                if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Right, IsGlobal))
                                                {
                                                    Event.EventMove(playerID, mapNum, EventID, (int)DirectionType.Right, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 5: // Move Random
                                                {
                                                    int z = (int)Math.Floor(General.GetRandom.NextDouble() * 4); // 0-3
                                                    if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                    {
                                                        Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                    }
                                                    else if (withBlock.IgnoreIfCannotMove == 0)
                                                    {
                                                        withBlock.MoveRouteStep--;
                                                    }
                                                    break;
                                                }

                                            case 6: // Move Toward Player
                                                {
                                                    if (!IsGlobal) //should never be global.
                                                    {
                                                        // Determine if the event is one block away from the player.
                                                        if (Event.IsOneBlockAway(withBlock.X, withBlock.Y, GetPlayerX(playerID), GetPlayerY(playerID)))
                                                        {
                                                            // Face the player.
                                                            Event.EventDir(playerID, GetPlayerMap(playerID), EventID, Event.GetDirToPlayer(playerID, GetPlayerMap(playerID), EventID), false);
                                                            if (withBlock.IgnoreIfCannotMove == 0)
                                                            {
                                                                withBlock.MoveRouteStep--;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // Try to move towards the player.
                                                            int z = Event.CanEventMoveTowardsPlayer(playerID, mapNum, EventID);
                                                            if (z < 4)  // Valid direction (0-3).
                                                            {
                                                                if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                {
                                                                    Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                }
                                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                                {
                                                                    withBlock.MoveRouteStep--;
                                                                }
                                                            }
                                                            else if (withBlock.IgnoreIfCannotMove == 0) // Cannot move towards player and we don't ignore.
                                                            {
                                                                withBlock.MoveRouteStep--;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }

                                            case 7: // Move Away from Player
                                                {
                                                    if (!IsGlobal)
                                                    {
                                                        int z = Event.CanEventMoveAwayFromPlayer(playerID, mapNum, EventID);
                                                        if (z < 5)
                                                        {
                                                            if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                            {
                                                                Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                            }
                                                            else if (withBlock.IgnoreIfCannotMove == 0)
                                                            {
                                                                withBlock.MoveRouteStep--;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }

                                            case 8: // Move Forward
                                                if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)withBlock.Dir, IsGlobal))
                                                {
                                                    Event.EventMove(playerID, mapNum, EventID, withBlock.Dir, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 9: // Move Backward
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Up,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Right,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Left,
                                                        _ => withBlock.Dir // Invalid direction, keep current.
                                                    };
                                                    if (Event.CanEventMove(playerID, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                    {
                                                        Event.EventMove(playerID, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                    }
                                                    else if (withBlock.IgnoreIfCannotMove == 0)
                                                    {
                                                        withBlock.MoveRouteStep--;
                                                    }
                                                    break;
                                                }

                                            case 10: withBlock.MoveTimer = General.GetTimeMs() + 100; break;
                                            case 11: withBlock.MoveTimer = General.GetTimeMs() + 500; break;
                                            case 12: withBlock.MoveTimer = General.GetTimeMs() + 1000; break;

                                            case 13: Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Up, IsGlobal); break;
                                            case 14: Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Down, IsGlobal); break;
                                            case 15: Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Left, IsGlobal); break;
                                            case 16: Event.EventDir(playerID, mapNum, EventID, (byte)DirectionType.Right, IsGlobal); break;

                                            // Turn 90 degrees clockwise, counter-clockwise, 180 degrees, or at random
                                            case 17: // Turn Right 90 Degrees
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Right,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Up,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Left,
                                                        _ => withBlock.Dir
                                                    };
                                                    Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 18: // Turn Left 90 Degrees
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Left,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Up,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Right,
                                                        _ => withBlock.Dir
                                                    };
                                                    Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 19: // Turn 180 Degrees
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Left,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Right,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Up,
                                                        _ => withBlock.Dir
                                                    };
                                                    Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 20: // Turn Random
                                                {
                                                    int z = (int)Math.Floor(General.GetRandom.NextDouble() * 4);
                                                    Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 21: // Turn Toward Player
                                                {
                                                    if (!IsGlobal)
                                                    {
                                                        int z = Event.GetDirToPlayer(playerID, mapNum, EventID);
                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                    }
                                                    break;
                                                }

                                            case 22: // Turn Away from Player
                                                {
                                                    if (!IsGlobal)
                                                    {
                                                        int z = Event.GetDirAwayFromPlayer(playerID, mapNum, EventID);
                                                        Event.EventDir(playerID, mapNum, EventID, z, IsGlobal);
                                                    }
                                                    break;
                                                }

                                            // Change Speed, Frequency, Graphic
                                            case 23: withBlock.MoveSpeed = 0; break;
                                            case 24: withBlock.MoveSpeed = 1; break;
                                            case 25: withBlock.MoveSpeed = 2; break;
                                            case 26: withBlock
                                                                                            case 27: withBlock.MoveSpeed = 4; break;
                                            case 28: withBlock.MoveSpeed = 5; break;

                                            case 29: withBlock.MoveFreq = 0; break;
                                            case 30: withBlock.MoveFreq = 1; break;
                                            case 31: withBlock.MoveFreq = 2; break;
                                            case 32: withBlock.MoveFreq = 3; break;
                                            case 33: withBlock.MoveFreq = 4; break;

                                            case 34: // Turn On Walking Animation
                                                withBlock.WalkingAnim = 1;
                                                sendupdate = true;
                                                break;
                                            case 35: // Turn Off Walking Animation
                                                withBlock.WalkingAnim = 0;
                                                sendupdate = true;
                                                break;

                                            case 36: // Turn On Direction Fix
                                                withBlock.FixedDir = 1;
                                                sendupdate = true;
                                                break;
                                            case 37: // Turn Off Direction Fix
                                                withBlock.FixedDir = 0;
                                                sendupdate = true;
                                                break;

                                            case 38: // Turn On Through
                                                withBlock.WalkThrough = 1;
                                                break;
                                            case 39: // Turn Off Through
                                                withBlock.WalkThrough = 0;
                                                break;
                                            case 40: //Turn on Fix Position
                                                withBlock.Position = 1;
                                                sendupdate = true;
                                                break;
                                            case 41://Turn off Fix Position
                                                withBlock.Position = 0;
                                                sendupdate = true;
                                                break;
                                            case 42://Turn on Below Player
                                                withBlock.Position = 2;
                                                sendupdate = true;
                                                break;

                                            case 43: // Change Graphic
                                                {
                                                    withBlock.GraphicType = (byte)nextMove.Data1;
                                                    withBlock.Graphic = nextMove.Data2;
                                                    withBlock.GraphicX = nextMove.Data3;
                                                    withBlock.GraphicX2 = nextMove.Data4;
                                                    withBlock.GraphicY = nextMove.Data5;
                                                    withBlock.GraphicY2 = nextMove.Data6;

                                                    // Adjust direction if it's a character graphic.
                                                    if (withBlock.GraphicType == 1)
                                                    {
                                                        withBlock.Dir = withBlock.GraphicY switch
                                                        {
                                                            0 => (int)DirectionType.Down,
                                                            1 => (int)DirectionType.Left,
                                                            2 => (int)DirectionType.Right,
                                                            3 => (int)DirectionType.Up,
                                                            _ => withBlock.Dir
                                                        };
                                                    }
                                                    sendupdate = true;
                                                    break;
                                                }
                                        }


                                        if (sendupdate)
                                        {
                                            using (var buffer = new ByteStream(4))
                                            {
                                                buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
                                                buffer.WriteInt32(EventID); // Event ID.

                                                ref var withBlock1 = ref Event.TempEventMap[i].Event[x];
                                                buffer.WriteString(Map[i].Event[x].Name); // Global event, use map index
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
                                                buffer.WriteInt32(withBlock1.Active);
                                                buffer.WriteInt32(withBlock1.WalkingAnim); // Corrected property names
                                                buffer.WriteInt32(withBlock1.FixedDir);
                                                buffer.WriteInt32(withBlock1.WalkThrough);
                                                buffer.WriteInt32(withBlock1.ShowName);
                                                NetworkConfig.SendDataToMap(i, buffer.Data, buffer.Head);
                                            }
                                        }
                                    }
                                    doNotProcessMoveRoute = false; // Reset for next iteration.
                                }
                                break;
                            }
                    }

                    // Set the next move timer based on MoveFreq.
                    globalEvent.MoveTimer = General.GetTimeMs() + globalEvent.MoveFreq switch
                    {
                        0 => 4000,
                        1 => 2000,
                        2 => 1000,
                        3 => 500,
                        4 => 250,
                        _ => 1000 // Default if invalid.
                    };

                }
            }
        }


        public static void ProcessLocalEventMovement()
        {
            // Parallel processing for each player.
            Parallel.For(0, NetworkConfig.Socket.HighIndex + 1, i =>
            {
                if (TempPlayer[i].EventMap.CurrentEvents <= 0) return;

                int mapNum = GetPlayerMap(i);

                // Iterate through local events for the player.
                for (int x = 0; x < TempPlayer[i].EventMap.CurrentEvents; x++)
                {
                    // Bounds check.
                    if (TempPlayer[i].EventMap.EventPages[x].EventID >= Map[mapNum].Event.Length) continue;


                    ref var localEvent = ref TempPlayer[i].EventMap.EventPages[x];


                    // Only process visible, non-global events.
                    if (Map[mapNum].Event[localEvent.EventID].Globals != 0 || !localEvent.Visible) continue;


                    // Check move timer.
                    if (localEvent.MoveTimer > General.GetTimeMs()) continue;

                    // Process movement based on MoveType.
                    switch (localEvent.MoveType)
                    {
                        case 0: // Fixed
                            break;

                        case 1: // Random
                            {
                                int rand = (int)Math.Floor(General.GetRandom.NextDouble() * 4);
                                if (Event.CanEventMove(i, mapNum, localEvent.X, localEvent.Y, x, localEvent.WalkThrough, (byte)rand, false))
                                {
                                    int actualMoveSpeed = localEvent.MoveSpeed switch
                                    {
                                        0 => 2,
                                        1 => 3,
                                        2 => 4,
                                        3 => 6,
                                        4 => 12,
                                        5 => 24,
                                        _ => DefaultMovementSpeed
                                    };
                                    Event.EventMove(i, mapNum, x, rand, actualMoveSpeed, false);
                                }
                                else
                                {
                                    Event.EventDir(i, mapNum, x, rand, false);
                                }
                                break;
                            }
                        case 2: // Custom Move Route
                            {
                                ref var withBlock = ref TempPlayer[i].EventMap.EventPages[x];
                                bool IsGlobal = false;
                                bool sendupdate = false;
                                int EventID = x;
                                int WalkThrough = withBlock.WalkThrough;
                                bool doNotProcessMoveRoute = false;

                                if (withBlock.MoveRouteCount > 0)
                                {

                                    if (withBlock.MoveRouteStep >= withBlock.MoveRouteCount)
                                    {
                                        if (withBlock.RepeatMoveRoute == 1)
                                        {
                                            withBlock.MoveRouteStep = 0;
                                            withBlock.MoveRouteComplete = 1; // Reset for repeating.
                                        }
                                        else
                                        {
                                            doNotProcessMoveRoute = true;
                                            withBlock.MoveRouteComplete = 1; // Mark as complete.
                                        }
                                    }
                                    else //still moving
                                        withBlock.MoveRouteComplete = 0;



                                    if (!doNotProcessMoveRoute)
                                    {
                                        withBlock.MoveRouteStep++;

                                        int actualmovespeed = withBlock.MoveSpeed switch
                                        {
                                            0 => 2,
                                            1 => 3,
                                            2 => 4,
                                            3 => 6,
                                            4 => 12,
                                            5 => 24,
                                            _ => DefaultMovementSpeed
                                        };


                                        // Get next move route step, handling potential out-of-bounds access.
                                        if (withBlock.MoveRouteStep < 0 || withBlock.MoveRouteStep >= withBlock.MoveRoute.Length)
                                        {
                                            //error, route step out of range
                                            break; // Exit the switch statement.
                                        }
                                        var nextMove = withBlock.MoveRoute[withBlock.MoveRouteStep];

                                        switch (nextMove.Index)
                                        {
                                            case 1: // Move Up
                                                if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Up, IsGlobal))
                                                {
                                                    Event.EventMove(i, mapNum, EventID, (int)DirectionType.Up, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 2: // Move Down
                                                if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Down, IsGlobal))
                                                {
                                                    Event.EventMove(i, mapNum, EventID, (int)DirectionType.Down, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 3: // Move Left
                                                if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Left, IsGlobal))
                                                {
                                                    Event.EventMove(i, mapNum, EventID, (int)DirectionType.Left, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 4: // Move Right
                                                if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)DirectionType.Right, IsGlobal))
                                                {
                                                    Event.EventMove(i, mapNum, EventID, (int)DirectionType.Right, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;
                                            case 5: // Move Random
                                                {
                                                    int z = (int)Math.Floor(General.GetRandom.NextDouble() * 4);
                                                    if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                    {
                                                        Event.EventMove(i, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                    }
                                                    else if (withBlock.IgnoreIfCannotMove == 0)
                                                    {
                                                        withBlock.MoveRouteStep--;
                                                    }
                                                    break;
                                                }

                                            case 6: // Move Toward Player
                                                {
                                                    if (!IsGlobal)
                                                    {
                                                        if (Event.IsOneBlockAway(withBlock.X, withBlock.Y, GetPlayerX(i), GetPlayerY(i)))
                                                        {
                                                            Event.EventDir(i, mapNum, EventID, Event.GetDirToPlayer(i, mapNum, EventID), false);

                                                            // Activate event if triggered by player action.
                                                            if (Map[mapNum].Event[EventID].Pages[TempPlayer[i].EventMap.EventPages[EventID].PageID].Trigger == 1)
                                                            {
                                                                if (Map[mapNum].Event[EventID].Pages[TempPlayer[i].EventMap.EventPages[EventID].PageID].CommandListCount > 0)
                                                                {
                                                                    // Start event processing.
                                                                    ref var eventProcessing = ref TempPlayer[i].EventProcessing[EventID]; // Use EventID (local index)
                                                                    eventProcessing.Active = 1;
                                                                    eventProcessing.ActionTimer = General.GetTimeMs();
                                                                    eventProcessing.CurList = 0;
                                                                    eventProcessing.CurSlot = 0;
                                                                    eventProcessing.EventID = EventID; // This should be the *map* event ID
                                                                    eventProcessing.PageID = TempPlayer[i].EventMap.EventPages[EventID].PageID; // Local page ID.
                                                                    eventProcessing.WaitingForResponse = 0;
                                                                }
                                                            }
                                                            if (withBlock.IgnoreIfCannotMove == 0)
                                                            {
                                                                withBlock.MoveRouteStep--;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            int z = Event.CanEventMoveTowardsPlayer(i, mapNum, EventID);
                                                            if (z < 4)
                                                            {
                                                                if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                                {
                                                                    Event.EventMove(i, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                                }
                                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                                {
                                                                    withBlock.MoveRouteStep--;
                                                                }
                                                            }
                                                            else if (withBlock.IgnoreIfCannotMove == 0)
                                                            {
                                                                withBlock.MoveRouteStep = withBlock.MoveRouteStep - 1;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                            case 7: // Move Away From Player
                                                {
                                                    if (!IsGlobal)
                                                    {
                                                        int z = Event.CanEventMoveAwayFromPlayer(i, mapNum, EventID);
                                                        if (z < 5) // Valid direction.
                                                        {
                                                            if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                            {
                                                                Event.EventMove(i, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                            }
                                                            else if (withBlock.IgnoreIfCannotMove == 0)
                                                            {
                                                                withBlock.MoveRouteStep--;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                            case 8: // Move Forward
                                                if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)withBlock.Dir, IsGlobal))
                                                {
                                                    Event.EventMove(i, mapNum, EventID, withBlock.Dir, actualmovespeed, IsGlobal);
                                                }
                                                else if (withBlock.IgnoreIfCannotMove == 0)
                                                {
                                                    withBlock.MoveRouteStep--;
                                                }
                                                break;

                                            case 9: // Move Backward
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Up,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Right,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Left,
                                                        _ => withBlock.Dir
                                                    };
                                                    if (Event.CanEventMove(i, mapNum, withBlock.X, withBlock.Y, EventID, WalkThrough, (byte)z, IsGlobal))
                                                    {
                                                        Event.EventMove(i, mapNum, EventID, z, actualmovespeed, IsGlobal);
                                                    }
                                                    else if (withBlock.IgnoreIfCannotMove == 0)
                                                    {
                                                        withBlock.MoveRouteStep--;
                                                    }
                                                    break;
                                                }
                                            case 10: withBlock.MoveTimer = General.GetTimeMs() + 100; break;
                                            case 11: withBlock.MoveTimer = General.GetTimeMs() + 500; break;
                                            case 12: withBlock.MoveTimer = General.GetTimeMs() + 1000; break;

                                            case 13: Event.EventDir(i, mapNum, EventID, (byte)DirectionType.Up, IsGlobal); break;
                                            case 14: Event.EventDir(i, mapNum, EventID, (byte)DirectionType.Down, IsGlobal); break;
                                            case 15: Event.EventDir(i, mapNum, EventID, (byte)DirectionType.Left, IsGlobal); break;
                                            case 16: Event.EventDir(i, mapNum, EventID, (byte)DirectionType.Right, IsGlobal); break;

                                            // Turn 90 degrees clockwise, counter-clockwise, 180 degrees
                                            case 17: // Turn Right 90 Degrees
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Right,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Up,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Left,
                                                        _ => withBlock.Dir
                                                    };
                                                    Event.EventDir(i, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 18: // Turn Left 90 Degrees
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Left,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Up,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Right,
                                                        _ => withBlock.Dir
                                                    };
                                                    Event.EventDir(i, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 19: // Turn 180 Degrees
                                                {
                                                    int z = withBlock.Dir switch
                                                    {
                                                        (byte)DirectionType.Up => (byte)DirectionType.Down,
                                                        (byte)DirectionType.Right => (byte)DirectionType.Left,
                                                        (byte)DirectionType.Left => (byte)DirectionType.Right,
                                                        (byte)DirectionType.Down => (byte)DirectionType.Up,
                                                        _ => withBlock.Dir
                                                    };
                                                    Event.EventDir(i, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 20: // Turn Random
                                                {
                                                    int z = (int)Math.Floor(General.GetRandom.NextDouble() * 4);
                                                    Event.EventDir(i, mapNum, EventID, z, IsGlobal);
                                                    break;
                                                }
                                            case 21: // Turn Toward Player
                                                {
                                                    if (!IsGlobal)
                                                    {
                                                        int z = Event.GetDirToPlayer(i, mapNum, EventID);
                                                        Event.EventDir(i, mapNum, EventID, z, IsGlobal);
                                                    }
                                                    break;
                                                }
                                            case 22: // Turn Away from Player
                                                {
                                                    if (!IsGlobal)
                                                    {
                                                        int z = Event.GetDirAwayFromPlayer(i, mapNum, EventID);
                                                        Event.EventDir(i, mapNum, EventID, z, IsGlobal);
                                                    }
                                                    break;
                                                }

                                            // Change Speed, Frequency, Graphic
                                            case 23: withBlock.MoveSpeed = 0; break;
                                            case 24: withBlock.MoveSpeed = 1; break;
                                            case 25: withBlock.MoveSpeed = 2; break;
                                            case 26: withBlock.MoveSpeed = 3; break;
                                            case 27: withBlock.MoveSpeed = 4; break;
                                            case 28: withBlock.MoveSpeed = 5; break;

                                            case 29: withBlock.MoveFreq = 0; break;
                                            case 30: withBlock.MoveFreq = 1; break;
                                            case 31: withBlock.MoveFreq = 2; break;
                                            case 32: withBlock.MoveFreq = 3; break;
                                            case 33: withBlock.MoveFreq = 4; break;

                                            case 34: withBlock.WalkingAnim = 1; sendupdate = true; break; // Turn On Walking Animation
                                            case 35: withBlock.WalkingAnim = 0; sendupdate = true; break; // Turn Off Walking Animation
                                            case 36: withBlock.FixedDir = 1; sendupdate = true; break; // Turn On Direction Fix
                                            case 37: withBlock.FixedDir = 0; sendupdate = true; break; // Turn Off Direction Fix
                                            case 38: withBlock.WalkThrough = 1; break; // Turn On Through
                                            case 39: withBlock.WalkThrough = 0; break; // Turn Off Through
                                            case 40: withBlock.Position = 1; sendupdate = true; break; // Turn On Fixed
                                            case 41: withBlock.Position = 0; sendupdate = true; break; // Turn Off Fixed
                                            case 42: withBlock.Position = 2; sendupdate = true; break; //Turn on Below player

                                            case 43: // Change Graphic
                                                {
                                                    withBlock.GraphicType = (byte)nextMove.Data1;
                                                    withBlock.Graphic = nextMove.Data2;
                                                    withBlock.GraphicX = nextMove.Data3;
                                                    withBlock.GraphicX2 = nextMove.Data4;
                                                    withBlock.GraphicY = nextMove.Data5;
                                                    withBlock.GraphicY2 = nextMove.Data6;

                                                    // Adjust direction if it's a character graphic.
                                                    if (withBlock.GraphicType == 1)
                                                    {
                                                        withBlock.Dir = withBlock.GraphicY switch
                                                        {
                                                            0 => (int)DirectionType.Down,
                                                            1 => (int)DirectionType.Left,
                                                            2 => (int)DirectionType.Right,
                                                            3 => (int)DirectionType.Up,
                                                            _ => withBlock.Dir
                                                        };
                                                    }
                                                    sendupdate = true;
                                                    break;
                                                }

                                        }

                                        // Send update if necessary.
                                        if (sendupdate && TempPlayer[i].EventMap.EventPages[EventID].EventID >= 0)
                                        {
                                            using (var buffer = new ByteStream(4))
                                            {
                                                buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
                                                buffer.WriteInt32(TempPlayer[i].EventMap.EventPages[EventID].EventID); // Use map event ID

                                                ref var withBlock1 = ref TempPlayer[i].EventMap.EventPages[EventID];
                                                buffer.WriteString(Map[mapNum].Event[withBlock1.EventID].Name);  //use map event Id
                                                buffer.WriteInt32(withBlock1.Dir);
                                                buffer.WriteByte(withBlock1.GraphicType);
                                                buffer.WriteInt32(withBlock1.Graphic);
                                                buffer.WriteInt32(withBlock1.GraphicX);
                                                buffer.WriteInt32(withBlock1.GraphicX2);
                                                buffer.WriteInt32(withBlock1.GraphicY);
                                                buffer.WriteInt32(withBlock1.GraphicY2);
                                                buffer.WriteInt32(withBlock1.MovementSpeed); // Use consistent naming
                                                buffer.WriteInt32(withBlock1.X);
                                                buffer.WriteInt32(withBlock1.Y);
                                                buffer.WriteByte(withBlock1.Position);
                                                buffer.WriteBoolean(withBlock1.Visible);
                                                buffer.WriteInt32(withBlock1.WalkingAnim);
                                                buffer.WriteInt32(withBlock1.FixedDir);
                                                buffer.WriteInt32(withBlock1.WalkThrough);
                                                buffer.WriteInt32(withBlock1.ShowName);
                                                NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                            }
                                        }
                                    }
                                    doNotProcessMoveRoute = false; // Reset for the next loop iteration.
                                }
                                break;
                            }
                    }

                    // Set next move timer based on MoveFreq.
                    localEvent.MoveTimer = General.GetTimeMs() + localEvent.MoveFreq switch
                    {
                        0 => 4000,
                        1 => 2000,
                        2 => 1000,
                        3 => 500,
                        4 => 250,
                        _ => 1000
                    };
                }
            });
        }


        public static void ProcessEventCommands()
        {
            // Parallel processing for each player.
            Parallel.For(0, NetworkConfig.Socket.HighIndex + 1, i =>
            {
                if (!NetworkConfig.IsPlaying(i) || TempPlayer[i].GettingMap || TempPlayer[i].EventMap.CurrentEvents <= 0) return;

                int mapNum = Player[i].Map; // Cache map number.

                // Iterate through the player's events.
                for (int x = 0; x < TempPlayer[i].EventMap.CurrentEvents; x++)
                {
                    if (TempPlayer[i].EventProcessingCount <= 0) continue;

                    ref var eventPage = ref TempPlayer[i].EventMap.EventPages[x];

                    if (!eventPage.Visible) continue;

                    // Check event and page validity.
                    if (eventPage.EventID >= Map[mapNum].Event.Length || eventPage.PageID >= Map[mapNum].Event[eventPage.EventID].Pages.Length) continue;


                    // Handle parallel process events (Trigger == 2).
                    if (Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].Trigger == 2)
                    {
                        // If not already active, start the event processing.
                        if (TempPlayer[i].EventProcessing[eventPage.EventID].Active == 0) // Use map event ID for indexing.
                        {
                            if (Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].CommandListCount > 0)
                            {
                                ref var eventProcessing = ref TempPlayer[i].EventProcessing[eventPage.EventID]; // And here.
                                eventProcessing.Active = 1;
                                eventProcessing.ActionTimer = General.GetTimeMs();
                                eventProcessing.CurList = 0;
                                eventProcessing.CurSlot = 0;
                                eventProcessing.EventID = eventPage.EventID;
                                eventProcessing.PageID = eventPage.PageID;
                                eventProcessing.WaitingForResponse = 0;

                                // Allocate ListLeftOff array.
                                eventProcessing.ListLeftOff = new int[Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].CommandListCount];

                            }
                        }
                    }

                }
            });

            // Process active event commands for each player.
            Parallel.For(0, NetworkConfig.Socket.HighIndex + 1, i =>
            {
                if (!NetworkConfig.IsPlaying(i) || TempPlayer[i].EventProcessingCount <= 0 || TempPlayer[i].GettingMap) return;

                int mapNum = GetPlayerMap(i); // Cache map number
                bool restartloop;
                do
                {
                    restartloop = false;
                    for (int x = 0; x < TempPlayer[i].EventProcessingCount; x++)
                    {
                        if (TempPlayer[i].EventProcessing[x].Active != 1) continue;


                        ref var withBlock1 = ref TempPlayer[i].EventProcessing[x];

                        // Basic validity checks
                        if (withBlock1.EventID < 0 || withBlock1.EventID >= Map[mapNum].Event.Length) continue;


                        bool removeEventProcess = false;

                        // Handle waiting states (shop, bank, event movement).
                        switch (withBlock1.WaitingForResponse)
                        {
                            case 2: // Waiting for shop to close.
                                if (TempPlayer[i].InShop == 0)
                                {
                                    withBlock1.WaitingForResponse = 0;
                                }
                                break;
                            case 3: // Waiting for bank to close.
                                if (!TempPlayer[i].InBank)
                                {
                                    withBlock1.WaitingForResponse = 0;
                                }
                                break;
                            case 4: // Waiting for event movement to complete.
                                {   //check to make sure event still exists
                                    if (withBlock1.EventMovingId < 0 || withBlock1.EventMovingId >= TempPlayer[i].EventMap.EventPages.Length)
                                        break;

                                    if (withBlock1.EventMovingType == 0) // Local event.
                                    {
                                        if (TempPlayer[i].EventMap.EventPages[withBlock1.EventMovingId].MoveRouteComplete == 1)
                                        {
                                            withBlock1.WaitingForResponse = 0;
                                        }
                                    }
                                    else // Global event.
                                    {
                                        //check that map still exists
                                        if (GetPlayerMap(i) < 0 || GetPlayerMap(i) >= Event.TempEventMap.Length)
                                            break;
                                        //check that event still exists.
                                        if (withBlock1.EventMovingId < 0 || withBlock1.EventMovingId >= Event.TempEventMap[GetPlayerMap(i)].Event.Length)
                                            break;

                                        if (Event.TempEventMap[GetPlayerMap(i)].Event[withBlock1.EventMovingId].MoveRouteComplete == 1)
                                        {
                                            withBlock1.WaitingForResponse = 0;
                                        }
                                    }
                                    break;
                                }
                        }

                        if (withBlock1.WaitingForResponse == 0 && withBlock1.ActionTimer <= General.GetTimeMs())
                        {

                            // Process event commands until a wait, branch, or end condition is encountered.
                            bool restartlist = true;
                            bool endprocess = false;
                            while (restartlist && !endprocess && withBlock1.WaitingForResponse == 0)
                            {
                                restartlist = false;

                                // Check for null or out-of-bounds conditions.
                                if (withBlock1.ListLeftOff == null) continue; // Should not happen, but handle it.

                                var commandList = Map[mapNum].Event[withBlock1.EventID].Pages[withBlock1.PageID].CommandList;

                                // More boundary checks
                                if (withBlock1.CurList >= commandList.Length)
                                {
                                    removeEventProcess = true;
                                    endprocess = true;
                                    continue; // Exit the inner loop.
                                }
                                if (withBlock1.CurSlot >= commandList[withBlock1.CurList].Commands.Length)
                                {
                                    if (withBlock1.CurList == commandList[withBlock1.CurList].ParentList)
                                    {
                                        removeEventProcess = true;
                                        endprocess = true;
                                    }
                                    else
                                    {
                                        withBlock1.CurList = commandList[withBlock1.CurList].ParentList;
                                        withBlock1.CurSlot = 0;
                                        restartlist = true;
                                    }
                                    continue;
                                }

                                // Restore saved position in the command list, if any.
                                if (withBlock1.ListLeftOff[withBlock1.CurList] > 0)
                                {
                                    withBlock1.CurSlot = withBlock1.ListLeftOff[withBlock1.CurList] + 1;
                                    withBlock1.ListLeftOff[withBlock1.CurList] = 0; // Clear the saved position.
                                }

                                // Check again, since curslot and curlist may have changed
                                if (withBlock1.CurList >= commandList.Length)
                                {
                                    removeEventProcess = true;
                                    endprocess = true;
                                    continue; // Exit inner loop.
                                }
                                if (withBlock1.CurSlot >= commandList[withBlock1.CurList].CommandCount)
                                {
                                    if (withBlock1.CurList == commandList[withBlock1.CurList].ParentList) //should be itself
                                    {
                                        removeEventProcess = true; // End of the main list.
                                        endprocess = true;
                                    }
                                    else
                                    {
                                                                                withBlock1.CurList = commandList[withBlock1.CurList].ParentList;
                                        withBlock1.CurSlot = 0;
                                        restartlist = true;
                                    }
                                    continue;
                                }



                                if (!restartlist && !endprocess)
                                {
                                    // Process the current event command.
                                    var command = commandList[withBlock1.CurList].Commands[withBlock1.CurSlot];

                                    switch (command.Index)
                                    {
                                        case (byte)EventType.AddText:
                                            {
                                                switch (command.Data2)
                                                {
                                                    case 0: // Player
                                                        NetworkSend.PlayerMsg(i, command.Text1, (Color)command.Data1);
                                                        break;
                                                    case 1: // Map
                                                        NetworkSend.MapMsg(mapNum, command.Text1, (Color)command.Data1);
                                                        break;
                                                    case 2: // Global
                                                        NetworkSend.GlobalMsg(command.Text1);
                                                        break;
                                                }
                                                break;
                                            }
                                        case (byte)EventType.ShowText:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SEventChat);
                                                    buffer.WriteInt32(withBlock1.EventID);
                                                    buffer.WriteInt32(withBlock1.PageID);
                                                    buffer.WriteInt32(command.Data1); // Face Icon
                                                    buffer.WriteString(ParseEventText(i, command.Text1));

                                                    // Determine if there's a next command to influence display behavior.
                                                    int nextCommandType = 0; // 0: None, 1: ShowText/Choices, 2: Condition
                                                    if (withBlock1.CurSlot + 1 < commandList[withBlock1.CurList].CommandCount)
                                                    {
                                                        byte nextIndex = commandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index;
                                                        if (nextIndex == (byte)EventType.ShowText || nextIndex == (byte)EventType.ShowChoices)
                                                        {
                                                            nextCommandType = 1;
                                                        }
                                                        else if (nextIndex == (byte)EventType.Condition)
                                                        {
                                                            nextCommandType = 2;
                                                        }
                                                    }
                                                    else //end of list
                                                        nextCommandType = 2;

                                                    buffer.WriteInt32(nextCommandType);
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                withBlock1.WaitingForResponse = 0; // No response needed.
                                                break;
                                            }
                                        case (byte)EventType.ShowChoices:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SEventChat);
                                                    buffer.WriteInt32(withBlock1.EventID);
                                                    buffer.WriteInt32(withBlock1.PageID);
                                                    buffer.WriteInt32(command.Data5); // Face Icon
                                                    buffer.WriteString(ParseEventText(i, command.Text1));

                                                    // Determine the number of choices.
                                                    int w = 0;
                                                    if (!string.IsNullOrEmpty(command.Text2))
                                                    {
                                                        w = 1;
                                                        if (!string.IsNullOrEmpty(command.Text3))
                                                        {
                                                            w = 2;
                                                            if (!string.IsNullOrEmpty(command.Text4))
                                                            {
                                                                w = 3;
                                                                if (!string.IsNullOrEmpty(command.Text5))
                                                                {
                                                                    w = 4;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    buffer.WriteInt32(w);

                                                    // Write choice texts.
                                                    for (int v = 1; v <= w; v++)
                                                    {
                                                        switch (v)
                                                        {
                                                            case 1: buffer.WriteString(ParseEventText(i, command.Text2)); break;
                                                            case 2: buffer.WriteString(ParseEventText(i, command.Text3)); break;
                                                            case 3: buffer.WriteString(ParseEventText(i, command.Text4)); break;
                                                            case 4: buffer.WriteString(ParseEventText(i, command.Text5)); break;
                                                        }
                                                    }

                                                    // Next command logic (similar to ShowText).
                                                    int nextCommandType = 0;
                                                    if (withBlock1.CurSlot + 1 < commandList[withBlock1.CurList].CommandCount)
                                                    {
                                                        byte nextIndex = commandList[withBlock1.CurList].Commands[withBlock1.CurSlot + 1].Index;
                                                        if (nextIndex == (byte)EventType.ShowText || nextIndex == (byte)EventType.ShowChoices)
                                                        {
                                                            nextCommandType = 1;
                                                        }
                                                        else if (nextIndex == (byte)EventType.Condition)
                                                        {
                                                            nextCommandType = 2;
                                                        }
                                                    }
                                                    else
                                                        nextCommandType = 2;

                                                    buffer.WriteInt32(nextCommandType);
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                withBlock1.WaitingForResponse = 0; // No response needed (choices handled separately).
                                                break;
                                            }
                                        case (byte)EventType.PlayerVar:
                                            {
                                                switch (command.Data2)
                                                {
                                                    case 0: // Set
                                                        Player[i].Variables[command.Data1] = command.Data3;
                                                        break;
                                                    case 1: // Add
                                                        Player[i].Variables[command.Data1] += command.Data3;
                                                        break;
                                                    case 2: // Subtract
                                                        Player[i].Variables[command.Data1] -= command.Data3;
                                                        break;
                                                    case 3: // Random
                                                        Player[i].Variables[command.Data1] = (int)General.GetRandom.NextDouble(command.Data3, command.Data4);
                                                        break;
                                                }
                                                break;
                                            }
                                        case (byte)EventType.PlayerSwitch:
                                            {
                                                Player[i].Switches[command.Data1] = (byte)(command.Data2 == 0 ? 0 : 1);
                                                break;
                                            }

                                        case (byte)EventType.SelfSwitch:
                                            {
                                                // Determine whether it's a global or local self switch.
                                                if (Map[mapNum].Event[withBlock1.EventID].Globals == 1)
                                                {
                                                    Map[mapNum].Event[withBlock1.EventID].SelfSwitches[command.Data1 + 1] = (byte)(command.Data2 == 0 ? 0 : 1);
                                                }
                                                else
                                                {
                                                    TempPlayer[i].EventMap.EventPages[withBlock1.EventID].SelfSwitches[command.Data1 + 1] = (byte)(command.Data2 == 0 ? 0 : 1);
                                                }
                                                break;
                                            }
                                        case (byte)EventType.Condition:
                                            {
                                                bool conditionMet = false;
                                                var branch = command.ConditionalBranch;

                                                switch (branch.Condition)
                                                {
                                                    case 0: // Variable
                                                        {
                                                            int playerVar = Player[i].Variables[branch.Data1];
                                                            switch (branch.Data2)
                                                            {
                                                                case 0: conditionMet = playerVar == branch.Data3; break;
                                                                case 1: conditionMet = playerVar >= branch.Data3; break;
                                                                case 2: conditionMet = playerVar <= branch.Data3; break;
                                                                case 3: conditionMet = playerVar > branch.Data3; break;
                                                                case 4: conditionMet = playerVar < branch.Data3; break;
                                                                case 5: conditionMet = playerVar != branch.Data3; break;
                                                            }
                                                            break;
                                                        }
                                                    case 1: // Switch
                                                        {
                                                            bool switchState = Player[i].Switches[branch.Data1] == 1;
                                                            conditionMet = (branch.Data2 == 0 && switchState) || (branch.Data2 == 1 && !switchState);
                                                            break;
                                                        }
                                                    case 2: // Item
                                                        conditionMet = Player.HasItem(i, branch.Data1) >= branch.Data2;
                                                        break;
                                                    case 3: // Class
                                                        conditionMet = Player[i].Job == branch.Data1;
                                                        break;
                                                    case 4: // Skill
                                                        conditionMet = HasSkill(i, branch.Data1);
                                                        break;
                                                    case 5: // Level
                                                        {
                                                            int playerLevel = GetPlayerLevel(i);
                                                            switch (branch.Data2)
                                                            {
                                                                case 0: conditionMet = playerLevel == branch.Data1; break;
                                                                case 1: conditionMet = playerLevel >= branch.Data1; break;
                                                                case 2: conditionMet = playerLevel <= branch.Data1; break;
                                                                case 3: conditionMet = playerLevel > branch.Data1; break;
                                                                case 4: conditionMet = playerLevel < branch.Data1; break;
                                                                case 5: conditionMet = playerLevel != branch.Data1; break;
                                                            }
                                                            break;
                                                        }
                                                    case 6: // Self Switch
                                                        {
                                                            bool selfSwitchState;
                                                            if (Map[mapNum].Event[withBlock1.EventID].Globals == 1)
                                                                selfSwitchState = Map[mapNum].Event[withBlock1.EventID].SelfSwitches[branch.Data1 + 1] == 1;
                                                            else
                                                                selfSwitchState = TempPlayer[i].EventMap.EventPages[withBlock1.EventID].SelfSwitches[branch.Data1 + 1] == 1;

                                                            conditionMet = (branch.Data2 == 0 && selfSwitchState) || (branch.Data2 == 1 && !selfSwitchState);
                                                            break;
                                                        }

                                                    case 7://Timer - Not currently implemented
                                                        break;
                                                    case 8: // Gender
                                                        conditionMet = Player[i].Sex == branch.Data1;
                                                        break;
                                                    case 9: // Time of Day
                                                        conditionMet = Clock.Instance.TimeOfDay == (TimeOfDay)branch.Data1;
                                                        break;
                                                }

                                                // Set the next command list and slot based on the condition.
                                                withBlock1.ListLeftOff[withBlock1.CurList] = withBlock1.CurSlot;
                                                withBlock1.CurList = conditionMet ? branch.CommandList : branch.ElseCommandList;
                                                withBlock1.CurSlot = 0;
                                                endprocess = true; //end process so we dont increment curslot, but instead start at the top of the conditional list.

                                                break;
                                            }

                                        case (byte)EventType.ExitProcess:
                                            removeEventProcess = true;
                                            endprocess = true;
                                            break;

                                        case (byte)EventType.ChangeItems:
                                            {
                                                switch (command.Data2)
                                                {
                                                    case 0: // Set
                                                        if (Player.HasItem(i, command.Data1) > 0)
                                                        {
                                                            SetPlayerInvValue(i, Player.FindItemSlot(i, command.Data1), command.Data3);
                                                        }
                                                        break;
                                                    case 1: // Give
                                                        Player.GiveInv(i, command.Data1, command.Data3, true);
                                                        break;
                                                    case 2: // Take
                                                        {
                                                            int itemAmount = Player.HasItem(i, command.Data1);
                                                            if (itemAmount >= command.Data3)
                                                            {
                                                                Player.TakeInv(i, command.Data1, command.Data3);
                                                            }
                                                            break;
                                                        }
                                                }
                                                NetworkSend.SendInventory(i);
                                                break;
                                            }

                                        case (byte)EventType.RestoreHP:
                                            SetPlayerVital(i, VitalType.HP, GetPlayerMaxVital(i, VitalType.HP));
                                            NetworkSend.SendVital(i, VitalType.HP);
                                            break;

                                        case (byte)EventType.RestoreSP:
                                            SetPlayerVital(i, VitalType.SP, GetPlayerMaxVital(i, VitalType.SP));
                                            NetworkSend.SendVital(i, VitalType.SP);
                                            break;

                                        case (byte)EventType.LevelUp:
                                            SetPlayerExp(i, GetPlayerNextLevel(i));
                                            Player.CheckPlayerLevelUp(i);
                                            NetworkSend.SendExp(i);
                                            NetworkSend.SendPlayerData(i);
                                            break;

                                        case (byte)EventType.ChangeLevel:
                                            SetPlayerLevel(i, command.Data1);
                                            SetPlayerExp(i, 0);
                                            NetworkSend.SendExp(i);
                                            NetworkSend.SendPlayerData(i);
                                            break;

                                        case (byte)EventType.ChangeSkills:
                                            {
                                                if (command.Data2 == 0) // Learn
                                                {
                                                    if (FindOpenSkill(i) >= 0 && !HasSkill(i, command.Data1))
                                                    {
                                                        SetPlayerSkill(i, FindOpenSkill(i), command.Data1);
                                                    }
                                                }
                                                else if (command.Data2 == 1) // Forget
                                                {
                                                    for (int p = 0; p < Core.Constant.MAX_PLAYER_SKILLS; p++)
                                                    {
                                                        if (Player[i].Skill[p].Num == command.Data1)
                                                        {
                                                            SetPlayerSkill(i, p, 0);
                                                        }
                                                    }
                                                }
                                                NetworkSend.SendPlayerSkills(i);
                                                break;
                                            }

                                        case (byte)EventType.ChangeJob:
                                            Player[i].Job = (byte)command.Data1;
                                            NetworkSend.SendPlayerData(i);
                                            break;

                                        case (byte)EventType.ChangeSprite:
                                            SetPlayerSprite(i, command.Data1);
                                            NetworkSend.SendPlayerData(i);
                                            break;

                                        case (byte)EventType.ChangeSex:
                                            Player[i].Sex = (byte)(command.Data1 == 0 ? SexType.Male : SexType.Female);
                                            NetworkSend.SendPlayerData(i);
                                            break;

                                        case (byte)EventType.ChangePk:
                                            Player[i].Pk = (byte)(command.Data1 == 0 ? 0 : 1);
                                            NetworkSend.SendPlayerData(i);
                                            break;

                                        case (byte)EventType.WarpPlayer:
                                            {
                                                int dir = command.Data4 == 0 ? Player[i].Dir : (byte)(command.Data4 - 1);
                                                Player.PlayerWarp(i, command.Data1, command.Data2, command.Data3, dir);
                                                break;
                                            }

                                        case (byte)EventType.SetMoveRoute:
                                            {
                                                // Check if the event exists.
                                                if (command.Data1 < Map[mapNum].Event.Length)
                                                {
                                                    if (Map[mapNum].Event[command.Data1].Globals == 1) // Global event
                                                    {
                                                        // Directly modify the global event.
                                                        ref var globalEvent = ref Event.TempEventMap[mapNum].Event[command.Data1];
                                                        globalEvent.MoveType = 2; // Custom route
                                                        globalEvent.IgnoreIfCannotMove = command.Data2;
                                                        globalEvent.RepeatMoveRoute = command.Data3;
                                                        globalEvent.MoveRouteCount = command.MoveRouteCount;
                                                        if (command.MoveRouteCount > 0)
                                                        {
                                                            globalEvent.MoveRoute = new  EventMoveRoute[command.MoveRouteCount];
                                                            Array.Copy(command.MoveRoute, globalEvent.MoveRoute, command.MoveRouteCount);
                                                        }
                                                        globalEvent.MoveRouteStep = 0;
                                                        globalEvent.MoveRouteComplete = (command.MoveRouteCount == 0) ? (byte)1 : (byte)0; //if routecount is 0, complete = true

                                                    }
                                                    else // Local event
                                                    {
                                                        // Modify the local event copy for this player.
                                                        ref var localEvent = ref TempPlayer[i].EventMap.EventPages[command.Data1]; // Assuming Data1 is the event index
                                                        localEvent.MoveType = 2;
                                                        localEvent.IgnoreIfCannotMove = command.Data2;
                                                        localEvent.RepeatMoveRoute = command.Data3;
                                                        localEvent.MoveRouteCount = command.MoveRouteCount;
                                                        if (command.MoveRouteCount > 0)
                                                        {
                                                            localEvent.MoveRoute = new EventMoveRoute[command.MoveRouteCount];
                                                            Array.Copy(command.MoveRoute, localEvent.MoveRoute, command.MoveRouteCount);
                                                        }
                                                        localEvent.MoveRouteStep = 0;
                                                        localEvent.MoveRouteComplete = (command.MoveRouteCount == 0) ? (byte)1 : (byte)0; // If no route, it's complete.
                                                    }
                                                }
                                                break;
                                            }

                                        case (byte)EventType.PlayAnimation:
                                            {
                                                switch (command.Data2)
                                                {
                                                    case 0: // On Player
                                                        Animation.SendAnimation(mapNum, command.Data1, GetPlayerX(i), GetPlayerY(i), TargetType.Player, i);
                                                        break;
                                                    case 1: // On Event
                                                        {
                                                            //check for valid event
                                                            if (command.Data3 < 0 || command.Data3 >= Map[mapNum].Event.Length)
                                                                break;

                                                            if (Map[mapNum].Event[command.Data3].Globals == 1)
                                                            {
                                                                // Play on global event.
                                                                Animation.SendAnimation(mapNum, command.Data1,
                                                                    Map[mapNum].Event[command.Data3].X,
                                                                    Map[mapNum].Event[command.Data3].Y);
                                                            }
                                                            else
                                                            {
                                                                //check that local event exists for this player.
                                                                if (command.Data3 < 0 || command.Data3 >= TempPlayer[i].EventMap.EventPages.Length)
                                                                    break;

                                                                // Play on local event.
                                                                Animation.SendAnimation(mapNum, command.Data1,
                                                                    TempPlayer[i].EventMap.EventPages[command.Data3].X,
                                                                    TempPlayer[i].EventMap.EventPages[command.Data3].Y,
                                                                    TargetType.Event, command.Data3);
                                                            }
                                                            break;
                                                        }
                                                    case 2: // On Coordinates
                                                        Animation.SendAnimation(mapNum, command.Data1, command.Data3, command.Data4, 0, 0);
                                                        break;
                                                }
                                                break;
                                            }

                                        case (byte)EventType.CustomScript:
                                            Event.CustomScript(i, command.Data1, mapNum, withBlock1.EventID);
                                            break;

                                        case (byte)EventType.PlayBgm:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SPlayBGM);
                                                    buffer.WriteString(command.Text1);
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }
                                        case (byte)EventType.FadeoutBgm:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SFadeoutBGM);
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }

                                        case (byte)EventType.PlaySound:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SPlaySound);
                                                    buffer.WriteString(command.Text1);
                                                    buffer.WriteInt32(Map[mapNum].Event[withBlock1.EventID].X);
                                                    buffer.WriteInt32(Map[mapNum].Event[withBlock1.EventID].Y);
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }

                                        case (byte)EventType.StopSound:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SStopSound);
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }
                                        case (byte)EventType.SetAccess:
                                            SetPlayerAccess(i, command.Data1);
                                            NetworkSend.SendPlayerData(i);
                                            break;

                                        case (byte)EventType.OpenShop:
                                            {
                                                // Check if the shop exists and has a valid name.
                                                if (command.Data1 > 0 && command.Data1 < Shop.Length && !string.IsNullOrEmpty(Shop[command.Data1]?.Name))
                                                {
                                                    NetworkSend.SendOpenShop(i, command.Data1);
                                                    TempPlayer[i].InShop = command.Data1;
                                                    withBlock1.WaitingForResponse = 2; // Wait for shop to close.
                                                }
                                                break;
                                            }
                                        case (byte)EventType.OpenBank:
                                            NetworkSend.SendBank(i);
                                            TempPlayer[i].InBank = true;
                                            withBlock1.WaitingForResponse = 3; // Wait for bank to close.
                                            break;

                                        case (byte)EventType.GiveExp:
                                            Event.GivePlayerExp(i, command.Data1);
                                            break;

                                        case (byte)EventType.ShowChatBubble:
                                            {
                                                ColorType color = ColorType.Blue; // Or any default color you prefer
                                                switch (command.Data1)
                                                {
                                                    case (byte)TargetType.Player:
                                                        NetworkSend.SendChatBubble(mapNum, i, command.Data1, command.Text1, (int)color);
                                                        break;
                                                    case (byte)TargetType.NPC:
                                                        NetworkSend.SendChatBubble(mapNum, command.Data2, command.Data1, command.Text1, (int)color);
                                                        break;
                                                    case (byte)TargetType.Event:
                                                        NetworkSend.SendChatBubble(mapNum, command.Data2, command.Data1, command.Text1, (int)color);
                                                        break;
                                                }
                                                break;
                                            }

                                        case (byte)EventType.Label:
                                            // No action needed, just a label for GoToLabel.
                                            break;

                                        case (byte)EventType.GoToLabel:
                                            // Find the label and update the command list position.
                                            FindEventLabel(command.Text1, mapNum, withBlock1.EventID, withBlock1.PageID, ref withBlock1.CurSlot, ref withBlock1.CurList, ref withBlock1.ListLeftOff);
                                            break;

                                        case (byte)EventType.SpawnNPC:
                                            if (command.Data1 > 0 && command.Data1 < Map[mapNum].NPC.Length) // Check if NPC exists
                                            {
                                                NPC.SpawnNPC(command.Data1, mapNum);
                                            }
                                            break;

                                        case (byte)EventType.FadeIn:
                                            Event.SendSpecialEffect(i, Event.EffectTypeFadein);
                                            break;

                                        case (byte)EventType.FadeOut:
                                            Event.SendSpecialEffect(i, Event.EffectTypeFadeout);
                                            break;

                                        case (byte)EventType.FlashWhite:
                                            Event.SendSpecialEffect(i, Event.EffectTypeFlash);
                                            break;

                                        case (byte)EventType.SetFog:
                                            Event.SendSpecialEffect(i, Event.EffectTypeFog, command.Data1, command.Data2, command.Data3);
                                            break;

                                        case (byte)EventType.SetWeather:
                                            Event.SendSpecialEffect(i, Event.EffectTypeWeather, command.Data1, command.Data2);
                                            break;

                                        case (byte)EventType.SetTint:
                                            Event.SendSpecialEffect(i, Event.EffectTypeTint, command.Data1, command.Data2, command.Data3, command.Data4);
                                            break;

                                        case (byte)EventType.Wait:
                                            withBlock1.ActionTimer = General.GetTimeMs() + command.Data1;
                                            break;

                                        case (byte)EventType.ShowPicture:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SPic);
                                                    buffer.WriteInt32(withBlock1.EventID); // Event ID.
                                                    buffer.WriteByte((byte)command.Data1); // Picture ID.
                                                    buffer.WriteByte((byte)command.Data2); // X
                                                    buffer.WriteByte((byte)command.Data3); // Y
                                                    buffer.WriteByte((byte)command.Data4); // Transparency
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }
                                        case (byte)EventType.HidePicture:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SPic);
                                                    buffer.WriteByte(0); // Hide picture.
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }
                                        case (byte)EventType.WaitMovement:
                                            {
                                                // Ensure the event exists.
                                                if (command.Data1 < Map[mapNum].Event.Length)
                                                {
                                                    if (Map[mapNum].Event[command.Data1].Globals == 1)
                                                    {
                                                        withBlock1.WaitingForResponse = 4;
                                                        withBlock1.EventMovingId = command.Data1; // Global event ID.
                                                        withBlock1.EventMovingType = 1; // Global.
                                                    }
                                                    else
                                                    {
                                                        //check that local event exists on player
                                                        if (command.Data1 < 0 || command.Data1 >= TempPlayer[i].EventMap.EventPages.Length)
                                                            break;

                                                        withBlock1.WaitingForResponse = 4;
                                                        withBlock1.EventMovingId = command.Data1; // Local event ID.
                                                        withBlock1.EventMovingType = 0; // Local.
                                                    }
                                                }
                                                break;
                                            }
                                        case (byte)EventType.HoldPlayer:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SHoldPlayer);
                                                    buffer.WriteInt32(0); // Hold
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }
                                        case (byte)EventType.ReleasePlayer:
                                            {
                                                using (var buffer = new ByteStream(4))
                                                {
                                                    buffer.WriteInt32((int)ServerPackets.SHoldPlayer);
                                                    buffer.WriteInt32(1); // Release
                                                    NetworkConfig.Socket.SendDataTo(i, buffer.Data, buffer.Head);
                                                }
                                                break;
                                            }

                                    }
                                }
                                // Increment to the next command, unless we've branched or ended.
                                if (!endprocess)
                                    withBlock1.CurSlot++;

                            }
                        }


                        // Clean up finished event processes.
                        if (removeEventProcess)
                        {
                            withBlock1.Active = 0;
                            restartloop = true;
                        }
                    }
                } while (restartloop);

            });

        }



        public static void UpdateEventLogic()
        {
            // These functions have been optimized to reduce redundant calls and improve clarity.
            RemoveDeadEvents();
            SpawnNewEvents();
            ProcessEventMovement();
            ProcessLocalEventMovement();
            ProcessEventCommands();

            // Consider adding a priority system here to determine which events should be processed first.
            // This is just a conceptual example.  A real priority system would need more work.
            // ProcessHighPriorityEvents();
            // ProcessNormalEvents();
            // ProcessLowPriorityEvents();
        }


        public static string ParseEventText(int index, string txt)
        {
            // Use StringBuilder for efficient string manipulation.
            var sb = new System.Text.StringBuilder(txt);

            sb.Replace("/name", Player[index].Name);
            sb.Replace("/p", Player[index].Name);
            sb.Replace("$playername$", Player[index].Name);
            sb.Replace("$playerclass$", Job[Player[index].Job].Name);

            // Process variables (/v[variableIndex]).
            int start = sb.ToString().IndexOf("/v"); // Find the first occurrence.
            while (start >= 0)
            {
                int end = start + 2;
                // Find the end of the number.
                while (end < sb.Length && char.IsDigit(sb[end]))
                {
                    end++;
                }

                if (end > start + 2) // Ensure we found a number.
                {
                    string varIndexStr = sb.ToString(start + 2, end - (start + 2));
                    if (int.TryParse(varIndexStr, out int varIndex))
                    {
                        // Make sure the variable index is within bounds
                        if (varIndex >= 0 && varIndex < Player[index].Variables.Length)
                        {
                            sb.Remove(start, end - start);
                            sb.Insert(start, Player[index].Variables[varIndex].ToString());
                        }
                        else
                        {   //invalid variable, remove it from the output.
                            sb.Remove(start, end - start);
                        }

                    }
                    else //should never occur, but just in case.
                        sb.Remove(start, end - start); //if it wasn't a valid number, remove it.
                }
                else // If no number, remove /v
                {
                    sb.Remove(start, 2);
                }
                start = sb.ToString().IndexOf("/v"); //check for any others
            }

            return sb.ToString();
        }



        public static void FindEventLabel(string Label, int mapNum, int EventID, int PageID, ref int CurSlot, ref int CurList, ref int[] ListLeftOff)
        {

            // Check for valid map, event, and page.
            if (mapNum < 0 || mapNum >= Map.Length || EventID < 0 || EventID >= Map[mapNum].Event.Length ||
                PageID < 0 || PageID >= Map[mapNum].Event[EventID].Pages.Length)
            {
                //invalid event, don't do anything.
                return;
            }


            int tmpCurSlot = CurSlot;
            int tmpCurList = CurList;
            int[] tmpListLeftOff = ListLeftOff;

            // Initialize data structures.
            var commandList = Map[mapNum].Event[EventID].Pages[PageID].CommandList;

            // Check if commandList is null
            if (commandList == null)
                return;

            ListLeftOff = new int[commandList.Length];
            int[] CurrentListOption = new int[commandList.Length];

            CurList = 0;
            CurSlot = 0;

            bool removeEventProcess = false;
            bool restartlist;

            while (!removeEventProcess)
            {
                restartlist = false;

                // Restore position if returning from a nested list.
                if (ListLeftOff[CurList] > 0)
                {
                    CurSlot = ListLeftOff[CurList];
                    ListLeftOff[CurList] = 0;
                }

                // Check for out-of-bounds conditions.
                if (CurList >= commandList.Length)
                {
                    removeEventProcess = true; // Invalid list index.
                    continue;
                }

                if (CurSlot >= commandList[CurList].CommandCount)
                {
                    if (CurList == commandList[CurList].ParentList) //should be itself
                    {
                        removeEventProcess = true; // Reached the end of a top-level list.
                    }
                    else
                    {
                        CurList = commandList[CurList].ParentList;
                        CurSlot = 0;
                        restartlist = true;
                    }
                    continue;
                }

                if (!restartlist && !removeEventProcess)
                {
                    // Get the current command.
                    var command = commandList[CurList].Commands[CurSlot];

                    switch (command.Index)
                    {
                        case (byte)EventType.ShowChoices:
                            {
                                int w = 0;
                                if (!string.IsNullOrEmpty(command.Text2))
                                {
                                    w = 1;
                                    if (!string.IsNullOrEmpty(command.Text3))
                                    {
                                        w = 2;
                                        if (!string.IsNullOrEmpty(command.Text4))
                                        {
                                            w = 3;
                                            if (!string.IsNullOrEmpty(command.Text5))
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
                                        CurrentListOption[CurList]++;
                                        ListLeftOff[CurList] = CurSlot; // Save current position.

                                        // Jump to the appropriate choice's command list.
                                        switch (CurrentListOption[CurList])
                                        {
                                            case 1: CurList = command.Data1; break;
                                            case 2: CurList = command.Data2; break;
                                            case 3: CurList = command.Data3; break;
                                            case 4: CurList = command.Data4; break;
                                        }
                                        CurSlot = 0; // Start at the beginning of the new list.
                                    }
                                    else
                                    {
                                        CurrentListOption[CurList] = 0; // Reset for next time.
                                    }
                                }
                                break;
                            }
                        case (byte)EventType.Condition:
                            {
                                // Handle conditional branches (simplified logic).
                                if (CurrentListOption[CurList] == 0)
                                {
                                    // First visit: Execute the "if" branch.
                                    ListLeftOff[CurList] = CurSlot;
                                    CurList = command.ConditionalBranch.CommandList;
                                    CurSlot = 0;
                                }
                                else if (CurrentListOption[CurList] == 1)
                                {
                                    // Second visit: Execute the "else" branch (if it exists).
                                    ListLeftOff[CurList] = CurSlot;
                                    CurList = command.ConditionalBranch.ElseCommandList;
                                    CurSlot = 0;
                                }
                                //else currentlistoption = 2, so continue on.
                                CurrentListOption[CurList] = (CurrentListOption[CurList] + 1) % 3; //prepare for next visit

                                break;
                            }
                        case (byte)EventType.Label:
                            {
                                // Check if this is the target label.
                                if (command.Text1 == Label)
                                {
                                    return; // Found the label, return to the caller.
                                }
                                break;
                            }
                    }
                    CurSlot++; // Move to the next command.
                }
            }

            // Label not found, restore original values.
            CurList = tmpCurList;
            CurSlot = tmpCurSlot;
            ListLeftOff = tmpListLeftOff;
        }

        public static int FindNPCPath(int mapNum, double MapNPCNum, int targetx, int targety)
        {

            // Check for valid map and NPC.
            if (mapNum < 0 || mapNum >= Map.Length || MapNPCNum < 0 || MapNPCNum >= MapNPC[mapNum].NPC.Length)
            {
                return 4; // Return a default value indicating failure.
            }

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

            sX = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].X;
            sY = Core.Type.MapNPC[mapNum].NPC[(int)MapNPCNum].Y;

            FX = targetx;
            FY = targety;

            if (FX == -1)
                FX = 0;
            if (FY == -1)
                FY = 0;

            pos = new int[(Map[mapNum].MaxX + 1), (Map[mapNum].MaxY + 1)]; //+1 to prevent errors
            // pos = MapBlocks(mapNum).Blocks

            pos[sX, sY] = 100 + tim;
            pos[FX, FY] = 2;

            // reset reachable
            reachable = false;

            // Do while reachable is false... if its set true in progress, we jump out
            // If the path is decided unreachable in process, we will use exit sub. Not proper,
            // but faster ;-)
            while (!reachable)
            {
                // we loop through all squares
                for (j = 0; j <= Map[mapNum].MaxY; j++) //changed to <=
                {
                    for (i = 0; i <= Map[mapNum].MaxX; i++)//changed to <=
                    {
                        // If j = 10 And i = 0 Then MsgBox "hi!"
                        // If they are to be extended, the pointer TIM is on them
                        if (pos[i, j] == 100 + tim)
                        {
                            // The part is to be extended, so do it
                            // We have to make sure that there is a pos(i+1,j) BEFORE we actually use it,
                            // because then we get error... If the square is on side, we dont test for this one!
                            if (i < Map[mapNum].MaxX) //changed to <
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
                                    reachable = true;
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
                                    reachable = true;
                                }
                            }

                            if (j < Map[mapNum].MaxY)  //changed to <
                            {
                                if (pos[i, j + 1] == 0)
                                {
                                    pos[i, j + 1] = 100 + tim + 1;
                                }
                                else if (pos[i, j + 1] == 2)
                                {
                                    reachable = true;
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
                                    reachable = true;
                                }
                            }
                        }
                    }
                }

                // If the reachable is STILL false, then
                if (!reachable)
                {
                    // reset sum
                    Sum = 0;
                    for (j = 0; j <= Map[mapNum].MaxY; j++) //changed to <=
                    {
                        for (i = 0; i <= Map[mapNum].MaxX; i++) //changed to <=
                            // we add up ALL the squares
                            Sum = Sum + pos[i, j];
                    }

                    // Now if the sum is euqal to the last sum, its not reachable, if it isnt, then we store
                    // sum to lastsum
                    if (Sum == LastSum)
                    {
                        return 4; // Indicate no path found.
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
                did = false;

                // If we arent on edge
                if (LastX < Map[mapNum].MaxX) //changed to <
                {
                    // check the square on the right of the solution. Is it a tim-1 one? or just a blank one
                    if (pos[LastX + 1, LastY] == 100 + tim)
                    {
                        // if it, then make it yellow, and change did to true
                        LastX = LastX + 1;
                        did = true;
                    }
                }

                // This will then only work if the previous part didnt execute, and did is still false. THen
                // we want to check another square, the on left. Is it a tim-1 one ?
                if (!did)
                {
                    if (LastX > 0)
                    {
                        if (pos[LastX - 1, LastY] == 100 + tim)
                        {
                            LastX = LastX - 1;
                            did = true;
                        }
                    }
                }

                // We check the one below it
                if (!did)
                {
                    if (LastY < Map[mapNum].MaxY) //changed to <
                    {
                        if (pos[LastX, LastY + 1] == 100 + tim)
                        {
                            LastY = LastY + 1;
                            did = true;
                        }
                    }
                }

                // And above it. One of these have to be it, since we have found the solution, we know that already
                // there is a way back.
                if (!did)
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
            if (path[1].X > sX) // Changed LastX to sX, which is startX
            {
                return (byte)DirectionType.Right;
            }
            else if (path[1].Y > sY) // Changed LastY to sY
            {
                return (byte)DirectionType.Down;
            }
            else if (path[1].Y < sY) // Changed LastY to sY
            {
                return (byte)DirectionType.Up;
            }
            else if (path[1].X < sX) // Changed LastX to sX
            {
                return (byte)DirectionType.Left;
            }

            return 4; //should never hit here, but just incase.
        }


        public static async Task SpawnAllMapGlobalEvents()
        {
            // Use Task.Run to avoid blocking the main thread.
            await Task.Run(() =>
            {
                for (int i = 0; i < Core.Constant.MAX_MAPS; i++)
                {
                    SpawnGlobalEvents(i).ConfigureAwait(false);
                }
            });
        }

        public static async Task SpawnGlobalEvents(int mapNum)
        {
            await Task.Run(() =>
            {
                // Check if the map exists and has events.
                if (mapNum < 0 || mapNum >= Map.Length || Map[mapNum].EventCount <= 0)
                {
                    return;
                }

                // Initialize the temporary event map.
                Event.TempEventMap[mapNum].EventCount = 0;
                Array.Resize(ref Event.TempEventMap[mapNum].Event, 1); // Start with size 1, resize as needed.

                for (int i = 0; i < Map[mapNum].EventCount; i++)
                {
                    // Check for valid global events.
                    if (Map[mapNum].Event[i].PageCount > 0 && Map[mapNum].Event[i].Globals == 1)
                    {
                        // Add a new event to the temporary map.
                        Event.TempEventMap[mapNum].EventCount++;
                        Array.Resize(ref Event.TempEventMap[mapNum].Event, Event.TempEventMap[mapNum].EventCount + 1); // +1 for easier indexing
                        ref var tempEvent = ref Event.TempEventMap[mapNum].Event[Event.TempEventMap[mapNum].EventCount];

                        // Set initial event properties.
                        tempEvent.X = Map[mapNum].Event[i].X;
                        tempEvent.Y = Map[mapNum].Event[i].Y;
                        tempEvent.Dir = Map[mapNum].Event[i].Pages[0].GraphicType == 1 ? (Map[mapNum].Event[i].Pages[0].GraphicY % 4) switch
                        {
                            0 => (int)DirectionType.Down,
                            1 => (int)DirectionType.Left,
                            2 => (int)DirectionType.Right,
                            _ => (int)DirectionType.Up //3
                        } : (int)DirectionType.Down;
                        tempEvent.Active = 0;
                        tempEvent.MoveType = Map[mapNum].Event[i].Pages[0].MoveType;


                        if (tempEvent.MoveType == 2) // Custom Move Route
                        {
                            int moveRouteCount = Map[mapNum].Event[i].Pages[0].MoveRouteCount;
                            tempEvent.MoveRouteCount = moveRouteCount;

                            if (moveRouteCount > 0)
                            {
                                // Copy the move route.
                                tempEvent.MoveRoute = new EventMoveRoute[moveRouteCount];
                                Array.Copy(Map[mapNum].Event[i].Pages[0].MoveRoute, tempEvent.MoveRoute, moveRouteCount);
                                tempEvent.MoveRouteComplete = 0; // Reset completion status.
                            }
                            else
                            {
                                tempEvent.MoveRouteComplete = 1;
                            }
                        }
                        else
                        {
                            tempEvent.MoveRouteComplete = 1; // Not a move route, so considered complete.
                        }


                        tempEvent.RepeatMoveRoute = Map[mapNum].Event[i].Pages[0].RepeatMoveRoute;
                        tempEvent.IgnoreIfCannotMove = Map[mapNum].Event[i].Pages[0].IgnoreMoveRoute;
                        tempEvent.MoveFreq = Map[mapNum].Event[i].Pages[0].MoveFreq;
                        tempEvent.MoveSpeed = Map[mapNum].Event[i].Pages[0].MoveSpeed;
                        tempEvent.WalkThrough = Map[mapNum].Event[i].Pages[0].WalkThrough;
                        tempEvent.FixedDir = Map[mapNum].Event[i].Pages[0].DirFix;
                        tempEvent.WalkingAnim = Map[mapNum].Event[i].Pages[0].WalkAnim;
                        tempEvent.ShowName = Map[mapNum].Event[i].Pages[0].ShowName;

                    }
                }
            });
        }


        public static void SpawnMapEventsFor(int index, int mapNum)
        {
            // Check for valid map.
            if (mapNum < 0 || mapNum >= Map.Length)
            {
                return;
            }
            // Reset player's event data.
            Core.Type.TempPlayer[index].EventMap.CurrentEvents = 0;
            Array.Resize(ref Core.Type.TempPlayer[index].EventMap.EventPages, 1);

            // Initialize event processing array.
            if (Map[mapNum].EventCount > 0)
            {
                Array.Resize(ref Core.Type.TempPlayer[index].EventProcessing, Map[mapNum].EventCount + 1); //+1 for easier indexing
                Core.Type.TempPlayer[index].EventProcessingCount = Map[mapNum].EventCount;
            }
            else
            {
                Array.Resize(ref Core.Type.TempPlayer[index].EventProcessing, 1); //+1 for easier indexing
                Core.Type.TempPlayer[index].EventProcessingCount = 0;
            }

            if (Map[mapNum].EventCount <= 0) return;


            // Iterate through map events.
            for (int i = 0; i < Map[mapNum].EventCount; i++)
            {
                int p = -1;

                // Check if event and its pages exist
                if (Map[mapNum].Event[i].Pages == null) continue;
                if (Map[mapNum].Event[i].PageCount <= 0) continue;

                // Find the highest-priority page that meets conditions.
                for (int z = 0; z < Map[mapNum].Event[i].PageCount; z++)
                {
                    bool spawncurrentevent = true;
                    ref var page = ref Map[mapNum].Event[i].Pages[z]; // Use ref for direct modification.

                    // Check conditions (Variable, Switch, Item, Self Switch).
                    if (page.ChkVariable == 1)
                    {
                        int playerVar = Core.Type.Player[index].Variables[page.VariableIndex];
                        switch (page.VariableCompare)
                        {
                            case 0: spawncurrentevent = playerVar != page.VariableCondition; break;
                            case 1: spawncurrentevent = playerVar < page.VariableCondition; break;
                            case 2: spawncurrentevent = playerVar > page.VariableCondition; break;
                            case 3: spawncurrentevent = playerVar <= page.VariableCondition; break;
                            case 4: spawncurrentevent = playerVar >= page.VariableCondition; break;
                            case 5: spawncurrentevent = playerVar == page.VariableCondition; break;
                        }
                    }

                    if (page.ChkSwitch == 1)
                    {
                        // Using XOR for switch check, handles both expecting true and false efficiently
                        if (!((page.SwitchCompare == 1) ^ (Core.Type.Player[index].Switches[page.SwitchIndex] == 0))) //we want true
                            spawncurrentevent = false;
                    }

                    if (page.ChkHasItem == 1 && Core.Type.Player.HasItem(index, page.HasItemIndex) == 0)
                    {
                        spawncurrentevent = false;
                    }

                    if (page.ChkSelfSwitch == 1)
                    {
                        int compare = page.SelfSwitchCompare; // 0 or 1, no need to check both values explicitly.
                        bool selfSwitchState;

                        if (Map[mapNum].Event[i].Globals == 1)
                            selfSwitchState = Map[mapNum].Event[i].SelfSwitches[page.SelfSwitchIndex] == compare;
                        else
                            selfSwitchState = false; // Local self switches are not checked when spawning.

                        if (!selfSwitchState)
                            spawncurrentevent = false;
                    }


                    if (spawncurrentevent)
                    {
                        p = z; // Store the valid page index.
                    }
                }


                // Spawn the event if a valid page was found.
                if (p >= 0)
                {
                    int z = p;

                    Core.Type.TempPlayer[index].EventMap.CurrentEvents++;
                    Array.Resize(ref Core.Type.TempPlayer[index].EventMap.EventPages, Core.Type.TempPlayer[index].EventMap.CurrentEvents + 1); //+1 for easier indexing
                    ref var withBlock1 = ref Core.Type.TempPlayer[index].EventMap.EventPages[Core.Type.TempPlayer[index].EventMap.CurrentEvents];

                    MapEventPage eventPage = Map[mapNum].Event[i].Pages[z];

                    // Set up the event page data.
                    withBlock1.Dir = eventPage.GraphicType == 1 ? (eventPage.GraphicY % 4) switch
                    {
                        0 => (int)DirectionType.Down,
                        1 => (int)DirectionType.Left,
                        2 => (int)DirectionType.Right,
                        _ => (int)DirectionType.Up
                    } : 0;

                    withBlock1.Graphic = eventPage.Graphic;
                    withBlock1.GraphicType = eventPage.GraphicType;
                    withBlock1.GraphicX = eventPage.GraphicX;
                    withBlock1.GraphicY = eventPage.GraphicY;
                    withBlock1.GraphicX2 = eventPage.GraphicX2;
                    withBlock1.GraphicY2 = eventPage.GraphicY2;
                    withBlock1.MovementSpeed = eventPage.MoveSpeed switch
                    {
                        0 => 2,
                        1 => 3,
                        2 => 4,
                        3 => 6,
                        4 => 12,
                        5 => 24,
                        _ => DefaultMovementSpeed
                    };

                    if (Map[mapNum].Event[i].Globals == 1)
                    {
                        // Use global event's position and direction.
                        withBlock1.X = Event.TempEventMap[mapNum].Event[i].X;
                        withBlock1.Y = Event.TempEventMap[mapNum].Event[i].Y;
                        withBlock1.Dir = Event.TempEventMap[mapNum].Event[i].Dir;
                        withBlock1.MoveRouteStep = Event.TempEventMap[mapNum].Event[i].MoveRouteStep;
                    }
                    else
                    {
                        // Use the event's initial position.
                        withBlock1.X = Map[mapNum].Event[i].X;
                        withBlock1.Y = Map[mapNum].Event[i].Y;
                        withBlock1.MoveRouteStep = 0;
                    }

                    withBlock1.Position = eventPage.Position;
                    withBlock1.EventID = i; // Map event ID.
                    withBlock1.PageID = z;
                    withBlock1.Visible = true; // Always visible when initially spawned.
                    withBlock1.MoveType = eventPage.MoveType;

                    if (withBlock1.MoveType == 2) // Custom move route
                    {
                        withBlock1.MoveRouteCount = eventPage.MoveRouteCount;

                        if (eventPage.MoveRouteCount > 0)
                        {
                            withBlock1.MoveRoute = new EventMoveRoute[eventPage.MoveRouteCount];
                            Array.Copy(eventPage.MoveRoute, withBlock1.MoveRoute, eventPage.MoveRouteCount);
                            withBlock1.MoveRouteComplete = 0; // Reset completion status
                        }
                        else
                            withBlock1.MoveRouteComplete = 1;
                    }
                    else
                    {
                        withBlock1.MoveRouteComplete = 1;
                    }

                    withBlock1.RepeatMoveRoute = eventPage.RepeatMoveRoute;
                    withBlock1.IgnoreIfCannotMove = eventPage.IgnoreMoveRoute;
                    withBlock1.MoveFreq = eventPage.MoveFreq;
                    withBlock1.MoveSpeed = eventPage.MoveSpeed; // Handled above.
                    withBlock1.WalkingAnim = eventPage.WalkAnim;
                    withBlock1.WalkThrough = eventPage.WalkThrough;
                    withBlock1.ShowName = eventPage.ShowName;
                    withBlock1.FixedDir = eventPage.DirFix;

                }
            }

            // Send spawn event packets to the player.
            using (var buffer = new ByteStream(4))
            {
                for (int i = 1; i <= Core.Type.TempPlayer[index].EventMap.CurrentEvents; i++) // Changed to start from 1, since we resized array + 1
                {
                    ref var eventPage = ref Core.Type.TempPlayer[index].EventMap.EventPages[i];
                    if (eventPage.EventID < 0) continue; //should never hit here, but just in case

                    buffer.WriteInt32((int)ServerPackets.SSpawnEvent);
                    buffer.WriteInt32(eventPage.EventID); // Map event ID.

                    buffer.WriteString(Map[mapNum].Event[eventPage.EventID].Name); // Map event ID
                    buffer.WriteInt32(eventPage.Dir);
                    buffer.WriteByte(eventPage.GraphicType);
                    buffer.WriteInt32(eventPage.Graphic);
                    buffer.WriteInt32(eventPage.GraphicX);
                    buffer.WriteInt32(eventPage.GraphicX2);
                    buffer.WriteInt32(eventPage.GraphicY);
                    buffer.WriteInt32(eventPage.GraphicY2);
                    buffer.WriteInt32(eventPage.MovementSpeed);
                    buffer.WriteInt32(eventPage.X);
                    buffer.WriteInt32(eventPage.Y);
                    buffer.WriteByte(eventPage.Position);
                    buffer.WriteBoolean(eventPage.Visible);
                    buffer.WriteInt32(Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].WalkAnim); // Use map event and page IDs
                    buffer.WriteInt32(Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].DirFix);
                    buffer.WriteInt32(Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].WalkThrough);
                    buffer.WriteInt32(Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].ShowName);
                    NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);

                    buffer.Reset(); // Reset the buffer for the next event.
                }
            }
        }


        public static bool TriggerEvent(int index, int eventId, byte triggerType, int x, int y)
        {
            // Check for valid player and map.
            if (index < 0 || index > NetworkConfig.Socket.HighIndex)
            {
                return false;
            }
            int mapNum = GetPlayerMap(index);
            if (mapNum < 0 || mapNum >= Map.Length)
            {
                return false;
            }

            // Find the correct local event index.
            int localEventIndex = -1;
            for (int z = 0; z < TempPlayer[index].EventMap.CurrentEvents; z++)
            {
                if (TempPlayer[index].EventMap.EventPages[z].EventID == eventId)
                {
                    localEventIndex = z;
                    break;
                }
            }

            if (localEventIndex == -1) return false; // Event not found for this player.

            // Get the event page data.
            ref var eventPage = ref TempPlayer[index].EventMap.EventPages[localEventIndex];

            // Check trigger type.
            if (Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].Trigger != triggerType)
            {
                return false; // Incorrect trigger.
            }

            // Adjust target coordinates based on player direction.
            switch (GetPlayerDir(index))
            {
                case (byte)DirectionType.Up:
                    if (GetPlayerY(index) > 0) y = GetPlayerY(index) - 1;
                    else return false;
                    break;
                case (byte)DirectionType.Down:
                    if (GetPlayerY(index) < Map[mapNum].MaxY) y = GetPlayerY(index) + 1;
                    else return false;
                    break;
                case (byte)DirectionType.Left:
                    if (GetPlayerX(index) > 0) x = GetPlayerX(index) - 1;
                    else return false;
                    break;
                case (byte)DirectionType.Right:
                    if (GetPlayerX(index) < Map[mapNum].MaxX) x = GetPlayerX(index) + 1;
                    else return false;
                    break;
                case (byte)DirectionType.UpRight:
                    if (GetPlayerX(index) < Map[mapNum].MaxX && GetPlayerY(index) > 0) { x = GetPlayerX(index) + 1; y = GetPlayerY(index) - 1; }
                    else return false;
                    break;
                case (byte)DirectionType.UpLeft:
                    if (GetPlayerX(index) > 0 && GetPlayerY(index) > 0) { x = GetPlayerX(index) - 1; y = GetPlayerY(index) - 1; }
                    else return false;
                    break;
                case (byte)DirectionType.DownLeft:
                    if (GetPlayerX(index) > 0 && GetPlayerY(index) < Map[mapNum].MaxY) { x = GetPlayerX(index) - 1; y = GetPlayerY(index) + 1; }
                    else return false;
                    break;
                case (byte)DirectionType.DownRight:
                    if (GetPlayerX(index) < Map[mapNum].MaxX && GetPlayerY(index) < Map[mapNum].MaxY) { x = GetPlayerX(index) + 1; y = GetPlayerY(index) + 1; }
                    else return false;
                    break;
            }
            // Check if the player is on the event's tile.
            if (x != eventPage.X || y != eventPage.Y)
            {
                return false; // Not on the event tile.
            }

            // Check for event commands and start event processing.
            if (Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].CommandListCount > 0)
            {
                // Use map event ID for indexing.
                if (TempPlayer[index].EventProcessing[eventPage.EventID].Active == 0)
                {
                    ref var eventProcessing = ref TempPlayer[index].EventProcessing[eventPage.EventID]; //And here.
                    eventProcessing.Active = 1;
                    eventProcessing.ActionTimer = General.GetTimeMs();
                    eventProcessing.CurList = 0;
                    eventProcessing.CurSlot = 0;
                    eventProcessing.EventID = eventPage.EventID; // This should be map event ID.
                    eventProcessing.PageID = eventPage.PageID;
                    eventProcessing.WaitingForResponse = 0;

                    // Allocate ListLeftOff array.
                    eventProcessing.ListLeftOff = new int[Map[mapNum].Event[eventPage.EventID].Pages[eventPage.PageID].CommandListCount];
                }
            }

            return false; // Event triggered (or not), but return false as per the original function's signature.
        }

    } // class EventLogic
} // namespace Server 
