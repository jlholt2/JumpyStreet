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

    //[SerializeField] private float YOffset;
    //[SerializeField] private float prevFrameMoveSpeed;

    private List<TileRow> activeTileRows;

    private void Awake()
    {
        activeTileRows = new List<TileRow>();
        metaTileHolder = gameObject.GetComponent<MetaTileHolder>();
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        BeginningGeneration();
        //GenerateMetaTile();
    }

    private void Update()
    {
        //YOffset = yOffset;
        if (generateRow)
        {
            GenerateTileRow();
            generateRow = false;
        }
        //if(prevFrameMoveSpeed != Scrollable.moveSpeed)
        //{
        //    Debug.Log("prevFrameMoveSpeed updated: " + (prevFrameMoveSpeed - Scrollable.moveSpeed) + " difference.");
        //}
        //prevFrameMoveSpeed = Scrollable.moveSpeed;
    }
    private void FixedUpdate()
    {
        AdjustAllRows();
    }

    public void GenerateTileRow()// Generates rows of tiles in currently loaded metatile. Should be called when a TileRow object is destroyed.
    {
        // Create new TileRow from currentMetaTile.tileRows[currentRow]
        GameObject newRowGO = new GameObject("TileRow");
        //newRowGO.transform.position = new Vector2(transform.position.x,transform.position.y+yOffset+(prevFrameMoveSpeed-Scrollable.moveSpeed));
        newRowGO.transform.position = new Vector2(transform.position.x,transform.position.y);
        TileRow newRow = newRowGO.AddComponent(typeof(TileRow)) as TileRow;
        DestroyOnBottom destroyRow = newRowGO.AddComponent(typeof(DestroyOnBottom)) as DestroyOnBottom;
        newRow.SetTilesInRow(currentMetaTile.tileRows[currentRow].tiles);
        newRow.SetSpawnData(currentMetaTile.tileRows[currentRow]);
        lastCreatedRow = newRow;
        AddTileRowToList(newRow);
        newRow.AdjustAllTiles();

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
        AdjustAllRows();
    }

    private void GenerateMetaTile()
    {
        int randValue = Random.Range(0, metaTileHolder.metaTiles.Length);
        Debug.Log("Loading MetaTile at value "+randValue+".");
        currentMetaTile = metaTileHolder.metaTiles[randValue];
    }

    private void AddTileRowToList(TileRow rowToAdd)
    {
        activeTileRows.Add(rowToAdd);
    }

    private void AdjustAllRows()
    {
        RowExistCheck();

        // get float value of y position of first TileRow in activeTileRows
        float firstRowY = activeTileRows[0].transform.position.y;

        for (int i = 1; i < activeTileRows.Count; i++)
        {
            activeTileRows[i].transform.position = new Vector2(activeTileRows[i].transform.position.x,firstRowY+i);
        }
    }

    private void RowExistCheck()
    {
        List<TileRow> tempRowList = new List<TileRow>();
        bool nullRow = false;

        // This checks that all values of activeTileRows are not null.
        for (int i = 0; i < activeTileRows.Count; i++)
        {
            if(activeTileRows[i] != null)
            {
                tempRowList.Add(activeTileRows[i]);
            }
            else
            {
                nullRow = true;
            }
        }

        if (nullRow)
        {
            Debug.Log("Active Tile Row List updated.");
            activeTileRows = tempRowList;
        }
    }
}
