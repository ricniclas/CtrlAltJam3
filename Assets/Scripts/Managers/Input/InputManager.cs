using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CtrlAltJam3
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;
        private InputEvents inputEvents;

        #region Monobehaviour Callbacks
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
            inputEvents = new InputEvents();
        }
        #endregion

        #region Public Methods

        public void AddGameplayEvents(InputPackage inputPackage, bool clearEvents)
        {
            inputEvents.AddInputPackageGameplay(inputPackage, clearEvents);
        }

        public void AddGameSelectionyEvents(InputPackage inputPackage, bool clearEvents)
        {
            inputEvents.AddInputPackageGameSelection(inputPackage, clearEvents);
        }

        public void AddInputPackageEvents(InputPackage inputPackage, bool clearEvents)
        {
            inputEvents.AddInputPackage(inputPackage, clearEvents);
        }

        #endregion

        #region Input Callbacks

        public void Directions(InputAction.CallbackContext context) 
        { 
            inputEvents.directionsEvent.Invoke(context.ReadValue<Vector2>()); 
        }
        public void Game1(InputAction.CallbackContext context)
        { 
            if(context.started)
            {
                inputEvents.game1Event.Invoke();
            }
        }
        public void Game2(InputAction.CallbackContext context) 
        {
            if (context.started)
            {
                inputEvents.game2Event.Invoke();
            }
        }
        public void Game3(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                inputEvents.game3Event.Invoke();
            }
        }
        public void Game4(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                inputEvents.game4Event.Invoke();
            }
        }
        public void Confirm(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                inputEvents.confirmEvent.Invoke();
            }
        }
        public void Cancel(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                inputEvents.cancelEvent.Invoke();
            }
        }

        #endregion
    }
}
