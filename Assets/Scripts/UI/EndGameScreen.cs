using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    public class EndGameScreen : MonoBehaviour
    {

        [SerializeField] private Button retryButton;
        [SerializeField] private Button backToMenuBUtton;
        [SerializeField] private Color winColor, loseColor;
        [SerializeField] private Image background;

        [SerializeField] private TMP_Text loseTimeText;
        [SerializeField] private TMP_Text winTimeText;
        [SerializeField] private TMP_Text completionPercentageText;
        [SerializeField] private TMP_Text remainingMatesText;

        [SerializeField] private GameObject loseGameObject;
        [SerializeField] private GameObject winGameObject;
        private bool isVictory;


        private void OnEnable()
        {
            retryButton.onClick.AddListener(ReloadScene);
            backToMenuBUtton.onClick.AddListener(BackToMenu);
        }

        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(ReloadScene);
            backToMenuBUtton.onClick.RemoveListener(BackToMenu);
        }

        public void Initialize(bool isVictory, float completion, float matchDuration, int remainingMates)
        {
            this.isVictory = isVictory;
            loseTimeText.SetText(matchDuration.ToString());
            completionPercentageText.SetText(completion.ToString());
            winTimeText.SetText(matchDuration.ToString());
            remainingMatesText.SetText(remainingMates.ToString());
            if(isVictory)
            {
                loseGameObject.SetActive(false);
                winGameObject.SetActive(true);
                background.color = winColor;
            }
            else
            {
                loseGameObject.SetActive(true);
                winGameObject.SetActive(false);
                background.color = loseColor;
            }
        }
        private void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene("Monitors");
        }
    }

}
