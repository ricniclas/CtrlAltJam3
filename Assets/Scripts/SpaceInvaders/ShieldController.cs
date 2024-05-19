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

        // Start is called before the first frame update

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float rotationAmount = -moveInput.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(0, 0, rotationAmount);
        }


        public void Move(Vector2 input)
        {
            moveInput = input;
        }

     
    }
}
