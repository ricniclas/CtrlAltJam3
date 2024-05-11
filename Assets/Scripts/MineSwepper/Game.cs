using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CtrlAltJam3
{
    public class Game : MonoBehaviour, IInputReceiver, IMinigame
    {
        public int width = 16;
        public int height = 16;
        public bool firstTry;

        private Board board;
        private Cell[,] state;

        public int MineCount;
        public static Game Instance;
        public GameObject targetObject;
        private bool gameover;

        [SerializeField] private float inputHoldTime;
        private float currentInputHoldTime;
        private Vector2Int currentInput;
        private InputPackage inputPackage => new InputPackage(this);

        [SerializeField] private CursorMovement cursorMovement;


        private void Awake()
        {
            board = GetComponentInChildren<Board>();
            Instance = this;
            cursorMovement.gameManager = this;
        }

        private void Update()
        {
            if (currentInput != Vector2Int.zero)
                currentInputHoldTime += Time.deltaTime;

            if (currentInputHoldTime >= inputHoldTime)
            {
                currentInputHoldTime =  0;
                if(currentInput != Vector2Int.zero)
                {
                    cursorMovement.Move(currentInput);
                }
            }



        }

        private void Start()
        {
            NewGame();
        }

        private void NewGame()
        {
            state = new Cell[width, height];
            GenerateCells();
            CursorPosition();
            firstTry = true;



            board.Draw(state);
        }

        private void CursorPosition()
        {
            /*if (width > height)
            {
                targetObject.transform.position = new Vector3(width / ham, height / burguer);
            }
            else
            {
                targetObject.transform.position = new Vector3(width / ham, height / ham);
            }*/
        }

        public void FirstMove()
        {
            firstTry = false;
            GenerateMines();
            GenerateNumbers();
            board.Draw(state);
        }

        private void GenerateCells()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = new Cell();
                    cell.position = new Vector3Int(x, y, 0);
                    cell.type = Cell.Type.Empty;
                    state[x, y] = cell;
                }
            }
        }
        public Cell.Type CellType()
        {
            //Vector3 worldPosition = targetObject.transform.position;
            Vector3Int cellPosition = board.tilemap.WorldToCell(targetObject.transform.position);
            Cell cell = GetCell(cellPosition.x, cellPosition.y);

            //Debug.Log(cell.type);
            return cell.type;
        }
        public void GenerateMines()
        {
            //Vector3 worldPosition = targetObject.transform.position;
            Vector3Int cursorPosition = board.tilemap.WorldToCell(targetObject.transform.position);

            int mineLimit = width * height;

            if (MineCount > mineLimit)
            {
                MineCount = mineLimit - 1;
            }

            for (int i = 0; i < MineCount; i++)
            {
                int x = Random.Range(0, width);
                int y = Random.Range(0, height);
                while (state[x, y].type == Cell.Type.Mine || (x == cursorPosition.x && y == cursorPosition.y))
                {
                    x++;
                    if (x >= width)
                    {
                        x = 0;
                        y++;

                        if (y >= height)
                        {
                            y = 0;
                        }
                    }
                }

                state[x, y].type = Cell.Type.Mine;
                //state[x,y].revealed = true;
            }
        }

        public void GenerateNumbers()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = state[x, y];

                    if (cell.type == Cell.Type.Mine)
                    {
                        continue;
                    }

                    cell.number = CountMines(x, y);

                    if (cell.number > 0)
                    {
                        cell.type = Cell.Type.Number;
                    }

                    //cell.revealed = true;
                    state[x, y] = cell;
                }

            }
        }


        private int CountMines(int cellX, int cellY)
        {
            int count = 0;

            for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
            {
                for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
                {
                    if (adjacentX == 0 && adjacentY == 0)
                    {
                        continue;
                    }


                    int x = cellX + adjacentX;
                    int y = cellY + adjacentY;

                    /*if(x<0 || x>= width || y<0 || y >= height)
                    {
                        continue;
                    }*/

                    if (GetCell(x, y).type == Cell.Type.Mine)
                    {
                        count++;
                    }
                }
            }
            Debug.Log("count:" + count);
            return count;
        }

        public void Flag()
        {
            Vector3 worldPosition = targetObject.transform.position;
            Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
            Cell cell = GetCell(cellPosition.x, cellPosition.y);

            if (cell.type == Cell.Type.Invalid || cell.revealed)
            {
                return;
            }

            cell.flagged = !cell.flagged;
            state[cellPosition.x, cellPosition.y] = cell;
            board.Draw(state);
        }

        public void Reveal()
        {
            Vector3 worldPosition = targetObject.transform.position;
            Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
            Cell cell = GetCell(cellPosition.x, cellPosition.y);



            if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged)
            {

                return;
            }
            switch (cell.type)
            {
                case Cell.Type.Mine:
                    Explode(cell);
                    break;
                case Cell.Type.Empty:
                    Flood(cell);
                    CheckWinCondition();
                    break;
                default:
                    cell.revealed = true;
                    state[cellPosition.x, cellPosition.y] = cell;
                    break;
            }

            board.Draw(state);
        }

        public void CheckWinCondition()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = state[x, y];
                    if (cell.type != Cell.Type.Mine && !cell.revealed)
                    {
                        return;
                    }
                }


            }

            Debug.Log("Winner!");
            gameover = true;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Cell cell = state[x, y];

                    if (cell.type == Cell.Type.Mine)
                    {
                        cell.flagged = true;
                        state[x, y] = cell;
                    }
                }
            }
        }


        private void Flood(Cell cell)
        {
            if (cell.revealed) return;
            if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;

            cell.revealed = true;
            state[cell.position.x, cell.position.y] = cell;

            if (cell.type == Cell.Type.Empty)
            {
                Flood(GetCell(cell.position.x - 1, cell.position.y));
                Flood(GetCell(cell.position.x + 1, cell.position.y));
                Flood(GetCell(cell.position.x, cell.position.y - 1));
                Flood(GetCell(cell.position.x, cell.position.y + 1));
            }
        }

        private void Explode(Cell cell)
        {
            Debug.Log("Game Over!");
            gameover = true;

            cell.revealed = true;
            cell.exploded = true;

            state[cell.position.x, cell.position.y] = cell;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cell = state[x, y];

                    if (cell.type == Cell.Type.Mine)
                    {
                        cell.revealed = true;
                        state[x, y] = cell;
                    }


                }
            }

        }

        private Cell GetCell(int x, int y)
        {
            if (IsValid(x, y))
            {
                return state[x, y];
            }
            else
            {
                return new Cell();
            }
        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        #region InputReceiver Interface
        void IInputReceiver.Directions(Vector2 direction)
        {
            Vector2Int intDirection = Vector2Int.RoundToInt(direction);
            if (currentInput != intDirection)
            {
                Debug.Log("Tetris Input: " + intDirection);

                currentInput = intDirection;
                currentInputHoldTime = 0;
                if(intDirection != Vector2Int.zero)
                {
                    cursorMovement.Move(intDirection);
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
            cursorMovement.OnReveal();
        }

        void IInputReceiver.Cancel()
        {
            cursorMovement.OnFlag();
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

        }

        InputPackage IMinigame.GetInputPackage()
        {
            return inputPackage;
        }
        #endregion

        /*void OnFlag(InputValue value)
        {
            Debug.Log("foi");
            Flag();
        }*/
    }

}
