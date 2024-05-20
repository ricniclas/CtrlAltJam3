using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        private bool activateInputs;


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
            activateInputs = false;
            retryButton.Select();
            Time.timeScale = 0;
            loseTimeText.SetText(matchDuration.ToString());
            completionPercentageText.SetText(completion.ToString());
            winTimeText.SetText(matchDuration.ToString());
            remainingMatesText.SetText(remainingMates.ToString());

            background.enabled = true;
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.5f).SetUpdate(true).OnComplete(() => activateInputs = true);
            if (isVictory)
            {
                background.DOColor(winColor, 0.3f).SetUpdate(true);
                loseGameObject.SetActive(false);
                winGameObject.SetActive(true);
            }
            else
            {
                background.DOColor(loseColor, 0.3f).SetUpdate(true);
                loseGameObject.SetActive(true);
                winGameObject.SetActive(false);
            }
        }
        private void BackToMenu()
        {
            if (activateInputs)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("MainMenu");
            }
        }

        private void ReloadScene()
        {
            if (activateInputs)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Monitors");
            }
        }
    }

}
