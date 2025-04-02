    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
using Core;

namespace Core
    {
        [XmlRoot("Locales")]
        public class Locales
        {
            [XmlElement("Load")]
            public List<LocaleItem> Load { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "Loading", Value = "Loading..." },
                new LocaleItem { Key = "Graphics", Value = "Initializing Graphics.." },
                new LocaleItem { Key = "Network", Value = "Initializing Network..." },
                new LocaleItem { Key = "Starting", Value = "Starting Game..." }
            };

            [XmlElement("MainMenu")]
            public List<LocaleItem> MainMenu { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "ServerStatus", Value = "Server Status:" },
                new LocaleItem { Key = "ServerOnline", Value = "Online" },
                new LocaleItem { Key = "ServerReconnect", Value = "Reconnecting..." },
                new LocaleItem { Key = "ServerOffline", Value = "Offline" },
                new LocaleItem { Key = "ButtonPlay", Value = "Play" },
                new LocaleItem { Key = "ButtonRegister", Value = "Register" },
                new LocaleItem { Key = "ButtonCredits", Value = "Credits" },
                new LocaleItem { Key = "ButtonExit", Value = "Exit" },
                new LocaleItem { Key = "NewsHeader", Value = "Latest News" },
                new LocaleItem { Key = "News", Value = @"Welcome To the XtremeWorlds.
                                   This is a free open-source C# game engine!
                                   For help or support please visit our site at
                                   https://xtremeworlds.com/." },
                new LocaleItem { Key = "Login", Value = "Login" },
                new LocaleItem { Key = "LoginName", Value = "Name: " },
                new LocaleItem { Key = "LoginPass", Value = "Password: " },
                new LocaleItem { Key = "LoginCheckBox", Value = "Save Password?" },
                new LocaleItem { Key = "LoginButton", Value = "Submit" },
                new LocaleItem { Key = "NewCharacter", Value = "Create Character" },
                new LocaleItem { Key = "NewCharacterName", Value = "Name: " },
                new LocaleItem { Key = "NewCharacterClass", Value = "Class: " },
                new LocaleItem { Key = "NewCharacterGender", Value = "Gender: " },
                new LocaleItem { Key = "NewCharacterMale", Value = "Male" },
                new LocaleItem { Key = "NewCharacterFemale", Value = "Female" },
                new LocaleItem { Key = "NewCharacterSprite", Value = "Sprite" },
                new LocaleItem { Key = "NewCharacterButton", Value = "Submit" },
                new LocaleItem { Key = "UseCharacter", Value = "Character Selection" },
                new LocaleItem { Key = "UseCharacterNew", Value = "New Character" },
                new LocaleItem { Key = "UseCharacterUse", Value = "Use Character" },
                new LocaleItem { Key = "UseCharacterDel", Value = "Delete Character" },
                new LocaleItem { Key = "Register", Value = "Registration" },
                new LocaleItem { Key = "RegisterName", Value = "Username: " },
                new LocaleItem { Key = "RegisterPass1", Value = "Password: " },
                new LocaleItem { Key = "RegisterPass2", Value = "Retype Password: " },
                new LocaleItem { Key = "Credits", Value = "Credits" },
                new LocaleItem { Key = "StringLegal", Value = "You cannot use high ASCII characters In your name, please re-enter." },
                new LocaleItem { Key = "SendLogin", Value = "Connected, sending login information..." },
                new LocaleItem { Key = "SendNewCharacter", Value = "Connected, sending character data..." },
                new LocaleItem { Key = "SendRegister", Value = "Connected, sending registration information..." },
                new LocaleItem { Key = "ConnectToServer", Value = "Connecting to Server...( {0} )" }
            };

            [XmlElement("Game")]
            public List<LocaleItem> Game { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "Time", Value = "Time: " },
                new LocaleItem { Key = "Fps", Value = "Fps: " },
                new LocaleItem { Key = "Lps", Value = "Lps: " },
                new LocaleItem { Key = "Ping", Value = "Ping: " },
                new LocaleItem { Key = "PingSync", Value = "Sync" },
                new LocaleItem { Key = "PingLocal", Value = "Local" },
                new LocaleItem { Key = "MapReceive", Value = "Receiving map..." },
                new LocaleItem { Key = "DataReceive", Value = "Receiving game data..." },
                new LocaleItem { Key = "MapCurMap", Value = "Map # {0}" },
                new LocaleItem { Key = "MapCurLoc", Value = "Loc() x: {0} y: {1}" },
                new LocaleItem { Key = "MapLoc", Value = "Cur Loc x: {0} y: {1}" },
                new LocaleItem { Key = "Fullscreen", Value = "Please restart the client for the changes to take effect." },
                new LocaleItem { Key = "InvalidMap", Value = "Invalid map index." },
                new LocaleItem { Key = "AccessDenied", Value = "Access Denied!" }
            };

            [XmlElement("Chat")]
            public List<LocaleItem> Chat { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "Emote", Value = "Usage : /emote [1-11]" },
                new LocaleItem { Key = "Info", Value = "Usage : /info [player]" },
                new LocaleItem { Key = "Party", Value = "Usage : /party [player]" },
                new LocaleItem { Key = "Trade", Value = "Usage : /trade [player]" },
                new LocaleItem { Key = "PlayerMsg", Value = "Usage : ![player] [message]" },
                new LocaleItem { Key = "InvalidCmd", Value = "Not a valid command!" },
                new LocaleItem { Key = "Help1", Value = "Social Commands : " },
                new LocaleItem { Key = "Help2", Value = "'[message] = Global Message" },
                new LocaleItem { Key = "Help3", Value = "-[message] = Party Message" },
                new LocaleItem { Key = "Help4", Value = "![player] [message] = Player Message" },
                new LocaleItem { Key = "Help5", Value = "@[message] = Admin Message" },
                new LocaleItem { Key = "Help6", Value = @"Available Commands: /help, /info, 
                                    /fps, /lps, /stats, /trade, 
                                    /party, /join, /leave" },
                new LocaleItem { Key = "AdminGblMsg", Value = "''msghere = Global Admin Message" },
                new LocaleItem { Key = "AdminPvtMsg", Value = "= msghere = Private Admin Message" },
                new LocaleItem { Key = "Admin1", Value = "Social Commands:" },
                new LocaleItem { Key = "Admin2", Value = @"Available Commands: /admin, /who, /access, /loc, 
                                    /warpmeto, /warptome, /warpto, 
                                    /sprite, /mapreport, /kick, 
                                    /ban, /respawn, /welcome,
                                    /editmap, /edititem, /editresource,
                                    /editskill, /editpet, /editshop
                                    /editprojectile, /editnpc, /editjob
                                    /editjob, /acp" },
                new LocaleItem { Key = "Welcome", Value = "Usage : /welcome [message]" },
                new LocaleItem { Key = "Access", Value = "Usage : /access [player] [access]" },
                new LocaleItem { Key = "Sprite", Value = "Usage : /sprite [index]" },
                new LocaleItem { Key = "Kick", Value = "Usage : /kick [player]" },
                new LocaleItem { Key = "Ban", Value = "Usage : /ban [player]" },
                new LocaleItem { Key = "WarpMeTo", Value = "Usage : /warpmeto [player]" },
                new LocaleItem { Key = "WarpToMe", Value = "Usage : /warptome [player]" },
                new LocaleItem { Key = "WarpTo", Value = "Usage : /warpto [map index]" }
            };

            [XmlElement("ItemDescription")]
            public List<LocaleItem> ItemDescription { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "NotAvailable", Value = "Not Available" },
                new LocaleItem { Key = "None", Value = "None" },
                new LocaleItem { Key = "Seconds", Value = "Seconds" },
                new LocaleItem { Key = "Currency", Value = "Currency" },
                new LocaleItem { Key = "CommonEvent", Value = "Event" },
                new LocaleItem { Key = "Potion", Value = "Potion" },
                new LocaleItem { Key = "Skill", Value = "Skill" },
                new LocaleItem { Key = "Weapon", Value = "Weapon" },
                new LocaleItem { Key = "Armor", Value = "Armor" },
                new LocaleItem { Key = "Helmet", Value = "Helmet" },
                new LocaleItem { Key = "Shield", Value = "Shield" },
                new LocaleItem { Key = "Shoes", Value = "Shoes" },
                new LocaleItem { Key = "Gloves", Value = "Gloves" },
                new LocaleItem { Key = "Amount", Value = "Amount : " },
                new LocaleItem { Key = "Restore", Value = "Restore Amount : " },
                new LocaleItem { Key = "Damage", Value = "Damage : " },
                new LocaleItem { Key = "Defense", Value = "Defense : " }
            };

            [XmlElement("SkillDescription")]
            public List<LocaleItem> SkillDescription { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "No", Value = "No" },
                new LocaleItem { Key = "None", Value = "None" },
                new LocaleItem { Key = "Warp", Value = "Warp" },
                new LocaleItem { Key = "Tiles", Value = "Tiles" },
                new LocaleItem { Key = "SelfCast", Value = "Self-Cast" },
                new LocaleItem { Key = "Gain", Value = "Regen : " },
                new LocaleItem { Key = "GainHp", Value = "Regen HP" },
                new LocaleItem { Key = "GainMp", Value = "Regen MP" },
                new LocaleItem { Key = "Lose", Value = "Syphon : " },
                new LocaleItem { Key = "LoseHp", Value = "Syphon HP" },
                new LocaleItem { Key = "LoseMp", Value = "Syphon MP" }
            };

            [XmlElement("Crafting")]
            public List<LocaleItem> Crafting { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "NotEnough", Value = "Not enough materials!" },
                new LocaleItem { Key = "NotSelected", Value = "Nothing selected" }
            };

            [XmlElement("Trade")]
            public List<LocaleItem> Trade { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "Request", Value = "{0} is requesting to trade." },
                new LocaleItem { Key = "Timeout", Value = "You took too long to decide. Please try again." },
                new LocaleItem { Key = "Value", Value = "Total Value : {0}g" },
                new LocaleItem { Key = "StatusOther", Value = "Other player confirmed offer." },
                new LocaleItem { Key = "StatusSelf", Value = "You confirmed the offer." }
            };

            [XmlElement("Events")]
            public List<LocaleItem> Events { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "OptContinue", Value = "- Continue -" }
            };

            [XmlElement("Quest")]
            public List<LocaleItem> Quest { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "Cancel", Value = "Cancel Started" },
                new LocaleItem { Key = "Started", Value = "Quest Started" },
                new LocaleItem { Key = "Completed", Value = "Quest Completed" },
                new LocaleItem { Key = "Slay", Value = "Defeat {0}/{1} {2}." },
                new LocaleItem { Key = "Collect", Value = "Collect {0}/{1} {2}." },
                new LocaleItem { Key = "Talk", Value = "Go talk To {0}." },
                new LocaleItem { Key = "Reach", Value = "Go To {0}." },
                new LocaleItem { Key = "TurnIn", Value = "Give {0} the {1} {2}/{3} they requested." },
                new LocaleItem { Key = "Kill", Value = "Defeat {0}/{1} Players In Battle." },
                new LocaleItem { Key = "Gather", Value = "Gather {0}/{1} {2}." },
                new LocaleItem { Key = "Fetch", Value = "Fetch {0} X {1} from {2}." }
            };

            [XmlElement("Character")]
            public List<LocaleItem> Character { get; set; } = new List<LocaleItem>
            {
                new LocaleItem { Key = "PName", Value = "Name: " },
                new LocaleItem { Key = "JobType", Value = "Job: " },
                new LocaleItem { Key = "Level", Value = "Lv: " },
                new LocaleItem { Key = "Exp", Value = "Exp: " },
                new LocaleItem { Key = "StatsLabel", Value = "Stats:" },
                new LocaleItem { Key = "Strength", Value = "Strength: " },
                new LocaleItem { Key = "Endurance", Value = "Endurance: " },
                new LocaleItem { Key = "Vitality", Value = "Vitality: " },
                new LocaleItem { Key = "Intelligence", Value = "Intelligence: " },
                new LocaleItem { Key = "Luck", Value = "Luck: " },
                new LocaleItem { Key = "Spirit", Value = "Spirit: " },
                new LocaleItem { Key = "Points", Value = "Points Available: " },
                new LocaleItem { Key = "SkillLabel", Value = "Skills:" }
            };

            public string GetValueByKey(List<LocaleItem> list, string key)
            {
                var item = list.FirstOrDefault(kvp => kvp.Key == key);
                return item == null ? "Key not found" : item.Value;
            }

            public string GetValueByKey(string listName, string key)
            {
                var property = this.GetType().GetProperty(listName);
                if (property == null)
                    return "List not found";
                var list = (List<LocaleItem>)property.GetValue(this);
                return GetValueByKey(list, key);
            }
        }

        public class LocaleItem
        {
            [XmlAttribute("Key")]
            public string Key { get; set; }

            [XmlAttribute("Value")]
            public string Value { get; set; }
        }

    public class LocalesManager
    {
        public static Locales Language = new Locales();

        public static void Load()
        {
            var x = new XmlSerializer(typeof(Locales), new XmlRootAttribute("Locales"));
            string languagePath = Core.Path.Config;
            string languageFile = System.IO.Path.Combine(languagePath, "Locales.xml");

            #if ANDROID
            #else
                Directory.CreateDirectory(languagePath);
            #endif
            if (!File.Exists(languageFile))
            {
            #if ANDROID
            #else  
                using (var writer = new StreamWriter(languageFile))
                {
  
                        x.Serialize(writer, new Locales()); // Save with default values
                    
                }
            #endif
            }
            else
            {
                using (var reader = new StreamReader(languageFile))
                {
                    Language = (Locales)x.Deserialize(reader);
                }
            }
        }
    }
}