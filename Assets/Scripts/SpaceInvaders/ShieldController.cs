using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace CtrlAltJam3
{
    public class ShieldController : MonoBehaviour
    {
        private Vector2 moveInput;
        private Rigidbody2D rb;
        public float rotationSpeed = 100f;
        public Vector3 rotation;
        public Vector3 localRotation;

        // Start is called before the first frame update

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float rotationAmount = -moveInput.x * rotationSpeed * Time.deltaTime;


            Vector3 currentRotation = transform.eulerAngles;

            float currentZRotation = currentRotation.z;
            if (currentZRotation > 180) currentZRotation -= 360;

            float newZRotation = currentZRotation + rotationAmount;

            if (newZRotation > -90 && newZRotation < 90)
            {
                transform.Rotate(0, 0, rotationAmount);
            }
        }


        public void Move(Vector2 input)
        {
            moveInput = input;
        }

     
    }
}
