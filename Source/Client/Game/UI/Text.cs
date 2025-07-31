using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using static Core.Global.Command;
using Color = Microsoft.Xna.Framework.Color;
using Path = Core.Path;

namespace Client
{

    public class Text
    {
        public static Dictionary<Core.Font, SpriteFont> Fonts = new Dictionary<Core.Font, SpriteFont>();

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
        public static int GetTextWidth(string text, Core.Font font = Core.Font.Georgia, float textSize = 1.0f)
        {
            if (!Fonts.ContainsKey(font))
                throw new ArgumentException("Font not found.");
            string sanitizedText = SanitizeText(text, Fonts[font]);
            var textDimensions = Fonts[font].MeasureString(sanitizedText);
            return (int)Math.Round(textDimensions.X * textSize);
        }

        // Get the height of the text with optional scaling
        public static int TextHeight(string text, Core.Font font = Core.Font.Georgia, float textSize = 1.0f)
        {
            if (!Fonts.ContainsKey(font))
                throw new ArgumentException("Font not found.");
            var textDimensions = Fonts[font].MeasureString(text);
            return (int)Math.Round(textDimensions.Y * textSize);
        }

        public static void AddText(string text, int color, long alpha = 255L, byte channel = 0)
        {
            // wordwrap
            string[] wrappedLines = null;
            WordWrap(text, Core.Font.Georgia, Gui.Windows[Gui.GetWindowIndex("winChat")].Width, ref wrappedLines);

            GameState.Chat_HighIndex += wrappedLines.Length;

            if (GameState.Chat_HighIndex > Constant.CHAT_LINES)
                GameState.Chat_HighIndex = Constant.CHAT_LINES;
            
            // Move the rest of the chat lines up
            for (int i = (int)GameState.Chat_HighIndex - wrappedLines.Length; i > 0; i--)
            {
                Data.Chat[i] = Data.Chat[i - 1];
            }
            
            for (int i = (int)wrappedLines.Length - 1, chatIndex = 0; i >= 0; i--, chatIndex++)
            {
                // Add the wrapped line to the chat
                Core.Data.Chat[chatIndex].Text = wrappedLines[i];
                Core.Data.Chat[chatIndex].Color = color;
                Core.Data.Chat[chatIndex].Visible = true;
                Core.Data.Chat[chatIndex].Timer = General.GetTickCount();
                Core.Data.Chat[chatIndex].Channel = channel;
            }
        }

