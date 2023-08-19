using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ImageQuestion
{
    public Sprite sprite;
    public List<string> imageTags;
    public string name;
    public string description;
    public Direction facingTowards;
    public int speed;
    public int size;
    public bool hasImageTag(string imageTag){
        return imageTags.Contains(imageTag);
    }

    public void addImageTag(string imageTag){
        imageTags.Add(imageTag);
    }

    public void removeImageTag(string imageTag){
        imageTags.Remove(imageTag);
    }
}