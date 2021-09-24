//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Scrollable : MonoBehaviour
{
    // Parent class to any objects that should incrementally move downward.

    public static float scrollSpeed = 0f;
    public static int speedupTimer = 0;
    public static int speedupCooldown = 60*10;
    public static int numOfCycles = 0;

    //public virtual void Awake()
    //{
    //    OnActiveAwake();
    //}

    //public void OnActiveAwake()
    //{

    //}

    //public virtual void Update()
    //{
    //    OnActiveUpdate();
    //}
    //public void OnActiveUpdate()
    //{

    //}

    public virtual void FixedUpdate()
    {
        OnActiveFixedUpdate();
    }
    public void OnActiveFixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, MathUtils.Round((transform.position.y - scrollSpeed),3));
    }
}
