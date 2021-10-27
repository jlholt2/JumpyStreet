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

    public GameObject movingObjPrefab;

    private void Update()
    {
        if (spawnCooldown > 0)
        {
            spawnCooldown--;
        }
        else if(Scrollable.should_scroll)
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

    public void SetSpawnData(TileRowData rowData, float prevMoveSpeed, Generator gen)
    {
        spawnData = rowData.spawnData;
        if(rowData.spawnDir == SpawnDir.Random)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    spawnDir = SpawnDir.Right;
                    break;
                case 1:
                    spawnDir = SpawnDir.Left;
                    break;
            }
        }
        else
        {
            spawnDir = rowData.spawnDir;
        }
        //int cycles = 0;
        do
        {
            moveSpeed = rowData.moveSpeed[Random.Range(0, rowData.moveSpeed.Length)];
            //cycles++;
        } while (moveSpeed == prevMoveSpeed /*&& cycles < 20*/ && rowData.moveSpeed.Length > 1); // repeats the moveSpeed set until it does not match the moveSpeed of the previously generated row, to avoid impassable rivers. The Length check is to avoid a crash in case a row only has one possible move speed in its data
        Generator.generator.prevMoveSpeed = moveSpeed;
        spawnRate = rowData.spawnRate;
        spawnCooldown = spawnRate[Random.Range(0, spawnRate.Length)];
    }

    private void SpawnObject()
    {
        if (spawnData != SpawnObjectData.None)
        {
            // Instantiate spawnedObject
            GameObject spawnedObject = Instantiate(movingObjPrefab, transform.position, Quaternion.identity);
            MovingObject movingObj = spawnedObject.GetComponent<MovingObject>();
            int direction = 1;
            if(spawnDir == SpawnDir.Right)
            {
                direction = -1;
            }
            movingObj.transform.localScale = new Vector2(direction, 1f);
            SpriteRenderer movingSR = movingObj.GetComponent<SpriteRenderer>();
            switch (spawnData)
            {
                default:
                    Debug.Log("Assigned spawndata not accounted for. (" + spawnData + ")");
                    break;
                case SpawnObjectData.Car:
                    movingObj.objectType = SpawnObjectData.Car;
                    break;
                case SpawnObjectData.Log:
                    movingObj.objectType = SpawnObjectData.Log;
                    movingObj.GetComponent<SpriteRenderer>().sprite = Generator.generator.logSprite;
                    break;
            }
            // Make spawnedObject's parent this transform
            spawnedObject.transform.parent = transform;
            // Set movingObj.moveSpeed to moveSpeed
            movingObj.moveSpeed = moveSpeed;
            // Set movingObj.spawnDirMod according to spawnDir and set movingObj.transform.localPosition according to spawnDir (9.5,-9.5)
            switch (spawnDir)
            {
                case SpawnDir.Left:
                    movingObj.spawnDirMod = -1;
                    spawnedObject.transform.localPosition = new Vector2(19f, 0f);
                    break;
                case SpawnDir.Right:
                    movingObj.spawnDirMod = 1;
                    spawnedObject.transform.localPosition = new Vector2(-1f, 0);
                    spawnedObject.transform.localScale = new Vector2(-1f, 1f);
                    break;
                default:
                    Debug.Log("Uh oh! Something went wrong when spawning in a spawnable object ...");
                    break;
            }
            spawnCooldown = spawnRate[Random.Range(0,spawnRate.Length)];
        }
    }
}
