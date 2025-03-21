using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework.Input;

namespace Client
{

    public struct ChatCursor
    {
        internal int X;
        internal int Y;
    }

    public struct ChatData
    {
        internal bool Active;
        internal int HistoryLimit;
        internal int MessageLimit;

        internal List<string> History;
        internal string CachedMessage;
        internal string CurrentMessage;
        internal ChatCursor Cursor;

        internal bool ProcessKey(KeyboardState keyboardState)
        {
            Microsoft.Xna.Framework.Input.Keys[] keys = keyboardState.GetPressedKeys();

            if (!Active)
            {
                if (keys.Contains(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    Active = Conversions.ToBoolean(1);
                    return true;
                }

                return false;
            }

            if (CurrentMessage is null)
                CurrentMessage = "";

            foreach (var key in keys)
            {
                switch (key)
                {
                    case Microsoft.Xna.Framework.Input.Keys.Enter:
                        {
                            History.Add(CurrentMessage);
                            if (History.Count > HistoryLimit)
                            {
                                History.RemoveRange(0, History.Count - HistoryLimit);
                            }
                            Cursor.Y = History.Count;
                            Active = Conversions.ToBoolean(0);
                            return true;
                        }

                    case Microsoft.Xna.Framework.Input.Keys.Back:
                        {
                            if (CurrentMessage.Length > 0)
                            {
                                CurrentMessage = CurrentMessage.Remove(CurrentMessage.Length - 1);
                            }

                            break;
                        }

                    case Microsoft.Xna.Framework.Input.Keys.Left:
                        {
                            Cursor.X = Math.Max(0, Cursor.X - 1);
                            break;
                        }

                    case Microsoft.Xna.Framework.Input.Keys.Right:
                        {
                            Cursor.X = Math.Min(CurrentMessage.Length, Cursor.X + 1);
                            break;
                        }

                    case Microsoft.Xna.Framework.Input.Keys.Down:
                        {
                            if (History.Count > 0)
                            {
                                Cursor.Y = Math.Min(History.Count, Cursor.Y + 1);
                                if (Cursor.Y == History.Count)
                                {
                                    CurrentMessage = CachedMessage;
                                }
                                else
                                {
                                    CurrentMessage = History[Cursor.Y];
                                }
                            }

                            break;
                        }

                    case Microsoft.Xna.Framework.Input.Keys.Up:
                        {
                            if (History.Count > 0)
                            {
                                if (Cursor.Y == History.Count)
                                {
                                    CachedMessage = CurrentMessage;
                                }

                                Cursor.Y = Math.Max(0, Cursor.Y - 1);
                                CurrentMessage = History[Cursor.Y];
                            }

                            break;
                        }

                    case Microsoft.Xna.Framework.Input.Keys.V:
                        {
                            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl))
                            {
                                // CurrentMessage &= Clipboard.GetText()
                            }

                            break;
                        }

                    default:
                        {
                            string keyName = Enum.GetName(typeof(Microsoft.Xna.Framework.Input.Keys), key);
                            if (keyName is not null && keyName.Length == 1)
                            {
                                CurrentMessage += keyName;
                                Cursor.Y = History.Count;
                                CachedMessage = CurrentMessage;
                            }

                            break;
                        }
                }
            }

            return true;
        }

    }

    static class ChatModule
    {
        static ChatData ToBoolean_ChatInput()
        {
            var init = new ChatData();
            return (init.Active = Conversions.ToBoolean(0), init.HistoryLimit = 10, init.MessageLimit = 100, init.History = new List<string>(init.HistoryLimit + 1), init.CurrentMessage = "", init.Cursor = new ChatCursor() { X = 0, Y = 0 }, init).init;
        }

        public static ChatData ChatInput = ToBoolean_ChatInput();
    }
}