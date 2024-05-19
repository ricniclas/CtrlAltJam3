using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public struct Cams
    {
        public enum Type
        {
            EMPTY,
            ENABLED,
            DISABLED
        }

        public int position;
        public Type type;
        public int size;
        public int number;
    }

}
