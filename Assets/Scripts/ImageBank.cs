using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageBank : MonoBehaviour
{
    public static ImageBank Instance { get; private set; }

    [SerializeField] public Image[] bank;
    static List<string> tags = new List<string>{
        "is a Animal", "Has a Soul",
        "you can take in a fight", "can fit in your mouth",
         "is an accessory"
    };
    static List<string> strangeTags = new List<string>{
        "a Soul",
    };

    void Awake()
    {
        if (Instance != null) {
        Debug.LogError("There is more than one instance!");
        return;
        }
        Instance = this;
    }

    public static string getRandomTag() {
        return tags[Random.Range(0,tags.Count)];
    }
    
    public List<Image> getImagesForCaptcha(string tag, int correct, int size) {
        List<Image> withTag = new List<Image>();
        List<Image> withoutTag = new List<Image>();
        foreach(Image i in bank) {
            if(i.hasImageTag(tag)) {
                withTag.Add(i);
            } else {
                withoutTag.Add(i);
            }
        }

        List<Image> answer = new List<Image>();
        while(withTag.Count > correct) {
            withTag.RemoveAt(Random.Range(0,withTag.Count));
        }

        while(withTag.Count < size && withoutTag.Count != 0) {
            int i = Random.Range(0,withoutTag.Count);
            withTag.Add(withoutTag[i]);
            withoutTag.RemoveAt(i);
        }
        return withTag;
    }

}
