using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CtrlAltJam3
{
    public class BotMove : MonoBehaviour
    {
        [SerializeField]
        private Tilemap tile;
        [SerializeField]
        private Tilemap collisionTilemap;

        public float  timer;

        public Vector2 direction;
        public Game gameManager;

        public bool isMoving;
        public bool start;
        public bool finish;
        private void Start()
        {
            gameManager = GetComponentInParent<Game>();
            StartCoroutine(Move(timer));
            start = true;
        }

        public void Move()
        {
           finish = CanMove(direction);

           Debug.Log(finish);

            if (start)
            {   
                direction.x = 1;
                transform.localPosition += (Vector3)direction;
            }else if (!start)
            {
                direction.x = -1;
                transform.localPosition += (Vector3)direction;
            }
        }

        public Cell CellType(GameObject targetObject)
        {

            return gameManager.CellType(targetObject);

        }

        private bool CanMove(Vector2 direction)
        {
            Vector3Int gridPosition = tile.WorldToCell(transform.position + (Vector3)direction * transform.parent.localScale.x);
            if (!tile.HasTile(gridPosition))
            {
                start = false;
                return false;
            }
            return true;
        }

        void Update()
        {
            //StartCoroutine(Move(timer));

            /*if(!start && !finish)
            {
                finish = true;
            }else if(!start && finish)
            {
                finish = CanMove(direction);

                if (!finish)
                {
                    direction.x = 0;
                }
            }*/

          
        }


        private IEnumerator Move(float timer)
        {
            while (true) // Loop infinito
            {
                isMoving = false;
                Move();
                yield return new WaitForSeconds(timer);
                isMoving = true;
            }
        }

    }
}
