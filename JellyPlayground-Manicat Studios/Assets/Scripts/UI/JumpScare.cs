using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public void Boom()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        CanvasGroup cg = this.GetComponent<CanvasGroup>();

        cg.alpha = 1;

        while (cg.alpha>0)
        {
            yield return new WaitForEndOfFrame();
            cg.alpha -= 0.025f;
        }
        
    }
}
