using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CtrlAltJam3
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;

        [SerializeField] private Button winScreenRetry;
        [SerializeField] private Button loseScreenRetry;
        [SerializeField] private Button winScreenBackToMenu;
        [SerializeField] private Button loseScreenBackToMenu;

    }
}
