using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Core.Global
{
    public class Command
    {
        public static string GetAccountLogin(int index)
        {
            return Data.Account[index].Login;
        }

        public static string GetAccountPasword(int index)
        {
            return Data.Account[index].Password;
        }

        public static int GetEntityMaxVital(int index, Vital Vital)
        {
            switch (Vital)
            {
                case Vital.Health:
                    return (int)Math.Round(100d + (Data.Player[index].Level + GetEntityStat(index, Stat.Vitality) / 2d) * 2d);
                case Vital.Mana:
                    return (int)Math.Round(50d + (Data.Player[index].Level + GetEntityStat(index, Stat.Intelligence) / 2d) * 2d);
                case Vital.Stamina:
                    return (int)Math.Round(50d + (Data.Player[index].Level + GetEntityStat(index, Stat.Spirit) / 2d) * 2d);
                default:
                    return 0;
            }
        }

        public static int GetEntityStat(int index, Stat Stat)
        {
            int x = Data.Player[index].Stat[(int)Stat];
            var count = Enum.GetNames(typeof(Equipment)).Length;
            for (int i = 0; i < (int)count; i++)
            {
                if (Data.Player[index].Equipment[i] >= 0)
                {
                    if (Data.Item[(int)Data.Player[index].Equipment[i]].Add_Stat[(int)Stat] > 0)
                    {
                        x += Data.Item[(int)Data.Player[index].Equipment[i]].Add_Stat[(int)Stat];
                    }
                }
            }
            return x;
        }

        public static int GetEntityAccess(int index)
        {
            return Data.Player[index].Access;
        }

        public static int GetEntityX(int index)
        {
            return Data.Player[index].X;
        }

        public static int GetEntityY(int index)
        {
            return Data.Player[index].Y;
        }

        public static byte GetEntityDir(int index)
        {
            return Data.Player[index].Dir;
        }

        public static bool GetEntityPK(int index)
        {
            return Data.Player[index].PK;
        }

        public static int GetEntityNextLevel(int index)
        {
            return (int)Math.Round(50d / 3d * (Math.Pow(GetEntityLevel(index) + 1, 3d) - 6d * Math.Pow(GetEntityLevel(index) + 1, 2d) + 17 * (GetEntityLevel(index) + 1) - 12d));
        }

        public static int GetEntityExp(int index)
        {
            return Data.Player[index].Exp;
        }

        public static int GetEntityRawStat(int index, Stat Stat)
        {
            return Data.Player[index].Stat[(int)Stat];
        }

        public static string GetEntityName(int index)
        {
            return Data.Player[index].Name;
        }

        public static int GetEntityGatherSkillLvl(int index, int skillSlot)
        {
            return Data.Player[index].GatherSkills[skillSlot].SkillLevel;
        }

        public static int GetEntityGatherSkillExp(int index, int skillSlot)
        {
            return Data.Player[index].GatherSkills[skillSlot].SkillCurExp;
        }

        public static int GetEntityGatherSkillMaxExp(int index, int skillSlot)
        {
            return Data.Player[index].GatherSkills[skillSlot].SkillNextLvlExp;
        }

        public static int GetEntityInv(int index, int invslot)
        {
            return Data.Player[index].Inv[invslot].Num;
        }

        public static int GetEntityInvValue(int index, int invslot)
        {
            return Data.Player[index].Inv[invslot].Value;
        }

        public static int GetEntityPoints(int index)
        {
            return Data.Player[index].Points;
        }

        public static int GetEntityVital(int index, Vital vital)
        {
            return Data.Player[index].Vital[(int)vital];
        }

        public static int GetEntitySprite(int index)
        {
            return Data.Player[index].Sprite;
        }

        public static int GetEntityJob(int index)
        {
            return Data.Player[index].Job;
        }

        public static int GetEntityMap(int index)
        {
            return Data.Player[index].Map;
        }

        public static int GetEntityLevel(int index)
        {
            return Data.Player[index].Level;
        }

        public static int GetEntityEquipment(int index, Equipment equipmentSlot)
        {
            return Data.Player[index].Equipment[(int)equipmentSlot];
        }

        public static int GetEntitySkill(int index, int skillSlot)
        {
            return Data.Player[index].Skill[skillSlot].Num;
        }

        public static int GetEntitySkillCD(int index, int skillSlot)
        {
            return Data.Player[index].Skill[skillSlot].CD;
        }

        public static void SetPlayerLogin(int index, string login)
        {
            Data.Account[index].Login = login;
        }

        public static string GetPlayerPassword(int index)
        {
            string GetPlayerPasswordRet = default;
            GetPlayerPasswordRet = Data.Account[index].Password;
            return GetPlayerPasswordRet;
        }

        public static void SetPlayerPassword(int index, string password)
        {
            Data.Account[index].Password = password;
        }
        public static int GetPlayerMaxVital(int index, Vital Vital)
        {
            int GetPlayerMaxVitalRet = default;
            switch (Vital)
            {
                case Vital.Health:
                    {
                        GetPlayerMaxVitalRet = (int)Math.Round(100d + (Data.Player[index].Level + GetPlayerStat(index, Stat.Vitality) / 2d) * 2d);
                        break;
                    }

                case Vital.Mana:
                    {
                        GetPlayerMaxVitalRet = (int)Math.Round(50d + (Data.Player[index].Level + GetPlayerStat(index, Stat.Intelligence) / 2d) * 2d);
                        break;

                    }
                case Vital.Stamina:
                    {
                        GetPlayerMaxVitalRet = (int)Math.Round(50d + (Data.Player[index].Level + GetPlayerStat(index, Stat.Spirit) / 2d) * 2d);
                        break;
                    }
            }

            return GetPlayerMaxVitalRet;
        }

        public static int GetPlayerStat(int index, Stat Stat)
        {
            int GetPlayerStatRet = default;
            int x;
            int i;

            x = Data.Player[index].Stat[(int)Stat];
            var count = Enum.GetNames(typeof(Equipment)).Length;

            for (i = 0; i < count; i++)
            {
                if (Data.Player[index].Equipment[i] >= 0)
                {
                    if (Data.Item[(int)Data.Player[index].Equipment[i]].Add_Stat[(int)Stat] > 0)
                    {
                        x += Data.Item[(int)Data.Player[index].Equipment[i]].Add_Stat[(int)Stat];
                    }
                }
            }

            GetPlayerStatRet = x;
            return GetPlayerStatRet;
        }

        public static int GetPlayerAccess(int index)
        {
            int GetPlayerAccessRet = default;
            GetPlayerAccessRet = Data.Player[index].Access;
            return GetPlayerAccessRet;
        }

        public static int GetPlayerX(int index)
        {
            int GetPlayerXRet = default;
            GetPlayerXRet = Data.Player[index].X;
            return GetPlayerXRet;
        }

        public static int GetPlayerY(int index)
        {
            int GetPlayerYRet = default;
            GetPlayerYRet = Data.Player[index].Y;
            return GetPlayerYRet;
        }

        public static byte GetPlayerDir(int index)
        {
            byte GetPlayerDirRet = default;
            GetPlayerDirRet = Data.Player[index].Dir;
            return GetPlayerDirRet;
        }

        public static bool GetPlayerPK(int index)
        {
            bool GetPlayerPKRet = default;
            GetPlayerPKRet = Data.Player[index].PK;
            return GetPlayerPKRet;
        }

        public static void SetPlayerVital(int index, Vital Vital, int Value)
        {
            Data.Player[index].Vital[(int)Vital] = Value;

            if (GetPlayerVital(index, Vital) > GetPlayerMaxVital(index, Vital))
            {
                Data.Player[index].Vital[(int)Vital] = GetPlayerMaxVital(index, Vital);
            }

            if (GetPlayerVital(index, Vital) < 0)
            {
                Data.Player[index].Vital[(int)Vital] = 0;
            }
        }

        public static bool IsDirBlocked(ref byte Blockvar, byte Dir)
        {
            if (Dir == (byte)Direction.UpRight)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Up)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Right)));
            }
            else if (Dir == (byte)Direction.UpLeft)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Up)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Left)));
            }
            else if (Dir == (byte)Direction.DownRight)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Down)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Right)));
            }
            else if (Dir == (byte)Direction.DownLeft)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Down)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Direction.Left)));
            }
            else
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, Dir)));
            }
        }


        public static int GetPlayerNextLevel(int index)
        {
            int GetPlayerNextLevelRet = default;
            GetPlayerNextLevelRet = (int)Math.Round(50d / 3d * (Math.Pow(GetPlayerLevel(index) + 1, 3d) - 6d * Math.Pow(GetPlayerLevel(index) + 1, 2d) + 17 * (GetPlayerLevel(index) + 1) - 12d));
            return GetPlayerNextLevelRet;
        }

        public static int GetPlayerExp(int index)
        {
            int GetPlayerExpRet = default;
            GetPlayerExpRet = Data.Player[index].Exp;
            return GetPlayerExpRet;
        }

        public static int GetPlayerRawStat(int index, Stat Stat)
        {
            int GetPlayerRawStatRet = default;
            GetPlayerRawStatRet = Data.Player[index].Stat[(int)Stat];
            return GetPlayerRawStatRet;
        }

        public static void SetPlayerGatherSkillLvl(int index, int SkillSlot, int lvl)
        {
            Data.Player[index].GatherSkills[SkillSlot].SkillLevel = lvl;
        }

        public static void SetPlayerGatherSkillExp(int index, int SkillSlot, int Exp)
        {
            Data.Player[index].GatherSkills[SkillSlot].SkillCurExp = Exp;
        }

        public static void SetPlayerGatherSkillMaxExp(int index, int SkillSlot, int MaxExp)
        {
            Data.Player[index].GatherSkills[SkillSlot].SkillNextLvlExp = MaxExp;
        }

        public static string GetResourceSkillName(ResourceSkill skillNum)
        {
            switch (skillNum)
            {
                case ResourceSkill.Herbalism:
                    {
                        return "Herbalism";
                    }
                case ResourceSkill.Woodcutting:
                    {
                        return "Woodcutting";
                    }
                case ResourceSkill.Mining:
                    {
                        return "Mining";
                    }
                case ResourceSkill.Fishing:
                    {
                        return "Fishing";
                    }
            }

            return default;
        }

        public static int GetSkillNextLevel(int index, int SkillSlot)
        {
            return (int)Math.Round(50d / 3d * (Math.Pow(GetPlayerGatherSkillLvl(index, SkillSlot) + 1, 3d) - 6d * Math.Pow(GetPlayerGatherSkillLvl(index, SkillSlot) + 1, 2d) + 17 * (GetPlayerGatherSkillLvl(index, SkillSlot) + 1) - 12d));
        }

        public static bool IsPlaying(int index)
        {
            // if the player doesn't exist, the name will equal 0
            if (Strings.Len(GetPlayerName(index)) > 0)
            {
                return true;
            }

            return false;
        }

        public static string GetPlayerName(int index)
        {
            return Data.Player[index].Name;
        }

        public static int GetPlayerGatherSkillLvl(int index, int skillSlot)
        {
            return Data.Player[index].GatherSkills[skillSlot].SkillLevel;
        }

        public static int GetPlayerGatherSkillExp(int index, int skillSlot)
        {
            return Data.Player[index].GatherSkills[skillSlot].SkillCurExp;
        }

        public static int GetPlayerGatherSkillMaxExp(int index, int skillSlot)
        {
            return Data.Player[index].GatherSkills[skillSlot].SkillNextLvlExp;
        }

        public static void SetPlayerMap(int index, int mapNum)
        {
            Data.Player[index].Map = mapNum;
        }

        public static int GetPlayerInv(int index, int invslot)
        {
            return Data.Player[index].Inv[invslot].Num;
        }

        public static void SetPlayerName(int index, string name)
        {
            Data.Player[index].Name = name;
        }

        public static void SetPlayerJob(int index, int jobNum)
        {
            Data.Player[index].Job = (byte)jobNum;
        }

        public static void SetPlayerPoints(int index, int points)
        {
            Data.Player[index].Points = (byte)points;
        }

        public static void SetPlayerStat(int index, Stat stat, int value)
        {
            Data.Player[index].Stat[(int)stat] = (byte)value;
        }

        public static void SetPlayerInv(int index, int invSlot, int itemNum)
        {
            Data.Player[index].Inv[invSlot].Num = itemNum;
        }

        public static int GetPlayerInvValue(int index, int invslot)
        {
            return Data.Player[index].Inv[invslot].Value;
        }

        public static void SetPlayerInvValue(int index, int invslot, int itemValue)
        {
            Data.Player[index].Inv[invslot].Value = itemValue;
        }

        public static int GetPlayerPoints(int index)
        {
            return Data.Player[index].Points;
        }

        public static void SetPlayerAccess(int index, int access)
        {
            Data.Player[index].Access = (byte)access;
        }

        public static void SetPlayerPK(int index, bool pk)
        {
            Data.Player[index].PK = pk;
        }

        public static void SetPlayerX(int index, int x)
        {
            Data.Player[index].X = (byte)x;
        }

        public static void SetPlayerY(int index, int y)
        {
            Data.Player[index].Y = (byte)y;
        }

        public static void SetPlayerSprite(int index, int sprite)
        {
            Data.Player[index].Sprite = sprite;
        }

        public static void SetPlayerExp(int index, int exp)
        {
            Data.Player[index].Exp = exp;
        }

        public static void SetPlayerLevel(int index, int level)
        {
            Data.Player[index].Level = (byte)level;
        }

        public static void SetPlayerDir(int index, int dir)
        {
            Data.Player[index].Dir = (byte)dir;
        }

        public static int GetPlayerVital(int index, Vital vital)
        {
            return Data.Player[index].Vital[(int)vital];
        }

        public static int GetPlayerSprite(int index)
        {
            return Data.Player[index].Sprite;
        }

        public static int GetPlayerJob(int index)
        {
            return Data.Player[index].Job;
        }

        public static int GetPlayerMap(int index)
        {
            return Data.Player[index].Map;
        }

        public static int GetPlayerLevel(int index)
        {
            return Data.Player[index].Level;
        }

        public static int GetPlayerEquipment(int index, Equipment equipmentSlot)
        {
            return Data.Player[index].Equipment[(int)equipmentSlot];
        }

        public static void SetPlayerEquipment(int index, int itemNum, Equipment equipmentSlot)
        {
            Data.Player[index].Equipment[(int)equipmentSlot] = itemNum;
        }

        public static string IsEditorLocked(int index, int id)
        {
            for (int i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                if (IsPlaying(i))
                {
                    if (i != index)
                    {
                        if (Data.TempPlayer[i].Editor == id)
                        {
                            return GetPlayerName(i);
                        }
                    }
                }
            }

            return "";
        }

        public static int FindOpenSkill(int index)
        {
            int i;

            for (i = 0; i < Constant.MAX_PLAYER_SKILLS; i++)
            {

                if (GetPlayerSkill(index, i) == -1)
                {
                    return i;
                }

            }

            return -1;
        }

        public static int GetPlayerSkill(int index, int skillSlot)
        {
            return Data.Player[index].Skill[skillSlot].Num;
        }

        public static int GetPlayerSkillCD(int index, int skillSlot)
        {
            return Data.Player[index].Skill[skillSlot].CD;
        }

        public static void SetPlayerSkillCD(int index, int skillSlot, int value)
        {
            Data.Player[index].Skill[skillSlot].CD = value;
        }

        public static bool HasSkill(int index, double skillNum)
        {
            for (int i = 0; i < Constant.MAX_PLAYER_SKILLS; i++)
            {

                if (GetPlayerSkill(index, i) == skillNum)
                {
                    return true;
                }

            }

            return false;
        }

        public static void SetPlayerSkill(int index, int Skillslot, int skillNum)
        {
            Data.Player[index].Skill[Skillslot].Num = skillNum;
        }

        public static int GetBank(int index, int bankslot)
        {
            int GetBankRet = default;
            GetBankRet = Data.Bank[index].Item[bankslot].Num;
            return GetBankRet;
        }

        public static void SetBank(int index, byte bankSlot, int itemNum)
        {
            Data.Bank[index].Item[bankSlot].Num = itemNum;
        }

        public static int GetBankValue(int index, int bankSlot)
        {
            int GetBankValueRet = default;
            GetBankValueRet = Data.Bank[index].Item[bankSlot].Value;
            return GetBankValueRet;
        }

        public static void SetBankValue(int index, byte bankSlot, int itemValue)
        {
            Data.Bank[index].Item[bankSlot].Value = itemValue;
        }

    }
}