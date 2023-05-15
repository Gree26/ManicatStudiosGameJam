using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public void OpenPage()
    {
        this.gameObject.active = true;
    }

    public void ClosePage()
    {
        this.gameObject.active = false;
    }
}
