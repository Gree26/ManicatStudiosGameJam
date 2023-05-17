using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBerryTrig : MonoBehaviour
{
    public GameObject FinalBerry;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the object
            FinalBerry.SetActive(true);
        }
    }
}