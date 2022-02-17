using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _curtain;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (_curtain.alpha>0)
        {
            _curtain.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
        }
        
        gameObject.SetActive(false);
    }
}
