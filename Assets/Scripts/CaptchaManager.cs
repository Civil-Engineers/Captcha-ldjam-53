using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CaptchaManager : MonoBehaviour
{
    public static CaptchaManager Instance { get; private set; }

    public int numActiveCaptchas = 0;
    
    public ClickCounter clickCounter;

    public static int LVL_1_CLICKS = 10;
    public static int LVL_2_CLICKS = 20;
    public static int LVL_3_CLICKS = 30;

    private int currentLvl = 0;
    private int maxLvl = 3;

    private bool isLvl_1 = false; // one-click txtCaptcha
    private bool isLvl_2 = false; // text gen txtCaptcha
    private bool isLvl_3 = false; // image gen txtCaptcha

    private static float countdown = 0.2f;
    private static float LVL_1_COUNT = 10;
    private static float LVL_2_COUNT = 15;
    private static float LVL_3_COUNT = 20;

    private static int x_lim = 270;
    private static int y_lim = 79;
    private static int maxWindows = 5;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLvl < maxLvl) {
            int numClicks = clickCounter.getTotalNumClicks();
            
            // start repeating captchas
            if (!isLvl_1 && numClicks >= LVL_1_CLICKS) {
                isLvl_1 = true;
                InvokeRepeating("daOneClickCaptcha", countdown, LVL_1_COUNT);
            } else if (!isLvl_2 && numClicks >= LVL_2_CLICKS) {
                isLvl_2 = true;
                InvokeRepeating("daTextCaptcha", countdown, LVL_2_COUNT);
            } else if (!isLvl_3 && numClicks >= LVL_3_CLICKS) {
                isLvl_3 = true;
                InvokeRepeating("daImageCaptcha", countdown, LVL_3_COUNT);
            }

            // cancel repeating captchas when you fall under click threshold
            // TODO llolllllololoololololololollololololl
        }
        // bool windowPause = false;
        // manageWindows(windowPause);
    }

    private void manageWindows(bool windowPause) {    
        if (numActiveCaptchas > maxWindows) {
            CancelInvoke("daTextCaptcha");
            CancelInvoke("daImageCaptcha");
            windowPause = true;
        }

        if (numActiveCaptchas == 0 && windowPause) {
            if (isLvl_1) {
                InvokeRepeating("daOneClickCaptcha", countdown, LVL_1_COUNT);
            } if (isLvl_2) {
                InvokeRepeating("daTextCaptcha", countdown, LVL_2_COUNT);
            } if (isLvl_3) {
                InvokeRepeating("daImageCaptcha", countdown, LVL_3_COUNT);
            }
            windowPause = false;
        }
    }

    public GameObject clickCaptcha;
    public GameObject textCaptcha;
    public GameObject imageCaptcha;

    void daOneClickCaptcha() {
        if(!clickCaptcha.gameObject.activeSelf) {
            clickCaptcha.gameObject.SetActive(true);
        }
    }

    void daTextCaptcha() {
        float randomX = Random.Range(-x_lim, x_lim);
        float randomY = Random.Range(-y_lim, y_lim);
        activateCaptcha();
        GameObject txtCaptcha = Instantiate(textCaptcha, new Vector3(randomX, randomY, 0), Quaternion.identity);
        txtCaptcha.transform.SetParent (this.transform.parent, false);
    }

    void daImageCaptcha() {
        float randomX = Random.Range(-x_lim, x_lim);
        float randomY = Random.Range(-y_lim, y_lim);
        GameObject imgCaptcha = Instantiate(imageCaptcha, new Vector3(randomX, randomY, 0), Quaternion.identity);
        CaptchaManager.Instance.activateCaptcha();
        imgCaptcha.transform.SetParent (this.transform.parent, false);
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
