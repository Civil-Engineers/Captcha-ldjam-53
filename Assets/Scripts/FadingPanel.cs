using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadingPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private Tween fadeTween;

    // Start is called before the first frame update
    void Start()
    {
        TestFade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn(float duration) {
        Fade(1f, duration, () =>
        {
           canvasGroup.interactable = true;
           canvasGroup.blocksRaycasts = true; 
        });
    }

    public void FadeOut(float duration) {
        Fade(0f, duration, () =>
        {
           canvasGroup.interactable = false;
           canvasGroup.blocksRaycasts = false; 
        });
    }

    private IEnumerator TestFade() {
        yield return new WaitForSeconds(2f);
        FadeOut(1f);
        yield return new WaitForSeconds(3f);
        FadeIn(1f);

    }

    private void Fade(float endValue, float duration, TweenCallback onEnd) {
        if (fadeTween != null) {
            //another tween is in process and we need to terminate it
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}
