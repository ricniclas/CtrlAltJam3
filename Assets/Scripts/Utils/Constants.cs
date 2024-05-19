using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class Constants : MonoBehaviour
    {
        #region Strings

        #region FMOD

        public static string AUDIO_MIXER_GROUP_MASTER = "MASTERVolume";
        public static string AUDIO_MIXER_GROUP_MUS = "MUSVolume";
        public static string AUDIO_MIXER_GROUP_SFX = "SFXVolume";


        public static string FMOD_BUS_MASTER = "bus:/";
        public static string FMOD_BUS_MUS = "bus:/MASTER/MUS";
        public static string FMOD_BUS_SFX = "bus:/MASTER/SFX";

        public static string FMOD_SNAPSHOT_PAUSE = "snapshot:/pause";

        #endregion

        #region MUS Events
        public static string FMOD_EVENT_INSTANCE_MUS_MAIN_MENU = "event:/MUS/MAIN_MENU";
        #endregion

        #region SFX Events
        public static string FMOD_EVENT_SFX_ALERT_DECREASE = "event:/SFX/GAMEPLAY/ALERT_DECREASE";
        public static string FMOD_EVENT_SFX_ALERT_RAISE = "event:/SFX/GAMEPLAY/ALERT_RAISE";
        public static string FMOD_EVENT_SFX_BOMB_EXPLODE = "event:/SFX/GAMEPLAY/BOMB_EXPLODE";
        public static string FMOD_EVENT_SFX_DRONE_SHOT = "event:/SFX/GAMEPLAY/DRONE_SHOT";
        public static string FMOD_EVENT_SFX_HURT = "event:/SFX/GAMEPLAY/HURT";
        public static string FMOD_EVENT_SFX_LINE_CLEAR = "event:/SFX/GAMEPLAY/LINE_CLEAR";
        public static string FMOD_EVENT_SFX_LOSE = "event:/SFX/GAMEPLAY/LOSE";
        public static string FMOD_EVENT_SFX_MOVE_BLOCK = "event:/SFX/GAMEPLAY/MOVE_BLOCK";
        public static string FMOD_EVENT_SFX_SET_BLOCK = "event:/SFX/GAMEPLAY/SET_BLOCK";
        public static string FMOD_EVENT_SFX_SPIN_WHEEL = "event:/SFX/GAMEPLAY/SPIN_WHEEL";
        public static string FMOD_EVENT_SFX_SWITCH_GAME = "event:/SFX/GAMEPLAY/SWITCH_GAME";
        public static string FMOD_EVENT_SFX_WIN = "event:/SFX/GAMEPLAY/WIN";

        public static string FMOD_EVENT_SFX_CLICK = "event:/SFX/GAMEPLAY/CLICK";
        public static string FMOD_EVENT_SFX_CANCEL = "event:/SFX/GAMEPLAY/CANCEL";
        #endregion

        #region Player Prefs

        public static string SAVED_LANGUAGE = "Language_Selected";

        #endregion

        #endregion

        #region Values
        public static float AUDIO_MIXER_DEFAULT_VOLUME = .5f;
        #endregion
    }
}
