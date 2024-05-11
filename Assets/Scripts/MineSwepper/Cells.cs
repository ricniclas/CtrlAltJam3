using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public struct Cell
    {
        public enum Type
        {
            Invalid,
            Empty,
            Mine,
            Number,
        }

        public Vector3Int position;
        public Type type;
        public int number;
        public bool revealed;
        public bool flagged;
        public bool exploded;
    }
}

