using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Image
{
    public Sprite sprite;
    public List<string> imageTags;
    public bool hasImageTag(string imageTag){
        return imageTags.Contains(imageTag);
    }
}
