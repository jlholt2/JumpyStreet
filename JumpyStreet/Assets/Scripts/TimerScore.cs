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

    public IEnumerator AddScoreCount(int scoreToAdd)
    {
        for (int i = 0; i < scoreToAdd; i++)
        {
            AddScoreCount();
            for (int cooldown = 0; cooldown < 3; cooldown++)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
