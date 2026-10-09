using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [Header("Grid Settings")]
    [SerializeField] private int gridWidth = 9;
    [SerializeField] private int gridHeight = 5;

    [Header("Grid Cell Settings")]
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private float gridCellSize = 1f;

    //Generates a HxW grid of cells
    //Used for placing allies on the board
    void GenerateGrid()
    {
        
        for(int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y++)
            {
                GameObject tile =Instantiate(gridCellPrefab, new Vector3(x * gridCellSize, y * gridCellSize, 0), Quaternion.identity, transform);
                tile.name = $"Tile_{x}_{y}";

                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                bool islight = (x + y) % 2 == 0;
                sr.color = new Color(islight ? 1f : 0.5f, islight ? 1f : 0.5f, islight ? 1f : 0.5f, 1f);
            }
        }        
    }

    // Start is called before the first frame update
    //Generates the grid at start of the game
    void Start()
    {
        GenerateGrid();
    }

}
