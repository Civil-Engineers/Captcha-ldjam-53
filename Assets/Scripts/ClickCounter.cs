using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCounter : MonoBehaviour {
    public static ClickCounter Instance { get; private set; }

    public int clickCount = 0;
    public TMPro.TextMeshProUGUI counter;
    public GameObject ClickCaptcha;

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
        counter.text =  "" + clickCount;
    }

    bool validClick() {
        return (CaptchaManager.Instance.getNumCaptchas() == 0);
    }

    public void addClick() {
        if (validClick()) {
            clickCount++;
        }
    }

    public void addNumClicks(int num) {
        if (validClick()) {
            clickCount+=num;
        }
    }

    public int getNumClicks() {
        return clickCount;
    }
}
