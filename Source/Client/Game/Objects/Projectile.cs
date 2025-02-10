using System;
using Core;
using static Core.Global.Command;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    internal static class Projectile
    {

        #region Sending
        public static void SendRequestEditProjectiles()
        {
            ByteStream buffer;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditProjectile);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendSaveProjectile(int ProjectileNum)
        {
            ByteStream buffer;

            buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveProjectile);
            buffer.WriteInt32(ProjectileNum);

            buffer.WriteString(Core.Type.Projectile[ProjectileNum].Name);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Sprite);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Range);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Speed);
            buffer.WriteInt32(Core.Type.Projectile[ProjectileNum].Damage);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendRequestProjectile(int projectileNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestProjectile);
            buffer.WriteInt32(projectileNum);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        public static void SendClearProjectile(int projectileNum, int collisionindex, byte collisionType, int collisionZone)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CClearProjectile);
            buffer.WriteInt32(projectileNum);
            buffer.WriteInt32(collisionindex);
            buffer.WriteInt32(collisionType);
            buffer.WriteInt32(collisionZone);

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

        }

        #endregion

        #region Recieving

        internal static void HandleUpdateProjectile(ref byte[] data)
        {
            int projectileNum;
            var buffer = new ByteStream(data);
            projectileNum = buffer.ReadInt32();

            Core.Type.Projectile[projectileNum].Name = buffer.ReadString();
            Core.Type.Projectile[projectileNum].Sprite = buffer.ReadInt32();
            Core.Type.Projectile[projectileNum].Range = (byte)buffer.ReadInt32();
            Core.Type.Projectile[projectileNum].Speed = buffer.ReadInt32();
            Core.Type.Projectile[projectileNum].Damage = buffer.ReadInt32();

            buffer.Dispose();

        }

        internal static void HandleMapProjectile(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, i];
                withBlock.ProjectileNum = buffer.ReadInt32();
                withBlock.Owner = buffer.ReadInt32();
                withBlock.OwnerType = (byte)buffer.ReadInt32();
                withBlock.Dir = (byte)buffer.ReadInt32();
                withBlock.X = buffer.ReadInt32();
                withBlock.Y = buffer.ReadInt32();
                withBlock.Range = 0;
                withBlock.Timer = General.GetTickCount() + 60000;
            }

            buffer.Dispose();

        }

        #endregion

        #region Database

        public static void ClearProjectile()
        {
            int i;

            for (i = 0; i < Constant.MAX_PROJECTILES;  i++)
                ClearProjectile(i);

        }

        public static void ClearProjectile(int index)
        {

            Core.Type.Projectile[index].Name = "";
            Core.Type.Projectile[index].Sprite = 0;
            Core.Type.Projectile[index].Range = 0;
            Core.Type.Projectile[index].Speed = 0;
            Core.Type.Projectile[index].Damage = 0;

        }

        public static void ClearMapProjectile(int projectileNum)
        {

            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum = 0;
            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Owner = 0;
            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].OwnerType = 0;
            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].X = 0;
            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Y = 0;
            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Dir = 0;
            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Timer = 0;

        }

        public static void StreamProjectile(int projectileNum)
        {
            if (projectileNum >= 0 & string.IsNullOrEmpty(Core.Type.Projectile[projectileNum].Name) && GameState.Projectile_Loaded[projectileNum] == 0)
            {
                GameState.Projectile_Loaded[projectileNum] = 1;
                SendRequestProjectile(projectileNum);
            }
        }

        #endregion

        #region Drawing
        internal static void DrawProjectile(int projectileNum)
        {
            Core.Type.RectStruct rec;
            var canClearProjectile = default(bool);
            var collisionindex = default(int);
            var collisionType = default(byte);
            var collisionZone = default(int);
            int xOffset;
            int yOffset;
            int x;
            int y;
            int i;
            int sprite;

            StreamProjectile(projectileNum);

            // check to see if it's time to move the Projectile
            if (General.GetTickCount() > Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].TravelTime)
            {
                switch (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Dir)
                {
                    case (byte)Core.Enum.DirectionType.Up:
                        {
                            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Y = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Y - 1;
                            break;
                        }
                    case (byte)Core.Enum.DirectionType.Down:
                        {
                            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Y = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Y + 1;
                            break;
                        }
                    case (byte)Core.Enum.DirectionType.Left:
                        {
                            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].X = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].X - 1;
                            break;
                        }
                    case (byte)Core.Enum.DirectionType.Right:
                        {
                            Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].X = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].X + 1;
                            break;
                        }
                }
                Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].TravelTime = General.GetTickCount() + Core.Type.Projectile[Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed;
                Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Range = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Range + 1;
            }

            x = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].X;
            y = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Y;

            // Check if its been going for over 1 minute, if so clear.
            if (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Timer < General.GetTickCount())
                canClearProjectile = Conversions.ToBoolean(1);

            if (x > Core.Type.MyMap.MaxX | x < 0)
                canClearProjectile = Conversions.ToBoolean(1);
            if (y > Core.Type.MyMap.MaxY | y < 0)
                canClearProjectile = Conversions.ToBoolean(1);

            // Check for blocked wall collision
            if (Conversions.ToInteger(canClearProjectile) == 0) // Add a check to prevent crashing
            {
                if (Core.Type.MyMap.Tile[x, y].Type == Core.Enum.TileType.Blocked | Core.Type.MyMap.Tile[x, y].Type2 == Core.Enum.TileType.Blocked)
                {
                    canClearProjectile = Conversions.ToBoolean(1);
                }
            }

            // Check for NPC collision
            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                if (Core.Type.MyMapNPC[i].X == x & Core.Type.MyMapNPC[i].Y == y)
                {
                    canClearProjectile = Conversions.ToBoolean(1);
                    collisionindex = i;
                    collisionType = (byte)Core.Enum.TargetType.NPC;
                    collisionZone = -1;
                    break;
                }
            }

            // Check for player collision
            for (i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                if (IsPlaying(i) & GetPlayerMap(i) == GetPlayerMap(GameState.MyIndex))
                {
                    if (GetPlayerX(i) == x & GetPlayerY(i) == y)
                    {
                        canClearProjectile = Conversions.ToBoolean(1);
                        collisionindex = i;
                        collisionType = (byte)Core.Enum.TargetType.Player;
                        collisionZone = -1;
                        if (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].OwnerType == (byte)Core.Enum.TargetType.Player)
                        {
                            if (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Owner == i)
                                canClearProjectile = Conversions.ToBoolean(0); // Reset if its the owner of projectile
                        }
                        break;
                    }

                }
            }

            // Check if it has hit its maximum range
            if (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Range >= Core.Type.Projectile[Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Range + 1)
                canClearProjectile = Conversions.ToBoolean(1);

            // Clear the projectile if possible
            if (Conversions.ToInteger(canClearProjectile) == 1)
            {
                // Only send the clear to the server if you're the projectile caster or the one hit (only if owner is not a player)
                if (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].OwnerType == (byte)Core.Enum.TargetType.Player & Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Owner == GameState.MyIndex)
                {
                    SendClearProjectile(projectileNum, collisionindex, collisionType, collisionZone);
                }

                ClearMapProjectile(projectileNum);
                return;
            }

            sprite = Core.Type.Projectile[Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Sprite;
            if (sprite < 1 | sprite > GameState.NumProjectiles)
                return;

            // src rect
            rec.Top = 0d;
            rec.Bottom = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Projectiles, sprite.ToString())).Height;
            rec.Left = Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Dir * GameState.PicX;
            rec.Right = rec.Left + GameState.PicX;

            // Find the offset
            switch (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].Dir)
            {
                case (byte)Core.Enum.DirectionType.Up:
                    {
                        yOffset = (int)Math.Round((Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Core.Type.Projectile[Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed * GameState.PicY);
                        break;
                    }
                case (byte)Core.Enum.DirectionType.Down:
                    {
                        yOffset = (int)Math.Round(-((Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Core.Type.Projectile[Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed) * GameState.PicY);
                        break;
                    }
                case (byte)Core.Enum.DirectionType.Left:
                    {
                        xOffset = (int)Math.Round((Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Core.Type.Projectile[Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed * GameState.PicX);
                        break;
                    }
                case (byte)Core.Enum.DirectionType.Right:
                    {
                        xOffset = (int)Math.Round(-((Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Core.Type.Projectile[Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed) * GameState.PicX);
                        break;
                    }
            }

            // Convert coordinates
            x = GameLogic.ConvertMapX(x * GameState.PicX);
            y = GameLogic.ConvertMapY(y * GameState.PicY);

            // Render texture
            string argpath = System.IO.Path.Combine(Core.Path.Projectiles, sprite.ToString());
            GameClient.RenderTexture(ref argpath, x, y, (int)Math.Round(rec.Left), (int)Math.Round(rec.Top), 32, 32);

        }

        #endregion

    }
}