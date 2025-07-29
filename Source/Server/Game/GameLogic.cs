using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static Core.Global.Command;

namespace Server
{

    public class GameLogic
    {
        public static int GetTotalMapPlayers(int mapNum)
        {
            int GetTotalMapPlayersRet = default;
            int i;
            int n;
            n = 0;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= loopTo; i++)
            {
                if (GetPlayerMap(i) == mapNum)
                {
                    n = n + 1;
                }
            }

            GetTotalMapPlayersRet = n;
            return GetTotalMapPlayersRet;
        }

        public static int GetNpcMaxVital(double NpcNum, Core.Vital Vital)
        {
            int GetNpcMaxVitalRet = default;
            // Prevent subscript out of range
            if (NpcNum < 0 | NpcNum > Core.Constant.MAX_NPCS)
                return GetNpcMaxVitalRet;

            switch (Vital)
            {
                case Core.Vital.Health:
                    {
                        GetNpcMaxVitalRet = Core.Data.Npc[(int)NpcNum].HP;
                        break;
                    }
                case Core.Vital.Stamina:
                    {
                        GetNpcMaxVitalRet = (int)Core.Data.Npc[(int)NpcNum].Stat[(byte)Core.Stat.Intelligence] * 2;
                        break;
                    }
            }

            return GetNpcMaxVitalRet;

        }

        public static int FindPlayer(string Name)
        {
            int FindPlayerRet = default;
            int i;

            var loopTo = NetworkConfig.Socket.HighIndex;
            for (i = 0; i <= loopTo; i++)
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

        public static string CheckGrammar(string Word, byte Caps = 0)
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