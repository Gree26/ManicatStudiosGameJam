using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(UiController))]
public class IngameMenuController : MonoBehaviour
{
    [SerializeField]
    private Page _pauseMenu;
    [SerializeField]
    private Page _infoMenu;
    private UiController _uiController;

    [SerializeField]
    private Image _countdownImage;

    [SerializeField]
    private List<Sprite> _countdownSprites;

    [HideInInspector]
    public AK.Wwise.Event accelerationEvent;

    [SerializeField]
    private List<AK.Wwise.Event> countdownEvents;
    [SerializeField] private AK.Wwise.Event stopMusicEvent;
    [SerializeField] private AK.Wwise.Event playMusicEvent;

    // Start is called before the first frame update
    void Start()
    {
        _uiController = this.GetComponent<UiController>();
        InputHandler.instance.Escape += Back;

        accelerationEvent = GameObject.Find("Jelly").GetComponent<PlayerMoveController>().accelerationEvent;
        StartCoroutine(Countdown());
    }

    public void Back()
    {
        if (_uiController.IsStackEmpty())
        {
            InputHandler.instance.LockMouse(false);
            _uiController.PushPage(_pauseMenu);
            Time.timeScale = 0;
            //Enter
            AkSoundEngine.SetState("MenuState","MenuPause");
            accelerationEvent.Stop(GameObject.Find("Jelly"));
            
            

        }
        else
        {
            _uiController.PopPage();
            if (_uiController.IsStackEmpty())
            {
                InputHandler.instance.LockMouse(true);
                Time.timeScale = 1;
                AkSoundEngine.SetState("MenuState", "Gameplay");
                accelerationEvent.Post(GameObject.Find("Jelly"));

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
        AkSoundEngine.SetState("MenuState", "Gameplay");
        accelerationEvent.Post(GameObject.Find("Jelly"));
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        AkSoundEngine.StopAll();
        
    }

    private IEnumerator Countdown()
    {
        accelerationEvent.Stop(GameObject.Find("Jelly"));
        stopMusicEvent.Post(this.gameObject);
        Time.timeScale = 0;
        int pos = 0;
        while (pos < _countdownSprites.Count)
        {
            countdownEvents[pos].Post(this.gameObject);
            _countdownImage.sprite = _countdownSprites[pos];
            pos++;
            yield return new WaitForSecondsRealtime(1);
        }
        _countdownImage.gameObject.SetActive(false);
        countdownEvents[3].Post(this.gameObject);
        playMusicEvent.Post(this.gameObject);
        accelerationEvent.Post(GameObject.Find("Jelly"));
        Time.timeScale = 1;
    }

}
