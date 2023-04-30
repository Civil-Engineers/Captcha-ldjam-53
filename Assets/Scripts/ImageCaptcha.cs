using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCaptcha : MonoBehaviour
{
    private string imageTag;
    private List<Image> choices;
    private const int numberOfChoices = 4;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    [SerializeField] private ImageSelect[] selection = new ImageSelect[4];

    // Start is called before the first frame update
    
    void Start()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
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
                    Reset();
                    return;
                }
            } else if (selection[i].getToggle()) {
                // wrong
                Reset();
                return;
            }
        }
        Debug.Log("good job");
    }

    // Update is called once per frame
    void Update()
    {
    }

    
}
