using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCursors : MonoBehaviour
{
    public GameObject cursorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createCursor() {
        float randZ = Random.Range(-66.8f, 66.8f);
        Quaternion myRotation = Quaternion.identity;
        myRotation.eulerAngles = new Vector3(0, 0, randZ);
        GameObject newCursor = Instantiate(cursorPrefab, new Vector3(0,0,0), myRotation);
        newCursor.transform.SetParent(this.transform, false);
        // newCursor.transform.position = ;
        
        // newCursor.transform.rotation.Set(0,0,randZ,0);
    }
}
