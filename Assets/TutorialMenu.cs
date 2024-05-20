using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    public class TutorialMenu : MonoBehaviour, IInputReceiver
    {
        [SerializeField] private Button controlsButton, rulesButton, alertButton, mineButton, wheelsButton, backButton;
        [SerializeField] private GameObject controlsTutorial, rulesTutorial, alertTutorial, mineTutorial, wheelsTutorial;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject tutorialMenu;

        private GameObject lastObject;
        private InputPackage inputPackage => new InputPackage(this);
        private void Start()
        {
            lastObject = controlsTutorial;
        }

        private void Clean()
        {
            controlsTutorial.SetActive(false);
            rulesTutorial.SetActive(false);
            alertTutorial.SetActive(false);
            mineTutorial.SetActive(false);
            wheelsTutorial.SetActive(false);
        }

        private void OnEnable()
        {
            controlsButton.onClick.AddListener(ControlsScreen);
            rulesButton.onClick.AddListener(RulesScreen);
            alertButton.onClick.AddListener(AlertScreen);
            mineButton.onClick.AddListener(MineScreen);
            wheelsButton.onClick.AddListener(WheelsScreen);
            backButton.onClick.AddListener(BackAction);
            Clean();
            controlsButton.Select();
            lastObject = controlsTutorial;
        }

        private void OnDisable()
        {
            controlsButton.onClick.RemoveListener(ControlsScreen);
            rulesButton.onClick.RemoveListener(RulesScreen);
            alertButton.onClick.RemoveListener(AlertScreen);
            mineButton.onClick.RemoveListener(MineScreen);
            wheelsButton.onClick.RemoveListener(WheelsScreen);
            backButton.onClick.RemoveListener(BackAction);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Monitors");
        }

        private void BackAction()
        {
            mainMenu.SetActive(true);
            tutorialMenu.SetActive(false);
        }

        private void ControlsScreen() {
            changeScreen(controlsTutorial);
        }

        private void RulesScreen()
        {
            changeScreen(rulesTutorial);
        }
        private void AlertScreen()
        {
            changeScreen(alertTutorial);
        }
        private void MineScreen()
        {
            changeScreen(mineTutorial);
        }
        private void WheelsScreen()
        {
            changeScreen(wheelsTutorial);
        }

        private void changeScreen(GameObject screen) {
            lastObject.SetActive(false);
            screen.SetActive(true);
            lastObject = screen;
        }

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
            BackAction();
        }

        void IInputReceiver.Pause()
        {
            BackAction();
        }

        InputPackage IInputReceiver.GetInputPackage()
        {
            return inputPackage;
        }
    }
}
