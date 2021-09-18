//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBottom : MonoBehaviour
{
    // Destroys the attached object when it reaches the specified y position. Should be on TileRow objects, NOT Tile objects.

    [SerializeField] private float destroyY = -7f; // Y position when object will be destroyed

    private void Update()
    {
        if(transform.position.y <= destroyY)
        {
            // NOTE: Get distance under coordinate of -7f and save it to a static offset variable in Generator
            Generator.yOffset = transform.position.y + 7f;
            Destroy(gameObject);
            Generator.generateRow = true;
        }
    }
}
