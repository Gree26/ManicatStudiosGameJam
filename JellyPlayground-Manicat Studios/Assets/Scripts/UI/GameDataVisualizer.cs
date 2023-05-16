using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity;
using UnityEngine.UIElements;
//using UnityEngine.UI;

public class GameDataVisualizer : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _numbers;

    [SerializeField]
    private Image _secondsOnes;

    [SerializeField]
    private Image _secondsTens;

    [SerializeField]
    private Image _minOnes;

    [SerializeField]
    private Image _minTens;

    //[SerializeField]
    //private TMP_Text _time;

    [SerializeField]
    private TextMesh _checkpoint;

    [SerializeField]
    private TextMesh _lap;

    [SerializeField]
    private TextMesh _victoryText;

    private void Start()
    {
        GameDataManager.NewCheckpoint += NewCheckPoint;
        GameDataManager.NewLap += NewLap;
        GameDataManager.RaceFinish += GameOver;
    }

    private void Update()
    {
        //_time.text = "TIME: " + PlayerMoveController.CurrentTime;
    }

    private void NewCheckPoint(int checkpoint)
    {
        _checkpoint.text = "Checkpoint: " + checkpoint + "/7";
    }

    private void NewLap(int lap)
    {
        _lap.text += "Lap: " + lap + "/3";
    }

    private void GameOver()
    {
        _victoryText.text = "RACE COMPLETED, FINISHING TIME:\n " + PlayerMoveController.CurrentTime + "!";
    }
}
