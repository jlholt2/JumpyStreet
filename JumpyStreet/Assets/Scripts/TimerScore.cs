using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScore : MonoBehaviour
{
    public Text scoreText;
    private int score = -15;
    public static TimerScore instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddScoreCount()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
