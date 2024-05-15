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
        float rotationAmount;
        // Start is called before the first frame update

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            

            
           rotationAmount = -moveInput.x * rotationSpeed * Time.deltaTime;

            
            //Debug.Log(rotationAmount);
           
        }


        void OnMove(InputValue value)
        {
            Debug.Log("Foi" + moveInput);
            moveInput = value.Get<Vector2>();

        }

     
    }
}
