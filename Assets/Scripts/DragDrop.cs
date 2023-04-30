using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private bool lastMouseWasOnScreen;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.root.GetComponent<Canvas>();
        rectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
    }

    public bool MouseScreenCheck(){
        int offset = 5;
        #if UNITY_EDITOR
        if (Input.mousePosition.x < offset || Input.mousePosition.y < offset || Input.mousePosition.x >= Handles.GetMainGameViewSize().x - offset || Input.mousePosition.y >= Handles.GetMainGameViewSize().y - offset){
        return false;
        }
        #else
        if (Input.mousePosition.x < offset || Input.mousePosition.y < offset || Input.mousePosition.x >= Screen.width - offset || Input.mousePosition.y >= Screen.height - offset) {
        return false;
        }
        #endif
        else {
            return true;
        }
    }
    public void OnDrag(PointerEventData eventData) {
        // Debug.Log(Input.mousePosition.x);
        if (MouseScreenCheck() ) {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