        public static void WordWrap(string text, Core.Font font, long MaxLineLen, ref string[] theArray)
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
                        Array.Resize(ref theArray, (int)(lineCount));
                        theArray[(int)lineCount - 1] = Strings.Mid(text, (int)b, (int)(i - 1L - b));
                        b = i - 1L;
                        size = 0L;
                    }
                    else
                    {
                        // Break at the last space to preserve the word
                        lineCount = lineCount + 1L;
                        Array.Resize(ref theArray, (int)(lineCount));

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
                        Array.Resize(ref theArray, (int)(lineCount));
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

        public static void RenderText(string text, int x, int y, Color frontColor, Color backColor, Core.Font font = Core.Font.Georgia)
        {
            if (text == null) return;
            string sanitizedText = new string(text.Where(c => Fonts[font].Characters.Contains(c)).ToArray());
            GameClient.SpriteBatch.DrawString(Fonts[font], sanitizedText, new Vector2(x + 1, y + 1), backColor, 0.0f, Vector2.Zero, 12f / 16.0f, SpriteEffects.None, 0.0f);
            GameClient.SpriteBatch.DrawString(Fonts[font], sanitizedText, new Vector2(x, y), frontColor, 0.0f, Vector2.Zero, 12f / 16.0f, SpriteEffects.None, 0.0f);
        }

        public static void DrawMapAttributes()
        {
            int x;
            int y;
            int tX;
            int tY;
            int tA;

            var loopTo = (int)GameState.TileView.Right;
            for (x = (int)GameState.TileView.Left; x < loopTo; x++)
            {
                var loopTo1 = (int)GameState.TileView.Bottom;
                for (y = (int)GameState.TileView.Top; y < loopTo1; y++)
                {
                    if (GameLogic.IsValidMapPoint(x, y))
                    {
                        {
                            ref var withBlock = ref Data.MyMap.Tile[x, y];
                            tX = (int)Math.Round(GameLogic.ConvertMapX(x) - 4 + GameState.SizeX * 0.5d);
                            tY = (int)Math.Round(GameLogic.ConvertMapY(y) - 7 + GameState.SizeY * 0.5d);

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
                                case (int)TileType.Blocked:
                                    {
                                        RenderText("B", tX, tY, Color.Red, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Warp:
                                    {
                                        RenderText("W", tX, tY, Color.Blue, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Item:
                                    {
                                        RenderText("I", tX, tY, Color.White, Color.Black);
                                        break;
                                    }
                                case (int)TileType.NpcAvoid:
                                    {
                                        RenderText("N", tX, tY, Color.White, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Resource:
                                    {
                                        RenderText("R", tX, tY, Color.Green, Color.Black);
                                        break;
                                    }
                                case (int)TileType.NpcSpawn:
                                    {
                                        RenderText("S", tX, tY, Color.Yellow, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Shop:
                                    {
                                        RenderText("S", tX, tY, Color.Blue, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Bank:
                                    {
                                        RenderText("B", tX, tY, Color.Blue, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Heal:
                                    {
                                        RenderText("H", tX, tY, Color.Green, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Trap:
                                    {
                                        RenderText("T", tX, tY, Color.Red, Color.Black);
                                        break;
                                    }
                                case (int)TileType.Animation:
                                    {
                                        RenderText("A", tX, tY, Color.Red, Color.Black);
                                        break;
                                    }
                                case (int)TileType.NoCrossing:
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

        public static void DrawNpcName(double MapNpcNum)
        {
            int textX;
            int textY;
            var color = default(Color);
            var backColor = default(Color);
            double NpcNum;

            NpcNum = (int)Data.MyMapNpc[(int)MapNpcNum].Num;

            if (NpcNum < 0 | NpcNum > Core.Constant.MAX_NPCS)
                return;

            switch (Core.Data.Npc[(int)NpcNum].Behaviour)
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
            textX = GameLogic.ConvertMapX(Data.MyMapNpc[(int)MapNpcNum].X) + GameState.SizeX / 2 - 6;
            textX -= (int)(GetTextWidth(Core.Data.Npc[(int)NpcNum].Name) / 6d);

            if (Core.Data.Npc[(int)NpcNum].Sprite < 1 | Core.Data.Npc[(int)NpcNum].Sprite > GameState.NumCharacters)
            {
                textY = GameLogic.ConvertMapY(Data.MyMapNpc[(int)MapNpcNum].Y) - 16;
            }
            else
            {
                textY = (int)GameLogic.ConvertMapY((int)(Data.MyMapNpc[(int)MapNpcNum].Y - GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, Core.Data.Npc[(int)NpcNum].Sprite.ToString())).Height / 4d + 16d));
            }

            // Draw name
            RenderText(Core.Data.Npc[(int)NpcNum].Name, textX, textY, color, backColor);
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

            name = Data.MapEvents[index].Name;

            // calc pos
            textX = GameLogic.ConvertMapX(Data.MapEvents[index].X) + GameState.SizeX / 2 - 6;
            textX -= GetTextWidth(name) / 6;

            if (Data.MapEvents[index].GraphicType == 0)
            {
                textY = GameLogic.ConvertMapY(Data.MapEvents[index].Y) - 16;
            }
            else if (Data.MapEvents[index].GraphicType == 1)
            {
                if (Data.MapEvents[index].Graphic < 1 | Data.MapEvents[index].Graphic > GameState.NumCharacters)
                {
                    textY = GameLogic.ConvertMapY(Data.MapEvents[index].Y) - 16;
                }
                else
                {
                    // Determine location for text
                    textY = GameLogic.ConvertMapY(Data.MapEvents[index].Y) - GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, Data.MapEvents[index].Graphic.ToString())).Height / 4 + 16;
                }
            }
            else if (Data.MapEvents[index].GraphicType == 2)
            {
                if (Data.MapEvents[index].GraphicY2 > 0)
                {
                    textX = textX + Data.MapEvents[index].GraphicY2 * GameState.SizeY / 2 - 16;
                    textY = GameLogic.ConvertMapY(Data.MapEvents[index].Y) - Data.MapEvents[index].GraphicY2 * GameState.SizeY + 16;
                }
                else
                {
                    textY = GameLogic.ConvertMapY(Data.MapEvents[index].Y) - 32 + 16;
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
            switch (Data.ActionMsg[index].Type)
            {
                case (int)Core.ActionMessageType.Static:
                    {
                        time = 1500;

                        if (Data.ActionMsg[index].Y > 0)
                        {
                            x = Data.ActionMsg[index].X + Conversion.Int(GameState.SizeX / 2) - Strings.Len(Data.ActionMsg[index].Message) / 2 * 8;
                            y = Data.ActionMsg[index].Y - Conversion.Int(GameState.SizeY / 2) - 2;
                        }
                        else
                        {
                            x = Data.ActionMsg[index].X + Conversion.Int(GameState.SizeX / 2) - Strings.Len(Data.ActionMsg[index].Message) / 2 * 8;
                            y = Data.ActionMsg[index].Y - Conversion.Int(GameState.SizeY / 2) + 18;
                        }

                        break;
                    }

                case (int)Core.ActionMessageType.Scroll:
                    {
                        time = 1500;

                        if (Data.ActionMsg[index].Y > 0)
                        {
                            x = Data.ActionMsg[index].X + Conversion.Int(GameState.SizeX / 2) - Strings.Len(Data.ActionMsg[index].Message) / 2 * 8;
                            y = (int)Math.Round(Data.ActionMsg[index].Y - Conversion.Int(GameState.SizeY / 2) - 2 - Data.ActionMsg[index].Scroll * 0.6d);
                            Data.ActionMsg[index].Scroll = Data.ActionMsg[index].Scroll + 1;
                        }
                        else
                        {
                            x = Data.ActionMsg[index].X + Conversion.Int(GameState.SizeX / 2) - Strings.Len(Data.ActionMsg[index].Message) / 2 * 8;
                            y = (int)Math.Round(Data.ActionMsg[index].Y - Conversion.Int(GameState.SizeY / 2) + 18 + Data.ActionMsg[index].Scroll * 0.6d);
                            Data.ActionMsg[index].Scroll = Data.ActionMsg[index].Scroll + 1;
                        }

                        break;
                    }

                case (int)Core.ActionMessageType.Screen:
                    {
                        time = 3000;

                        // This will kill any action screen messages that there in the system
                        for (i = byte.MaxValue; i >= 0; i -= 1)
                        {
                            if (Data.ActionMsg[i].Type == (int)Core.ActionMessageType.Screen)
                            {
                                if (i != index)
                                {
                                    GameLogic.ClearActionMsg((byte)index);
                                    index = i;
                                }
                            }
                        }
                        x = GameState.ResolutionWidth / 2 - Strings.Len(Data.ActionMsg[index].Message) / 2 * 8;
                        y = 425;
                        break;
                    }

            }

            x = GameLogic.ConvertMapX(x);
            y = GameLogic.ConvertMapY(y);

            if (General.GetTickCount() < Data.ActionMsg[index].Created + time)
            {
                RenderText(Data.ActionMsg[index].Message, x, y, GameClient.QbColorToXnaColor(Data.ActionMsg[index].Color), Color.Black);
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
            int width;

            // set the position
            xO = 19L;
            xO += Gui.Windows[Gui.GetWindowIndex("winChat")].Left;
            yO = GameState.ResolutionHeight - 45;
            width = (int)Gui.Windows[Gui.GetWindowIndex("winChat")].Width;

            // loop through chat
            rLines = 1;
            i = GameState.ChatScroll;

            while (rLines < 8)
            {
                if (i >= Constant.CHAT_LINES)
                    break;
                lineCount = 1;

                // exit out early if we come to a blank string
                if (Strings.Len(Core.Data.Chat[(int)i].Text) == 0)
                    break;

                // get visible state
                isVisible = true;
                if (GameState.inSmallChat == true)
                {
                    if (!(Core.Data.Chat[(int)i].Visible == true))
                        isVisible = false;
                }

                if (SettingsManager.Instance.ChannelState[Core.Data.Chat[i].Channel] == 0)
                    isVisible = false;

                // make sure it's visible
                if (isVisible == true)
                {
                    // render line
                    Color = Core.Data.Chat[(int)i].Color;
                    Color2 = GameClient.QbColorToXnaColor(Color);

                    // check if we need to word wrap
                    if (GetTextWidth(Core.Data.Chat[i].Text) > width)
                    {
                        // word wrap
                        string[] wrappedLines = null;
                        WordWrap(Core.Data.Chat[(int)i].Text, Core.Font.Georgia, width, ref wrappedLines);

                        // continue on
                        yOffset = yOffset - 10 * wrappedLines.Length;
                        for (int j = 0; j < wrappedLines.Length; j++)
                        {
                            RenderText(wrappedLines[j], (int)xO, (int)(yO + yOffset + 10 * j), Color2, Color2);
                        }
                        rLines += wrappedLines.Length;

                        // set the top width
                        var loopTo = Information.UBound(wrappedLines);
                        for (x = 0; x < loopTo; x++)
                        {
                            if (GetTextWidth(wrappedLines[x]) > topWidth)
                                topWidth = GetTextWidth(wrappedLines[x]);
                        }
                    }
                    else
                    {
                        // normal
                        yOffset = yOffset - 12L; // Adjusted spacing from 14 to 12

                        RenderText(Core.Data.Chat[(int)i].Text, (int)xO, (int)(yO + yOffset), Color2, Color2);
                        rLines = rLines + 1;

                        // set the top width
                        if (GetTextWidth(Core.Data.Chat[(int)i].Text) > topWidth)
                            topWidth = GetTextWidth(Core.Data.Chat[(int)i].Text);
                    }
                }
                // increment chat pointer
                i = i + 1L;
            }

            // get the height of the small chat box
            GameLogic.SetChatHeight(rLines * 12); // Adjusted spacing from 14 to 12
            GameLogic.SetChatWidth(topWidth);
        }

        public static void DrawMapName()
        {
            RenderText(Data.MyMap.Name, (int)Math.Round(GameState.ResolutionWidth / 2d - GetTextWidth(Data.MyMap.Name)), 10, GameState.DrawMapNameColor, Color.Black);
        }

        public static void DrawPlayerName(int index)
        {
            int textX;
            int textY;
            var color = default(Color);
            var backColor = default(Color);
            string name;

            // Check access level
            if (GetPlayerPK(index) == false)
            {
                switch (GetPlayerAccess(index))
                {
                    case (int)AccessLevel.Player:
                        {
                            color = Color.White;
                            backColor = Color.Black;
                            break;
                        }
                    case (int)AccessLevel.Moderator:
                        {
                            color = Color.Cyan;
                            backColor = Color.White;
                            break;
                        }
                    case (int)AccessLevel.Mapper:
                        {
                            color = Color.Green;
                            backColor = Color.Black;
                            break;
                        }
                    case (int)AccessLevel.Developer:
                        {
                            color = Color.Blue;
                            backColor = Color.Black;
                            break;
                        }
                    case (int)AccessLevel.Owner:
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

            name = Core.Data.Player[index].Name;

            // calc pos
            textX = GameLogic.ConvertMapX(GetPlayerRawX(index)) + 8;
            textX = (int)Math.Round(textX - GetTextWidth(name) / 6d);

            if (GetPlayerSprite(index) <= 0 | GetPlayerSprite(index) > GameState.NumCharacters)
            {
                textY = GameLogic.ConvertMapY(GetPlayerRawY(index)) - 16;
            }
            else
            {
                // Determine location for text
                textY = (int)Math.Round((decimal)GameLogic.ConvertMapY((int)(GetPlayerRawY(index) - GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, GetPlayerSprite(index).ToString())).Height / 4d + 16d)));
            }

            // Draw name
            RenderText(name, textX, textY, color, backColor);
        }

    }
}