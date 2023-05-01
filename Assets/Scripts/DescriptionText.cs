using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
