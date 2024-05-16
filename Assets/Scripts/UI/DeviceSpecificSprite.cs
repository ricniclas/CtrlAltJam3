using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    [RequireComponent(typeof(SpriteButtonAnimation))]
    public class DeviceSpecificSprite : MonoBehaviour
    {
        public ButtonType buttonType;
        private SpriteButtonAnimation spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteButtonAnimation>();
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
                    spriteRenderer.SetSprite(PlatformManager.instance.button1);
                    break;
                case ButtonType.Game2:
                    spriteRenderer.SetSprite(PlatformManager.instance.button2);
                    break;
                case ButtonType.Game3:
                    spriteRenderer.SetSprite(PlatformManager.instance.button3);
                    break;
                case ButtonType.Game4:
                    spriteRenderer.SetSprite(PlatformManager.instance.button4);
                    break;
                case ButtonType.Confirm:
                    spriteRenderer.SetSprite(PlatformManager.instance.buttonConfirm);
                    break;
                case ButtonType.Cancel:
                    spriteRenderer.SetSprite(PlatformManager.instance.buttonCancel);
                    break;
            }
        }

    }
}
