using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    public TileType sensedTileType;

    private void OnTriggerStay2D(Collider2D hit)
    {
        if (hit.tag == "Tile")
        {
            sensedTileType = hit.GetComponent<Tile>().TypeOfTile;
        }
    }
}
