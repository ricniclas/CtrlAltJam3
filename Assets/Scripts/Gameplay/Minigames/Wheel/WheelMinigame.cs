using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CtrlAltJam3
{
    public class WheelMinigame : MonoBehaviour, IMinigame, IInputReceiver
    {
        public int[] wheelPositions = new int[3];
        private int currentWheelIndex = 0;
        [SerializeField] private GameObject[] wheelSprites = new GameObject[3];
        private float[] wheelsTargetRotation = new float[3];
        private Queue<float>[] rotationQueue = new Queue<float>[3];

        private InputPackage inputPackage => new InputPackage(this);

        [SerializeField] private float inputHoldTime;
        private float currentInputHoldTime;

        private Vector2Int currentInput = Vector2Int.zero;

        [SerializeField] private GameObject selectedGameObject;


        #region MonoBehaviour Callbacks
        private void Start()
        {
            for (int i = 0; i < wheelPositions.Length; i++)
            {
                wheelPositions[i] = 0;
                wheelsTargetRotation[i] = wheelSprites[i].transform.eulerAngles.z;
                rotationQueue[i] = new Queue<float>();
            }
            selectedGameObject.SetActive(false);

        }

        private void Update()
        {
            if (currentInput != Vector2Int.zero)
                currentInputHoldTime += Time.deltaTime;

            if (currentInputHoldTime >= inputHoldTime)
            {
                currentInputHoldTime =  0;

                if (currentInput == Vector2Int.left || currentInput == Vector2Int.right)
                {
                    ChangeWheelIndex(currentInput.x);                }
                else if (currentInput == Vector2Int.down || currentInput == Vector2Int.up)
                {
                    ChangeWheelValue(currentInput.y);
                }

            }
        }
        #endregion

        #region Private Methods
        private void ChangeWheelIndex(int change)
        {
            currentWheelIndex = MathUtils.Wrap(currentWheelIndex + change, 0, 3);
        }
        private void ChangeWheelValue(int change)
        {
            wheelPositions[currentWheelIndex] = MathUtils.Wrap(wheelPositions[currentWheelIndex] + change, 0, 3);

            RotateWheel(change);

        }

        private void RotateWheel(int change)
        {
            rotationQueue[currentWheelIndex].Enqueue(change);
            StartRotation();

            if (!DOTween.IsTweening(wheelSprites[currentWheelIndex]))
            {
            }


        }

        private void StartRotation()
        {
            if (rotationQueue[currentWheelIndex].Count > 0)
            {
                float nextRotation = rotationQueue[currentWheelIndex].Dequeue();
                float newRotation = wheelsTargetRotation[currentWheelIndex] + (120 * nextRotation);
                wheelsTargetRotation[currentWheelIndex] = newRotation;
                Transform targetWheel = wheelSprites[currentWheelIndex].transform;
                targetWheel.transform.DOLocalRotate(new Vector3(0, 0, newRotation), 0.3f).
                    OnComplete(() =>
                    {
                        StartRotation();
                    });
            }

        }

        #endregion

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
            currentInput = Vector2Int.zero;
            currentInputHoldTime = 0;
        }

        InputPackage IMinigame.GetInputPackage()
        {
            return inputPackage;
        }

        void IMinigame.Selected()
        {
            selectedGameObject.SetActive(true);
        }

        void IMinigame.Unselected()
        {
            selectedGameObject.SetActive(false);
        }
        #endregion

        #region Input Receiver Interface
        void IInputReceiver.Directions(Vector2 direction)
        {
            Vector2Int intDirection = Vector2Int.RoundToInt(direction);
            if (currentInput != intDirection)
            {
                currentInput = intDirection;
                currentInputHoldTime = 0;

                if ( intDirection == Vector2Int.left || intDirection == Vector2Int.right)
                {
                    ChangeWheelValue(intDirection.x);
                }
                else if (intDirection == Vector2Int.down || currentInput == Vector2Int.up)
                {
                    ChangeWheelIndex(intDirection.y);
                }
            }
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
