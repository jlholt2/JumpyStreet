using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBottom : MonoBehaviour
{
    [SerializeField] private float destroyY = -7f; // Y position when object will be destroyed

    private void Update()
    {
        if(transform.position.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }
}
