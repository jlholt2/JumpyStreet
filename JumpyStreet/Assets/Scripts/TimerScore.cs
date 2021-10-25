using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScore : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    private int score = -15;
    private static int highScore = 0;
    public static TimerScore instance;
    [SerializeField] private AudioSource scoreBlipSound;

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
        if(highScoreText != null)
        {
            if (PlayerPrefs.HasKey("highScore"))
            {
                highScore = PlayerPrefs.GetInt("highScore");
            }
            else
            {
                PlayerPrefs.SetInt("highScore", highScore);
            }
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void AddScoreCount()
    {
        if(highScoreText == null)
        {
            score++;
            scoreBlipSound.Play();
            scoreText.text = score.ToString();
        }
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
    public void SaveHighScore()
    {
        if (score > highScore) highScore = score;
        PlayerPrefs.SetInt("highScore", highScore);
        PlayerPrefs.Save();
    }
}
