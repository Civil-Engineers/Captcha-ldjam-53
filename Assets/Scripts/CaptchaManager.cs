using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CaptchaManager : MonoBehaviour
{
    public static CaptchaManager Instance { get; private set; }

    public int numActiveCaptchas = 0;

    public GameObject clickCaptcha;
    public GameObject textCaptcha;
    
    ClickCounter clickCounter;

    public static int LVL_1_CLICKS;
    public static int LVL_2_CLICKS;

    int currentLvl = 0;
    int maxLvl = 2;

    bool isLvl_1 = false;
    bool isLvl_2 = false;

    static float countdown = 0.2f;
    static float LVL_1_COUNT = 10;
    static float LVL_2_COUNT = 15;

    static int x_lim = 270;
    static int y_lim = 79;
    static int maxWindows = 10;

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
                InvokeRepeating("daOneClickCaptcha", countdown, LVL_1_COUNT);
            } else if (!isLvl_2 && numClicks >= LVL_2_CLICKS) {
                isLvl_2 = true;
                InvokeRepeating("daTextCaptcha", countdown, LVL_2_COUNT);
            }

            // cancel repeating captchas when you fall under click threshold
            // TODO llolllllololoololololololollololololl
        }

        if (numActiveCaptchas > maxWindows) {
            CancelInvoke("daTextCaptcha");
        }
    }

    void daOneClickCaptcha() {
        if(!clickCaptcha.gameObject.activeSelf) {
            clickCaptcha.gameObject.SetActive(true);
        }
    }

    void daTextCaptcha() {
        float randomX = Random.Range(-x_lim, x_lim);
        float randomY = Random.Range(-y_lim, y_lim);
        activateCaptcha();
        GameObject captcha = Instantiate(textCaptcha, new Vector3(randomX, randomY, 0), Quaternion.identity);
        captcha.transform.SetParent (this.transform.parent, false);
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
