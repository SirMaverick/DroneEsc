using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonAnimation : MonoBehaviour {

    public Animator animator;
    public bool throwCore, insertMachine, moveElevator, moveMagnet; 

    public static TriggerButtonAnimation _instance;
    public static TriggerButtonAnimation Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<TriggerButtonAnimation>();

                if (_instance == null) {
                    GameObject container = new GameObject("TutorialAnimator");
                    _instance = container.AddComponent<TriggerButtonAnimation>();
                }
            }
            return _instance;
        }
    }

    private void Start() {
        animator = GameObject.Find("TutorialCanvas").GetComponent<Animator>();
    }

    public void TurnAnimationOn(string boolName) {
        animator.SetBool(boolName, true);
    }

    public void TurnAnimationOff(string boolName) {
        animator.SetBool(boolName, false);
    }
}
