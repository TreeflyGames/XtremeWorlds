using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using static Core.Global.Command;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Path = Core.Path;

namespace Client
{

    public class Loop
    {
        // Declare private fields
        private static int i;
        private static int tmr1000;
        private static int tick;
        private static int fogTmr;
        private static int chatTmr;
        private static int tmpFps;
        private static int tmpLps;
        private static int walkTimer;
        private static int frameTime;
        private static int tmrWeather;
        private static int barTmr;
        private static int tmr25;
        private static int tmr500;
        private static int tmr250;
        private static int tmrConnect;
        private static int TickFPS;
        private static int fadeTmr;
        private static int renderTmr;
        private static int[] animationTmr = new int[2];

        public static void Game()
        {
            tick = General.GetTickCount();
            GameState.ElapsedTime = tick - frameTime; // Set the time difference for time-based movement

            frameTime = tick;

            if (GameLogic.GameStarted())
            {
                if (tmr1000 < tick)
                {
                    NetworkSend.GetPing();
                    tmr1000 = tick + 1000;
                }

                if (tmr25 < tick)
                {
                    Sound.PlayMusic(Data.MyMap.Music);
                    tmr25 = tick + 25;
                }

                if (GameState.ShowAnimTimer < tick)
                {
                    GameState.ShowAnimLayers = !GameState.ShowAnimLayers;
                    GameState.ShowAnimTimer = tick + 500;
                }

                for (int layer = 0; layer <= 1; layer++)
                {
                    if (animationTmr[layer] < tick)
                    {
                        for (byte x = 0, loopTo = Data.MyMap.MaxX; x < loopTo; x++)
                        {
                            for (byte y = 0, loopTo1 = Data.MyMap.MaxY; y < loopTo1; y++)
                            {
                                if (GameLogic.IsValidMapPoint(x, y))
                                {
                                    if (Data.MyMap.Tile[x, y].Type == TileType.Animation)
                                    {                                      
                                        animationTmr[layer] = tick + Animation.PlayAnimation(Data.Animation[Data.MyMap.Tile[x, y].Data1].Sprite[layer], layer, Data.MyMap.Tile[x, y].Data1, x, y);
                                    }

                                    if (Data.MyMap.Tile[x, y].Type2 == TileType.Animation)
                                    {
                                        animationTmr[layer] = tick + Animation.PlayAnimation(Data.Animation[Data.MyMap.Tile[x, y].Data1_2].Sprite[layer], layer, Data.MyMap.Tile[x, y].Data1_2, x, y);
                                    }
                                }
                            }
                        }
                        ;


                    }
                }

                for (i = 0; i < byte.MaxValue; i++)
                {
                    Animation.CheckAnimInstance(i);
                }

                if (tick > Event.EventChatTimer)
                {
                    if (string.IsNullOrEmpty(Event.EventText))
                    {
                        if (Conversions.ToInteger(Event.EventChat) == 1)
                        {
                            Event.EventChat = false;
                        }
                    }
                }

                // screenshake
                if (GameState.ShakeTimerEnabled)
                {
                    if (GameState.ShakeTimer < tick)
                    {
                        if (GameState.ShakeCount < 10)
                        {
                            if (GameState.LastDir == 0)
                            {
                                GameState.LastDir = 1;
                            }
                            else
                            {
                                GameState.LastDir = 0;
                            }
                        }
                        else
                        {
                            GameState.ShakeCount = 0;
                            GameState.ShakeTimerEnabled = false;
                        }

                        GameState.ShakeCount += 1;

                        GameState.ShakeTimer = tick + 50;
                    }
                }

                // check if we need to end the CD icon
                if (GameState.NumSkills > 0)
                {
                    for (i = 0; i < Constant.MAX_PLAYER_SKILLS; i++)
                    {
                        if (Core.Data.Player[GameState.MyIndex].Skill[i].Num >= 0)
                        {
                            if (Core.Data.Player[GameState.MyIndex].Skill[i].CD > 0)
                            {
                                if (Core.Data.Player[GameState.MyIndex].Skill[i].CD + Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Skill[i].Num].CdTime * 1000 < tick)
                                {
                                    Core.Data.Player[GameState.MyIndex].Skill[i].CD = 0;
                                }
                            }
                        }
                    }
                }

                // check if we need to unlock the player's skill casting restriction
                if (GameState.SkillBuffer >= 0)
                {
                    if (GameState.SkillBufferTimer + Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Skill[GameState.SkillBuffer].Num].CastTime * 1000 < tick)
                    {
                        GameState.SkillBuffer = -1;
                        GameState.SkillBufferTimer = 0;
                    }
                }
                
