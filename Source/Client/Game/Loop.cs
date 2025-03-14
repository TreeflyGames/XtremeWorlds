using Core;
using Microsoft.VisualBasic.CompilerServices;
using static Core.Global.Command;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Path = Core.Path;

namespace Client
{

    static class Loop
    {
        // Declare private fields
        private static int i;
        private static int tmr1000;
        private static int tick;
        private static int fogtmr;
        private static int chattmr;
        private static int tmpfps;
        private static int tmplps;
        private static int walkTimer;
        private static int frameTime;
        private static int tmrweather;
        private static int barTmr;
        private static int tmr25;
        private static int tmr500;
        private static int tmr250;
        private static int tmrconnect;
        private static int TickFPS;
        private static int fadetmr;
        private static int rendertmr;
        private static int[] animationtmr = new int[2]; // Array of size 2

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
                    Sound.PlayMusic(Core.Type.MyMap.Music);
                    tmr25 = tick + 25;
                }

                if (GameState.ShowAnimTimer < tick)
                {
                    GameState.ShowAnimLayers = !GameState.ShowAnimLayers;
                    GameState.ShowAnimTimer = tick + 500;
                }

                for (int layer = 0; layer <= 1; layer++)
                {
                    if (animationtmr[layer] < tick)
                    {
                        for (byte x = 0, loopTo = Core.Type.MyMap.MaxX; x < loopTo; x++)
                        {
                            for (byte y = 0, loopTo1 = Core.Type.MyMap.MaxY; y < loopTo1; y++)
                            {
                                if (GameLogic.IsValidMapPoint(x, y))
                                {
                                    if (Core.Type.MyMap.Tile[x, y].Type == Core.Enum.TileType.Animation)
                                    {                                      
                                        animationtmr[layer] = tick + Animation.PlayAnimation(Core.Type.Animation[Core.Type.MyMap.Tile[x, y].Data1].Sprite[layer], layer, Core.Type.MyMap.Tile[x, y].Data1, x, y);
                                    }

                                    if (Core.Type.MyMap.Tile[x, y].Type2 == Core.Enum.TileType.Animation)
                                    {
                                        animationtmr[layer] = tick + Animation.PlayAnimation(Core.Type.Animation[Core.Type.MyMap.Tile[x, y].Data1_2].Sprite[layer], layer, Core.Type.MyMap.Tile[x, y].Data1_2, x, y);
                                    }
                                }
                            }
                        }
                        ;


                    }
                }

                for (i = 0; i <= byte.MaxValue; i++)
                    Animation.CheckAnimInstance(i);

                if (tick > Event.EventChatTimer)
                {
                    if (string.IsNullOrEmpty(Event.EventText))
                    {
                        if (Conversions.ToInteger(Event.EventChat) == 1)
                        {
                            Event.EventChat = Conversions.ToBoolean(0);
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
                            GameState.ShakeTimerEnabled = Conversions.ToBoolean(0);
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
                        if (Core.Type.Player[GameState.MyIndex].Skill[i].Num >= 0)
                        {
                            if (Core.Type.Player[GameState.MyIndex].Skill[i].CD > 0)
                            {
                                if (Core.Type.Player[GameState.MyIndex].Skill[i].CD + Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Skill[i].Num].CdTime * 1000 < tick)
                                {
                                    Core.Type.Player[GameState.MyIndex].Skill[i].CD = 0;
                                }
                            }
                        }
                    }
                }

                // check if we need to unlock the player's skill casting restriction
                if (GameState.SkillBuffer >= 0)
                {
                    if (GameState.SkillBufferTimer + Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Skill[GameState.SkillBuffer].Num].CastTime * 1000 < tick)
                    {
                        GameState.SkillBuffer = -1;
                        GameState.SkillBufferTimer = 0;
                    }
                }

                // check if we need to unlock the pets's Skill casting restriction
                if (Pet.PetSkillBuffer >= 0)
                {
                    if (Core.Type.Player[GameState.MyIndex].Pet.Num >= 0 || Core.Type.Player[GameState.MyIndex].Pet.Num <= Constant.MAX_PETS)
                    {
                        if (Pet.PetSkillBufferTimer + Core.Type.Skill[Core.Type.Pet[(int)Core.Type.Player[GameState.MyIndex].Pet.Num].Skill[(int)Pet.PetSkillBuffer]].CastTime * 1000 < tick)
                        {
                            Pet.PetSkillBuffer = -1;
                            Pet.PetSkillBufferTimer = 0;
                        }
                    }
                }

                if (GameState.CanMoveNow)
                {
                    Player.CheckMovement(); // Check if player is trying to move
                    Player.CheckAttack();   // Check to see if player is trying to attack
                }

                // Process input before rendering, otherwise input will be behind by 1 frame
                if (walkTimer < tick)
                {
                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i))
                        {
                            Player.ProcessPlayerMovement(i);
                            if (Pet.PetAlive(i))
                            {
                                Pet.ProcessPetMovement(i);
                            }
                        }
                    }

                    // Process NPC movements (actually move them)
                    for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        if (Core.Type.MyMap.NPC[i] >= 0)
                        {
                            GameLogic.ProcessNPCMovement(i);
                        }
                    }

                    var loopTo2 = GameState.CurrentEvents;
                    for (i = 0; i < loopTo2; i++)
                        Event.ProcessEventMovement(i);

                    walkTimer = tick + 25; // edit this value to change WalkTimer
                }

                // chat timer
                if (chattmr < tick)
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

                    chattmr = tick + 50;
                }

                // fog scrolling
                if (fogtmr < tick)
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
                        fogtmr = tick + 255 - GameState.CurrentFogSpeed;
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
                    if (GameState.chatShowLine == "|")
                    {
                        GameState.chatShowLine = "";
                    }
                    else
                    {
                        GameState.chatShowLine = "|";
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
                        if (Core.Type.MyMapNPC[i].Num >= 0)
                        {
                            GameLogic.SetBarWidth(ref GameState.BarWidth_NPCHP_Max[i], ref GameState.BarWidth_NPCHP[i]);
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
                    if (GameState.MapAnim == 0)
                    {
                        GameState.MapAnim = 1;
                    }
                    else
                    {
                        GameState.MapAnim = 0;
                    }
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
                    if (GameState.chatShowLine == "|")
                    {
                        GameState.chatShowLine = "";
                    }
                    else
                    {
                        GameState.chatShowLine = "|";
                    }

                    tmr500 = tick + 500;
                }

                if (tmr25 < tick)
                {
                    Sound.PlayMusic(Settings.Instance.MenuMusic);
                    tmr25 = tick + 25;
                }
            }

            if (tmrweather < tick)
            {
                Weather.ProcessWeather();
                tmrweather = tick + 50;
            }

            if (fadetmr < tick)
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
                            GameState.UseFade = Conversions.ToBoolean(0);
                        }
                        else
                        {
                            GameState.FadeAmount = GameState.FadeAmount - 5;
                        }
                    }
                }
                fadetmr = tick + 30;
            }

            Gui.ResizeGUI();
        }
    }
}