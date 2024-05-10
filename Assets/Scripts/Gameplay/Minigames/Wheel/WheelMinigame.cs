using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CtrlAltJam3
{
    public class WheelMinigame : MonoBehaviour
    {
        [SerializeField] private GameObject[] wheelSprites = new GameObject[3];
        private int[] wheelPositions = new int[3];
        private float[] wheelsTargetRotation = new float[3];
        private int currentWheelIndex = 0;
        private float targetRotation = 0;

        private void Start()
        {
            for (int i = 0; i < wheelPositions.Length; i++)
            {
                wheelPositions[i] = 0;
                wheelsTargetRotation[i] = wheelSprites[i].transform.eulerAngles.z;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeWheelIndex(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeWheelIndex(1);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeWheelValue(1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeWheelValue(-1);
            }
        }

        private void ChangeWheelIndex(int change)
        {
            currentWheelIndex = MathUtils.Wrap(currentWheelIndex + change,0,3);
        }
        private void ChangeWheelValue(int change)
        {
            wheelPositions[currentWheelIndex] = MathUtils.Wrap(wheelPositions[currentWheelIndex] + change,0,3);

            float newRotation = wheelsTargetRotation[currentWheelIndex] + (120 * change);
            Transform targetWheel = wheelSprites[currentWheelIndex].transform;
            targetWheel.transform.DORotate(new Vector3(0, 0,newRotation),0.3f).
                OnComplete(()=> wheelsTargetRotation[currentWheelIndex] = newRotation);

        }
    }
}
