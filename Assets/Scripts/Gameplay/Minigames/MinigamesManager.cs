using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    public class MinigamesManager : MonoBehaviour, IInputReceiver
    {
        [SerializeField] private GameObject[] minigamesGameObject;
        [SerializeField] private OptionsMenu optionsMenu;
        [SerializeField] private LightsController lightsController;
        [SerializeField] private LifeCiclesManagers lifeManager;
        [SerializeField] private int initialMinigame = 3;
        [SerializeField] private Slider alertLevelSlider;
        [SerializeField] private Image alertLevelSliderFill;

        [SerializeField] private Color[] sliderColors = new Color[4];

        [SerializeField] private EndGameScreen endGameScreen;

        public int alertLevel = 1;
        private List<IMinigame> minigames;
        private InputPackage inputPackage => new InputPackage(this);
        private int currentGame = 0;

        [HideInInspector] public UnityEvent<float, LifeBarAction> healthUpdateEvent = new UnityEvent<float, LifeBarAction>();

        #region Monobehaviour Callbacks

        private void Start()
        {
            InputManager.instance.AddGameSelectionyEvents(inputPackage, true);
            minigames = new List<IMinigame>();
            for(int i = 0; i < minigamesGameObject.Length; i++)
            {
                minigames.Add(minigamesGameObject[i].GetComponent<IMinigame>());
                minigames[i].SetMinigameManager(this);
            }
            InputManager.instance.AddGameplayEvents(minigames[initialMinigame].GetInputPackage(), true);
            optionsMenu.gameObject.SetActive(false);
            minigames[initialMinigame].Selected();
            lightsController.Initialize(initialMinigame);
            lifeManager.Initialize(this);
            currentGame = initialMinigame;
            SetAlertSlider(alertLevel);
        }

        private void OnEnable()
        {
            healthUpdateEvent.AddListener(UpdateHealth);
        }

        private void OnDisable()
        {
            healthUpdateEvent.RemoveListener(UpdateHealth);
        }

        #endregion

        #region Public Methods
        public void UpdateAlertLevel(int change)
        {
            
            alertLevel = MathUtils.Limit(change + alertLevel,1,4);
            for(int i = 0;i < minigames.Count; i++)
            {
                minigames[i].SetAlertLevel(alertLevel);
            }
            SetAlertSlider(alertLevel);
        }

        public void EndGame(bool win)
        {
            endGameScreen.gameObject.SetActive(true);
            endGameScreen.Initialize(win, 50f, 50f, lifeManager.GetMembersAlive());
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

        private void UpdateHealth(float value, LifeBarAction barAction)
        {
            lifeManager.UpdateLife(value, barAction);
        }

        private void SetAlertSlider(int value)
        {
            alertLevelSlider.DOValue(value,0.3f);
            alertLevelSlider.gameObject.transform.DOPunchRotation(new Vector3(0, 0, 10), 0.5f, elasticity: 1);
            alertLevelSliderFill.DOColor(sliderColors[value-1],0.3f);
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
