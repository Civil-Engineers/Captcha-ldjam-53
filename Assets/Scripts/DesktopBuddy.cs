using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DesktopBuddy : MonoBehaviour
{
    private static List<DesktopBuddy> buddies;
    Animator animator;
    private UnityEngine.UI.Image image;
    bool idling = false;
    // Start is called before the first frame update
    void Start()
    {
        if(buddies == null) {
            buddies = new List<DesktopBuddy>();
        }
        buddies.Add(this);
        animator = GetComponent<Animator>();
        image = GetComponent<UnityEngine.UI.Image>();
        startIdle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startIdle(){
        animator.Play("base.walk");
        idling = true;
        Idle();

    }
    void Idle() {
        if(idling) {
            Vector3 goal = new Vector3(Random.Range(-350,350), -180, 0);
            if(goal.x - transform.localPosition.x > 0) {
                Debug.Log("right");
                transform.DORotate(new Vector3(0,180,0), 0);
            } else {
                Debug.Log("left");
                transform.DORotate(new Vector3(0,0,0), 0);
            }
            Tweener t1 = transform.DOLocalMove(goal,Random.Range(2,4));
            t1.OnStepComplete(() =>
            {
                Idle();
            });
        }
    }

    public static void captchaTry() {
        foreach(DesktopBuddy b in buddies) {
            b.idling = false;
            b.transform.DOKill();
            b.animator.Play("base.think");
        }
    }

    public static void captchaAllSolve() {
        foreach(DesktopBuddy b in buddies) {
            b.idling = false;
            b.transform.DOKill();
            b.animator.Play("base.yaya");
            Sequence t1 = b.transform.DOLocalJump(b.transform.localPosition, 4,2,2);
            t1.AppendCallback(()=>{b.idling = true;b.Idle();});
        }
    }
}
