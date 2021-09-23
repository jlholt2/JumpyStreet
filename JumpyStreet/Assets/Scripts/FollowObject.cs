using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;

    private void FixedUpdate()
    {
        transform.position = objectToFollow.transform.position;
    }
}
