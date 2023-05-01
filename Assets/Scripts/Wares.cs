using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wares : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI Price;
    int price;
    int level;
    
    public int getPrice() {
        return price;
    }

    public int getLevel() {
        return level;
    }
    public void setLevel(int l) {
        level = l;
    }
    
    public void setDescription(string s) {
        Description.text = s;
    }

    public void setTitle(string s) {
        Title.text = s;
    }

    public void setPrice(int i) {
        price = i; 
        Price.text = ""+i;
    }
}
