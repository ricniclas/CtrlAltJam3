using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public struct CamBoard
    {
        public Cams[] board;
        public CamGroup[] cams;

        public void checkCode(string code)
        {
            for (int i = 0; i < cams.Length; i++) {
                cams[i].checkCode(code);
            }
            updateBoard();
        }

        public bool hasCamEnabled(int x)
        {
            Debug.Log("posição: " + x);
            if (board[x].type != Cams.Type.ENABLED){
                return false;
            }
            else{
                return true;
            }
        }

        private void updateBoard()
        {
            for ( int j = 0; j < cams.Length; j++)
            {
                for(int i = 0;i < cams[j].cams.Length;i++)
                {
                    board[cams[j].cams[i].position] = cams[j].cams[i];
                }
            }
        }
    }
}
