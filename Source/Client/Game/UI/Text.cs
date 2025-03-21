using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;
using System.Text;
using static Core.Global.Command;
using Color = Microsoft.Xna.Framework.Color;
using Path = Core.Path;

namespace Client
{

    static class Text
    {
        public static Dictionary<Core.Enum.FontType, SpriteFont> Fonts = new Dictionary<Core.Enum.FontType, SpriteFont>();

        internal const byte MaxChatDisplayLines = 11;
        internal const byte ChatLineSpacing = 10; // Should be same height as font
        internal const int MyChatTextLimit = 40;
        internal const int MyAmountValueLimit = 3;
        internal const int AllChatLineWidth = 40;
        internal const int ChatboxPadding = 45 + 16 + 2; // 10 = left and right border padding +2 each (3+2+3+2), 16 = scrollbar width, +2 for padding between scrollbar and text
        internal const int ChatEntryPadding = 10; // 5 on left and right
        public static int FirstLineindex = 0;
        public static int LastLineindex = 0;
        public static int ScrollMod = 0;

        public static string CensorText(string input)
        {
            return new string('*', input.Length);
        }

        public static string SanitizeText(string text, SpriteFont font)
        {
            if (text == null) return "";

            var sanitizedText = new StringBuilder();
            foreach (char ch in text)
            {
                if (font.Characters.Contains(ch))
                {
                    sanitizedText.Append(ch);
                } // Replace unsupported characters with a placeholder
            }
            return sanitizedText.ToString();
        }

        // Get the width of the text with optional scaling
        public static int GetTextWidth(string text, Core.Enum.FontType font = Core.Enum.FontType.Georgia, float textSize = 1.0f)
        {
            if (!Fonts.ContainsKey(font))
                throw new ArgumentException("Font not found.");
            string sanitizedText = SanitizeText(text, Fonts[font]);
            var textDimensions = Fonts[font].MeasureString(sanitizedText);
            return (int)Math.Round(textDimensions.X * textSize);
        }

        // Get the height of the text with optional scaling
        public static int TextHeight(string text, Core.Enum.FontType font = Core.Enum.FontType.Georgia, float textSize = 1.0f)
        {
            if (!Fonts.ContainsKey(font))
                throw new ArgumentException("Font not found.");
            var textDimensions = Fonts[font].MeasureString(text);
            return (int)Math.Round(textDimensions.Y * textSize);
        }

        public static void AddText(string text, int color, long alpha = 255L, byte channel = 0)
        {
            GameState.Chat_HighIndex += 1;

            if (GameState.Chat_HighIndex > Constant.CHAT_LINES)
                GameState.Chat_HighIndex = Constant.CHAT_LINES;

            // Move the rest of the chat lines up
            for (int i = (int)GameState.Chat_HighIndex - 1; i > 0; i--)
            {
                Core.Type.Chat[i] = Core.Type.Chat[i - 1];
            }

            // Add the new text
            Core.Type.Chat[0].Text = text;
            Core.Type.Chat[0].Color = color;
            Core.Type.Chat[0].Visible = true;
            Core.Type.Chat[0].Timer = General.GetTickCount();
            Core.Type.Chat[0].Channel = (byte)(channel);
        }

