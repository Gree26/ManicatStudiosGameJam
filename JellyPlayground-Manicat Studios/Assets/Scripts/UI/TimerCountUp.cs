using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountUp : MonoBehaviour
{
    [SerializeField]
    private Image _secondsOnes;
    [SerializeField]
    private Image _secondsTens;

    [SerializeField]
    private Image _minsOnes;
    [SerializeField]
    private Image _minsTens;

    [SerializeField]
    private List<Sprite> numbers;

    private float startTime;
    private bool timerActive;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (timerActive)
        {
            float currentTime = Time.time - startTime;
            int minutes = (int)(currentTime / 60);
            int seconds = (int)(currentTime % 60);

            UpdateMins(minutes);
            UpdateSecs(seconds);
            

        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    private void UpdateMins(int num)
    {
        if (num / 10 < 10)
        {
            _minsTens.sprite = numbers[num / 10];
        }
        else
        {
            _minsTens.sprite = numbers[9];
        }
        _minsOnes.sprite = numbers[num % 10];
    }
    private void UpdateSecs(int num)
    {
        if (num / 10 < 10)
        {
            _secondsTens.sprite = numbers[num / 10];
        }
        else
        {
            _secondsTens.sprite = numbers[9];
        }
        _secondsOnes.sprite = numbers[num % 10];
    }
}
