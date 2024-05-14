using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public interface IInputReceiver
    {
        void Directions(Vector2 direction);
        void Game1();
        void Game2();
        void Game3();
        void Game4();
        void Confirm();
        void Cancel();
        void Pause();
        InputPackage GetInputPackage();
    }
}
