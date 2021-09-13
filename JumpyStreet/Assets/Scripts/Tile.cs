using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Walkable = 0, Water = 1 }
public class Tile : MonoBehaviour
{
    public TileType TypeOfTile { get { return typeOfTile; } }

    [SerializeField] private TileType typeOfTile = 0;
}
