using UnityEngine;
using UnityEngine.Tilemaps;

namespace CtrlAltJam3
{
    public class CamsManager : MonoBehaviour
    {

        [SerializeField] Tilemap tilemap;
        [SerializeField] int height, width, camAmount;  
        [SerializeField] int[] players;
        [SerializeField] CamBoard[] camBoards;
        [SerializeField] Tile enabledCam,disabledCam;
        [SerializeField] CamsGeneretor generetor;

        public void Init() 
        {
            generetor.updateGenerator(camAmount, width);
            camBoards = new CamBoard[players.Length];
            for(int i = 0; i < camBoards.Length; i++)
            {
                camBoards[i] = generateBoard();
                drawnBoard(i);
            }
        }

        public void drawnBoard(int player)
        {
            for (int col = 0; col < width; col++)
            {
                Cams.Type type = camBoards[player].board[col].type;
                if ( type != Cams.Type.EMPTY)
                {
                    tilemap.SetTile(new Vector3Int(col, players[player], 0), getCamTile(type));
                }
            }
        }

        public void checkCode(string code)
        {
            tilemap.ClearAllTiles();
            for (int i = 0;i < camBoards.Length; i++)
            {
                camBoards[i].checkCode(code);
                drawnBoard(i);
            }
        }

        public bool hasCamEnabled(int player, int position)
        {
            if (player < camBoards.Length)
            {
                return camBoards[player].hasCamEnabled(position);
            }else { return false; } 
        }

        public string getCamCode(int x, int y)
        {
            bool cancheck = false;
            foreach(var position in players)
            {
                if(position == y) cancheck = true;
            }
            if (cancheck) return camBoards[y].cams[x].code;
            else return "";
        }

        private Tile getCamTile(Cams.Type type)
        {
            switch (type)
            {
                case Cams.Type.DISABLED:
                    return disabledCam;
                case Cams.Type.ENABLED:
                    return enabledCam;
                default: return null;
            }
        }

        private CamBoard generateBoard()
        { 
            return generetor.GetBoard(); 
        }    

    }
}
