using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Back()
    {
        if (_uiController.IsStackEmpty())
        {
            InputHandler.instance.LockMouse(false);
            _uiController.PushPage(_pauseMenu);
            Time.timeScale = 0;
        }
        else
        {
            _uiController.PopPage();
            if (_uiController.IsStackEmpty())
            {
                InputHandler.instance.LockMouse(true);
                Time.timeScale = 1;
            }
        }
    }

    public void OpenInfoPage()
    {
        _uiController.PushPage(_infoMenu);
    }

    public void ContinueGame()
    {
        _uiController.PopAllPages();
        InputHandler.instance.LockMouse(true);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
