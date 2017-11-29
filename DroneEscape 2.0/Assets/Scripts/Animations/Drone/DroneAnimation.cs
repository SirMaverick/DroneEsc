﻿using System;
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
        Debug.Log("AnimationWakeUpDone");
        animator.SetBool("WakeUp", false);
        emptyDrone.AnimationWakeUpDone();
    }


    public void PickUp()
    {
        animator.SetBool("PickUpCore", true);
    }
    public void AnimationPickUpDone()
    {
        Debug.Log("PickupDone");
        animator.SetBool("PickUpCore", false);
        emptyDrone.AnimationPickUpDone();
    }

}

