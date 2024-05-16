using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace CtrlAltJam3
{
    public enum LifeBarAction
    {
        TAKE, ADD
    }

    public class LifeCircle : MonoBehaviour
    {

        [SerializeField] Material material;

        void Start()
        {
            material = this.GetComponent<SpriteRenderer>().material;
            updateLifeBar(0f, LifeBarAction.TAKE);
        }

        public void updateLifeBar(float value, LifeBarAction action)
        {
            switch (action)
            {
                case LifeBarAction.TAKE:
                    setArcOne(MathUtils.ConverterPorcentageToDegrees(value));
                    break;
                case LifeBarAction.ADD:
                    break;
            }
        }

        private void setArcOne(float value)
        {
            material.SetFloat("_Arc1", value);
        }


    }
}
