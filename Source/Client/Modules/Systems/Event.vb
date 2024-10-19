Imports Core
Imports Mirage.Sharp.Asfw

Module Events

#Region "Globals"

    ' Temp event storage
    Friend TmpEvent As EventStruct

    Friend IsEdit As Boolean

    Friend CurPageNum As Integer
    Friend CurCommand As Integer
    Friend GraphicSelX As Integer
    Friend GraphicSelY As Integer
    Friend GraphicSelX2 As Integer
    Friend GraphicSelY2 As Integer

    Friend EventTileX As Integer
    Friend EventTileY As Integer

    Friend EditorEvent As Integer

    Friend GraphicSelType As Integer
    Friend TempMoveRouteCount As Integer
    Friend TempMoveRoute() As MoveRouteStruct
    Friend IsMoveRouteCommand As Boolean
    Friend ListOfEvents() As Integer

    Friend EventReplyId As Integer
    Friend EventReplyPage As Integer
    Friend EventChatFace As Integer

    Friend RenameType As Integer
    Friend RenameIndex As Integer
    Friend EventChatTimer As Integer

    Friend EventChat As Boolean
    Friend EventText As String
    Friend ShowEventLbl As Boolean
    Friend EventChoices(4) As String
    Friend EventChoiceVisible(4) As Boolean
    Friend EventChatType As Integer
    Friend AnotherChat As Integer

    'constants
    Friend Switches(MAX_SWITCHES) As String
    Friend Variables(NAX_VARIABLES) As String

    Friend EventCopy As Boolean
    Friend EventPaste As Boolean
    Friend EventList() As EventListStruct
    Friend CopyEvent As EventStruct
    Friend CopyEventPage As EventPageStruct

    Friend InEvent As Boolean
    Friend HoldPlayer As Boolean
    Friend InitEventEditorForm As Boolean

    Friend Picture As PictureStruct

#End Region

#Region "EventEditor"
    Sub CopyEvent_Map(ByVal X As Integer, ByVal Y As Integer)
        Dim count As Integer, i As Integer

        count = MyMap.EventCount
        If count = 0 Then Exit Sub

        For i = 0 To count
            If MyMap.Event(i).X = X And MyMap.Event(i).Y = Y Then
                CopyEvent = MyMap.Event(i)
                Exit Sub
            End If
        Next

    End Sub

    Sub PasteEvent_Map(ByVal X As Integer, ByVal Y As Integer)
        Dim count As Integer, i As Integer, EventNum As Integer

        count = MyMap.EventCount

        If count > 0 Then
            For i = 0 To count
                If MyMap.Event(i).X = X And MyMap.Event(i).Y = Y Then
                    EventNum = i
                End If
            Next
        End If

        ' couldn't find one - create one
        If EventNum = 0 Then
            AddEvent(X, Y, True)
            EventNum = count + 1
        End If

        ' copy it
        MyMap.Event(EventNum) = CopyEvent

        ' set position
        MyMap.Event(EventNum).X = X
        MyMap.Event(EventNum).Y = Y
    End Sub

    Sub DeleteEvent(ByVal X As Integer, ByVal Y As Integer)
        Dim i As Integer
        Dim lowIndex As Integer = -1
        Dim shifted As Boolean = False

        If MyEditorType <> EditorType.Map Then Exit Sub
        If frmEditor_Event.Visible = True Then Exit Sub

        ' First pass: find all events to delete and shift others down
        For i = 1 To MyMap.EventCount
            If MyMap.Event(i).X = X And MyMap.Event(i).Y = Y Then
                ' Clear the event
                ClearEvent(i)
                lowIndex = i
                shifted = True
            ElseIf shifted Then
                ' Shift this event down to fill the gap
                MyMap.Event(lowIndex) = MyMap.Event(i)
                lowIndex = lowIndex + 1
            End If
        Next

        ' Adjust the event count if anything was deleted
        If lowIndex <> -1 Then
            ' Set the new count
            MyMap.EventCount = lowIndex - 1
            ReDim Preserve MapEvents(MyMap.EventCount)
            ReDim Preserve MyMap.Event(MyMap.EventCount)
            TmpEvent = Nothing
        End If
    End Sub


    Sub AddEvent(ByVal X As Integer, ByVal Y As Integer, Optional ByVal cancelLoad As Boolean = False)
        Dim count As Integer, pageCount As Integer, i As Integer

        count = MyMap.EventCount

        ' make sure there's not already an event
        If count > 0 Then
            For i = 0 To count
                If MyMap.Event(i).X = X And MyMap.Event(i).Y = Y Then
                    ' already an event - edit it
                    If Not cancelLoad Then EventEditorInit(i)
                    Exit Sub
                End If
            Next
        End If

        ' increment count
        If count = 0 Then
            count = 2
        else
            count = count + 1
        End If

        MyMap.EventCount = count
        ReDim Preserve MyMap.Event(count)
        ' set the new event
        MyMap.Event(count).X = X
        MyMap.Event(count).Y = Y
        ' give it a new page
        pageCount = MyMap.Event(count).PageCount + 1
        MyMap.Event(count).PageCount = pageCount
        ReDim Preserve MyMap.Event(count).Pages(pageCount)
        ' load the editor
        If Not cancelLoad Then EventEditorInit(count)
    End Sub

    Sub ClearEvent(ByVal EventNum As Integer)
        If EventNum > MyMap.EventCount Or EventNum > UBound(MyMap.Event) Then Exit Sub
        
        With MyMap.Event(EventNum)
            .Name = ""
            .PageCount = 0
            ReDim .Pages(0)
            .Globals = 0
            .X = 0
            .Y = 0
        End With
    End Sub

    Sub EventEditorInit(ByVal EventNum As Integer)
        EditorEvent = EventNum
        TmpEvent = MyMap.Event(EventNum)
        InitEventEditorForm = True
        If TmpEvent.Pages(1).CommandListCount = 0 Then
            ReDim Preserve TmpEvent.Pages(1).CommandList(1)
            TmpEvent.Pages(1).CommandListCount = 1
            TmpEvent.Pages(1).CommandList(1).CommandCount = 1
            ReDim Preserve TmpEvent.Pages(1).CommandList(1).Commands(TmpEvent.Pages(1).CommandList(1).CommandCount)
        End If
    End Sub

    Sub EventEditorLoadPage(ByVal PageNum As Integer)
        With TmpEvent.Pages(PageNum)
            GraphicSelX = .GraphicX
            GraphicSelY = .GraphicY
            GraphicSelX2 = .GraphicX2
            GraphicSelY2 = .GraphicY2
            frmEditor_Event.cmbGraphic.SelectedIndex = .GraphicType
            frmEditor_Event.cmbHasItem.SelectedIndex = .HasItemIndex
            If .HasItemAmount = 0 Then
                frmEditor_Event.nudCondition_HasItem.Value = 1
            Else
                frmEditor_Event.nudCondition_HasItem.Value = .HasItemAmount
            End If
            frmEditor_Event.cmbMoveFreq.SelectedIndex = .MoveFreq
            frmEditor_Event.cmbMoveSpeed.SelectedIndex = .MoveSpeed
            frmEditor_Event.cmbMoveType.SelectedIndex = .MoveType
            frmEditor_Event.cmbPlayerVar.SelectedIndex = .VariableIndex
            frmEditor_Event.cmbPlayerSwitch.SelectedIndex = .SwitchIndex
            frmEditor_Event.cmbSelfSwitchCompare.SelectedIndex = .SelfSwitchCompare
            frmEditor_Event.cmbPlayerSwitchCompare.SelectedIndex = .SwitchCompare
            frmEditor_Event.cmbPlayervarCompare.SelectedIndex = .VariableCompare
            frmEditor_Event.chkGlobal.Checked = TmpEvent.Globals
            frmEditor_Event.cmbTrigger.SelectedIndex = .Trigger
            frmEditor_Event.chkDirFix.Checked = .DirFix
            frmEditor_Event.chkHasItem.Checked = .ChkHasItem
            frmEditor_Event.chkPlayerVar.Checked = .ChkVariable
            frmEditor_Event.chkPlayerSwitch.Checked = .ChkSwitch
            frmEditor_Event.chkSelfSwitch.Checked = .ChkSelfSwitch
            frmEditor_Event.chkWalkAnim.Checked = .WalkAnim
            frmEditor_Event.chkWalkThrough.Checked = .WalkThrough
            frmEditor_Event.chkShowName.Checked = .ShowName
            frmEditor_Event.nudPlayerVariable.Value = .VariableCondition
            frmEditor_Event.nudGraphic.Value = .Graphic

            If .ChkSelfSwitch = 0 Then
                frmEditor_Event.cmbSelfSwitch.Enabled = False
                frmEditor_Event.cmbSelfSwitchCompare.Enabled = False
            Else
                frmEditor_Event.cmbSelfSwitch.Enabled = True
                frmEditor_Event.cmbSelfSwitchCompare.Enabled = True
            End If
            If .ChkSwitch = 0 Then
                frmEditor_Event.cmbPlayerSwitch.Enabled = False
                frmEditor_Event.cmbPlayerSwitchCompare.Enabled = False
            Else
                frmEditor_Event.cmbPlayerSwitch.Enabled = True
                frmEditor_Event.cmbPlayerSwitchCompare.Enabled = True
            End If
            If .ChkVariable = 0 Then
                frmEditor_Event.cmbPlayerVar.Enabled = False
                frmEditor_Event.nudPlayerVariable.Enabled = False
                frmEditor_Event.cmbPlayervarCompare.Enabled = False
            Else
                frmEditor_Event.cmbPlayerVar.Enabled = True
                frmEditor_Event.nudPlayerVariable.Enabled = True
                frmEditor_Event.cmbPlayervarCompare.Enabled = True
            End If
            If frmEditor_Event.cmbMoveType.SelectedIndex = 2 Then
                frmEditor_Event.btnMoveRoute.Enabled = True
            Else
                frmEditor_Event.btnMoveRoute.Enabled = False
            End If
            frmEditor_Event.cmbPositioning.SelectedIndex = Integer.Parse(.Position)   
            EventListCommands()
            Client.EditorEvent_DrawGraphic()
        End With

    End Sub

    Sub EventEditorOK()
        ' copy the event data from the temp event
        MyMap.Event(EditorEvent) = TmpEvent
        TmpEvent = Nothing

        ' unload the form
        frmEditor_Event.Dispose()
    End Sub

    Public Sub EventListCommands()
        Dim i As Integer, curlist As Integer, X As Integer, indent As String = "", listleftoff() As Integer, conditionalstage() As Integer

        frmEditor_Event.lstCommands.Items.Clear()

        If TmpEvent.Pages(CurPageNum).CommandListCount > 0 Then
            ReDim listleftoff(TmpEvent.Pages(CurPageNum).CommandListCount)
            ReDim conditionalstage(TmpEvent.Pages(CurPageNum).CommandListCount)
            curlist = 1
            X = 0
            ReDim Preserve EventList(X)
