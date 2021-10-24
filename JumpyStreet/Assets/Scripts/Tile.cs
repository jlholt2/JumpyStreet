//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum TileType { Walkable = 0, Water = 1, Wall = 2 }   
[System.Serializable]
public class Tile : MonoBehaviour
{
    public TileType TypeOfTile { get { return typeOfTile; } }

    [Header("Tile Variables")]
    [SerializeField] private TileType typeOfTile = 0;
    public Sprite tileSprite;

    public void SpawnFruit()
    {
        GameObject fruitGO = Instantiate(Generator.generator.fruitPrefab, transform);
        fruitGO.transform.localPosition = Vector2.zero;
        Fruit fruit = fruitGO.GetComponent<Fruit>();
        fruit.DetermineFruitType();
    }

    public void CreateTileFromData(TileData data)
    {
        typeOfTile = data.typeOfTile;
        tileSprite = data.tileSprite;
        if (typeOfTile == 0)
        {
            if (Random.Range(0, 25) >= 24)
            {
                SpawnFruit();
            }
        }
    }
}
