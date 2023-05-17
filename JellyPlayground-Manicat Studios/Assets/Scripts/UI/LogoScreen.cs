using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoScreen : MonoBehaviour
{
    public RawImage logoImage;
    public float fadeDuration = 1.0f;
    public float displayDuration = 3.0f;
    public string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        // Set the logo image to fully transparent
        Color logoColor = logoImage.color;
        logoColor.a = 0f;
        logoImage.color = logoColor;

        // Start the fade-in coroutine
        StartCoroutine(FadeInLogo());
    }

    private IEnumerator FadeInLogo()
    {
        // Fade in the logo
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            Color logoColor = logoImage.color;
            logoColor.a = alpha;
            logoImage.color = logoColor;
            yield return null;
        }

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);

        // Start the fade-out coroutine
        StartCoroutine(FadeOutLogo());
    }

    private IEnumerator FadeOutLogo()
    {
        // Fade out the logo
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            Color logoColor = logoImage.color;
            logoColor.a = alpha;
            logoImage.color = logoColor;
            yield return null;
        }

        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}