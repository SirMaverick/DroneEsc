using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemArmAnimation : MonoBehaviour
{
    private Animator animator;
    private SystemArm systemArm;

    private void Start()
    {
        animator = GetComponent<Animator>();
        systemArm = GetComponent<SystemArm>();
    }

    public void Animation_PickUpDrone()
    {
        animator.SetBool("PickUp", true);
    }

    // start actually lifting the drone
    public void Animation_PickingUpDrone()
    {
        animator.SetBool("PickUp", false);
        systemArm.PickingUpDrone();
    }

    // drop the drone which was lifted
    public void Animation_DonePickingUpDrone()
    {
        systemArm.DonePickingUpDrone();
    }

    // full animation done
    public void Animation_PickUpDroneDone()
    {

        systemArm.PickUpDroneDone();
        

    }

}

