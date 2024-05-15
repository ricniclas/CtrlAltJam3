using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CtrlAltJam3
{
    public class DroneController : MonoBehaviour
    {

        public GameObject shoot;
        public GameObject player;
        public Transform shootPosition;
        public float timer;
        public bool isMoving;

        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private float moveSpeed = 5f;
        public int _currentWaypoint;


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

               
            /*if (!isMoving)
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    timer = 0;


                    //Shoot();
                }

            }*/


            //}
            ///Shoot();


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
            isMoving = false;
            Shoot();
            yield return new WaitForSeconds(spawnR);
            isMoving = true;

        }


        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (isMoving)
            {
               
                transform.position = Vector3.MoveTowards(transform.position, waypoints[_currentWaypoint].transform.position,
                (moveSpeed * Time.deltaTime));

                if (Vector3.Distance(waypoints[_currentWaypoint].transform.position, transform.position) <= 0)
                {

                    //_currentWaypoint++;
                    _currentWaypoint = Random.Range(0, waypoints.Count);
                    StartCoroutine(Fire(timer));
                }

           
                
                if (_currentWaypoint != waypoints.Count) return;
                waypoints.Reverse();
                _currentWaypoint = 0;
                

            }
            
        }

    }
}
