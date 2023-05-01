using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    void Awake()
    {
        Invoke("UpdateParentLayoutGroup", 0.01f);
    }

    void UpdateParentLayoutGroup() {
        transform.parent.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
