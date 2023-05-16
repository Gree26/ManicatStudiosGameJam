using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event clickEvent;
    [SerializeField] List<AK.Wwise.Event> sfxEvents;
    
    public void ClickSound() 
    {
        clickEvent.Post(this.gameObject);
    }

}
