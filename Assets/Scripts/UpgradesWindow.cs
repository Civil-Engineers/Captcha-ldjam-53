using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesWindow : MonoBehaviour
{
    private int[] baseCost = {25, 50, 300, 800, 1200};
    private int[] level = {0, 0, 0, 0, 0};
    // private int[] title = {"Crazy Clic ", 0, 0, 0, 0};
    private float incRate = 1.15f;
    private int shownWares = 1;
    [SerializeField] private Wares[] wares = new Wares[5];
    
    // Start is called before the first frame update
    void Start()
    {
        for(int n = 0; n < 5; n++) {
            wares[n].setPrice(baseCost[n]);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void tryBuy(int slot) {
        int price = wares[slot].getPrice();
        ClickCounter c = ClickCounter.Instance;
        if(price <= c.getNumClicks()) {
            buy(slot);
            // c.spendNumClicks(price);
            level[slot] = level[slot]+1;
            float newPrice = baseCost[slot] * Mathf.Pow(incRate, level[slot]);
            wares[slot].setPrice(((int)newPrice));
        }
        
    }

    void buy(int slot) {
        switch(slot) {
            case 0: 
                AutoCursors.Instance.createCursor();
                break;
        }
    }

    void showMoreWares() {
        wares[shownWares].gameObject.SetActive(true);
        shownWares++;
    }
}
