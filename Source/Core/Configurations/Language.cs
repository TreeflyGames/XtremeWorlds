using System.IO;
using System.Xml.Serialization;

namespace Core
{

    public class LanguageDef
    {

        public LoadDef Load = new LoadDef();

        public class LoadDef
        {

            public string Loading = "Loading...";
            public string Graphics = "Initializing Graphics..";
            public string Network = "Initializing Network...";
            public string Starting = "Starting Game...";

        }

        public MainMenuDef MainMenu = new MainMenuDef();
        public class MainMenuDef
        {

            // Main Panel
            public string ServerStatus = "Server Status:";
            public string ServerOnline = "Online";
            public string ServerReconnect = "Reconnecting...";
            public string ServerOffline = "Offline";
            public string ButtonPlay = "Play";
            public string ButtonRegister = "Register";
            public string ButtonCredits = "Credits";
            public string ButtonExit = "Exit";
            public string NewsHeader = "Latest News";
            public string News = @"Welcome To the XtremeWorlds.
                                 This is a free open source VB.Net game engine!
                                 For help or support please visit our site at
                                 https://xtremeworlds.com/.";

            // Login Panel
            public string Login = "Login";
            public string LoginName = "Name: ";
            public string LoginPass = "Password: ";
            public string LoginCheckBox = "Save Password?";
            public string LoginButton = "Submit";

            // New Character Panel
            public string NewCharacter = "Create Character";
            public string NewCharacterName = "Name: ";
            public string NewCharacterClass = "Class: ";
            public string NewCharacterGender = "Gender: ";
            public string NewCharacterMale = "Male";
            public string NewCharacterFemale = "Female";
            public string NewCharacterSprite = "Sprite";
            public string NewCharacterButton = "Submit";

            // Character Select
            public string UseCharacter = "Character Selection";
            public string UseCharacterNew = "New Character";
            public string UseCharacterUse = "Use Character";
            public string UseCharacterDel = "Delete Character";

            // Register
            public string Register = "Registration";
            public string RegisterName = "Username: ";
            public string RegisterPass1 = "Password: ";
            public string RegisterPass2 = "Retype Password: ";

            // Credits
            public string Credits = "Credits";

            // Misc
            public string StringLegal = "You cannot use high ASCII characters In your name, please re-enter.";
            public string SendLogin = "Connected, sending login information...";
            public string SendNewCharacter = "Connected, sending character data...";
            public string SendRegister = "Connected, sending registration information...";
            public string ConnectToServer = "Connecting to Server...( {0} )";
        }

        public GameDef Game = new GameDef();
        public class GameDef
        {
            public string MapName = "";
            public string Time = "Time: ";
            public string Fps = "Fps: ";
            public string Lps = "Lps: ";

            public string Ping = "Ping: ";
            public string PingSync = "Sync";
            public string PingLocal = "Local";

            public string MapReceive = "Receiving MyMap...";
            public string DataReceive = "Receiving game data...";

            public string MapCurMap = "Map # {0}";
            public string MapCurLoc = "Loc() x: {0} y: {1}";
            public string MapLoc = "Cur Loc x: {0} y: {1}";

            public string Fullscreen = "Please restart the client for the changes to take effect.";
        }

        public ChatDef Chat = new ChatDef();
        public class ChatDef
        {

            // Universal
            public string Emote = "Usage : /emote [1-11]";
            public string Info = "Usage : /info [player]";
            public string Party = "Usage : /party [player]";
            public string Trade = "Usage : /trade [player]";
            public string PlayerMsg = "Usage : ![player] [message]";
            public string InvalidCmd = "Not a valid command!";
            public string Help1 = "Social Commands : ";
            public string Help2 = "'[message] = Global Message";
            public string Help3 = "-[message] = Party Message";
            public string Help4 = "![player] [message] = Player Message";
            public string Help5 = "@[message] = Admin Message";
            public string Help6 = @"Available Commands: /help, /info, 
                                  /fps, /lps, /stats, /trade, 
                                  /party, /join, /leave";


            // Admin-Only
            public string AccessAlert = "You need a higher access to do this!";
            public string AdminGblMsg = "''msghere = Global Admin Message";
            public string AdminPvtMsg = "= msghere = Private Admin Message";
            public string Admin1 = "Social Commands:";
            public string Admin2 = @"Available Commands: /admin, /who, /access, /loc, 
                                   /warpmeto, /warptome, /warpto, 
                                   /sprite, /mapreport, /kick, 
                                   /ban, /respawn, /welcome,
                                   /editmap, /edititem, /editresource,
                                   /editskill, /editpet, /editshop
                                   /editprojectile, /editnpc, /editjob
                                   /editjob, /acp";

