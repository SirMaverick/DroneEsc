using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] EmptyDrone emptyDrone;

    public void WakeUp()
    {
        //animator.Play("WakeUp");
        animator.SetBool("WakeUp", true);
        
    }
    public void AnimationWakeUpDone()
    {
        animator.SetBool("WakeUp", false);
        emptyDrone.AnimWakeUpDone();
    }


    public void PickUp()
    {
        GenericFunctions.Instance.SetFadeInAndOut("BlackFadeImage", 1.0f, 0.5f, 0.63f, 0.2f);
        animator.SetBool("PickUpCore", true);
        
    }
    public void AnimationPickUpDone()
    {     
        animator.SetBool("PickUpCore", false);
        emptyDrone.AnimPickUpDone();
    }

    public void HasShot()
    {
        animator.SetBool("Shot", true);
        animator.Play("ShootCore");
    }
    public void AnimationHasShotDone()
    {
        animator.SetBool("Shot", false);
    }

    public void Default()
    {
        animator.SetBool("Shot", false);
        animator.SetBool("WakeUp", false);
        animator.SetBool("PickUpCore", false);
        animator.Play("Default");
    }

    public void DefaultInDrone()
    {
        animator.SetBool("Shot", false);
        animator.Play("DefaultInDrone");
    }
}

