using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(UiController))]
public class TutorialMenuController : MonoBehaviour
{
    [SerializeField]
    private Page _mainMenu;
    [SerializeField]
    private UiController _uiController;

    // Start is called before the first frame update
    void Start()
    {
        _uiController = this.GetComponent<UiController>();
        InputHandler.instance.Escape += Back;
    }

    public void Back()
    {
        if (_uiController.IsStackEmpty())
        {
            InputHandler.instance.LockMouse(false);
            _uiController.PushPage(_mainMenu);
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

    public void StartGame()
    {
        _uiController.PopAllPages();  
        InputHandler.instance.LockMouse(true);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Tutorial()
    {
        Debug.Log("Tutorial video is supposed to be here");
    }
}
