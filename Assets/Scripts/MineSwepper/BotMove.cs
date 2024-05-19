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

        public static int totalBots = 3;

        private bool hasFinished = false;

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

            
           //Debug.Log(isIngrid);
           
            if (start )
            {   
                direction.x = 1;
                transform.localPosition += (Vector3)direction;
            }else if (!start)
            {
                direction.x = -1;
                transform.localPosition += (Vector3)direction;
            }

          

            /*if (finish)
            {
                gameManager.CleamBoard(gameObject);
            }*/
            

            if (gameManager.botsFinished >= totalBots)
            {
                //shouldMove = true;
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
            DecodeBomb();



            if (gameManager.botsFinished >= totalBots)
            {
                shouldMove = true;

                if (gameManager.botsFinished == 3)
                {

                    if (!Ingrid(direction) && direction.x==-1)
                    {
                        Debug.Log("pare agora");
                        shouldMove = false;
                    }
                }

            }
            if (finish && !hasFinished)
            {
                hasFinished = true;
                gameManager.botsFinished++;
                shouldMove = false;
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
            //Debug.Log(cell.type);
            if (cell.type == Cell.Type.Mine && !cell.flagged)
            {
                gameManager.Reveal(gameObject);
                //Debug.Log("Damage");
               
            }
        }

        public void DecodeBomb()
        {
            if (check.cell.flagged)
            {
                decodTime += Time.deltaTime;

                shouldMove = false;
                if (decodTime > decodTimer)
                {
                    decodTime = 0;
                    shouldMove = true;
                }

            }

        }
           
}
}
