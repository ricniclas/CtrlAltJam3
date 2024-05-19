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
            foreach (var cam in cams)
            {
                cam.checkCode(code);
            }
        }

        public bool hasCamEnabled(int x)
        {
            return board[x].type == Cams.Type.ENABLED;
        }
    }
}
