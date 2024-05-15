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

            //Debug.Log(rotationAmount);
            transform.Rotate(0, 0, rotationAmount);
        }


        void OnMove(InputValue value)
        {
            Debug.Log("Foi" + moveInput);
            moveInput = value.Get<Vector2>();

        }

     
    }
}
