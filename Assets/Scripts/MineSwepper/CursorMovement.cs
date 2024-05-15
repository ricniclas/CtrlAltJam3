using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CursorMovement : MonoBehaviour
{
    [SerializeField]
    private Tilemap tile;
    [SerializeField]
    private Tilemap collisionTilemap;

    private PlayerInputs controls;
    public bool teste;
    public float cellSize;

    [SerializeField]
    private Game gameManager;
   

    private void Update()
    {


       



    }
    private void Awake()
    {
        controls = new PlayerInputs();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Main.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        gameManager = GetComponentInParent<Game>();
        gameManager.CellType();
        //firstTry=true;  
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction * cellSize;
            gameManager.CellType();
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = tile.WorldToCell(transform.position + (Vector3)direction * cellSize);
        if (!tile.HasTile(gridPosition)/*||collisionTilemap.HasTile(gridPosition)*/)
        {
           return false;
        }
        return true;
    }

    void OnFlag(InputValue value)
    {
        Debug.Log("foi");
        gameManager.Flag();
    }

    void OnReveal(InputValue value)
    {
        Debug.Log("foi");

        if (gameManager.firstTry)
        {
            gameManager.FirstMove();
            //gameManager.firstTry = false;
        }
        gameManager.Reveal();


        //gameManager.GenerateMines();
        //gameManager.GenerateMines();
    }

}
