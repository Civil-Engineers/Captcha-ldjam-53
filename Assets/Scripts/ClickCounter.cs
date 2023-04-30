using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCounter : MonoBehaviour {

    public static int clickCount = 0;
    public TMPro.TextMeshProUGUI counter;
    public GameObject clickCaptcha;

    static int LVL_1 = 10;
    static int LVL_2 = 100;

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
        if (clickCount >= LVL_1) {
            clickCaptcha.gameObject.SetActive(true);
            return false;
        }
        return true;
    }

    public void addClick() {
        if (validClick()) {
            clickCount++;
        }
    }
}