newlist:
            For i = 1 To TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
                If listleftoff(curlist) > 0 Then
                    If (TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(listleftoff(curlist)).Index = EventType.Condition Or TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(listleftoff(curlist)).Index = EventType.ShowChoices) And conditionalstage(curlist) <> 0 Then
                        i = listleftoff(curlist)
                    ElseIf listleftoff(curlist) >= i Then
                        i = listleftoff(curlist) + 1
                    End If
                End If
                If i < TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then
                    If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Index = EventType.Condition Then
                        X = X + 1
                        ReDim Preserve EventList(X)
                        Select Case conditionalstage(curlist)
                            Case 0
                                EventList(X).CommandList = curlist
                                EventList(X).CommandNum = i
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Condition
                                    Case 0
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2
                                            Case 0
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] == " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 1
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] >= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 2
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] <= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 3
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] > " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 4
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] < " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                            Case 5
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] != " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                        End Select
                                    Case 1
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 0 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] == " & "True")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 1 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1) & "] == " & "False")
                                        End If
                                    Case 2
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Has Item [" & Type.Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1).Name & "] x" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2)
                                    Case 3
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Job Is [" & Type.Job(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1).Name & "]")
                                    Case 4
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player Knows Skill [" & Type.Skill(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1).Name & "]")
                                    Case 5
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2
                                            Case 0
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is == " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 1
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is >= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 2
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is <= " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 3
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is > " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 4
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is < " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                            Case 5
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Level is NOT " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1)
                                        End Select
                                    Case 6
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 0 Then
                                            Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                                Case 0
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [A] == " & "True")
                                                Case 1
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [B] == " & "True")
                                                Case 2
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [C] == " & "True")
                                                Case 3
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [D] == " & "True")
                                            End Select
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 1 Then
                                            Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                                Case 0
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [A] == " & "False")
                                                Case 1
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [B] == " & "False")
                                                Case 2
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [C] == " & "False")
                                                Case 3
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Self Switch [D] == " & "False")
                                            End Select
                                        End If
                                    Case 7
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 0 Then
                                            Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3
                                                Case 0
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] not started.")
                                                Case 1
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] is started.")
                                                Case 2
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] is completed.")
                                                Case 3
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] can be started.")
                                                Case 4
                                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] can be ended. (All tasks complete)")
                                            End Select
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data2 = 1 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Quest [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1 & "] in progress and on task #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data3)
                                        End If
                                    Case 8
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                            Case SexType.Male
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's Gender is Male")
                                            Case SexType.Female
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Player's  Gender is Female")
                                        End Select
                                    Case 9
                                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.Data1
                                            Case TimeOfDay.Day
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Day")
                                            Case TimeOfDay.Night
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Night")
                                            Case TimeOfDay.Dawn
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Dawn")
                                            Case TimeOfDay.Dusk
                                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Conditional Branch: Time of Day is Dusk")
                                        End Select
                                End Select
                                indent = indent & "       "
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 1
                                curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.CommandList
                                GoTo newlist
                            Case 1
                                EventList(X).CommandList = curlist
                                EventList(X).CommandNum = 0
                                frmEditor_Event.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "Else")
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 2
                                curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).ConditionalBranch.ElseCommandList
                                GoTo newlist
                            Case 2
                                EventList(X).CommandList = curlist
                                EventList(X).CommandNum = 0
                                frmEditor_Event.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "End Branch")
                                indent = Mid(indent, 1, Len(indent) - 7)
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 0
                        End Select
                    ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Index = EventType.ShowChoices Then
                        X = X + 1
                        Select Case conditionalstage(curlist)
                            Case 0
                                ReDim Preserve EventList(X)
                                EventList(X).CommandList = curlist
                                EventList(X).CommandNum = i
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Choices - Prompt: " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20))
                                indent = indent & "       "
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 1
                                GoTo newlist
                            Case 1
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text2 <> "" Then
                                    ReDim Preserve EventList(X)
                                    EventList(X).CommandList = curlist
                                    EventList(X).CommandNum = 0
                                    frmEditor_Event.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text2 & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 2
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    GoTo newlist
                                Else
                                    X = X - 1
                                    ReDim Preserve EventList(X)
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 2
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 2
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text3 <> "" Then
                                    ReDim Preserve EventList(X)
                                    EventList(X).CommandList = curlist
                                    EventList(X).CommandNum = 0
                                    frmEditor_Event.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text3 & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 3
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2
                                    GoTo newlist
                                Else
                                    X = X - 1
                                    ReDim Preserve EventList(X)
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 3
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 3
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text4 <> "" Then
                                    ReDim Preserve EventList(X)
                                    EventList(X).CommandList = curlist
                                    EventList(X).CommandNum = 0
                                    frmEditor_Event.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text4 & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 4
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3
                                    GoTo newlist
                                Else
                                    X = X - 1
                                    ReDim Preserve EventList(X)
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 4
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 4
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text5 <> "" Then
                                    ReDim Preserve EventList(X)
                                    EventList(X).CommandList = curlist
                                    EventList(X).CommandNum = 0
                                    frmEditor_Event.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "When [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text5 & "]")
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 5
                                    curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4
                                    GoTo newlist
                                Else
                                    X = X - 1
                                    ReDim Preserve EventList(X)
                                    listleftoff(curlist) = i
                                    conditionalstage(curlist) = 5
                                    curlist = curlist
                                    GoTo newlist
                                End If
                            Case 5
                                ReDim Preserve EventList(X)
                                EventList(X).CommandList = curlist
                                EventList(X).CommandNum = 0
                                frmEditor_Event.lstCommands.Items.Add(Mid(indent, 1, Len(indent) - 4) & " : " & "Branch End")
                                indent = Mid(indent, 1, Len(indent) - 7)
                                listleftoff(curlist) = i
                                conditionalstage(curlist) = 0
                        End Select
                    Else
                        X = X + 1
                        ReDim Preserve EventList(X)
                        EventList(X).CommandList = curlist
                        EventList(X).CommandNum = i
                        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Index
                            Case EventType.AddText
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2
                                    Case 0
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Add Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Color: " & GetColorString(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " - Chat Type: Player")
                                    Case 1
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Add Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Color: " & GetColorString(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " - Chat Type: Map")
                                    Case 2
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Add Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - Color: " & GetColorString(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " - Chat Type: Global")
                                End Select
                            Case EventType.ShowText
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Text - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20))
                            Case EventType.PlayerVar
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2
                                    Case 0
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] == " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                    Case 1
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] + " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                    Case 2
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] - " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                    Case 3
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Variable [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & Variables(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] Random Between " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " and " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4)
                                End Select
                            Case EventType.PlayerSwitch
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] == True")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Switch [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & ". " & Switches(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "] == False")
                                End If
                            Case EventType.SelfSwitch
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    Case 0
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [A] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [A] to OFF")
                                        End If
                                    Case 1
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [B] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [B] to OFF")
                                        End If
                                    Case 2
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [C] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [C] to OFF")
                                        End If
                                    Case 3
                                        If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [D] to ON")
                                        ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Self Switch [D] to OFF")
                                        End If
                                End Select
                            Case EventType.ExitProcess
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Exit Event Processing")
                            Case EventType.ChangeItems
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Item Amount of [" & Type.Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "] to " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3)
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Give Player " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " " & Type.Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "(s)")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 2 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Take " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " " & Type.Item(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "(s) from Player.")
                                End If
                            Case EventType.RestoreHP
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Restore Player HP")
                            Case EventType.RestoreSP
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Restore Player MP")
                            Case EventType.LevelUp
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Level Up Player")
                            Case EventType.ChangeLevel
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Level to " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)
                            Case EventType.ChangeSkills
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Teach Player Skill [" & Type.Skill(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "]")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Remove Player Skill [" & Type.Skill(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "]")
                                End If
                            Case EventType.ChangeJob
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Job to " & Type.Job(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name)
                            Case EventType.ChangeSprite
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Sprite to " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)
                            Case EventType.ChangeSex
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Sex to Male.")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 1 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Sex to Female.")
                                End If
                            Case EventType.ChangePk
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player PK to No.")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 = 1 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player PK to Yes.")
                                End If
                            Case EventType.WarpPlayer
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4 = 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") while retaining direction.")
                                Else
                                    Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4 - 1
                                        Case DirectionType.Up
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing upward.")
                                        Case DirectionType.Down
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing downward.")
                                        Case DirectionType.Left
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing left.")
                                        Case DirectionType.Right
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Warp Player To Map: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & ") facing right.")
                                    End Select
                                End If
                            Case EventType.SetMoveRoute
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 <= MyMap.EventCount Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Move Route for Event #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & MyMap.Event(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "]")
                                Else
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Move Route for COULD NOT FIND EVENT!")
                                End If
                            Case EventType.PlayAnimation
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Play Animation " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Type.Animation(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "]" & " on Player")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 1 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Play Animation " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Type.Animation(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "]" & " on Event #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & " [" & MyMap.Event(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3).Name & "]")
                                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2 = 2 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Play Animation " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & Type.Animation(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "]" & " on Tile(" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3 & "," & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4 & ")")
                                End If
                            Case EventType.CustomScript
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Execute Custom Script Case: " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)
                            Case EventType.PlayBgm
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Play BGM [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1 & "]")
                            Case EventType.FadeoutBgm
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Fadeout BGM")
                            Case EventType.PlaySound
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Play Sound [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1 & "]")
                            Case EventType.StopSound
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Stop Sound")
                            Case EventType.OpenBank
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Open Bank")
                            Case EventType.OpenShop
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Open Shop [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & Type.Shop(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "]")
                            Case EventType.SetAccess
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Player Access [" & frmEditor_Event.cmbSetAccess.Items(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "]")
                            Case EventType.GiveExp
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Give Player " & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " Experience.")
                            Case EventType.ShowChatBubble
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    Case TargetType.Player
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On Player")
                                    Case TargetType.NPC
                                        If MyMap.NPC(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) <= 0 Then
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On NPC [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & ". ]")
                                        Else
                                            frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On NPC [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & ". " & Type.NPC(MyMap.NPC(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2)).Name & "]")
                                        End If
                                    Case TargetType.Event
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Chat Bubble - " & Mid(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1, 1, 20) & "... - On Event [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & ". " & MyMap.Event(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2).Name & "]")
                                End Select
                            Case EventType.Label
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Label: [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1 & "]")
                            Case EventType.GotoLabel
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Jump to Label: [" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Text1 & "]")
                            Case EventType.SpawnNpc
                                If MyMap.NPC(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) <= 0 Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Spawn NPC: [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & "]")
                                Else
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Spawn NPC: [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ". " & Type.NPC(MyMap.NPC(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1)).Name & "]")
                                End If
                            Case EventType.FadeIn
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Fade In")
                            Case EventType.FadeOut
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Fade Out")
                            Case EventType.FlashWhite
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Flash White")
                            Case EventType.SetFog
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Fog [Fog: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " Speed: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " Opacity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3) & "]")
                            Case EventType.SetWeather
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1
                                    Case [Enum].Weather.None
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Weather [None]")
                                    Case [Enum].Weather.Rain
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Weather [Rain - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                    Case [Enum].Weather.Snow
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Weather [Snow - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                    Case [Enum].Weather.Sandstorm
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Weather [Sand Storm - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                    Case [Enum].Weather.Storm
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Weather [Storm - Intensity: " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "]")
                                End Select
                            Case EventType.SetTint
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Set Map Tint RGBA [" & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & "," & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & "," & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data3) & "," & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & "]")
                            Case EventType.Wait
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Wait " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & " Ms")
                            Case EventType.ShowPicture
                                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2
                                    Case 0
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ": Pic=" & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " Top Left, X: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & " Y: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5))
                                    Case 1
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ": Pic=" & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " Center Screen, X: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & " Y: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5))
                                    Case 2
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ": Pic=" & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " On Event, X: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & " Y: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5))
                                    Case 3
                                        frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Show Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1) & ": Pic=" & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data2) & " On Player, X: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data4) & " Y: " & Str(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data5))
                                End Select
                            Case EventType.HidePicture
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Hide Picture " & CStr(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1))
                            Case EventType.WaitMovement
                                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 <= MyMap.EventCount Then
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Wait for Event #" & TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1 & " [" & MyMap.Event(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i).Data1).Name & "] to complete move route.")
                                Else
                                    frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Wait for COULD NOT FIND EVENT to complete move route.")
                                End If
                            Case EventType.HoldPlayer
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Hold Player [Do not allow player to move.]")
                            Case EventType.ReleasePlayer
                                frmEditor_Event.lstCommands.Items.Add(indent & "@>" & "Release Player [Allow player to turn and move again.]")
                            Case Else
                                'Ghost
                                X = X - 1
                                If X = -1 Then
                                    ReDim EventList(0)
                                Else
                                    ReDim Preserve EventList(X)
                                End If
                        End Select
                    End If
                End If
            Next

            If curlist > 1 Then
                X = X + 1
                ReDim Preserve EventList(X)
                EventList(X).CommandList = curlist
                EventList(X).CommandNum = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount + 1
                frmEditor_Event.lstCommands.Items.Add(indent & "@> ")
                curlist = TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList
                GoTo newlist
            End If
        End If
        frmEditor_Event.lstCommands.Items.Add(indent & "@> ")

        Dim z As Integer
        X = 0
        For i = 0 To frmEditor_Event.lstCommands.Items.Count
            If X > z Then z = X
        Next

    End Sub

    Sub AddCommand(ByVal Index As Integer)
        Dim curlist As Integer, i As Integer, X As Integer, curslot As Integer, p As Integer
        Dim oldCommandList As CommandListStruct

        If frmEditor_Event.lstCommands.SelectedIndex + 1 = frmEditor_Event.lstCommands.Items.Count Then
            curlist = 1
        Else
            curlist = EventList(frmEditor_Event.lstCommands.SelectedIndex + 1).CommandList
        End If
      
        TmpEvent.Pages(CurPageNum).CommandListCount = TmpEvent.Pages(CurPageNum).CommandListCount + 1
        ReDim Preserve TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount)
        TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount + 1
        p = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
        ReDim Preserve TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(p)

        If frmEditor_Event.lstCommands.SelectedIndex + 1 = frmEditor_Event.lstCommands.Items.Count Then
            curslot = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount - 1
        Else
            oldCommandList = TmpEvent.Pages(CurPageNum).CommandList(curlist)
            TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList = oldCommandList.ParentList

            For i = 1 To p - 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(i) = oldCommandList.Commands(i)
            Next

            i = EventList(frmEditor_Event.lstCommands.SelectedIndex + 1).CommandNum
            If i <= TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then

                For X = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount To i
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(X + 1) = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(X)
                Next

                curslot = EventList(frmEditor_Event.lstCommands.SelectedIndex + 1).CommandNum
            Else
                curslot = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
            End If

        End If

        Select Case Index
            Case EventType.AddText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtAddText_Text.Text
                If frmEditor_Event.optAddText_Player.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.optAddText_Map.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEditor_Event.optAddText_Global.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
            Case EventType.Condition
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandListCount = TmpEvent.Pages(CurPageNum).CommandListCount + 2
                ReDim Preserve TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount)
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.CommandList = TmpEvent.Pages(CurPageNum).CommandListCount - 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.ElseCommandList = TmpEvent.Pages(CurPageNum).CommandListCount
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.CommandList).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.ElseCommandList).ParentList = curlist

                If frmEditor_Event.optCondition0.Checked = True Then X = 0
                If frmEditor_Event.optCondition1.Checked = True Then X = 1
                If frmEditor_Event.optCondition2.Checked = True Then X = 2
                If frmEditor_Event.optCondition3.Checked = True Then X = 3
                If frmEditor_Event.optCondition4.Checked = True Then X = 4
                If frmEditor_Event.optCondition5.Checked = True Then X = 5
                If frmEditor_Event.optCondition6.Checked = True Then X = 6
                If frmEditor_Event.optCondition8.Checked = True Then X = 8
                If frmEditor_Event.optCondition9.Checked = True Then X = 9

                Select Case X
                    Case 0 'Player Var
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 0
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_PlayerVarIndex.SelectedIndex
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondition_PlayerVarCompare.SelectedIndex
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEditor_Event.nudCondition_PlayerVarCondition.Value
                    Case 1 'Player Switch
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 1
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_PlayerSwitch.SelectedIndex
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondtion_PlayerSwitchCondition.SelectedIndex
                    Case 2 'Has Item
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 2
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_HasItem.SelectedIndex
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.nudCondition_HasItem.Value
                    Case 3 'Job Is
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 3
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_JobIs.SelectedIndex
                    Case 4 'Learnt Skill
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 4
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_LearntSkill.SelectedIndex
                    Case 5 'Level Is
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 5
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.nudCondition_LevelAmount.Value
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondition_LevelCompare.SelectedIndex
                    Case 6 'Self Switch
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 6
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_SelfSwitch.SelectedIndex
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondition_SelfSwitchCondition.SelectedIndex
                    Case 7

                    Case 8 'Gender
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 8
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_Gender.SelectedIndex
                    Case 9 'Time
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 9
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_Time.SelectedIndex
                End Select

            Case EventType.ShowText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                Dim tmptxt As String = ""
                For i = 0 To UBound(frmEditor_Event.txtShowText.Lines)
                    tmptxt = tmptxt & frmEditor_Event.txtShowText.Lines(i)
                Next
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = tmptxt

            Case EventType.ShowChoices
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtChoicePrompt.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text2 = frmEditor_Event.txtChoices1.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text3 = frmEditor_Event.txtChoices2.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text4 = frmEditor_Event.txtChoices3.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text5 = frmEditor_Event.txtChoices4.Text
                TmpEvent.Pages(CurPageNum).CommandListCount = TmpEvent.Pages(CurPageNum).CommandListCount + 4
                ReDim Preserve TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount)
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = TmpEvent.Pages(CurPageNum).CommandListCount - 3
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = TmpEvent.Pages(CurPageNum).CommandListCount - 2
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = TmpEvent.Pages(CurPageNum).CommandListCount - 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = TmpEvent.Pages(CurPageNum).CommandListCount
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount - 3).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount - 2).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount - 1).ParentList = curlist
                TmpEvent.Pages(CurPageNum).CommandList(TmpEvent.Pages(CurPageNum).CommandListCount).ParentList = curlist

            Case EventType.PlayerVar
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbVariable.SelectedIndex

                If frmEditor_Event.optVariableAction0.Checked = True Then i = 0
                If frmEditor_Event.optVariableAction1.Checked = True Then i = 1
                If frmEditor_Event.optVariableAction2.Checked = True Then i = 2
                If frmEditor_Event.optVariableAction3.Checked = True Then i = 3

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = i
                If i = 3 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData3.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudVariableData4.Value
                ElseIf i = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData0.Value
                ElseIf i = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData1.Value
                ElseIf i = 2 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData2.Value
                End If

            Case EventType.PlayerSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSwitch.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbPlayerSwitchSet.SelectedIndex

            Case EventType.SelfSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSetSelfSwitch.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbSetSelfSwitchTo.SelectedIndex

            Case EventType.ExitProcess
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.ChangeItems
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChangeItemIndex.SelectedIndex
                If frmEditor_Event.optChangeItemSet.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.optChangeItemAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEditor_Event.optChangeItemRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudChangeItemsAmount.Value

            Case EventType.RestoreHP
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.RestoreSP
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.LevelUp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.ChangeLevel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudChangeLevel.Value

            Case EventType.ChangeSkills
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChangeSkills.SelectedIndex
                If frmEditor_Event.optChangeSkillsAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.optChangeSkillsRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                End If

            Case EventType.ChangeJob
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChangeJob.SelectedIndex

            Case EventType.ChangeSprite
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudChangeSprite.Value

            Case EventType.ChangeSex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                If frmEditor_Event.optChangeSexMale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = SexType.Male
                ElseIf frmEditor_Event.optChangeSexFemale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = SexType.Female
                End If

            Case EventType.ChangePk
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSetPK.SelectedIndex

            Case EventType.WarpPlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudWPMap.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudWPX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudWPY.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.cmbWarpPlayerDir.SelectedIndex

            Case EventType.SetMoveRoute
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEditor_Event.cmbEvent.SelectedIndex)
                If frmEditor_Event.chkIgnoreMove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                End If

                If frmEditor_Event.chkRepeatRoute.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 0
                End If

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRouteCount = TempMoveRouteCount
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRoute = TempMoveRoute

            Case EventType.PlayAnimation
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbPlayAnim.SelectedIndex
                If frmEditor_Event.cmbAnimTargetType.SelectedIndex = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.cmbAnimTargetType.SelectedIndex = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.cmbPlayAnimEvent.SelectedIndex
                ElseIf frmEditor_Event.cmbAnimTargetType.SelectedIndex = 2 = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudPlayAnimTileX.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudPlayAnimTileY.Value
                End If

            Case EventType.CustomScript
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudCustomScript.Value

            Case EventType.PlayBgm
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = MusicCache(frmEditor_Event.cmbPlayBGM.SelectedIndex)

            Case EventType.FadeoutBgm
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.PlaySound
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = SoundCache(frmEditor_Event.cmbPlaySound.SelectedIndex)

            Case EventType.StopSound
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.OpenBank
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.OpenShop
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbOpenShop.SelectedIndex

            Case EventType.SetAccess
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSetAccess.SelectedIndex + 1

            Case EventType.GiveExp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudGiveExp.Value

            Case EventType.ShowChatBubble
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtChatbubbleText.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChatBubbleTargetType.SelectedIndex + 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbChatBubbleTarget.SelectedIndex

            Case EventType.Label
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtLabelName.Text

            Case EventType.GotoLabel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtGotoLabel.Text

            Case EventType.SpawnNpc
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSpawnNpc.SelectedIndex

            Case EventType.FadeIn
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.FadeOut
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.FlashWhite
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.SetFog
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudFogData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudFogData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudFogData2.Value

            Case EventType.SetWeather
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.CmbWeather.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudWeatherIntensity.Value

            Case EventType.SetTint
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudMapTintData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudMapTintData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudMapTintData2.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudMapTintData3.Value

            Case EventType.Wait
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudWaitAmount.Value

            Case EventType.ShowPicture
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudShowPicture.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbPicLoc.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudPicOffsetX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudPicOffsetY.Value

            Case EventType.HidePicture
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.WaitMovement
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEditor_Event.cmbMoveWait.SelectedIndex)

            Case EventType.HoldPlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index

            Case EventType.ReleasePlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index = Index
        End Select

        EventListCommands()

    End Sub

    Public Sub EditEventCommand()
        Dim i As Integer, X As Integer, curlist As Integer, curslot As Integer

        i = frmEditor_Event.lstCommands.SelectedIndex + 1
        If i = -1 Then Exit Sub
        If i > UBound(EventList) Then Exit Sub

        frmEditor_Event.fraConditionalBranch.Visible = False
        frmEditor_Event.fraDialogue.BringToFront()

        curlist = EventList(i).CommandList
        curslot = EventList(i).CommandNum
        If curlist = 0 Then Exit Sub
        If curslot = 0 Then Exit Sub
        If curlist > TmpEvent.Pages(CurPageNum).CommandListCount Then Exit Sub
        If curslot > TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then Exit Sub

        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index
            Case EventType.AddText
                IsEdit = True
                frmEditor_Event.txtAddText_Text.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                'frmEditor_Event.scrlAddText_Color.Value = tmpEvent.Pages(curPageNum).CommandList(curlist).Commands(curslot).Data1
                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                    Case 0
                        frmEditor_Event.optAddText_Player.Checked = True
                    Case 1
                        frmEditor_Event.optAddText_Map.Checked = True
                    Case 2
                        frmEditor_Event.optAddText_Global.Checked = True
                End Select
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraAddText.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.Condition
                IsEdit = True
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraConditionalBranch.Visible = True
                frmEditor_Event.fraCommands.Visible = False
                frmEditor_Event.ClearConditionFrame()

                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition
                    Case 0
                        frmEditor_Event.optCondition0.Checked = True
                    Case 1
                        frmEditor_Event.optCondition1.Checked = True
                    Case 2
                        frmEditor_Event.optCondition2.Checked = True
                    Case 3
                        frmEditor_Event.optCondition3.Checked = True
                    Case 4
                        frmEditor_Event.optCondition4.Checked = True
                    Case 5
                        frmEditor_Event.optCondition5.Checked = True
                    Case 6
                        frmEditor_Event.optCondition6.Checked = True
                    Case 7

                    Case 8
                        frmEditor_Event.optCondition8.Checked = True
                    Case 9
                        frmEditor_Event.optCondition9.Checked = True
                End Select

                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition
                    Case 0
                        frmEditor_Event.cmbCondition_PlayerVarIndex.Enabled = True
                        frmEditor_Event.cmbCondition_PlayerVarCompare.Enabled = True
                        frmEditor_Event.nudCondition_PlayerVarCondition.Enabled = True
                        frmEditor_Event.cmbCondition_PlayerVarIndex.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                        frmEditor_Event.cmbCondition_PlayerVarCompare.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                        frmEditor_Event.nudCondition_PlayerVarCondition.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3
                    Case 1
                        frmEditor_Event.cmbCondition_PlayerSwitch.Enabled = True
                        frmEditor_Event.cmbCondtion_PlayerSwitchCondition.Enabled = True
                        frmEditor_Event.cmbCondition_PlayerSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                        frmEditor_Event.cmbCondtion_PlayerSwitchCondition.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 2
                        frmEditor_Event.cmbCondition_HasItem.Enabled = True
                        frmEditor_Event.nudCondition_HasItem.Enabled = True
                        frmEditor_Event.cmbCondition_HasItem.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                        frmEditor_Event.nudCondition_HasItem.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 3
                        frmEditor_Event.cmbCondition_JobIs.Enabled = True
                        frmEditor_Event.cmbCondition_JobIs.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                    Case 4
                        frmEditor_Event.cmbCondition_LearntSkill.Enabled = True
                        frmEditor_Event.cmbCondition_LearntSkill.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 - 1
                    Case 5
                        frmEditor_Event.cmbCondition_LevelCompare.Enabled = True
                        frmEditor_Event.nudCondition_LevelAmount.Enabled = True
                        frmEditor_Event.nudCondition_LevelAmount.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                        frmEditor_Event.cmbCondition_LevelCompare.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 6
                        frmEditor_Event.cmbCondition_SelfSwitch.Enabled = True
                        frmEditor_Event.cmbCondition_SelfSwitchCondition.Enabled = True
                        frmEditor_Event.cmbCondition_SelfSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                        frmEditor_Event.cmbCondition_SelfSwitchCondition.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2
                    Case 7

                    Case 8
                        frmEditor_Event.cmbCondition_Gender.Enabled = True
                        frmEditor_Event.cmbCondition_Gender.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                    Case 9
                        frmEditor_Event.cmbCondition_Time.Enabled = True
                        frmEditor_Event.cmbCondition_Time.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1
                End Select
            Case EventType.ShowText
                IsEdit = True
                frmEditor_Event.txtShowText.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraShowText.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ShowChoices
                IsEdit = True
                frmEditor_Event.txtChoicePrompt.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEditor_Event.txtChoices1.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text2
                frmEditor_Event.txtChoices2.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text3
                frmEditor_Event.txtChoices3.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text4
                frmEditor_Event.txtChoices4.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text5
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraShowChoices.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.PlayerVar
                IsEdit = True
                frmEditor_Event.cmbVariable.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                    Case 0
                        frmEditor_Event.optVariableAction0.Checked = True
                        frmEditor_Event.nudVariableData0.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    Case 1
                        frmEditor_Event.optVariableAction1.Checked = True
                        frmEditor_Event.nudVariableData1.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    Case 2
                        frmEditor_Event.optVariableAction2.Checked = True
                        frmEditor_Event.nudVariableData2.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    Case 3
                        frmEditor_Event.optVariableAction3.Checked = True
                        frmEditor_Event.nudVariableData3.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                        frmEditor_Event.nudVariableData4.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                End Select
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraPlayerVariable.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.PlayerSwitch
                IsEdit = True
                frmEditor_Event.cmbSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEditor_Event.cmbPlayerSwitchSet.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraPlayerSwitch.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.SelfSwitch
                IsEdit = True
                frmEditor_Event.cmbSetSelfSwitch.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.cmbSetSelfSwitchTo.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraSetSelfSwitch.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ChangeItems
                IsEdit = True
                frmEditor_Event.cmbChangeItemIndex.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0 Then
                    frmEditor_Event.optChangeItemSet.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1 Then
                    frmEditor_Event.optChangeItemAdd.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2 Then
                    frmEditor_Event.optChangeItemRemove.Checked = True
                End If
                frmEditor_Event.nudChangeItemsAmount.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraChangeItems.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ChangeLevel
                IsEdit = True
                frmEditor_Event.nudChangeLevel.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraChangeLevel.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ChangeSkills
                IsEdit = True
                frmEditor_Event.cmbChangeSkills.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0 Then
                    frmEditor_Event.optChangeSkillsAdd.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1 Then
                    frmEditor_Event.optChangeSkillsRemove.Checked = True
                End If
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraChangeSkills.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ChangeJob
                IsEdit = True
                frmEditor_Event.cmbChangeJob.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraChangeJob.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ChangeSprite
                IsEdit = True
                frmEditor_Event.nudChangeSprite.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraChangeSprite.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ChangeSex
                IsEdit = True
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 0 Then
                    frmEditor_Event.optChangeSexMale.Checked = True
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 1 Then
                    frmEditor_Event.optChangeSexFemale.Checked = True
                End If
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraChangeGender.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ChangePk
                IsEdit = True

                frmEditor_Event.cmbSetPK.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1

                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraChangePK.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.WarpPlayer
                IsEdit = True
                frmEditor_Event.nudWPMap.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.nudWPX.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEditor_Event.nudWPY.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEditor_Event.cmbWarpPlayerDir.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraPlayerWarp.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.SetMoveRoute
                IsEdit = True
                frmEditor_Event.fraMoveRoute.Visible = True
                frmEditor_Event.fraMoveRoute.BringToFront()
                frmEditor_Event.lstMoveRoute.Items.Clear()
                ReDim ListOfEvents(0 To MyMap.EventCount)
                ListOfEvents(0) = EditorEvent
                For i = 0 To MyMap.EventCount 
                    If i <> EditorEvent Then
                        frmEditor_Event.cmbEvent.Items.Add(MyMap.Event(i).Name)
                        X = X + 1
                        ListOfEvents(X) = i
                        If i = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 Then frmEditor_Event.cmbEvent.SelectedIndex = X
                    End If
                Next

                IsMoveRouteCommand = True
                frmEditor_Event.chkIgnoreMove.Checked = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEditor_Event.chkRepeatRoute.Checked = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                TempMoveRouteCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRouteCount
                TempMoveRoute = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRoute
                For i = 0 To TempMoveRouteCount
                    Select Case TempMoveRoute(i).Index
                        Case 1
                            frmEditor_Event.lstMoveRoute.Items.Add("Move Up")
                        Case 2
                            frmEditor_Event.lstMoveRoute.Items.Add("Move Down")
                        Case 3
                            frmEditor_Event.lstMoveRoute.Items.Add("Move Left")
                        Case 4
                            frmEditor_Event.lstMoveRoute.Items.Add("Move Right")
                        Case 5
                            frmEditor_Event.lstMoveRoute.Items.Add("Move Randomly")
                        Case 6
                            frmEditor_Event.lstMoveRoute.Items.Add("Move Towards Player")
                        Case 7
                            frmEditor_Event.lstMoveRoute.Items.Add("Move Away From Player")
                        Case 8
                            frmEditor_Event.lstMoveRoute.Items.Add("Step Forward")
                        Case 9
                            frmEditor_Event.lstMoveRoute.Items.Add("Step Back")
                        Case 10
                            frmEditor_Event.lstMoveRoute.Items.Add("Wait 100ms")
                        Case 11
                            frmEditor_Event.lstMoveRoute.Items.Add("Wait 500ms")
                        Case 12
                            frmEditor_Event.lstMoveRoute.Items.Add("Wait 1000ms")
                        Case 13
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Up")
                        Case 14
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Down")
                        Case 15
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Left")
                        Case 16
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Right")
                        Case 17
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn 90 Degrees To the Right")
                        Case 18
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn 90 Degrees To the Left")
                        Case 19
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Around 180 Degrees")
                        Case 20
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Randomly")
                        Case 21
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Towards Player")
                        Case 22
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Away from Player")
                        Case 23
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Speed 8x Slower")
                        Case 24
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Speed 4x Slower")
                        Case 25
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Speed 2x Slower")
                        Case 26
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Speed to Normal")
                        Case 27
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Speed 2x Faster")
                        Case 28
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Speed 4x Faster")
                        Case 29
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Frequency Lowest")
                        Case 30
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Frequency Lower")
                        Case 31
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Frequency Normal")
                        Case 32
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Frequency Higher")
                        Case 33
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Frequency Highest")
                        Case 34
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn On Walking Animation")
                        Case 35
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Off Walking Animation")
                        Case 36
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn On Fixed Direction")
                        Case 37
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Off Fixed Direction")
                        Case 38
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn On Walk Through")
                        Case 39
                            frmEditor_Event.lstMoveRoute.Items.Add("Turn Off Walk Through")
                        Case 40
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Position Below Characters")
                        Case 41
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Position Same as Characters")
                        Case 42
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Position Above Characters")
                        Case 43
                            frmEditor_Event.lstMoveRoute.Items.Add("Set Graphic")
                    End Select
                Next
                frmEditor_Event.fraMoveRoute.Visible = True
                frmEditor_Event.fraDialogue.Visible = False
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.PlayAnimation
                IsEdit = True
                frmEditor_Event.lblPlayAnimX.Visible = False
                frmEditor_Event.lblPlayAnimY.Visible = False
                frmEditor_Event.nudPlayAnimTileX.Visible = False
                frmEditor_Event.nudPlayAnimTileY.Visible = False
                frmEditor_Event.cmbPlayAnimEvent.Visible = False
                frmEditor_Event.cmbPlayAnim.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEditor_Event.cmbPlayAnimEvent.Items.Clear()
                For i = 0 To MyMap.EventCount 
                    frmEditor_Event.cmbPlayAnimEvent.Items.Add(i & ". " & MyMap.Event(i).Name)
                Next
                frmEditor_Event.cmbPlayAnimEvent.SelectedIndex = 0
                If TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0 Then
                    frmEditor_Event.cmbAnimTargetType.SelectedIndex = 0
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1 Then
                    frmEditor_Event.cmbAnimTargetType.SelectedIndex = 1
                    frmEditor_Event.cmbPlayAnimEvent.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 - 1
                ElseIf TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2 Then
                    frmEditor_Event.cmbAnimTargetType.SelectedIndex = 2
                    frmEditor_Event.nudPlayAnimTileX.Maximum = MyMap.MaxX
                    frmEditor_Event.nudPlayAnimTileY.Maximum = MyMap.MaxY
                    frmEditor_Event.nudPlayAnimTileX.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                    frmEditor_Event.nudPlayAnimTileY.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                End If
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraPlayAnimation.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.CustomScript
                IsEdit = True
                frmEditor_Event.nudCustomScript.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraCustomScript.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.PlayBgm
                IsEdit = True
                For i = 1 To UBound(MusicCache)
                    If MusicCache(i) = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 Then
                        frmEditor_Event.cmbPlayBGM.SelectedIndex = i
                    End If
                Next
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraPlayBGM.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.PlaySound
                IsEdit = True
                For i = 0 To UBound(SoundCache)
                    If SoundCache(i) = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 Then
                        frmEditor_Event.cmbPlaySound.SelectedIndex = i
                    End If
                Next
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraPlaySound.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.OpenShop
                IsEdit = True
                frmEditor_Event.cmbOpenShop.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraOpenShop.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.SetAccess
                IsEdit = True
                frmEditor_Event.cmbSetAccess.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraSetAccess.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.GiveExp
                IsEdit = True
                frmEditor_Event.nudGiveExp.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraGiveExp.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ShowChatBubble
                IsEdit = True
                frmEditor_Event.txtChatbubbleText.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEditor_Event.cmbChatBubbleTargetType.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEditor_Event.cmbChatBubbleTarget.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 - 1

                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraShowChatBubble.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.Label
                IsEdit = True
                frmEditor_Event.txtLabelName.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraCreateLabel.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.GotoLabel
                IsEdit = True
                frmEditor_Event.txtGotoLabel.Text = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraGoToLabel.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.SpawnNpc
                IsEdit = True
                frmEditor_Event.cmbSpawnNpc.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 - 1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraSpawnNpc.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.SetFog
                IsEdit = True
                frmEditor_Event.nudFogData0.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.nudFogData1.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEditor_Event.nudFogData2.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraSetFog.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.SetWeather
                IsEdit = True
                frmEditor_Event.CmbWeather.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.nudWeatherIntensity.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraSetWeather.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.SetTint
                IsEdit = True
                frmEditor_Event.nudMapTintData0.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.nudMapTintData1.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2
                frmEditor_Event.nudMapTintData2.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEditor_Event.nudMapTintData3.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraMapTint.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.Wait
                IsEdit = True
                frmEditor_Event.nudWaitAmount.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraSetWait.Visible = True
                frmEditor_Event.fraCommands.Visible = False
            Case EventType.ShowPicture
                IsEdit = True
                frmEditor_Event.nudShowPicture.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1

                frmEditor_Event.cmbPicLoc.SelectedIndex = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2

                frmEditor_Event.nudPicOffsetX.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3
                frmEditor_Event.nudPicOffsetY.Value = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraShowPic.Visible = True
                frmEditor_Event.fraCommands.Visible = False
                Client.EditorEvent_DrawPicture()
            Case EventType.WaitMovement
                IsEdit = True
                frmEditor_Event.fraDialogue.Visible = True
                frmEditor_Event.fraMoveRouteWait.Visible = True
                frmEditor_Event.fraCommands.Visible = False
                frmEditor_Event.cmbMoveWait.Items.Clear()
                ReDim ListOfEvents(0 To MyMap.EventCount)
                ListOfEvents(0) = EditorEvent
                frmEditor_Event.cmbMoveWait.Items.Add("This Event")
                frmEditor_Event.cmbMoveWait.SelectedIndex = 0
                For i = 0 To MyMap.EventCount 
                    If i <> EditorEvent Then
                        frmEditor_Event.cmbMoveWait.Items.Add(MyMap.Event(i).Name)
                        X = X + 1
                        ListOfEvents(X) = i
                        If i = TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 Then frmEditor_Event.cmbMoveWait.SelectedIndex = X
                    End If
                Next
        End Select

    End Sub

    Public Sub DeleteEventCommand()
        Dim i As Integer, X As Integer, curlist As Integer, curslot As Integer, p As Integer, oldCommandList As CommandListStruct

        i = frmEditor_Event.lstCommands.SelectedIndex + 1
        If i = -1 Then Exit Sub
        If i > UBound(EventList) Then Exit Sub

        curlist = EventList(i).CommandList
        curslot = EventList(i).CommandNum

        If curlist = 0 Then Exit Sub
        If curslot = 0 Then Exit Sub
        If curlist > TmpEvent.Pages(CurPageNum).CommandListCount Then Exit Sub
        If curslot > TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then Exit Sub

        If curslot = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then
            TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount - 1
            p = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
            If p <= 0 Then
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(0)
            Else
                oldCommandList = TmpEvent.Pages(CurPageNum).CommandList(curlist)
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(p)
                X = 1
                TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList = oldCommandList.ParentList
                TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = p
                For i = 1 To p + 1
                    If i <> curslot Then
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(X) = oldCommandList.Commands(i)
                        X = X + 1
                    End If
                Next
            End If
        Else
            TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount - 1
            p = TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount
            oldCommandList = TmpEvent.Pages(CurPageNum).CommandList(curlist)
            X = 1
            If p <= 0 Then
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(0)
            Else
                ReDim TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(p)
                TmpEvent.Pages(CurPageNum).CommandList(curlist).ParentList = oldCommandList.ParentList
                TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount = p
                For i = 1 To p + 1
                    If i <> curslot Then
                        TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(X) = oldCommandList.Commands(i)
                        X = X + 1
                    End If
                Next
            End If
        End If
        EventListCommands()

    End Sub

    Public Sub ClearEventCommands()
        ReDim TmpEvent.Pages(CurPageNum).CommandList(0)
        TmpEvent.Pages(CurPageNum).CommandListCount = 0
        EventListCommands()
    End Sub

    Public Sub EditCommand()
        Dim i As Integer, curlist As Integer, curslot As Integer

        i = frmEditor_Event.lstCommands.SelectedIndex + 1
        If i = -1 Then Exit Sub
        If i > UBound(EventList) Then Exit Sub

        curlist = EventList(i).CommandList
        curslot = EventList(i).CommandNum
        If curlist = 0 Then Exit Sub
        If curslot = 0 Then Exit Sub
        If curlist > TmpEvent.Pages(CurPageNum).CommandListCount Then Exit Sub
        If curslot > TmpEvent.Pages(CurPageNum).CommandList(curlist).CommandCount Then Exit Sub
        Select Case TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Index
            Case EventType.AddText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtAddText_Text.Text
                'tmpEvent.Pages(curPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.scrlAddText_Color.Value
                If frmEditor_Event.optAddText_Player.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.optAddText_Map.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEditor_Event.optAddText_Global.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
            Case EventType.Condition
                If frmEditor_Event.optCondition0.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 0
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_PlayerVarIndex.SelectedIndex
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondition_PlayerVarCompare.SelectedIndex
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data3 = frmEditor_Event.nudCondition_PlayerVarCondition.Value
                ElseIf frmEditor_Event.optCondition1.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_PlayerSwitch.SelectedIndex
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondtion_PlayerSwitchCondition.SelectedIndex
                ElseIf frmEditor_Event.optCondition2.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 2
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_HasItem.SelectedIndex
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.nudCondition_HasItem.Value
                ElseIf frmEditor_Event.optCondition3.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 3
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_JobIs.SelectedIndex
                ElseIf frmEditor_Event.optCondition4.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 4
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_LearntSkill.SelectedIndex
                ElseIf frmEditor_Event.optCondition5.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 5
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.nudCondition_LevelAmount.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondition_LevelCompare.SelectedIndex
                ElseIf frmEditor_Event.optCondition6.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 6
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_SelfSwitch.SelectedIndex
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data2 = frmEditor_Event.cmbCondition_SelfSwitchCondition.SelectedIndex
                ElseIf frmEditor_Event.optCondition8.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 8
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_Gender.SelectedIndex
                ElseIf frmEditor_Event.optCondition9.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Condition = 9
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).ConditionalBranch.Data1 = frmEditor_Event.cmbCondition_Time.SelectedIndex
                End If
            Case EventType.ShowText
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtShowText.Text
            Case EventType.ShowChoices
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtChoicePrompt.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text2 = frmEditor_Event.txtChoices1.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text3 = frmEditor_Event.txtChoices2.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text4 = frmEditor_Event.txtChoices3.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text5 = frmEditor_Event.txtChoices4.Text
            Case EventType.PlayerVar
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbVariable.SelectedIndex 
                If frmEditor_Event.optVariableAction0.Checked = True Then i = 0
                If frmEditor_Event.optVariableAction1.Checked = True Then i = 1
                If frmEditor_Event.optVariableAction2.Checked = True Then i = 2
                If frmEditor_Event.optVariableAction3.Checked = True Then i = 3
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = i
                If i = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData0.Value
                ElseIf i = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData1.Value
                ElseIf i = 2 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData2.Value
                ElseIf i = 3 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudVariableData3.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudVariableData4.Value
                End If
            Case EventType.PlayerSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSwitch.SelectedIndex 
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbPlayerSwitchSet.SelectedIndex
            Case EventType.SelfSwitch
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSetSelfSwitch.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbSetSelfSwitchTo.SelectedIndex
            Case EventType.ChangeItems
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChangeItemIndex.SelectedIndex 
                If frmEditor_Event.optChangeItemSet.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.optChangeItemAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                ElseIf frmEditor_Event.optChangeItemRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                End If
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudChangeItemsAmount.Value
            Case EventType.ChangeLevel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudChangeLevel.Value
            Case EventType.ChangeSkills
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChangeSkills.SelectedIndex 
                If frmEditor_Event.optChangeSkillsAdd.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.optChangeSkillsRemove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                End If
            Case EventType.ChangeJob
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChangeJob.SelectedIndex 
            Case EventType.ChangeSprite
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudChangeSprite.Value
            Case EventType.ChangeSex
                If frmEditor_Event.optChangeSexMale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 0
                ElseIf frmEditor_Event.optChangeSexFemale.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = 1
                End If
            Case EventType.ChangePk
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSetPK.SelectedIndex

            Case EventType.WarpPlayer
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudWPMap.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudWPX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudWPY.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.cmbWarpPlayerDir.SelectedIndex
            Case EventType.SetMoveRoute
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEditor_Event.cmbEvent.SelectedIndex)
                If frmEditor_Event.chkIgnoreMove.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                End If

                If frmEditor_Event.chkRepeatRoute.Checked = True Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 1
                Else
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = 0
                End If
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRouteCount = TempMoveRouteCount
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).MoveRoute = TempMoveRoute
            Case EventType.PlayAnimation
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbPlayAnim.SelectedIndex 
                If frmEditor_Event.cmbAnimTargetType.SelectedIndex = 0 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 0
                ElseIf frmEditor_Event.cmbAnimTargetType.SelectedIndex = 1 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 1
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.cmbPlayAnimEvent.SelectedIndex 
                ElseIf frmEditor_Event.cmbAnimTargetType.SelectedIndex = 2 Then
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = 2
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudPlayAnimTileX.Value
                    TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudPlayAnimTileY.Value
                End If
            Case EventType.CustomScript
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudCustomScript.Value
            Case EventType.PlayBgm
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = MusicCache(frmEditor_Event.cmbPlayBGM.SelectedIndex )
            Case EventType.PlaySound
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = SoundCache(frmEditor_Event.cmbPlaySound.SelectedIndex )
            Case EventType.OpenShop
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbOpenShop.SelectedIndex 
            Case EventType.SetAccess
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSetAccess.SelectedIndex
            Case EventType.GiveExp
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudGiveExp.Value
            Case EventType.ShowChatBubble
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtChatbubbleText.Text
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbChatBubbleTargetType.SelectedIndex 
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbChatBubbleTarget.SelectedIndex 
            Case EventType.Label
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtLabelName.Text
            Case EventType.GotoLabel
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Text1 = frmEditor_Event.txtGotoLabel.Text
            Case EventType.SpawnNpc
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.cmbSpawnNpc.SelectedIndex 
            Case EventType.SetFog
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudFogData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudFogData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudFogData2.Value
            Case EventType.SetWeather
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.CmbWeather.SelectedIndex
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudWeatherIntensity.Value
            Case EventType.SetTint
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudMapTintData0.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.nudMapTintData1.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudMapTintData2.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudMapTintData3.Value
            Case EventType.Wait
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudWaitAmount.Value
            Case EventType.ShowPicture
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = frmEditor_Event.nudShowPicture.Value

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data2 = frmEditor_Event.cmbPicLoc.SelectedIndex 

                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data3 = frmEditor_Event.nudPicOffsetX.Value
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data4 = frmEditor_Event.nudPicOffsetY.Value
            Case EventType.WaitMovement
                TmpEvent.Pages(CurPageNum).CommandList(curlist).Commands(curslot).Data1 = ListOfEvents(frmEditor_Event.cmbMoveWait.SelectedIndex)
        End Select
        EventListCommands()

    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_SpawnEvent(ByRef data() As Byte)
        Dim id As Integer
        Dim buffer As New ByteStream(data)

        id = buffer.ReadInt32

        If id > CurrentEvents Then
            CurrentEvents = id
            ReDim Preserve MapEvents(CurrentEvents)
        End If

        With MapEvents(id)
            .Name = buffer.ReadString
            .Dir = buffer.ReadInt32
            .ShowDir = .Dir
            .GraphicType = buffer.ReadByte
            .Graphic = buffer.ReadInt32
            .GraphicX = buffer.ReadInt32
            .GraphicX2 = buffer.ReadInt32
            .GraphicY = buffer.ReadInt32
            .GraphicY2 = buffer.ReadInt32
            .MovementSpeed = buffer.ReadInt32
            .Moving = 0
            .X = buffer.ReadInt32
            .Y = buffer.ReadInt32
            .XOffset = 0
            .YOffset = 0
            .Position = buffer.ReadByte
            .Visible = buffer.ReadInt32
            .WalkAnim = buffer.ReadInt32
            .DirFix = buffer.ReadInt32
            .WalkThrough = buffer.ReadInt32
            .ShowName = buffer.ReadInt32
            .QuestNum = buffer.ReadInt32
        End With

        buffer.Dispose()

    End Sub

    Sub Packet_EventMove(ByRef data() As Byte)
        Dim id As Integer
        Dim x As Integer
        Dim y As Integer
        Dim dir As Integer, showDir As Integer
        Dim movementSpeed As Integer
        Dim buffer As New ByteStream(data)

        id = buffer.ReadInt32
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        dir = buffer.ReadInt32
        showDir = buffer.ReadInt32
        movementSpeed = buffer.ReadInt32

        If id > CurrentEvents Then Exit Sub

        With MapEvents(id)
            .X = x
            .Y = y
            .Dir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 1
            .ShowDir = showDir
            .MovementSpeed = movementSpeed

            Select Case dir
                Case DirectionType.Up
                    .YOffset = PicY
                Case DirectionType.Down
                    .YOffset = PicY * -1
                Case DirectionType.Left
                    .XOffset = PicX
                Case DirectionType.Right
                    .XOffset = PicX * -1
            End Select

        End With

    End Sub

    Sub Packet_EventDir(ByRef data() As Byte)
        Dim i As Integer
        Dim dir As Byte
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        dir = buffer.ReadInt32

        If i > CurrentEvents Then Exit Sub

        With MapEvents(i)
            .Dir = dir
            .ShowDir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

    End Sub

    Sub Packet_SwitchesAndVariables(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)

        For i = 1 To MAX_SWITCHES
            Switches(i) = buffer.ReadString
        Next
        For i = 1 To NAX_VARIABLES
            Variables(i) = buffer.ReadString
        Next

        buffer.Dispose()

    End Sub

    Sub Packet_MapEventData(ByRef data() As Byte)
        Dim i As Integer, x As Integer, y As Integer, z As Integer, w As Integer
        Dim buffer As New ByteStream(data)

        MyMap.EventCount = buffer.ReadInt32

        If MyMap.EventCount > 0 Then
            ReDim MyMap.Event(MyMap.EventCount)
            For i = 0 To MyMap.EventCount
                With MyMap.Event(i)
                    .Name = buffer.ReadString
                    .Globals = buffer.ReadByte
                    .X = buffer.ReadInt32
                    .Y = buffer.ReadInt32
                    .PageCount = buffer.ReadInt32
                End With

                If MyMap.Event(i).PageCount > 0 Then
                    ReDim MyMap.Event(i).Pages(MyMap.Event(i).PageCount)
                    For x = 0 To MyMap.Event(i).PageCount
                        With MyMap.Event(i).Pages(x)
                            .ChkVariable = buffer.ReadInt32
                            .VariableIndex = buffer.ReadInt32
                            .VariableCondition = buffer.ReadInt32
                            .VariableCompare = buffer.ReadInt32
                            .ChkSwitch = buffer.ReadInt32
                            .SwitchIndex = buffer.ReadInt32
                            .SwitchCompare = buffer.ReadInt32
                            .ChkHasItem = buffer.ReadInt32
                            .HasItemIndex = buffer.ReadInt32
                            .HasItemAmount = buffer.ReadInt32
                            .ChkSelfSwitch = buffer.ReadInt32
                            .SelfSwitchIndex = buffer.ReadInt32
                            .SelfSwitchCompare = buffer.ReadInt32
                            .GraphicType = buffer.ReadByte
                            .Graphic = buffer.ReadInt32
                            .GraphicX = buffer.ReadInt32
                            .GraphicY = buffer.ReadInt32
                            .GraphicX2 = buffer.ReadInt32
                            .GraphicY2 = buffer.ReadInt32

                            .MoveType = buffer.ReadByte
                            .MoveSpeed = buffer.ReadByte
                            .MoveFreq = buffer.ReadByte
                            .MoveRouteCount = buffer.ReadInt32
                            .IgnoreMoveRoute = buffer.ReadInt32
                            .RepeatMoveRoute = buffer.ReadInt32

                            If .MoveRouteCount > 0 Then
                                ReDim MyMap.Event(i).Pages(x).MoveRoute(.MoveRouteCount)
                                For y = 0 To .MoveRouteCount
                                    .MoveRoute(y).Index = buffer.ReadInt32
                                    .MoveRoute(y).Data1 = buffer.ReadInt32
                                    .MoveRoute(y).Data2 = buffer.ReadInt32
                                    .MoveRoute(y).Data3 = buffer.ReadInt32
                                    .MoveRoute(y).Data4 = buffer.ReadInt32
                                    .MoveRoute(y).Data5 = buffer.ReadInt32
                                    .MoveRoute(y).Data6 = buffer.ReadInt32
                                Next
                            End If

                            .WalkAnim = buffer.ReadInt32
                            .DirFix = buffer.ReadInt32
                            .WalkThrough = buffer.ReadInt32
                            .ShowName = buffer.ReadInt32
                            .Trigger = buffer.ReadByte
                            .CommandListCount = buffer.ReadInt32
                            .Position = buffer.ReadByte
                            .QuestNum = buffer.ReadInt32
                        End With

                        If MyMap.Event(i).Pages(x).CommandListCount > 0 Then
                            ReDim MyMap.Event(i).Pages(x).CommandList(MyMap.Event(i).Pages(x).CommandListCount)
                            For y = 0 To MyMap.Event(i).Pages(x).CommandListCount
                                MyMap.Event(i).Pages(x).CommandList(y).CommandCount = buffer.ReadInt32
                                MyMap.Event(i).Pages(x).CommandList(y).ParentList = buffer.ReadInt32
                                If MyMap.Event(i).Pages(x).CommandList(y).CommandCount > 0 Then
                                    ReDim MyMap.Event(i).Pages(x).CommandList(y).Commands(MyMap.Event(i).Pages(x).CommandList(y).CommandCount)
                                    For z = 0 To MyMap.Event(i).Pages(x).CommandList(y).CommandCount
                                        With MyMap.Event(i).Pages(x).CommandList(y).Commands(z)
                                            .Index = buffer.ReadByte
                                            .Text1 = buffer.ReadString
                                            .Text2 = buffer.ReadString
                                            .Text3 = buffer.ReadString
                                            .Text4 = buffer.ReadString
                                            .Text5 = buffer.ReadString
                                            .Data1 = buffer.ReadInt32
                                            .Data2 = buffer.ReadInt32
                                            .Data3 = buffer.ReadInt32
                                            .Data4 = buffer.ReadInt32
                                            .Data5 = buffer.ReadInt32
                                            .Data6 = buffer.ReadInt32
                                            .ConditionalBranch.CommandList = buffer.ReadInt32
                                            .ConditionalBranch.Condition = buffer.ReadInt32
                                            .ConditionalBranch.Data1 = buffer.ReadInt32
                                            .ConditionalBranch.Data2 = buffer.ReadInt32
                                            .ConditionalBranch.Data3 = buffer.ReadInt32
                                            .ConditionalBranch.ElseCommandList = buffer.ReadInt32
                                            .MoveRouteCount = buffer.ReadInt32

                                            If .MoveRouteCount > 0 Then
                                                ReDim .MoveRoute(.MoveRouteCount)
                                                For w = 0 To .MoveRouteCount
                                                    .MoveRoute(w).Index = buffer.ReadInt32
                                                    .MoveRoute(w).Data1 = buffer.ReadInt32
                                                    .MoveRoute(w).Data2 = buffer.ReadInt32
                                                    .MoveRoute(w).Data3 = buffer.ReadInt32
                                                    .MoveRoute(w).Data4 = buffer.ReadInt32
                                                    .MoveRoute(w).Data5 = buffer.ReadInt32
                                                    .MoveRoute(w).Data6 = buffer.ReadInt32
                                                Next
                                            End If
                                        End With
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If

        buffer.Dispose()

    End Sub

    Sub Packet_EventChat(ByRef data() As Byte)
        Dim i As Integer
        Dim choices As Integer
        Dim buffer As New ByteStream(data)
        EventReplyId = buffer.ReadInt32
        EventReplyPage = buffer.ReadInt32
        EventChatFace = buffer.ReadInt32
        EventText = buffer.ReadString
        If EventText = "" Then EventText = " "
        EventChat = True
        ShowEventLbl = True
        choices = buffer.ReadInt32
        InEvent = True
        For i = 1 To 4
            EventChoices(i) = ""
            EventChoiceVisible(i) = False
        Next
        EventChatType = 0
        If choices = 0 Then
        Else
            EventChatType = 1
            For i = 0 To choices
                EventChoices(i) = buffer.ReadString
                EventChoiceVisible(i) = True
            Next
        End If
        AnotherChat = buffer.ReadInt32

        buffer.Dispose()

    End Sub

    Sub Packet_EventStart(ByRef data() As Byte)
        InEvent = True
    End Sub

    Sub Packet_EventEnd(ByRef data() As Byte)
        InEvent = False
    End Sub

    Sub Packet_Picture(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        Dim picIndex As Integer, spriteType As Integer, xOffset As Integer, yOffset As Integer, eventid As Integer

        eventid = buffer.ReadInt32
        picIndex = buffer.ReadByte

        If picIndex = 0 Then
            Picture.Index = 0
            Picture.EventId = 0
            Picture.SpriteType = 0
            Picture.xOffset = 0
            Picture.yOffset = 0
            Exit Sub
        End If

        spriteType = buffer.ReadByte
        xOffset = buffer.ReadByte
        yOffset = buffer.ReadByte

        Picture.Index = picIndex
        Picture.EventId = eventid
        Picture.SpriteType = spriteType
        Picture.xOffset = xOffset
        Picture.yOffset = yOffset

        buffer.Dispose()

    End Sub

    Sub Packet_HidePicture(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)

        Picture = Nothing
    End Sub

    Sub Packet_HoldPlayer(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        If buffer.ReadInt32 = 0 Then
            HoldPlayer = True
        Else
            HoldPlayer = False
        End If

        buffer.Dispose()

    End Sub

    Sub Packet_PlayBGM(ByRef data() As Byte)
        Dim music As String
        Dim buffer As New ByteStream(data)

        music = buffer.ReadString
        MyMap.Music = music

        buffer.Dispose()
    End Sub

    Sub Packet_FadeOutBGM(ByRef data() As Byte)
        CurrentMusic = ""
        FadeOutSwitch = True
    End Sub

    Sub Packet_PlaySound(ByRef data() As Byte)
        Dim sound As String
        Dim buffer As New ByteStream(data)
        Dim x As Integer, y As Integer

        sound = buffer.ReadString
        x = buffer.ReadInt32
        y = buffer.ReadInt32

        PlaySound(sound, x, y)

        buffer.Dispose()
    End Sub

    Sub Packet_StopSound(ByRef data() As Byte)
        StopSound()
    End Sub

    Sub Packet_SpecialEffect(ByRef data() As Byte)
        Dim effectType As Integer
        Dim buffer As New ByteStream(data)
        effectType = buffer.ReadInt32

        Select Case effectType
            Case EffectTypeFadein
                UseFade = True
                FadeType = 1
                FadeAmount = 0
            Case EffectTypeFadeout
                UseFade = True
                FadeType = 0
                FadeAmount = 255
            Case EffectTypeFlash
                FlashTimer = GetTickCount() + 150
            Case EffectTypeFog
                CurrentFog = buffer.ReadInt32
                CurrentFogSpeed = buffer.ReadInt32
                CurrentFogOpacity = buffer.ReadInt32
            Case EffectTypeWeather
                CurrentWeather = buffer.ReadInt32
                CurrentWeatherIntensity = buffer.ReadInt32
            Case EffectTypeTint
                MyMap.MapTint = 1
                CurrentTintR = buffer.ReadInt32
                CurrentTintG = buffer.ReadInt32
                CurrentTintB = buffer.ReadInt32
                CurrentTintA = buffer.ReadInt32
        End Select

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"

    Sub RequestSwitchesAndVariables()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestSwitchesAndVariables)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendSwitchesAndVariables()
        Dim i As Integer
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSwitchesAndVariables)

        For i = 1 To MAX_SWITCHES
            buffer.WriteString((Switches(i)))
        Next
        For i = 1 To NAX_VARIABLES
            buffer.WriteString((Variables(i)))
        Next

        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

