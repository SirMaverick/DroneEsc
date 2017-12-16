using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDroneAnimation : MonoBehaviour {

    [SerializeField] Animator animator;

    public void CaughtByArm()
    {
        animator.SetBool("GotCaught", true);
    }


    public void Shocked()
    {
        animator.SetBool("Shocked", true);
    }
    public void NotShocked()
    {
        animator.SetBool("Shocked", false);
    }
}
