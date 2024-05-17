using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class PlayerCollision : MonoBehaviour
    {
        public DronesMinigame dronesMinigame;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Shoot"))
            {
                dronesMinigame.TakeDamage();
            }

        }
    }
}
