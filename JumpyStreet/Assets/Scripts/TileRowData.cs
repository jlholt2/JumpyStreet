//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum SpawnObjectData { None = 0, Car = 1, Log = 2}
public enum SpawnDir { Random = 0, Right = 1, Left = 2 }

[System.Serializable]
public class TileRowData
{
    public TileData[] tiles = new TileData[18]; // contains the tiles for the rows. Must always be size 18.
    [Header("Spawn Object Data")]
    public SpawnObjectData spawnData = SpawnObjectData.None;
    public SpawnDir spawnDir = 0; 
    public float[] moveSpeed = { 0 }; // Holds the possible values the spawned object can move at on the row. Set on generation from one of the values in the array.
    public int[] spawnRate = { 60 }; // Holds the possible rates at which objects can be spawned on the row. Randomly selected each time and object spawns. ("rate" refers to the cooldown in frames between each spawn)
}
