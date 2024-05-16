using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CtrlAltJam3
{
    public class PlatformManager : MonoBehaviour
    {
        public static PlatformManager instance;
        public UnityEvent loadedSpritesEvent;
        public Sprite button1, button2, button3, button4;
        public Sprite buttonConfirm, buttonCancel;

        private AsyncOperationHandle<IList<Sprite>> spriteLoadOpHandle;
        private List<string> pcLabel = new List<string> {"Platform","PC"};
        private List<string> psLabel = new List<string> { "Platform", "PS" };

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                loadedSpritesEvent = new UnityEvent();
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        private void Start()
        {

        }

        #region Public Methods

        public void SwitchSprites(string device)
        {
            switch (device)
            {
                case "Keyboard":
                    spriteLoadOpHandle = Addressables.LoadAssetsAsync<Sprite>(pcLabel, null, Addressables.MergeMode.Intersection);
                    break;
                case "Gamepad":
                    spriteLoadOpHandle = Addressables.LoadAssetsAsync<Sprite>(psLabel, null, Addressables.MergeMode.Intersection);
                    break;
                default:
                    spriteLoadOpHandle = Addressables.LoadAssetsAsync<Sprite>(pcLabel, null, Addressables.MergeMode.Intersection);
                    break;
            }
            spriteLoadOpHandle.Completed += OnSpriteLoadComplete;

        }

        #endregion

        #region Private Methods

        private void OnSpriteLoadComplete(AsyncOperationHandle<IList<Sprite>> asyncOperationHandle)
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                IList<Sprite> results = asyncOperationHandle.Result;
                for (int i = 0; i < results.Count; i++)
                {
                    switch (results[i].name.Split("_")[1])
                    {
                        case "1":
                            button1 = results[i];
                            break;
                        case "2":
                            button2 = results[i];
                            break;
                        case "3":
                            button3 = results[i];
                            break;
                        case "4":
                            button4 = results[i];
                            break;
                        case "Confirm":
                            buttonConfirm = results[i];
                            break;
                        case "Cancel":
                            buttonCancel = results[i];
                            break;
                    }
                }

            }
            loadedSpritesEvent.Invoke();
        }
        #endregion
    }

    public enum ButtonType
    {
        Game1,
        Game2,
        Game3,
        Game4,
        Confirm,
        Cancel
    }
}
