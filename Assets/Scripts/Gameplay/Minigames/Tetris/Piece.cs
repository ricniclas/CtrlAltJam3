using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace CtrlAltJam3
{
    public class Piece : MonoBehaviour
    {
        public TetrisBoard tetrisBoard { get; private set; }
        public TetrominoData data { get; private set; }
        public Vector3Int[] cells { get; private set; }
        public Vector3Int position { get; private set; }
        public int rotationIndex { get; private set; }

        public float stepDelay = 1f;
        public float lockDelay = 0.5f;

        private float stepTime;
        private float lockTime;

        private Queue<Vector2Int> directionInputs = new Queue<Vector2Int>();
        private Queue<int> rotationInputs = new Queue<int>();


        #region MonoBehaviour Callbacks

        private void Update()
        {
            lockTime += Time.deltaTime;
            tetrisBoard.Clear(this);

            if (directionInputs.Count > 0)
            {
                Vector2Int direction = directionInputs.Dequeue();
                if (direction == Vector2Int.up)
                    HardDrop();
                else
                    Move(direction);
            }
            if (rotationInputs.Count > 0)
            {
                Rotate(rotationInputs.Dequeue());
            }
            if (Time.time >= stepTime)
            {
                Step();
            }
            tetrisBoard.Set(this);

        }

        #endregion

        #region Public Methods
        public void Initialize(TetrisBoard tetrisboard, Vector3Int position, TetrominoData data)
        {
            this.tetrisBoard = tetrisboard;
            this.position = position;
            this.data = data;
            rotationIndex = 0;
            stepTime = Time.time + stepDelay;
            lockTime = 0f;


            if (cells == null)
            {
                cells = new Vector3Int[data.cells.Length];
            }

            for (int i = 0; i < data.cells.Length; i++)
            {
                cells[i] = (Vector3Int)data.cells[i];
            }
        }

        #endregion

        #region Private Methods

        private bool Move(Vector2Int translation)
        {
            Vector3Int newPosition = position;
            newPosition.x += translation.x;
            newPosition.y += translation.y;

            bool valid = tetrisBoard.IsValidPosition(this, newPosition);
            if (valid)
            {
                position = newPosition;
                lockTime = 0f;
                if(translation.y != 0)
                {
                    stepTime = Time.time + stepDelay;
                }
            }
            return valid;
        }

        private void Rotate(int direction)
        {
            int originalRotation = rotationIndex;
            rotationIndex = MathUtils.Wrap(rotationIndex + direction,0,4);
            ApplyRotationMatrix(direction);


            if (!TestWallKicks(rotationIndex,direction))
            {
                rotationIndex = originalRotation;
                ApplyRotationMatrix(-direction);
            }
        }

        private void ApplyRotationMatrix(int direction)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Vector3 cell = cells[i];
                int x, y;
                switch (data.tetromino)
                {
                    case Tetromino.I:
                    case Tetromino.O:
                        cell.x -= 0.5f;
                        cell.y -= 0.5f;
                        x = Mathf.CeilToInt((cell.x * TetrisData.RotationMatrix[0] * direction) + (cell.y * TetrisData.RotationMatrix[1] * direction));
                        y = Mathf.CeilToInt((cell.x * TetrisData.RotationMatrix[2] * direction) + (cell.y * TetrisData.RotationMatrix[3] * direction));
                        break;
                    default:
                        x = Mathf.RoundToInt((cell.x * TetrisData.RotationMatrix[0] * direction) + (cell.y * TetrisData.RotationMatrix[1] * direction));
                        y = Mathf.RoundToInt((cell.x * TetrisData.RotationMatrix[2] * direction) + (cell.y * TetrisData.RotationMatrix[3] * direction));
                        break;
                }
                cells[i] = new Vector3Int(x, y, 0);
            }
        }

        public void HardDrop()
        {
            while(Move(Vector2Int.down))
            {
                continue;
            }
            Lock();
        }

        private bool TestWallKicks(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

            for(int i = 0; i < data.wallKicks.GetLength(1); i++)
            {
                Vector2Int translation = data.wallKicks[wallKickIndex, i];
                if(Move(translation))
                {
                    return true;
                }
            }
            return false;
        }

        private int GetWallKickIndex(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = rotationIndex * 2;
            if(rotationDirection < 0)
            {
                wallKickIndex--;
            }

            return MathUtils.Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));
        }

        private void Step()
        {
            stepTime = Time.time + stepDelay;
            Move(Vector2Int.down);

            if(lockTime >= lockDelay)
            {
                Lock();
            }
        }

        private void Lock()
        {
            tetrisBoard.Set(this);
            tetrisBoard.ClearLines();
            tetrisBoard.SpawnPiece();
        }
        #endregion

        #region Public Methods
        public void QueueMovement(Vector2Int movement)
        {
            directionInputs.Enqueue(movement);
        }

        public void QueueRotation(int direction)
        {
            rotationInputs.Enqueue(direction);
        }
        #endregion

    }
}
