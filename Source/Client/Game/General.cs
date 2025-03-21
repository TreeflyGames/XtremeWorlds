using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.InteropServices;
using static Core.Global.Command;

namespace Client
{

    public class General
    {
        public static GameClient Client = new GameClient();
        public static GameState State = new GameState();
        public static RandomUtility Random = new RandomUtility();
        public static Gui Gui = new Gui();
        
        public static IConfiguration? Configuration;
        
		public static int GetTickCount()
        {
            return Environment.TickCount;
        }

        public static void Startup()
        {
            GameState.InMenu = true;
            ClearGameData();
            LoadGame();
        }

        public static void LoadGame()
        {
            Settings.Load();
            Languages.Load();
            CheckAnimations();
            CheckCharacters();
            CheckEmotes();
            CheckTilesets();
            CheckFogs();
            CheckItems();
            CheckPanoramas();
            CheckPaperdolls();
            CheckParallax();
            CheckPictures();
            CheckProjectile();
            CheckResources();
            CheckSkills();
            CheckInterface();
            CheckGradients();
            CheckDesigns();
            Sound.InitializeBASS();
            NetworkConfig.InitNetwork();
            Gui.Init();
            GameState.Ping = -1;
        }

        public static void CheckAnimations()
        {
            GameState.NumAnimations = GetFileCount(Core.Path.Animations);
        }

        public static void CheckCharacters()
        {
            GameState.NumCharacters = GetFileCount(Core.Path.Characters);
        }

        public static void CheckEmotes()
        {
            GameState.NumEmotes = GetFileCount(Core.Path.Emotes);
        }

        public static void CheckTilesets()
        {
            GameState.NumTileSets = GetFileCount(Core.Path.Tilesets);
        }

        public static void CheckFogs()
        {
            GameState.NumFogs = GetFileCount(Core.Path.Fogs);
        }

        public static void CheckItems()
        {
            GameState.NumItems = GetFileCount(Core.Path.Items);
        }

        public static void CheckPanoramas()
        {
            GameState.NumPanoramas = GetFileCount(Core.Path.Panoramas);
        }

        public static void CheckPaperdolls()
        {
            GameState.NumPaperdolls = GetFileCount(Core.Path.Paperdolls);
        }

        public static void CheckParallax()
        {
            GameState.NumParallax = GetFileCount(Core.Path.Parallax);
        }

        public static void CheckPictures()
        {
            GameState.NumPictures = GetFileCount(Core.Path.Pictures);
        }

        public static void CheckProjectile()
        {
            GameState.NumProjectiles = GetFileCount(Core.Path.Projectiles);
        }

        public static void CheckResources()
        {
            GameState.NumResources = GetFileCount(Core.Path.Resources);
        }

        public static void CheckSkills()
        {
            GameState.NumSkills = GetFileCount(Core.Path.Skills);
        }

        public static void CheckInterface()
        {
            GameState.NumInterface = GetFileCount(Core.Path.Gui);
        }

        public static void CheckGradients()
        {
            GameState.NumGradients = GetFileCount(Core.Path.Gradients);
        }

        public static void CheckDesigns()
        {
            GameState.NumDesigns = GetFileCount(Core.Path.Designs);
        }

        public static object GetResolutionSize(byte Resolution, ref int Width, ref int Height)
        {
            switch (Resolution)
            {
                case 1:
                    {
                        Width = 1920;
                        Height = 1080;
                        break;
                    }
                case 2:
                    {
                        Width = 1680;
                        Height = 1050;
                        break;
                    }
                case 3:
                    {
                        Width = 1600;
                        Height = 900;
                        break;
                    }
                case 4:
                    {
                        Width = 1440;
                        Height = 900;
                        break;
                    }
                case 5:
                    {
                        Width = 1440;
                        Height = 1050;
                        break;
                    }
                case 6:
                    {
                        Width = 1366;
                        Height = 768;
                        break;
                    }
                case 7:
                    {
                        Width = 1360;
                        Height = 1024;
                        break;
                    }
                case 8:
                    {
                        Width = 1360;
                        Height = 768;
                        break;
                    }
                case 9:
                    {
                        Width = 1280;
                        Height = 1024;
                        break;
                    }
                case 10:
                    {
                        Width = 1280;
                        Height = 800;
                        break;
                    }
                case 11:
                    {
                        Width = 1280;
                        Height = 768;
                        break;
                    }
                case 12:
                    {
                        Width = 1280;
                        Height = 720;
                        break;
                    }
                case 13:
                    {
                        Width = 1120;
                        Height = 864;
                        break;
                    }
                case 14:
                    {
                        Width = 1024;
                        Height = 768;
                        break;
                    }
            }

            return default;
        }

