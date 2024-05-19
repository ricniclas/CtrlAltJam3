using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace CtrlAltJam3
{
    public class CursorMovement : MonoBehaviour
    {
        [SerializeField]
        private Tilemap tile;
        [SerializeField]
        private Tilemap collisionTilemap;

        public bool teste;

        public Game gameManager { set; private get; }


        private void Start()
        {
            gameManager = GetComponentInParent<Game>();
           
            //firstTry=true;  
        }

        public void Move(Vector2 direction)
        {
            if (CanMove(direction))
            {
                transform.localPosition += (Vector3)direction;
                //gameManager.CellType();
            }
        }

        /*public Cell CellType(GameObject targetObject)
        {

            return gameManager.CellType(targetObject);

        }*/

        private bool CanMove(Vector2 direction)
        {
            Vector3Int gridPosition = tile.WorldToCell(transform.position + (Vector3)direction * transform.parent.localScale.x);
            if (!tile.HasTile(gridPosition)/*||collisionTilemap.HasTile(gridPosition)*/)
            {
                return false;
            }
            return true;
        }

        public void OnFlag()
        {
            gameManager.Flag();
        }

        public void OnReveal()
        {
            Debug.Log("foi");

            if (gameManager.firstTry)
            {
                gameManager.FirstMove();
                //gameManager.firstTry = false;
            }
            gameManager.Reveal(gameObject);


            //gameManager.GenerateMines();
            //gameManager.GenerateMines();
        }

    }


}
