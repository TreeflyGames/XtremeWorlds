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

    static class Animation
    {

        #region Drawing
        internal static void DrawAnimation(int index, int layer)
        {
            if (AnimInstance[index].Animation == 0)
                return;

            int sprite = Core.Type.Animation[AnimInstance[index].Animation].Sprite[layer];
            if (sprite < 1 | sprite > GameState.NumAnimations)
                return;

            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, sprite.ToString()));

            // Get dimensions and column count from controls and graphic info
            int totalWidth = gfxInfo.Width;
            int totalHeight = gfxInfo.Height;
            int columns = Core.Type.Animation[AnimInstance[index].Animation].Frames[layer];
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
                case (byte)Core.Enum.TargetType.Player:
                    {
                        if (IsPlaying(lockindex) && GetPlayerMap(lockindex) == GetPlayerMap(GameState.MyIndex))
                        {
                            x = (int)Math.Round(GetPlayerX(lockindex) * GameState.PicX + 16 - width / 2d + Core.Type.Player[lockindex].XOffset);
                            y = (int)Math.Round(GetPlayerY(lockindex) * GameState.PicY + 16 - height / 2d + Core.Type.Player[lockindex].YOffset);
                        }

                        break;
                    }
                case (byte)Core.Enum.TargetType.NPC:
                    {
                        if (Core.Type.MyMapNPC[lockindex].Num > 0 && Core.Type.MyMapNPC[lockindex].Vital[(int)Core.Enum.VitalType.HP] > 0)
                        {
                            x = (int)Math.Round(Core.Type.MyMapNPC[lockindex].X * GameState.PicX + 16 - width / 2d + Core.Type.MyMapNPC[lockindex].XOffset);
                            y = (int)Math.Round(Core.Type.MyMapNPC[lockindex].Y * GameState.PicY + 16 - height / 2d + Core.Type.MyMapNPC[lockindex].YOffset);
                        }

                        break;
                    }
                case (byte)Core.Enum.TargetType.Pet:
                    {
                        if (IsPlaying(lockindex) && Pet.PetAlive(lockindex) && GetPlayerMap(lockindex) == GetPlayerMap(GameState.MyIndex))
                        {
                            x = (int)Math.Round(Core.Type.Player[lockindex].Pet.X * GameState.PicX + 16 - width / 2d + Core.Type.Player[lockindex].Pet.XOffset);
                            y = (int)Math.Round(Core.Type.Player[lockindex].Pet.Y * GameState.PicY + 16 - height / 2d + Core.Type.Player[lockindex].Pet.YOffset);
                        }

                        break;
                    }
            }

            return new Point(x, y);
        }

        internal static void CheckAnimInstance(int index)
        {
            int looptime;
            var layer = default(int);
            string sound;

            // if doesn't exist then exit sub
            if (AnimInstance[index].Animation <= 0)
                return;

            if (AnimInstance[index].Animation > Constant.MAX_ANIMATIONS)
                return;

            StreamAnimation(AnimInstance[index].Animation);

            // Get dimensions and column count from controls and graphic info
            int totalWidth = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, AnimInstance[index].Animation.ToString())).Width;
            int totalHeight = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, AnimInstance[index].Animation.ToString())).Height;
            int columns = Core.Type.Animation[AnimInstance[index].Animation].Frames[layer];

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
                    looptime = Core.Type.Animation[AnimInstance[index].Animation].LoopTime[layer];

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
                            if (AnimInstance[index].LoopIndex[layer] > Core.Type.Animation[AnimInstance[index].Animation].LoopCount[layer])
                            {
                                AnimInstance[index].Used[layer] = Conversions.ToBoolean(0);
                            }
                            else
                            {
                                AnimInstance[index].FrameIndex[layer] = 1;
                                sound = Core.Type.Animation[AnimInstance[index].Animation].Sound;
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
            if (Conversions.ToInteger(AnimInstance[index].Used[0]) == 0 & Conversions.ToInteger(AnimInstance[index].Used[1]) == 0)
            {
                ClearAnimInstance(index);
            }
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
                withBlock.Used[0] = Conversions.ToBoolean(1);
                withBlock.Used[1] = Conversions.ToBoolean(1);

                sound = Core.Type.Animation[withBlock.Animation].Sound;
                if (!string.IsNullOrEmpty(sound))
                    Sound.PlaySound(sound, withBlock.X, withBlock.Y);
            }
        }

        #endregion

        #region Globals

        internal static byte AnimationIndex;
        internal static Core.Type.AnimInstanceStruct[] AnimInstance;

        #endregion

        #region Database

        public static void ClearAnimation(int index)
        {
            Core.Type.Animation[index] = default;
            Core.Type.Animation[index] = new Core.Type.AnimationStruct();

            for (int X = 0; X <= 1; X++)
                Core.Type.Animation[index].Sprite = new int[X + 1];

            for (int X = 0; X <= 1; X++)
                Core.Type.Animation[index].Frames = new int[X + 1];

            for (int x = 0; x <= 1; x++)
                Core.Type.Animation[index].Frames[x] = 5;

            for (int X = 0; X <= 1; X++)
                Core.Type.Animation[index].LoopCount = new int[X + 1];

            for (int X = 0; X <= 1; X++)
                Core.Type.Animation[index].LoopTime = new int[X + 1];

            Core.Type.Animation[index].Name = "";
            Core.Type.Animation[index].LoopCount[0] = 1;
            Core.Type.Animation[index].LoopCount[1] = 1;
            Core.Type.Animation[index].LoopTime[0] = 1;
            Core.Type.Animation[index].LoopTime[1] = 1;
            GameState.Animation_Loaded[index] = 0;
        }

        public static void ClearAnimations()
        {
            int i;

            Core.Type.Animation = new Core.Type.AnimationStruct[101];

            for (i = 0; i < Constant.MAX_ANIMATIONS; i++)
                ClearAnimation(i);
        }

        public static void ClearAnimInstances()
        {
            int i;

            AnimInstance = new Core.Type.AnimInstanceStruct[(byte.MaxValue + 1)];

            for (i = 0; i <= byte.MaxValue - 1; i++)
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

            for (int i = 0, loopTo = Information.UBound(AnimInstance[index].Used); i <= loopTo; i++)
                AnimInstance[index].Used[i] = Conversions.ToBoolean(0);

            for (int i = 0, loopTo1 = Information.UBound(AnimInstance[index].Timer); i <= loopTo1; i++)

                AnimInstance[index].Timer[i] = 0;

            for (int i = 0, loopTo2 = Information.UBound(AnimInstance[index].FrameIndex); i <= loopTo2; i++)
                AnimInstance[index].FrameIndex[i] = 0;

            AnimInstance[index].LockType = 0;
            AnimInstance[index].LockIndex = 0;
        }

        public static void StreamAnimation(int animationNum)
        {
            if (Conversions.ToBoolean(Operators.OrObject(animationNum > 0 & string.IsNullOrEmpty(Core.Type.Animation[animationNum].Name), Operators.ConditionalCompareObjectEqual(GameState.Animation_Loaded[animationNum], 0, false))))
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
            var loopTo = Information.UBound(Core.Type.Animation[n].Frames);
            for (i = 0; i < loopTo; i++)
                Core.Type.Animation[n].Frames[i] = buffer.ReadInt32();

            var loopTo1 = Information.UBound(Core.Type.Animation[n].LoopCount);
            for (i = 0; i <= loopTo1; i++)
                Core.Type.Animation[n].LoopCount[i] = buffer.ReadInt32();

            var loopTo2 = Information.UBound(Core.Type.Animation[n].LoopTime);
            for (i = 0; i <= loopTo2; i++)
                Core.Type.Animation[n].LoopTime[i] = buffer.ReadInt32();

            Core.Type.Animation[n].Name = buffer.ReadString();
            Core.Type.Animation[n].Sound = buffer.ReadString();

            var loopTo3 = Information.UBound(Core.Type.Animation[n].Sprite);
            for (i = 0; i <= loopTo3; i++)
                Core.Type.Animation[n].Sprite[i] = buffer.ReadInt32();
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
                withBlock.Used[0] = Conversions.ToBoolean(1);
                withBlock.Used[1] = Conversions.ToBoolean(1);
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

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}