            public string Welcome = "Usage : /welcome [message]";
            public string Access = "Usage : /access [player] [access]";
            public string Sprite = "Usage : /sprite [index]";
            public string Kick = "Usage : /kick [player]";
            public string Ban = "Usage : /ban [player]";

            public string WarpMeTo = "Usage : /warpmeto [player]";
            public string WarpToMe = "Usage : /warptome [player]";
            public string WarpTo = "Usage : /warpto [map index]";

            public string InvalidMap = "Invalid map index.";
            public string InvalidQuest = "Invalid quest index.";

        }

        public ItemDescriptionDef ItemDescription = new ItemDescriptionDef();

        public class ItemDescriptionDef
        {

            public string NotAvailable = "Not Available";
            public string None = "None";
            public string Seconds = "Seconds";

            public string Currency = "Currency";
            public string CommonEvent = "Event";
            public string Furniture = "Furniture";
            public string Potion = "Potion";
            public string Skill = "Skill";

            public string Weapon = "Weapon";
            public string Armor = "Armor";
            public string Helmet = "Helmet";
            public string Shield = "Shield";
            public string Shoes = "Shoes";
            public string Gloves = "Gloves";

            public string Amount = "Amount : ";
            public string Restore = "Restore Amount : ";
            public string Damage = "Damage : ";
            public string Defense = "Defense : ";

        }

        public SkillDescriptionDef SkillDescription = new SkillDescriptionDef();
        public class SkillDescriptionDef
        {

            public string No = "No";
            public string None = "None";
            public string Warp = "Warp";
            public string Tiles = "Tiles";
            public string SelfCast = "Self-Cast";

            public string Gain = "Regen : ";
            public string GainHp = "Regen HP";
            public string GainMp = "Regen MP";
            public string Lose = "Syphon : ";
            public string LoseHp = "Syphon HP";
            public string LoseMp = "Syphon MP";

        }

        public CraftingDef Crafting = new CraftingDef();
        public class CraftingDef
        {

            public string NotEnough = "Not enough materials!";
            public string NotSelected = "Nothing selected";

        }

        public TradeDef Trade = new TradeDef();
        public class TradeDef
        {

            public string Request = "{0} is requesting to trade.";
            public string Timeout = "You took too long to decide. Please try again.";

            public string Value = "Total Value : {0}g";

            public string StatusOther = "Other player confirmed offer.";
            public string StatusSelf = "You confirmed the offer.";

        }

        public EventDef Events = new EventDef();
        public class EventDef
        {

            public object OptContinue = "- Continue -";

        }

        public QuestDef Quest = new QuestDef();
        public class QuestDef
        {

            public string Cancel = "Cancel Started";
            public string Started = "Quest Started";
            public string Completed = "Quest Completed";

            // Quest Type
            public string Slay = "Defeat {0}/{1} {2}.";
            public string Collect = "Collect {0}/{1} {2}.";
            public string Talk = "Go talk To {0}.";
            public string Reach = "Go To {0}.";
            public string TurnIn = "Give {0} the {1} {2}/{3} they requested.";
            public string Kill = "Defeat {0}/{1} Players In Battle.";
            public string Gather = "Gather {0}/{1} {2}.";
            public string Fetch = "Fetch {0} X {1} from {2}.";

        }

        public CharacterDef Character = new CharacterDef();
        public class CharacterDef
        {

            public string PName = "Name: ";
            public string JobType = "Job: ";
            public string Level = "Lv: ";
            public string Exp = "Exp: ";

            public string StatsLabel = "Stats:";
            public string Strength = "Strength: ";
            public string Endurance = "Endurance: ";
            public string Vitality = "Vitality: ";
            public string Intelligence = "Intelligence: ";
            public string Luck = "Luck: ";
            public string Spirit = "Spirit: ";
            public string Points = "Points Available: ";

            public string SkillLabel = "Skills:";

        }
    }

    public class Languages
    {
        public static LanguageDef Language = new LanguageDef();

        public static void Load()
        {
            string cf = Path.Config;
            var x = new XmlSerializer(typeof(LanguageDef), new XmlRootAttribute("Language"));

            Directory.CreateDirectory(cf);
            cf += "Language.xml";

            if (!File.Exists(cf))
            {
                File.Create(cf).Dispose();
                var writer = new StreamWriter(cf);
                x.Serialize(writer, Language);
                writer.Close();
            }

            var reader = new StreamReader(cf);
            Language = (LanguageDef)x.Deserialize(reader);
            reader.Close();
        }
    }
}