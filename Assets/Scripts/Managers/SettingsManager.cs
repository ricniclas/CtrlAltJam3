using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace CtrlAltJam3
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager instance;
        [SerializeField] private int targetFPS = 60;
        [SerializeField] private List<Locale> availableLanguages;

        private int currentLanguageIndex;



        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this);
                return;
            }
            Application.targetFrameRate = targetFPS;
        }

        private void GetPlayerPrefs()
        {
            if (PlayerPrefs.GetString(Constants.SAVED_LANGUAGE, " ") == " ")
            {
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.English:
                        ChangeCurrentLocalization(0);
                        break;
                    case SystemLanguage.Portuguese:
                        ChangeCurrentLocalization(1);
                        break;
                    case SystemLanguage.Spanish:
                        ChangeCurrentLocalization(2);
                        break;
                    default:
                        ChangeCurrentLocalization(0);
                        break;
                }
            }
            else
            {
                string language = PlayerPrefs.GetString(Constants.SAVED_LANGUAGE);
                switch (language)
                {
                    case "Portuguese (Brazil) (pt-BR)":
                        ChangeCurrentLocalization(0);
                        break;
                    case "English (en)":
                        ChangeCurrentLocalization(1);
                        break;
                    default:
                        ChangeCurrentLocalization(3);
                        break;
                }
            }
        }

        public void ChangeCurrentLocalization(int index)
        {
            currentLanguageIndex = index;
            LocalizationSettings.SelectedLocale = (availableLanguages[index]);
            LocalizationSettings.SelectedLocale.Identifier = availableLanguages[index].Identifier;

            PlayerPrefs.SetString(Constants.SAVED_LANGUAGE, LocalizationSettings.SelectedLocale.LocaleName);
        }

        public List<Locale> GetLocales()
        {
            return availableLanguages;
        }
    }
}
