using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    private float bestTime = Mathf.Infinity;

    private void Start()
    {
        LoadHighscore();
    }

    public void SaveHighscore(float time)
    {
        if (time < bestTime)
        {
            bestTime = time;
            highscoreText.text = "Best Time: " + FormatTime(bestTime);
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighscore()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
            highscoreText.text = "Best Time: " + FormatTime(bestTime);
        }
        else
        {
            highscoreText.text = "Best Time: --:--:--";
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 100) % 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}