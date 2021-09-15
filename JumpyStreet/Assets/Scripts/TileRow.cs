using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRow : Scrollable
{
    [SerializeField] private Tile[] tiles = new Tile[18]; // contains the objects of all of the tiles in the row. Should always have 18 tiles on a row, to fill the screen.

    //public override void Awake()
    //{
    //    OnActiveAwake();
    //}
    //public override void Update()
    //{
    //    OnActiveUpdate();
    //}
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
            Collider2D tileColl = tileGO.AddComponent(typeof(BoxCollider2D)) as Collider2D;
            tile.transform.parent = transform;
            tile.transform.localPosition = new Vector2(i,0);
            tileSR.sprite = tile.tileSprite;
            tiles[i] = tile;
        }
    }
}