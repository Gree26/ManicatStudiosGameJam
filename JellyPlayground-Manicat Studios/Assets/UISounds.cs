using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event clickEvent;
    [SerializeField] List<AK.Wwise.Event> sfxEvents;
    [SerializeField] AK.Wwise.RTPC sfxVolume;
    [SerializeField] AK.Wwise.RTPC musicVolume;

    private int musicCounter = 1;
    private int sfxCounter = 1;

    public void ClickSound() 
    {
        clickEvent.Post(this.gameObject);
    }


    public void toggleSound() 
    {
        if (sfxCounter % 2 == 0)
        {
            sfxVolume.SetGlobalValue(0.0f);
            
        }
        else 
        {
            sfxVolume.SetGlobalValue(1.0f);
        }
        
    }

    public void toggleMusic()
    {
        if (musicCounter % 2 == 0)
        {
            musicVolume.SetGlobalValue(0.0f);
        }
        else
        {
            
            musicVolume.SetGlobalValue(1.0f);
        }
    }

    public void sfxClick() 
    {
        sfxCounter++;
    }
    public void musicClick()
    {
        musicCounter++;
    }


}
