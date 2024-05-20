using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    public class TutorialMenu : MonoBehaviour
    {
        [SerializeField] private Button controlsButton, rulesButton, alertButton, mineButton, wheelsButton, backButton;
        [SerializeField] private GameObject controlsTutorial, rulesTutorial, alertTutorial, mineTutorial, wheelsTutorial;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject tutorialMenu;  

        private GameObject lastObject;
        private void Start()
        {
            controlsButton.Select();
            lastObject = controlsTutorial;
        }

        private void OnEnable()
        {
            controlsButton.onClick.AddListener(ControlsScreen);
            rulesButton.onClick.AddListener(RulesScreen);
            alertButton.onClick.AddListener(AlertScreen);
            mineButton.onClick.AddListener(MineScreen);
            wheelsButton.onClick.AddListener(WheelsScreen);
            backButton.onClick.AddListener(BackAction);
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

    }
}
