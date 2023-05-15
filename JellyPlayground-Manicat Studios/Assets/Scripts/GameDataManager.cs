using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataManager
{
    private static bool _isSpeed = false;
    public static bool isSpeed
    {
        get
        {
            return _isSpeed;
        }
        set
        {
            bool newValue = isSpeed != value;
            _isSpeed = value;
            if(newValue)
                isSpeedChanged?.Invoke(_isSpeed);
        }
    }
    public static Action<bool> isSpeedChanged;

    private static int _currentCheckpoint = 0;

    public static int CurrentCheckpoint
    {
        get
        {
            return _currentCheckpoint;
        }
    }

    public static int TotalCheckpoints = 0;

    private static Action RaceFinish;

    private static Action<int> NewCheckpoint;

    private static Action<int, int> MissedCheckpoint;

    public static Action<int> NewLap;



    public static void RaceCompleted() => RaceFinish?.Invoke();
    public static void CheckpointReached(int currentCheckpoint) => NewCheckpoint?.Invoke(currentCheckpoint);
    public static void LapFinished(int currentLap) => NewLap?.Invoke(currentLap);
    public static void WrongCheckpoint(int currentCheckpoint, int missedCheckpoint) => MissedCheckpoint?.Invoke(currentCheckpoint, missedCheckpoint);



}
