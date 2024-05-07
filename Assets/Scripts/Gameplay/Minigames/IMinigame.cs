using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public interface IMinigame
    {
        void PauseGame();
        void InvokeConsequence();
        void ReceiveConsequence();
        void ResetInputs();
        InputPackage GetInputPackage();
    }
}
