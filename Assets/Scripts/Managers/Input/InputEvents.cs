using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CtrlAltJam3
{
    public class InputEvents
    {
        public DirectionsEvent directionsEvent;
        public Game1Event game1Event;
        public Game2Event game2Event;
        public Game3Event game3Event;
        public Game4Event game4Event;
        public ConfirmEvent confirmEvent;
        public CancelEvent cancelEvent;


        #region Public Methods

        public InputEvents()
        {
            directionsEvent = new DirectionsEvent();
            game1Event = new Game1Event();
            game2Event = new Game2Event();
            game3Event = new Game3Event();
            game4Event = new Game4Event();
            confirmEvent = new ConfirmEvent();
            cancelEvent = new CancelEvent();
        }

        public void AddInputPackage(InputPackage inputPackage, bool clearEvents)
        {
            if (clearEvents)
                ClearEvents(true, true);
            AddGameSelectionListeners(inputPackage);
            AddGameplayListeners(inputPackage);

        }
        public void AddInputPackage(InputPackage inputPackage)
        {
            AddInputPackage(inputPackage, true);
        }

        public void AddInputPackageGameSelection(InputPackage inputPackage, bool clearEvents)
        {
            if (clearEvents)
                ClearEvents(true, false);
            AddGameSelectionListeners(inputPackage);
        }

        public void AddInputPackageGameplay(InputPackage inputPackage, bool clearEvents)
        {
            if (clearEvents)
                ClearEvents(false, true);
            AddGameplayListeners(inputPackage);
        }
        #endregion

        #region Private Methods
        private void ClearEvents(bool clearGameSelectionListeners, bool clearGameplayListeners)
        {
            if (clearGameSelectionListeners)
            {
                game1Event.RemoveAllListeners();
                game2Event.RemoveAllListeners();
                game3Event.RemoveAllListeners();
                game4Event.RemoveAllListeners();
            }
            if (clearGameplayListeners)
            {
                directionsEvent.RemoveAllListeners();
                confirmEvent.RemoveAllListeners();
                cancelEvent.RemoveAllListeners();
            }
        }

        private void AddGameSelectionListeners(InputPackage inputPackage)
        {
            game1Event.AddListener(inputPackage.game1Event);
            game2Event.AddListener(inputPackage.game2Event);
            game3Event.AddListener(inputPackage.game3Event);
            game4Event.AddListener(inputPackage.game4Event);
        }

        private void AddGameplayListeners(InputPackage inputPackage)
        {
            directionsEvent.AddListener(inputPackage.directionsEvent);
            confirmEvent.AddListener(inputPackage.confirmEvent);
            cancelEvent.AddListener(inputPackage.cancelEvent);
        }
        #endregion

    }
    [System.Serializable] public class DirectionsEvent : UnityEvent<Vector2> { }
    [System.Serializable] public class Game1Event : UnityEvent { }
    [System.Serializable] public class Game2Event : UnityEvent { }
    [System.Serializable] public class Game3Event : UnityEvent { }
    [System.Serializable] public class Game4Event : UnityEvent { }
    [System.Serializable] public class ConfirmEvent : UnityEvent { }
    [System.Serializable] public class CancelEvent : UnityEvent { }
}

