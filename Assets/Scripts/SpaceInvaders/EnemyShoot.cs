using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CtrlAltJam3
{
    public class EnemyShoot : MonoBehaviour
    {

        public GameObject shoot;
        public GameObject player;
        public Transform shootPosition;
        public float timer;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

        }

        // Update is called once per frame
        void Update()
        {
            //float distance = Vector2.Distance(transform.position, player.transform.position);
            //if(distance < 10)
            //{

            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                Shoot();
            }
            //}
            
        }

        void Shoot()
        {
            Instantiate(shoot, shootPosition.position, Quaternion.identity);
        }
    }
}
