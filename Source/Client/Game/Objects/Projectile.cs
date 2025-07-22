using System;
using Core;
using static Core.Global.Command;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    public class Projectile
    {

        #region Sending
        public static void SendRequestEditProjectiles()
        {
            ByteStream buffer;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditProjectile);
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void SendSaveProjectile(int ProjectileNum)
        {
            ByteStream buffer;

            buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveProjectile);
            buffer.WriteInt32(ProjectileNum);

            buffer.WriteString(Data.Projectile[ProjectileNum].Name);
            buffer.WriteInt32(Core.Data.Projectile[ProjectileNum].Sprite);
            buffer.WriteInt32(Core.Data.Projectile[ProjectileNum].Range);
            buffer.WriteInt32(Core.Data.Projectile[ProjectileNum].Speed);
            buffer.WriteInt32(Core.Data.Projectile[ProjectileNum].Damage);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void SendRequestProjectile(int projectileNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestProjectile);
            buffer.WriteInt32(projectileNum);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
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

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        #endregion

        #region Recieving

        public static void HandleUpdateProjectile(ref byte[] data)
        {
            int projectileNum;
            var buffer = new ByteStream(data);
            projectileNum = buffer.ReadInt32();

            Core.Data.Projectile[projectileNum].Name = buffer.ReadString();
            Core.Data.Projectile[projectileNum].Sprite = buffer.ReadInt32();
            Core.Data.Projectile[projectileNum].Range = (byte)buffer.ReadInt32();
            Core.Data.Projectile[projectileNum].Speed = buffer.ReadInt32();
            Core.Data.Projectile[projectileNum].Damage = buffer.ReadInt32();

            buffer.Dispose();

        }

        public static void HandleMapProjectile(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);
            i = buffer.ReadInt32();

            {
                ref var withBlock = ref Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, i];
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
            Core.Data.Projectile[index].Name = "";
            Core.Data.Projectile[index].Sprite = 0;
            Core.Data.Projectile[index].Range = 0;
            Core.Data.Projectile[index].Speed = 0;
            Core.Data.Projectile[index].Damage = 0;

        }

        public static void ClearMapProjectile(int projectileNum)
        {
            Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum = 0;
            Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Owner = 0;
            Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].OwnerType = 0;
            Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].X = 0;
            Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Y = 0;
            Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Dir = 0;
            Core.Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Timer = 0;

        }

        public static void StreamProjectile(int projectileNum)
        {
            if (projectileNum >= 0 & string.IsNullOrEmpty(Core.Data.Projectile[projectileNum].Name) && GameState.Projectile_Loaded[projectileNum] == 0)
            {
                GameState.Projectile_Loaded[projectileNum] = 1;
                SendRequestProjectile(projectileNum);
            }
        }

        #endregion

        #region Drawing
        public static void DrawProjectile(int projectileNum)
        {
            Core.Type.Rect rec;
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
            if (General.GetTickCount() > Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].TravelTime)
            {
                switch (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Dir)
                {
                    case (byte)Direction.Up:
                        {
                            Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Y = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Y - 1;
                            break;
                        }
                    case (byte)Direction.Down:
                        {
                            Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Y = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Y + 1;
                            break;
                        }
                    case (byte)Direction.Left:
                        {
                            Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].X = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].X - 1;
                            break;
                        }
                    case (byte)Direction.Right:
                        {
                            Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].X = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].X + 1;
                            break;
                        }
                }
                Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].TravelTime = General.GetTickCount() + Data.Projectile[Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed;
                Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Range = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Range + 1;
            }

            x = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].X;
            y = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Y;

            // Check if its been going for over 1 minute, if so clear.
            if (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Timer < General.GetTickCount())
                canClearProjectile = true;

            if (x > Data.MyMap.MaxX | x < 0)
                canClearProjectile = true;

            if (y > Data.MyMap.MaxY | y < 0)
                canClearProjectile = true;

            // Check for blocked wall collision
            if (Conversions.ToInteger(canClearProjectile) == 0) // Add a check to prevent crashing
            {
                if (Data.MyMap.Tile[x, y].Type == TileType.Blocked | Data.MyMap.Tile[x, y].Type2 == TileType.Blocked)
                {
                    canClearProjectile = true;
                }
            }

            // Check for Npc collision
            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                if (Data.MyMapNpc[i].X == x & Data.MyMapNpc[i].Y == y)
                {
                    canClearProjectile = true;
                    collisionindex = i;
                    collisionType = (byte)TargetType.Npc;
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
                        canClearProjectile = true;
                        collisionindex = i;
                        collisionType = (byte)TargetType.Player;
                        collisionZone = -1;
                        if (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].OwnerType == (byte)TargetType.Player)
                        {
                            if (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Owner == i)
                                canClearProjectile = false; // Reset if its the owner of projectile
                        }
                        break;
                    }

                }
            }

            // Check if it has hit its maximum range
            if (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Range >= Data.Projectile[Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Range + 1)
                canClearProjectile = true;

            // Clear the projectile if possible
            if (Conversions.ToInteger(canClearProjectile) == 1)
            {
                // Only send the clear to the server if you're the projectile caster or the one hit (only if owner is not a player)
                if (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].OwnerType == (byte)TargetType.Player & Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Owner == GameState.MyIndex)
                {
                    SendClearProjectile(projectileNum, collisionindex, collisionType, collisionZone);
                }

                ClearMapProjectile(projectileNum);
                return;
            }

            sprite = Data.Projectile[Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Sprite;
            if (sprite < 1 | sprite > GameState.NumProjectiles)
                return;

            // src rect
            rec.Top = 0d;
            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Projectiles, sprite.ToString()));
            if (gfxInfo == null)
            {
                return;
            }
            rec.Bottom = gfxInfo.Height;
            rec.Left = Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Dir * GameState.PicX;
            rec.Right = rec.Left + GameState.PicX;

            // Find the offset
            switch (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].Dir)
            {
                case (byte)Direction.Up:
                    {
                        yOffset = (int)Math.Round((Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Data.Projectile[Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed * GameState.PicY);
                        break;
                    }
                case (byte)Direction.Down:
                    {
                        yOffset = (int)Math.Round(-((Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Data.Projectile[Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed) * GameState.PicY);
                        break;
                    }
                case (byte)Direction.Left:
                    {
                        xOffset = (int)Math.Round((Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Data.Projectile[Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed * GameState.PicX);
                        break;
                    }
                case (byte)Direction.Right:
                    {
                        xOffset = (int)Math.Round(-((Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].TravelTime - General.GetTickCount()) / (double)Data.Projectile[Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, projectileNum].ProjectileNum].Speed) * GameState.PicX);
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