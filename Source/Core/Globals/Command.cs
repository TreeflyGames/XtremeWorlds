using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Core.Global
{
    public static class Command
    {
        public static string GetPlayerLogin(int index)
        {
            string GetPlayerLoginRet = default;
            GetPlayerLoginRet = Type.Account[index].Login;
            return GetPlayerLoginRet;
        }

        public static void SetPlayerLogin(int index, string login)
        {
            Type.Account[index].Login = login;
        }

        public static string GetPlayerPassword(int index)
        {
            string GetPlayerPasswordRet = default;
            GetPlayerPasswordRet = Type.Account[index].Password;
            return GetPlayerPasswordRet;
        }

        public static void SetPlayerPassword(int index, string password)
        {
            Type.Account[index].Password = password;
        }

        public static int GetPlayerMaxVital(int index, Enum.VitalType Vital)
        {
            int GetPlayerMaxVitalRet = default;
            switch (Vital)
            {
                case Enum.VitalType.HP:
                    {
                        GetPlayerMaxVitalRet = (int)Math.Round(100d + (Type.Player[index].Level + GetPlayerStat(index, Enum.StatType.Vitality) / 2d) * 2d);
                        break;
                    }

                case Enum.VitalType.SP:
                    {
                        GetPlayerMaxVitalRet = (int)Math.Round(50d + (Type.Player[index].Level + GetPlayerStat(index, Enum.StatType.Spirit) / 2d) * 2d);
                        break;
                    }
            }

            return GetPlayerMaxVitalRet;
        }

        public static int GetPlayerStat(int index, Enum.StatType Stat)
        {
            int GetPlayerStatRet = default;
            int x;
            int i;

            x = Type.Player[index].Stat[(int)Stat];

            for (i = 0; i < (int)Enum.EquipmentType.Count; i++)
            {
                if (Type.Player[index].Equipment[i] > 0)
                {
                    if (Type.Item[Type.Player[index].Equipment[i]].Add_Stat[(int)Stat] > 0)
                    {
                        x += Type.Item[Type.Player[index].Equipment[i]].Add_Stat[(int)Stat];
                    }
                }
            }

            GetPlayerStatRet = x;
            return GetPlayerStatRet;
        }

        public static int GetPlayerAccess(int index)
        {
            int GetPlayerAccessRet = default;
            GetPlayerAccessRet = Type.Player[index].Access;
            return GetPlayerAccessRet;
        }

        public static int GetPlayerX(int index)
        {
            int GetPlayerXRet = default;
            GetPlayerXRet = Type.Player[index].X;
            return GetPlayerXRet;
        }

        public static int GetPlayerY(int index)
        {
            int GetPlayerYRet = default;
            GetPlayerYRet = Type.Player[index].Y;
            return GetPlayerYRet;
        }

        public static int GetPlayerDir(int index)
        {
            int GetPlayerDirRet = default;
            GetPlayerDirRet = Type.Player[index].Dir;
            return GetPlayerDirRet;
        }

        public static int GetPlayerPK(int index)
        {
            int GetPlayerPKRet = default;
            GetPlayerPKRet = Type.Player[index].Pk;
            return GetPlayerPKRet;
        }

        public static void SetPlayerVital(int index, Enum.VitalType Vital, int Value)
        {
            Type.Player[index].Vital[(int)Vital] = Value;

            if (GetPlayerVital(index, Vital) > GetPlayerMaxVital(index, Vital))
            {
                Type.Player[index].Vital[(int)Vital] = GetPlayerMaxVital(index, Vital);
            }

            if (GetPlayerVital(index, Vital) < 0)
            {
                Type.Player[index].Vital[(int)Vital] = 0;
            }
        }

        public static bool IsDirBlocked(ref byte Blockvar, byte Dir)
        {
            if (Dir == (byte)Enum.DirectionType.UpRight)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Up)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Right)));
            }
            else if (Dir == (byte)Enum.DirectionType.UpLeft)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Up)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Left)));
            }
            else if (Dir == (byte)Enum.DirectionType.DownRight)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Down)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Right)));
            }
            else if (Dir == (byte)Enum.DirectionType.DownLeft)
            {
                return Conversions.ToBoolean(Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Down)) | Blockvar & (long)Math.Round(Math.Pow(2d, (double)Enum.DirectionType.Left)));
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
            GetPlayerExpRet = Type.Player[index].Exp;
            return GetPlayerExpRet;
        }

        public static int GetPlayerRawStat(int index, Enum.StatType Stat)
        {
            int GetPlayerRawStatRet = default;
            GetPlayerRawStatRet = Type.Player[index].Stat[(int)Stat];
            return GetPlayerRawStatRet;
        }

        public static void SetPlayerGatherSkillLvl(int index, int SkillSlot, int lvl)
        {
            Type.Player[index].GatherSkills[SkillSlot].SkillLevel = lvl;
        }

        public static void SetPlayerGatherSkillExp(int index, int SkillSlot, int Exp)
        {
            Type.Player[index].GatherSkills[SkillSlot].SkillCurExp = Exp;
        }

        public static void SetPlayerGatherSkillMaxExp(int index, int SkillSlot, int MaxExp)
        {
            Type.Player[index].GatherSkills[SkillSlot].SkillNextLvlExp = MaxExp;
        }

        public static string GetResourceSkillName(Enum.ResourceType skillNum)
        {
            switch (skillNum)
            {
                case Enum.ResourceType.Herb:
                    {
                        return "Herbalism";
                    }
                case Enum.ResourceType.Woodcut:
                    {
                        return "Woodcutting";
                    }
                case Enum.ResourceType.Mine:
                    {
                        return "Mining";
                    }
                case Enum.ResourceType.Fish:
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
            return Type.Player[index].Name;
        }

        public static int GetPlayerGatherSkillLvl(int index, int skillSlot)
        {
            return Type.Player[index].GatherSkills[skillSlot].SkillLevel;
        }

        public static int GetPlayerGatherSkillExp(int index, int skillSlot)
        {
            return Type.Player[index].GatherSkills[skillSlot].SkillCurExp;
        }

        public static int GetPlayerGatherSkillMaxExp(int index, int skillSlot)
        {
            return Type.Player[index].GatherSkills[skillSlot].SkillNextLvlExp;
        }

        public static void SetPlayerMap(int index, int mapNum)
        {
            Type.Player[index].Map = mapNum;
        }

        public static double GetPlayerInv(int index, int invslot)
        {
            return Type.Player[index].Inv[invslot].Num;
        }

        public static void SetPlayerName(int index, string name)
        {
            Type.Player[index].Name = name;
        }

        public static void SetPlayerJob(int index, int jobNum)
        {
            Type.Player[index].Job = (byte)jobNum;
        }

        public static void SetPlayerPoints(int index, int points)
        {
            Type.Player[index].Points = (byte)points;
        }

        public static void SetPlayerStat(int index, Enum.StatType stat, int value)
        {
            Type.Player[index].Stat[(int)stat] = (byte)value;
        }

        public static void SetPlayerInv(int index, int invSlot, int itemNum)
        {
            Type.Player[index].Inv[invSlot].Num = itemNum;
        }

        public static int GetPlayerInvValue(int index, int invslot)
        {
            return Type.Player[index].Inv[invslot].Value;
        }

        public static void SetPlayerInvValue(int index, int invslot, int itemValue)
        {
            Type.Player[index].Inv[invslot].Value = itemValue;
        }

        public static int GetPlayerPoints(int index)
        {
            return Type.Player[index].Points;
        }

        public static void SetPlayerAccess(int index, int access)
        {
            Type.Player[index].Access = (byte)access;
        }

        public static void SetPlayerPk(int index, int pk)
        {
            Type.Player[index].Pk = (byte)pk;
        }

        public static void SetPlayerX(int index, int x)
        {
            Type.Player[index].X = (byte)x;
        }

        public static void SetPlayerY(int index, int y)
        {
            Type.Player[index].Y = (byte)y;
        }

        public static void SetPlayerSprite(int index, int sprite)
        {
            Type.Player[index].Sprite = sprite;
        }

        public static void SetPlayerExp(int index, int exp)
        {
            Type.Player[index].Exp = exp;
        }

        public static void SetPlayerLevel(int index, int level)
        {
            Type.Player[index].Level = (byte)level;
        }

        public static void SetPlayerDir(int index, int dir)
        {
            Type.Player[index].Dir = (byte)dir;
        }

        public static int GetPlayerVital(int index, Enum.VitalType vital)
        {
            return Type.Player[index].Vital[(int)vital];
        }

        public static int GetPlayerSprite(int index)
        {
            return Type.Player[index].Sprite;
        }

        public static int GetPlayerJob(int index)
        {
            return Type.Player[index].Job;
        }

        public static int GetPlayerMap(int index)
        {
            return Type.Player[index].Map;
        }

        public static int GetPlayerLevel(int index)
        {
            return Type.Player[index].Level;
        }

        public static double GetPlayerEquipment(int index, Enum.EquipmentType equipmentSlot)
        {
            return Type.Player[index].Equipment[(int)equipmentSlot];
        }

        public static void SetPlayerEquipment(int index, int invNum, Enum.EquipmentType equipmentSlot)
        {
            Type.Player[index].Equipment[(int)equipmentSlot] = invNum;
        }

        public static string IsEditorLocked(int index, int id)
        {
            for (int i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                if (IsPlaying(i))
                {
                    if (i != index)
                    {
                        if (Type.TempPlayer[i].Editor == id)
                        {
                            return GetPlayerName(i);
                            return default;
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

                if (GetPlayerSkill(index, i) == 0)
                {
                    return i;
                    return default;
                }

            }

            return 0;
        }

        public static double GetPlayerSkill(int index, int skillSlot)
        {
            return Type.Player[index].Skill[skillSlot].Num;
        }

        public static int GetPlayerSkillCD(int index, int skillSlot)
        {
            return Type.Player[index].Skill[skillSlot].CD;
        }

        public static void SetPlayerSkillCD(int index, int skillSlot, int Value)
        {
            Type.Player[index].Skill[skillSlot].CD = Value;
        }

        public static bool HasSkill(int index, int skillNum)
        {
            int i;

            for (i = 0; i < Constant.MAX_PLAYER_SKILLS; i++)
            {

                if (GetPlayerSkill(index, i) == skillNum)
                {
                    return true;
                    return default;
                }

            }

            return false;
        }

        public static void SetPlayerSkill(int index, int Skillslot, int skillNum)
        {
            Type.Player[index].Skill[Skillslot].Num = skillNum;
        }

        public static double GetBank(int index, byte bankslot)
        {
            double GetBankRet = default;
            GetBankRet = Type.Bank[index].Item[bankslot].Num;
            return GetBankRet;
        }

        public static void SetBank(int index, byte bankSlot, int itemNum)
        {
            Type.Bank[index].Item[bankSlot].Num = itemNum;
        }

        public static int GetBankValue(int index, byte bankSlot)
        {
            int GetBankValueRet = default;
            GetBankValueRet = Type.Bank[index].Item[bankSlot].Value;
            return GetBankValueRet;
        }

        public static void SetBankValue(int index, byte bankSlot, int itemValue)
        {
            Type.Bank[index].Item[bankSlot].Value = itemValue;
        }

    }
}