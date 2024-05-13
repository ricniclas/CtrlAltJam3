using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musSlider;
        [SerializeField] private Slider sfxSlider;

        [SerializeField] private Button closeButton;
        public UnityEvent closeButtonEvent;
        

        private void Start()
        {
            SetSlidersValue();

        }

        private void OnEnable()
        {
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            musSlider.onValueChanged.AddListener(SetMusVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            closeButton.onClick.AddListener(CloseOptions);
        }

        private void OnDisable()
        {
            masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
            musSlider.onValueChanged.RemoveListener(SetMusVolume);
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
            closeButton.onClick.RemoveListener(CloseOptions);

        }

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



        private void SetSlidersValue()
        {
            masterSlider.value = AudioManager.instance.GetVolume(VolumeGroup.MASTER);
            musSlider.value = AudioManager.instance.GetVolume(VolumeGroup.MUS);
            sfxSlider.value = AudioManager.instance.GetVolume(VolumeGroup.SFX);
        }

        private void CloseOptions()
        {
            gameObject.SetActive(false);
            closeButtonEvent.Invoke();
        }
    }
}
