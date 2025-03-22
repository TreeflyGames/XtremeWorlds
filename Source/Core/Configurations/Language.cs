using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Core
{

    public class Locales
    {
        public string GetValueByKey(List<KeyValuePair<string, string>> list, string key)
        {
            var pair = list.FirstOrDefault(kvp => kvp.Key == key);
            return pair.Equals(default(KeyValuePair<string, string>)) ? "Key not found" : pair.Value;
        }

        public string GetValueByKey(string listName, string key)
        {
            var property = this.GetType().GetProperty(listName);
            if (property == null)
                return "List not found";
            var list = (List<KeyValuePair<string, string>>)property.GetValue(this);
            return GetValueByKey(list, key);
        }

        public List<KeyValuePair<string, string>> Load { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Loading", "Loading..."),
                new KeyValuePair<string, string>("Graphics", "Initializing Graphics.."),
                new KeyValuePair<string, string>("Network", "Initializing Network..."),
                new KeyValuePair<string, string>("Starting", "Starting Game...")
            };

        public List<KeyValuePair<string, string>> MainMenu { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ServerStatus", "Server Status:"),
                new KeyValuePair<string, string>("ServerOnline", "Online"),
                new KeyValuePair<string, string>("ServerReconnect", "Reconnecting..."),
                new KeyValuePair<string, string>("ServerOffline", "Offline"),
                new KeyValuePair<string, string>("ButtonPlay", "Play"),
                new KeyValuePair<string, string>("ButtonRegister", "Register"),
                new KeyValuePair<string, string>("ButtonCredits", "Credits"),
                new KeyValuePair<string, string>("ButtonExit", "Exit"),
                new KeyValuePair<string, string>("NewsHeader", "Latest News"),
                new KeyValuePair<string, string>("News", @"Welcome To the XtremeWorlds.
                               This is a free open-source C# game engine!
                               For help or support please visit our site at
                               https://xtremeworlds.com/."),
                new KeyValuePair<string, string>("Login", "Login"),
                new KeyValuePair<string, string>("LoginName", "Name: "),
                new KeyValuePair<string, string>("LoginPass", "Password: "),
                new KeyValuePair<string, string>("LoginCheckBox", "Save Password?"),
                new KeyValuePair<string, string>("LoginButton", "Submit"),
                new KeyValuePair<string, string>("NewCharacter", "Create Character"),
                new KeyValuePair<string, string>("NewCharacterName", "Name: "),
                new KeyValuePair<string, string>("NewCharacterClass", "Class: "),
                new KeyValuePair<string, string>("NewCharacterGender", "Gender: "),
                new KeyValuePair<string, string>("NewCharacterMale", "Male"),
                new KeyValuePair<string, string>("NewCharacterFemale", "Female"),
                new KeyValuePair<string, string>("NewCharacterSprite", "Sprite"),
                new KeyValuePair<string, string>("NewCharacterButton", "Submit"),
                new KeyValuePair<string, string>("UseCharacter", "Character Selection"),
                new KeyValuePair<string, string>("UseCharacterNew", "New Character"),
                new KeyValuePair<string, string>("UseCharacterUse", "Use Character"),
                new KeyValuePair<string, string>("UseCharacterDel", "Delete Character"),
                new KeyValuePair<string, string>("Register", "Registration"),
                new KeyValuePair<string, string>("RegisterName", "Username: "),
                new KeyValuePair<string, string>("RegisterPass1", "Password: "),
                new KeyValuePair<string, string>("RegisterPass2", "Retype Password: "),
                new KeyValuePair<string, string>("Credits", "Credits"),
                new KeyValuePair<string, string>("StringLegal", "You cannot use high ASCII characters In your name, please re-enter."),
                new KeyValuePair<string, string>("SendLogin", "Connected, sending login information..."),
                new KeyValuePair<string, string>("SendNewCharacter", "Connected, sending character data..."),
                new KeyValuePair<string, string>("SendRegister", "Connected, sending registration information..."),
                new KeyValuePair<string, string>("ConnectToServer", "Connecting to Server...( {0} )")
            };

        public List<KeyValuePair<string, string>> Game { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Time", "Time: "),
                new KeyValuePair<string, string>("Fps", "Fps: "),
                new KeyValuePair<string, string>("Lps", "Lps: "),
                new KeyValuePair<string, string>("Ping", "Ping: "),
                new KeyValuePair<string, string>("PingSync", "Sync"),
                new KeyValuePair<string, string>("PingLocal", "Local"),
                new KeyValuePair<string, string>("MapReceive", "Receiving map..."),
                new KeyValuePair<string, string>("DataReceive", "Receiving game data..."),
                new KeyValuePair<string, string>("MapCurMap", "Map # {0}"),
                new KeyValuePair<string, string>("MapCurLoc", "Loc() x: {0} y: {1}"),
                new KeyValuePair<string, string>("MapLoc", "Cur Loc x: {0} y: {1}"),
                new KeyValuePair<string, string>("Fullscreen", "Please restart the client for the changes to take effect."),
                new KeyValuePair<string, string>("InvalidMap", "Invalid map index."),
                new KeyValuePair<string, string>("AccessDenied", "Access Denied!")
            };

        public List<KeyValuePair<string, string>> Chat { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Emote", "Usage : /emote [1-11]"),
                new KeyValuePair<string, string>("Info", "Usage : /info [player]"),
                new KeyValuePair<string, string>("Party", "Usage : /party [player]"),
                new KeyValuePair<string, string>("Trade", "Usage : /trade [player]"),
                new KeyValuePair<string, string>("PlayerMsg", "Usage : ![player] [message]"),
                new KeyValuePair<string, string>("InvalidCmd", "Not a valid command!"),
                new KeyValuePair<string, string>("Help1", "Social Commands : "),
                new KeyValuePair<string, string>("Help2", "'[message] = Global Message"),
                new KeyValuePair<string, string>("Help3", "-[message] = Party Message"),
                new KeyValuePair<string, string>("Help4", "![player] [message] = Player Message"),
                new KeyValuePair<string, string>("Help5", "@[message] = Admin Message"),
                new KeyValuePair<string, string>("Help6", @"Available Commands: /help, /info, 
                                /fps, /lps, /stats, /trade, 
                                /party, /join, /leave"),
                new KeyValuePair<string, string>("AdminGblMsg", "''msghere = Global Admin Message"),
                new KeyValuePair<string, string>("AdminPvtMsg", "= msghere = Private Admin Message"),
                new KeyValuePair<string, string>("Admin1", "Social Commands:"),
                new KeyValuePair<string, string>("Admin2", @"Available Commands: /admin, /who, /access, /loc, 
                                /warpmeto, /warptome, /warpto, 
                                /sprite, /mapreport, /kick, 
                                /ban, /respawn, /welcome,
                                /editmap, /edititem, /editresource,
                                /editskill, /editpet, /editshop
                                /editprojectile, /editnpc, /editjob
                                /editjob, /acp"),
                new KeyValuePair<string, string>("Welcome", "Usage : /welcome [message]"),
                new KeyValuePair<string, string>("Access", "Usage : /access [player] [access]"),
                new KeyValuePair<string, string>("Sprite", "Usage : /sprite [index]"),
                new KeyValuePair<string, string>("Kick", "Usage : /kick [player]"),
                new KeyValuePair<string, string>("Ban", "Usage : /ban [player]"),
                new KeyValuePair<string, string>("WarpMeTo", "Usage : /warpmeto [player]"),
                new KeyValuePair<string, string>("WarpToMe", "Usage : /warptome [player]"),
                new KeyValuePair<string, string>("WarpTo", "Usage : /warpto [map index]"),
            };

        public List<KeyValuePair<string, string>> ItemDescription { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("NotAvailable", "Not Available"),
                new KeyValuePair<string, string>("None", "None"),
                new KeyValuePair<string, string>("Seconds", "Seconds"),
                new KeyValuePair<string, string>("Currency", "Currency"),
                new KeyValuePair<string, string>("CommonEvent", "Event"),
                new KeyValuePair<string, string>("Furniture", "Furniture"),
                new KeyValuePair<string, string>("Potion", "Potion"),
                new KeyValuePair<string, string>("Skill", "Skill"),
                new KeyValuePair<string, string>("Weapon", "Weapon"),
                new KeyValuePair<string, string>("Armor", "Armor"),
                new KeyValuePair<string, string>("Helmet", "Helmet"),
                new KeyValuePair<string, string>("Shield", "Shield"),
                new KeyValuePair<string, string>("Shoes", "Shoes"),
                new KeyValuePair<string, string>("Gloves", "Gloves"),
                new KeyValuePair<string, string>("Amount", "Amount : "),
                new KeyValuePair<string, string>("Restore", "Restore Amount : "),
                new KeyValuePair<string, string>("Damage", "Damage : "),
                new KeyValuePair<string, string>("Defense", "Defense : ")
            };

        public List<KeyValuePair<string, string>> SkillDescription { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("No", "No"),
                new KeyValuePair<string, string>("None", "None"),
                new KeyValuePair<string, string>("Warp", "Warp"),
                new KeyValuePair<string, string>("Tiles", "Tiles"),
                new KeyValuePair<string, string>("SelfCast", "Self-Cast"),
                new KeyValuePair<string, string>("Gain", "Regen : "),
                new KeyValuePair<string, string>("GainHp", "Regen HP"),
                new KeyValuePair<string, string>("GainMp", "Regen MP"),
                new KeyValuePair<string, string>("Lose", "Syphon : "),
                new KeyValuePair<string, string>("LoseHp", "Syphon HP"),
                new KeyValuePair<string, string>("LoseMp", "Syphon MP")
            };

        public List<KeyValuePair<string, string>> Crafting { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("NotEnough", "Not enough materials!"),
                new KeyValuePair<string, string>("NotSelected", "Nothing selected")
            };

        public List<KeyValuePair<string, string>> Trade { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Request", "{0} is requesting to trade."),
                new KeyValuePair<string, string>("Timeout", "You took too long to decide. Please try again."),
                new KeyValuePair<string, string>("Value", "Total Value : {0}g"),
                new KeyValuePair<string, string>("StatusOther", "Other player confirmed offer."),
                new KeyValuePair<string, string>("StatusSelf", "You confirmed the offer.")
            };

        public List<KeyValuePair<string, string>> Events { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("OptContinue", "- Continue -")
            };

        public List<KeyValuePair<string, string>> Quest { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Cancel", "Cancel Started"),
                new KeyValuePair<string, string>("Started", "Quest Started"),
                new KeyValuePair<string, string>("Completed", "Quest Completed"),
                new KeyValuePair<string, string>("Slay", "Defeat {0}/{1} {2}."),
                new KeyValuePair<string, string>("Collect", "Collect {0}/{1} {2}."),
                new KeyValuePair<string, string>("Talk", "Go talk To {0}."),
                new KeyValuePair<string, string>("Reach", "Go To {0}."),
                new KeyValuePair<string, string>("TurnIn", "Give {0} the {1} {2}/{3} they requested."),
                new KeyValuePair<string, string>("Kill", "Defeat {0}/{1} Players In Battle."),
                new KeyValuePair<string, string>("Gather", "Gather {0}/{1} {2}."),
                new KeyValuePair<string, string>("Fetch", "Fetch {0} X {1} from {2}.")
            };

        public List<KeyValuePair<string, string>> Character { get; set; } = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("PName", "Name: "),
                new KeyValuePair<string, string>("JobType", "Job: "),
                new KeyValuePair<string, string>("Level", "Lv: "),
                new KeyValuePair<string, string>("Exp", "Exp: "),
                new KeyValuePair<string, string>("StatsLabel", "Stats:"),
                new KeyValuePair<string, string>("Strength", "Strength: "),
                new KeyValuePair<string, string>("Endurance", "Endurance: "),
                new KeyValuePair<string, string>("Vitality", "Vitality: "),
                new KeyValuePair<string, string>("Intelligence", "Intelligence: "),
                new KeyValuePair<string, string>("Luck", "Luck: "),
                new KeyValuePair<string, string>("Spirit", "Spirit: "),
                new KeyValuePair<string, string>("Points", "Points Available: "),
                new KeyValuePair<string, string>("SkillLabel", "Skills:")
            };
    }

    public class LocalesManager
    {
        public static Locales Language = new Locales();

        public static void Load()
        {
            string cf = Path.Config;
            var x = new XmlSerializer(typeof(Locales), new XmlRootAttribute("Language"));

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
            Language = (Locales)x.Deserialize(reader);
            reader.Close();
        }
    }
}