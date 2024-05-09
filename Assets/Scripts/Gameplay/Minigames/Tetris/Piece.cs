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

        #region MonoBehaviour Callbacks

        private void Update()
        {
            lockTime += Time.deltaTime;
            if(Time.time >= stepTime)
            {
                Step();
            }

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

        public bool Move(Vector2Int translation)
        {
            tetrisBoard.Clear(this);

            Vector3Int newPosition = position;
            newPosition.x += translation.x;
            newPosition.y += translation.y;

            bool valid = tetrisBoard.IsValidPosition(this, newPosition);
            if (valid)
            {
                position = newPosition;
                lockTime = 0f;
                tetrisBoard.Set(this);

            }
            return valid;
        }

        public void Rotate(int direction)
        {
            tetrisBoard.Clear(this);
            int originalRotation = rotationIndex;
            rotationIndex = Wrap(rotationIndex + direction,0,4);
            ApplyRotationMatrix(direction);


            if (!TestWallKicks(rotationIndex,direction))
            {
                rotationIndex = originalRotation;
                ApplyRotationMatrix(-direction);
                tetrisBoard.Set(this);
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

        private int Wrap(int input, int min, int max)
        {
            if (input < min)
            {
                return max - (min - input) % (max - min);
            }
            else
            {
                return min + (input - min) % (max - min);
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

            return Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));
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

    }
}
