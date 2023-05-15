using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UiController))]
public class IngameMenuController : MonoBehaviour
{
    [SerializeField]
    private Page _pauseMenu;
    [SerializeField]
    private Page _infoMenu;
    private UiController _uiController;

    // Start is called before the first frame update
    void Start()
    {
        _uiController = this.GetComponent<UiController>();
        InputHandler.instance.Escape += Back;
    }

    private void Back()
    {
        if (_uiController.IsStackEmpty())
        {
            InputHandler.instance.LockMouse(false);
            _uiController.PushPage(_pauseMenu);
        }
        else
        {
            _uiController.PopPage();
            if (_uiController.IsStackEmpty())
            {
                InputHandler.instance.LockMouse(true);
            }
        }
    }

    public void OpenInfoPage()
    {
        _uiController.PushPage(_infoMenu);
    }
}
