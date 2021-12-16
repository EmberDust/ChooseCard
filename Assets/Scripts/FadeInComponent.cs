using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class FadeInComponent : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] CanvasGroup _canvasGroup;
    [Header("Fade params")]
    [SerializeField] float _enabledAlpha = 1.0f;
    [SerializeField] float _disabledAlpha = 0.0f;
    [SerializeField] float _fadeDuration = 0.5f;

    public void FadeIn()
    {
        Fade(_enabledAlpha, true);
    }

    public void FadeOut()
    {
        Fade(_disabledAlpha, false);
    }

    public Task TaskFadeIn()
    {
        return Fade(_enabledAlpha, true);
    }

    public Task TaskFadeOut()
    {
        return Fade(_disabledAlpha, false);
    }

    private Task Fade(float finalAlpha, bool setCanvasActive)
    {
        CanvasGroupSetActive(setCanvasActive);
        return _canvasGroup.
            DOFade(finalAlpha, _fadeDuration).
            SetEase(Ease.InCubic).AsyncWaitForCompletion();
    }

    private void CanvasGroupSetActive(bool active)
    {
        _canvasGroup.interactable = active;
        _canvasGroup.blocksRaycasts = active;
    }

}
