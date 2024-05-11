using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace CtrlAltJam3
{
    public class TetrisBoard : MonoBehaviour, IMinigame, IInputReceiver
    {
        public TetrominoData[] tetrominoes;
        public Piece activePiece { get; private set; }
        public Tilemap tilemap { get; private set; }
        [SerializeField] private PreviewBoard previewBoard;

        public Vector3Int spawnPosition;
        public Vector2Int boardSize = new Vector2Int(10, 20);
        public int currentMaxHeight;
        public Queue<int> pieceStack = new Queue<int>();

        private InputPackage inputPackage => new InputPackage(this);


        [SerializeField] private float inputHoldTime;
        private float currentInputHoldTime;

        private Vector2Int currentInput = Vector2Int.zero;
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
            currentMaxHeight = 0;
            for (int i = 0; i < tetrominoes.Length; i++)
            {
                tetrominoes[i].Initialize();
            }
        }

        private void Start()
        {
            SpawnPiece();
        }

        private void Update()
        {
            if (currentInput != Vector2Int.zero)
                currentInputHoldTime += Time.deltaTime;
            
            if(currentInputHoldTime >= inputHoldTime)
            {
                currentInputHoldTime = currentInput == Vector2Int.down? inputHoldTime/1.5f : 0;

                if (currentInput == Vector2Int.down || currentInput == Vector2Int.left || currentInput == Vector2Int.right)
                {
                    activePiece.Move(currentInput);
                }
                else if(currentInput == Vector2Int.up)
                {
                    activePiece.HardDrop();
                }

            }
        }
        #endregion

        #region Public Methods

        public void SpawnPiece()
        {
            while(pieceStack.Count < 2)
            {
                pieceStack.Enqueue(Random.Range(0, tetrominoes.Length));
            }
            TetrominoData data = tetrominoes[pieceStack.Dequeue()];
            activePiece.Initialize(this,spawnPosition,data);

            if (IsValidPosition(activePiece, spawnPosition))
            {
                Set(activePiece);
                TetrominoData previewTetronimo = tetrominoes[pieceStack.Peek()];
                previewBoard.Set(previewTetronimo);
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
            currentMaxHeight = CalculateMaxHeight();
        }


        #endregion

        #region Private Methods

        private int CalculateMaxHeight()
        {
            RectInt bounds = boardBounds;
            int maxHeightIndex = bounds.yMin;
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                for (int row = bounds.yMax; row >= bounds.yMin; row--)
                {
                    Vector3Int position = new Vector3Int(col, row, 0);
                    if (tilemap.HasTile(position))
                    {
                        if (maxHeightIndex < row) 
                        {
                            maxHeightIndex = row;
                        }
                        break;
                    }
                }
            }
            return maxHeightIndex + (bounds.size.y/2) +1;
        }

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
            currentMaxHeight = 0;
        }

        #endregion

        #region Input Receiver Interface

        void IInputReceiver.Directions(Vector2 direction)
        {
            Vector2Int intDirection = Vector2Int.RoundToInt(direction);
            if(currentInput != intDirection)
            {
                Debug.Log("Tetris Input: " + intDirection);

                currentInput = intDirection;
                currentInputHoldTime = 0;
                if (intDirection == Vector2Int.down || intDirection == Vector2Int.left || intDirection == Vector2Int.right)
                {
                    activePiece.Move(intDirection);
                }
                else if (currentInput == Vector2Int.up)
                {
                    activePiece.HardDrop();
                }
            }

        }

        void IInputReceiver.Game1()
        {
        }

        void IInputReceiver.Game2()
        {
        }

        void IInputReceiver.Game3()
        {
        }

        void IInputReceiver.Game4()
        {
        }

        void IInputReceiver.Confirm()
        {
            activePiece.Rotate(1);
        }

        void IInputReceiver.Cancel()
        {
            activePiece.Rotate(-1);
        }

        InputPackage IInputReceiver.GetInputPackage()
        {
            return inputPackage;
        }

        #endregion

        #region Minigame Interface


        void IMinigame.PauseGame()
        {
        }

        void IMinigame.InvokeConsequence()
        {
        }

        void IMinigame.ReceiveConsequence()
        {
        }

        void IMinigame.ResetInputs()
        {
            currentInput = Vector2Int.zero; 
            currentInputHoldTime = 0;
        }

        InputPackage IMinigame.GetInputPackage()
        {
            return inputPackage;
        }
        #endregion
    }
}
