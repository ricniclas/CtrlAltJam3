using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button tutorialButton;
        [SerializeField] private Button exitButton;

        [SerializeField] private OptionsMenu optionsGameObject;
        [SerializeField] private GameObject tutorialMenu;
        [SerializeField] private GameObject mainMenu;

        private void Start()
        {
            optionsGameObject.gameObject.SetActive(false);
            startGameButton.Select();
        }

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(StartGame);
            optionsButton.onClick.AddListener(OpenOptions);
            tutorialButton.onClick.AddListener(TutorialScreen);
            exitButton.onClick.AddListener(CloseGame);
            startGameButton.Select();
        }

        private void OnDisable()
        {
            startGameButton.onClick.RemoveListener(StartGame);
            optionsButton.onClick.RemoveListener(OpenOptions);
            exitButton.onClick.RemoveListener(CloseGame);
        }

        private void OpenOptions()
        {
            optionsGameObject.gameObject.SetActive(true);
            optionsGameObject.GetCloseButtonEvent().AddListener(() => optionsButton.Select());
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Monitors");
        }

        private void TutorialScreen()
        {
            tutorialMenu.SetActive(true);
            mainMenu.SetActive(false);
        }

        private void CloseGame()
        {
            Application.Quit();
        }
    }
}
