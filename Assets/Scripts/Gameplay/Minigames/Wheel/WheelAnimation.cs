using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WheelAnimation : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            Unselect();
        }
        public void Select()
        {
            spriteRenderer.color = Color.white;
            Vector3 originalScale = transform.localScale;
            transform.DOPunchScale(originalScale * 0.3f, 0.3f);
        }

        public void Unselect()
        {
            spriteRenderer.color = Color.gray;
        }
    }
}
