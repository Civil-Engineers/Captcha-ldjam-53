using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextCaptcha : MonoBehaviour
{
    [Header("UI References :")]
    [SerializeField] private UnityEngine.UI.Image codeImage;

    [Header("Captcha Generator :")]
    [SerializeField] private TextGenerator textGenerator;
    
    public TMP_InputField inputField;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI OKText;

    private Tween fadeTween;
    private TexMex currentCaptcha;
    private float timeFromShift = 0;
    private float baseShiftTime = .4f;
    private float timeFromSolve = 0;
    private float timeToFinishSolve = 3;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCaptcha();
        inputField = GetComponentInChildren<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        int level = UpgradesWindow.Instance.getMonkeyLevel();
        if(level > 0) {
            timeFromShift += Time.deltaTime;
            timeFromSolve += Time.deltaTime;
            if(timeFromShift > baseShiftTime*Mathf.Pow(.75f,level)) {
                inputField.text = RandomStringGenerator(currentCaptcha.Value.Length);
                timeFromShift=0;
            }
            if(timeFromSolve > timeToFinishSolve*Mathf.Pow(.75f,level)) {
                inputField.text = currentCaptcha.Value;
                StartCoroutine(displayOKAndDestroy());
            }
        }
        
    }
    
    void OnDestroy() {
        errorText.DOKill();
        // OKText.DOKill();
    }

    private void GenerateCaptcha() {
        currentCaptcha = textGenerator.Generate();

        // change UI
        codeImage.sprite = currentCaptcha.Image;
    }

    private string RandomStringGenerator(int length)
    {
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        string generated_string = "";

        for(int i = 0; i < length; i++)
            generated_string += characters[Random.Range(0, characters.Length)];

        return generated_string;
    }

    bool isValid(string fieldValue) {
        return textGenerator.isCodeValid(fieldValue, currentCaptcha);
        // change to right kind of validation later
        // return string.IsNullOrEmpty(fieldValue);
    }

    IEnumerator displayError() {
        errorText.DOFade(1,0);
        errorText.gameObject.SetActive(true);
        errorText.DOFade (0,1f);
        yield return new WaitForSeconds(1f);
        errorText.gameObject.SetActive(false);
    }
    
    IEnumerator displayOKAndDestroy() {
        OKText.DOFade(1,0);
        OKText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        CaptchaManager.Instance.deactivateCaptcha();
        Destroy(gameObject);
    }

    void validateAndSubmit(string fieldValue) {
        if (isValid(fieldValue)) {
            StartCoroutine(displayOKAndDestroy());
            return;
        } else {
            StartCoroutine(displayError());
            return;
        }
    }

    // to be called from a submit button onClick event
    public void validateAndSubmit() {
        validateAndSubmit(inputField.text);
    }

    public void refreshCaptcha() {
        GenerateCaptcha();
    }

}
