using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace CtrlAltJam3
{
    public class WheelMinigame : MonoBehaviour, IMinigame, IInputReceiver
    {
        public int[] wheelPositions = new int[3];
        private int currentWheelIndex = 0;
        [SerializeField] private WheelAnimation[] wheelSprites;
        private float[] wheelsTargetRotation = new float[3];
        private Queue<float>[] rotationQueue = new Queue<float>[3];
        private InputPackage inputPackage => new InputPackage(this);

        [SerializeField] private float inputHoldTime;
        private float currentInputHoldTime;
        private Vector2Int currentInput = Vector2Int.zero;

        [SerializeField] private GameObject selectedGameObject;
        [SerializeField] private SpriteButtonAnimation inputButtonSprite;
        [SerializeField] private Game minefieldManager;
        private MinigamesManager minigamesManager;

        private string[] code1 = { "1", "2", "3" };
        private string[] code2 = { "A", "B", "C" };
        private string[] code3 = { "X", "Y", "Z" };

        #region MonoBehaviour Callbacks
        private void Start()
        {
            for (int i = 0; i < wheelPositions.Length; i++)
            {
                wheelPositions[i] = 0;
                wheelsTargetRotation[i] = wheelSprites[i].transform.eulerAngles.z;
                rotationQueue[i] = new Queue<float>();
            }
            minefieldManager.camsManager.checkCode(TranslateCode());
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
                    ChangeWheelValue(currentInput.x);
                }
                else if (currentInput == Vector2Int.down || currentInput == Vector2Int.up)
                {
                    ChangeWheelIndex(currentInput.y);
                }

            }
        }
        #endregion

        #region Private Methods
        private void ChangeWheelIndex(int change)
        {
            currentWheelIndex = MathUtils.Wrap(currentWheelIndex + change, 0, 3);
            SelectWheelAnimation(currentWheelIndex);
        }
        private void ChangeWheelValue(int change)
        {
            wheelPositions[currentWheelIndex] = MathUtils.Wrap(wheelPositions[currentWheelIndex] + change, 0, 3);

            RotateWheel(change);
            minefieldManager.camsManager.checkCode(TranslateCode());

        }

        private string TranslateCode()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(code1[wheelPositions[0]]);
            stringBuilder.Append(code2[wheelPositions[1]]);
            stringBuilder.Append(code3[wheelPositions[2]]);
            return stringBuilder.ToString();

        }

        private void RotateWheel(int change)
        {
            rotationQueue[currentWheelIndex].Enqueue(change);
            StartRotation();

            if (!DOTween.IsTweening(wheelSprites[currentWheelIndex]))
            {
            }
        }

        private void SelectWheelAnimation(int index)
        {
            if(index < 0)
            {
                for (int i = 0; i < wheelSprites.Length; i++)
                {
                    wheelSprites[i].Unselect();
                }
            }
            else
            {
                for (int i = 0; i < wheelSprites.Length; i++)
                {
                    if (i != index)
                        wheelSprites[i].Unselect();
                }
                wheelSprites[index].Select();
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
            SelectWheelAnimation(currentWheelIndex);
            inputButtonSprite.AnimateClick();
        }

        void IMinigame.UpdateAlertLevel(int alertLevel)
        {

        }
        int IMinigame.GetInnerAlertLevel()
        {
            return 0;
        }

        void IMinigame.Unselected()
        {
            selectedGameObject.SetActive(false);
            SelectWheelAnimation(-1);
        }

        void IMinigame.SetMinigameManager(MinigamesManager manager)
        {
            minigamesManager = manager;
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
