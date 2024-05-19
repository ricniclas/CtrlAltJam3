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

        public bool shouldMove;
        public bool finish;
        public bool isMoving;
        public bool start;
        public bool isIngrid;
        Cell cell;
        private void Start()
        {
            gameManager = GetComponentInParent<Game>();
            shouldMove = true;
            StartCoroutine(Move(timer));
            start = true;
           
        }

        public void Move()
        {

            
            Debug.Log(isIngrid);

            if (start)
            {   
                direction.x = 1;
                transform.localPosition += (Vector3)direction;
            }else if (!start)
            {
                direction.x = -1;
                transform.localPosition += (Vector3)direction;
            }

            if (direction.x == -1)
            {
               
                if (!CanMove(direction))
                {
                    Debug.Log("pare agora");
                    shouldMove = false;
                }
            }

            if (finish)
            {
                gameManager.CleamBoard(gameObject);
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
            isIngrid = CanMove(direction);

            if(!isIngrid && direction.x == 1)
            {
               
                   finish = true;
            }
            else{
                finish = false;
            }
            cell = CellType(gameObject);
            TakeDamage();
            //gameManager.Reveal();
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
            while (shouldMove) // Loop infinito
            {
                isMoving = false;
                Move();
                yield return new WaitForSeconds(timer);
                isMoving = true;
            }
        }
        public void TakeDamage()
        {
            Debug.Log(cell.type);
            if (cell.type == Cell.Type.Mine)
            {
                gameManager.Reveal(gameObject);
                Debug.Log("Damage");
               
            }
        }
    }
}
