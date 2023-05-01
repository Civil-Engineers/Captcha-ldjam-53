using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }

    public int numActiveCaptchas = 0;

    public GameObject clickCaptcha;
    ClickCounter clickCounter;

    public static int LVL_1_CLICKS;
    public static int LVL_2_CLICKS;

    int currentLvl = 0;
    int maxLvl = 2;

    bool isLvl_1 = false;
    bool isLvl_2 = false;

    static float countdown = 0.1f;
    static float LVL_1_COUNT = 10;
    static float LVL_2_COUNT = 30;

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
        clickCounter = gameObject.GetComponent<ClickCounter>();
        LVL_1_CLICKS = clickCounter.LVL_1;
        LVL_2_CLICKS = clickCounter.LVL_2;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLvl < maxLvl) {
            int numClicks = clickCounter.getNumClicks();
            
            // start repeating captchas
            if (!isLvl_1 && numClicks >= LVL_1_CLICKS) {
                isLvl_1 = true;
                InvokeRepeating("oneClickCaptcha", countdown, LVL_1_COUNT);
            } else if (!isLvl_2 && numClicks >= LVL_2_CLICKS) {
                isLvl_2 = true;
                // InvokeRepeating("");
            }

            // cancel repeating captchas when you fall under click threshold
            // TODO llolllllololoololololololollololololl
        }
    }

    void oneClickCaptcha() {
        if(!clickCaptcha.gameObject.activeSelf) {
            clickCaptcha.gameObject.SetActive(true);
        }
    }

    public void activateCaptcha() {
        numActiveCaptchas++;
    }

    public void deactivateCaptcha() {
        if (numActiveCaptchas > 0) {
             numActiveCaptchas--;
        }
    }

    public int getNumCaptchas() {
        return numActiveCaptchas;
    }
}
