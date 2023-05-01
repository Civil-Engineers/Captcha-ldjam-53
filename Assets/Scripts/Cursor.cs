using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cursor : MonoBehaviour
{
    private static float clickSpeed = 1;
    private Transform Movement;
    private UnityEngine.UI.Image UIImage;
    private Animator animator;
    private float timeFromLastClick = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeFromLastClick += Time.deltaTime;
        if(timeFromLastClick >= clickSpeed) {
            click();
            timeFromLastClick = 0;
        }
    }

    void click() {
        animator.Play("Base Layer.click");
    }
}
