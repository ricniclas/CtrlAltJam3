using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CtrlAltJam3
{
    public class PreviewPiece : MonoBehaviour
    {
        public TetrominoData data { get; private set; }
        public Vector3Int[] cells { get; private set; }
        public Vector3Int position { get; private set; }
        public int rotationIndex { get; private set; }
        public void Initialize(Vector3Int position, TetrominoData data)
        {
            this.position = position;
            this.data = data;
            rotationIndex = 0;


            if (cells == null)
            {
                cells = new Vector3Int[data.cells.Length];
            }

            for (int i = 0; i < data.cells.Length; i++)
            {
                cells[i] = (Vector3Int)data.cells[i];
            }
        }
    }
}
