using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextCaptcha : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI OKText;

    private Tween fadeTween;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        errorText.gameObject.SetActive(false);
        OKText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool isInvalid(string fieldValue) {
        // change to right kind of validation later
        return string.IsNullOrEmpty(fieldValue);
    }

    IEnumerator displayError() {
        errorText.DOFade(1,0);
        errorText.gameObject.SetActive(true);
        errorText.DOFade (0,1f);
        yield return new WaitForSeconds(1f);
        errorText.gameObject.SetActive(false);
    }
    
    IEnumerator displayOKAndExit() {
        OKText.DOFade(1,0);
        OKText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        // OKText.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator destroyThis() {
        yield return new WaitForSeconds(1f);
        
    }

    void validateAndSubmit(string fieldValue) {
        if (isInvalid(fieldValue)) {
            Debug.Log("invalid meep");
            StartCoroutine(displayError());
            return;
        } else {
            StartCoroutine(displayOKAndExit());
            return;
        }
    }

    // to be called from a submit button onClick event
    public void validateAndSubmit() {
        validateAndSubmit(inputField.text);
    }

}