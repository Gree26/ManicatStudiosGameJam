using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerState : MonoBehaviour
{
    [SerializeField] public AK.Wwise.State currentState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState.SetValue();
        }
    }
}
