using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    private Stack<Page> openPages;

    [SerializeField]
    private Page defaultPage;

    public void OpenPage(Page pageToOpen)
    {

    }
}
