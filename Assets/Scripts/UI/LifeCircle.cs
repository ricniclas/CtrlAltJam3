using DG.Tweening;
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
            //updateLifeBar(0f, LifeBarAction.TAKE);
        }

        public void updateLifeBar(float value, LifeBarAction action)
        {
            switch (action)
            {
                case LifeBarAction.TAKE:
                    setArcOne(MathUtils.ConverterPorcentageToDegrees(Mathf.Abs(value - 100)));
                    break;
                case LifeBarAction.ADD:
                    setArcOne(MathUtils.ConverterPorcentageToDegrees(Mathf.Abs(value - 100)));
                    break;
            }
        }

        private void setArcOne(float value)
        {
            //material.SetFloat("_Arc1", value);

            DOVirtual.Float(material.GetFloat("_Arc1"), value, .5f, (float value) => {
                material.SetFloat("_Arc1", value);
            });
        }


    }
}
