using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeInOut : UIPopupBase
{
    public float _Alpha;
    private float _fadeTime = 1.5f;

    private Image _fadeImage;

    void Awake()
    {
        _fadeImage = GetComponent<Image>();
    }

    public void FadeIn(UnityAction func)
    {
        StartCoroutine(FadeInStart(func));
    }
    public void FadeIn(UnityAction<string> func, string str)
    {
        StartCoroutine(FadeInStart(func, str));
    }

    public void FadeOut(UnityAction func)
    {
        StartCoroutine(FadeOutStart(func));
    }
    public void FadeOut(UnityAction<string> func, string str)
    {
        StartCoroutine(FadeOutStart(func, str));
    }

    public IEnumerator FadeInStart(UnityAction func)
    {
        float accTime = 0;

        while (accTime < _fadeTime)
        {
            accTime += Time.deltaTime;

             _Alpha = (_fadeTime - accTime) / _fadeTime;

            _fadeImage.color = new Color(0, 0, 0, _Alpha);

            yield return null;
        }

        _fadeImage.color = new Color(0, 0, 0, 0);

        func.Invoke();
    }
    public IEnumerator FadeInStart(UnityAction<string> func, string str)
    {

        float accTime = 0;

        while (accTime < _fadeTime)
        {
            accTime += Time.deltaTime;

             _Alpha = (_fadeTime - accTime) / _fadeTime;

            _fadeImage.color = new Color(0, 0, 0, _Alpha);

            yield return null;
        }

        _fadeImage.color = new Color(0, 0, 0, 0);

        if (func != null)
            func(str);
    }

    public IEnumerator FadeOutStart(UnityAction func)
    {

        float accTime = 0;

        while (accTime < _fadeTime)
        {
            accTime += Time.deltaTime;

             _Alpha = 1 - (_fadeTime - accTime) / _fadeTime;

            _fadeImage.color = new Color(0, 0, 0, _Alpha);

            yield return null;
        }

        _fadeImage.color = new Color(0, 0, 0, 1);

        func.Invoke();
    }
    public IEnumerator FadeOutStart(UnityAction<string> func, string str)
    {
        float accTime = 0;

        while (accTime < _fadeTime)
        {
            accTime += Time.deltaTime;

             _Alpha = 1 - (_fadeTime - accTime) / _fadeTime;

            _fadeImage.color = new Color(0, 0, 0, _Alpha);

            yield return null;
        }

        _fadeImage.color = new Color(0, 0, 0, 1);

        if (func != null)
            func.Invoke(str);
    }

}
