Imports Core
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Module Text
    Friend Const MaxChatDisplayLines As Byte = 11
    Friend Const ChatLineSpacing As Byte = FontSize ' Should be same height as font
    Friend Const MyChatTextLimit As Integer = 40
    Friend Const MyAmountValueLimit As Integer = 3
    Friend Const AllChatLineWidth As Integer = 40
    Friend Const ChatboxPadding As Integer = 45 + 16 + 2 ' 10 = left and right border padding +2 each (3+2+3+2), 16 = scrollbar width, +2 for padding between scrollbar and text
    Friend Const ChatEntryPadding As Integer = 10 ' 5 on left and right
    Friend FirstLineindex As Integer = 0
    Friend LastLineindex As Integer = 0
    Friend ScrollMod As Integer = 0

    Function CensorText(input As String) As String
        Return New String("*"c, input.Length)
    End Function

    ' Declare and load the font
    Public FontTester As SpriteFont

    ' Function to get the width of the given text
    Friend Function TextWidth(text As String, Optional textSize As Single = 1.0F) As Integer
        If FontTester Is Nothing Then Exit Function
        Dim textDimensions = FontTester.MeasureString(text)
        Return CInt(textDimensions.X * textSize)
    End Function

    ' Function to get the height of the given text
    Friend Function GetTextHeight(text As String, Optional textSize As Single = 1.0F) As Integer
        If FontTester Is Nothing Then Exit Function
        Dim textDimensions = FontTester.MeasureString(text)
        Return CInt(textDimensions.Y * textSize)
    End Function

    Public Sub AddText(ByVal text As String, ByVal Color As Integer, Optional ByVal alpha As Long = 255, Optional channel As Byte = 1)
        Dim i As Long

        Chat_HighIndex = 0

        ' Move the rest of it up
        For i = (CHAT_LINES - 1) To 1 Step -1
            If Len(Chat(i).Text) > 0 Then
                If i > Chat_HighIndex Then Chat_HighIndex = i + 1
            End If
            Chat(i + 1) = Chat(i)
        Next

        Chat(1).Text = text
        Chat(1).Color = Color
        Chat(1).Visible = True
        Chat(1).Timer = GetTickCount()
        Chat(1).Channel = channel
    End Sub

    Friend SplitChars As Char() = New Char() {" "c, "-"c, ControlChars.Tab}

    Public Sub WordWrap(ByVal text As String, ByVal MaxLineLen As Long, ByRef theArray() As String)
        Dim lineCount As Long, i As Long, size As Long, lastSpace As Long, b As Long, tmpNum As Long

        'Too small of text
        If Len(text) < 2 Then
            ReDim theArray(1)
            theArray(1) = text
            Exit Sub
        End If

        ' default values
        b = 1
        lastSpace = 1
        size = 0
        tmpNum = Len(text)

        For i = 1 To tmpNum

            ' if it's a space, store it
            Select Case Mid$(text, i, 1)
                Case " " : lastSpace = i
            End Select

            'Add up the size
            size = size + FontSize

            'Check for too large of a size
            If size > MaxLineLen Then
                'Check if the last space was too far back
                If i - lastSpace > FontSize Then
                    'Too far away to the last space, so break at the last character
                    lineCount = lineCount + 1
                    ReDim Preserve theArray(lineCount)
                    theArray(lineCount) = Mid$(text, b, (i - 1) - b)
                    b = i - 1
                    size = 0
                Else
                    'Break at the last space to preserve the word
                    lineCount = lineCount + 1
                    ReDim Preserve theArray(lineCount)

                    ' Ensure b is within valid range
                    If b < 1 Then b = 1
                    If b > text.Length Then b = text.Length

                    ' Ensure the length parameter is not negative
                    Dim substringLength As Integer = lastSpace - b
                    If substringLength < 0 Then substringLength = 0

                    ' Extract the substring and assign it to the array
                    theArray(lineCount) = Mid$(text, b, substringLength)

                    b = lastSpace + 1
                    'Count all the words we ignored (the ones that weren't printed, but are before "i")
                    size = TextWidth(Mid$(text, lastSpace, i - lastSpace))
                End If
            End If

            ' Remainder
            If i = Len(text) Then
                If b <> i Then
                    lineCount = lineCount + 1
                    ReDim Preserve theArray(lineCount)
                    theArray(lineCount) = theArray(lineCount) & Mid$(text, b, i)
                End If
            End If
        Next
    End Sub

    Public Function WordWrap(ByVal text As String, ByVal MaxLineLen As Integer, Optional ByRef lineCount As Long = 0) As String
        Dim TempSplit() As String, TSLoop As Long, lastSpace As Long, size As Long, i As Long, b As Long, tmpNum As Long, skipCount As Long

        'Too small of text
        If Len(text) < 2 Then
            WordWrap = text
            Exit Function
        End If

        'Check if there are any line breaks - if so, we will support them
        TempSplit = Split(text, vbNewLine)
        tmpNum = UBound(TempSplit)

        For TSLoop = 0 To tmpNum
            'Clear the values for the new line
            size = 0
            b = 1
            lastSpace = 1

            'Add back in the vbNewLines
            If TSLoop < UBound(TempSplit) Then TempSplit(TSLoop) = TempSplit(TSLoop) & vbNewLine

            'Only check lines with a space
            If InStr(1, TempSplit(TSLoop), " ") Then
                'Loop through all the characters
                tmpNum = Len(TempSplit(TSLoop))

                For i = 1 To tmpNum
                    'If it is a space, store it so we can easily break at it
                    Select Case Mid$(TempSplit(TSLoop), i, 1)
                        Case " "
                            lastSpace = i
                    End Select

                    If skipCount > 0 Then
                        skipCount = skipCount - 1
                    ElseIf TSLoop > 0 Then
                        'Add up the size
                        size = size + TextWidth(TempSplit(TSLoop))

                        'Check for too large of a size
                        If size > MaxLineLen Then
                            'Check if the last space was too far back
                            If i - lastSpace > 12 Then
                                'Too far away to the last space, so break at the last character
                                WordWrap = WordWrap & Mid$(TempSplit(TSLoop), b, (i - 1) - b) & vbNewLine
                                lineCount = lineCount + 1
                                b = i - 1
                                size = 0
                            Else
                                'Break at the last space to preserve the word
                                WordWrap = WordWrap & Mid$(TempSplit(TSLoop), b, lastSpace - b) & vbNewLine
                                lineCount = lineCount + 1
                                b = lastSpace + 1

                                'Count all the words we ignored (the ones that weren't printed, but are before "i")
                                size = TextWidth(Mid$(TempSplit(TSLoop), lastSpace, i - lastSpace))
                            End If
                        End If

                        'This handles the remainder
                        If i = Len(TempSplit(TSLoop)) Then
                            If b <> i Then
                                WordWrap = WordWrap & Mid$(TempSplit(TSLoop), b, i)
                                lineCount = lineCount + 1
                            End If
                        End If
                    End If
                Next i
            Else
                WordWrap = WordWrap & TempSplit(TSLoop)
            End If
        Next TSLoop
    End Function

    Friend Function Explode(str As String, splitChars As Char()) As String()

        Dim parts As New List(Of String)()
        Dim startindex As Integer = 0
        Explode = Nothing

        If str = Nothing Then Exit Function

        While True
            Dim index As Integer = str.IndexOfAny(splitChars, startindex)

            If index = -1 Then
                parts.Add(str.Substring(startindex))
                Return parts.ToArray()
            End If

            Dim word As String = str.Substring(startindex, index - startindex)
            Dim nextChar As Char = str.Substring(index, 1)(0)
            ' Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
            If Char.IsWhiteSpace(nextChar) Then
                parts.Add(word)
                parts.Add(nextChar.ToString())
            Else
                parts.Add(word + nextChar)
            End If

            startindex = index + 1
        End While

    End Function

End Module