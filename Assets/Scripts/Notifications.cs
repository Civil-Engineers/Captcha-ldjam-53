using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Notifications : MonoBehaviour
{
    public static Notifications Instance { get; private set; }

    private UnityEngine.UI.Image wares;
    private UnityEngine.UI.Image hey;
    public GameObject NewWares;
    public GameObject Hey;

    void Awaken() {
        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
            return;
        }
        Instance = this;

        wares = NewWares.gameObject.GetComponent<UnityEngine.UI.Image>();
        hey = Hey.gameObject.GetComponent<UnityEngine.UI.Image>();
        setImageAlpha(wares, 0);
        setImageAlpha(hey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setImageAlpha(UnityEngine.UI.Image img, int alpha) {
        Color col = img.color;
        col.a = alpha;
        img.color = col;
    }

    public void notifyHey() {
        Hey.SetActive(true);
        Tween showFade = hey.DOFade(1, 0.5f);
        // Tween hideFade = hey.DOFade(0, 0.2f).SetDelay(0.3f);
        showFade.OnComplete(
            () => {
                hey.DOKill();
            }
        );
    }
}
