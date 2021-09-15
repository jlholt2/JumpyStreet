using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Walkable = 0, Water = 1, Wall = 2 }   
public class Tile : Movable
{
    public TileType TypeOfTile { get { return typeOfTile; } }
    public Sprite tileSprite;

    [Header("Tile Variables")]
    [SerializeField] private TileType typeOfTile = 0;

    public override void Awake()
    {
        OnActiveAwake();
    }
    public override void Update()
    {
        OnActiveUpdate();
    }
    public override void FixedUpdate()
    {
        OnActiveFixedUpdate();
    }
}
