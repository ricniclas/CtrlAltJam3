using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class MinigamesManager : MonoBehaviour, IInputReceiver
    {
        [SerializeField] private GameObject[] minigamesGameObject;
        [SerializeField] private OptionsMenu optionsMenu;
        [SerializeField] private LightsController lightsController;
        [SerializeField] private int initialMinigame = 3;
        private List<IMinigame> minigames;
        private InputPackage inputPackage => new InputPackage(this);
        private int currentGame = 0;


        #region Monobehaviour Callbacks

        private void Start()
        {
            InputManager.instance.AddGameSelectionyEvents(inputPackage, true);
            minigames = new List<IMinigame>();
            for(int i = 0; i < minigamesGameObject.Length; i++)
            {
                minigames.Add(minigamesGameObject[i].GetComponent<IMinigame>());
            }
            InputManager.instance.AddGameplayEvents(minigames[initialMinigame].GetInputPackage(), true);
            optionsMenu.gameObject.SetActive(false);
            minigames[initialMinigame].Selected();
            lightsController.Initialize(initialMinigame);
            currentGame = initialMinigame;
        }

        #endregion
        #region Private Methods

        private void SwitchGame(int currentGame)
        {
            if(this.currentGame != currentGame)
            {
                for (int i = 0; i < minigames.Count; i++)
                {
                    if (i != currentGame)
                    {
                        minigames[i].ResetInputs();
                        minigames[i].Unselected();
                    }
                }
                try
                {
                    InputManager.instance.AddGameplayEvents(minigames[currentGame].GetInputPackage(), true);
                    this.currentGame = currentGame;
                    minigames[currentGame].Selected();
                    lightsController.ActivateLight(currentGame);
                }
                catch (Exception e)
                {
                    Debug.LogError($"No game assigned to index {currentGame}");
                }
            }
        }

        private void ResumeGame()
        {
            InputManager.instance.AddGameSelectionyEvents(inputPackage, true);
            SwitchGame(currentGame);
            Time.timeScale = 1.0f;
        }
        #endregion


        #region InputReceiver
        void IInputReceiver.Cancel()
        {
        }

        void IInputReceiver.Confirm()
        {
        }

        void IInputReceiver.Directions(Vector2 direction)
        {
        }

        void IInputReceiver.Game1()
        {
            SwitchGame(0);
        }

        void IInputReceiver.Game2()
        {
            SwitchGame(1);
        }

        void IInputReceiver.Game3()
        {
            SwitchGame(2);
        }

        void IInputReceiver.Game4()
        {
            SwitchGame(3);
        }

        void IInputReceiver.Pause()
        {
            optionsMenu.gameObject.SetActive(true);
            for (int i = 0; i < minigames.Count; i++)
            {
                minigames[i].ResetInputs();
            }
            Time.timeScale = 0;
            optionsMenu.GetCloseButtonEvent().AddListener(() => ResumeGame());
        }

        InputPackage IInputReceiver.GetInputPackage()
        {
            return inputPackage;
        }
        #endregion
    }
}
