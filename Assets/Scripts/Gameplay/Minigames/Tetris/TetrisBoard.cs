using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace CtrlAltJam3
{
    public class TetrisBoard : MonoBehaviour
    {
        public TetrominoData[] tetrominoes;
        public Piece activePiece { get; private set; }
        public Tilemap tilemap { get; private set; }
        public Vector3Int spawnPosition;
        public Vector2Int boardSize = new Vector2Int(10, 20);

        public RectInt boardBounds
        {
            get
            {
                Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
                return new RectInt(position, boardSize);
            }
        }

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            tilemap = GetComponentInChildren<Tilemap>();
            activePiece = GetComponentInChildren<Piece>();
            for (int i = 0; i < tetrominoes.Length; i++)
            {
                tetrominoes[i].Initialize();
            }
        }

        private void Start()
        {
            SpawnPiece();
        }
        #endregion

        #region Public Methods

        public void SpawnPiece()
        {
            int random = Random.Range(0, tetrominoes.Length);
            TetrominoData data = tetrominoes[random];
            activePiece.Initialize(this,spawnPosition,data);

            if (IsValidPosition(activePiece, spawnPosition))
            {
                Set(activePiece);
            }
            else
            {
                GameOver();
            }
        }


        public void Set(Piece piece)
        {
            for(int i = 0; i< piece.cells.Length; i++)
            {
                Vector3Int tilePosition = piece.cells[i] + piece.position;
                tilemap.SetTile(tilePosition, piece.data.tile);
            }
        }
        public void Clear(Piece piece)
        {
            for (int i = 0; i < piece.cells.Length; i++)
            {
                Vector3Int tilePosition = piece.cells[i] + piece.position;
                tilemap.SetTile(tilePosition, null);
            }
        }

        public bool IsValidPosition(Piece piece, Vector3Int position)
        {
            RectInt bounds = boardBounds;

            for (int i = 0; i < piece.cells.Length; i++)
            {
                Vector3Int tilePosition = piece.cells[i] + position;
                if (!bounds.Contains((Vector2Int)tilePosition))
                {
                    return false;
                }

                if (tilemap.HasTile(tilePosition))
                {
                    return false;
                }
            }

            return true;
        }

        public void ClearLines()
        {
            RectInt bounds = boardBounds;
            int row = bounds.yMin;

            while (row < bounds.yMax)
            {
                if (IsLineFull(row))
                {
                    LineClear(row);
                }
                else
                {
                    row++;
                }
            }
        }


        #endregion

        #region Private Methods
        private bool IsLineFull(int row)
        {
            RectInt bounds = boardBounds;

            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                if (!tilemap.HasTile(position))
                {
                    return false;
                }
            }

            return true;
        }

        private void LineClear(int row)
        {
            RectInt bounds = boardBounds;

            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, null);
            }

            while (row < bounds.yMax)
            {
                for (int col = bounds.xMin; col < bounds.xMax; col++)
                {
                    Vector3Int position = new Vector3Int(col, row + 1, 0);
                    TileBase above = tilemap.GetTile(position);
                    position = new Vector3Int(col, row, 0);
                    tilemap.SetTile(position, above);
                }
                row++;
            }
        }

        private void GameOver()
        {
            tilemap.ClearAllTiles();
        }
        #endregion
    }
}
