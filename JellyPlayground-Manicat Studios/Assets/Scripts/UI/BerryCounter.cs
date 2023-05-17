using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class BerryCounter : MonoBehaviour
{
    [SerializeField]
    private Image _firstNumber;

    [SerializeField]
    private Image _secondNumber;

    [SerializeField]
    private List<Sprite> _numbers;

    public void ChangeValues(int first, int second)
    {
        Debug.Log("Berries Collected: " + first + "\nTotal Berries: " + second);
        _firstNumber.sprite = _numbers[first];
        _secondNumber.sprite = _numbers[second];
        StartCoroutine(ShowBerryPopup(first, second));
    }

    private IEnumerator ShowBerryPopup(int numberCollceted, int numberLeft)
    {
        CanvasGroup _myCanvas = this.GetComponent<CanvasGroup>();

        _myCanvas.alpha = 1;

        while (_myCanvas.alpha > 0)
        {
            _myCanvas.alpha -= 0.025f;
            yield return new WaitForEndOfFrame();
        }
    }
}
