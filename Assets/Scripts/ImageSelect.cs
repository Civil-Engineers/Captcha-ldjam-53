using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ImageCaptcha captcha;
    public int selection;
    private Image image;
    private UnityEngine.UI.Image UIImage;
    private UnityEngine.UI.Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        captcha = transform.parent.parent.GetComponent<ImageCaptcha>();
        UIImage = GetComponent<UnityEngine.UI.Image>();
        toggle = GetComponent<UnityEngine.UI.Toggle>();
    }

    public void setImage(Image i) {
        image = i;
        if (!UIImage) {
            UIImage = GetComponent<UnityEngine.UI.Image>();
        }
        UIImage.sprite = i.sprite;
    }

    public bool getToggle() {
        return toggle.isOn;
    }
    public void resetToggle() {
        if(toggle) {
            toggle.isOn = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        captcha.select(selection);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
