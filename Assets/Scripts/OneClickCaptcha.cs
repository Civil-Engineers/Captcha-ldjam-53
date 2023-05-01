using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OneClickCaptcha : MonoBehaviour
{
    private UnityEngine.UI.Image captchaBase;
    public UnityEngine.UI.Image box;
    public UnityEngine.UI.Image check;

    private UnityEngine.UI.Toggle toggle;

    private static float fadeInTime = 0.3f;
    private static float fadeOutTime = 0.3f;


    // Start is called before the first frame update
    void Awake()
    {
        toggle = GetComponentInChildren<UnityEngine.UI.Toggle>();
        captchaBase = GetComponent<UnityEngine.UI.Image>();
        
        setImageAlpha(captchaBase, 0);
        setImageAlpha(box, 0);
        setImageAlpha(check, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable() {
        Tween showTween = fadeAll(1, fadeInTime);
        CaptchaManager.Instance.activateCaptcha();
        // showTween.OnComplete(
        //     () =>   CaptchaManager.Instance.activateCaptcha()
        // );
    }

    void OnDisable() {
        captchaBase.DOKill();
        box.DOKill();
        check.DOKill();
        toggle.isOn = false;
        toggle.interactable = true;
    }

    private void setImageAlpha(UnityEngine.UI.Image img, int alpha) {
        Color col = img.color;
        col.a = alpha;
        img.color = col;
    }

    Tween fadeAll(float val, float duration) {
        captchaBase.DOFade(val, duration).SetDelay(1);
        box.DOFade(val, duration).SetDelay(1);
        return check.DOFade(val, duration).SetDelay(1); 
    }

    void stopCaptcha() {
        toggle.interactable = false;
        Tween hideTween = fadeAll(0, fadeOutTime);
        CaptchaManager.Instance.deactivateCaptcha();
        hideTween.OnComplete (
            () => {
                this.gameObject.SetActive(false);
            }
        );
    }

    public void deactivateCaptcha() {
        if(toggle && toggle.isOn) {
            stopCaptcha();
        }
    }
}
