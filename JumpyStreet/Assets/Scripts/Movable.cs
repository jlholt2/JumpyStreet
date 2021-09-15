using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    // Parent class to any objects that should incrementally move downward.

    public static float moveSpeed = 0f;
    public static int speedupTimer = 0;
    public static int speedupCooldown = 60*10;
    public static int numOfCycles = 0;

    [Header("Read Only Variables")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int SpeedupTimer;
    [SerializeField] private int SpeedupCooldown;
    [SerializeField] private int NumOfCycles;

    public virtual void Awake()
    {
        OnActiveAwake();
    }

    public void OnActiveAwake()
    {
        moveSpeed = 0f; speedupTimer = speedupCooldown; numOfCycles = 0;
    }

    public virtual void Update()
    {
        OnActiveUpdate();
    }
    public void OnActiveUpdate()
    {
        // If (!gameOver)
        if(speedupTimer > 0)
        {
            speedupTimer--;
        }
        else
        {
            numOfCycles++;
            speedupTimer = speedupCooldown + (speedupCooldown * ((numOfCycles*2) / 5));
            moveSpeed += 0.002f;
            //print("Current moveSpeed: " + moveSpeed);
        }
        MoveSpeed = moveSpeed;
        SpeedupTimer = speedupTimer;
        SpeedupCooldown = speedupCooldown;
        NumOfCycles = numOfCycles;
    }

    public virtual void FixedUpdate()
    {
        OnActiveFixedUpdate();
    }
    public void OnActiveFixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, MathUtils.Round((transform.position.y - moveSpeed),3));
    }
}
