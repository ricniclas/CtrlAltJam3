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
        public BotMove bot;
        public bool teste;
        public Cell cell;
        


        private void Start()
        {
            bot = GetComponentInParent<BotMove>();
           
            //firstTry=true;  
        }

        private void Update()
        {
            cell = bot.CellType(gameObject);

            //Debug.Log(cell.type);
            //Debug.Log(cell.flagged);
        }

       
       


    }
}
