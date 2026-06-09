using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenFadeManager : MonoBehaviour
{
    public float startAlpha = 1f;
    public float endAlpha = 0f;

    public float waitTime = 0f; //bekleme süresi
    public float fadeTime = 1f; //fade süresi 

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = startAlpha;
        StartCoroutine(FadeRoutineFNC());
    }


    IEnumerator FadeRoutineFNC() //routine fonksiyonu 
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<CanvasGroup>().DOFade(endValue:endAlpha,duration:fadeTime);
    }
}
