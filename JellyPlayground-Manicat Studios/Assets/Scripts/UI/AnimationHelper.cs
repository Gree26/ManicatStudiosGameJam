using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHelper : MonoBehaviour
{
    /// <summary>
    /// Slide enter animation handler
    /// </summary>
    /// <param name="transform">The object bieng moved's transform.</param>
    /// <param name="direction">Direction of the slide</param>
    /// <param name="Speed">Speed of the transition</param>
    /// <param name="OnFinish">Called when the animation is completed.</param>
    /// <returns></returns>
    public static IEnumerator SlideIn(RectTransform transform, Direction direction, float Speed, UnityEvent? OnFinish)
    {
        Vector2 startPos = Vector2.zero;

        switch (direction)
        {
            case Direction.UP:
                startPos = new Vector2(0, -Screen.height);
                break;
            case Direction.DOWN:
                startPos = new Vector2(0, Screen.height);
                break;
            case Direction.LEFT:
                startPos = new Vector2(Screen.width, 0);
                break;
            case Direction.RIGHT:
                startPos = new Vector2(-Screen.width, 0);
                break;
        }

        float time = 0;
        while (time < 1)
        {
            transform.anchoredPosition = Vector2.Lerp(startPos, Vector2.zero, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        transform.anchoredPosition = Vector2.zero;
        OnFinish?.Invoke();
    }

    /// <summary>
    /// Slide exit animation handler
    /// </summary>
    /// <param name="transform">The object bieng moved's transform.</param>
    /// <param name="direction">Direction of the slide</param>
    /// <param name="Speed">Speed of the transition</param>
    /// <param name="OnFinish">Called when the animation is completed.</param>
    /// <returns></returns>
    public static IEnumerator SlideOut(RectTransform transform, Direction direction, float Speed, UnityEvent? OnFinish)
    {
        Vector2 endPos = Vector2.zero;

        switch (direction)
        {
            case Direction.UP:
                endPos = new Vector2(0, -Screen.height);
                break;
            case Direction.DOWN:
                endPos = new Vector2(0, Screen.height);
                break;
            case Direction.LEFT:
                endPos = new Vector2(Screen.width, 0);
                break;
            case Direction.RIGHT:
                endPos = new Vector2(-Screen.width, 0);
                break;
        }

        float time = 0;
        while (time < 1)
        {
            transform.anchoredPosition = Vector2.Lerp(Vector2.zero, endPos, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        transform.anchoredPosition = endPos;
        OnFinish?.Invoke();
    }

    /// <summary>
    /// Fade animation handler.
    /// </summary>
    /// <param name="canvasGroup">The canvas group to fade in/out.</param>
    /// <param name="Speed">The speed of the transition.</param>
    /// <param name="OnFinish">Called when the animation is completed.</param>
    /// <returns></returns>
    public static IEnumerator FadeIn(CanvasGroup canvasGroup, float Speed, UnityEvent? OnFinish)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        float time = 0;
        while (time < 1)
        {
            //Changes whether we are fading in or out
            canvasGroup.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        canvasGroup.alpha = 1;
        OnFinish?.Invoke();
    }

    /// <summary>
    /// Fade animation handler.
    /// </summary>
    /// <param name="canvasGroup">The canvas group to fade in/out.</param>
    /// <param name="Speed">The speed of the transition.</param>
    /// <param name="OnFinish">Called when the animation is completed.</param>
    /// <returns></returns>
    public static IEnumerator FadeOut(CanvasGroup canvasGroup, float Speed, UnityEvent? OnFinish)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        float time = 0;
        while (time < 1)
        {
            //Changes whether we are fading in or out
            canvasGroup.alpha = Mathf.Lerp(1, 0, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        canvasGroup.alpha = 0;
        OnFinish?.Invoke();
    }

    /// <summary>
    /// Zoom enter handler.
    /// </summary>
    /// <param name="transform">The transform of the object to be zoomed on.</param>
    /// <param name="Speed">Speed of the zoome.</param>
    /// <param name="OnFinish">UnityEvent to be called when completed.</param>
    /// <returns></returns>
    public static IEnumerator ZoomIn(RectTransform transform, float Speed, UnityEvent? OnFinish)
    {
        float time = 0;
        while (time < 1)
        {
            //Changes whether we are fading in or out
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        transform.localScale = Vector3.one;
        OnFinish?.Invoke();
    }

    /// <summary>
    /// Zoom enter handler.
    /// </summary>
    /// <param name="transform">The transform of the object to be zoomed on.</param>
    /// <param name="Speed">Speed of the zoome.</param>
    /// <param name="OnFinish">UnityEvent to be called when completed.</param>
    /// <returns></returns>
    public static IEnumerator ZoomOut(RectTransform transform, float Speed, UnityEvent? OnFinish)
    {
        float time = 0;
        while (time < 1)
        {
            //Changes whether we are fading in or out
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        transform.localScale = Vector3.zero;
        OnFinish?.Invoke();
    }
}
