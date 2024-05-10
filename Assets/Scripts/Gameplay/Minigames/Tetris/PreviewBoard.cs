using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CtrlAltJam3
{
    public class PreviewBoard : MonoBehaviour
    {
        private Tilemap tilemap;
        private PreviewPiece previewPiece;

        private void Awake()
        {
            tilemap = GetComponentInChildren<Tilemap>();
            previewPiece = GetComponent<PreviewPiece>();
        }

        public void Set(TetrominoData data)
        {
            tilemap.ClearAllTiles();
            previewPiece.Initialize(Vector3Int.zero, data);
            for (int i = 0; i < previewPiece.cells.Length; i++)
            {
                Vector3Int tilePosition = previewPiece.cells[i] + previewPiece.position;
                tilemap.SetTile(tilePosition, previewPiece.data.tile);
            }
        }
        public void Clear()
        {
            tilemap.ClearAllTiles();
        }
    }
}
