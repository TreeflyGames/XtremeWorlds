using System;
using Core;
using Core.Common;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    static class Event
    {

        #region Globals

        // Temp event storage
        internal static Core.Type.EventStruct TmpEvent;

        internal static bool IsEdit;

        internal static int CurPageNum;
        internal static int CurCommand;
        internal static int GraphicSelX;
        internal static int GraphicSelY;
        internal static int GraphicSelX2;
        internal static int GraphicSelY2;

        internal static int EventTileX;
        internal static int EventTileY;

        internal static int EditorEvent;

        internal static int GraphicSelType;
        internal static int TempMoveRouteCount;
        internal static Core.Type.MoveRouteStruct[] TempMoveRoute;
        internal static bool IsMoveRouteCommand;
        internal static int[] ListOfEvents;

        internal static int EventReplyId;
        internal static int EventReplyPage;
        internal static int EventChatFace;

        internal static int RenameType;
        internal static int RenameIndex;
        internal static int EventChatTimer;

        internal static bool EventChat;
        internal static string EventText;
        internal static bool ShowEventLbl;
        internal static string[] EventChoices = new string[5];
        internal static bool[] EventChoiceVisible = new bool[5];
        internal static int EventChatType;
        internal static int AnotherChat;

        // constants
        internal static string[] Switches = new string[101];
        internal static string[] Variables = new string[101];

        internal static bool EventCopy;
        internal static bool EventPaste;
        internal static Core.Type.EventListStruct[] EventList;
        internal static Core.Type.EventStruct CopyEvent;
        internal static Core.Type.EventPageStruct CopyEventPage;

        internal static bool InEvent;
        internal static bool HoldPlayer;
        internal static bool InitEventEditorForm;

        internal static Core.Type.PictureStruct Picture;

        #endregion

        #region EventEditor
        public static void CopyEvent_Map(int X, int Y)
        {
            int count;
            int i;

            count = Core.Type.MyMap.EventCount;
            if (count == 0)
                return;

            var loopTo = count;
            for (i = 0; i <= loopTo; i++)
            {
                if (Core.Type.MyMap.Event[i].X == X & Core.Type.MyMap.Event[i].Y == Y)
                {
                    CopyEvent = Core.Type.MyMap.Event[i];
                    return;
                }
            }

        }

        public static void PasteEvent_Map(int X, int Y)
        {
            int count;
            int i;
            var EventNum = default(int);

            count = Core.Type.MyMap.EventCount;

            if (count > 0)
            {
                var loopTo = count;
                for (i = 0; i <= loopTo; i++)
                {
                    if (Core.Type.MyMap.Event[i].X == X & Core.Type.MyMap.Event[i].Y == Y)
                    {
                        EventNum = i;
                    }
                }
            }

            // couldn't find one - create one
            if (EventNum == 0)
            {
                AddEvent(X, Y, true);
                EventNum = count;
            }

            // copy it
            Core.Type.MyMap.Event[EventNum] = CopyEvent;

            // set position
            Core.Type.MyMap.Event[EventNum].X = X;
            Core.Type.MyMap.Event[EventNum].Y = Y;
        }

        public static void DeleteEvent(int X, int Y)
        {
            int i;
            int lowIndex = -1;
            bool shifted = false;

            if (GameState.MyEditorType != (int)Core.Enum.EditorType.Map)
                return;

            // First pass: find all events to delete and shift others down
            var loopTo = Core.Type.MyMap.EventCount;
            for (i = 0; i <= loopTo; i++)
            {
                if (Core.Type.MyMap.Event[i].X == X & Core.Type.MyMap.Event[i].Y == Y)
                {
                    // Clear the event
                    ClearEvent(i);
                    lowIndex = i;
                    shifted = true;
                }
                else if (shifted)
                {
                    // Shift this event down to fill the gap
                    Core.Type.MyMap.Event[lowIndex] = Core.Type.MyMap.Event[i];
                    lowIndex = lowIndex + 1;
                }
            }

            // Adjust the event count if anything was deleted
            if (lowIndex != -1)
            {
                // Set the new count
                Core.Type.MyMap.EventCount = lowIndex - 1;
                Array.Resize(ref Core.Type.MapEvents, Core.Type.MyMap.EventCount);
                Array.Resize(ref Core.Type.MyMap.Event, Core.Type.MyMap.EventCount);
                TmpEvent = default;
            }
        }


        public static void AddEvent(int X, int Y, bool cancelLoad = false)
        {
            int count;
            int pageCount;
            int i;

            count = Core.Type.MyMap.EventCount;

            // make sure there's not already an event
            if (count > 0)
            {
                var loopTo = count;
                for (i = 0; i <= loopTo; i++)
                {
                    if (Core.Type.MyMap.Event[i].X == X & Core.Type.MyMap.Event[i].Y == Y)
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
                count = count + 1;
            }

            Core.Type.MyMap.EventCount = count;
            Array.Resize(ref Core.Type.MyMap.Event, count + 1);
            // set the new event
            Core.Type.MyMap.Event[count].X = X;
            Core.Type.MyMap.Event[count].Y = Y;
            // give it a new page
            pageCount = Core.Type.MyMap.Event[count].PageCount;
            Core.Type.MyMap.Event[count].PageCount = pageCount;
            Array.Resize(ref Core.Type.MyMap.Event[count].Pages, pageCount);
            // load the editor
            if (!cancelLoad)
                EventEditorInit(count);
        }

        public static void ClearEvent(int EventNum)
        {
            if (EventNum > Core.Type.MyMap.EventCount | EventNum > Information.UBound(Core.Type.MyMap.Event))
                return;

            {
                ref var withBlock = ref Core.Type.MyMap.Event[EventNum];
                withBlock.Name = "";
                withBlock.PageCount = 0;
                withBlock.Pages = new Core.Type.EventPageStruct[1];
                withBlock.Globals = 0;
                withBlock.X = 0;
                withBlock.Y = 0;
            }
        }

        public static void EventEditorInit(int EventNum)
        {
            EditorEvent = EventNum;
            TmpEvent = Core.Type.MyMap.Event[EventNum];
            InitEventEditorForm = true;
            if (TmpEvent.Pages[1].CommandListCount == 0)
            {
                Array.Resize(ref TmpEvent.Pages[1].CommandList, 1);
                TmpEvent.Pages[1].CommandListCount = 1;
                TmpEvent.Pages[1].CommandList[1].CommandCount = 1;
                Array.Resize(ref TmpEvent.Pages[1].CommandList[1].Commands, TmpEvent.Pages[1].CommandList[1].CommandCount);
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
            Core.Type.MyMap.Event[EditorEvent] = TmpEvent;
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
                curlist = 1;
                X = 0;
                Array.Resize(ref EventList, X + 1);
            newlist:
                ;

                var loopTo = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
                for (i = 0; i <= loopTo; i++)
                {
                    if (listleftoff[curlist] > 0)
                    {
                        if ((TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[listleftoff[curlist]].Index == (int)Core.Enum.EventType.Condition | TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[listleftoff[curlist]].Index == (int)Core.Enum.EventType.ShowChoices) & conditionalstage[curlist] != 0)
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
                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Index == (int)Core.Enum.EventType.Condition)
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
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] == " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 1:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] >= " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] <= " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] > " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] < " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] != " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data3);
                                                                break;
                                                            }
                                                    }

                                                    break;
                                                }
                                            case 1:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Switch [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Switches[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] == " + "True");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Switch [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1 + ". " + Switches[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1] + "] == " + "False");
                                                    }

                                                    break;
                                                }
                                            case 2:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Has Item [" + Core.Type.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1].Name + "] x" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data2);
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Job Is [" + Strings.Trim(Core.Type.Job[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1].Name) + "]");
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player Knows Skill [" + Strings.Trim(Core.Type.Skill[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].ConditionalBranch.Data1].Name) + "]");
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
                                                        case (int)Core.Enum.SexType.Male:
                                                            {
                                                                frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Conditional Branch: Player's Gender is Male");
                                                                break;
                                                            }
                                                        case (int)Core.Enum.SexType.Female:
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
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Index == (int)Core.Enum.EventType.ShowChoices)
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
                                            EventList[X].CommandList = curlist;
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
                                case (byte)Core.Enum.EventType.AddText:
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
                                case (byte)Core.Enum.EventType.ShowText:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Text - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20));
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.PlayerVar:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2)
                                        {
                                            case 0:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] + "] == " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                                    break;
                                                }
                                            case 1:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] + "] + " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] + "] - " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Variable [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + Variables[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] + "] Random Between " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " and " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4);
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.PlayerSwitch:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Switch [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + ". " + Switches[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] + "] == True");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Switch [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + ". " + Switches[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] + "] == False");
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.SelfSwitch:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)
                                        {
                                            case 0:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [A] to ON");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [A] to OFF");
                                                    }

                                                    break;
                                                }
                                            case 1:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [B] to ON");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [B] to OFF");
                                                    }

                                                    break;
                                                }
                                            case 2:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [C] to ON");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [C] to OFF");
                                                    }

                                                    break;
                                                }
                                            case 3:
                                                {
                                                    if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [D] to ON");
                                                    }
                                                    else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Self Switch [D] to OFF");
                                                    }

                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ExitProcess:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Exit Event Processing");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ChangeItems:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Item Amount of [" + Core.Type.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "] to " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3);
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Give Player " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " " + Core.Type.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "(s)");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 2)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Take " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " " + Core.Type.Item[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "(s) from Player.");
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.RestoreHP:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Restore Player HP");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.RestoreSP:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Restore Player MP");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.LevelUp:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Level Up Player");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ChangeLevel:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Level to " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1);
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ChangeSkills:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Teach Player Skill [" + Strings.Trim(Core.Type.Skill[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name) + "]");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Remove Player Skill [" + Strings.Trim(Core.Type.Skill[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name) + "]");
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ChangeJob:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Job to " + Strings.Trim(Core.Type.Job[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name));
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ChangeSprite:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Player Sprite to " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1);
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ChangeSex:
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
                                case (byte)Core.Enum.EventType.ChangePk:
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
                                case (byte)Core.Enum.EventType.WarpPlayer:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") while retaining direction.");
                                        }
                                        else
                                        {
                                            switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4 - 1)
                                            {
                                                case (int)Core.Enum.DirectionType.Up:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing upward.");
                                                        break;
                                                    }
                                                case (int)Core.Enum.DirectionType.Down:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing downward.");
                                                        break;
                                                    }
                                                case (int)Core.Enum.DirectionType.Left:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing left.");
                                                        break;
                                                    }
                                                case (int)Core.Enum.DirectionType.Right:
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Warp Player To Map: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + ") facing right.");
                                                        break;
                                                    }
                                            }
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.SetMoveRoute:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 <= Core.Type.MyMap.EventCount)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Move Route for Event #" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " [" + Core.Type.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]");
                                        }
                                        else
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Move Route for COULD NOT FIND EVENT!");
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.PlayAnimation:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Animation " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " [" + Core.Type.Animation[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]" + " on Player");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 1)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Animation " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " [" + Core.Type.Animation[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]" + " on Event #" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + " [" + Strings.Trim(Core.Type.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3].Name) + "]");
                                        }
                                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2 == 2)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Animation " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " [" + Core.Type.Animation[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]" + " on Tile(" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3 + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4 + ")");
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.CustomScript:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Execute Custom Script Case: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1);
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.PlayBgm:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play BGM [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1) + "]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.FadeoutBgm:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Fadeout BGM");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.PlaySound:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Play Sound [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1) + "]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.StopSound:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Stop Sound");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.OpenBank:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Open Bank");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.OpenShop:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Open Shop [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ". " + Core.Type.Shop[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name + "]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.SetAccess:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(Operators.ConcatenateObject(Operators.ConcatenateObject(indent + "@>" + "Set Player Access [", frmEditor_Event.Instance.cmbSetAccess.Items[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1]), "]"));
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.GiveExp:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Give Player " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " Experience.");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ShowChatBubble:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)
                                        {
                                            case (int)Core.Enum.TargetType.Player:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On Player");
                                                    break;
                                                }
                                            case (int)Core.Enum.TargetType.NPC:
                                                {
                                                    if (Core.Type.MyMap.NPC[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2] <= 0)
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On NPC [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + ". ]");
                                                    }
                                                    else
                                                    {
                                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On NPC [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + ". " + Core.Type.NPC[Core.Type.MyMap.NPC[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2]].Name + "]");
                                                    }

                                                    break;
                                                }
                                            case (int)Core.Enum.TargetType.Event:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Show Chat Bubble - " + Strings.Mid(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1, 1, 20) + "... - On Event [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + ". " + Core.Type.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2].Name + "]");
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.Label:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Label: [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1) + "]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.GotoLabel:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Jump to Label: [" + Strings.Trim(TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Text1) + "]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.SpawnNPC:
                                    {
                                        if (Core.Type.MyMap.NPC[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1] <= 0)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Spawn NPC: [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ". " + "]");
                                        }
                                        else
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Spawn NPC: [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + ". " + Core.Type.NPC[Core.Type.MyMap.NPC[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1]].Name + "]");
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.FadeIn:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Fade In");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.FadeOut:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Fade Out");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.FlashWhite:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Flash White");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.SetFog:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Fog [Fog: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + " Speed: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + " Opacity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3.ToString() + "]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.SetWeather:
                                    {
                                        switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1)
                                        {
                                            case (int)Core.Enum.Weather.None:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [None]");
                                                    break;
                                                }
                                            case (int)Core.Enum.Weather.Rain:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Rain - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                            case (int)Core.Enum.Weather.Snow:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Snow - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                            case (int)Core.Enum.Weather.Sandstorm:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Sand Storm - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                            case (int)Core.Enum.Weather.Storm:
                                                {
                                                    frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Weather [Storm - Intensity: " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "]");
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.SetTint:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Set Map Tint RGBA [" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data2.ToString() + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data3.ToString() + "," + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data4.ToString() + "]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.Wait:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Wait " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString() + " Ms");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ShowPicture:
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
                                case (byte)Core.Enum.EventType.HidePicture:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Hide Picture " + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1.ToString());
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.WaitMovement:
                                    {
                                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 <= Core.Type.MyMap.EventCount)
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Wait for Event #" + TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1 + " [" + Strings.Trim(Core.Type.MyMap.Event[TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i].Data1].Name) + "] to complete move route.");
                                        }
                                        else
                                        {
                                            frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Wait for COULD NOT FIND EVENT to complete move route.");
                                        }

                                        break;
                                    }
                                case (byte)Core.Enum.EventType.HoldPlayer:
                                    {
                                        frmEditor_Event.Instance.lstCommands.Items.Add(indent + "@>" + "Hold Player [Do not allow player to move.]");
                                        break;
                                    }
                                case (byte)Core.Enum.EventType.ReleasePlayer:
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
                                            EventList = new Core.Type.EventListStruct[1];
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
            for (i = 0; i <= loopTo1; i++)
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
            Core.Type.CommandListStruct oldCommandList;

            if (frmEditor_Event.Instance.lstCommands.SelectedIndex == frmEditor_Event.Instance.lstCommands.Items.Count)
            {
                curlist = 1;
            }
            else
            {
                curlist = EventList[frmEditor_Event.Instance.lstCommands.SelectedIndex].CommandList;
            }

            TmpEvent.Pages[CurPageNum].CommandListCount = TmpEvent.Pages[CurPageNum].CommandListCount;
            Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList, TmpEvent.Pages[CurPageNum].CommandListCount);
            TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
            p = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
            Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands, p + 1);

            if (frmEditor_Event.Instance.lstCommands.SelectedIndex == frmEditor_Event.Instance.lstCommands.Items.Count)
            {
                curslot = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount - 1;
            }
            else
            {
                oldCommandList = TmpEvent.Pages[CurPageNum].CommandList[curlist];
                TmpEvent.Pages[CurPageNum].CommandList[curlist].ParentList = oldCommandList.ParentList;

                var loopTo = p - 1;
                for (i = 0; i <= loopTo; i++)
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[i] = oldCommandList.Commands[i];

                i = EventList[frmEditor_Event.Instance.lstCommands.SelectedIndex].CommandNum;
                if (i <= TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                {

                    var loopTo1 = i;
                    for (X = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount; X <= loopTo1; X++)
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
                case (int)Core.Enum.EventType.AddText:
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
                case (int)Core.Enum.EventType.Condition:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandListCount = TmpEvent.Pages[CurPageNum].CommandListCount + 2;
                        Array.Resize(ref TmpEvent.Pages[CurPageNum].CommandList, TmpEvent.Pages[CurPageNum].CommandListCount + 1);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.CommandList = TmpEvent.Pages[CurPageNum].CommandListCount - 1;
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

                case (int)Core.Enum.EventType.ShowText:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        string tmptxt = "";
                        var loopTo2 = Information.UBound(frmEditor_Event.Instance.txtShowText.Lines);
                        for (i = 0; i <= loopTo2; i++)
                            tmptxt = tmptxt + frmEditor_Event.Instance.txtShowText.Lines[i];
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = tmptxt;
                        break;
                    }

                case (int)Core.Enum.EventType.ShowChoices:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChoicePrompt.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text2 = frmEditor_Event.Instance.txtChoices1.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text3 = frmEditor_Event.Instance.txtChoices2.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text4 = frmEditor_Event.Instance.txtChoices3.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text5 = frmEditor_Event.Instance.txtChoices4.Text;
                        TmpEvent.Pages[CurPageNum].CommandListCount = TmpEvent.Pages[CurPageNum].CommandListCount + 4;
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

                case (int)Core.Enum.EventType.PlayerVar:
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

                case (int)Core.Enum.EventType.PlayerSwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPlayerSwitchSet.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.SelfSwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetSelfSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbSetSelfSwitchTo.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.ExitProcess:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.ChangeItems:
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

                case (int)Core.Enum.EventType.RestoreHP:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.RestoreSP:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.LevelUp:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.ChangeLevel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeLevel.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.ChangeSkills:
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

                case (int)Core.Enum.EventType.ChangeJob:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeJob.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.ChangeSprite:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeSprite.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.ChangeSex:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        if (frmEditor_Event.Instance.optChangeSexMale.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Core.Enum.SexType.Male;
                        }
                        else if (frmEditor_Event.Instance.optChangeSexFemale.Checked == true)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Core.Enum.SexType.Female;
                        }

                        break;
                    }

                case (int)Core.Enum.EventType.ChangePk:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetPK.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.WarpPlayer:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWPMap.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWPX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudWPY.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = frmEditor_Event.Instance.cmbWarpPlayerDir.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.SetMoveRoute:
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

                case (int)Core.Enum.EventType.PlayAnimation:
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

                case (int)Core.Enum.EventType.CustomScript:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudCustomScript.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.PlayBgm:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.MusicCache[frmEditor_Event.Instance.cmbPlayBGM.SelectedIndex];
                        break;
                    }

                case (int)Core.Enum.EventType.FadeoutBgm:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.PlaySound:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.SoundCache[frmEditor_Event.Instance.cmbPlaySound.SelectedIndex];
                        break;
                    }

                case (int)Core.Enum.EventType.StopSound:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.OpenBank:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.OpenShop:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbOpenShop.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.SetAccess:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetAccess.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.GiveExp:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudGiveExp.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.ShowChatBubble:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChatbubbleText.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChatBubbleTargetType.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbChatBubbleTarget.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.Label:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtLabelName.Text;
                        break;
                    }

                case (int)Core.Enum.EventType.GotoLabel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtGotoLabel.Text;
                        break;
                    }

                case (int)Core.Enum.EventType.SpawnNPC:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSpawnNPC.SelectedIndex;
                        break;
                    }

                case (int)Core.Enum.EventType.FadeIn:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.FadeOut:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.FlashWhite:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.SetFog:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudFogData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudFogData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudFogData2.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.SetWeather:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.CmbWeather.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWeatherIntensity.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.SetTint:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData2.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData3.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.Wait:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWaitAmount.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.ShowPicture:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudShowPicture.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPicLoc.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetY.Value);
                        break;
                    }

                case (int)Core.Enum.EventType.HidePicture:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.WaitMovement:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = ListOfEvents[frmEditor_Event.Instance.cmbMoveWait.SelectedIndex];
                        break;
                    }

                case (int)Core.Enum.EventType.HoldPlayer:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index = (byte)Index;
                        break;
                    }

                case (int)Core.Enum.EventType.ReleasePlayer:
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

            i = frmEditor_Event.Instance.lstCommands.SelectedIndex;
            if (i == -1)
                return;
            if (i > Information.UBound(EventList))
                return;

            frmEditor_Event.Instance.fraConditionalBranch.Visible = false;
            frmEditor_Event.Instance.fraDialogue.BringToFront();

            curlist = EventList[i].CommandList;
            curslot = EventList[i].CommandNum;
            if (curlist == 0)
                return;
            if (curslot == 0)
                return;
            if (curlist > TmpEvent.Pages[CurPageNum].CommandListCount)
                return;
            if (curslot > TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                return;

            switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index)
            {
                case (byte)Core.Enum.EventType.AddText:
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
                case (byte)Core.Enum.EventType.Condition:
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
                                    frmEditor_Event.Instance.cmbCondition_PlayerVarIndex.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 - 1;
                                    frmEditor_Event.Instance.cmbCondition_PlayerVarCompare.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    frmEditor_Event.Instance.nudCondition_PlayerVarCondition.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data3;
                                    break;
                                }
                            case 1:
                                {
                                    frmEditor_Event.Instance.cmbCondition_PlayerSwitch.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondtion_PlayerSwitchCondition.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_PlayerSwitch.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 - 1;
                                    frmEditor_Event.Instance.cmbCondtion_PlayerSwitchCondition.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    break;
                                }
                            case 2:
                                {
                                    frmEditor_Event.Instance.cmbCondition_HasItem.Enabled = true;
                                    frmEditor_Event.Instance.nudCondition_HasItem.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_HasItem.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 - 1;
                                    frmEditor_Event.Instance.nudCondition_HasItem.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data2;
                                    break;
                                }
                            case 3:
                                {
                                    frmEditor_Event.Instance.cmbCondition_JobIs.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_JobIs.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 - 1;
                                    break;
                                }
                            case 4:
                                {
                                    frmEditor_Event.Instance.cmbCondition_LearntSkill.Enabled = true;
                                    frmEditor_Event.Instance.cmbCondition_LearntSkill.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].ConditionalBranch.Data1 - 1;
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
                case (byte)Core.Enum.EventType.ShowText:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtShowText.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraShowText.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.ShowChoices:
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
                case (byte)Core.Enum.EventType.PlayerVar:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbVariable.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
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
                case (byte)Core.Enum.EventType.PlayerSwitch:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSwitch.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        frmEditor_Event.Instance.cmbPlayerSwitchSet.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlayerSwitch.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.SelfSwitch:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSetSelfSwitch.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.cmbSetSelfSwitchTo.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetSelfSwitch.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeItems:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbChangeItemIndex.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
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
                case (byte)Core.Enum.EventType.ChangeLevel:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudChangeLevel.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeLevel.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeSkills:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbChangeSkills.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
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
                case (byte)Core.Enum.EventType.ChangeJob:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbChangeJob.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeJob.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeSprite:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudChangeSprite.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangeSprite.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeSex:
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
                case (byte)Core.Enum.EventType.ChangePk:
                    {
                        IsEdit = true;

                        frmEditor_Event.Instance.cmbSetPK.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;

                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraChangePK.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.WarpPlayer:
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
                case (byte)Core.Enum.EventType.SetMoveRoute:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.fraMoveRoute.Visible = true;
                        frmEditor_Event.Instance.fraMoveRoute.BringToFront();
                        frmEditor_Event.Instance.lstMoveRoute.Items.Clear();
                        ListOfEvents = new int[Core.Type.MyMap.EventCount];
                        ListOfEvents[0] = EditorEvent;
                        var loopTo = Core.Type.MyMap.EventCount;
                        for (i = 0; i <= loopTo; i++)
                        {
                            if (i != EditorEvent)
                            {
                                frmEditor_Event.Instance.cmbEvent.Items.Add(Strings.Trim(Core.Type.MyMap.Event[i].Name));
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
                        for (i = 0; i <= loopTo1; i++)
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
                case (byte)Core.Enum.EventType.PlayAnimation:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.lblPlayAnimX.Visible = false;
                        frmEditor_Event.Instance.lblPlayAnimY.Visible = false;
                        frmEditor_Event.Instance.nudPlayAnimTileX.Visible = false;
                        frmEditor_Event.Instance.nudPlayAnimTileY.Visible = false;
                        frmEditor_Event.Instance.cmbPlayAnimEvent.Visible = false;
                        frmEditor_Event.Instance.cmbPlayAnim.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        frmEditor_Event.Instance.cmbPlayAnimEvent.Items.Clear();
                        var loopTo2 = Core.Type.MyMap.EventCount;
                        for (i = 0; i <= loopTo2; i++)
                            frmEditor_Event.Instance.cmbPlayAnimEvent.Items.Add(i + ". " + Core.Type.MyMap.Event[i].Name);
                        frmEditor_Event.Instance.cmbPlayAnimEvent.SelectedIndex = 0;
                        if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 0)
                        {
                            frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex = 0;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 1)
                        {
                            frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex = 1;
                            frmEditor_Event.Instance.cmbPlayAnimEvent.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 - 1;
                        }
                        else if (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 == 2)
                        {
                            frmEditor_Event.Instance.cmbAnimTargetType.SelectedIndex = 2;
                            frmEditor_Event.Instance.nudPlayAnimTileX.Maximum = Core.Type.MyMap.MaxX;
                            frmEditor_Event.Instance.nudPlayAnimTileY.Maximum = Core.Type.MyMap.MaxY;
                            frmEditor_Event.Instance.nudPlayAnimTileX.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3;
                            frmEditor_Event.Instance.nudPlayAnimTileY.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4;
                        }
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraPlayAnimation.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.CustomScript:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudCustomScript.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraCustomScript.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.PlayBgm:
                    {
                        IsEdit = true;
                        var loopTo3 = Information.UBound(Sound.MusicCache);
                        for (i = 0; i <= loopTo3; i++)
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
                case (byte)Core.Enum.EventType.PlaySound:
                    {
                        IsEdit = true;
                        var loopTo4 = Information.UBound(Sound.SoundCache);
                        for (i = 0; i <= loopTo4; i++)
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
                case (byte)Core.Enum.EventType.OpenShop:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbOpenShop.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraOpenShop.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.SetAccess:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSetAccess.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetAccess.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.GiveExp:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudGiveExp.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraGiveExp.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.ShowChatBubble:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtChatbubbleText.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.cmbChatBubbleTargetType.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        frmEditor_Event.Instance.cmbChatBubbleTarget.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 - 1;

                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraShowChatBubble.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.Label:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtLabelName.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraCreateLabel.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.GotoLabel:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.txtGotoLabel.Text = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraGoToLabel.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.SpawnNPC:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.cmbSpawnNPC.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 - 1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSpawnNPC.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.SetFog:
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
                case (byte)Core.Enum.EventType.SetWeather:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.CmbWeather.SelectedIndex = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.nudWeatherIntensity.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetWeather.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.SetTint:
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
                case (byte)Core.Enum.EventType.Wait:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.nudWaitAmount.Value = TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraSetWait.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        break;
                    }
                case (byte)Core.Enum.EventType.ShowPicture:
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
                case (byte)Core.Enum.EventType.WaitMovement:
                    {
                        IsEdit = true;
                        frmEditor_Event.Instance.fraDialogue.Visible = true;
                        frmEditor_Event.Instance.fraMoveRouteWait.Visible = true;
                        frmEditor_Event.Instance.fraCommands.Visible = false;
                        frmEditor_Event.Instance.cmbMoveWait.Items.Clear();
                        ListOfEvents = new int[Core.Type.MyMap.EventCount];
                        ListOfEvents[0] = EditorEvent;
                        frmEditor_Event.Instance.cmbMoveWait.Items.Add("This Event");
                        frmEditor_Event.Instance.cmbMoveWait.SelectedIndex = 0;
                        var loopTo5 = Core.Type.MyMap.EventCount;
                        for (i = 0; i <= loopTo5; i++)
                        {
                            if (i != EditorEvent)
                            {
                                frmEditor_Event.Instance.cmbMoveWait.Items.Add(Strings.Trim(Core.Type.MyMap.Event[i].Name));
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
            int X;
            int curlist;
            int curslot;
            int p;
            Core.Type.CommandListStruct oldCommandList;

            i = frmEditor_Event.Instance.lstCommands.SelectedIndex;
            if (i == -1)
                return;
            if (i > Information.UBound(EventList))
                return;

            curlist = EventList[i].CommandList;
            curslot = EventList[i].CommandNum;

            if (curlist == 0)
                return;
            if (curslot == 0)
                return;
            if (curlist > TmpEvent.Pages[CurPageNum].CommandListCount)
                return;
            if (curslot > TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                return;

            if (curslot == TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
            {
                TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount - 1;
                p = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
                if (p <= 0)
                {
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands = new Core.Type.EventCommandStruct[1];
                }
                else
                {
                    oldCommandList = TmpEvent.Pages[CurPageNum].CommandList[curlist];
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands = new Core.Type.EventCommandStruct[p + 1];
                    X = 1;
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].ParentList = oldCommandList.ParentList;
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount = p;
                    var loopTo = p + 1;
                    for (i = 0; i <= loopTo; i++)
                    {
                        if (i != curslot)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[X] = oldCommandList.Commands[i];
                            X = X + 1;
                        }
                    }
                }
            }
            else
            {
                TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount - 1;
                p = TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount;
                oldCommandList = TmpEvent.Pages[CurPageNum].CommandList[curlist];
                X = 1;
                if (p <= 0)
                {
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands = new Core.Type.EventCommandStruct[1];
                }
                else
                {
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands = new Core.Type.EventCommandStruct[p + 1];
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].ParentList = oldCommandList.ParentList;
                    TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount = p;
                    var loopTo1 = p + 1;
                    for (i = 0; i <= loopTo1; i++)
                    {
                        if (i != curslot)
                        {
                            TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[X] = oldCommandList.Commands[i];
                            X = X + 1;
                        }
                    }
                }
            }
            EventListCommands();

        }

        public static void ClearEventCommands()
        {
            TmpEvent.Pages[CurPageNum].CommandList = new Core.Type.CommandListStruct[1];
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
            if (curlist == 0)
                return;
            if (curslot == 0)
                return;
            if (curlist > TmpEvent.Pages[CurPageNum].CommandListCount)
                return;
            if (curslot > TmpEvent.Pages[CurPageNum].CommandList[curlist].CommandCount)
                return;
            switch (TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Index)
            {
                case (byte)Core.Enum.EventType.AddText:
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
                case (byte)Core.Enum.EventType.Condition:
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
                case (byte)Core.Enum.EventType.ShowText:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtShowText.Text;
                        break;
                    }
                case (byte)Core.Enum.EventType.ShowChoices:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChoicePrompt.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text2 = frmEditor_Event.Instance.txtChoices1.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text3 = frmEditor_Event.Instance.txtChoices2.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text4 = frmEditor_Event.Instance.txtChoices3.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text5 = frmEditor_Event.Instance.txtChoices4.Text;
                        break;
                    }
                case (byte)Core.Enum.EventType.PlayerVar:
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
                case (byte)Core.Enum.EventType.PlayerSwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPlayerSwitchSet.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.SelfSwitch:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetSelfSwitch.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbSetSelfSwitchTo.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeItems:
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
                case (byte)Core.Enum.EventType.ChangeLevel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeLevel.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeSkills:
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
                case (byte)Core.Enum.EventType.ChangeJob:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChangeJob.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeSprite:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudChangeSprite.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.ChangeSex:
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
                case (byte)Core.Enum.EventType.ChangePk:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetPK.SelectedIndex;
                        break;
                    }

                case (byte)Core.Enum.EventType.WarpPlayer:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWPMap.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWPX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudWPY.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = frmEditor_Event.Instance.cmbWarpPlayerDir.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.SetMoveRoute:
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
                case (byte)Core.Enum.EventType.PlayAnimation:
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
                case (byte)Core.Enum.EventType.CustomScript:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudCustomScript.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.PlayBgm:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.MusicCache[frmEditor_Event.Instance.cmbPlayBGM.SelectedIndex];
                        break;
                    }
                case (byte)Core.Enum.EventType.PlaySound:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = Sound.SoundCache[frmEditor_Event.Instance.cmbPlaySound.SelectedIndex];
                        break;
                    }
                case (byte)Core.Enum.EventType.OpenShop:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbOpenShop.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.SetAccess:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSetAccess.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.GiveExp:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudGiveExp.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.ShowChatBubble:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtChatbubbleText.Text;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbChatBubbleTargetType.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbChatBubbleTarget.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.Label:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtLabelName.Text;
                        break;
                    }
                case (byte)Core.Enum.EventType.GotoLabel:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Text1 = frmEditor_Event.Instance.txtGotoLabel.Text;
                        break;
                    }
                case (byte)Core.Enum.EventType.SpawnNPC:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.cmbSpawnNPC.SelectedIndex;
                        break;
                    }
                case (byte)Core.Enum.EventType.SetFog:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudFogData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudFogData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudFogData2.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.SetWeather:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = frmEditor_Event.Instance.CmbWeather.SelectedIndex;
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudWeatherIntensity.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.SetTint:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData0.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData1.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData2.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudMapTintData3.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.Wait:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudWaitAmount.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.ShowPicture:
                    {
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data1 = (int)Math.Round(frmEditor_Event.Instance.nudShowPicture.Value);

                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data2 = frmEditor_Event.Instance.cmbPicLoc.SelectedIndex;

                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data3 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetX.Value);
                        TmpEvent.Pages[CurPageNum].CommandList[curlist].Commands[curslot].Data4 = (int)Math.Round(frmEditor_Event.Instance.nudPicOffsetY.Value);
                        break;
                    }
                case (byte)Core.Enum.EventType.WaitMovement:
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

            if (id > GameState.CurrentEvents)
            {
                GameState.CurrentEvents = id;
                Array.Resize(ref Core.Type.MapEvents, GameState.CurrentEvents + 1);
            }

            {
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
                withBlock.QuestNum = buffer.ReadInt32();
            }

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

            for (i = 0; i <= Constant.MAX_SWITCHES - 1; i++)
                Switches[i] = buffer.ReadString();
            for (i = 0; i <= Constant.NAX_VARIABLES - 1; i++)
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
                var loopTo = Core.Type.MyMap.EventCount - 1;
                for (i = 0; i <= loopTo; i++)
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
                        var loopTo1 = Core.Type.MyMap.Event[i].PageCount - 1;
                        for (x = 0; x <= loopTo1; x++)
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
                                    var loopTo2 = withBlock1.MoveRouteCount - 1;
                                    for (y = 0; y <= loopTo2; y++)
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
                                var loopTo3 = Core.Type.MyMap.Event[i].Pages[x].CommandListCount - 1;
                                for (y = 0; y <= loopTo3; y++)
                                {
                                    Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount = buffer.ReadInt32();
                                    Core.Type.MyMap.Event[i].Pages[x].CommandList[y].ParentList = buffer.ReadInt32();
                                    if (Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount > 0)
                                    {
                                        Core.Type.MyMap.Event[i].Pages[x].CommandList[y].Commands = new Core.Type.EventCommandStruct[Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount];
                                        var loopTo4 = Core.Type.MyMap.Event[i].Pages[x].CommandList[y].CommandCount - 1;
                                        for (z = 0; z <= loopTo4; z++)
                                        {
                                            {
                                                ref var withBlock2 = ref Core.Type.MyMap.Event[i].Pages[x].CommandList[y].Commands[z];
                                                withBlock2.Index = buffer.ReadByte();
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
                                                    var loopTo5 = withBlock2.MoveRouteCount - 1;
                                                    for (w = 0; w <= loopTo5; w++)
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
            for (i = 0; i <= 4; i++)
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
                for (i = 0; i <= loopTo; i++)
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
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendSwitchesAndVariables()
        {
            int i;
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSwitchesAndVariables);

            for (i = 0; i <= Constant.MAX_SWITCHES - 1; i++)
                buffer.WriteString(Switches[i]);
            for (i = 0; i <= Constant.NAX_VARIABLES - 1; i++)
                buffer.WriteString(Variables[i]);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        #endregion

        #region Misc

        public static void ProcessEventMovement(int id)
        {
            if (GameState.MyEditorType == (int)Core.Enum.EditorType.Map)
                return;
            if (id > Core.Type.MyMap.EventCount)
                return;
            if (id > Core.Type.MapEvents.Length)
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

        internal static object GetColorString(int color)
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
                for (i = 0; i <= 4; i++)
                    EventChoiceVisible[i] = Conversions.ToBoolean(0);
                EventText = "";
                EventChatType = 1;
                EventChatTimer = General.GetTickCount() + 100;
            }
            else if (AnotherChat == 2)
            {
                for (i = 0; i <= 4; i++)
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