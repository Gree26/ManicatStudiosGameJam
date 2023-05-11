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
}
