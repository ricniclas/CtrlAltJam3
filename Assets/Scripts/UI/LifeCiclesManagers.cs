using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class LifeCiclesManagers : MonoBehaviour
    {
        [SerializeField] LifeCircle[] lifeCircle;
        [SerializeField] private float unitMaxHealth;
        [SerializeField] Animator[] crewmatesAnimators;
        public float[] unitsHealth = new float[3];
        private MinigamesManager minigamesManager;
        private EventInstance eventInstance;

        public void Initialize(MinigamesManager minigamesManager)
        {
            this.minigamesManager = minigamesManager;
        }

        private void Start()
        {
            for (int i = 0; i < lifeCircle.Length; i++)
            {
                lifeCircle[i].updateLifeBar(100f,LifeBarAction.TAKE);
                unitsHealth[i] = unitMaxHealth;
            }
        }


        public void UpdateLife(float value, LifeBarAction barAction)
        {
            int currentLife;
            switch (barAction)
            {
                case LifeBarAction.TAKE:
                    currentLife = CheckUnitWithHealth(barAction);
                    if (currentLife != 999)
                    {
                        unitsHealth[currentLife] -= value;
                        lifeCircle[currentLife].updateLifeBar(unitsHealth[currentLife] / unitMaxHealth * 100, barAction);
                        if (unitsHealth[currentLife] <= 0)
                        {
                            crewmatesAnimators[currentLife].SetTrigger("Death");
                        }
                        else
                        {
                            crewmatesAnimators[currentLife].SetTrigger("Hurt");
                        }
                        if (GetMembersAlive() == 0)
                        {
                            minigamesManager.EndGame(false);
                        }
                    }
                    else
                    {
                        minigamesManager.EndGame(false);
                    }
                    break;
                case LifeBarAction.ADD:
                    currentLife = CheckUnitWithHealth(barAction);
                    if (currentLife != 999)
                    {
                        
                        unitsHealth[currentLife] = MathUtils.Limit((int)(unitsHealth[currentLife] + value),0,100);
                        lifeCircle[currentLife].updateLifeBar(unitsHealth[currentLife] / unitMaxHealth * 100, barAction);
                    }
                    break;
            }
        }

        public int GetMembersAlive()
        {
            int result = 0;
            for(int i = 0; i < unitsHealth.Length; i++)
            {
                if (unitsHealth[i] > 0)
                    result++;
            }
            return result;
        }

        private int CheckUnitWithHealth(LifeBarAction barAction)
        {
            switch (barAction)
            {
                case LifeBarAction.TAKE:
                    for (int i = unitsHealth.Length - 1; i >= 0; i--)
                    {
                        if (unitsHealth[i] > 0f)
                            return i;
                    }
                    return 999;
                case LifeBarAction.ADD:
                    for(int i = 0; i < unitsHealth.Length; i++)
                    {
                        if (unitsHealth[i] < unitMaxHealth)
                            return i;
                    }
                    return 999;
                default: 
                    return 999;
            }

        }

    }
}