#End Region

#Region "Misc"

    Sub ProcessEventMovement(id As Integer)
        If MyEditorType = EditorType.Map Then Exit Sub
        If id > MyMap.EventCount Then Exit Sub
        If id > MapEvents.Length Then Exit Sub

       If MapEvents(id).Moving = 1 Then
            Select Case MapEvents(id).Dir
                Case DirectionType.Up
                    MapEvents(id).YOffset = MapEvents(id).YOffset - ((ElapsedTime / 1000) * (MapEvents(id).MovementSpeed * SizeY))
                   If MapEvents(id).YOffset < 0 Then MapEvents(id).YOffset = 0
                Case DirectionType.Down
                    MapEvents(id).YOffset = MapEvents(id).YOffset + ((ElapsedTime / 1000) * (MapEvents(id).MovementSpeed * SizeY))
                   If MapEvents(id).YOffset > 0 Then MapEvents(id).YOffset = 0
                Case DirectionType.Left
                    MapEvents(id).XOffset = MapEvents(id).XOffset - ((ElapsedTime / 1000) * (MapEvents(id).MovementSpeed * SizeX))
                   If MapEvents(id).XOffset < 0 Then MapEvents(id).XOffset = 0
                Case DirectionType.Right
                    MapEvents(id).XOffset = MapEvents(id).XOffset + ((ElapsedTime / 1000) * (MapEvents(id).MovementSpeed * SizeX))
                   If MapEvents(id).XOffset > 0 Then MapEvents(id).XOffset = 0
            End Select

            ' Check if completed walking over to the next tile
           If MapEvents(id).Moving > 0 Then
               If MapEvents(id).Dir = DirectionType.Right Or MapEvents(id).Dir = DirectionType.Down Then
                    If (MapEvents(id).XOffset >= 0) And (MapEvents(id).YOffset >= 0) Then
                        MapEvents(id).Moving = 0
                       If MapEvents(id).Steps = 1 Then
                            MapEvents(id).Steps = 3
                        Else
                            MapEvents(id).Steps = 1
                        End If
                    End If
                Else
                    If (MapEvents(id).XOffset <= 0) And (MapEvents(id).YOffset <= 0) Then
                        MapEvents(id).Moving = 0
                       If MapEvents(id).Steps = 1 Then
                            MapEvents(id).Steps = 3
                        Else
                            MapEvents(id).Steps = 1
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Friend Function GetColorString(color As Integer)

        Select Case color
            Case 0
                GetColorString = "Black"
            Case 1
                GetColorString = "Blue"
            Case 2
                GetColorString = "Green"
            Case 3
                GetColorString = "Cyan"
            Case 4
                GetColorString = "Red"
            Case 5
                GetColorString = "Magenta"
            Case 6
                GetColorString = "Brown"
            Case 7
                GetColorString = "Grey"
            Case 8
                GetColorString = "Dark Grey"
            Case 9
                GetColorString = "Bright Blue"
            Case 10
                GetColorString = "Bright Green"
            Case 11
                GetColorString = "Bright Cyan"
            Case 12
                GetColorString = "Bright Red"
            Case 13
                GetColorString = "Pink"
            Case 14
                GetColorString = "Yellow"
            Case 15
                GetColorString = "White"
            Case Else
                GetColorString = "Black"
        End Select

    End Function

    Sub ClearEventChat()
        Dim i As Integer

        If AnotherChat = 1 Then
            For i = 1 To 4
                EventChoiceVisible(i) = False
            Next
            EventText = ""
            EventChatType = 1
            EventChatTimer = GetTickCount() + 100
        ElseIf AnotherChat = 2 Then
            For i = 1 To 4
                EventChoiceVisible(i) = False
            Next
            EventText = ""
            EventChatType = 1
            EventChatTimer = GetTickCount() + 100
        Else
            EventChat = False
        End If
    End Sub

#End Region

End Module