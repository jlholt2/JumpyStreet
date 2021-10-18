using UnityEngine;

/*
 * An object that uses this script is meant to hold all of the meta tiles used by the tile generator.
 */

public class MetaTileHolder : MonoBehaviour
{
    public MetaTile[] metaTiles;

    [HideInInspector] public TileRowData roadRow;
    [HideInInspector] public TileRowData waterRow;

    [SerializeField] private Sprite roadSprite;
    [SerializeField] private Sprite waterSprite;

    private void Awake()
    {
        for (int i = 0; i < roadRow.tiles.Length; i++)
        {
            roadRow.tiles[i].typeOfTile = TileType.Walkable;
            roadRow.tiles[i].tileSprite = roadSprite;
        }
        roadRow.spawnData = SpawnObjectData.Car;
<<<<<<< HEAD
        roadRow.moveSpeed = new float[] { 0.1f, 0.15f, 0.20f };
        roadRow.spawnRate = new int[] { 30, 40, 60 };
=======
<<<<<<< Updated upstream
        roadRow.moveSpeed = new float[] { 0.1f, 0.25f, 0.5f };
        roadRow.spawnRate = new int[] { 20, 30, 60 };
=======
        roadRow.moveSpeed = new float[] { 0.05f, 0.1f, 0.25f };
        roadRow.spawnRate = new int[] { 60, 40, 120, 80 };
>>>>>>> Stashed changes
>>>>>>> d661eb9 (moving logs implemented)

        for (int i = 0; i < waterRow.tiles.Length; i++)
        {
            waterRow.tiles[i].typeOfTile = TileType.Water;
            waterRow.tiles[i].tileSprite = waterSprite;
        }
        waterRow.spawnData = SpawnObjectData.Log;
        waterRow.moveSpeed = new float[] { 0.05f, 0.1f, 0.15f };
        waterRow.spawnRate = new int[] { 60, 40 };
    }
}