        public static void WordWrap(string text, Core.Enum.FontType font, long MaxLineLen, ref string[] theArray)
        {
            var lineCount = default(long);
            long i;
            long size;
            long lastSpace;
            long b;
            long tmpNum;

            // Too small of text
            if (Strings.Len(text) < 2)
            {
                theArray = new string[2];
                theArray[1] = text;
                return;
            }

            // default values
            b = 1L;
            lastSpace = 1L;
            size = 0L;
            tmpNum = Strings.Len(text);

            var loopTo = tmpNum;
            for (i = 1L; i <= loopTo; i++)
            {
                // if it's a space, store it
                switch (Strings.Mid(text, (int)i, 1) ?? "")
                {
                    case " ":
                        {
                            lastSpace = i;
                            break;
                        }
                }

                // Add up the size
                size = size + 10L;

                // Check for too large of a size
                if (size > MaxLineLen)
                {
                    // Check if the last space was too far back
                    if (i - lastSpace > 10L)
                    {
                        // Too far away to the last space, so break at the last character
                        lineCount = lineCount + 1L;
                        Array.Resize(ref theArray, (int)(lineCount + 1));
                        theArray[(int)lineCount - 1] = Strings.Mid(text, (int)b, (int)(i - 1L - b));
                        b = i - 1L;
                        size = 0L;
                    }
                    else
                    {
                        // Break at the last space to preserve the word
                        lineCount = lineCount + 1L;
                        Array.Resize(ref theArray, (int)(lineCount + 1));

                        // Ensure b is within valid range
                        if (b < 0L)
                            b = 0L;

                        if (b > text.Length)
                            b = text.Length;

                        // Ensure the length parameter is not negative
                        int substringLength = (int)(lastSpace - b);
                        if (substringLength < 0)
                            substringLength = 0;

                        // Extract the substring and assign it to the array
                        theArray[(int)lineCount - 1] = Strings.Mid(text, (int)b, substringLength);

                        b = lastSpace + 1L;
                        // Count all the words we ignored (the ones that weren't printed, but are before "i")
                        size = GetTextWidth(Strings.Mid(text, (int)lastSpace, (int)(i - lastSpace)), font);
                    }
                }

                // Remainder
                if (i == Strings.Len(text))
                {
                    if (b != i)
                    {
                        lineCount = lineCount + 1L;
                        Array.Resize(ref theArray, (int)(lineCount + 1));
                        theArray[(int)lineCount - 1] = Strings.Mid(text, (int)b, (int)i);
                    }
                }
            }
        }

        public static string[] Explode(string str, char[] splitChars)
        {
            string[] ExplodeRet = default;
            var parts = new List<string>();
            int startindex = 0;

            ExplodeRet = null;

            if (string.IsNullOrEmpty(str))
                return ExplodeRet;

            while (true)
            {
                int index = str.IndexOfAny(splitChars, startindex);

                if (index == -1)
                {
                    parts.Add(str.Substring(startindex));
                    return parts.ToArray();
                }

                string word = str.Substring(startindex, index - startindex);
                char nextChar = str.Substring(index, 1)[0];
                // Dashes and the likes should stick to the word occuring before it. Whitespace doesn't have to.
                if (char.IsWhiteSpace(nextChar))
                {
                    parts.Add(word);
                    parts.Add(nextChar.ToString());
                }
                else
                {
                    parts.Add(word + Conversions.ToString(nextChar));
                }

                startindex = index + 1;
            }

        }

        public static void RenderText(string text, int x, int y, Color frontColor, Color backColor, Core.Enum.FontType font = Core.Enum.FontType.Georgia)
        {
            if (text == null) return;
            string sanitizedText = new string(text.Where(c => Fonts[font].Characters.Contains(c)).ToArray());
            GameClient.SpriteBatch.DrawString(Fonts[font], sanitizedText, new Vector2(x + 1, y + 1), backColor, 0.0f, Vector2.Zero, 12f / 16.0f, SpriteEffects.None, 0.0f);
            GameClient.SpriteBatch.DrawString(Fonts[font], sanitizedText, new Vector2(x, y), frontColor, 0.0f, Vector2.Zero, 12f / 16.0f, SpriteEffects.None, 0.0f);
        }

