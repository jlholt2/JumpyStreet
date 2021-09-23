using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRow : Scrollable
{
    [SerializeField] private Tile[] tiles = new Tile[18]; // contains the objects of all of the tiles in the row. Should always have 18 tiles on a row, to fill the screen.
    [Header("Spawn Object Data")]
    [SerializeField] private SpawnObjectData spawnData;
    [SerializeField] private SpawnDir spawnDir;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int[] spawnRate; // Holds the possible rates at which objects can be spawned on the row. Randomly selected each time and object spawns. ("rate" refers to the cooldown in frames between each spawn)

    [SerializeField] private int spawnCooldown;

    private void Update()
    {
        if (spawnCooldown > 0)
        {
            spawnCooldown--;
        }
        else
        {
            SpawnObject();
        }
    }

    public override void FixedUpdate()
    {
        OnActiveFixedUpdate();
    }

    public void SetTilesInRow(TileData[] tileData)
    {
        for (int i = 0; i < tileData.Length; i++)
        {
            GameObject tileGO = new GameObject("Tile");
            Tile tile = tileGO.AddComponent(typeof(Tile)) as Tile;
            tile.CreateTileFromData(tileData[i]);
            SpriteRenderer tileSR = tileGO.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            BoxCollider2D tileColl = tileGO.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            tileColl.isTrigger = true;
            tileColl.size = Vector2.one;
            tile.transform.parent = transform;
            tileGO.tag = "Tile";
            tile.transform.localPosition = new Vector2(i,0);
            tileSR.sprite = tile.tileSprite;
            // NOTE: Need to set sorting layer for tileSR to "Tiles"
            tiles[i] = tile;
        }
    }

    public void AdjustAllTiles()
    {
        // get float value of y position of first TileRow in activeTileRows
        float firstTileX = tiles[0].transform.position.x;

        for (int i = 1; i < tiles.Length; i++)
        {
            tiles[i].transform.position = new Vector2(firstTileX + i, tiles[i].transform.position.y);
        }
    }

    public void SetSpawnData(TileRowData rowData)
    {
        spawnData = rowData.spawnData;
        spawnDir = rowData.spawnDir;
        if(spawnDir == SpawnDir.Random)
        {
            switch (Random.Range(0, 2))
            {
                case 1:
                    spawnDir = SpawnDir.Right;
                    break;
                case 2:
                    spawnDir = SpawnDir.Left;
                    break;
            }
        }
        moveSpeed = rowData.moveSpeed[Random.Range(0, rowData.moveSpeed.Length)];
        spawnRate = rowData.spawnRate;
        spawnCooldown = spawnRate[Random.Range(0, spawnRate.Length)];
    }

    private void SpawnObject()
    {
        GameObject spawnedObject = null;
        switch (spawnData)
        {
            default:
                break;
            case SpawnObjectData.Car:
                // set spawnedObject to Car prefab
                break;
            case SpawnObjectData.Log:
                // set spawnedObject to Log prefab
                break;
        }
        if (spawnedObject != null)
        {
            // Instantiate spawnedObject
            // Add MovingObject component to spawnedObject and set its parent to 'this'
            // Set spawnedObject.MovingObject.moveSpeed to moveSpeed
            spawnCooldown = spawnRate[Random.Range(0,spawnRate.Length)];
        }
    }
}
