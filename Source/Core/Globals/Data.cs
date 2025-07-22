using Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Type;

namespace Core
{
    public class Data
    {
        // Common data structure arrays with improved initialization
        public static Job[] Job = new Job[Constant.MAX_JOBS];
        public static Moral[] Moral = new Moral[Constant.MAX_MORALS];
        public static Item[] Item = new Item[Constant.MAX_ITEMS];
        public static Npc[] Npc = new Npc[Constant.MAX_NPCS];
        public static Shop[] Shop = new Shop[Constant.MAX_SHOPS];
        public static Skill[] Skill = new Skill[Constant.MAX_SKILLS];
        public static MapResource[] MapResource = new MapResource[Constant.MAX_RESOURCES];
        public static MapResourceCache[] MyMapResource = new MapResourceCache[Constant.MAX_RESOURCES];
        public static Animation[] Animation = new Animation[Constant.MAX_ANIMATIONS];
        public static Map[] Map = new Map[Constant.MAX_MAPS];
        public static Map MyMap;
        public static Tile[,] TempTile;
        public static MapItem[,] MapItem = new MapItem[Constant.MAX_MAPS, Constant.MAX_MAP_ITEMS];
        public static MapItem[] MyMapItem = new MapItem[Constant.MAX_MAP_ITEMS];
        public static MapData[] MapNpc = new MapData[Constant.MAX_MAPS];
        public static MapNpc[] MyMapNpc = new MapNpc[Constant.MAX_MAP_NPCS];
        public static Bank[] Bank = new Bank[Constant.MAX_PLAYERS];
        public static TempPlayer[] TempPlayer = new TempPlayer[Constant.MAX_PLAYERS];
        public static Account[] Account = new Account[Constant.MAX_PLAYERS];
        public static Player[] Player = new Player[Constant.MAX_PLAYERS];
        public static Projectile[] Projectile = new Projectile[Constant.MAX_PROJECTILES];
        public static MapProjectile[,] MapProjectile = new MapProjectile[Constant.MAX_MAPS, Constant.MAX_PROJECTILES];
        public static PlayerInv[] TradeYourOffer = new PlayerInv[Constant.MAX_INV];
        public static PlayerInv[] TradeTheirOffer = new PlayerInv[Constant.MAX_INV];
        public static Party[] Party = new Party[Constant.MAX_PARTY];
        public static Party MyParty;
        public static Resource[] Resource = new Resource[Constant.MAX_RESOURCES];
        public static CharList Char;
        public static Pet[] Pet = new Pet[Constant.MAX_PETS];
        public static ChatBubble[] ChatBubble = new ChatBubble[byte.MaxValue];
        public static Script Script = new Script();

        public static Quest[] Quests = new Quest[Constant.MAX_QUESTS];
        public static Event[] Events = new Event[Constant.MAX_EVENTS];
        public static Guild[] Guilds = new Guild[Constant.MAX_GUILDS];
        public static Weather Weather = new Weather();

        public static ActionMsg[] ActionMsg = new ActionMsg[byte.MaxValue];
        public static Blood[] Blood = new Blood[byte.MaxValue];
        public static Chat[] Chat = new Chat[Constant.CHAT_LINES];
        public static Tile[,] MapTile;
        public static TileHistory[] TileHistory;
        public static Autotile[,] Autotile;
        public static MapEvent[] MapEvents;

        public static UI UI;
    }
}