                // Process input before rendering, otherwise input will be behind by 1 frame
                if (walkTimer < tick)
                {
                    if (GameState.CanMoveNow)
                    {
                        Player.CheckMovement(); // Check if player is trying to move
                        Player.CheckAttack();   // Check to see if player is trying to attack
                    }
                    
                    // Process player movements
                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i))
                        {
                            Player.ProcessPlayerMovement(i);                            
                        }
                    }

                    // Process npc movements
                    for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        GameLogic.ProcessNpcMovement(i);
                        
                    }

                    var loopTo2 = GameState.CurrentEvents;
                    for (i = 0; i < loopTo2; i++)
                    {
                        Event.ProcessEventMovement(i);
                    }

                    walkTimer = tick + 30;
                }

                // chat timer
                if (chatTmr < tick)
                {
                    // scrolling
                    if (GameState.ChatButtonUp)
                    {
                        GameLogic.ScrollChatBox(0);
                    }

                    if (GameState.ChatButtonDown)
                    {
                        GameLogic.ScrollChatBox(1);
                    }

                    chatTmr = tick + 50;
                }

                // fog scrolling
                if (fogTmr < tick)
                {
                    if (GameState.CurrentFogSpeed > 0)
                    {
                        // move
                        GameState.FogOffsetX = GameState.FogOffsetX - 1;
                        GameState.FogOffsetY = GameState.FogOffsetY - 1;

                        // reset
                        if (GameState.FogOffsetX < -255)
                            GameState.FogOffsetX = 1;

                        if (GameState.FogOffsetY < -255)
                            GameState.FogOffsetY = 1;

                        fogTmr = tick + 255 - GameState.CurrentFogSpeed;
                    }
                }

                if (tmr500 < tick)
                {
                    // animate waterfalls
                    switch (GameState.WaterfallFrame)
                    {
                        case 0:
                            {
                                GameState.WaterfallFrame = 1;
                                break;
                            }
                        case 1:
                            {
                                GameState.WaterfallFrame = 2;
                                break;
                            }
                        case 2:
                            {
                                GameState.WaterfallFrame = 0;
                                break;
                            }
                    }

                    // animate autotiles
                    switch (GameState.AutoTileFrame)
                    {
                        case 0:
                            {
                                GameState.AutoTileFrame = 1;
                                break;
                            }
                        case 1:
                            {
                                GameState.AutoTileFrame = 2;
                                break;
                            }
                        case 2:
                            {
                                GameState.AutoTileFrame = 0;
                                break;
                            }
                    }

                    // animate textbox
                    if (GameState.ChatShowLine == "|")
                    {
                        GameState.ChatShowLine = "";
                    }
                    else
                    {
                        GameState.ChatShowLine = "|";
                    }

                    tmr500 = tick + 500;
                }

                // elastic bars
                if (barTmr < tick)
                {
                    GameLogic.SetBarWidth(ref GameState.BarWidth_GuiHP_Max, ref GameState.BarWidth_GuiHP);
                    GameLogic.SetBarWidth(ref GameState.BarWidth_GuiSP_Max, ref GameState.BarWidth_GuiSP);
                    GameLogic.SetBarWidth(ref GameState.BarWidth_GuiEXP_Max, ref GameState.BarWidth_GuiEXP);
                    for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        if (Data.MyMapNpc[i].Num >= 0)
                        {
                            GameLogic.SetBarWidth(ref GameState.BarWidth_NpcHP_Max[i], ref GameState.BarWidth_NpcHP[i]);
                        }
                    }

                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i) & GetPlayerMap(i) == GetPlayerMap(GameState.MyIndex))
                        {
                            GameLogic.SetBarWidth(ref GameState.BarWidth_PlayerHP_Max[i], ref GameState.BarWidth_PlayerHP[i]);
                            GameLogic.SetBarWidth(ref GameState.BarWidth_PlayerSP_Max[i], ref GameState.BarWidth_PlayerSP[i]);
                        }
                    }

                    // reset timer
                    barTmr = tick + 10;
                }

                // Change map animation
                if (tmr250 < tick)
                {
                    for (int i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i))
                        {
                            if (GetPlayerMap(i) == GetPlayerMap(GameState.MyIndex))
                            {
                                // Check if completed walking over to the next tile
                                if (Core.Data.Player[i].Steps == 3)
                                {
                                    Core.Data.Player[i].Steps = 0;
                                }
                                else
                                {
                                    Core.Data.Player[i].Steps++;
                                }                              
                            }
                        }
                    }

                    for (int i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        if (Data.MyMapNpc[i].Num >= 0)
                        {
                            // Check if completed walking over to the next tile
                            if (Data.MyMapNpc[i].Steps == 3)
                            {
                                Data.MyMapNpc[i].Steps = 0;
                            }
                            else
                            {
                                Data.MyMapNpc[i].Steps++;
                            }
                        }
                    }

                    if (Data.MapEvents != null)
                    {
                        var loopTo = Information.UBound(Data.MapEvents);
                        for (i = 0; i < loopTo; i++)
                        {
                            if (Data.MapEvents[i].WalkAnim == 1)
                            {
                                // Check if completed walking over to the next tile
                                if (Data.MyMapNpc[i].Steps == 3)
                                {
                                    Data.MyMapNpc[i].Steps = 0;
                                }
                                else
                                {
                                    Data.MyMapNpc[i].Steps++;
                                }
                            }
                        }
                    }

                    GameState.MapAnim = !GameState.MapAnim;
                    tmr250 = tick + 250;
                }

                if (Sound.FadeInSwitch == true)
                {
                    Sound.FadeIn();
                }

                if (Sound.FadeOutSwitch == true)
                {
                    Sound.FadeOut();
                }
            }
            else
            {
                if (tmr500 < tick)
                {
                    // animate textbox
                    if (GameState.ChatShowLine == "|")
                    {
                        GameState.ChatShowLine = "";
                    }
                    else
                    {
                        GameState.ChatShowLine = "|";
                    }

                    tmr500 = tick + 500;
                }

                if (tmr25 < tick)
                {
                    Sound.PlayMusic(SettingsManager.Instance.MenuMusic);
                    tmr25 = tick + 25;
                }
            }

            if (tmrWeather < tick)
            {
                Weather.ProcessWeather();
                tmrWeather = tick + 50;
            }

            if (fadeTmr < tick)
            {
                if (GameState.FadeType != 2)
                {
                    if (GameState.FadeType == 1)
                    {
                        if (GameState.FadeAmount == 255)
                        {

                        }
                        else
                        {
                            GameState.FadeAmount = GameState.FadeAmount + 5;
                        }
                    }
                    else if (GameState.FadeType == 0)
                    {
                        if (GameState.FadeAmount == 0)
                        {
                            GameState.UseFade = false;
                        }
                        else
                        {
                            GameState.FadeAmount = GameState.FadeAmount - 5;
                        }
                    }
                }
                fadeTmr = tick + 30;
            }

            Gui.ResizeGUI();
        }
    }
}