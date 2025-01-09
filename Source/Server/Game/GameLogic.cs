using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static Core.Enum;
using static Core.Global.Command;

namespace Server
{

    static class GameLogic
    {
        public static int GetTotalMapPlayers(int mapNum)
        {
            int GetTotalMapPlayersRet = default;
            int i;
            int n;
            n = 0;

            var loopTo = NetworkConfig.Socket.HighIndex + 1;
            for (i = 0; i < loopTo; i++)
            {
                if (GetPlayerMap(i) == mapNum)
                {
                    n = n + 1;
                }
            }

            GetTotalMapPlayersRet = n;
            return GetTotalMapPlayersRet;
        }

        public static int GetNPCMaxVital(double NPCNum, Core.Enum.VitalType Vital)
        {
            int GetNPCMaxVitalRet = default;
            // Prevent subscript out of range
            if (NPCNum < 0 | NPCNum > Core.Constant.MAX_NPCS)
                return GetNPCMaxVitalRet;

            switch (Vital)
            {
                case VitalType.HP:
                    {
                        GetNPCMaxVitalRet = Core.Type.NPC[(int)NPCNum].HP;
                        break;
                    }
                case VitalType.SP:
                    {
                        GetNPCMaxVitalRet = (int)Core.Type.NPC[(int)NPCNum].Stat[(byte)StatType.Intelligence] * 2;
                        break;
                    }
            }

            return GetNPCMaxVitalRet;

        }

        public static int FindPlayer(string Name)
        {
            int FindPlayerRet = default;
            int i;

            var loopTo = NetworkConfig.Socket.HighIndex + 1;
            for (i = 0; i < loopTo; i++)
            {
                // Trim and convert both names to uppercase for case-insensitive comparison
                if (Strings.UCase(GetPlayerName(i)) == Strings.UCase(Name))
                {
                    FindPlayerRet = i;
                    return FindPlayerRet;
                }
            }

            FindPlayerRet = -1;
            return FindPlayerRet;
        }

        internal static string CheckGrammar(string Word, byte Caps = 0)
        {
            string CheckGrammarRet = default;
            string FirstLetter;

            FirstLetter = Strings.LCase(Strings.Left(Word, 1));

            if (FirstLetter == "$")
            {
                CheckGrammarRet = Strings.Mid(Word, 2, Strings.Len(Word) - 1);
                return CheckGrammarRet;
            }

            if (LikeOperator.LikeString(FirstLetter, "*[aeiou]*", CompareMethod.Binary))
            {
                if (Conversions.ToBoolean(Caps))
                    CheckGrammarRet = "An " + Word;
                else
                    CheckGrammarRet = "an " + Word;
            }
            else if (Conversions.ToBoolean(Caps))
                CheckGrammarRet = "A " + Word;
            else
                CheckGrammarRet = "a " + Word;
            return CheckGrammarRet;
        }

    }
}