        public static void ClearGameData()
        {
            Map.ClearMap();
            Map.ClearMapNPCs();
            Map.ClearMapItems();
            Database.ClearNPCs();
            MapResource.ClearResources();
            Item.ClearItems();
            Shop.ClearShops();
            Database.ClearSkills();
            Animation.ClearAnimations();
            Projectile.ClearProjectile();
            Pet.ClearPets();
            Database.ClearJobs();
            Moral.ClearMorals();
            Bank.ClearBanks();
            Party.ClearParty();

            for (int i = 0; i < Constant.MAX_PLAYERS; i++)
                Player.ClearPlayer(i);

            Animation.ClearAnimInstances();
            Autotile.ClearAutotiles();

            // clear chat
            for (int i = 0; i < Constant.CHAT_LINES; i++)
                Core.Type.Chat[i].Text = "";
        }

        public static int GetFileCount(string folderName)
        {
            string folderPath = System.IO.Path.Combine(Core.Path.Graphics, folderName);
            if (Directory.Exists(folderPath))
            {
                return Directory.GetFiles(folderPath, "*.png").Length; // Adjust for other formats if needed
            }
            else
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return 0;
            }
        }

        public static void CacheMusic()
        {
            Sound.MusicCache = new string[Directory.GetFiles(Core.Path.Music, "*" + Settings.Instance.MusicExt).Count() + 1];
            string[] files = Directory.GetFiles(Core.Path.Music, "*" + Settings.Instance.MusicExt);
            string maxNum = Directory.GetFiles(Core.Path.Music, "*" + Settings.Instance.MusicExt).Count().ToString();
            int counter = 0;

            foreach (var FileName in files)
            {
                Sound.MusicCache[counter] = System.IO.Path.GetFileName(FileName);
                counter = counter + 1;
            }
        }

        public static void CacheSound()
        {
            Sound.SoundCache = new string[Directory.GetFiles(Core.Path.Sounds, "*" + Settings.Instance.SoundExt).Count() + 1];
            string[] files = Directory.GetFiles(Core.Path.Sounds, "*" + Settings.Instance.SoundExt);
            string maxNum = Directory.GetFiles(Core.Path.Sounds, "*" + Settings.Instance.SoundExt).Count().ToString();
            int counter = 0;

            foreach (var FileName in files)
            {
                Sound.SoundCache[counter] = System.IO.Path.GetFileName(FileName);
                counter = counter + 1;
            }
        }

        public static void GameInit()
        {
            // Send a request to the server to open the admin menu if the user wants it.
            if (Settings.Instance.OpenAdminPanelOnLogin == true)
            {
                if (GetPlayerAccess(GameState.MyIndex) > 0)
                {
                    NetworkSend.SendRequestAdmin();
                }
            }
        }

        public static void DestroyGame()
        {
            // break out of GameLoop
            GameState.InGame = false;
            GameState.InMenu = false;
            Sound.FreeBASS();
            NetworkConfig.DestroyNetwork();
            Environment.Exit(0);
        }

        // Get the shifted version of a digit key (for symbols)
        public static char GetShiftedDigit(char digit)
        {
            switch (digit)
            {
                case '1':
                    {
                        return '!';
                    }
                case '2':
                    {
                        return '@';
                    }
                case '3':
                    {
                        return '#';
                    }
                case '4':
                    {
                        return '$';
                    }
                case '5':
                    {
                        return '%';
                    }
                case '6':
                    {
                        return '^';
                    }
                case '7':
                    {
                        return '&';
                    }
                case '8':
                    {
                        return '*';
                    }
                case '9':
                    {
                        return '(';
                    }
                case '0':
                    {
                        return ')';
                    }

                default:
                    {
                        return digit;
                    }
            }
        }

        public static long IsEq(long StartX, long StartY)
        {
            long IsEqRet = default;
            Core.Type.RectStruct tempRec;
            long i;

            for (i = 0L; i < (int)Core.Enum.EquipmentType.Count; i++)
            {
                if (Conversions.ToBoolean(GetPlayerEquipment(GameState.MyIndex, (Core.Enum.EquipmentType)i)))
                {
                    tempRec.Top = StartY + GameState.EqTop + GameState.PicY * (i / GameState.EqColumns);
                    tempRec.Bottom = tempRec.Top + GameState.PicY;
                    tempRec.Left = StartX + GameState.EqLeft + (GameState.EqOffsetX + GameState.PicX) * (i % GameState.EqColumns);
                    tempRec.Right = tempRec.Left + GameState.PicX;

                    if (GameState.CurMouseX >= tempRec.Left & GameState.CurMouseX <= tempRec.Right)
                    {
                        if (GameState.CurMouseY >= tempRec.Top & GameState.CurMouseY <= tempRec.Bottom)
                        {
                            IsEqRet = i;
                            return IsEqRet;
                        }
                    }
                }
            }

            return IsEqRet;
        }

