using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public SpawnObjectData objectType = SpawnObjectData.Car;
    public int spawnDirMod = 1;
    public float moveSpeed;

    private void Update()
    {
        Vector2 pos = transform.localPosition;
        transform.localPosition = new Vector2(pos.x+(moveSpeed*spawnDirMod),pos.y);
        if(transform.position.x > 12 || transform.position.x < -12)
        {
            Destroy(gameObject);
        }
    }
}
