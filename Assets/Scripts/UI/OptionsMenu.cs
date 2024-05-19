using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Localization;

namespace CtrlAltJam3
{
    public class OptionsMenu : MonoBehaviour, IInputReceiver
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Button closeButton;

        [SerializeField] private TMP_Dropdown languageDropdown;
        List<string> optionData = new List<string>();
        [SerializeField] private List<Locale> availableLanguages;

        private UnityEvent closeButtonEvent;
        private InputPackage inputPackage => new InputPackage(this);


        private void Awake()
        {
            closeButtonEvent = new UnityEvent();
        }

        private void Start()
        {
            SetOptionsValues();

        }

        private void OnEnable()
        {
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            musSlider.onValueChanged.AddListener(SetMusVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            closeButton.onClick.AddListener(CloseOptions);
            masterSlider.Select();
            languageDropdown.onValueChanged.AddListener(UpdateLocation);
            InputManager.instance.AddInputPackageEvents(inputPackage, true);
        }

        private void OnDisable()
        {
            masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
            musSlider.onValueChanged.RemoveListener(SetMusVolume);
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
            closeButton.onClick.RemoveListener(CloseOptions);
            languageDropdown.onValueChanged.RemoveListener(UpdateLocation);


        }

        #region Public Methods
        public UnityEvent GetCloseButtonEvent()
        {
            return closeButtonEvent;
        }
        #endregion

        #region Private Methods
        private void SetMasterVolume(float volume)
        {
            AudioManager.instance.SetVolume(VolumeGroup.MASTER, volume);
        }
        private void SetMusVolume(float volume)
        {
            AudioManager.instance.SetVolume(VolumeGroup.MUS, volume);
        }
        private void SetSFXVolume(float volume)
        {
            AudioManager.instance.SetVolume(VolumeGroup.SFX, volume);
        }



        private void SetOptionsValues()
        {
            masterSlider.value = AudioManager.instance.GetVolume(VolumeGroup.MASTER);
            musSlider.value = AudioManager.instance.GetVolume(VolumeGroup.MUS);
            sfxSlider.value = AudioManager.instance.GetVolume(VolumeGroup.SFX);
            foreach(Locale language in SettingsManager.instance.GetLocales())
            {
                optionData.Add(language.LocaleName.ToString());
            }
            languageDropdown.AddOptions(optionData);
        }

        private void CloseOptions()
        {
            gameObject.SetActive(false);
            closeButtonEvent.Invoke();
        }

        private void UpdateLocation(int index)
        {
            SettingsManager.instance.ChangeCurrentLocalization(index);
        }

        #endregion

        #region InputReceiver Interface
        void IInputReceiver.Directions(Vector2 direction)
        {
            
        }

        void IInputReceiver.Game1()
        {
            
        }

        void IInputReceiver.Game2()
        {
            
        }

        void IInputReceiver.Game3()
        {
            
        }

        void IInputReceiver.Game4()
        {
            
        }

        void IInputReceiver.Confirm()
        {
            
        }

        void IInputReceiver.Cancel()
        {
            CloseOptions();
        }

        void IInputReceiver.Pause()
        {
            CloseOptions();
        }

        InputPackage IInputReceiver.GetInputPackage()
        {
            return inputPackage;
        }
        #endregion
    }
}
