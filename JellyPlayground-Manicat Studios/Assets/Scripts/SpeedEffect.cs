using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _speedEffectObject;

    private void Start()
    {
        GameDataManager.isSpeedChanged += makeVisable;
    }

    private void makeVisable(bool isItVisable)
    {
        Debug.Log("ZOOM!!!!");
        _speedEffectObject.SetActive(isItVisable);
    }
}
