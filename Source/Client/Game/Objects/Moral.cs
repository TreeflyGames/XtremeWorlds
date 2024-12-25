using Core;
using Microsoft.VisualBasic.CompilerServices;

namespace Client
{

    internal static class Moral
    {
        #region Database

        internal static void ClearMoral(int index)
        {
            Core.Type.Moral[index] = default;

            Core.Type.Moral[index].Name = "";
            GameState.Moral_Loaded[index] = 0;
        }

        public static void ClearMorals()
        {
            int i;

            Core.Type.Moral = new Core.Type.MoralStruct[(Constant.MAX_MORALS + 1)];

            for (i = 0; i <= Constant.MAX_MORALS; i++)
                ClearMoral(i);
        }

        internal static void StreamMoral(int moralNum)
        {
            if (Conversions.ToBoolean(Operators.OrObject(moralNum > 0 & string.IsNullOrEmpty(Core.Type.Moral[moralNum].Name), Operators.ConditionalCompareObjectEqual(GameState.Moral_Loaded[moralNum], 0, false))))
            {
                GameState.Moral_Loaded[moralNum] = 1;
                NetworkSend.SendRequestMoral(moralNum);
            }
        }

        #endregion

        #region Incoming Packets

        #endregion
    }
}