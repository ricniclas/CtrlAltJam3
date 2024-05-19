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
        private float  decodTime;
        public float decodTimer;



        public Vector2 direction;
        public Game gameManager;
        public BotCheck check;
        public bool shouldMove;
        public bool finish;
        public bool isMoving;
        public bool start;
        public bool isIngrid;
        public bool hasStartedDecoding;
        Cell cell;
        int countToBegin;
        private void Start()
        {
            gameManager = GetComponentInParent<Game>();
            check = GetComponentInChildren<BotCheck>();
            shouldMove = true;
            StartCoroutine(Move(timer));
            start = true;
            countToBegin = 2;
        }

        public void Move()
        {

            
            Debug.Log(isIngrid);
           
            if (start || countToBegin <=0)
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
               
                if (!Ingrid(direction))
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

        private bool Ingrid(Vector2 direction)
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
            isIngrid = Ingrid(direction);

            if(!isIngrid && direction.x == 1)
            {
               
                   finish = true;
            }
            else{
                finish = false;
            }
            cell = CellType(gameObject);
            TakeDamage();

            if (hasStartedDecoding)
            {
                
                StartCoroutine(Decoding(decodTime));
                hasStartedDecoding = false;
            }
            if (check.cell.flagged)
            {
                decodTime += Time.deltaTime;

                shouldMove = false;
                if (decodTime > decodTimer)
                {
                    decodTime = 0;
                    shouldMove = true;

                    //Shoot();
                }

            }

        }


        private IEnumerator Move(float timer)
        {
            while (true) 
            {
                if (shouldMove)
                {
                    isMoving = false;
                    Move();
                    yield return new WaitForSeconds(timer);
                    isMoving = true;
                }
                else
                {
                    yield return null;
                }
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

        public void DecodeBomb()
        {
            StartCoroutine(Decoding(decodTime));
           
        }
        private IEnumerator Decoding(float decodTime)
        {

            if (check.cell.flagged)
            {
                ///gameManager.Reveal(gameObject);
                Debug.Log("Deocoding...");
                hasStartedDecoding=true;
                shouldMove = false;
                yield return new WaitForSeconds(timer);
                shouldMove = true;
            }
            

        }
           
}
}
