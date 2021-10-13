//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ScrollHandler : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int SpeedupTimer;
    [SerializeField] private int SpeedupCooldown;
    [SerializeField] private int NumOfCycles;

    private void Awake()
    {
        Scrollable.scrollSpeed = 0f; Scrollable.speedupTimer = Scrollable.speedupCooldown; Scrollable.numOfCycles = 0;
    }

    void Update()
    {
        // If (!gameOver)
        if (Scrollable.should_scroll)
        {
            if (NumOfCycles < 30)
            {
                if (Scrollable.speedupTimer > 0)
                {
                    Scrollable.speedupTimer--;
                    Scrollable.speedupTimer--;
                    Scrollable.speedupTimer--;
                }
                else
                {
                    Scrollable.numOfCycles++;
                    Scrollable.speedupTimer = Scrollable.speedupCooldown + (Scrollable.speedupCooldown * ((Scrollable.numOfCycles * 2) / 5));
                    Scrollable.scrollSpeed += 0.002f;
                    //print("Current Scrollable.moveSpeed: " + Scrollable.moveSpeed);
                }
            }

            MoveSpeed = Scrollable.scrollSpeed;
            SpeedupTimer = Scrollable.speedupTimer;
            SpeedupCooldown = Scrollable.speedupCooldown;
            NumOfCycles = Scrollable.numOfCycles;
        }
        else
        {
            MoveSpeed = 0;
        }
    }
}
