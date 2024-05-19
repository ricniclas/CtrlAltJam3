using DG.Tweening;
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

        private List<GameObject> waitPoints;
        private GameObject deactivateWaitPoint;
        private float moveSpeed = 2f;
        public int _currentWaypoint;

        private bool isActive = false;

        private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        void Start()
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
        }
        void Shoot()
        {
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
                if (isActive)
                {
                    spriteRenderer.flipX = transform.position.x > player.transform.position.x ? true : false;
                    transform.position = Vector3.MoveTowards(transform.position, waitPoints[_currentWaypoint].transform.position,
                    (moveSpeed * Time.deltaTime));

                    if (Vector3.Distance(waitPoints[_currentWaypoint].transform.position, transform.position) <= 0.1f)
                    {
                        _currentWaypoint = Random.Range(0, waitPoints.Count);
                        StartCoroutine(Fire(timer));
                    }
                    if (_currentWaypoint != waitPoints.Count) return;
                    waitPoints.Reverse();
                    _currentWaypoint = 0;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        deactivateWaitPoint.transform.position,(moveSpeed * Time.deltaTime));
                }
            }
        }

        public void Initialize(GameObject player,List<GameObject> waitPoints, GameObject deactivateWaitPoint)
        {
            this.player = player;
            this.waitPoints = waitPoints;
            this.deactivateWaitPoint = deactivateWaitPoint;
            _currentWaypoint = Random.Range(0, waitPoints.Count);

        }

        public void Activate()
        {
            isActive = true;
            spriteRenderer.DOFade(1f, .8f);
        }

        public void Deactivate()
        {
            isActive = false;
            spriteRenderer.DOFade(0f, .8f);
        }
    }
}
