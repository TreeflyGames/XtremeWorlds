using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Type;
using static Core.Global.Command;
using static Core.Enum;
using static Core.Packets;

namespace Server
{

    public class Projectile
    {

        #region Database
        public static void SaveProjectile(int projectileNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Projectile[projectileNum]).ToString();

            if (Database.RowExists(projectileNum, "projectile"))
            {
                Database.UpdateRow(projectileNum, json, "projectile", "data");
            }
            else
            {
                Database.InsertRow(projectileNum, json, "projectile");
            }
        }

        public static void LoadProjectiles()
        {
            int i;

            var loopTo = Core.Constant.MAX_PROJECTILES;
            for (i = 0; i < loopTo; i++)
                LoadProjectile(i);
        }

        public static void LoadProjectile(int projectileNum)
        {
            JObject data;

            data = Database.SelectRow(projectileNum, "projectile", "data");

            if (data is null)
            {
                ClearProjectile(projectileNum);
                return;
            }

            var projectileData = JObject.FromObject(data).ToObject<Core.Type.ProjectileStruct>();
            Core.Type.Projectile[projectileNum] = projectileData;
        }

        public static void ClearMapProjectile()
        {
            int x;
            int y;
            ;

            Core.Type.MapProjectile = new Core.Type.MapProjectileStruct[Core.Constant.MAX_MAPS, Core.Constant.MAX_PROJECTILES];

            var loopTo = Core.Constant.MAX_MAPS;
            for (x = 0; x < (int)loopTo; x++)
            {
                var loopTo1 = Core.Constant.MAX_PROJECTILES;
                for (y = 0; y < (int)loopTo1; y++)
                    ClearMapProjectile(x, y);
            }

        }

        public static void ClearMapProjectile(int mapNum, int index)
        {

            MapProjectile[mapNum, index].ProjectileNum = 0;
            MapProjectile[mapNum, index].Owner = 0;
            MapProjectile[mapNum, index].OwnerType = 0;
            MapProjectile[mapNum, index].X = 0;
            MapProjectile[mapNum, index].Y = 0;
            MapProjectile[mapNum, index].Dir = 0;
            MapProjectile[mapNum, index].Timer = 0;

        }

        public static void ClearProjectile(int index)
        {

            Core.Type.Projectile[index].Name = "";
            Core.Type.Projectile[index].Sprite = 0;
            Core.Type.Projectile[index].Range = 0;
            Core.Type.Projectile[index].Speed = 0;
            Core.Type.Projectile[index].Damage = 0;

        }

        public static void ClearProjectile()
        {
            int i;

            Core.Type.Projectile = new Core.Type.ProjectileStruct[Core.Constant.MAX_PROJECTILES];

            var loopTo = Core.Constant.MAX_PROJECTILES;
            for (i = 0; i < loopTo; i++)
                ClearProjectile(i);

        }

        #endregion

        #region Incoming

