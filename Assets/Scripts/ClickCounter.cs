using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCounter : MonoBehaviour {

    public int clickCount = 0;
    public TMPro.TextMeshProUGUI counter;
    public GameObject ClickCaptcha;

    public int LVL_1 = 10;
    public int LVL_2 = 100;

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
        return (ClickManager.Instance.getNumCaptchas() == 0);
    }

    public void addClick() {
        if (validClick()) {
            clickCount++;
        }
    }

    public int getNumClicks() {
        return clickCount;
    }
}
