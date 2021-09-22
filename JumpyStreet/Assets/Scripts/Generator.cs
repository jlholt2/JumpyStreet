using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public MetaTile currentMetaTile;
    public int currentRow; // used to determine which row of the metatile should be generated. Counts up as a row is generated. When it reaches the current MetaTile's array length, should reset to 0 and set currentMetaTile to a different random MetaTile.

    private TileRow lastCreatedRow;
    private MetaTileHolder metaTileHolder;

    public static bool generateRow;
    public static float yOffset = 0f;

    [SerializeField] private float YOffset;

    private void Awake()
    {
        metaTileHolder = gameObject.GetComponent<MetaTileHolder>();
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        GenerateMetaTile();
        BeginningGeneration();
    }

    private void Update()
    {
        YOffset = yOffset;
        if (generateRow)
        {
            GenerateTileRow();
            generateRow = false;
        }
    }

    public void GenerateTileRow()// Generates rows of tiles in currently loaded metatile. Should be called when a TileRow object is destroyed.
    {
        // Create new TileRow from currentMetaTile.tileRows[currentRow]
        GameObject newRowGO = new GameObject("TileRow");
        newRowGO.transform.position = new Vector2(transform.position.x,transform.position.y+yOffset);
        TileRow newRow = newRowGO.AddComponent(typeof(TileRow)) as TileRow;
        DestroyOnBottom destroyRow = newRowGO.AddComponent(typeof(DestroyOnBottom)) as DestroyOnBottom;
        newRow.SetTilesInRow(currentMetaTile.tileRows[currentRow].tiles);
        lastCreatedRow = newRow;

        // Increment currentRow by 1
        currentRow++;

        // If currentRow is equal to currentMetaTile.tileRows.Length, set currentMetaTile to other random MetaTile and reset currentRow.
        if(currentRow >= currentMetaTile.tileRows.Length)
        {
            currentRow = 0;
            GenerateMetaTile();
        }
    }

    // Make function that runs at game start and creates 14 tile rows all the way from the top to the bottom of the screen
    public void BeginningGeneration()
    {
        // Randomly load a metaTile to currentMetaTile

        // Generate the tile rows and increment them down as needed
        List<TileRow> rows = new List<TileRow>();
        for (int i = 0; i < 14; i++)
        {
            GenerateTileRow();
            rows.Add(lastCreatedRow);
            for (int j = 0; j < rows.Count; j++)
            {
                rows[j].transform.localPosition = new Vector2(rows[j].transform.localPosition.x, rows[j].transform.localPosition.y-1);
            }
        }
    }

    private void GenerateMetaTile()
    {
        currentMetaTile = metaTileHolder.metaTiles[Random.Range(1,metaTileHolder.metaTiles.Length)];
    }
}
