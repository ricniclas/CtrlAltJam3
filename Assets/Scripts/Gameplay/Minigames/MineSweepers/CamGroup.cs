using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

namespace CtrlAltJam3
{
    public struct CamGroup
    {
        public int size;
        public Cams[] cams;
        public string code;

        public void checkCode(string _code)
        {
            if (code == _code)
            {
                this.setEnable(Cams.Type.ENABLED);
            }else
            {
                this.setEnable(Cams.Type.ENABLED);
            }
        }

        private void setEnable(Cams.Type enable)
        {
            for (int i = 0; i < cams.Length; i++)
            {
                cams[i].type = enable;
            }
        }

    }

}
