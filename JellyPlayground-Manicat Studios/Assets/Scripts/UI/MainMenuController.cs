using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(UiController))]
public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Page _mainMenu;
    [SerializeField]
    private UiController _uiController;
    private Page _creditScene;

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

    public void QuitGame()
    {
        Debug.Log("Come back soon!");
        Application.Quit();
    }

    public void CreditPage()
    {
        _uiController.PushPage(_creditScene);
    }
}