        public static void DrawMapAttributes()
        {
            int X;
            int y;
            int tX;
            int tY;
            int tA;

            var loopTo = (int)Math.Round(GameState.TileView.Right + 1d);
            for (X = (int)Math.Round(GameState.TileView.Left - 1d); X < loopTo; X++)
            {
                var loopTo1 = (int)Math.Round(GameState.TileView.Bottom + 1d);
                for (y = (int)Math.Round(GameState.TileView.Top - 1d); y < loopTo1; y++)
                {
                    if (GameLogic.IsValidMapPoint(X, y))
                    {
                        {
                            ref var withBlock = ref Core.Type.MyMap.Tile[X, y];
                            tX = (int)Math.Round(GameLogic.ConvertMapX(X * GameState.PicX) - 4 + GameState.PicX * 0.5d);
                            tY = (int)Math.Round(GameLogic.ConvertMapY(y * GameState.PicY) - 7 + GameState.PicY * 0.5d);

                            if (GameState.EditorAttribute == 1)
                            {
                                tA = (int)withBlock.Type;
                            }
                            else
                            {
                                tA = (int)withBlock.Type2;
                            }

                            switch (tA)
                            {
                                case (int)Core.Enum.TileType.Blocked:
                                    {
                                        RenderText("B", tX, tY, Color.Red, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Warp:
                                    {
                                        RenderText("W", tX, tY, Color.Blue, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Item:
                                    {
                                        RenderText("I", tX, tY, Color.White, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.NPCAvoid:
                                    {
                                        RenderText("N", tX, tY, Color.White, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Resource:
                                    {
                                        RenderText("R", tX, tY, Color.Green, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.NPCSpawn:
                                    {
                                        RenderText("S", tX, tY, Color.Yellow, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Shop:
                                    {
                                        RenderText("S", tX, tY, Color.Blue, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Bank:
                                    {
                                        RenderText("B", tX, tY, Color.Blue, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Heal:
                                    {
                                        RenderText("H", tX, tY, Color.Green, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Trap:
                                    {
                                        RenderText("T", tX, tY, Color.Red, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.Animation:
                                    {
                                        RenderText("A", tX, tY, Color.Red, Color.Black);
                                        break;
                                    }
                                case (int)Core.Enum.TileType.NoXing:
                                    {
                                        RenderText("X", tX, tY, Color.Red, Color.Black);
                                        break;
                                    }
                            }
                        }
                    }
                }
            }

        }

        public static void DrawNPCName(double MapNPCNum)
        {
            int textX;
            int textY;
            var color = default(Color);
            var backColor = default(Color);
            double NPCNum;

            NPCNum = (int)Core.Type.MyMapNPC[(int)MapNPCNum].Num;

            if (NPCNum < 0 | NPCNum > Core.Constant.MAX_NPCS)
                return;

            switch (Core.Type.NPC[(int)NPCNum].Behaviour)
            {
                case 0: // attack on sight
                    {
                        color = Color.Red;
                        backColor = Color.Black;
                        break;
                    }
                case 1:
                case 4: // attack when attacked + guard
                    {
                        color = Color.Green;
                        backColor = Color.Black;
                        break;
                    }
                case 2:
                case 3:
                case 5: // friendly + shopkeeper + quest
                    {
                        color = Color.Yellow;
                        backColor = Color.Black;
                        break;
                    }
            }
            textX = GameLogic.ConvertMapX(Core.Type.MyMapNPC[(int)MapNPCNum].X * GameState.PicX) + Core.Type.MyMapNPC[(int)MapNPCNum].XOffset + GameState.PicX / 2 - 6;
            textX -= (int)(GetTextWidth(Core.Type.NPC[(int)NPCNum].Name) / 6d);

            if (Core.Type.NPC[(int)NPCNum].Sprite < 1 | Core.Type.NPC[(int)NPCNum].Sprite > GameState.NumCharacters)
            {
                textY = GameLogic.ConvertMapY(Core.Type.MyMapNPC[(int)MapNPCNum].Y * GameState.PicY) + Core.Type.MyMapNPC[(int)MapNPCNum].YOffset - 16;
            }
            else
            {
                textY = (int)Math.Round(GameLogic.ConvertMapY(Core.Type.MyMapNPC[(int)MapNPCNum].Y * GameState.PicY) + Core.Type.MyMapNPC[(int)MapNPCNum].YOffset - GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, Core.Type.NPC[(int)NPCNum].Sprite.ToString())).Height / 4d + 16d);
            }

            // Draw name
            RenderText(Core.Type.NPC[(int)NPCNum].Name, textX, textY, color, backColor);
        }

        public static void DrawEventName(int index)
        {
            int textX;
            var textY = default(int);
            Color color;
            Color backcolor;
            string name;

            color = Color.Yellow;
            backcolor = Color.Black;

            name = Core.Type.MapEvents[index].Name;

            // calc pos
            textX = GameLogic.ConvertMapX(Core.Type.MapEvents[index].X * GameState.PicX) + Core.Type.MapEvents[index].XOffset + GameState.PicX / 2 - 6;
            textX -= GetTextWidth(name) / 6;

            if (Core.Type.MapEvents[index].GraphicType == 0)
            {
                textY = GameLogic.ConvertMapY(Core.Type.MapEvents[index].Y * GameState.PicY) + Core.Type.MapEvents[index].YOffset - 16;
            }
            else if (Core.Type.MapEvents[index].GraphicType == 1)
            {
                if (Core.Type.MapEvents[index].Graphic < 1 | Core.Type.MapEvents[index].Graphic > GameState.NumCharacters)
                {
                    textY = GameLogic.ConvertMapY(Core.Type.MapEvents[index].Y * GameState.PicY) + Core.Type.MapEvents[index].YOffset - 16;
                }
                else
                {
                    // Determine location for text
                    textY = GameLogic.ConvertMapY(Core.Type.MapEvents[index].Y * GameState.PicY) + Core.Type.MapEvents[index].YOffset - GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, Core.Type.MapEvents[index].Graphic.ToString())).Height / 4 + 16;
                }
            }
            else if (Core.Type.MapEvents[index].GraphicType == 2)
            {
                if (Core.Type.MapEvents[index].GraphicY2 > 0)
                {
                    textX = textX + Core.Type.MapEvents[index].GraphicY2 * GameState.PicY / 2 - 16;
                    textY = GameLogic.ConvertMapY(Core.Type.MapEvents[index].Y * GameState.PicY) + Core.Type.MapEvents[index].YOffset - Core.Type.MapEvents[index].GraphicY2 * GameState.PicY + 16;
                }
                else
                {
                    textY = GameLogic.ConvertMapY(Core.Type.MapEvents[index].Y * GameState.PicY) + Core.Type.MapEvents[index].YOffset - 32 + 16;
                }
            }

            // Draw name
            RenderText(name, textX, textY, color, backcolor);
        }

        public static void DrawActionMsg(int index)
        {
            var x = default(int);
            var y = default(int);
            int i;
            var time = default(int);

            // how long we want each message to appear
            switch (Core.Type.ActionMsg[index].Type)
            {
                case (int)Core.Enum.ActionMsgType.Static:
                    {
                        time = 1500;

                        if (Core.Type.ActionMsg[index].Y > 0)
                        {
                            x = Core.Type.ActionMsg[index].X + Conversion.Int(GameState.PicX / 2) - Strings.Len(Core.Type.ActionMsg[index].Message) / 2 * 8;
                            y = Core.Type.ActionMsg[index].Y - Conversion.Int(GameState.PicY / 2) - 2;
                        }
                        else
                        {
                            x = Core.Type.ActionMsg[index].X + Conversion.Int(GameState.PicX / 2) - Strings.Len(Core.Type.ActionMsg[index].Message) / 2 * 8;
                            y = Core.Type.ActionMsg[index].Y - Conversion.Int(GameState.PicY / 2) + 18;
                        }

                        break;
                    }

                case (int)Core.Enum.ActionMsgType.Scroll:
                    {
                        time = 1500;

                        if (Core.Type.ActionMsg[index].Y > 0)
                        {
                            x = Core.Type.ActionMsg[index].X + Conversion.Int(GameState.PicX / 2) - Strings.Len(Core.Type.ActionMsg[index].Message) / 2 * 8;
                            y = (int)Math.Round(Core.Type.ActionMsg[index].Y - Conversion.Int(GameState.PicY / 2) - 2 - Core.Type.ActionMsg[index].Scroll * 0.6d);
                            Core.Type.ActionMsg[index].Scroll = Core.Type.ActionMsg[index].Scroll + 1;
                        }
                        else
                        {
                            x = Core.Type.ActionMsg[index].X + Conversion.Int(GameState.PicX / 2) - Strings.Len(Core.Type.ActionMsg[index].Message) / 2 * 8;
                            y = (int)Math.Round(Core.Type.ActionMsg[index].Y - Conversion.Int(GameState.PicY / 2) + 18 + Core.Type.ActionMsg[index].Scroll * 0.6d);
                            Core.Type.ActionMsg[index].Scroll = Core.Type.ActionMsg[index].Scroll + 1;
                        }

                        break;
                    }

                case (int)Core.Enum.ActionMsgType.Screen:
                    {
                        time = 3000;

                        // This will kill any action screen messages that there in the system
                        for (i = byte.MaxValue; i >= 0; i -= 1)
                        {
                            if (Core.Type.ActionMsg[i].Type == (int)Core.Enum.ActionMsgType.Screen)
                            {
                                if (i != index)
                                {
                                    GameLogic.ClearActionMsg((byte)index);
                                    index = i;
                                }
                            }
                        }
                        x = GameState.ResolutionWidth / 2 - Strings.Len(Core.Type.ActionMsg[index].Message) / 2 * 8;
                        y = 425;
                        break;
                    }

            }

            x = GameLogic.ConvertMapX(x);
            y = GameLogic.ConvertMapY(y);

            if (General.GetTickCount() < Core.Type.ActionMsg[index].Created + time)
            {
                RenderText(Core.Type.ActionMsg[index].Message, x, y, GameClient.QbColorToXnaColor(Core.Type.ActionMsg[index].Color), Color.Black);
            }
            else
            {
                GameLogic.ClearActionMsg((byte)index);
            }

        }

        public static void DrawChat()
        {
            long xO;
            long yO;
            int Color;
            var yOffset = default(long);
            int rLines;
            int lineCount;
            string tmpText;
            long i;
            bool isVisible;
            var topWidth = default(int);
            string[] tmpArray;
            int x;
            Color Color2;

            // set the position
            xO = 19L;
            xO += Gui.Windows[Gui.GetWindowIndex("winChat")].Left;
            yO = GameState.ResolutionHeight - 40;

            // loop through chat
            rLines = 1;
            i = GameState.ChatScroll;

            while (rLines <= 8)
            {
                if (i >= Constant.CHAT_LINES)
                    break;
                lineCount = 1;

                // exit out early if we come to a blank string
                if (Strings.Len(Core.Type.Chat[(int)i].Text) == 0)
                    break;

                // get visible state
                isVisible = true;
                if (GameState.inSmallChat == true)
                {
                    if (!(Core.Type.Chat[(int)i].Visible == true))
                        isVisible = false;
                }

                if (Settings.Instance.ChannelState[Core.Type.Chat[i].Channel] == 0)
                    isVisible = false;

                // make sure it's visible
                if (isVisible == true)
                {
                    // render line
                    Color = Core.Type.Chat[(int)i].Color;
                    Color2 = GameClient.QbColorToXnaColor(Color);

                    // check if we need to word wrap
                    if (GetTextWidth(Core.Type.Chat[i].Text) > GameState.ChatWidth)
                    {
                        // word wrap
                        string[] wrappedLines = null;
                        WordWrap(Core.Type.Chat[(int)i].Text, Core.Enum.FontType.Georgia, (int)GameState.ChatWidth, ref wrappedLines);
                        tmpText = string.Join(Environment.NewLine, wrappedLines);

                        // can't have it going offscreen.
                        if (rLines + wrappedLines.Length > 9)
                            break;

                        // continue on
                        yOffset = yOffset - 14 * wrappedLines.Length;
                        RenderText(tmpText, (int)xO, (int)(yO + yOffset), Color2, Color2);
                        rLines += wrappedLines.Length;

                        // set the top width
                        tmpArray = Strings.Split(tmpText, Environment.NewLine);
                        var loopTo = Information.UBound(tmpArray);
                        for (x = 0; x < loopTo; x++)
                        {
                            if (GetTextWidth(tmpArray[x]) > topWidth)
                                topWidth = GetTextWidth(tmpArray[x]);
                        }
                    }
                    else
                    {
                        // normal
                        yOffset = yOffset - 14L;

                        RenderText(Core.Type.Chat[(int)i].Text, (int)xO, (int)(yO + yOffset), Color2, Color2);
                        rLines = rLines + 1;

                        // set the top width
                        if (GetTextWidth(Core.Type.Chat[(int)i].Text) > topWidth)
                            topWidth = GetTextWidth(Core.Type.Chat[(int)i].Text);
                    }
                }
                // increment chat pointer
                i = i + 1L;
            }

            // get the height of the small chat box
            GameLogic.SetChatHeight(rLines * 14);
            GameLogic.SetChatWidth(topWidth);
        }

        public static void DrawMapName()
        {
            RenderText(Languages.Language.Game.MapName + Core.Type.MyMap.Name, (int)Math.Round(GameState.ResolutionWidth / 2d - GetTextWidth(Core.Type.MyMap.Name)), 10, GameState.DrawMapNameColor, Color.Black);
        }

        public static void DrawPlayerName(int index)
        {
            int textX;
            int textY;
            var color = default(Color);
            var backColor = default(Color);
            string name;

            // Check access level
            if (GetPlayerPK(index) == 0)
            {
                switch (GetPlayerAccess(index))
                {
                    case (int)Core.Enum.AccessType.Player:
                        {
                            color = Color.White;
                            backColor = Color.Black;
                            break;
                        }
                    case (int)Core.Enum.AccessType.Moderator:
                        {
                            color = Color.Cyan;
                            backColor = Color.White;
                            break;
                        }
                    case (int)Core.Enum.AccessType.Mapper:
                        {
                            color = Color.Green;
                            backColor = Color.Black;
                            break;
                        }
                    case (int)Core.Enum.AccessType.Developer:
                        {
                            color = Color.Blue;
                            backColor = Color.Black;
                            break;
                        }
                    case (int)Core.Enum.AccessType.Owner:
                        {
                            color = Color.Yellow;
                            backColor = Color.Black;
                            break;
                        }
                }
            }
            else
            {
                color = Color.Red;
            }

            name = Core.Type.Player[index].Name;

            // calc pos
            textX = GameLogic.ConvertMapX(GetPlayerX(index) * GameState.PicX) + Core.Type.Player[index].XOffset + GameState.PicX / 2 - 6;
            textX = (int)Math.Round(textX - GetTextWidth(name) / 6d);

            if (GetPlayerSprite(index) <= 0 | GetPlayerSprite(index) > GameState.NumCharacters)
            {
                textY = GameLogic.ConvertMapY(GetPlayerY(index) * GameState.PicY) + Core.Type.Player[GameState.MyIndex].YOffset - 16;
            }
            else
            {
                // Determine location for text
                textY = (int)Math.Round(GameLogic.ConvertMapY(GetPlayerY(index) * GameState.PicY) + Core.Type.Player[index].YOffset - GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, GetPlayerSprite(index).ToString())).Height / 4d + 16d);
            }

            // Draw name
            RenderText(name, textX, textY, color, backColor);
        }

    }
}