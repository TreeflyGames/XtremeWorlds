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
        public static Core.Type.Event TmpEvent;

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
        public static Core.Type.MoveRoute[] TempMoveRoute;
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
        public static Core.Type.EventList[] EventList;
        public static Core.Type.Event CopyEvent;
        public static Core.Type.EventPage CopyEventPage;

        public static bool InEvent;
        public static bool HoldPlayer;

        public static Core.Type.Picture Picture;

        #endregion

        #region EventEditor
        public static void CopyEvent_Map(int X, int Y)
        {
            int count;
            int i;

            count = Data.MyMap.EventCount;
            if (count == 0)
                return;

            var loopTo = count;
            for (i = 0; i < loopTo; i++)
            {
                if (Data.MyMap.Event[i].X == X & Data.MyMap.Event[i].Y == Y)
                {
                    CopyEvent = Data.MyMap.Event[i];
                    return;
                }
            }

        }

        public static void PasteEvent_Map(int x, int y)
        {
            int count;
            int i;
            var EventNum = default(int);

            count = Data.MyMap.EventCount;

            if (count > 0)
            {
                var loopTo = count;
                for (i = 0; i < loopTo; i++)
                {
                    if (Data.MyMap.Event[i].X == x & Data.MyMap.Event[i].Y == y)
                    {
                        EventNum = i;
                    }
                }
            }

            // couldn't find one - create one
            if (EventNum == 0)
            {
                AddEvent(x, y, true);
                EventNum = count;
            }

            // copy it
            Data.MyMap.Event[EventNum] = CopyEvent;

            // set position
            Data.MyMap.Event[EventNum].X = x;
            Data.MyMap.Event[EventNum].Y = y;
        }

        public static void DeleteEvent(int X, int Y)
        {
            int i;
            int lowIndex = -1;
            bool shifted = false;

            if (GameState.MyEditorType != (int)EditorType.Map)
                return;

            // First pass: find all events to delete and shift others down
            var loopTo = Data.MyMap.EventCount;
            for (i = 0; i < loopTo; i++)
            {
                if (Data.MyMap.Event.Length <= i)
                    break;

                if (Data.MyMap.Event[i].X == X & Data.MyMap.Event[i].Y == Y)
                {
                    // Clear the event
                    ClearEvent(i);
                    lowIndex = i;
                    shifted = true;
                }
                else if (shifted)
                {
                    // Shift this event down to fill the gap
                    Data.MyMap.Event[lowIndex] = Data.MyMap.Event[i];
                    lowIndex = lowIndex + 1;
                }
            }

            // Adjust the event count if anything was deleted
            if (lowIndex != -1)
            {
                // Set the new count
                Data.MyMap.EventCount = lowIndex;

                var newEvents = new Core.Type.Event[Data.MyMap.EventCount];
                for (i = 0; i < Data.MyMap.EventCount; i++)
                {
                    newEvents[i] = Data.MyMap.Event[i];
                }
                Data.MyMap.Event = newEvents;

                var newMapEvents = new Core.Type.MapEvent[Data.MyMap.EventCount];
                for (i = 0; i < Data.MyMap.EventCount; i++)
                {
                    newMapEvents[i] = Data.MapEvents[i];
                }
                Data.MapEvents = newMapEvents;

                TmpEvent = default;
            }
        }


        public static void AddEvent(int X, int Y, bool cancelLoad = false)
        {
            int count;
            int pageCount;
            int i;

            count = Data.MyMap.EventCount;

            // make sure there's not already an event
            if (count > 0)
            {
                var loopTo = count;
                for (i = 0; i < loopTo; i++)
                {
                    if (Data.MyMap.Event[i].X == X & Data.MyMap.Event[i].Y == Y)
                    {
                        // already an event - edit it
                        if (!cancelLoad)
                            EventEditorInit(i);
                        return;
                    }
                }
            }

            // increment count
            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count += 1;
            }

            ClearEvent(count);
            Data.MyMap.EventCount = count;
            Array.Resize(ref Data.MyMap.Event, count);
            // set the new event
            Data.MyMap.Event[count - 1].X = X;
            Data.MyMap.Event[count - 1].Y = Y;
            // give it a new page
            pageCount = Data.MyMap.Event[count - 1].PageCount + 1;
            Data.MyMap.Event[count - 1].PageCount = pageCount;
            Array.Resize(ref Data.MyMap.Event[count - 1].Pages, pageCount);
            // load the editor
            if (!cancelLoad)
                EventEditorInit(count - 1);
        }

        public static void ClearEvent(int EventNum)
        {
            Array.Resize(ref Data.MyMap.Event, EventNum + 1);
            Array.Resize(ref Data.MapEvents, EventNum + 1);
            ref var withBlock = ref Data.MyMap.Event[EventNum];
            withBlock.Name = "";
            withBlock.PageCount = 1;
            withBlock.Pages = new Core.Type.EventPage[1];
            Array.Resize(ref withBlock.Pages[0].CommandList, 1);
            Array.Resize(ref withBlock.Pages[0].CommandList[0].Commands, 1);
            withBlock.Pages[0].CommandList[0].Commands[0].Index = -1;
            withBlock.Globals = 0;
            withBlock.X = 0;
            withBlock.Y = 0;
        }

        public static void EventEditorInit(int EventNum)
        {
            EditorEvent = EventNum;
            TmpEvent = Data.MyMap.Event[EventNum];
            GameState.InitEventEditor = true;
            if (TmpEvent.Pages[0].CommandListCount == 0)
            {
                Array.Resize(ref TmpEvent.Pages[0].CommandList, 1);
                TmpEvent.Pages[0].CommandListCount = 0;
                TmpEvent.Pages[0].CommandList[0].CommandCount = 0;
                Array.Resize(ref TmpEvent.Pages[0].CommandList[0].Commands, TmpEvent.Pages[0].CommandList[0].CommandCount);
            }
        }

        public static void EventEditorLoadPage(int PageNum)
        {
            {
                ref var withBlock = ref TmpEvent.Pages[PageNum];
                GraphicSelX = withBlock.GraphicX;
                GraphicSelY = withBlock.GraphicY;
                GraphicSelX2 = withBlock.GraphicX2;
                GraphicSelY2 = withBlock.GraphicY2;
                frmEditor_Event.Instance.cmbGraphic.SelectedIndex = withBlock.GraphicType;
                frmEditor_Event.Instance.cmbHasItem.SelectedIndex = withBlock.HasItemIndex;
                if (withBlock.HasItemAmount == 0)
                {
                    frmEditor_Event.Instance.nudCondition_HasItem.Value = 1m;
                }
                else
                {
                    frmEditor_Event.Instance.nudCondition_HasItem.Value = withBlock.HasItemAmount;
                }
                frmEditor_Event.Instance.cmbMoveFreq.SelectedIndex = withBlock.MoveFreq;
                frmEditor_Event.Instance.cmbMoveSpeed.SelectedIndex = withBlock.MoveSpeed;
                frmEditor_Event.Instance.cmbMoveType.SelectedIndex = withBlock.MoveType;
                frmEditor_Event.Instance.cmbPlayerVar.SelectedIndex = withBlock.VariableIndex;
                frmEditor_Event.Instance.cmbPlayerSwitch.SelectedIndex = withBlock.SwitchIndex;
                frmEditor_Event.Instance.cmbSelfSwitchCompare.SelectedIndex = withBlock.SelfSwitchCompare;
                frmEditor_Event.Instance.cmbPlayerSwitchCompare.SelectedIndex = withBlock.SwitchCompare;
                frmEditor_Event.Instance.cmbPlayervarCompare.SelectedIndex = withBlock.VariableCompare;
                frmEditor_Event.Instance.chkGlobal.Checked = Conversions.ToBoolean(TmpEvent.Globals);
                frmEditor_Event.Instance.cmbTrigger.SelectedIndex = withBlock.Trigger;
                frmEditor_Event.Instance.chkDirFix.Checked = Conversions.ToBoolean(withBlock.DirFix);
                frmEditor_Event.Instance.chkHasItem.Checked = Conversions.ToBoolean(withBlock.ChkHasItem);
                frmEditor_Event.Instance.chkPlayerVar.Checked = Conversions.ToBoolean(withBlock.ChkVariable);
                frmEditor_Event.Instance.chkPlayerSwitch.Checked = Conversions.ToBoolean(withBlock.ChkSwitch);
                frmEditor_Event.Instance.chkSelfSwitch.Checked = Conversions.ToBoolean(withBlock.ChkSelfSwitch);
                frmEditor_Event.Instance.chkWalkAnim.Checked = Conversions.ToBoolean(withBlock.WalkAnim);
                frmEditor_Event.Instance.chkWalkThrough.Checked = Conversions.ToBoolean(withBlock.WalkThrough);
                frmEditor_Event.Instance.chkShowName.Checked = Conversions.ToBoolean(withBlock.ShowName);
                frmEditor_Event.Instance.nudPlayerVariable.Value = withBlock.VariableCondition;
                frmEditor_Event.Instance.nudGraphic.Value = withBlock.Graphic;

                if (withBlock.ChkSelfSwitch == 0)
                {
                    frmEditor_Event.Instance.cmbSelfSwitch.Enabled = false;
                    frmEditor_Event.Instance.cmbSelfSwitchCompare.Enabled = false;
                }
                else
                {
                    frmEditor_Event.Instance.cmbSelfSwitch.Enabled = true;
                    frmEditor_Event.Instance.cmbSelfSwitchCompare.Enabled = true;
                }
                if (withBlock.ChkSwitch == 0)
                {
                    frmEditor_Event.Instance.cmbPlayerSwitch.Enabled = false;
                    frmEditor_Event.Instance.cmbPlayerSwitchCompare.Enabled = false;
                }
                else
                {
                    frmEditor_Event.Instance.cmbPlayerSwitch.Enabled = true;
                    frmEditor_Event.Instance.cmbPlayerSwitchCompare.Enabled = true;
                }
                if (withBlock.ChkVariable == 0)
                {
                    frmEditor_Event.Instance.cmbPlayerVar.Enabled = false;
                    frmEditor_Event.Instance.nudPlayerVariable.Enabled = false;
                    frmEditor_Event.Instance.cmbPlayervarCompare.Enabled = false;
                }
                else
                {
                    frmEditor_Event.Instance.cmbPlayerVar.Enabled = true;
                    frmEditor_Event.Instance.nudPlayerVariable.Enabled = true;
                    frmEditor_Event.Instance.cmbPlayervarCompare.Enabled = true;
                }
                if (frmEditor_Event.Instance.cmbMoveType.SelectedIndex == 2)
                {
                    frmEditor_Event.Instance.btnMoveRoute.Enabled = true;
                }
                else
                {
                    frmEditor_Event.Instance.btnMoveRoute.Enabled = false;
                }
                frmEditor_Event.Instance.cmbPositioning.SelectedIndex = int.Parse(withBlock.Position.ToString());
                EventListCommands();
            }

        }

        public static void EventEditorOK()
        {
            // copy the event data from the temp event
            Data.MyMap.Event[EditorEvent] = TmpEvent;
            TmpEvent = default;

            // unload the form
            frmEditor_Event.Instance.Dispose();
        }

        public static void EventListCommands()
        {
            int i;
            int curlist;
            int X;
            string indent = "";
            int[] listleftoff;
            int[] conditionalstage;

            frmEditor_Event.Instance.lstCommands.Items.Clear();

            if (TmpEvent.Pages[CurPageNum].CommandListCount > 0)
            {
                listleftoff = new int[TmpEvent.Pages[CurPageNum].CommandListCount];
                conditionalstage = new int[TmpEvent.Pages[CurPageNum].CommandListCount];
                curlist = 0;
                X = 0;
                Array.Resize(ref EventList, X + 1);
            newlist:
                var loopTo = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
                for (i = 0; i < loopTo; i++)
                {
                    if (listleftoff[curlist] > 0)
                    {
                        if ((TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[listleftoff[curlist]].Index == (int)Core.EventCommand.ConditionalBranch | TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[listleftoff[curlist]].Index == (int)Core.EventCommand.ShowChoices) & conditionalstage[curlist] != 0)
                        {
                            i = listleftoff[curlist];
                        }
                        else if (listleftoff[curlist] >= i)
                        {
                            i = listleftoff[curlist] + 1;
                        }
                    }
                    if (i < TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                    {
                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Index == (int)Core.EventCommand.ConditionalBranch)
                        {
                            X = X + 1;
                            Array.Resize(ref EventList, X + 1);
                            switch (conditionalstage[curlist])
                            {
                                case 0:
                                    {
                                        EventList[X].CommandList = curlist;
                                        EventList[X].CommandNum = i;
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Condition)
                                        {
                                            case 0:
                                                {
                                                    switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2)
                                                    {
                                                        case 0:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + 1 + "] == " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + 1 + "] >= " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + 1 + "] <= " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + 1 + "] > " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + 1 + "] < " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + 1 + "] != " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                    }

                                                    break;
                                                }
                                            case 1:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Switch [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Switches[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + 1] + "] == " + "True");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Switch [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Switches[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + 1] + "] == " + "False");
                                                    }

                                                    break;
                                                }
                                            case 2:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Has Item [" + Core.Data.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1].Name + "] x" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2);
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Job Is [" + Strings.Trim(Data.Job[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1].Name) + "]");
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Knows Skill [" + Strings.Trim(Data.Skill[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1].Name) + "]");
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2)
                                                    {
                                                        case 0:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Level is == " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1);
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Level is >= " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Level is <= " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1);
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Level is > " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1);
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Level is < " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1);
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Level is NOT " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1);
                                                                break;
                                                            }
                                                    }

                                                    break;
                                                }
                                            case 6:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 0)
                                                    {
                                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1)
                                                        {
                                                            case 0:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [A] == " + "True");
                                                                    break;
                                                                }
                                                            case 1:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [B] == " + "True");
                                                                    break;
                                                                }
                                                            case 2:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [C] == " + "True");
                                                                    break;
                                                                }
                                                            case 3:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [D] == " + "True");
                                                                    break;
                                                                }
                                                        }
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 1)
                                                    {
                                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1)
                                                        {
                                                            case 0:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [A] == " + "False");
                                                                    break;
                                                                }
                                                            case 1:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [B] == " + "False");
                                                                    break;
                                                                }
                                                            case 2:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [C] == " + "False");
                                                                    break;
                                                                }
                                                            case 3:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Self Switch [D] == " + "False");
                                                                    break;
                                                                }
                                                        }
                                                    }

                                                    break;
                                                }
                                            case 7:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 0)
                                                    {
                                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3)
                                                        {
                                                            case 0:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Quest [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + "] not started.");
                                                                    break;
                                                                }
                                                            case 1:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Quest [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + "] is started.");
                                                                    break;
                                                                }
                                                            case 2:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Quest [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + "] is completed.");
                                                                    break;
                                                                }
                                                            case 3:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Quest [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + "] can be started.");
                                                                    break;
                                                                }
                                                            case 4:
                                                                {
                                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Quest [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + "] can be ended. (All tasks complete)");
                                                                    break;
                                                                }
                                                        }
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Quest [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + "] in progress and on task #" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                    }

                                                    break;
                                                }
                                            case 8:
                                                {
                                                    switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1)
                                                    {
                                                        case (int)Sex.Male:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Gender is Male");
                                                                break;
                                                            }
                                                        case (int)Sex.Female:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's  Gender is Female");
                                                                break;
                                                            }
                                                    }

                                                    break;
                                                }
                                            case 9:
                                                {
                                                    switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1)
                                                    {
                                                        case (int)TimeOfDay.Day:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Time of Day is Day");
                                                                break;
                                                            }
                                                        case (int)TimeOfDay.Night:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Time of Day is Night");
                                                                break;
                                                            }
                                                        case (int)TimeOfDay.Dawn:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Time of Day is Dawn");
                                                                break;
                                                            }
                                                        case (int)TimeOfDay.Dusk:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Time of Day is Dusk");
                                                                break;
                                                            }
                                                    }

                                                    break;
                                                }
                                        }
                                        indent = indent + "       ";
                                        listleftoff[curlist] = i;
                                        conditionalstage[curlist] = 1;
                                        curlist = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.CommandList;
                                        goto newlist;
                                        break;
                                    }
                                case 1:
                                    {
                                        EventList[X].CommandList = curlist;
                                        EventList[X].CommandNum = 0;
                                        frmEditor_Event.Instance.lstCommands.Items.Add(Strings.Mid(indent, 1, Strings.Len(indent) - 4) + " : " + "Else");
                                        listleftoff[curlist] = i;
                                        conditionalstage[curlist] = 2;
                                        curlist = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.ElseCommandList;
                                        goto newlist;
                                        break;
                                    }
                                case 2:
                                    {
                                        EventList[X].CommandList = curlist;
                                        EventList[X].CommandNum = 0;
                                        frmEditor_Event.Instance.lstCommands.Items.Add(Strings.Mid(indent, 1, Strings.Len(indent) - 4) + " : " + "End Branch");
                                        indent = Strings.Mid(indent, 1, Strings.Len(indent) - 7);
                                        listleftoff[curlist] = i;
                                        conditionalstage[curlist] = 0;
                                        break;
                                    }
                            }
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Index == (int)Core.EventCommand.ShowChoices)
                        {
                            X = X + 1;
                            switch (conditionalstage[curlist])
                            {
                                case 0:
                                    {
                                        Array.Resize(ref EventList, X + 1);
                                        EventList[X].CommandList = curlist;
                                        EventList[X].CommandNum = i;
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Choices - Prompt: " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20));
                                        indent = indent + "       ";
                                        listleftoff[curlist] = i;
                                        conditionalstage[curlist] = 1;
                                        goto newlist;
                                        break;
                                    }
                                case 1:
                                    {
                                        if (!string.IsNullOrEmpty(Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text2)))
                                        {
                                            Array.Resize(ref EventList, X + 1);
                                            EventList[X].CommandList = 7;
                                            EventList[X].CommandNum = 0;
                                            frmEditor_Event.Instance.lstCommands.Items.Add(Strings.Mid(indent, 1, Strings.Len(indent) - 4) + " : " + "When [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text2) + "]");
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 2;
                                            curlist = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1;
                                            goto newlist;
                                        }
                                        else
                                        {
                                            X = X - 1;
                                            Array.Resize(ref EventList, X + 1);
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 2;
                                            curlist = curlist;
                                            goto newlist;
                                        }

                                        break;
                                    }
                                case 2:
                                    {
                                        if (!string.IsNullOrEmpty(Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text3)))
                                        {
                                            Array.Resize(ref EventList, X + 1);
                                            EventList[X].CommandList = curlist;
                                            EventList[X].CommandNum = 0;
                                            frmEditor_Event.Instance.lstCommands.Items.Add(Strings.Mid(indent, 1, Strings.Len(indent) - 4) + " : " + "When [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text3) + "]");
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 3;
                                            curlist = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2;
                                            goto newlist;
                                        }
                                        else
                                        {
                                            X = X - 1;
                                            Array.Resize(ref EventList, X + 1);
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 3;
                                            curlist = curlist;
                                            goto newlist;
                                        }

                                        break;
                                    }
                                case 3:
                                    {
                                        if (!string.IsNullOrEmpty(Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text4)))
                                        {
                                            Array.Resize(ref EventList, X + 1);
                                            EventList[X].CommandList = curlist;
                                            EventList[X].CommandNum = 0;
                                            frmEditor_Event.Instance.lstCommands.Items.Add(Strings.Mid(indent, 1, Strings.Len(indent) - 4) + " : " + "When [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text4) + "]");
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 4;
                                            curlist = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3;
                                            goto newlist;
                                        }
                                        else
                                        {
                                            X = X - 1;
                                            Array.Resize(ref EventList, X + 1);
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 4;
                                            curlist = curlist;
                                            goto newlist;
                                        }

                                        break;
                                    }
                                case 4:
                                    {
                                        if (!string.IsNullOrEmpty(Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text5)))
                                        {
                                            Array.Resize(ref EventList, X + 1);
                                            EventList[X].CommandList = curlist;
                                            EventList[X].CommandNum = 0;
                                            frmEditor_Event.Instance.lstCommands.Items.Add(Strings.Mid(indent, 1, Strings.Len(indent) - 4) + " : " + "When [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text5) + "]");
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 5;
                                            curlist = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4;
                                            goto newlist;
                                        }
                                        else
                                        {
                                            X = X - 1;
                                            Array.Resize(ref EventList, X + 1);
                                            listleftoff[curlist] = i;
                                            conditionalstage[curlist] = 5;
                                            curlist = curlist;
                                            goto newlist;
                                        }

                                        break;
                                    }
                                case 5:
                                    {
                                        Array.Resize(ref EventList, X + 1);
                                        EventList[X].CommandList = curlist;
                                        EventList[X].CommandNum = 0;
                                        frmEditor_Event.Instance.lstCommands.Items.Add(Strings.Mid(indent, 1, Strings.Len(indent) - 4) + " : " + "Branch End");
                                        indent = Strings.Mid(indent, 1, Strings.Len(indent) - 7);
                                        listleftoff[curlist] = i;
                                        conditionalstage[curlist] = 0;
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            X = X + 1;
                            Array.Resize(ref EventList, X + 1);
                            EventList[X].CommandList = curlist;
                            EventList[X].CommandNum = i;
                            switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Index)
                            {
                                case (byte)Core.EventCommand.AddText:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2)
                                        {
                                            case 0:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(Operators.ConcatenateObject(Operators.ConcatenateObject(indent + "@>" + "Add Text - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - Color: ", GetColorString(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)), " - Chat Type: Player"));
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(Operators.ConcatenateObject(Operators.ConcatenateObject(indent + "@>" + "Add Text - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - Color: ", GetColorString(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)), " - Chat Type: Map"));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(Operators.ConcatenateObject(Operators.ConcatenateObject(indent + "@>" + "Add Text - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - Color: ", GetColorString(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)), " - Chat Type: Global"));
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.ShowText:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Text - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20));
                                        break;
                                    }
                                case (byte)Core.EventCommand.ModifyVariable:
                                    {
                                        string variableValue = Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1];
                                        if (variableValue == "")
                                            variableValue = ": None";
                                        else
                                            variableValue = ": " + variableValue;

                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2)
                                        {
                                            case 0:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + variableValue + "] == " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + variableValue + "] + " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + variableValue + "] - " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + variableValue + "] Random Between " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " and " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4);
                                                    break;
                                                }
                                        }
   
                                        break;
                                    }
                                case (byte)Core.EventCommand.ModifySwitch:
                                    {
                                        string switchValue = Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1];
                                        if (switchValue == "")
                                            switchValue = ": None";
                                        else
                                            switchValue = ": " + switchValue;

                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Switch [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + switchValue + "] == False");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Switch [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + switchValue + "] == True");
                                        }
                                        
                                        break;
                                    }
                                case (byte)Core.EventCommand.ModifySelfSwitch:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)
                                        {
                                            case 0:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [A] to Off");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [A] to On");
                                                    }

                                                    break;
                                                }
                                            case 1:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [B] to Off");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [B] to On");
                                                    }

                                                    break;
                                                }
                                            case 2:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [C] to Off");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [C] to On");
                                                    }

                                                    break;
                                                }
                                            case 3:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [D] to Off");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [D] to On");
                                                    }

                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.ExitEventProcess:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Exit Event Processing");
                                        break;
                                    }
                                case (byte)Core.EventCommand.ChangeItems:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Item Amount of [" + Core.Data.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "] to " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Give Player " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " " + Core.Data.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "(s)");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 2)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Take " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " " + Core.Data.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "(s) from Player.");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.RestoreHealth:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Restore Player HP");
                                        break;
                                    }
                                case (byte)Core.EventCommand.RestoreMana:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Restore Player MP");
                                        break;
                                    }
                                case (byte)Core.EventCommand.RestoreStamina:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Restore Player SP");
                                        break;
                                    }
                                case (byte)Core.EventCommand.LevelUp:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Level Up Player");
                                        break;
                                    }
                                case (byte)Core.EventCommand.ChangeLevel:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Level to " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1);
                                        break;
                                    }
                                case (byte)Core.EventCommand.ChangeSkills:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Teach Player Skill [" + Strings.Trim(Data.Skill[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name) + "]");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Remove Player Skill [" + Strings.Trim(Data.Skill[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name) + "]");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.ChangeJob:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Job to " + Strings.Trim(Data.Job[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name));
                                        break;
                                    }
                                case (byte)Core.EventCommand.ChangeSprite:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Sprite to " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1);
                                        break;
                                    }
                                case (byte)Core.EventCommand.ChangeSex:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Sex to Male.");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Sex to Female.");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.SetPlayerKillable:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player PK to No.");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player PK to Yes.");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.WarpPlayer:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") while retaining direction.");
                                        }
                                        else
                                        {
                                            switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4 - 1)
                                            {
                                                case (int)Direction.Up:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing upward.");
                                                        break;
                                                    }
                                                case (int)Direction.Down:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing downward.");
                                                        break;
                                                    }
                                                case (int)Direction.Left:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing left.");
                                                        break;
                                                    }
                                                case (int)Direction.Right:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing right.");
                                                        break;
                                                    }
                                            }
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.SetMoveRoute:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 <= Data.MyMap.EventCount)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Move Route for Event #" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " [" + Data.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]");
                                        }
                                        else
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Move Route for COULD NOT FIND EVENT!");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.PlayAnimation:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Animation " + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + " [" + Data.Animation[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]" + " On Player");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Animation " + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + " [" + Data.Animation[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]" + " On Event " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " [" + Strings.Trim(Data.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3].Name) + "]");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 2)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Animation " + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + 1) + " [" + Data.Animation[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]" + " On Tile (" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4 + ")");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.PlayBgm:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play BGM [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1 + "]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.FadeOutBgm:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Fadeout BGM");
                                        break;
                                    }
                                case (byte)Core.EventCommand.PlaySound:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Sound [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1 + "]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.StopSound:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Stop Sound");
                                        break;
                                    }
                                case (byte)Core.EventCommand.OpenBank:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Open Bank");
                                        break;
                                    }
                                case (byte)Core.EventCommand.OpenShop:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Open Shop [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ". " + Data.Shop[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.SetAccessLevel:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Access [" + frmEditor_Event.Instance.cmbSetAccess.Items[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 - 1]);
                                        break;
                                    }
                                case (byte)Core.EventCommand.GiveExperience:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Give Player " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Experience.");
                                        break;
                                    }
                                case (byte)Core.EventCommand.ShowChatBubble:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)
                                        {
                                            case (int)TargetType.Player:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On Player");
                                                    break;
                                                }
                                            case (int)TargetType.Npc:
                                                {
                                                    if (Data.MyMap.Npc[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2] <= 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On Npc [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + 1).ToString() + ". ]");
                                                    }
                                                    else
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On Npc [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + 1).ToString() + ". " + Data.Npc[Data.MyMap.Npc[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2]].Name + "]");
                                                    }

                                                    break;
                                                }
                                            case (int)TargetType.Event:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On Event [" + (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + 1).ToString() + ". " + Data.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2].Name + "]");
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.Label:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Label: [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1) + "]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.GoToLabel:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Jump to Label: [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1) + "]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.SpawnNpc:
                                    {
                                        if (Data.MyMap.Npc[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] <= 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Spawn Npc: [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ". " + "]");
                                        }
                                        else
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Spawn Npc: [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ". " + Data.Npc[Data.MyMap.Npc[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1]].Name + "]");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.FadeIn:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Fade In");
                                        break;
                                    }
                                case (byte)Core.EventCommand.FadeOut:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Fade Out");
                                        break;
                                    }
                                case (byte)Core.EventCommand.FlashScreen:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Flash White");
                                        break;
                                    }
                                case (byte)Core.EventCommand.SetFog:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Fog [Fog: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + " Speed: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + " Opacity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3.ToString() + "]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.SetWeather:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)
                                        {
                                            case (int)WeatherType.None:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [None]");
                                                    break;
                                                }
                                            case (int)WeatherType.Rain:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Rain - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                            case (int)WeatherType.Snow:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Snow - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                            case (int)WeatherType.Sandstorm:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Sand Storm - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                            case (int)WeatherType.Storm:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Storm - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.SetScreenTint:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Map Tint RGBA [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3.ToString() + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4.ToString() + "]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.Wait:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Wait " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + " Ms");
                                        break;
                                    }
                                case (byte)Core.EventCommand.ShowPicture:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2)
                                        {
                                            case 0:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Picture " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ": Pic=" + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2) + " Top Left, X: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4) + " Y: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data5));
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Picture " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ": Pic=" + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2) + " Center Screen, X: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4) + " Y: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data5));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Picture " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ": Pic=" + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2) + " On Event, X: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4) + " Y: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data5));
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Picture " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ": Pic=" + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2) + " On Player, X: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4) + " Y: " + Conversion.Str(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data5));
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.HidePicture:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Hide Picture " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString());
                                        break;
                                    }
                                case (byte)Core.EventCommand.WaitMovementCompletion:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 <= Data.MyMap.EventCount)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Wait for Event #" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " [" + Strings.Trim(Data.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name) + "] to complete move route.");
                                        }
                                        else
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Wait for COULD NOT FIND EVENT to complete move route.");
                                        }

                                        break;
                                    }
                                case (byte)Core.EventCommand.HoldPlayer:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Hold Player [Do not allow player to move.]");
                                        break;
                                    }
                                case (byte)Core.EventCommand.ReleasePlayer:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Release Player [Allow player to turn and move again.]");
                                        break;
                                    }

                                default:
                                    {
                                        // Ghost
                                        X = X - 1;
                                        if (X == -1)
                                        {
                                            EventList = new Core.Type.EventList[1];
                                        }
                                        else
                                        {
                                            Array.Resize(ref EventList, X + 1);
                                        }

                                        break;
                                    }
                            }
                        }
                    }
                }

                if (curlist > 1)
                {
                    X = X + 1;
                    Array.Resize(ref EventList, X + 1);
                    EventList[X].CommandList = curlist;
                    EventList[X].CommandNum = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@> ");
                    curlist = TmpEvent.Pages[CurPageNum].CommandList[curlist].ParentList;
                    goto newlist;
                }
            }
            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@> ");

            var z = default(int);
            X = 0;
            var loopTo1 = frmEditor_Event.Instance.lstCommands.Items.Count;
            for (i = 0; i < loopTo1; i++)
            {
                if (X > z)
                    z = X;
            }

        }

        public static void AddCommand(int Index)
        {
            int curlist;
            var i = default(int);
            var X = default(int);
            int curslot;
            int p;
            Core.Type.CommandList oldCommandList;

            if (frmEditor_Event.Instance.lstCommands.SelectedIndex == -1 || EventList == null)
            {
                curlist = 0;
            }
            else
            {
                curlist = EventList[frmEditor_Event.Instance.lstCommands.SelectedIndex].CommandList;
            }

            TmpEvent.Pages[CurPageNum].CommandListCount += 1;
            Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList, TmpEvent.Pages[CurPageNum].CommandListCount);
            TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount += 1;
            p = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
            Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands, p);

            if (frmEditor_Event.Instance.lstCommands.SelectedIndex + 1 == frmEditor_Event.Instance.lstCommands.Items.Count)
            {
                curslot = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount - 1;
            }
            else
            {
                oldCommandList = TmpEvent.Pages[CurPageNum].CommandList[curlist];
                TmpEvent.Pages[CurPageNum].CommandList[curlist].ParentList = oldCommandList.ParentList;

                var loopTo = p;
                for (i = 0; i < loopTo; i++)
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i] = oldCommandList.Commands[i];

                i = EventList[frmEditor_Event.Instance.lstCommands.SelectedIndex].CommandNum;
                if (i <= TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                {

                    var loopTo1 = i;
                    for (X = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount; X < loopTo1; X++)
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[X + 1] = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[X];

                    curslot = EventList[frmEditor_Event.Instance.lstCommands.SelectedIndex].CommandNum;
                }
                else
                {
                    curslot = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
                }

            }

            switch (Index)
            {
                case (int)Core.EventCommand.AddText:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtAddText_Text.Text;
                        if (frmEditor_Event.Instance.optAddText_Player.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.optAddText_Map.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }
                        else if (frmEditor_Event.Instance.optAddText_Global.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 2;
                        }

                        break;
                    }
                case (int)Core.EventCommand.ConditionalBranch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandListCount += 1;
                        Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList, TmpEvent.Pages[CurPageNum].CommandListCount);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.CommandList = TmpEvent.Pages[CurPageNum].CommandListCount;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.ElseCommandList = TmpEvent.Pages[CurPageNum].CommandListCount;
                        TmpEvent.Pages[CurPageNum].CommandList[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.CommandList].ParentList = curlist;
                        TmpEvent.Pages[CurPageNum].CommandList[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.ElseCommandList].ParentList = curlist;

                        if (frmEditor_Event.Instance.optCondition0.Checked == true)
                            X = 0;
                        if (frmEditor_Event.Instance.optCondition1.Checked == true)
                            X = 1;
                        if (frmEditor_Event.Instance.optCondition2.Checked == true)
                            X = 2;
                        if (frmEditor_Event.Instance.optCondition3.Checked == true)
                            X = 3;
                        if (frmEditor_Event.Instance.optCondition4.Checked == true)
                            X = 4;
                        if (frmEditor_Event.Instance.optCondition5.Checked == true)
                            X = 5;
                        if (frmEditor_Event.Instance.optCondition6.Checked == true)
                            X = 6;
                        if (frmEditor_Event.Instance.optCondition8.Checked == true)
                            X = 8;
                        if (frmEditor_Event.Instance.optCondition9.Checked == true)
                            X = 9;

                        switch (X)
                        {
                            case 0: // Player Var
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 0;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_PlayerVarIndex.SelectedIndex;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondition_PlayerVarCompare.SelectedIndex;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data3 = (int)Math.Round(frmEditor_Event.Instance.nudCondition_PlayerVarCondition.Value);
                                    break;
                                }
                            case 1: // Player Switch
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 1;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_PlayerSwitch.SelectedIndex;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondtion_PlayerSwitchCondition.SelectedIndex;
                                    break;
                                }
                            case 2: // Has Item
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 2;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_HasItem.SelectedIndex;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = (int)Math.Round(frmEditor_Event.Instance.nudCondition_HasItem.Value);
                                    break;
                                }
                            case 3: // Job Is
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 3;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_JobIs.SelectedIndex;
                                    break;
                                }
                            case 4: // Learnt Skill
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 4;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_LearntSkill.SelectedIndex;
                                    break;
                                }
                            case 5: // Level Is
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 5;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = (int)Math.Round(frmEditor_Event.Instance.nudCondition_LevelAmount.Value);
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondition_LevelCompare.SelectedIndex;
                                    break;
                                }
                            case 6: // Self Switch
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 6;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_SelfSwitch.SelectedIndex;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondition_SelfSwitchCondition.SelectedIndex;
                                    break;
                                }
                            case 7:
                                {
                                    break;
                                }

                            case 8: // Gender
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 8;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_Gender.SelectedIndex;
                                    break;
                                }
                            case 9: // Time
                                {
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 9;
                                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_Time.SelectedIndex;
                                    break;
                                }
                        }

                        break;
                    }

                case (int)Core.EventCommand.ShowText:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        string tmptxt = "";
                        var loopTo2 = Information.UBound(frmEditor_Event.Instance.txtShowText.Lines);
                        for (i = 0; i <= loopTo2; i++)
                            tmptxt = tmptxt + frmEditor_Event.Instance.txtShowText.Lines[i];
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = tmptxt;
                        break;
                    }

                case (int)Core.EventCommand.ShowChoices:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChoicePrompt.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text2 = frmEditor_Event.Instance.txtChoices1.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text3 = frmEditor_Event.Instance.txtChoices2.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text4 = frmEditor_Event.Instance.txtChoices3.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text5 = frmEditor_Event.Instance.txtChoices4.Text;
                        TmpEvent.Pages[CurPageNum].CommandListCount += 3;
                        Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList, TmpEvent.Pages[CurPageNum].CommandListCount + 1);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = TmpEvent.Pages[CurPageNum].CommandListCount - 3;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = TmpEvent.Pages[CurPageNum].CommandListCount - 2;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = TmpEvent.Pages[CurPageNum].CommandListCount - 1;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = TmpEvent.Pages[CurPageNum].CommandListCount;
                        TmpEvent.Pages[CurPageNum].CommandList[TmpEvent.Pages[CurPageNum].CommandListCount - 3].ParentList = curlist;
                        TmpEvent.Pages[CurPageNum].CommandList[TmpEvent.Pages[CurPageNum].CommandListCount - 2].ParentList = curlist;
                        TmpEvent.Pages[CurPageNum].CommandList[TmpEvent.Pages[CurPageNum].CommandListCount - 1].ParentList = curlist;
                        TmpEvent.Pages[CurPageNum].CommandList[TmpEvent.Pages[CurPageNum].CommandListCount].ParentList = curlist;
                        break;
                    }

                case (int)Core.EventCommand.ModifyVariable:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbVariable.SelectedIndex;

                        if (frmEditor_Event.Instance.optVariableAction0.Checked == true)
                            i = 0;
                        if (frmEditor_Event.Instance.optVariableAction1.Checked == true)
                            i = 1;
                        if (frmEditor_Event.Instance.optVariableAction2.Checked == true)
                            i = 2;
                        if (frmEditor_Event.Instance.optVariableAction3.Checked == true)
                            i = 3;

                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = i;
                        if (i == 3)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData3.Value);
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData4.Value);
                        }
                        else if (i == 0)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData0.Value);
                        }
                        else if (i == 1)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData1.Value);
                        }
                        else if (i == 2)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData2.Value);
                        }

                        break;
                    }

                case (int)Core.EventCommand.ModifySwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPlayerSwitchSet.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.ModifySelfSwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetSelfSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbSetSelfSwitchTo.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.ExitEventProcess:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.ChangeItems:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeItemIndex.SelectedIndex;
                        if (frmEditor_Event.Instance.optChangeItemSet.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.optChangeItemAdd.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }
                        else if (frmEditor_Event.Instance.optChangeItemRemove.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 2;
                        }
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudChangeItemsAmount.Value);
                        break;
                    }

                case (int)Core.EventCommand.RestoreHealth:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.RestoreMana:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.RestoreStamina:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.LevelUp:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.ChangeLevel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeLevel.Value);
                        break;
                    }

                case (int)Core.EventCommand.ChangeSkills:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeSkills.SelectedIndex;
                        if (frmEditor_Event.Instance.optChangeSkillsAdd.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.optChangeSkillsRemove.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }

                        break;
                    }

                case (int)Core.EventCommand.ChangeJob:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeJob.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.ChangeSprite:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeSprite.Value);
                        break;
                    }

                case (int)Core.EventCommand.ChangeSex:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        if (frmEditor_Event.Instance.optChangeSexMale.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Sex.Male;
                        }
                        else if (frmEditor_Event.Instance.optChangeSexFemale.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Sex.Female;
                        }

                        break;
                    }

                case (int)Core.EventCommand.SetPlayerKillable:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetPK.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.WarpPlayer:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWPMap.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWPX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudWPY.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = frmEditor_Event.Instance.cmbWarpPlayerDir.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.SetMoveRoute:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = ListOfEvents[frmEditor_Event.Instance.cmbEvent.SelectedIndex];
                        if (frmEditor_Event.Instance.chkIgnoreMove.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }
                        else
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }

                        if (frmEditor_Event.Instance.chkRepeatRoute.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = 1;
                        }
                        else
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = 0;
                        }

                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].MoveRouteCount = TempMoveRouteCount;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].MoveRoute = TempMoveRoute;
                        break;
                    }

                case (int)Core.EventCommand.PlayAnimation:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbPlayAnim.SelectedIndex;
                        if (frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex == 0)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex == 1)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = frmEditor_Event.Instance.cmbPlayAnimEvent.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex == 2 == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 2;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudPlayAnimTileX.Value);
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudPlayAnimTileY.Value);
                        }

                        break;
                    }

                case (int)Core.EventCommand.PlayBgm:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.MusicCache[frmEditor_Event.Instance.cmbPlayBGM.SelectedIndex];
                        break;
                    }

                case (int)Core.EventCommand.FadeOutBgm:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.PlaySound:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.SoundCache[frmEditor_Event.Instance.cmbPlaySound.SelectedIndex];
                        break;
                    }

                case (int)Core.EventCommand.StopSound:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.OpenBank:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.OpenShop:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbOpenShop.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.SetAccessLevel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetAccess.SelectedIndex + 1;
                        break;
                    }

                case (int)Core.EventCommand.GiveExperience:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudGiveExp.Value);
                        break;
                    }

                case (int)Core.EventCommand.ShowChatBubble:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChatbubbleText.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChatBubbleTargetType.SelectedIndex + 1;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbChatBubbleTarget.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.Label:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtLabelName.Text;
                        break;
                    }

                case (int)Core.EventCommand.GoToLabel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtGoToLabel.Text;
                        break;
                    }

                case (int)Core.EventCommand.SpawnNpc:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSpawnNpc.SelectedIndex;
                        break;
                    }

                case (int)Core.EventCommand.FadeIn:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.FadeOut:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.FlashScreen:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.SetFog:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudFogData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudFogData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudFogData2.Value);
                        break;
                    }

                case (int)Core.EventCommand.SetWeather:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.CmbWeather.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWeatherIntensity.Value);
                        break;
                    }

                case (int)Core.EventCommand.SetScreenTint:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData2.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData3.Value);
                        break;
                    }

                case (int)Core.EventCommand.Wait:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWaitAmount.Value);
                        break;
                    }

                case (int)Core.EventCommand.ShowPicture:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudShowPicture.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPicLoc.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetY.Value);
                        break;
                    }

                case (int)Core.EventCommand.HidePicture:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.WaitMovementCompletion:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = ListOfEvents[frmEditor_Event.Instance.cmbMoveWait.SelectedIndex];
                        break;
                    }

                case (int)Core.EventCommand.HoldPlayer:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.EventCommand.ReleasePlayer:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }
            }

            EventListCommands();

        }

        public static void EditEventCommand()
        {
            int i;
            var X = default(int);
            int curlist;
            int curslot;

            i = frmEditor_Event.Instance.lstCommands.SelectedIndex + 1;
            if (i == -1)
                return;

            if (i > Information.UBound(EventList))
                return;

            frmEditor_Event.Instance.fraConditionalBranch.Visible = false;
            frmEditor_Event.Instance.fraDialogue.BringToFront();

            curlist = EventList[i].CommandList;
            curslot = EventList[i].CommandNum;

            if (curlist > TmpEvent.Pages[CurPageNum].CommandListCount)
                return;

            if (TmpEvent.Pages[CurPageNum].CommandList == null)
                return;

            if (curslot > TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                return;

            switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index)
            {
                case (byte)Core.EventCommand.AddText:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtAddText_Text.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        // frmEditor_Event.Instance.scrlAddText_Color.Value = tmpEvent.Pages(curPageNum).CommandList(curlist).Commands(curslot).Data1
                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2)
                        {
                            case 0:
                                {
                                    frmEditor_Event.Instance.optAddText_Player.Checked = true;
                                    break;
                                }
                            case 1:
                                {
                                    frmEditor_Event.Instance.optAddText_Map.Checked = true;
                                    break;
                                }
                            case 2:
                                {
                                    frmEditor_Event.Instance.optAddText_Global.Checked = true;
                                    break;
                                }
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraAddText.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ConditionalBranch:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraConditionalBranch.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        frmEditor_Event.Instance.ClearConditionFrame();

                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition)
                        {
                            case 0:
                                {
                                    frmEditor_Event.Instance.optCondition0.Checked = true;
                                    break;
                                }
                            case 1:
                                {
                                    frmEditor_Event.Instance.optCondition1.Checked = true;
                                    break;
                                }
                            case 2:
                                {
                                    frmEditor_Event.Instance.optCondition2.Checked = true;
                                    break;
                                }
                            case 3:
                                {
                                    frmEditor_Event.Instance.optCondition3.Checked = true;
                                    break;
                                }
                            case 4:
                                {
                                    frmEditor_Event.Instance.optCondition4.Checked = true;
                                    break;
                                }
                            case 5:
                                {
                                    frmEditor_Event.Instance.optCondition5.Checked = true;
                                    break;
                                }
                            case 6:
                                {
                                    frmEditor_Event.Instance.optCondition6.Checked = true;
                                    break;
                                }
                            case 7:
                                {
                                    break;
                                }

                            case 8:
                                {
                                    frmEditor_Event.Instance.optCondition8.Checked = true;
                                    break;
                                }
                            case 9:
                                {
                                    frmEditor_Event.Instance.optCondition9.Checked = true;
                                    break;
                                }
                        }

                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition)
                        {
                            case 0:
                                {
                                    frmEditor_Event.Instance.cmbCondition_PlayerVarIndex.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_PlayerVarCompare.Enabled = true;
                                    frmEditor_Event.Instance.nudCondition_PlayerVarCondition.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_PlayerVarIndex.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    frmEditor_Event.Instance.cmbCondition_PlayerVarCompare.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    frmEditor_Event.Instance.nudCondition_PlayerVarCondition.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data3;
                                    break;
                                }
                            case 1:
                                {
                                    frmEditor_Event.Instance.cmbCondition_PlayerSwitch.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondtion_PlayerSwitchCondition.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_PlayerSwitch.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    frmEditor_Event.Instance.cmbCondtion_PlayerSwitchCondition.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    break;
                                }
                            case 2:
                                {
                                    frmEditor_Event.Instance.cmbCondition_HasItem.Enabled = true;
                                    frmEditor_Event.Instance.nudCondition_HasItem.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_HasItem.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    frmEditor_Event.Instance.nudCondition_HasItem.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    break;
                                }
                            case 3:
                                {
                                    frmEditor_Event.Instance.cmbCondition_JobIs.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_JobIs.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    break;
                                }
                            case 4:
                                {
                                    frmEditor_Event.Instance.cmbCondition_LearntSkill.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_LearntSkill.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    break;
                                }
                            case 5:
                                {
                                    frmEditor_Event.Instance.cmbCondition_LevelCompare.Enabled = true;
                                    frmEditor_Event.Instance.nudCondition_LevelAmount.Enabled = true;
                                    frmEditor_Event.Instance.nudCondition_LevelAmount.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    frmEditor_Event.Instance.cmbCondition_LevelCompare.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    break;
                                }
                            case 6:
                                {
                                    frmEditor_Event.Instance.cmbCondition_SelfSwitch.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_SelfSwitchCondition.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_SelfSwitch.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    frmEditor_Event.Instance.cmbCondition_SelfSwitchCondition.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    break;
                                }
                            case 7:
                                {
                                    break;
                                }

                            case 8:
                                {
                                    frmEditor_Event.Instance.cmbCondition_Gender.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_Gender.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    break;
                                }
                            case 9:
                                {
                                    frmEditor_Event.Instance.cmbCondition_Time.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_Time.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1;
                                    break;
                                }
                        }

                        break;
                    }
                case (byte)Core.EventCommand.ShowText:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtShowText.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraShowText.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ShowChoices:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtChoicePrompt.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.txtChoices1.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text2;
                        frmEditor_Event.Instance.txtChoices2.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text3;
                        frmEditor_Event.Instance.txtChoices3.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text4;
                        frmEditor_Event.Instance.txtChoices4.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text5;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraShowChoices.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ModifyVariable:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbVariable.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2)
                        {
                            case 0:
                                {
                                    frmEditor_Event.Instance.optVariableAction0.Checked = true;
                                    frmEditor_Event.Instance.nudVariableData0.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                                    break;
                                }
                            case 1:
                                {
                                    frmEditor_Event.Instance.optVariableAction1.Checked = true;
                                    frmEditor_Event.Instance.nudVariableData1.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                                    break;
                                }
                            case 2:
                                {
                                    frmEditor_Event.Instance.optVariableAction2.Checked = true;
                                    frmEditor_Event.Instance.nudVariableData2.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                                    break;
                                }
                            case 3:
                                {
                                    frmEditor_Event.Instance.optVariableAction3.Checked = true;
                                    frmEditor_Event.Instance.nudVariableData3.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                                    frmEditor_Event.Instance.nudVariableData4.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4;
                                    break;
                                }
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlayerVariable.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ModifySwitch:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSwitch.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.cmbPlayerSwitchSet.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlayerSwitch.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ModifySelfSwitch:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSetSelfSwitch.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.cmbSetSelfSwitchTo.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetSelfSwitch.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeItems:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbChangeItemIndex.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 0)
                        {
                            frmEditor_Event.Instance.optChangeItemSet.Checked = true;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 1)
                        {
                            frmEditor_Event.Instance.optChangeItemAdd.Checked = true;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 2)
                        {
                            frmEditor_Event.Instance.optChangeItemRemove.Checked = true;
                        }
                        frmEditor_Event.Instance.nudChangeItemsAmount.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeItems.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeLevel:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudChangeLevel.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeLevel.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeSkills:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbChangeSkills.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 0)
                        {
                            frmEditor_Event.Instance.optChangeSkillsAdd.Checked = true;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 1)
                        {
                            frmEditor_Event.Instance.optChangeSkillsRemove.Checked = true;
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeSkills.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeJob:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbChangeJob.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeJob.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeSprite:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudChangeSprite.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeSprite.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeSex:
                    {
                        IsEdit = true;
                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 == 0)
                        {
                            frmEditor_Event.Instance.optChangeSexMale.Checked = true;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 == 1)
                        {
                            frmEditor_Event.Instance.optChangeSexFemale.Checked = true;
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeGender.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.SetPlayerKillable:
                    {
                        IsEdit = true;

                        frmEditor_Event.Instance.cmbSetPK.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;

                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangePK.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.WarpPlayer:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudWPMap.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.nudWPX.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.nudWPY.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                        frmEditor_Event.Instance.cmbWarpPlayerDir.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlayerWarp.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.SetMoveRoute:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.fraMoveRoute.Visible = true;
                        frmEditor_Event.Instance.fraMoveRoute.BringToFront();
                        frmEditor_Event.Instance.lstMoveRoute.Items.Clear();
                        ListOfEvents = new int[Data.MyMap.EventCount];
                        ListOfEvents[0] = EditorEvent;
                        var loopTo = Data.MyMap.EventCount;
                        for (i = 0; i < loopTo; i++)
                        {
                            if (i != EditorEvent)
                            {
                                frmEditor_Event.Instance.cmbEvent.Items.Add(Strings.Trim(Data.MyMap.Event[i].Name));
                                X = X + 1;
                                ListOfEvents[X] = i;
                                if (i == TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1)
                                    frmEditor_Event.Instance.cmbEvent.SelectedIndex = X;
                            }
                        }

                        IsMoveRouteCommand = true;
                        frmEditor_Event.Instance.chkIgnoreMove.Checked = Conversions.ToBoolean(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2);
                        frmEditor_Event.Instance.chkRepeatRoute.Checked = Conversions.ToBoolean(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3);
                        TempMoveRouteCount = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].MoveRouteCount;
                        TempMoveRoute = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].MoveRoute;
                        var loopTo1 = TempMoveRouteCount;
                        for (i = 0; i < loopTo1; i++)
                        {
                            switch (TempMoveRoute[i].Index)
                            {
                                case 1:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Move Up");
                                        break;
                                    }
                                case 2:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Move Down");
                                        break;
                                    }
                                case 3:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Move Left");
                                        break;
                                    }
                                case 4:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Move Right");
                                        break;
                                    }
                                case 5:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Move Randomly");
                                        break;
                                    }
                                case 6:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Move Towards Player");
                                        break;
                                    }
                                case 7:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Move Away From Player");
                                        break;
                                    }
                                case 8:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Step Forward");
                                        break;
                                    }
                                case 9:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Step Back");
                                        break;
                                    }
                                case 10:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Wait 100ms");
                                        break;
                                    }
                                case 11:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Wait 500ms");
                                        break;
                                    }
                                case 12:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Wait 1000ms");
                                        break;
                                    }
                                case 13:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Up");
                                        break;
                                    }
                                case 14:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Down");
                                        break;
                                    }
                                case 15:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Left");
                                        break;
                                    }
                                case 16:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Right");
                                        break;
                                    }
                                case 17:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn 90 Degrees To the Right");
                                        break;
                                    }
                                case 18:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn 90 Degrees To the Left");
                                        break;
                                    }
                                case 19:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Around 180 Degrees");
                                        break;
                                    }
                                case 20:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Randomly");
                                        break;
                                    }
                                case 21:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Towards Player");
                                        break;
                                    }
                                case 22:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Away from Player");
                                        break;
                                    }
                                case 23:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Speed 8x Slower");
                                        break;
                                    }
                                case 24:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Speed 4x Slower");
                                        break;
                                    }
                                case 25:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Speed 2x Slower");
                                        break;
                                    }
                                case 26:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Speed to Normal");
                                        break;
                                    }
                                case 27:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Speed 2x Faster");
                                        break;
                                    }
                                case 28:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Speed 4x Faster");
                                        break;
                                    }
                                case 29:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Frequency Lowest");
                                        break;
                                    }
                                case 30:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Frequency Lower");
                                        break;
                                    }
                                case 31:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Frequency Normal");
                                        break;
                                    }
                                case 32:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Frequency Higher");
                                        break;
                                    }
                                case 33:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Frequency Highest");
                                        break;
                                    }
                                case 34:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn On Walking Animation");
                                        break;
                                    }
                                case 35:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Off Walking Animation");
                                        break;
                                    }
                                case 36:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn On Fixed Direction");
                                        break;
                                    }
                                case 37:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Off Fixed Direction");
                                        break;
                                    }
                                case 38:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn On Walk Through");
                                        break;
                                    }
                                case 39:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Turn Off Walk Through");
                                        break;
                                    }
                                case 40:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Position Below Characters");
                                        break;
                                    }
                                case 41:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Position Same as Characters");
                                        break;
                                    }
                                case 42:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Position Above Characters");
                                        break;
                                    }
                                case 43:
                                    {
                                        frmEditor_Event.Instance.lstMoveRoute.Items.Add("Set Graphic");
                                        break;
                                    }
                            }
                        }
                        frmEditor_Event.Instance.fraMoveRoute.Visible = true;
                        frmEditor_Event.Instance.fraDialogue.Visible = false;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.PlayAnimation:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.lblPlayAnimX.Visible = false;
                        frmEditor_Event.Instance.lblPlayAnimY.Visible = false;
                        frmEditor_Event.Instance.nudPlayAnimTileX.Visible = false;
                        frmEditor_Event.Instance.nudPlayAnimTileY.Visible = false;
                        frmEditor_Event.Instance.cmbPlayAnimEvent.Visible = false;
                        frmEditor_Event.Instance.cmbPlayAnim.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.cmbPlayAnimEvent.Items.Clear();
                        var loopTo2 = Data.MyMap.EventCount;
                        for (i = 0; i < loopTo2; i++)
                            frmEditor_Event.Instance.cmbPlayAnimEvent.Items.Add(i + 1 + ". " + Data.MyMap.Event[i].Name);
                        frmEditor_Event.Instance.cmbPlayAnimEvent.SelectedIndex = 0;
                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 0)
                        {
                            frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex = 0;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 1)
                        {
                            frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex = 1;
                            frmEditor_Event.Instance.cmbPlayAnimEvent.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 2)
                        {
                            frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex = 2;
                            frmEditor_Event.Instance.nudPlayAnimTileX.Maximum = Data.MyMap.MaxX;
                            frmEditor_Event.Instance.nudPlayAnimTileY.Maximum = Data.MyMap.MaxY;
                            frmEditor_Event.Instance.nudPlayAnimTileX.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                            frmEditor_Event.Instance.nudPlayAnimTileY.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4;
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlayAnimation.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }

                case (byte)Core.EventCommand.PlayBgm:
                    {
                        IsEdit = true;
                        var loopTo3 = Information.UBound(Sound.MusicCache);
                        for (i = 0; i < loopTo3; i++)
                        {
                            if ((Sound.MusicCache[i] ?? "") == (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 ?? ""))
                            {
                                frmEditor_Event.Instance.cmbPlayBGM.SelectedIndex = i;
                            }
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlayBGM.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.PlaySound:
                    {
                        IsEdit = true;
                        var loopTo4 = Information.UBound(Sound.SoundCache);
                        for (i = 0; i < loopTo4; i++)
                        {
                            if ((Sound.SoundCache[i] ?? "") == (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 ?? ""))
                            {
                                frmEditor_Event.Instance.cmbPlaySound.SelectedIndex = i;
                            }
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlaySound.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.OpenShop:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbOpenShop.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraOpenShop.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.SetAccessLevel:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSetAccess.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetAccess.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.GiveExperience:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudGiveExp.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraGiveExp.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ShowChatBubble:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtChatbubbleText.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.cmbChatBubbleTargetType.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        if (frmEditor_Event.Instance.cmbChatBubbleTarget.Items.Count > -1)
                            frmEditor_Event.Instance.cmbChatBubbleTarget.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;

                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraShowChatBubble.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.Label:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtLabelName.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraCreateLabel.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.GoToLabel:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtGoToLabel.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraGoToLabel.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.SpawnNpc:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSpawnNpc.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSpawnNpc.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.SetFog:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudFogData0.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.nudFogData1.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.nudFogData2.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetFog.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.SetWeather:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.CmbWeather.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.nudWeatherIntensity.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetWeather.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.SetScreenTint:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudMapTintData0.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.nudMapTintData1.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.nudMapTintData2.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                        frmEditor_Event.Instance.nudMapTintData3.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraMapTint.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.Wait:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudWaitAmount.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetWait.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.EventCommand.ShowPicture:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudShowPicture.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;

                        frmEditor_Event.Instance.cmbPicLoc.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;

                        frmEditor_Event.Instance.nudPicOffsetX.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                        frmEditor_Event.Instance.nudPicOffsetY.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraShowPic.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        Map.DrawPicture();
                        break;
                    }
                case (byte)Core.EventCommand.WaitMovementCompletion:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraMoveRouteWait.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        frmEditor_Event.Instance.cmbMoveWait.Items.Clear();
                        ListOfEvents = new int[Data.MyMap.EventCount];
                        ListOfEvents[0] = EditorEvent;
                        frmEditor_Event.Instance.cmbMoveWait.Items.Add("This Event");
                        frmEditor_Event.Instance.cmbMoveWait.SelectedIndex = 0;
                        var loopTo5 = Data.MyMap.EventCount;
                        for (i = 0; i < loopTo5; i++)
                        {
                            if (i != EditorEvent)
                            {
                                frmEditor_Event.Instance.cmbMoveWait.Items.Add(Strings.Trim(Data.MyMap.Event[i].Name));
                                X = X + 1;
                                ListOfEvents[X] = i;
                                if (i == TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1)
                                    frmEditor_Event.Instance.cmbMoveWait.SelectedIndex = X;
                            }
                        }

                        break;
                    }
            }

        }

        public static void DeleteEventCommand()
        {
            int i;
            int curlist;
            int curslot;
            int p;
            Core.Type.CommandList oldCommandList;

            i = frmEditor_Event.Instance.lstCommands.SelectedIndex;
            if (i == -1)
                return;

            if (i > Information.UBound(EventList))
                return;

            curlist = EventList[i].CommandList;
            curslot = EventList[i].CommandNum;

            if (curlist > TmpEvent.Pages[CurPageNum].CommandListCount)
                return;

            if (TmpEvent.Pages[CurPageNum].CommandList == null)
                return;

            if (curslot >= TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                return;

            if (TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount != i + 1)
            {
                TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount--;
                p = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
                oldCommandList = TmpEvent.Pages[CurPageNum].CommandList[curlist];

                if (p <= 0)
                {
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands = new Core.Type.EventCommand[1];
                }
                else
                {
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands = new Core.Type.EventCommand[p];
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].ParentList = oldCommandList.ParentList;
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount = p;

                    // Move all commands down by 1  
                    for (i = frmEditor_Event.Instance.lstCommands.SelectedIndex + 1; i <= p; i++)
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i - 1] = oldCommandList.Commands[i];
                    }
                }
            }
            else
            {
                // If we are deleting the last command in the list, set only the last command  
                TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount--;
                Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands, TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount);
            }

            EventListCommands();
        }

        public static void ClearEventCommands()
        {
            TmpEvent.Pages[CurPageNum].CommandList = new Core.Type.CommandList[1];
            TmpEvent.Pages[CurPageNum].CommandListCount = 0;
            EventListCommands();
        }

        public static void EditCommand()
        {
            int i;
            int curlist;
            int curslot;

            i = frmEditor_Event.Instance.lstCommands.SelectedIndex;
            if (i == -1)
                return;

            if (i > Information.UBound(EventList))
                return;

            curlist = EventList[i].CommandList;
            curslot = EventList[i].CommandNum;

            if (curlist > TmpEvent.Pages[CurPageNum].CommandListCount)
                return;

            if (curslot > TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                return;

            switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index)
            {
                case (byte)Core.EventCommand.AddText:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtAddText_Text.Text;
                        // tmpEvent.Pages(curPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.Instance.scrlAddText_Color.Value
                        if (frmEditor_Event.Instance.optAddText_Player.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.optAddText_Map.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }
                        else if (frmEditor_Event.Instance.optAddText_Global.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 2;
                        }

                        break;
                    }
                case (byte)Core.EventCommand.ConditionalBranch:
                    {
                        if (frmEditor_Event.Instance.optCondition0.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 0;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_PlayerVarIndex.SelectedIndex;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondition_PlayerVarCompare.SelectedIndex;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data3 = (int)Math.Round(frmEditor_Event.Instance.nudCondition_PlayerVarCondition.Value);
                        }
                        else if (frmEditor_Event.Instance.optCondition1.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 1;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_PlayerSwitch.SelectedIndex;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondtion_PlayerSwitchCondition.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.optCondition2.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 2;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_HasItem.SelectedIndex;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = (int)Math.Round(frmEditor_Event.Instance.nudCondition_HasItem.Value);
                        }
                        else if (frmEditor_Event.Instance.optCondition3.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 3;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_JobIs.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.optCondition4.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 4;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_LearntSkill.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.optCondition5.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 5;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = (int)Math.Round(frmEditor_Event.Instance.nudCondition_LevelAmount.Value);
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondition_LevelCompare.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.optCondition6.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 6;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_SelfSwitch.SelectedIndex;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2 = frmEditor_Event.Instance.cmbCondition_SelfSwitchCondition.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.optCondition8.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 8;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_Gender.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.optCondition9.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Condition = 9;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 = frmEditor_Event.Instance.cmbCondition_Time.SelectedIndex;
                        }

                        break;
                    }
                case (byte)Core.EventCommand.ShowText:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtShowText.Text;
                        break;
                    }
                case (byte)Core.EventCommand.ShowChoices:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChoicePrompt.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text2 = frmEditor_Event.Instance.txtChoices1.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text3 = frmEditor_Event.Instance.txtChoices2.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text4 = frmEditor_Event.Instance.txtChoices3.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text5 = frmEditor_Event.Instance.txtChoices4.Text;
                        break;
                    }
                case (byte)Core.EventCommand.ModifyVariable:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbVariable.SelectedIndex;
                        if (frmEditor_Event.Instance.optVariableAction0.Checked == true)
                            i = 0;
                        if (frmEditor_Event.Instance.optVariableAction1.Checked == true)
                            i = 1;
                        if (frmEditor_Event.Instance.optVariableAction2.Checked == true)
                            i = 2;
                        if (frmEditor_Event.Instance.optVariableAction3.Checked == true)
                            i = 3;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = i;
                        if (i == 0)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData0.Value);
                        }
                        else if (i == 1)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData1.Value);
                        }
                        else if (i == 2)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData2.Value);
                        }
                        else if (i == 3)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData3.Value);
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudVariableData4.Value);
                        }

                        break;
                    }
                case (byte)Core.EventCommand.ModifySwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPlayerSwitchSet.SelectedIndex;
                        break;
                    }
                case (byte)Core.EventCommand.ModifySelfSwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetSelfSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbSetSelfSwitchTo.SelectedIndex;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeItems:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeItemIndex.SelectedIndex;
                        if (frmEditor_Event.Instance.optChangeItemSet.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.optChangeItemAdd.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }
                        else if (frmEditor_Event.Instance.optChangeItemRemove.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 2;
                        }
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudChangeItemsAmount.Value);
                        break;
                    }
                case (byte)Core.EventCommand.ChangeLevel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeLevel.Value);
                        break;
                    }
                case (byte)Core.EventCommand.ChangeSkills:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeSkills.SelectedIndex;
                        if (frmEditor_Event.Instance.optChangeSkillsAdd.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.optChangeSkillsRemove.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }

                        break;
                    }
                case (byte)Core.EventCommand.ChangeJob:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeJob.SelectedIndex;
                        break;
                    }
                case (byte)Core.EventCommand.ChangeSprite:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeSprite.Value);
                        break;
                    }
                case (byte)Core.EventCommand.ChangeSex:
                    {
                        if (frmEditor_Event.Instance.optChangeSexMale.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = 0;
                        }
                        else if (frmEditor_Event.Instance.optChangeSexFemale.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = 1;
                        }

                        break;
                    }
                case (byte)Core.EventCommand.SetPlayerKillable:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetPK.SelectedIndex;
                        break;
                    }

                case (byte)Core.EventCommand.WarpPlayer:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWPMap.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWPX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudWPY.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = frmEditor_Event.Instance.cmbWarpPlayerDir.SelectedIndex;
                        break;
                    }
                case (byte)Core.EventCommand.SetMoveRoute:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = ListOfEvents[frmEditor_Event.Instance.cmbEvent.SelectedIndex];
                        if (frmEditor_Event.Instance.chkIgnoreMove.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                        }
                        else
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }

                        if (frmEditor_Event.Instance.chkRepeatRoute.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = 1;
                        }
                        else
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = 0;
                        }
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].MoveRouteCount = TempMoveRouteCount;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].MoveRoute = TempMoveRoute;
                        break;
                    }
                case (byte)Core.EventCommand.PlayAnimation:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbPlayAnim.SelectedIndex;
                        if (frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex == 0)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 0;
                        }
                        else if (frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex == 1)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 1;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = frmEditor_Event.Instance.cmbPlayAnimEvent.SelectedIndex;
                        }
                        else if (frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex == 2)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = 2;
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudPlayAnimTileX.Value);
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudPlayAnimTileY.Value);
                        }

                        break;
                    }
                case (byte)Core.EventCommand.PlayBgm:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.MusicCache[frmEditor_Event.Instance.cmbPlayBGM.SelectedIndex];
                        break;
                    }
                case (byte)Core.EventCommand.PlaySound:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.SoundCache[frmEditor_Event.Instance.cmbPlaySound.SelectedIndex];
                        break;
                    }
                case (byte)Core.EventCommand.OpenShop:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbOpenShop.SelectedIndex;
                        break;
                    }
                case (byte)Core.EventCommand.SetAccessLevel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetAccess.SelectedIndex + 1;
                        break;
                    }
                case (byte)Core.EventCommand.GiveExperience:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudGiveExp.Value);
                        break;
                    }
                case (byte)Core.EventCommand.ShowChatBubble:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChatbubbleText.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChatBubbleTargetType.SelectedIndex + 1;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbChatBubbleTarget.SelectedIndex;
                        break;
                    }
                case (byte)Core.EventCommand.Label:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtLabelName.Text;
                        break;
                    }
                case (byte)Core.EventCommand.GoToLabel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtGoToLabel.Text;
                        break;
                    }
                case (byte)Core.EventCommand.SpawnNpc:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSpawnNpc.SelectedIndex;
                        break;
                    }
                case (byte)Core.EventCommand.SetFog:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudFogData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudFogData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudFogData2.Value);
                        break;
                    }
                case (byte)Core.EventCommand.SetWeather:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.CmbWeather.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWeatherIntensity.Value);
                        break;
                    }
                case (byte)Core.EventCommand.SetScreenTint:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData2.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData3.Value);
                        break;
                    }
                case (byte)Core.EventCommand.Wait:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWaitAmount.Value);
                        break;
                    }
                case (byte)Core.EventCommand.ShowPicture:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudShowPicture.Value);

                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPicLoc.SelectedIndex;

                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetY.Value);
                        break;
                    }
                case (byte)Core.EventCommand.WaitMovementCompletion:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = ListOfEvents[frmEditor_Event.Instance.cmbMoveWait.SelectedIndex];
                        break;
                    }
            }
            EventListCommands();

        }

        #endregion

        #region Incoming Packets

        public static void Packet_SpawnEvent(ref byte[] data)
        {
            int id;
            var buffer = new ByteStream(data);

            id = buffer.ReadInt32();

            GameState.CurrentEvents = id + 1;
            Array.Resize(ref Data.MapEvents, GameState.CurrentEvents);

            ref var withBlock = ref Data.MapEvents[id];
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
                ref var withBlock = ref Data.MapEvents[id];
                withBlock.X = x;
                withBlock.Y = y;
                withBlock.Dir = dir;
                withBlock.Moving = 1;
                withBlock.ShowDir = showDir;
                withBlock.MovementSpeed = movementSpeed;
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
                ref var withBlock = ref Data.MapEvents[i];
                withBlock.Dir = dir;
                withBlock.ShowDir = dir;
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

            Data.MyMap.EventCount = buffer.ReadInt32();

            if (Data.MyMap.EventCount > 0)
            {
                Data.MyMap.Event = new Core.Type.Event[Data.MyMap.EventCount];
                var loopTo = Data.MyMap.EventCount;
                for (i = 0; i < loopTo; i++)
                {
                    {
                        ref var withBlock = ref Data.MyMap.Event[i];
                        withBlock.Name = buffer.ReadString();
                        withBlock.Globals = buffer.ReadByte();
                        withBlock.X = buffer.ReadInt32();
                        withBlock.Y = buffer.ReadInt32();
                        withBlock.PageCount = buffer.ReadInt32();
                    }

                    if (Data.MyMap.Event[i].PageCount > 0)
                    {
                        Data.MyMap.Event[i].Pages = new Core.Type.EventPage[Data.MyMap.Event[i].PageCount];
                        var loopTo1 = Data.MyMap.Event[i].PageCount;
                        for (x = 0; x < loopTo1; x++)
                        {
                            {
                                ref var withBlock1 = ref Data.MyMap.Event[i].Pages[x];
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
                                    Data.MyMap.Event[i].Pages[x].MoveRoute = new Core.Type.MoveRoute[withBlock1.MoveRouteCount];
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

                            if (Data.MyMap.Event[i].Pages[x].CommandListCount > 0)
                            {
                                Data.MyMap.Event[i].Pages[x].CommandList = new Core.Type.CommandList[Data.MyMap.Event[i].Pages[x].CommandListCount];
                                var loopTo3 = Data.MyMap.Event[i].Pages[x].CommandListCount;
                                for (y = 0; y < loopTo3; y++)
                                {
                                    Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount = buffer.ReadInt32();
                                    Data.MyMap.Event[i].Pages[x].CommandList[y].ParentList = buffer.ReadInt32();
                                    if (Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        Data.MyMap.Event[i].Pages[x].CommandList[y].Commands = new Core.Type.EventCommand[Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount];
                                        var loopTo4 = Data.MyMap.Event[i].Pages[x].CommandList[y].CommandCount;
                                        for (z = 0; z < loopTo4; z++)
                                        {
                                            {
                                                ref var withBlock2 = ref Data.MyMap.Event[i].Pages[x].CommandList[y].Commands[z];
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
                                                    withBlock2.MoveRoute = new Core.Type.MoveRoute[withBlock2.MoveRouteCount];
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
            EventChat = true;
            ShowEventLbl = true;
            choices = buffer.ReadInt32();
            InEvent = true;
            for (i = 0; i < Core.Constant.MAX_EVENT_CHOICES; i++)
            {
                EventChoices[i] = "";
                EventChoiceVisible[i] = false;
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
                    EventChoiceVisible[i] = true;
                }
            }
            AnotherChat = buffer.ReadInt32();

            buffer.Dispose();

        }

        public static void Packet_EventStart(ref byte[] data)
        {
            InEvent = true;
        }

        public static void Packet_EventEnd(ref byte[] data)
        {
            InEvent = false;
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
                HoldPlayer = true;
            }
            else
            {
                HoldPlayer = false;
            }

            buffer.Dispose();

        }

        public static void Packet_PlayBGM(ref byte[] data)
        {
            string music;
            var buffer = new ByteStream(data);

            music = buffer.ReadString();
            Data.MyMap.Music = music;

            buffer.Dispose();
        }

        public static void Packet_FadeOutBGM(ref byte[] data)
        {
            Sound.CurrentMusic = "";
            Sound.FadeOutSwitch = true;
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
                        GameState.UseFade = true;
                        GameState.FadeType = 1;
                        GameState.FadeAmount = 0;
                        break;
                    }
                case GameState.EffectTypeFadeout:
                    {
                        GameState.UseFade = true;
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
                        Data.MyMap.MapTint = true;
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
            if (GameState.MyEditorType == (int)EditorType.Map)
                return;

            if (id >= Data.MyMap.EventCount)
                return;

            if (id >= Data.MapEvents.Length)
                return;

            if (Data.MapEvents[id].Moving == 1)
            {
                // Check if completed walking over to the next tile
                if (Data.MapEvents[id].Moving > 0)
                {
                    if (Data.MapEvents[id].Dir == (int)Direction.Right | Data.MapEvents[id].Dir == (int)Direction.Down)
                    {
                        switch (Data.MapEvents[(int)id].Dir)
                        {
                            case (int)Direction.Up:
                                {
                                    Core.Data.MapEvents[(int)id].Y = (byte)(Core.Data.MapEvents[(int)id].Y - 1);

                                    break;
                                }
                            case (int)Direction.Down:
                                {
                                    Core.Data.MapEvents[(int)id].Y = (byte)(Core.Data.MapEvents[(int)id].Y + 1);
                                    break;
                                }
                            case (int)Direction.Left:
                                {
                                    Core.Data.MapEvents[(int)id].X = (byte)(Core.Data.MapEvents[(int)id].X - 1);
                                    break;
                                }
                            case (int)Direction.Right:
                                {
                                    Core.Data.MyMapNpc[(int)id].X = (byte)(Core.Data.MyMapNpc[(int)id].X + 1);
                                    break;
                                }
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
                    EventChoiceVisible[i] = false;
                EventText = "";
                EventChatType = 1;
                EventChatTimer = General.GetTickCount() + 100;
            }
            else if (AnotherChat == 2)
            {
                for (i = 0; i < Core.Constant.MAX_EVENT_CHOICES; i++)
                    EventChoiceVisible[i] = false;
                EventText = "";
                EventChatType = 1;
                EventChatTimer = General.GetTickCount() + 100;
            }
            else
            {
                EventChat = false;
            }
        }

        #endregion

    }
}