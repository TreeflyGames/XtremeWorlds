using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using static Core.Global.Command;
using Path = Core.Path;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Client
{
    public class Animation
    {

        #region Drawing
        public static void DrawAnimation(int index, int layer)
        {
            int sprite = Data.Animation[AnimInstance[index].Animation].Sprite[layer];
            if (sprite < 1 | sprite > GameState.NumAnimations)
                return;

            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, sprite.ToString()));

            if (gfxInfo == null)
                return;

            // Get dimensions and column count from controls and graphic info
            int totalWidth = gfxInfo.Width;
            int totalHeight = gfxInfo.Height;
            int columns = Data.Animation[AnimInstance[index].Animation].Frames[layer];
            var frameWidth = default(int);

            // Calculate frame dimensions
            if (columns > 0)
            {
                frameWidth = (int)Math.Round(totalWidth / (double)columns);
            }

            int frameHeight = frameWidth;
            var rows = default(int);
            if (frameHeight > 0)
            {
                rows = (int)Math.Round(totalHeight / (double)frameHeight);
            }

            int frameCount = rows * columns;
            var frameIndex = default(int);

            // Calculate the current frame index
            if (frameCount > 0)
            {
                frameIndex = AnimInstance[index].FrameIndex[layer] % frameCount;
            }

            var column = default(int);
            var row = default(int);

            if (columns > 0)
            {
                column = frameIndex % columns;
                row = frameIndex / columns;
            }

            // Calculate the source rectangle for the texture or image
            var sRect = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

            // Determine the position based on lock type and instance status
            int x;
            int y;

            if (AnimInstance[index].LockType > 0)
            {
                int lockindex = AnimInstance[index].LockIndex;
                var point = GetLockedPosition(index, lockindex, frameWidth, frameHeight);
                x = point.X;
                y = point.Y;
            }
            else
            {
                x = (int)Math.Round(AnimInstance[index].X * 32 + 16 - frameWidth / 2d);
                y = (int)Math.Round(AnimInstance[index].Y * 32 + 16 - frameHeight / 2d);
            }

            x = GameLogic.ConvertMapX(x);
            y = GameLogic.ConvertMapY(y);

            // Render the frame using the calculated source rectangle and position
            string argpath = System.IO.Path.Combine(Path.Animations, sprite.ToString());
            GameClient.RenderTexture(ref argpath, x, y, sRect.X, sRect.Y, frameWidth, frameHeight, frameWidth, frameHeight);
        }

        private static Point GetLockedPosition(int index, int lockindex, int width, int height)
        {
            int x = 0;
            int y = 0;

            switch (AnimInstance[index].LockType)
            {
                case (byte)TargetType.Player:
                    {
                        if (IsPlaying(lockindex) && GetPlayerMap(lockindex) == GetPlayerMap(GameState.MyIndex))
                        {
                            x = (int)Math.Round(GetPlayerX(lockindex) * GameState.PicX + 16 - width / 2d + Core.Data.Player[lockindex].XOffset);
                            y = (int)Math.Round(GetPlayerY(lockindex) * GameState.PicY + 16 - height / 2d + Core.Data.Player[lockindex].YOffset);
                        }

                        break;
                    }
                case (byte)TargetType.Npc:
                    {
                        if (Data.MyMapNpc[lockindex].Num >= 0 && Data.MyMapNpc[lockindex].Vital[(int)Core.Vital.Health] > 0)
                        {
                            x = (int)Math.Round(Data.MyMapNpc[lockindex].X * GameState.PicX + 16 - width / 2d + Data.MyMapNpc[lockindex].XOffset);
                            y = (int)Math.Round(Data.MyMapNpc[lockindex].Y * GameState.PicY + 16 - height / 2d + Data.MyMapNpc[lockindex].YOffset);
                        }

                        break;
                    }
                case (byte)TargetType.Pet:
                    {
                        break;
                    }
            }

            return new Point(x, y);
        }

        public static void CheckAnimInstance(int index)
        {
            int looptime;
            var layer = default(int);
            string sound;

            // if doesn't exist then exit sub
            if (AnimInstance[index].Animation < 0 || AnimInstance[index].Animation > Constant.MAX_ANIMATIONS)
                return;

            StreamAnimation(AnimInstance[index].Animation);

            if (Data.Animation[AnimInstance[index].Animation].Sprite[layer] < 1 || Data.Animation[AnimInstance[index].Animation].Sprite[layer] > GameState.NumAnimations)
                return;

            // Get dimensions and column count from controls and graphic info
            int totalWidth = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, Data.Animation[AnimInstance[index].Animation].Sprite[layer].ToString())).Width;
            int totalHeight = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, Data.Animation[AnimInstance[index].Animation].Sprite[layer].ToString())).Height;
            int columns = Data.Animation[AnimInstance[index].Animation].Frames[layer];

            // Calculate frame dimensions
            int frameWidth = (int)Math.Round(totalWidth / (double)columns);
            int frameHeight = frameWidth;
            var rows = default(int);
            if (frameHeight > 0)
            {
                rows = (int)Math.Round(totalHeight / (double)frameHeight);
            }

            int frameCount = rows * columns;
            var frameIndex = default(int);

            if (AnimInstance[index].FrameIndex == null || AnimInstance[index].FrameIndex.Length <= layer)
            {
                // Handle the error or initialize the array
                return;
            }

            // Calculate the current frame index
            if (frameCount > 0)
            {
                frameIndex = AnimInstance[index].FrameIndex[layer] % frameCount;
            }

            int column = frameIndex % columns;
            int row = frameIndex / columns;

            for (layer = 0; layer <= 1; layer++)
            {
                if (AnimInstance[index].Used[layer])
                {
                    looptime = Data.Animation[AnimInstance[index].Animation].LoopTime[layer];

                    // if zero'd then set so we don't have extra loop and/or frame
                    if (AnimInstance[index].FrameIndex[layer] == 0)
                        AnimInstance[index].FrameIndex[layer] = 1;

                    if (AnimInstance[index].LoopIndex[layer] == 0)
                        AnimInstance[index].LoopIndex[layer] = 1;

                    // check if frame timer is set, and needs to have a frame change
                    if (AnimInstance[index].Timer[layer] + looptime <= General.GetTickCount())
                    {
                        // check if out of range
                        if (AnimInstance[index].FrameIndex[layer] >= frameCount)
                        {
                            AnimInstance[index].LoopIndex[layer] = AnimInstance[index].LoopIndex[layer] + 1;
                            if (AnimInstance[index].LoopIndex[layer] > Data.Animation[AnimInstance[index].Animation].LoopCount[layer])
                            {
                                AnimInstance[index].Used[layer] = false;
                            }
                            else
                            {
                                AnimInstance[index].FrameIndex[layer] = 1;
                                sound = Data.Animation[AnimInstance[index].Animation].Sound;
                                if (!string.IsNullOrEmpty(sound))
                                    Sound.PlaySound(sound, AnimInstance[index].X, AnimInstance[index].Y);
                            }
                        }
                        else
                        {
                            AnimInstance[index].FrameIndex[layer] += 1;
                        }
                        AnimInstance[index].Timer[layer] = General.GetTickCount();
                    }
                }
            }

            // if neither layer is used, clear
            if ((AnimInstance[index].Used[0] == false & AnimInstance[index].Used[1] == false))
            {
                ClearAnimInstance(index);
            }
        }

        public static int PlayAnimation(int sprite, int layer, int data, byte x, byte y)
        {
            Animation.StreamAnimation(data);

            if (sprite == 0)
                return 0;

            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, sprite.ToString()));

            if (gfxInfo == null)
                return 0;        

            // Get dimensions and column count from controls and graphic info
            int totalWidth = gfxInfo.Width;
            int totalHeight = gfxInfo.Height;
            int columns = Data.Animation[data].Frames[layer];
            int frameWidth = 0;
            int rows = 0;

            // Calculate frame dimensions
            if (columns > 0)
            {
                frameWidth = (int)Math.Round(totalWidth / (double)columns);
            }

            int frameHeight = frameWidth;

            if (frameHeight > 0)
            {
                rows = (int)Math.Round(totalHeight / (double)frameHeight);
            }
            int frameCount = rows * columns;

            Animation.CreateAnimation(data, x, y);
            return Data.Animation[data].LoopTime[layer] * frameCount * Data.Animation[data].LoopCount[layer];
        }

        public static void CreateAnimation(int animationNum, byte x, byte y)
        {
            string sound;
            AnimationIndex = (byte)(AnimationIndex + 1);
            if (AnimationIndex >= byte.MaxValue)
                AnimationIndex = 1;

            {
                ref var withBlock = ref AnimInstance[AnimationIndex];
                withBlock.Animation = animationNum;
                withBlock.X = x;
                withBlock.Y = y;
                withBlock.LockType = 0;
                withBlock.LockIndex = 0;
                withBlock.Used[0] = true;
                withBlock.Used[1] = true;

                sound = Data.Animation[withBlock.Animation].Sound;
                if (!string.IsNullOrEmpty(sound))
                    Sound.PlaySound(sound, withBlock.X, withBlock.Y);
            }
        }

        #endregion

        #region Globals

        public static byte AnimationIndex;
        public static Core.Type.AnimInstance[] AnimInstance;

        #endregion

        #region Database

        public static void ClearAnimation(int index)
        {
            Data.Animation[index] = default;
            Data.Animation[index] = new Core.Type.Animation();

            for (int X = 0; X <= 1; X++)
                Data.Animation[index].Sprite = new int[X + 1];

            for (int X = 0; X <= 1; X++)
                Data.Animation[index].Frames = new int[X + 1];

            for (int x = 0; x <= 1; x++)
                Data.Animation[index].Frames[x] = 5;

            for (int X = 0; X <= 1; X++)
                Data.Animation[index].LoopCount = new int[X + 1];

            for (int X = 0; X <= 1; X++)
                Data.Animation[index].LoopTime = new int[X + 1];

            Data.Animation[index].Name = "";
            Data.Animation[index].LoopCount[0] = 1;
            Data.Animation[index].LoopCount[1] = 1;
            Data.Animation[index].LoopTime[0] = 1;
            Data.Animation[index].LoopTime[1] = 1;
            GameState.Animation_Loaded[index] = 0;
        }

        public static void ClearAnimations()
        {
            int i;

            Data.Animation = new Core.Type.Animation[Core.Constant.MAX_ANIMATIONS];

            for (i = 0; i < Constant.MAX_ANIMATIONS; i++)
                ClearAnimation(i);
        }

        public static void ClearAnimInstances()
        {
            int i;

            AnimInstance = new Core.Type.AnimInstance[(byte.MaxValue)];

            for (i = 0; i < byte.MaxValue; i++)
            {
                for (int X = 0; X <= 1; X++)
                    AnimInstance[i].Timer = new int[X + 1];

                for (int X = 0; X <= 1; X++)
                    AnimInstance[i].Used = new bool[X + 1];

                for (int X = 0; X <= 1; X++)
                    AnimInstance[i].LoopIndex = new int[X + 1];

                for (int X = 0; X <= 1; X++)
                    AnimInstance[i].FrameIndex = new int[X + 1];

                ClearAnimInstance(i);
            }
        }

        public static void ClearAnimInstance(int index)
        {
            AnimInstance[index].Animation = 0;
            AnimInstance[index].X = 0;
            AnimInstance[index].Y = 0;

            for (int i = 0, loopTo = Information.UBound(AnimInstance[index].Used); i < loopTo; i++)
                AnimInstance[index].Used[i] = false;

            for (int i = 0, loopTo1 = Information.UBound(AnimInstance[index].Timer); i < loopTo1; i++)

                AnimInstance[index].Timer[i] = 0;

            for (int i = 0, loopTo2 = Information.UBound(AnimInstance[index].FrameIndex); i < loopTo2; i++)
                AnimInstance[index].FrameIndex[i] = 0;

            AnimInstance[index].LockType = 0;
            AnimInstance[index].LockIndex = 0;
        }

        public static void StreamAnimation(int animationNum)
        {
            if (animationNum >= 0 && string.IsNullOrEmpty(Data.Animation[animationNum].Name) && GameState.Animation_Loaded[animationNum] == 0)
            {
                GameState.Animation_Loaded[animationNum] = 1;
                SendRequestAnimation(animationNum);
            }
        }

        #endregion

        #region Incoming Traffic

        public static void Packet_UpdateAnimation(ref byte[] data)
        {
            int n;
            int i;
            var buffer = new ByteStream(data);

            n = buffer.ReadInt32();
            // Update the Animation
            var loopTo = Information.UBound(Data.Animation[n].Frames);
            for (i = 0; i < loopTo; i++)
                Data.Animation[n].Frames[i] = buffer.ReadInt32();

            var loopTo1 = Information.UBound(Data.Animation[n].LoopCount);
            for (i = 0; i < loopTo1; i++)
                Data.Animation[n].LoopCount[i] = buffer.ReadInt32();

            var loopTo2 = Information.UBound(Data.Animation[n].LoopTime);
            for (i = 0; i < loopTo2; i++)
                Data.Animation[n].LoopTime[i] = buffer.ReadInt32();

            Data.Animation[n].Name = buffer.ReadString();
            Data.Animation[n].Sound = buffer.ReadString();

            var loopTo3 = Information.UBound(Data.Animation[n].Sprite);
            for (i = 0; i < loopTo3; i++)
                Data.Animation[n].Sprite[i] = buffer.ReadInt32();
            buffer.Dispose();
        }

        public static void Packet_Animation(ref byte[] data)
        {
            var buffer = new ByteStream(data);

            AnimationIndex = (byte)(AnimationIndex + 1);
            if (AnimationIndex >= byte.MaxValue)
                AnimationIndex = 1;

            {
                ref var withBlock = ref AnimInstance[AnimationIndex];
                withBlock.Animation = buffer.ReadInt32();
                withBlock.X = buffer.ReadInt32();
                withBlock.Y = buffer.ReadInt32();
                withBlock.LockType = (byte)buffer.ReadInt32();
                withBlock.LockIndex = buffer.ReadInt32();
                withBlock.Used[0] = true;
                withBlock.Used[1] = true;
            }
            buffer.Dispose();
        }

        #endregion
        #region Outgoing Traffic

        public static void SendRequestAnimation(int animationNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CRequestAnimation);

            buffer.WriteInt32(animationNum);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();
        }

        #endregion

    }
}