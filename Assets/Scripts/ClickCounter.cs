using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class ClickCounter : MonoBehaviour {
    public static ClickCounter Instance { get; private set; }

    public int clickCount = 0;
    public int totalClickCount = 0;

    public TMPro.TextMeshProUGUI counter;
    public GameObject ClickCaptcha;

    private static int maxClicks = 99999;
    private static int maxDigits = 5;

    private bool heyBool = false; 

    private UnityEngine.UI.Image wares;
    private UnityEngine.UI.Image hey;
    public GameObject NewWares;
    public GameObject Hey;

    void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
        return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        wares = NewWares.gameObject.GetComponent<UnityEngine.UI.Image>();
        hey = Hey.gameObject.GetComponent<UnityEngine.UI.Image>();
        setImageAlpha(wares, 0);
        setImageAlpha(hey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        int numDigits;
        if (clickCount <= 9) {
            numDigits = 1;
        } else if (clickCount <= 99) {
            numDigits = 2;
        } else if (clickCount <= 999) {
            numDigits = 3;
        } else if (clickCount <= 9999) {
            numDigits = 4;
        } else {
            numDigits = maxDigits;
        }

        string zeroes = new System.String('0', maxDigits-numDigits);
        counter.text =  zeroes + clickCount;


    }

    bool validClick() {
        return (CaptchaManager.Instance.getNumCaptchas() <= 0 && clickCount <= maxClicks);
    }

    public void addClick() {
        if (validClick()) {
            clickCount++;
            totalClickCount++;

            

            CaptchaManager.Instance.manageDifficulty();
        
        }
    }

    public void addPlayerClick() {
        if (validClick()) {
            clickCount += 1 + UpgradesWindow.Instance.getCursorLevel();
            totalClickCount += 1 + UpgradesWindow.Instance.getCursorLevel();
            if (!heyBool && clickCount >= 10) {
                notifyHey();
                heyBool = true;
              }
        }
    }

    public void addNumClicks(int num) {
        if (validClick()) {
            clickCount+=num;
            totalClickCount+=num;
        }
    }

    public int getNumClicks() {
        return clickCount;
    }

    public int getTotalNumClicks() {
        return totalClickCount;
    }

    public void subtractClicks(int num) {
        clickCount-=num;
    }

    public void notifyHey() {
        Hey.SetActive(true);
        Tween showFade = hey.DOFade(1, 0.5f);
        Tween hideFade = hey.DOFade(0, 0.2f).SetDelay(2f);
        hideFade.OnComplete(
            () => {
                Hey.SetActive(false);
                hey.DOKill();
            }
        );
    }

        private void setImageAlpha(UnityEngine.UI.Image img, int alpha) {
        Color col = img.color;
        col.a = alpha;
        img.color = col;
    }
}
