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
