﻿
namespace Core
{
    public static class Packets
    {

        // Packets sent by client to server
        public enum ClientPackets
        {
            CLogin,
            CRegister,
            CAddChar,
            CUseChar,
            CDelChar,
            CSayMsg,
            CBroadcastMsg,
            CPlayerMsg,
            CAdminMsg,
            CPlayerMove,
            CPlayerDir,
            CUseItem,
            CAttack,
            CPlayerInfoRequest,
            CWarpMeTo,
            CWarpToMe,
            CWarpTo,
            CSetSprite,
            CGetStats,
            CRequestNewMap,
            CNeedMap,
            CMapGetItem,
            CMapDropItem,
            CKickPlayer,
            CBanList,
            CBanDestroy,
            CBanPlayer,
            CRequestEditMap,

            CSetAccess,
            CWhosOnline,
            CSetMotd,
            CSearch,
            CSkills,
            CCast,
            CQuit,
            CSwapInvSlots,
            CSwapSkillSlots,

            CCheckPing,
            CUnequip,
            CRequestPlayerData,
            CRequestItem,
            CRequestNPC,
            CRequestResource,
            CSpawnItem,
            CTrainStat,

            CRequestAnimation,
            CRequestSkill,
            CRequestShop,
            CRequestLevelUp,
            CForgetSkill,
            CCloseShop,
            CBuyItem,
            CSellItem,
            CChangeBankSlots,
            CDepositItem,
            CWithdrawItem,
            CCloseBank,
            CAdminWarp,
            CTradeInvite,
            CHandleTradeInvite,
            CAcceptTrade,
            CDeclineTrade,
            CTradeItem,
            CUntradeItem,
            CAdmin,
            CSetHotbarSlot,
            CDeleteHotbarSlot,
            CUseHotbarSlot,
            CEventChatReply,
            CEvent,
            CSwitchesAndVariables,
            CRequestSwitchesAndVariables,

            CRequestProjectile,
            CClearProjectile,
            CEmote,
            CRequestParty,
            CAcceptParty,
            CDeclineParty,
            CLeaveParty,
            CPartyChatMsg,
            CRequestPets,
            CSummonPet,
            CPetMove,
            CSetBehaviour,
            CReleasePet,
            CPetSkill,
            CPetUseStatPoint,
            CRequestPet,

            // *************************
            // ***   EDITOR PACKETS  ***
            // *************************
            CMapRespawn,
            CMapReport,
            CSaveMap,
            CSkillLearn,

            // ####################
            // ### Dev+ Packets ###
            // ####################
            CRequestEditAnimation,
            CSaveAnimation,
            CRequestEditJob,
            CSaveJob,
            CRequestEditItem,
            CSaveItem,
            CRequestEditNPC,
            CSaveNPC,
            CRequestEditPet,
            CSavePet,
            CRequestEditProjectile,
            CSaveProjectile,
            CRequestEditResource,
            CSaveResource,
            CRequestEditShop,
            CSaveShop,
            CRequestEditSkill,
            CSaveSkill,

            CRequestMoral,
            CSaveMoral,
            CRequestEditMoral,

            CCloseEditor,

            // Make sure COUNT is below everything else
            Count
        }

        // Packets sent by server to client
        public enum ServerPackets
        {
            SAlertMsg,
            SKeyPair,
            SLoginOK,
            SPlayerChars,
            SJobData,
            SInGame,
            SPlayerInv,
            SPlayerInvUpdate,
            SPlayerWornEq,
            SPlayerHP,
            SPlayerSP,
            SPlayerStats,
            SPlayerData,
            SPlayerMove,
            SNPCMove,
            SPlayerDir,
            SNPCDir,
            SPlayerXY,
            SAttack,
            SNPCAttack,
            SCheckForMap,
            SMapData,
            SMapItemData,
            SMapNPCData,
            SMapNPCUpdate,
            SMapDone,
            SGlobalMsg,
            SAdminMsg,
            SPlayerMsg,
            SMapMsg,
            SSpawnItem,
            SItemEditor,
            SUpdateItem,
            SSpawnNPC,
            SNPCDead,
            SNPCEditor,
            SUpdateNPC,
            SEditMap,
            SShopEditor,
            SUpdateShop,
            SSkillEditor,
            SUpdateSkill,
            SSkills,
            SLeftMap,
            SMapResource,
            SResourceEditor,
            SUpdateResource,
            SSendPing,
            SActionMsg,
            SPlayerEXP,
            SBlood,
            SAnimationEditor,
            SUpdateAnimation,
            SAnimation,
            SMapNPCVitals,
            SCooldown,
            SClearSkillBuffer,
            SSayMsg,
            SOpenShop,
            SResetShopAction,
            SStunned,
            SMapWornEq,
            SBank,
            SLeftGame,
            STradeInvite,
            STrade,
            SCloseTrade,
            STradeUpdate,
            STradeStatus,
            SMapReport,
            STarget,
            SAdmin,
            SMapNames,
            SCritical,
            SrClick,
            SHotbar,
            SSpawnEvent,
            SEventMove,
            SEventDir,
            SEventChat,
            SEventStart,
            SEventEnd,
            SPlayBGM,
            SPlaySound,
            SFadeoutBGM,
            SStopSound,
            SSwitchesAndVariables,
            SMapEventData,
            SChatBubble,
            SSpecialEffect,
            SPic,
            SHoldPlayer,
            SProjectileEditor,
            SUpdateProjectile,
            SMapProjectile,
            SJobEditor,
            SUpdateJob,
            SUpdateMoral,
            SEmote,
            SPartyInvite,
            SPartyUpdate,
            SPartyVitals,
            SPetEditor,
            SUpdatePet,
            SUpdatePlayerPet,
            SPetMove,
            SPetDir,
            SPetVital,
            SClearPetSkillBuffer,
            SPetAttack,
            SPetXY,
            SPetExp,
            SMoralEditor,

            STime,
            SClock,

            // Make sure COUNT is below everything else
            COUNT
        }

    }
}