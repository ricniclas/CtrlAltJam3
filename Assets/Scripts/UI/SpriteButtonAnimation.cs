using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteButtonAnimation : MonoBehaviour
    {
        private SpriteRenderer mainSprite;
        private SpriteRenderer secundarySprite;

        private void Awake()
        {
            mainSprite = GetComponent<SpriteRenderer>();

            GameObject secundaryGameObject = new GameObject();
            secundaryGameObject = Instantiate(secundaryGameObject, gameObject.transform);
            secundaryGameObject.AddComponent<SpriteRenderer>();
            secundarySprite = secundaryGameObject.GetComponent<SpriteRenderer>();
            secundarySprite.sprite = mainSprite.sprite;
            secundarySprite.sortingLayerID = mainSprite.sortingLayerID;
            secundarySprite.sortingOrder = mainSprite.sortingOrder + 1;
            secundarySprite.enabled = false;
        }

        public void SetSprite(Sprite sprite)
        {
            mainSprite.sprite = sprite;
            secundarySprite.sprite = sprite;
        }

        public void AnimateClick()
        {
            secundarySprite.enabled = true;
            Vector3 originalScale = secundarySprite.transform.localScale;
            secundarySprite.gameObject.transform.DOScale(3,0.4f)
                .OnComplete(() =>
                {
                    secundarySprite.transform.localScale = originalScale;
                    secundarySprite.enabled = false;
                });
            secundarySprite.color = Color.white;
            secundarySprite.DOFade(0f, .35f);
        }
    }
}
