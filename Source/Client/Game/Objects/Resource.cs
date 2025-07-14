using System;
using System.Drawing;
using Core;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;

namespace Client
{

    public class MapResource
    {

        #region Database

        public static void ClearResource(int index)
        {
            Core.Type.Resource[index] = default;
            Core.Type.Resource[index].Name = "";
            GameState.Resource_Loaded[index] = 0;
        }

        public static void ClearResources()
        {
            Array.Resize(ref Core.Type.Resource, Constant.MAX_RESOURCES);

            for (int i = 0; i < Constant.MAX_RESOURCES; i++)
                ClearResource(i);

        }

        public static void StreamResource(int resourceNum)
        {
            if (resourceNum >= 0 && string.IsNullOrEmpty(Core.Type.Resource[resourceNum].Name) && GameState.Resource_Loaded[resourceNum] == 0)
            {
                GameState.Resource_Loaded[resourceNum] = 1;
                SendRequestResource(resourceNum);
            }
        }

        #endregion

        #region Incoming Packets

        public static void Packet_MapResource(ref byte[] data)
        {
            int i;
            var buffer = new ByteStream(data);
            GameState.ResourceIndex = buffer.ReadInt32();
            GameState.ResourcesInit = false;

            if (GameState.ResourceIndex > 0)
            {
                Array.Resize(ref Core.Type.MapResource, GameState.ResourceIndex);
                Array.Resize(ref Core.Type.MyMapResource, GameState.ResourceIndex);

                var loopTo = GameState.ResourceIndex;
                for (i = 0; i < loopTo; i++)
                {
                    Core.Type.MyMapResource[i].State = buffer.ReadByte();
                    Core.Type.MyMapResource[i].X = buffer.ReadInt32();
                    Core.Type.MyMapResource[i].Y = buffer.ReadInt32();
                }

                GameState.ResourcesInit = true;
            }

            buffer.Dispose();
        }

        public static void Packet_UpdateResource(ref byte[] data)
        {
            int resourceNum;
            var buffer = new ByteStream(data);
            resourceNum = buffer.ReadInt32();

            Core.Type.Resource[resourceNum].Animation = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].EmptyMessage = buffer.ReadString();
            Core.Type.Resource[resourceNum].ExhaustedImage = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].Health = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].ExpReward = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].ItemReward = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].Name = buffer.ReadString();
            Core.Type.Resource[resourceNum].ResourceImage = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].ResourceType = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].RespawnTime = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].SuccessMessage = buffer.ReadString();
            Core.Type.Resource[resourceNum].LvlRequired = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].ToolRequired = buffer.ReadInt32();
            Core.Type.Resource[resourceNum].Walkthrough = Conversions.ToBoolean(buffer.ReadInt32());

            buffer.Dispose();
        }

        #endregion

        #region Outgoing Packets

        public static void SendRequestResource(int resourceNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestResource);

            buffer.WriteInt32(resourceNum);
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        #endregion

        #region Drawing

        public static void DrawResource(int resource, int dx, int dy, Rectangle rec)
        {
            int x;
            int y;
            int width;
            int height;

            if (resource < 1 | resource > GameState.NumResources)
                return;

            x = GameLogic.ConvertMapX(dx);
            y = GameLogic.ConvertMapY(dy);
            width = rec.Right - rec.Left;
            height = rec.Bottom - rec.Top;

            if (rec.Width < 0 | rec.Height < 0)
                return;

            string argpath = System.IO.Path.Combine(Core.Path.Resources, resource.ToString());
            GameClient.RenderTexture(ref argpath, x, y, rec.X, rec.Y, rec.Width, rec.Height, rec.Width, rec.Height);
        }

        public static void DrawMapResource(int resourceNum)
        {
            int mapResourceNum;
            int resourceState;
            var resourceSprite = default(int);
            var rec = default(Rectangle);
            int x;
            int y;

            if (GameState.GettingMap)
                return;

            if (!GameState.MapData)
                return;

            if (Core.Type.MyMapResource[resourceNum].X > Core.Type.MyMap.MaxX | Core.Type.MyMapResource[resourceNum].Y > Core.Type.MyMap.MaxY)
                return;

            mapResourceNum = Core.Type.MyMap.Tile[Core.Type.MyMapResource[resourceNum].X, Core.Type.MyMapResource[resourceNum].Y].Data1;

            if (mapResourceNum == 0)
                mapResourceNum = Core.Type.MyMap.Tile[Core.Type.MyMapResource[resourceNum].X, Core.Type.MyMapResource[resourceNum].Y].Data1_2;

            StreamResource(mapResourceNum);

            if (Core.Type.Resource[mapResourceNum].ResourceImage == 0)
                return;

            // Get the Resource state
            resourceState = Core.Type.MyMapResource[resourceNum].State;

            if (resourceState == 0) // normal
            {
                resourceSprite = Core.Type.Resource[mapResourceNum].ResourceImage;
            }
            else if (resourceState == 1) // used
            {
                resourceSprite = Core.Type.Resource[mapResourceNum].ExhaustedImage;
            }

            // src rect
            rec.Y = 0;
            rec.Height = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Resources, resourceSprite.ToString())).Height;
            rec.X = 0;
            rec.Width = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Resources, resourceSprite.ToString())).Width;

            // Set base x + y, then the offset due to size
            x = (int)Math.Round(Core.Type.MyMapResource[resourceNum].X * GameState.PicX - GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Resources, resourceSprite.ToString())).Width / 2d + 16d);
            y = Core.Type.MyMapResource[resourceNum].Y * GameState.PicY - GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Resources, resourceSprite.ToString())).Height + 32;

            DrawResource(resourceSprite, x, y, rec);
        }

        #endregion

    }
}