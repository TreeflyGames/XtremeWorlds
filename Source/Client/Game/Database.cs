using Core;

namespace Client
{

    public class Database
    {
        #region Blood

        public static void ClearBlood()
        {
            for (int i = 0; i < byte.MaxValue; i++)
                Data.Blood[i].Timer = 0;
        }

        #endregion

        #region Npc

        public static void ClearNpcs()
        {
            Data.Npc = new Core.Type.Npc[Core.Constant.MAX_NPCS];

            for (int i = 0; i < Constant.MAX_NPCS; i++)
                ClearNpc(i);

        }

        public static void ClearNpc(int index)
        {
            Data.Npc[index].AttackSay = "";
            Data.Npc[index].Name = "";
            Data.Npc[index] = default;
            Data.Npc[index].Stat = new byte[6];
            Data.Npc[index].DropChance = new int[6];
            Data.Npc[index].DropItem = new int[6];
            Data.Npc[index].DropItemValue = new int[6];
            Data.Npc[index].Skill = new byte[7];
            GameState.Npc_Loaded[index] = 0;
        }

        public static void StreamNpc(int NpcNum)
        {
            if (NpcNum >= 0 && string.IsNullOrEmpty(Data.Npc[NpcNum].Name) && GameState.Npc_Loaded[NpcNum] == 0)
            {
                GameState.Npc_Loaded[(int)NpcNum] = 1;
                NetworkSend.SendRequestNpc(NpcNum);
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
            var statCount = System.Enum.GetValues(typeof(Stat)).Length;
            Data.Job[index] = default;
            Data.Job[index].Stat = new int[statCount];
            Data.Job[index].Name = "";
            Data.Job[index].Desc = "";
            Data.Job[index].StartItem = new int[Constant.MAX_START_ITEMS];
            Data.Job[index].StartValue = new int[Constant.MAX_START_ITEMS];
            Data.Job[index].MaleSprite = 1;
            Data.Job[index].FemaleSprite = 1;
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
            Data.Skill[index] = default;
            Data.Skill[index].Name = "";
            Data.Skill[index].JobReq = -1;
            GameState.Skill_Loaded[index] = 0;
        }

        public static void StreamSkill(int skillNum)
        {
            if (skillNum >= 0 && string.IsNullOrEmpty(Data.Skill[skillNum].Name) && GameState.Skill_Loaded[skillNum] == 0)
            {
                GameState.Skill_Loaded[skillNum] = 1;
                NetworkSend.SendRequestSkill(skillNum);
            }
        }
        #endregion
    }
}