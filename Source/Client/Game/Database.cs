using Core;
using Microsoft.VisualBasic.CompilerServices;

namespace Client
{

    static class Database
    {
        #region Blood

        public static void ClearBlood()
        {
            for (int i = 0; i <= byte.MaxValue - 1; i++)
                Core.Type.Blood[i].Timer = 0;
        }

        #endregion

        #region NPC

        public static void ClearNPCs()
        { 
            Core.Type.NPC = new Core.Type.NPCStruct[Core.Constant.MAX_NPCS];

            for (int i = 0; i < Constant.MAX_NPCS; i++)
                ClearNPC(i);

        }

        public static void ClearNPC(int index)
        {
            Core.Type.NPC[index] = default;
            Core.Type.NPC[index].Stat = new byte[6];
            Core.Type.NPC[index].DropChance = new int[6];
            Core.Type.NPC[index].DropItem = new int[6];
            Core.Type.NPC[index].DropItemValue = new int[6];
            Core.Type.NPC[index].Skill = new byte[7];
            GameState.NPC_Loaded[index] = 0;
        }

        public static void StreamNPC(int NPCNum)
        {
            if (Conversions.ToBoolean(Operators.OrObject(NPCNum > 0 & string.IsNullOrEmpty(Core.Type.NPC[NPCNum].Name), Operators.ConditionalCompareObjectEqual(GameState.NPC_Loaded[NPCNum], 0, false))))
            {
                GameState.NPC_Loaded[NPCNum] = 1;
                NetworkSend.SendRequestNPC(NPCNum);
            }
        }

        #endregion

        #region Jobs
        public static void ClearJobs()
        {
            for (int i = 0; i < Constant.MAX_JOBS; i++)
                ClearJob(i);
        }

        public static void ClearJob(int index)
        {
            Core.Type.Job[index] = default;
            Core.Type.Job[index].Stat = new int[6];
            Core.Type.Job[index].Name = "";
            Core.Type.Job[index].Desc = "";
            Core.Type.Job[index].StartItem = new int[6];
            Core.Type.Job[index].StartValue = new int[6];
            Core.Type.Job[index].MaleSprite = 1;
            Core.Type.Job[index].FemaleSprite = 1;
        }
        #endregion

        #region Skills

        public static void ClearSkills()
        {
            int i;

            for (i = 0; i < Constant.MAX_SKILLS; i++)
                ClearSkill(i);

        }

        public static void ClearSkill(int index)
        {
            Core.Type.Skill[index] = default;
            Core.Type.Skill[index].Name = "";
            GameState.Skill_Loaded[index] = 0;
        }

        public static void StreamSkill(int skillNum)
        {
            if (Conversions.ToBoolean(Operators.OrObject(skillNum > 0 & string.IsNullOrEmpty(Core.Type.Skill[skillNum].Name), Operators.ConditionalCompareObjectEqual(GameState.Skill_Loaded[skillNum], 0, false))))
            {
                GameState.Skill_Loaded[skillNum] = 1;
                NetworkSend.SendRequestSkill(skillNum);
            }
        }
        #endregion
    }
}