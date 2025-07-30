using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Type;
using static Core.Global.Command;
using static Core.Packets;
using Core;

namespace Server
{

    public class Projectile
    {

        #region Database
        public static void SaveProjectile(int projectileNum)
        {
            string json = JsonConvert.SerializeObject(Data.Projectile[projectileNum]).ToString();

            if (Database.RowExists(projectileNum, "projectile"))
            {
                Database.UpdateRow(projectileNum, json, "projectile", "data");
            }
            else
            {
                Database.InsertRow(projectileNum, json, "projectile");
            }
        }

        public static async System.Threading.Tasks.Task LoadProjectilesAsync()
        {
            int i;

            var loopTo = Core.Constant.MAX_PROJECTILES;
            for (i = 0; i < loopTo; i++)
                await LoadProjectile(i);
        }

        public static async System.Threading.Tasks.Task LoadProjectile(int projectileNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(projectileNum, "projectile", "data");

            if (data is null)
            {
                ClearProjectile(projectileNum);
                return;
            }

            var projectileData = data.ToObject<Core.Type.Projectile>();
            Core.Data.Projectile[projectileNum] = projectileData;
        }

        public static void ClearMapProjectile(int mapNum, int index)
        {

            Data.MapProjectile[mapNum, index].ProjectileNum = 0;
            Data.MapProjectile[mapNum, index].Owner = 0;
            Data.MapProjectile[mapNum, index].OwnerType = 0;
            Data.MapProjectile[mapNum, index].X = 0;
            Data.MapProjectile[mapNum, index].Y = 0;
            Data.MapProjectile[mapNum, index].Dir = 0;
            Data.MapProjectile[mapNum, index].Timer = 0;

        }

        public static void ClearProjectile(int index)
        {

            Data.Projectile[index].Name = "";
            Data.Projectile[index].Sprite = 0;
            Data.Projectile[index].Range = 0;
            Data.Projectile[index].Speed = 0;
            Data.Projectile[index].Damage = 0;

        }

        #endregion

        #region Incoming

        public static void HandleRequestEditProjectile(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessLevel.Developer)
                return;

            string user;

            user = IsEditorLocked(index, (byte)EditorType.Projectile);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) Color.BrightRed);
                return;
            }

            SendProjectiles(index);

            Core.Data.TempPlayer[index].Editor = (byte)EditorType.Projectile;

            buffer.WriteInt32((int) ServerPackets.SProjectileEditor);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void HandleSaveProjectile(int index, ref byte[] data)
        {
            int ProjectileNum;
            var buffer = new ByteStream(data);

            if (GetPlayerAccess(index) < (byte)AccessLevel.Developer)
                return;

            ProjectileNum = buffer.ReadInt32();

            // Prevent hacking
            if (ProjectileNum < 0 | ProjectileNum > Core.Constant.MAX_PROJECTILES)
            {
                return;
            }

            Data.Projectile[ProjectileNum].Name = buffer.ReadString();
            Data.Projectile[ProjectileNum].Sprite = buffer.ReadInt32();
            Data.Projectile[ProjectileNum].Range = (byte)buffer.ReadInt32();
            Data.Projectile[ProjectileNum].Speed = buffer.ReadInt32();
            Data.Projectile[ProjectileNum].Damage = buffer.ReadInt32();

            // Save it
            SendUpdateProjectileToAll(ProjectileNum);
            SaveProjectile(ProjectileNum);
            Core.Log.Add(GetAccountLogin(index) + " saved Projectile #" + ProjectileNum + ".", Constant.ADMIN_LOG);
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
            Core.TargetType TargetType;
            int TargetZone;
            int mapNum;
            int Damage;
            int armor;
            int NpcNum;
            var buffer = new ByteStream(data);
            ProjectileNum = buffer.ReadInt32();
            Targetindex = buffer.ReadInt32();
            TargetType = (Core.TargetType)buffer.ReadInt32();
            TargetZone = buffer.ReadInt32();
            buffer.Dispose();

            mapNum = GetPlayerMap(index);

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
            buffer.WriteString(Data.Projectile[ProjectileNum].Name);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Sprite);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Range);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Speed);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Damage);

            NetworkConfig.SendDataToAll(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void SendUpdateProjectileTo(int index, int ProjectileNum)
        {
            ByteStream buffer;

            buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateProjectile);
            buffer.WriteInt32(ProjectileNum);
            buffer.WriteString(Data.Projectile[ProjectileNum].Name);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Sprite);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Range);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Speed);
            buffer.WriteInt32(Data.Projectile[ProjectileNum].Damage);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);
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
                if (Strings.Len(Data.Projectile[i].Name) > 0)
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

            var withBlock = Data.MapProjectile[mapNum, ProjectileNum];
            buffer.WriteInt32(ProjectileNum);
            buffer.WriteInt32(withBlock.ProjectileNum);
            buffer.WriteInt32(withBlock.Owner);
            buffer.WriteInt32(withBlock.OwnerType);
            buffer.WriteInt32(withBlock.Dir);
            buffer.WriteInt32(withBlock.X);
            buffer.WriteInt32(withBlock.Y);          

            NetworkConfig.SendDataToMap(mapNum, buffer.UnreadData, buffer.WritePosition);
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
                if (Data.MapProjectile[mapNum, i].ProjectileNum == -1) // Free Projectile
                {
                    ProjectileSlot = i;
                    break;
                }
            }

            // Check for skill, if so then load data acordingly
            if (IsSkill > 0)
            {
                ProjectileNum = Data.Skill[IsSkill].Projectile;
            }
            else
            {
                ProjectileNum = Core.Data.Item[(int)GetPlayerEquipment(index, Equipment.Weapon)].Projectile;
            }

            if (ProjectileNum == -1)
                return;

            {
                var withBlock = Data.MapProjectile[mapNum, ProjectileSlot];
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