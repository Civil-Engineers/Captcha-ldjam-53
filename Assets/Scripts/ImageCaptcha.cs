using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ImageCaptcha : MonoBehaviour
{
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI OKText;

    private string imageTag;
    private List<Image> choices;
    private const int numberOfChoices = 4;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private ImageSelect[] selection = new ImageSelect[4];

    // Start is called before the first frame update
    
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        Reset();
    }

    void Reset() {
        ImageBank bank = ImageBank.Instance;
        imageTag = ImageBank.getRandomTag();
        int numberOfCorrect = Random.Range(1,4); // 1 to 3 
        choices = bank.getImagesForCaptcha(imageTag, numberOfCorrect, numberOfChoices);
        shuffleChoices();
        updateSelection();
        text.text = imageTag;
    }

    void shuffleChoices() {
        for (int t = 0; t < choices.Count; t++ )
        {
            Image tmp = choices[t];
            int r = Random.Range(t, choices.Count);
            choices[t] = choices[r];
            choices[r] = tmp;
        }
    }

    void updateSelection() {
        for(int i = 0; i < numberOfChoices; i++) {
            selection[i].setImage(choices[i]);
            selection[i].resetToggle();
        }
    }

    public void select(int slot) {
        // Debug.Log(imageTag);
        // Debug.Log(currentCorrect+1);
        // if (choices[slot].hasImageTag(imageTag)) {
        //     currentCorrect++;
        // } else {
        //     currentWrong++;
        // }
    }

    public void submit() {
        for(int i = 0; i < numberOfChoices; i++) {
            if (choices[i].hasImageTag(imageTag)) {
                if(!selection[i].getToggle()) {
                    // wrong
                    StartCoroutine("displayError");
                    Reset();
                    return;
                }
            } else if (selection[i].getToggle()) {
                // wrong
                StartCoroutine("displayError");
                Reset();
                return;
            }
        }
        text.text = "Success!";
        StartCoroutine("closeWindow");
        // Debug.Log("good job");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator displayError() {
        errorText.DOFade(1,0);
        errorText.gameObject.SetActive(true);
        errorText.DOFade (0,1f);
        yield return new WaitForSeconds(1f);
        errorText.gameObject.SetActive(false);
    }

    IEnumerator closeWindow() {
        // OKText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        CaptchaManager.Instance.deactivateCaptcha();
        Destroy(gameObject);
    }
    
}
