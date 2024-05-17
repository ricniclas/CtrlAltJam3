using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CtrlAltJam3
{
    public interface IMinigame
    {
        void PauseGame();
        void InvokeConsequence();
        void ReceiveConsequence();
        void ApplyDamage();
        void ApplyHeal();
        void ResetInputs();
        void Selected();
        void Unselected();
        void SetMinigameManager(MinigamesManager manager);
        InputPackage GetInputPackage();
    }
}
