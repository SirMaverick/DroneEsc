using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCore : MonoBehaviour {
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Throw()
    {
        animator.SetBool("Throw", true);
    }
}
