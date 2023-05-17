using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UiController))]
public class MainMenuUiController : MonoBehaviour
{
    private UiController _uiController;

    [SerializeField]
    private Page _credits;

    private void Awake()
    {
        _uiController = this.GetComponent<UiController>();

    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("RaceTrack_Lvl_01", LoadSceneMode.Single);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void CreditsPressed()
    {
        _uiController.PushPage(_credits);
    }
}
