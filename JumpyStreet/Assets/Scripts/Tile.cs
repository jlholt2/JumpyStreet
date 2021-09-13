using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Walkable = 0, Water = 1, Wall = 2 }   
public class Tile : Movable
{
    public TileType TypeOfTile { get { return typeOfTile; } }

    [SerializeField] private TileType typeOfTile = 0;
    private Transform mainCamPos;

    public override void Awake()
    {
        OnActiveAwake();
        mainCamPos = Camera.main.transform;
    }
    public override void Update()
    {
        OnActiveUpdate();
        if (mainCamPos.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }
    public override void FixedUpdate()
    {
        OnActiveFixedUpdate();
    }
}
