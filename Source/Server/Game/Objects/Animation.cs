using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Core.Enum;
using static Core.Packets;
using static Core.Type;
using static Core.Global.Command;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Server
{

    public struct AnimationFrameData
    {
        public int Sprite { get; set; }
        public int Frame { get; set; }
        public int Duration { get; set; } // Duration of this frame in milliseconds
    }

    public struct AnimationStruct
    {
        public string Name { get; set; }
        public string Sound { get; set; }
        // public int[] Sprite { get; set; } // Consider using a list of AnimationFrameData
        // public int[] Frames { get; set; } // Redundant with AnimationFrameData
        public List<AnimationFrameData> FrameData { get; set; }
        public int[] LoopCount { get; set; } // [0] = Loop Type (0: No Loop, 1: Loop Count, 2: Infinite), [1] = Loop Value
        public int[] LoopTime { get; set; }   // [0] = Delay before first loop, [1] = Delay between loops (milliseconds)
    }

    public class Animation
    {

        #region Database
        public static void SaveAnimation(int animationNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Animation[animationNum]).ToString();

            if (Database.RowExists(animationNum, "animation"))
            {
                Database.UpdateRow(animationNum, json, "animation", "data");
            }
            else
            {
                Database.InsertRow(animationNum, json, "animation");
            }
        }

        public static async Task LoadAnimationsAsync()
        {
            int i;
            var loopTo = Core.Constant.MAX_ANIMATIONS - 1;
            for (i = 0; i <= loopTo; i++)
                await Task.Run(() => LoadAnimation(i));
        }

        public static async Task LoadAnimation(int animationNum)
        {
            JObject data;

            data = await Database.SelectRowAsync(animationNum, "animation", "data");

            if (data is null)
            {
                ClearAnimation(animationNum);
                return;
            }

            try
            {
                var animationData = JsonConvert.DeserializeObject<AnimationStruct>(data.ToString());
                Core.Type.Animation[animationNum] = animationData;
            }
            catch (JsonSerializationException ex)
            {
                Core.Log.WriteError($"Error deserializing animation {animationNum}: {ex.Message}");
                ClearAnimation(animationNum);
            }
        }

        public static void ClearAnimation(int index)
        {
            Core.Type.Animation[index].Name = "";
            Core.Type.Animation[index].Sound = "";
            Core.Type.Animation[index].FrameData = new List<AnimationFrameData>();
            Core.Type.Animation[index].LoopCount = new int[2] { 0, 0 };
            Core.Type.Animation[index].LoopTime = new int[2] { 0, 0 };
        }

        public static void ClearAnimations()
        {
            for (int i = 0; i < Core.Constant.MAX_ANIMATIONS; i++)
                ClearAnimation(i);
        }

        public static byte[] AnimationData(int AnimationNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(AnimationNum);

            // Write Frame Data
            buffer.WriteInt32(Core.Type.Animation[AnimationNum].FrameData.Count);
            foreach (var frameData in Core.Type.Animation[AnimationNum].FrameData)
            {
                buffer.WriteInt32(frameData.Sprite);
                buffer.WriteInt32(frameData.Frame);
                buffer.WriteInt32(frameData.Duration);
            }

            // Write Loop Data
            buffer.WriteInt32(Core.Type.Animation[AnimationNum].LoopCount[0]);
            buffer.WriteInt32(Core.Type.Animation[AnimationNum].LoopCount[1]);
            buffer.WriteInt32(Core.Type.Animation[AnimationNum].LoopTime[0]);
            buffer.WriteInt32(Core.Type.Animation[AnimationNum].LoopTime[1]);

            buffer.WriteString(Core.Type.Animation[AnimationNum].Name);
            buffer.WriteString(Core.Type.Animation[AnimationNum].Sound);

            return buffer.ToArray();
        }

        #endregion

        #region Incoming Packets

        public static void Packet_EditAnimation(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte)AccessType.Developer)
                return;
            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte)EditorType.Animation);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int)ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte)EditorType.Animation;

            SendAnimations(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SAnimationEditor);
            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void Packet_SaveAnimation(int index, ref byte[] data)
        {
            int AnimNum;
            var buffer = new ByteStream(data);

            AnimNum = buffer.ReadInt32();

            if (AnimNum < 0 || AnimNum >= Core.Constant.MAX_ANIMATIONS)
            {
                Core.Log.WriteWarning($"Player {GetPlayerLogin(index)} tried to save animation with invalid index: {AnimNum}");
                return;
            }

            var animation = Core.Type.Animation[AnimNum];
            animation.FrameData.Clear();
            int frameCount = buffer.ReadInt32();
            for (int i = 0; i < frameCount; i++)
            {
                animation.FrameData.Add(new AnimationFrameData
                {
                    Sprite = buffer.ReadInt32(),
                    Frame = buffer.ReadInt32(),
                    Duration = buffer.ReadInt32()
                });
            }

            animation.LoopCount[0] = buffer.ReadInt32();
            animation.LoopCount[1] = buffer.ReadInt32();
            animation.LoopTime[0] = buffer.ReadInt32();
            animation.LoopTime[1] = buffer.ReadInt32();

            animation.Name = buffer.ReadString();
            animation.Sound = buffer.ReadString();

            buffer.Dispose();

            // Save it
            SaveAnimation(AnimNum);
            SendUpdateAnimationToAll(AnimNum);
            Core.Log.Add(GetPlayerLogin(index) + " saved Animation #" + AnimNum + ".", Constant.ADMIN_LOG);
        }

        public static void Packet_RequestAnimation(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int n;

            n = buffer.ReadInt32();

            if (n < 0 || n >= Core.Constant.MAX_ANIMATIONS)
                return;

            SendUpdateAnimationTo(index, n);
        }

        #endregion

        #region Outgoing Packets

        public static void SendAnimation(int mapNum, int Anim, int X, int Y, byte LockType = 0, int Lockindex = 0)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)ServerPackets.SAnimation);
            buffer.WriteInt32(Anim);
            buffer.WriteInt32(X);
            buffer.WriteInt32(Y);
            buffer.WriteInt32(LockType);
            buffer.WriteInt32(Lockindex);

            NetworkConfig.SendDataToMap(mapNum, buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendAnimations(int index)
        {
            for (int i = 0; i < Core.Constant.MAX_ANIMATIONS; i++)
            {
                if (!string.IsNullOrEmpty(Core.Type.Animation[i].Name))
                {
                    SendUpdateAnimationTo(index, i);
                }
            }
        }

        public static void SendUpdateAnimationTo(int index, int AnimationNum)
        {
            if (AnimationNum < 0 || AnimationNum >= Core.Constant.MAX_ANIMATIONS)
            {
                Core.Log.WriteWarning($"Attempted to send update for invalid animation index: {AnimationNum} to player {index}");
                return;
            }

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SUpdateAnimation);
            buffer.WriteBlock(AnimationData(AnimationNum));

            NetworkConfig.Socket.SendDataTo(index, buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendUpdateAnimationToAll(int AnimationNum)
        {
            if (AnimationNum < 0 || AnimationNum >= Core.Constant.MAX_ANIMATIONS)
            {
                Core.Log.WriteWarning($"Attempted to send update for invalid animation index: {AnimationNum} to all players");
                return;
            }

            var buffer = new ByteStream(4);

            buffer.WriteInt32((int)ServerPackets.SUpdateAnimation);
            buffer.WriteBlock(AnimationData(AnimationNum));

            NetworkConfig.SendDataToAll(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}