        public static void HandleRequestEditProjectile(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Developer)
                return;

            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte)EditorType.Projectile);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            SendProjectiles(index);

            Core.Type.TempPlayer[index].Editor = (byte)EditorType.Projectile;

            buffer.WriteInt32((int) ServerPackets.SProjectileEditor);

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void HandleSaveProjectile(int index, ref byte[] data)
        {
            int ProjectileNum;
            var buffer = new ByteStream(data);

            if (GetPlayerAccess(index) < (byte)AccessType.Developer)
                return;

            ProjectileNum = buffer.ReadInt32();

            // Prevent hacking
            if (ProjectileNum < 0 | ProjectileNum > Core.Constant.MAX_PROJECTILES)
            {
                return;
            }

            Core.Type.Projectile[ProjectileNum].Name = buffer.ReadString();
            Core.Type.Projectile[ProjectileNum].Sprite = buffer.ReadInt32();
            Core.Type.Projectile[ProjectileNum].Range = (byte)buffer.ReadInt32();
            Core.Type.Projectile[ProjectileNum].Speed = buffer.ReadInt32();
            Core.Type.Projectile[ProjectileNum].Damage = buffer.ReadInt32();

            // Save it
            SendUpdateProjectileToAll(ProjectileNum);
            SaveProjectile(ProjectileNum);
            Core.Log.Add(GetPlayerLogin(index) + " saved Projectile #" + ProjectileNum + ".", Constant.ADMIN_LOG);
            buffer.Dispose();

        }

        public static void HandleRequestProjectile(int index, ref byte[] data)
        {
            int ProjectileNum;

            var buffer = new ByteStream(data);
            ProjectileNum = buffer.ReadInt32();
            buffer.Dispose();

            SendProjectile(index, ProjectileNum);
        }

        public static void HandleClearProjectile(int index, ref byte[] data)
        {
            int ProjectileNum;
            int Targetindex;
            TargetType TargetType;
            int TargetZone;
            int mapNum;
            int Damage;
            int armor;
            int NPCNum;
            var buffer = new ByteStream(data);
            ProjectileNum = buffer.ReadInt32();
            Targetindex = buffer.ReadInt32();
            TargetType = (TargetType)buffer.ReadInt32();
            TargetZone = buffer.ReadInt32();
            buffer.Dispose();

            mapNum = GetPlayerMap(index);

            switch (Core.Type.MapProjectile[mapNum, ProjectileNum].OwnerType)
            {
                case (byte)TargetType.Player:
                    {
                        if (Core.Type.MapProjectile[mapNum, ProjectileNum].Owner == index)
                        {
                            switch (TargetType)
                            {
                                case TargetType.Player:
                                    {

                                        if (NetworkConfig.IsPlaying(Targetindex))
                                        {
                                            if (Targetindex != index)
                                            {
                                                if (Conversions.ToInteger(Player.CanPlayerAttackPlayer(index, Targetindex, true)) == 1)
                                                {

                                                    // Get the damage we can do
                                                    Damage = Player.GetPlayerDamage(index) + Core.Type.Projectile[Core.Type.MapProjectile[mapNum, ProjectileNum].ProjectileNum].Damage;

                                                    // if the npc blocks, take away the block amount
                                                    armor = Conversions.ToInteger(Player.CanPlayerBlockHit(Targetindex));
                                                    Damage = Damage - armor;

                                                    // randomise for up to 10% lower than Core.Constant.MAX hit
                                                    Damage = (int)Math.Round(General.Random.NextDouble(1d, Damage));

                                                    Player.AttackPlayer(index, Targetindex, Damage);
                                                }
                                            }
                                        }

                                        break;
                                    }

                                case TargetType.NPC:
                                    {
                                        NPCNum = (int)Core.Type.MapNPC[mapNum].NPC[Targetindex].Num;
                                        if (Conversions.ToInteger(Player.CanPlayerAttackNPC(index, Targetindex, true)) == 1)
                                        {
                                            // Get the damage we can do
                                            Damage = Player.GetPlayerDamage(index) + Core.Type.Projectile[Core.Type.MapProjectile[mapNum, ProjectileNum].ProjectileNum].Damage;

                                            // if the npc blocks, take away the block amount
                                            armor = 0;
                                            Damage = Damage - armor;

                                            // randomise from 1 to Core.Constant.MAX hit
                                            Damage = (int)Math.Round(General.Random.NextDouble(1d, Damage));

                                            Player.PlayerAttackNPC(index, Targetindex, Damage);
                                        }

                                        break;
                                    }
                            }
                        }

                        break;
                    }

            }

            ClearMapProjectile(mapNum, ProjectileNum);

        }

        #endregion

        #region Outgoing

        public static void SendUpdateProjectileToAll(int ProjectileNum)
        {
            ByteStream buffer;

            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateProjectile);
            buffer.WriteInt32(ProjectileNum);
            buffer.WriteString(Core.Type.Projectile[ProjectileNum].Name);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Sprite);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Range);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Speed);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Damage);

            NetworkConfig.SendDataToAll(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendUpdateProjectileTo(int index, int ProjectileNum)
        {
            ByteStream buffer;

            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateProjectile);
            buffer.WriteInt32(ProjectileNum);
            buffer.WriteString(Core.Type.Projectile[ProjectileNum].Name);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Sprite);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Range);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Speed);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Damage);

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendProjectile(int index, int projectileNum)
        {
            SendUpdateProjectileTo(index, projectileNum);
        }

        public static void SendProjectiles(int index)
        {
            var loopTo = Core.Constant.MAX_PROJECTILES;
            for (int i = 0; i < loopTo; i++)
            {
                if (Strings.Len(Core.Type.Projectile[i].Name) > 0)
                {
                    SendUpdateProjectileTo(index, i);
                }
            }

        }

        public static void SendProjectileToMap(int mapNum, int ProjectileNum)
        {
            ByteStream buffer;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SMapProjectile);

            {
                var withBlock = Core.Type.MapProjectile[mapNum, ProjectileNum];
                buffer.WriteInt32(ProjectileNum);
                buffer.WriteInt32(withBlock.ProjectileNum);
                buffer.WriteInt32(withBlock.Owner);
                buffer.WriteInt32(withBlock.OwnerType);
                buffer.WriteInt32(withBlock.Dir);
                buffer.WriteInt32(withBlock.X);
                buffer.WriteInt32(withBlock.Y);
            }

            NetworkConfig.SendDataToMap(mapNum, buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        #endregion

        #region Functions

        public static void PlayerFireProjectile(int index, int IsSkill = 0)
        {
            var ProjectileSlot = default(int);
            int ProjectileNum;
            int mapNum;
            int i;

            mapNum = GetPlayerMap(index);

            // Find a free projectile
            var loopTo = Core.Constant.MAX_PROJECTILES;
            for (i = 0; i < loopTo; i++)
            {
                if (Core.Type.MapProjectile[mapNum, i].ProjectileNum == -1) // Free Projectile
                {
                    ProjectileSlot = i;
                    break;
                }
            }

            // Check for skill, if so then load data acordingly
            if (IsSkill > 0)
            {
                ProjectileNum = Core.Type.Skill[IsSkill].Projectile;
            }
            else
            {
                ProjectileNum = Core.Type.Item[(int)GetPlayerEquipment(index, EquipmentType.Weapon)].Projectile;
            }

            if (ProjectileNum == -1)
                return;

            {
                var withBlock = Core.Type.MapProjectile[mapNum, ProjectileSlot];
                withBlock.ProjectileNum = ProjectileNum;
                withBlock.Owner = index;
                withBlock.OwnerType = (byte)TargetType.Player;
                withBlock.Dir = (byte)GetPlayerDir(index);
                withBlock.X = GetPlayerX(index);
                withBlock.Y = GetPlayerY(index);
                withBlock.Timer = General.GetTimeMs() + 60000;
            }

            SendProjectileToMap(mapNum, ProjectileSlot);

        }

        public static float Engine_GetAngle(int CenterX, int CenterY, int targetX, int targetY)
        {
            float Engine_GetAngleRet = default;
            // ************************************************************
            // Gets the angle between two points in a 2d plane
            // ************************************************************
            float SideA;
            float SideC;
            try
            {

                // Check for horizontal lines (90 or 270 degrees)
                if (CenterY == targetY)
                {
                    // Check for going right (90 degrees)
                    if (CenterX < targetX)
                    {
                        Engine_GetAngleRet = 90f;
                    }
                    // Check for going left (270 degrees)
                    else
                    {
                        Engine_GetAngleRet = 270f;
                    }

                    // Exit the function
                    return Engine_GetAngleRet;
                }

                // Check for horizontal lines (360 or 180 degrees)
                if (CenterX == targetX)
                {
                    // Check for going up (360 degrees)
                    if (CenterY > targetY)
                    {
                        Engine_GetAngleRet = 360f;
                    }

                    // Check for going down (180 degrees)
                    else
                    {
                        Engine_GetAngleRet = 180f;
                    }

                    // Exit the function
                    return Engine_GetAngleRet;
                }

                // Calculate Side C
                SideC = (float)Math.Sqrt(Math.Pow(Math.Abs(targetX - CenterX), 2d) + Math.Pow(Math.Abs(targetY - CenterY), 2d));

                // Side B = CenterY

                // Calculate Side A
                SideA = (float)Math.Sqrt(Math.Pow(Math.Abs(targetX - CenterX), 2d) + Math.Pow(targetY, 2d));

                // Calculate the angle
                Engine_GetAngleRet = (float)((Math.Pow((double)SideA, 2d) - Math.Pow(CenterY, 2d) - Math.Pow((double)SideC, 2d)) / (double)(CenterY * SideC * -2));
                Engine_GetAngleRet = (float)((Math.Atan((double)-Engine_GetAngleRet / Math.Sqrt((double)(-Engine_GetAngleRet * Engine_GetAngleRet + 1f))) + 1.5708d) * 57.29583d);

                // If the angle is >180, subtract from 360
                if (targetX < CenterX)
                    Engine_GetAngleRet = 360f - Engine_GetAngleRet;

                // Exit function

                // Check for error
                return Engine_GetAngleRet;
            }
            catch
            {


                // Return a 0 saying there was an error
                Engine_GetAngleRet = 0f;

                return Engine_GetAngleRet;
            }
        }

        #endregion

    }
}