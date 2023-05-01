using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesWindow : MonoBehaviour
{
    public static UpgradesWindow Instance { get; private set; }
    private int[] baseCost = {5, 10, 20, 100, 1200};
    // private int[] baseCost = {25, 50, 300, 800, 1200};

    private int[] level = {0, 0, 0, 0, 0};

    [SerializeField] Texture2D[] cursors = new Texture2D[10];
    // private string[] title = {"Crazy Click ", 0, 0, 0, 0};
    private float incRate = 1.15f;
    private int shownWares = 1;
    [SerializeField] private Wares[] wares = new Wares[5];

    [SerializeField] private GameObject spawnParent;
    [SerializeField] private GameObject desktopBuddyPrefab;

    bool wareDownloaded = false;
    bool lvl_1 = false;
    bool lvl_2 = false;
    bool lvl_3 = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        for(int n = 0; n < 5; n++) {
            wares[n].setPrice(baseCost[n]);
        }
    }

     void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
        return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update() {
        updateWares();
    }

    public void tryBuy(int slot) {
        int price = wares[slot].getPrice();
        ClickCounter c = ClickCounter.Instance;
        if(price <= c.getNumClicks()) {
            buy(slot);
            c.subtractClicks(price);
            level[slot] = level[slot]+1;
            float newPrice = baseCost[slot] * Mathf.Pow(incRate, level[slot]);
            wares[slot].setPrice(((int)newPrice));
        }
        
    }

    void updateWares() {
        CaptchaManager cm = CaptchaManager.Instance;
        ClickCounter cc = ClickCounter.Instance;

        int numClicks = cc.getTotalNumClicks();
        if (numClicks >= cm.getLvl1Clicks() && !lvl_1) {
            showMoreWares();
            lvl_1 = true; // special cursor
        } else if (numClicks >= cm.getLvl2Clicks() && !lvl_2) {
            showMoreWares();
            lvl_2 = true; // monkeytype
        } else if (numClicks >= cm.getLvl3Clicks() && !lvl_3) {
            showMoreWares();
            lvl_3 = true; // pictopal
        }
    }

    void buy(int slot) {
        if (wareDownloaded == false) {
            wareDownloaded = true;
            CaptchaManager.Instance.downloadFirstWare();
        }

        int slotLevel = level[slot];
        // if (slotLevel == 0) {
        //     showMoreWares();
        // }
        slotLevel++;
        switch(slot) {
            case 0: 
                if(slotLevel < 4) {
                    wares[slot].setTitle("Crazy Click Version 0."+(slotLevel*2));
                } else {
                    wares[slot].setTitle("Crazy Click Version "+(slotLevel-4));
                }
                
                if(slotLevel == 4) {
                    wares[slot].setDescription("Get a bot to click for you! => [update] captcha-killer");
                } else if (slotLevel == 5) {
                    wares[slot].setDescription("Get a bot to click for you!");
                }

                AutoCursors.Instance.createCursor();
                break;
            case 1:
                wares[slot].setTitle("Custom Cursor Version "+(slotLevel+1));
                if(slotLevel < 10) {
                    UnityEngine.Cursor.SetCursor(cursors[slotLevel], Vector2.zero, CursorMode.Auto);
                }
                break;
            case 2:
                wares[slot].setTitle("MonkeyType Version "+(slotLevel+1));
                if(slotLevel == 1) {
                    //MonkeyType.setActive
                }
                break;
            case 3:
                wares[slot].setTitle("PictoPal MK. "+(slotLevel+1));
                float randx = Random.Range(-350f, 350f);
                Quaternion myRotation = Quaternion.identity;
                GameObject newCursor = Instantiate(desktopBuddyPrefab, new Vector3(randx,-165,0), myRotation);
                newCursor.transform.SetParent(spawnParent.transform, false);
                break;
                
        }
    }

    void showMoreWares() {
        if(shownWares < wares.Length){ 
            wares[shownWares].gameObject.SetActive(true);
            shownWares++;
        }
    }

    public int getCursorLevel(){
        return level[1];
    }
    public int getMonkeyLevel(){
        return level[2];
    }

    public int getPictoLevel() {
        return level[3];
    }

    public void toggleWindowVisibility() {
        float a = this.GetComponent<CanvasGroup>().alpha;
        if (a == 1f) { //visible => invisible
            this.GetComponent<CanvasGroup>().alpha = 0f;
            this.GetComponent<CanvasGroup>().interactable = false;
        } else { // invisible => visible
            this.GetComponent<CanvasGroup>().interactable = true;
            this.GetComponent<CanvasGroup>().alpha = 1f;
        }
    }
}
