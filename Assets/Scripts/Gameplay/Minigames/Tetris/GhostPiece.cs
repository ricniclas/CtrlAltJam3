using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CtrlAltJam3
{
    public class GhostPiece : MonoBehaviour
    {
        public Tile tile;
        public TetrisBoard tetrisBoard;
        public Piece trackedPiece;

        public Tilemap tilemap { get; private set; }
        public Vector3Int[] cells { get; private set; }
        public Vector3Int position { get; private set; }

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            tilemap = GetComponentInChildren<Tilemap>();
            cells = new Vector3Int[4];
        }

        private void LateUpdate()
        {
            Clear();
            Copy();
            Drop();
            Set();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        private void Clear()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Vector3Int tilePosition = cells[i] + position;
                tilemap.SetTile(tilePosition, null);
            }
        }

        private void Copy()
        {
            for(int i = 0; i < cells.Length;i++)
            {
                cells[i] = trackedPiece.cells[i];
            }
        }

        private void Drop()
        {
            Vector3Int position = trackedPiece.position;
            int currentRow = position.y;
            int bottom = - tetrisBoard.boardSize.y / 2 - 1;
            tetrisBoard.Clear(trackedPiece);
            for(int row = currentRow; row >= bottom; row--)
            {
                position.y = row;
                if (tetrisBoard.IsValidPosition(trackedPiece, position))
                {
                    this.position = position;
                }
                else
                {
                    break;
                }
            }
            tetrisBoard.Set(trackedPiece);
        }

        private void Set()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Vector3Int tilePosition = cells[i] + position;
                tilemap.SetTile(tilePosition, tile);
            }
        }
        #endregion
    }
}