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

namespace Server
{

    internal static class Animation
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

        public static void LoadAnimations()
        {
            int i;

            var loopTo = Core.Constant.MAX_ANIMATIONS - 1;
            for (i = 0; i < loopTo; i++)
                LoadAnimation(i);

        }

        public static void LoadAnimation(int animationNum)
        {
            JObject data;

            data = Database.SelectRow(animationNum, "animation", "data");

            if (data is null)
            {
                ClearAnimation(animationNum);
                return;
            }

            var animationData = JObject.FromObject(data).ToObject<AnimationStruct>();
            Core.Type.Animation[animationNum] = animationData;
        }

        public static void ClearAnimation(int index)
        {
            Core.Type.Animation[index].Name = "";
            Core.Type.Animation[index].Sound = "";
            Core.Type.Animation[index].Sprite = new int[2];
            Core.Type.Animation[index].Frames = new int[2];
            Core.Type.Animation[index].LoopCount = new int[2];
            Core.Type.Animation[index].LoopTime = new int[2];
            Core.Type.Animation[index].LoopCount[0] = 0;
            Core.Type.Animation[index].LoopCount[1] = 0;
            Core.Type.Animation[index].LoopTime[0] = 0;
            Core.Type.Animation[index].LoopTime[1] = 0;
        }

        public static void ClearAnimations()
        {
            for (int i = 0, loopTo = Core.Constant.MAX_ANIMATIONS; i < loopTo; i++)
                ClearAnimation(Conversions.ToInteger(i));
        }

        public static byte[] AnimationData(int AnimationNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(AnimationNum);
            for (int i = 0, loopTo = Core.Type.Animation[AnimationNum].Frames.Length; i < loopTo; i++)
                buffer.WriteInt32(Core.Type.Animation[AnimationNum].Frames[i]);

            for (int i = 0, loopTo1 = Core.Type.Animation[AnimationNum].LoopCount.Length; i < loopTo1; i++)
                buffer.WriteInt32(Core.Type.Animation[AnimationNum].LoopCount[i]);

            for (int i = 0, loopTo2 = Core.Type.Animation[AnimationNum].LoopTime.Length; i < loopTo2; i++)
                buffer.WriteInt32(Core.Type.Animation[AnimationNum].LoopTime[i]);

            buffer.WriteString(Core.Type.Animation[AnimationNum].Name);
            buffer.WriteString(Core.Type.Animation[AnimationNum].Sound);

            for (int i = 0, loopTo3 = Information.UBound(Core.Type.Animation[AnimationNum].Sprite); i < loopTo3; i++)
                buffer.WriteInt32(Core.Type.Animation[AnimationNum].Sprite[i]);

            return buffer.ToArray();
        }

        #endregion

        #region Incoming Packets

        public static void Packet_EditAnimation(int index, ref byte[] data)
        {
            // Prevent hacking
            if (GetPlayerAccess(index) < (byte) AccessType.Developer)
                return;
            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = IsEditorLocked(index, (byte) EditorType.Animation);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int) ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte) EditorType.Animation;

            SendAnimations(index);

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SAnimationEditor);
            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
            buffer.Dispose();
        }

        public static void Packet_SaveAnimation(int index, ref byte[] data)
        {
            int AnimNum;
            var buffer = new ByteStream(data);

            AnimNum = buffer.ReadInt32();

            // Update the Animation
            for (int i = 0, loopTo = Information.UBound(Core.Type.Animation[AnimNum].Frames); i < loopTo; i++)
                Core.Type.Animation[AnimNum].Frames[i] = buffer.ReadInt32();

            for (int i = 0, loopTo1 = Information.UBound(Core.Type.Animation[AnimNum].LoopCount); i < loopTo1; i++)
                Core.Type.Animation[AnimNum].LoopCount[i] = buffer.ReadInt32();

            for (int i = 0, loopTo2 = Information.UBound(Core.Type.Animation[AnimNum].LoopTime); i < loopTo2; i++)
                Core.Type.Animation[AnimNum].LoopTime[i] = buffer.ReadInt32();

            Core.Type.Animation[AnimNum].Name = buffer.ReadString();
            Core.Type.Animation[AnimNum].Sound = buffer.ReadString();

            for (int i = 0, loopTo3 = Information.UBound(Core.Type.Animation[AnimNum].Sprite); i < loopTo3; i++)
                Core.Type.Animation[AnimNum].Sprite[i] = buffer.ReadInt32();

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

            if (n < 0 | n > Core.Constant.MAX_ANIMATIONS)
                return;

            SendUpdateAnimationTo(index, n);
        }

        #endregion

        #region Outgoing Packets

        public static void SendAnimation(int mapNum, int Anim, int X, int Y, byte LockType = 0, int Lockindex = 0)
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int) ServerPackets.SAnimation);
            buffer.WriteInt32(Anim);
            buffer.WriteInt32(X);
            buffer.WriteInt32(Y);
            buffer.WriteInt32(LockType);
            buffer.WriteInt32(Lockindex);

            NetworkConfig.SendDataToMap(mapNum, ref buffer.Data, buffer.Head);

            buffer.Dispose();
        }

        public static void SendAnimations(int index)
        {
            int i;

            var loopTo = Core.Constant.MAX_ANIMATIONS - 1;
            for (i = 0; i < loopTo; i++)
            {

                if (Strings.Len(Core.Type.Animation[i].Name) > 0)
                {
                    SendUpdateAnimationTo(index, i);
                }

            }

        }

        public static void SendUpdateAnimationTo(int index, int AnimationNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateAnimation);

            buffer.WriteBlock(AnimationData(AnimationNum));

            NetworkConfig.Socket.SendDataTo(ref index, ref buffer.Data, ref buffer.Head);
            buffer.Dispose();
        }

        public static void SendUpdateAnimationToAll(int AnimationNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32((int) ServerPackets.SUpdateAnimation);

            buffer.WriteBlock(AnimationData(AnimationNum));

            NetworkConfig.SendDataToAll(ref buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

    }
}