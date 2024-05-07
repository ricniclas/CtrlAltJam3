using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class MinigamesManager : MonoBehaviour, IInputReceiver
    {
        public GameObject minigame1GameObject;
        public GameObject minigame2GameObject;
        public GameObject minigame3GameObject;
        public GameObject minigame4GameObject;
        private IMinigame minigame1;
        private IMinigame minigame2;
        private IMinigame minigame3;
        private IMinigame minigame4;
        private List<IMinigame> minigames;
        private InputPackage inputPackage => new InputPackage(this);


        #region Monobehaviour Callbacks

        private void Start()
        {
            InputManager.instance.AddGameSelectionyEvents(inputPackage, true);
            minigame1 = minigame1GameObject.GetComponent<IMinigame>();
            minigame2 = minigame2GameObject.GetComponent<IMinigame>();
            minigame3 = minigame3GameObject.GetComponent<IMinigame>();
            minigame4 = minigame4GameObject.GetComponent<IMinigame>();
            minigames = new List<IMinigame>
            {
                minigame1,
                minigame2,
                minigame3,
                minigame4
            };
            InputManager.instance.AddGameplayEvents(minigame1.GetInputPackage(), true);
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
            for(int i = 0; i < minigames.Count; i++)
            {
                minigames[i].ResetInputs();
            }
            InputManager.instance.AddGameplayEvents(minigame1.GetInputPackage(), true);
        }

        void IInputReceiver.Game2()
        {
            for (int i = 0; i < minigames.Count; i++)
            {
                minigames[i].ResetInputs();
            }
            InputManager.instance.AddGameplayEvents(minigame2.GetInputPackage(), true);
        }

        void IInputReceiver.Game3()
        {
            for (int i = 0; i < minigames.Count; i++)
            {
                minigames[i].ResetInputs();
            }
            InputManager.instance.AddGameplayEvents(minigame3.GetInputPackage(), true);
        }

        void IInputReceiver.Game4()
        {
            for (int i = 0; i < minigames.Count; i++)
            {
                minigames[i].ResetInputs();
            }
            InputManager.instance.AddGameplayEvents(minigame4.GetInputPackage(), true);
        }

        InputPackage IInputReceiver.GetInputPackage()
        {
            return inputPackage;
        }
        #endregion
    }
}
