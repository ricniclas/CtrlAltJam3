using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CtrlAltJam3
{
    public class BotCheck : MonoBehaviour
    {
        [SerializeField]
        private Tilemap tile;
        [SerializeField]
        private Tilemap collisionTilemap;
        public CursorMovement cursor;
        public bool teste;

        


        private void Start()
        {
            cursor = GetComponentInParent<CursorMovement>();
           
            //firstTry=true;  
        }

        private void Update()
        {
            cursor.CellType(gameObject);

            Debug.Log(cursor.CellType(gameObject));
        }

       



    }
}
