using Core;
using Microsoft.VisualBasic.CompilerServices;

namespace Client
{

    public class Moral
    {
        #region Database

        public static void ClearMoral(int index)
        {
            Data.Moral[index] = default;

            Data.Moral[index].Name = "";
            GameState.Moral_Loaded[index] = 0;
        }

        public static void ClearMorals()
        {
            int i;

            Data.Moral = new Core.Type.Moral[(Constant.MAX_MORALS)];

            for (i = 0; i < Constant.MAX_MORALS; i++)
                ClearMoral(i);
        }

        public static void StreamMoral(int moralNum)
        {
            if (moralNum >= 0 & string.IsNullOrEmpty(Data.Moral[moralNum].Name) && GameState.Moral_Loaded[moralNum] == 0)
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