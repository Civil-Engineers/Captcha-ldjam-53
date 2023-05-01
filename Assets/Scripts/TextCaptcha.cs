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

    // Start is called before the first frame update
    void Start()
    {
        GenerateCaptcha();
        inputField = GetComponentInChildren<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateCaptcha() {
        currentCaptcha = textGenerator.Generate();

        // change UI
        codeImage.sprite = currentCaptcha.Image;
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
