using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CtrlAltJam3
{
    public class LightsController : MonoBehaviour
    {
        [SerializeField] private Light2D globalLight;
        [SerializeField] private Light2D[] lights;



        public void Initialize(int targetLight)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].gameObject.SetActive(false);
            }
            DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 0.3f, 1f)
                .OnComplete(() =>
                {
                    ActivateLight(targetLight);
                });
        }

        public void ActivateLight(int index)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].gameObject.SetActive(false);
            }
            lights[index].gameObject.SetActive(true);
            lights[index].intensity = 0f;
            DOTween.To(() => lights[index].intensity, x => lights[index].intensity = x, 1f, 0.3f);

        }
    }
}
