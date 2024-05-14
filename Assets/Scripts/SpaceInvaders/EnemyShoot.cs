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
        public bool isMoving;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            //StartCoroutine(Fire(timer));
        }


        // Update is called once per frame
        void Update()
        {
            /*if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }*/
            //float distance = Vector2.Distance(transform.position, player.transform.position);
            //if(distance < 10)
            //{

            //timer += Time.deltaTime;

            /*timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0; 
                Shoot();
            }*/
            //}
            Shoot();


        }



        void Shoot()
        {
            //Instantiate(shoot, shootPosition.position, Quaternion.identity);
            GameObject bullet = ObjectPool.instance.GetNormalShootPool();
            if (bullet != null)
            {
                bullet.transform.position = shootPosition.position;

                bullet.SetActive(true);
            }
        }

        private IEnumerator Fire(float spawnR)
        {
            while (this.gameObject != null)
            {
                Shoot();
                yield return new WaitForSeconds(spawnR);
            }
        }
    }
}
