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
        // fade in
        Tween showTween = this.GetComponent<CanvasGroup>().DOFade(1,0.3f);

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

    void OnDestroy() {
        this.GetComponent<CanvasGroup>().DOKill();
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
        closeWindow();
        // Debug.Log("good job");
    }

    // Update is called once per frame
    void Update()
    {
        int level = UpgradesWindow.Instance.getPictoLevel();
        if (level > 0) {
            timeFromShift += Time.deltaTime;
            timeFromSolve += Time.deltaTime;
            if(timeFromShift > baseShiftTime*Mathf.Pow(.75f,level)) {
                thinkingMode();
                timeFromShift=0;
            }
            if(timeFromSolve > timeToFinishSolve*Mathf.Pow(.75f,level)) {
                // solve it
                for (int i = 0; i < numberOfChoices; i++) {
                    if (choices[i].hasImageTag(imageTag)) {
                        selection[i].setToggle();
                    } else {
                        selection[i].resetQuestion();
                        selection[i].resetToggle();
                    }
                }
                StartCoroutine("closeWindow");
            }
        }
    }
    private float timeFromShift = 0;
    private float baseShiftTime = .4f;
    private float timeFromSolve = 0;
    private float timeToFinishSolve = 5;

    void thinkingMode() {
        int r = Random.Range(0, numberOfChoices);
        selection[r].toggleQuestion();
    }


    IEnumerator displayError() {
        errorText.DOFade(1,0);
        errorText.gameObject.SetActive(true);
        errorText.DOFade (0,1f);
        yield return new WaitForSeconds(1f);
        errorText.gameObject.SetActive(false);
    }

    void closeWindow() {
        Tween hideTween = this.GetComponent<CanvasGroup>().DOFade(0,0.4f).SetDelay(0.2f);
        hideTween.OnComplete(
            () => {
                CaptchaManager.Instance.deactivateCaptcha();
                Destroy(gameObject);
            }
        );
    }
    
}
