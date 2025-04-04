using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using SharpDX.Direct2D1;
using static Core.Global.Command;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Path = Core.Path;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Client
{
    public class Animation
    {
        // Define AnimationEvent struct for frame-specific events
        public struct AnimationEvent
        {
            public int Frame;
            public string Sound;
        }

        #region Drawing
        public static void DrawAnimation(int index, int layer)
        {
            int sprite = Core.Type.Animation[AnimInstance[index].Animation].Sprite[layer];
            if (sprite < 1 || sprite > GameState.NumAnimations)
                return;

            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, sprite.ToString()));
            if (gfxInfo == null)
                return;

            // Get dimensions and column count
            int totalWidth = gfxInfo.Width;
            int totalHeight = gfxInfo.Height;
            int columns = Core.Type.Animation[AnimInstance[index].Animation].Frames[layer];
            int frameWidth = columns > 0 ? (int)Math.Round(totalWidth / (double)columns) : 0;
            int frameHeight = frameWidth;
            int rows = frameHeight > 0 ? (int)Math.Round(totalHeight / (double)frameHeight) : 0;
            int frameCount = rows * columns;

            // Calculate current frame index
            int frameIndex = frameCount > 0 ? AnimInstance[index].FrameIndex[layer] % frameCount : 0;
            int column = columns > 0 ? frameIndex % columns : 0;
            int row = columns > 0 ? frameIndex / columns : 0;

            // Calculate source rectangle
            var sRect = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

            // Determine position
            int x, y;
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

            // Render the frame
            string argpath = System.IO.Path.Combine(Path.Animations, sprite.ToString());
            GameClient.RenderTexture(ref argpath, x, y, sRect.X, sRect.Y, frameWidth, frameHeight, frameWidth, frameHeight);
        }

        private static Point GetLockedPosition(int index, int lockindex, int width, int height)
        {
            int x = 0, y = 0;
            switch (AnimInstance[index].LockType)
            {
                case (byte)Core.Enum.TargetType.Player:
                    if (IsPlaying(lockindex) && GetPlayerMap(lockindex) == GetPlayerMap(GameState.MyIndex))
                    {
                        x = (int)Math.Round(GetPlayerX(lockindex) * GameState.PicX + 16 - width / 2d + Core.Type.Player[lockindex].XOffset);
                        y = (int)Math.Round(GetPlayerY(lockindex) * GameState.PicY + 16 - height / 2d + Core.Type.Player[lockindex].YOffset);
                    }
                    break;
                case (byte)Core.Enum.TargetType.NPC:
                    if (Core.Type.MyMapNPC[lockindex].Num >= 0 && Core.Type.MyMapNPC[lockindex].Vital[(int)Core.Enum.VitalType.HP] > 0)
                    {
                        x = (int)Math.Round(Core.Type.MyMapNPC[lockindex].X * GameState.PicX + 16 - width / 2d + Core.Type.MyMapNPC[lockindex].XOffset);
                        y = (int)Math.Round(Core.Type.MyMapNPC[lockindex].Y * GameState.PicY + 16 - height / 2d + Core.Type.MyMapNPC[lockindex].YOffset);
                    }
                    break;
                case (byte)Core.Enum.TargetType.Pet:
                    if (IsPlaying(lockindex) && Pet.PetAlive(lockindex) && GetPlayerMap(lockindex) == GetPlayerMap(GameState.MyIndex))
                    {
                        x = (int)Math.Round(Core.Type.Player[lockindex].Pet.X * GameState.PicX + 16 - width / 2d + Core.Type.Player[lockindex].Pet.XOffset);
                        y = (int)Math.Round(Core.Type.Player[lockindex].Pet.Y * GameState.PicY + 16 - height / 2d + Core.Type.Player[lockindex].Pet.YOffset);
                    }
                    break;
            }
            return new Point(x, y);
        }

        public static void CheckAnimInstance(int index)
        {
            if (AnimInstance[index].Animation < 0 || AnimInstance[index].Animation > Constant.MAX_ANIMATIONS)
                return;

            StreamAnimation(AnimInstance[index].Animation);

            for (int layer = 0; layer <= 1; layer++)
            {
                if (!AnimInstance[index].Used[layer] || AnimInstance[index].IsPaused[layer])
                    continue;

                int sprite = Core.Type.Animation[AnimInstance[index].Animation].Sprite[layer];
                if (sprite < 1 || sprite > GameState.NumAnimations)
                    continue;

                // Get frame dimensions
                var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Animations, sprite.ToString()));
                int totalWidth = gfxInfo.Width;
                int totalHeight = gfxInfo.Height;
                int columns = Core.Type.Animation[AnimInstance[index].Animation].Frames[layer];
                int frameWidth = (int)Math.Round(totalWidth / (double)columns);
                int frameHeight = frameWidth;
                int rows = frameHeight > 0 ? (int)Math.Round(totalHeight / (double)frameHeight) : 0;
                int frameCount = rows * columns;

                if (AnimInstance[index].FrameIndex[layer] == 0)
                {
                    AnimInstance[index].FrameIndex[layer] = AnimInstance[index].Direction[layer] > 0 ? 1 : frameCount - 1;
                    TriggerFrameEvent(index, layer, AnimInstance[index].FrameIndex[layer]);
                }

                if (AnimInstance[index].LoopIndex[layer] == 0)
                    AnimInstance[index].LoopIndex[layer] = 1;

                int looptime = Core.Type.Animation[AnimInstance[index].Animation].LoopTime[layer];
                float effectiveLoopTime = looptime / Math.Max(AnimInstance[index].SpeedMultiplier, 0.1f); // Avoid division by zero

                if (AnimInstance[index].Timer[layer] + (int)effectiveLoopTime <= General.GetTickCount())
                {
                    int direction = AnimInstance[index].Direction[layer];
                    int newFrame = AnimInstance[index].FrameIndex[layer] + direction;

                    if (direction > 0 && newFrame >= frameCount)
                    {
                        AnimInstance[index].LoopIndex[layer]++;
                        if (AnimInstance[index].LoopIndex[layer] > Core.Type.Animation[AnimInstance[index].Animation].LoopCount[layer])
                        {
                            AnimInstance[index].Used[layer] = false;
                        }
                        else
                        {
                            AnimInstance[index].FrameIndex[layer] = 1;
                            string sound = Core.Type.Animation[AnimInstance[index].Animation].Sound;
                            if (!string.IsNullOrEmpty(sound))
                                Sound.PlaySound(sound, AnimInstance[index].X, AnimInstance[index].Y);
                            TriggerFrameEvent(index, layer, 1);
                        }
                    }
                    else if (direction < 0 && newFrame < 0)
                    {
                        AnimInstance[index].LoopIndex[layer]++;
                        if (AnimInstance[index].LoopIndex[layer] > Core.Type.Animation[AnimInstance[index].Animation].LoopCount[layer])
                        {
                            AnimInstance[index].Used[layer] = false;
                        }
                        else
                        {
                            AnimInstance[index].FrameIndex[layer] = frameCount - 1;
                            string sound = Core.Type.Animation[AnimInstance[index].Animation].Sound;
                            if (!string.IsNullOrEmpty(sound))
                                Sound.PlaySound(sound, AnimInstance[index].X, AnimInstance[index].Y);
                            TriggerFrameEvent(index, layer, frameCount - 1);
                        }
                    }
                    else
                    {
                        AnimInstance[index].FrameIndex[layer] = newFrame;
                        TriggerFrameEvent(index, layer, newFrame);
                    }
                    AnimInstance[index].Timer[layer] = General.GetTickCount();
                }
            }

            if (!AnimInstance[index].Used[0] && !AnimInstance[index].Used[1])
                ClearAnimInstance(index);
        }

        private static void TriggerFrameEvent(int index, int layer, int frame)
        {
            if (Core.Type.Animation[AnimInstance[index].Animation].Events != null && 
                Core.Type.Animation[AnimInstance[index].Animation].Events[layer] != null)
            {
                foreach (var evt in Core.Type.Animation[AnimInstance[index].Animation].Events[layer])
                {
                    if (evt.Frame == frame)
                        Sound.PlaySound(evt.Sound, AnimInstance[index].X, AnimInstance[index].Y);
                }
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

            int totalWidth = gfxInfo.Width;
            int totalHeight = gfxInfo.Height;
            int columns = Core.Type.Animation[data].Frames[layer];
            int frameWidth = columns > 0 ? (int)Math.Round(totalWidth / (double)columns) : 0;
            int frameHeight = frameWidth;
            int rows = frameHeight > 0 ? (int)Math.Round(totalHeight / (double)frameHeight) : 0;
            int frameCount = rows * columns;

            Animation.CreateAnimation(data, x, y);
            return Core.Type.Animation[data].LoopTime[layer] * frameCount * Core.Type.Animation[data].LoopCount[layer];
        }

        public static void CreateAnimation(int animationNum, byte x, byte y)
        {
            AnimationIndex = (byte)(AnimationIndex + 1);
            if (AnimationIndex >= byte.MaxValue)
                AnimationIndex = 1;

            ref var withBlock = ref AnimInstance[AnimationIndex];
            withBlock.Animation = animationNum;
            withBlock.X = x;
            withBlock.Y = y;
            withBlock.LockType = 0;
            withBlock.LockIndex = 0;
            withBlock.Used[0] = true;
            withBlock.Used[1] = true;
            withBlock.SpeedMultiplier = 1.0f; // Default speed
            withBlock.IsPaused[0] = false;
            withBlock.IsPaused[1] = false;
            withBlock.Direction[0] = 1; // Forward by default
            withBlock.Direction[1] = 1;

            string sound = Core.Type.Animation[withBlock.Animation].Sound;
            if (!string.IsNullOrEmpty(sound))
                Sound.PlaySound(sound, withBlock.X, withBlock.Y);
        }

        // New methods for pausing and resuming
        public static void PauseAnimation(int index, int layer)
        {
            if (index >= 0 && index <= byte.MaxValue && layer >= 0 && layer <= 1)
                AnimInstance[index].IsPaused[layer] = true;
        }

        public static void ResumeAnimation(int index, int layer)
        {
            if (index >= 0 && index <= byte.MaxValue && layer >= 0 && layer <= 1)
            {
                AnimInstance[index].IsPaused[layer] = false;
                AnimInstance[index].Timer[layer] = General.GetTickCount(); // Reset timer to prevent jump
            }
        }

        #endregion

        #region Globals
        public static byte AnimationIndex;
        public static Core.Type.AnimInstanceStruct[] AnimInstance;
        #endregion

        #region Database
        public static void ClearAnimation(int index)
        {
            Core.Type.Animation[index] = new Core.Type.AnimationStruct();
            Core.Type.Animation[index].Sprite = new int[2];
            Core.Type.Animation[index].Frames = new int[2] { 5, 5 };
            Core.Type.Animation[index].LoopCount = new int[2] { 1, 1 };
            Core.Type.Animation[index].LoopTime = new int[2] { 1, 1 };
            Core.Type.Animation[index].Events = new List<AnimationEvent>[2];
            for (int i = 0; i <= 1; i++)
                Core.Type.Animation[index].Events[i] = new List<AnimationEvent>();
            Core.Type.Animation[index].Name = "";
            GameState.Animation_Loaded[index] = 0;
        }

        public static void ClearAnimations()
        {
            Core.Type.Animation = new Core.Type.AnimationStruct[101];
            for (int i = 0; i < Constant.MAX_ANIMATIONS; i++)
                ClearAnimation(i);
        }

        public static void ClearAnimInstances()
        {
            AnimInstance = new Core.Type.AnimInstanceStruct[byte.MaxValue + 1];
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                AnimInstance[i].Timer = new int[2];
                AnimInstance[i].Used = new bool[2];
                AnimInstance[i].LoopIndex = new int[2];
                AnimInstance[i].FrameIndex = new int[2];
                AnimInstance[i].IsPaused = new bool[2];
                AnimInstance[i].Direction = new int[2];
                ClearAnimInstance(i);
            }
        }

        public static void ClearAnimInstance(int index)
        {
            AnimInstance[index].Animation = 0;
            AnimInstance[index].X = 0;
            AnimInstance[index].Y = 0;
            for (int i = 0; i <= 1; i++)
            {
                AnimInstance[index].Used[i] = false;
                AnimInstance[index].Timer[i] = 0;
                AnimInstance[index].FrameIndex[i] = 0;
                AnimInstance[index].IsPaused[i] = false;
                AnimInstance[index].SpeedMultiplier = 1.0f;
                AnimInstance[index].Direction[i] = 1;
            }
            AnimInstance[index].LockType = 0;
            AnimInstance[index].LockIndex = 0;
        }

        public static void StreamAnimation(int animationNum)
        {
            if (animationNum >= 0 && string.IsNullOrEmpty(Core.Type.Animation[animationNum].Name) && GameState.Animation_Loaded[animationNum] == 0)
            {
                GameState.Animation_Loaded[animationNum] = 1;
                SendRequestAnimation(animationNum);
            }
        }
        #endregion

        #region Incoming Traffic
        public static void Packet_UpdateAnimation(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n = buffer.ReadInt32();
            for (int i = 0; i <= 1; i++)
                Core.Type.Animation[n].Frames[i] = buffer.ReadInt32();
            for (int i = 0; i <= 1; i++)
                Core.Type.Animation[n].LoopCount[i] = buffer.ReadInt32();
            for (int i = 0; i <= 1; i++)
                Core.Type.Animation[n].LoopTime[i] = buffer.ReadInt32();
            Core.Type.Animation[n].Name = buffer.ReadString();
            Core.Type.Animation[n].Sound = buffer.ReadString();
            for (int i = 0; i <= 1; i++)
                Core.Type.Animation[n].Sprite[i] = buffer.ReadInt32();

            // Read frame events
            for (int layer = 0; layer <= 1; layer++)
            {
                int eventCount = buffer.ReadInt32();
                Core.Type.Animation[n].Events[layer] = new List<AnimationEvent>();
                for (int j = 0; j < eventCount; j++)
                {
                    AnimationEvent evt;
                    evt.Frame = buffer.ReadInt32();
                    evt.Sound = buffer.ReadString();
                    Core.Type.Animation[n].Events[layer].Add(evt);
                }
            }
            buffer.Dispose();
        }

        public static void Packet_Animation(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            AnimationIndex = (byte)(AnimationIndex + 1);
            if (AnimationIndex >= byte.MaxValue)
                AnimationIndex = 1;

            ref var withBlock = ref AnimInstance[AnimationIndex];
            withBlock.Animation = buffer.ReadInt32();
            withBlock.X = buffer.ReadInt32();
            withBlock.Y = buffer.ReadInt32();
            withBlock.LockType = (byte)buffer.ReadInt32();
            withBlock.LockIndex = buffer.ReadInt32();
            withBlock.Used[0] = true;
            withBlock.Used[1] = true;
            withBlock.SpeedMultiplier = 1.0f;
            withBlock.IsPaused[0] = false;
            withBlock.IsPaused[1] = false;
            withBlock.Direction[0] = 1;
            withBlock.Direction[1] = 1;
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
