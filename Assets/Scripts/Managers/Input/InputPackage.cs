using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CtrlAltJam3
{
    public class InputPackage
    {
        public UnityAction<Vector2> directionsEvent { get; private set; }
        public UnityAction game1Event { get; private set; }
        public UnityAction game2Event { get; private set; }
        public UnityAction game3Event { get; private set; }
        public UnityAction game4Event { get; private set; }
        public UnityAction confirmEvent { get; private set; }
        public UnityAction cancelEvent { get; private set; }
        public UnityAction pauseEvent { get; private set; }

        public InputPackage(IInputReceiver inputReceiver)
        {
            directionsEvent = inputReceiver.Directions;
            game1Event = inputReceiver.Game1;
            game2Event = inputReceiver.Game2;
            game3Event = inputReceiver.Game3;
            game4Event = inputReceiver.Game4;
            pauseEvent = inputReceiver.Pause;
            confirmEvent = inputReceiver.Confirm;
            cancelEvent = inputReceiver.Cancel;
        }
    }
}