        public static long IsInv(long StartX, long StartY)
        {
            long IsInvRet = default;
            Core.Type.RectStruct tempRec;
            long i;

            for (i = 0L; i < Constant.MAX_INV; i++)
            {
                if (GetPlayerInv(GameState.MyIndex, (int)i) >= 0)
                {
                    tempRec.Top = StartY + GameState.InvTop + (GameState.InvOffsetY + GameState.PicY) * (i / GameState.InvColumns);
                    tempRec.Bottom = tempRec.Top + GameState.PicY;
                    tempRec.Left = StartX + GameState.InvLeft + (GameState.InvOffsetX + GameState.PicX) * (i % GameState.InvColumns);
                    tempRec.Right = tempRec.Left + GameState.PicX;

                    if (GameState.CurMouseX >= tempRec.Left & GameState.CurMouseX <= tempRec.Right)
                    {
                        if (GameState.CurMouseY >= tempRec.Top & GameState.CurMouseY <= tempRec.Bottom)
                        {
                            IsInvRet = i;
                            return IsInvRet;
                        }
                    }
                }
            }

            return -1;
        }

        public static long IsSkill(long StartX, long StartY)
        {
            long IsSkillRet = default;
            Core.Type.RectStruct tempRec;
            long i;

            for (i = 0L; i < Constant.MAX_PLAYER_SKILLS; i++)
            {
                if (Core.Type.Player[GameState.MyIndex].Skill[(int)i].Num >= 0)
                {
                    tempRec.Top = StartY + GameState.SkillTop + (GameState.SkillOffsetY + GameState.PicY) * (i / GameState.SkillColumns);
                    tempRec.Bottom = tempRec.Top + GameState.PicY;
                    tempRec.Left = StartX + GameState.SkillLeft + (GameState.SkillOffsetX + GameState.PicX) * (i % GameState.SkillColumns);
                    tempRec.Right = tempRec.Left + GameState.PicX;

                    if (GameState.CurMouseX >= tempRec.Left & GameState.CurMouseX <= tempRec.Right)
                    {
                        if (GameState.CurMouseY >= tempRec.Top & GameState.CurMouseY <= tempRec.Bottom)
                        {
                            IsSkillRet = i;
                            return IsSkillRet;
                        }
                    }
                }
            }

            return -1;
        }

        public static long IsBank(long StartX, long StartY)
        {
            byte IsBankRet = default;
            Core.Type.RectStruct tempRec;

            for (byte i = 0; i < Constant.MAX_BANK; i++)
            {
                if (GetBank(GameState.MyIndex, (byte)i) >= 0)
                {
                    tempRec.Top = StartY + GameState.BankTop + (GameState.BankOffsetY + GameState.PicY) * (i / GameState.BankColumns);
                    tempRec.Bottom = tempRec.Top + GameState.PicY;
                    tempRec.Left = StartX + GameState.BankLeft + (GameState.BankOffsetX + GameState.PicX) * (i % GameState.BankColumns);
                    tempRec.Right = tempRec.Left + GameState.PicX;

                    if (GameState.CurMouseX >= tempRec.Left & GameState.CurMouseX <= tempRec.Right)
                    {
                        if (GameState.CurMouseY >= tempRec.Top & GameState.CurMouseY <= tempRec.Bottom)
                        {
                            IsBankRet = i;
                            return IsBankRet;
                        }
                    }
                }

            }

            return -1;

        }

        public static long IsShop(long StartX, long StartY)
        {
            long IsShopRet = default;
            Core.Type.RectStruct tempRec;
            long i;

            for (i = 0L; i < Constant.MAX_TRADES; i++)
            {
                tempRec.Top = StartY + GameState.ShopTop + (GameState.ShopOffsetY + GameState.PicY) * (i / GameState.ShopColumns);
                tempRec.Bottom = tempRec.Top + GameState.PicY;
                tempRec.Left = StartX + GameState.ShopLeft + (GameState.ShopOffsetX + GameState.PicX) * (i % GameState.ShopColumns);
                tempRec.Right = tempRec.Left + GameState.PicX;

                if (GameState.CurMouseX >= tempRec.Left & GameState.CurMouseX <= tempRec.Right)
                {
                    if (GameState.CurMouseY >= tempRec.Top & GameState.CurMouseY <= tempRec.Bottom)
                    {
                        IsShopRet = i;
                        return IsShopRet;
                    }
                }
            }

            return -1;
        }

        public static long IsTrade(long StartX, long StartY)
        {
            long IsTradeRet = default;
            Core.Type.RectStruct tempRec;
            long i;

            for (i = 0L; i < Constant.MAX_INV; i++)
            {
                tempRec.Top = StartY + GameState.TradeTop + (GameState.TradeOffsetY + GameState.PicY) * (i / GameState.TradeColumns);
                tempRec.Bottom = tempRec.Top + GameState.PicY;
                tempRec.Left = StartX + GameState.TradeLeft + (GameState.TradeOffsetX + GameState.PicX) * (i % GameState.TradeColumns);
                tempRec.Right = tempRec.Left + GameState.PicX;

                if (GameState.CurMouseX >= tempRec.Left & GameState.CurMouseX <= tempRec.Right)
                {
                    if (GameState.CurMouseY >= tempRec.Top & GameState.CurMouseY <= tempRec.Bottom)
                    {
                        IsTradeRet = i;
                        return IsTradeRet;
                    }
                }
            }

            return -1;
        }

    }
}