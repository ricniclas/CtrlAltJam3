using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CtrlAltJam3
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Rigidbody2D rb;
        public float force;
        private float timer;
        private DroneController droneController;
        // Start is called before the first frame update
        void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();

            player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

            //float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }

        // Update is called once per frame
        void Update()
        {
           
        }


        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.CompareTag("Player") || other.CompareTag("Shield"))
            {
                Debug.Log("Jogador entrou no trigger.");

                //Destroy(this.gameObject);
                //gameObject.SetActive(false);
                ObjectPool.instance.ReturnToPool(gameObject);
            }
            if (other.CompareTag("Player"))
            {
                HealthManager.Instance.TakeDamage(15f);
            }

        }

    }
}
