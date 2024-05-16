using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    [RequireComponent(typeof(Image))]
    public class DeviceSpecificImage : MonoBehaviour
    {
        public ButtonType buttonType;
        private Image spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<Image>();
        }

        private void OnEnable()
        {
            PlatformManager.instance.loadedSpritesEvent.AddListener(SetSprite);
            SetSprite();
        }

        private void OnDisable()
        {
            PlatformManager.instance.loadedSpritesEvent.RemoveListener(SetSprite);
        }

        private void SetSprite()
        {
            switch (buttonType)
            {
                case ButtonType.Game1:
                    spriteRenderer.sprite = PlatformManager.instance.button1;
                    break; 
                case ButtonType.Game2:
                    spriteRenderer.sprite = PlatformManager.instance.button2;
                    break;
                case ButtonType.Game3:
                    spriteRenderer.sprite = PlatformManager.instance.button3;
                    break;
                case ButtonType.Game4:
                    spriteRenderer.sprite = PlatformManager.instance.button4;
                    break;
                case ButtonType.Confirm:
                    spriteRenderer.sprite = PlatformManager.instance.buttonConfirm;
                    break;
                case ButtonType.Cancel:
                    spriteRenderer.sprite = PlatformManager.instance.buttonCancel;
                    break;
            }
        }

    }
}