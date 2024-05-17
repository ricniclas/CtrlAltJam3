using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class Minigame1 : MonoBehaviour, IMinigame, IInputReceiver
    {

        private InputPackage inputPackage => new InputPackage(this);
        private SpriteRenderer spriteRenderer;
        public Vector2 currentDirectionInput = Vector2.zero;
        private float speed = 5;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            transform.Translate(currentDirectionInput * speed *Time.deltaTime);
        }



        #region IMinigame Interface
        InputPackage IMinigame.GetInputPackage()
        {
            return inputPackage;
        }

        void IMinigame.InvokeConsequence()
        {

        }

        void IMinigame.PauseGame()
        {

        }

        void IMinigame.ResetInputs()
        {
            currentDirectionInput = Vector2.zero;
        }

        void IMinigame.ReceiveConsequence()
        {
            
        }

        void IMinigame.Selected()
        {

        }

        void IMinigame.Unselected()
        {

        }
        #endregion

        #region InputReceiver Interface
        void IInputReceiver.Cancel()
        {
        }

        void IInputReceiver.Confirm()
        {
            spriteRenderer.color = Random.ColorHSV();
        }

        void IInputReceiver.Directions(Vector2 direction)
        {
            currentDirectionInput = direction;
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
