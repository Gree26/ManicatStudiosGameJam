using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EntryMode
{
    NONE,
    SLIDE,
    ZOOM,
    FADE
}

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Page : MonoBehaviour
{
    private AudioSource _audioSource;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private float _animationSpeed = 1f;
    public bool ExitOnNewPagePush = false;

    [SerializeField]
    private AudioClip _entryClip;
    [SerializeField]
    private AudioClip _exitClip;
    [SerializeField]
    private EntryMode _entryMode = EntryMode.ZOOM;
    [SerializeField]
    private EntryMode _exitMode = EntryMode.ZOOM;
    [SerializeField]
    private Direction _entryDirection = Direction.UP;
    [SerializeField]
    private Direction _exitDirection = Direction.DOWN;

    private Coroutine _animationCoroutine;
    private Coroutine _audioCoroutine;


    private void Awake()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        _canvasGroup = this.GetComponent<CanvasGroup>();
        _audioSource = this.GetComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.spatialBlend = 0;
        _audioSource.enabled = false;
    }

    /// <summary>
    /// Called on entry to this page
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    public void Enter(bool playAudio)
    {
        switch (_entryMode)
        {
            case EntryMode.SLIDE:
                SlideIn(playAudio);
                break;
            case EntryMode.ZOOM:
                ZoomIn(playAudio);
                break;
            case EntryMode.FADE:
                FadeIn(playAudio);
                break;
        }
    }

    /// <summary>
    /// Called on entry to this page
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    public void Exit(bool playAudio)
    {
        switch (_entryMode)
        {
            case EntryMode.SLIDE:
                SlideOut(playAudio);
                break;
            case EntryMode.ZOOM:
                ZoomOut(playAudio);
                break;
            case EntryMode.FADE:
                FadeOut(playAudio);
                break;
        }
    }

    /// <summary>
    /// Play the fade animation
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    private void FadeIn(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimationHelper.FadeIn(_canvasGroup, _animationSpeed, null));

        PlayEntryClip(playAudio);
    }

    /// <summary>
    /// Play the fade animation
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    private void FadeOut(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimationHelper.FadeOut(_canvasGroup, _animationSpeed, null));

        PlayExitClip(playAudio);
    }

    /// <summary>
    /// Play the zoom animation
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    private void ZoomIn(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimationHelper.ZoomIn(_rectTransform, _animationSpeed, null));

        PlayEntryClip(playAudio);
    }

    /// <summary>
    /// Play the zoom animation
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    private void ZoomOut(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimationHelper.ZoomOut(_rectTransform, _animationSpeed, null));
        PlayExitClip(playAudio);
    }

    /// <summary>
    /// Play the slide animation. 
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    private void SlideIn(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimationHelper.SlideIn(_rectTransform, _entryDirection, _animationSpeed, null));

        PlayEntryClip(playAudio);
    }

    /// <summary>
    /// Play the slide animation. 
    /// </summary>
    /// <param name="playAudio">Should this Play audio?</param>
    private void SlideOut(bool playAudio)
    {
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(AnimationHelper.SlideOut(_rectTransform, _exitDirection, _animationSpeed, null));
        PlayExitClip(playAudio);
    }

    private void PlayEntryClip(bool playAudio)
    {
        if (_entryClip != null && _audioSource != null)
        {
            if (_audioCoroutine != null)
            {
                StopCoroutine(_audioCoroutine);
            }

            _audioCoroutine = StartCoroutine(PlayClip(_entryClip));
        }
    }

    private void PlayExitClip(bool playAudio)
    {
        if (_exitClip != null && _audioSource != null)
        {
            if (_audioCoroutine != null)
            {
                StopCoroutine(_audioCoroutine);
            }

            _audioCoroutine = StartCoroutine(PlayClip(_exitClip));
        }
    }

    private IEnumerator PlayClip(AudioClip clip)
    {
        _audioSource.enabled = true;

        WaitForSeconds wait = new WaitForSeconds(clip.length);

        _audioSource.PlayOneShot(clip);

        yield return wait;

        _audioSource.enabled = false;
    }


}
