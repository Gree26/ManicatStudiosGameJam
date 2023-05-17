using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarUIHandler : MonoBehaviour
{
    [SerializeField]
    private List<Image> _stars;

    [SerializeField]
    private Sprite _noStar;

    [SerializeField]
    private Sprite _yesStar;

    public void UpdateStars(int totalStars)
    {
        foreach(Image i in _stars)
        {
            i.sprite = _noStar;
        }

        for(int i = 0; i < totalStars; i++)
        {
            _stars[i].sprite = _yesStar;
        }
    }
}
