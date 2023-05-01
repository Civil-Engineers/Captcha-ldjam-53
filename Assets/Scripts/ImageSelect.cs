using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ImageCaptcha captcha;
    [SerializeField] private UnityEngine.UI.Image question;

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

    public void setToggle() {
        if(!toggle.isOn) {
            toggle.isOn = true;
        }
    }
    public void resetToggle() {
        if(toggle) {
            toggle.isOn = false;
        }
    }

    public void resetQuestion() {
        Color col = question.color;
        float a = col.a;
        if (a == 1f) {
            setImageAlpha(question, 0);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        captcha.select(selection);
    }
    
    public void toggleQuestion() {
        Color col = question.color;
        float a = col.a;

        if (a == 1f) { //visible => invisible
            setImageAlpha(question, 0);
        } else { // invisible => visible
            setImageAlpha(question, 1);
        }
    }

    private void setImageAlpha(UnityEngine.UI.Image img, int alpha) {
        Color col = img.color;
        col.a = alpha;
        img.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
