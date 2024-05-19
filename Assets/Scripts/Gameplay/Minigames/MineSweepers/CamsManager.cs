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

        void Start () 
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
                Debug.Log("Position: " + col + " - Type: " + type);
                if ( type != Cams.Type.EMPTY)
                {
                    tilemap.SetTile(new Vector3Int(col, players[player], 0), getCamTile(type));
                }
            }
        }

        public void checkCode(string code)
        {
            foreach (var board in camBoards)
            {
                board.checkCode(code);
            }
        }

        public bool hasCamEnabled(int player, int position)
        {
            return camBoards[player].hasCamEnabled(position);
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
