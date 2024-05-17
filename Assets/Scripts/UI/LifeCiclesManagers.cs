using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class LifeCiclesManagers : MonoBehaviour
    {
        [SerializeField] LifeCircle[] lifeCircle;
        [SerializeField] private float unitMaxHealth;
        public float[] unitsHealth = new float[3];




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
                    }
                    else
                    {
                        Debug.Log("Game Over");
                    }
                    break;
                case LifeBarAction.ADD:
                    currentLife = CheckUnitWithHealth(barAction);
                    if (currentLife != 999)
                    {
                        unitsHealth[currentLife] += value;
                        lifeCircle[currentLife].updateLifeBar(unitsHealth[currentLife] / unitMaxHealth * 100, barAction);
                    }
                    else
                    {
                        Debug.Log("Max Health");
                    }
                    break;
            }
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
                        if (unitsHealth[i] > unitMaxHealth)
                            return i;
                    }
                    return 999;
                default: 
                    return 999;
            }

        }

    }
}
