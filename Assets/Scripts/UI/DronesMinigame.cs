
using CtrlAltJam3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CtrlAltJam3
{
    public class DronesMinigame : MonoBehaviour, IMinigame, IInputReceiver
    {
        private int nextDrone;

        [SerializeField] private ShieldController player; 
        [SerializeField] private int maxDrones;
        [SerializeField] private DroneController dronePrefab;
        private DroneController[] activeDrones;

        [SerializeField] private GameObject waitPointsParent;
        private GameObject[] waitPoints;

        [SerializeField] private GameObject deactivateWaitpoint;
        private InputPackage inputPackage => new InputPackage(this);

        [SerializeField] private GameObject selectedGameObject;
        [SerializeField] private SpriteButtonAnimation inputButtonSprite;
        // Start is called before the first frame update
        void Start()
        {
            selectedGameObject.SetActive(false);
            activeDrones = new DroneController[maxDrones];
            waitPoints = new GameObject[waitPointsParent.transform.childCount];

            for(int i  = 0; i < waitPointsParent.transform.childCount; i++)
            {
                waitPoints[i] = waitPointsParent.transform.GetChild(i).gameObject;
            }
            for(int i = 0; i < activeDrones.Length; i++)
            {
                activeDrones[i] = Instantiate(dronePrefab, deactivateWaitpoint.transform);
                activeDrones[i].Initialize(player.transform.parent.gameObject, waitPoints.ToList(), deactivateWaitpoint);
            }
            AlertLevelUpdated(2);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                TakeDamage(20);
            }

            if (Input.GetKeyUp(KeyCode.B))
            {
                Heal(5);
            }

        }

        public void TakeDamage(float damage)
        {
            ActivateNextDrone();
        }


        public void Heal(float healingAmount)
        {

        }
        public void ActivateNextDrone()
        {
            if (nextDrone < activeDrones.Length)
            {
                nextDrone++;
            }
        }

        private void AlertLevelUpdated(int alertLevel)
        {
            switch (alertLevel)
            {
                case 1:
                    ActivateDrones(2);
                    break;
                case 2:
                    ActivateDrones(4);
                    break;
                case 3:
                    ActivateDrones(6);
                    break;
                default:
                    ActivateDrones(1);
                    break;

            }
        }

        private void ActivateDrones(int numberOfDrones)
        {
            for (int i = 0; i < activeDrones.Length; i++)
            {
                if (numberOfDrones > i)
                    activeDrones[i].Activate();
                else
                    activeDrones[i].Deactivate();
            }
        }

        #region Minigame Interface

        void IMinigame.PauseGame()
        {
        }

        void IMinigame.InvokeConsequence()
        {
        }

        void IMinigame.ReceiveConsequence()
        {
        }

        void IMinigame.ResetInputs()
        {
            player.Move(Vector2.zero);
        }

        void IMinigame.Selected()
        {
            selectedGameObject.SetActive(true);
            inputButtonSprite.AnimateClick();
        }

        void IMinigame.Unselected()
        {
            selectedGameObject.SetActive(false);
        }

        InputPackage IMinigame.GetInputPackage()
        {
            return inputPackage;
        }
        #endregion

        #region InputReceiver Interface


        void IInputReceiver.Directions(Vector2 direction)
        {
            player.Move(direction);
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
        }

        void IInputReceiver.Pause()
        {
        }

        InputPackage IInputReceiver.GetInputPackage()
        {
            return inputPackage;
        }
        #endregion
    }
}
