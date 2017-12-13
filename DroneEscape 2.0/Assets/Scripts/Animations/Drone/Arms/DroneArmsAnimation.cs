using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneArmsAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool forwards = false;
    bool backwards = false;
    bool shoot = false;
    bool shootReady = false;

    bool insertIntoMachine = false;

    //used for call backs
    [SerializeField] DronePlayerController playerController;

    public void WalkForwards()
    {
        if (insertIntoMachine)
        {
            return;
        }
        if (!forwards)
        {
            if (backwards)
            {
                backwards = false;
                animator.SetFloat("BackwardsAnimSpeedMultiplier", -1);
                animator.SetBool("WalkBackwards", backwards);
            }

            forwards = true;

            animator.SetFloat("ForwardsAnimSpeedMultiplier", 1);
            animator.SetBool("WalkForwards", forwards);
        }
    }

    public void WalkBackwards()
    {
        if (insertIntoMachine)
        {
            return;
        }
        if (!backwards)
        {
            if (forwards)
            {
                forwards = false;
                animator.SetFloat("ForwardsAnimSpeedMultiplier", -1);
                animator.SetBool("WalkForwards", forwards);
            }
            
            backwards = true;
            animator.SetFloat("BackwardsAnimSpeedMultiplier", 1);
            animator.SetBool("WalkBackwards", backwards);
        }
    }


    public void WalkNotVertically()
    {
        if (forwards)
        {
            forwards = false;
            animator.SetFloat("ForwardsAnimSpeedMultiplier", -1);
            animator.SetBool("WalkForwards", forwards);
            
        }
        if (backwards)
        {
            backwards = false;
            animator.SetFloat("BackwardsAnimSpeedMultiplier", -1);
            animator.SetBool("WalkBackwards", backwards);
        }
    }

    public void ShootReady()
    {
        if (!shootReady)
        {
            shoot = false;
            animator.SetBool("Shoot", shoot);
            shootReady = true;
            animator.SetBool("ShootReady", shootReady);
        }
    }

    public void Shoot()
    {
        if (!shoot)
        {
            shoot = true;
            animator.SetBool("Shoot", shoot);
            shootReady = false;
            animator.SetBool("ShootReady", shootReady);
        }
    }

    public void InsertIntoMachine()
    {
        if (!insertIntoMachine)
        {
            WalkNotVertically();
            insertIntoMachine = true;
            animator.SetBool("InsertIntoMachine", insertIntoMachine);
            bool test = animator.GetBool("WalkForwards");
            Debug.Log("test:" + test);
        }
    }

    public void InsertIntoMachineDone()
    {
        // event from animation
        playerController.AnimationInsertIntoMachineDone();
    }

    public bool ExitOutOfMachine()
    {
        if (insertIntoMachine)
        {
            insertIntoMachine = false;
            animator.SetBool("InsertIntoMachine", insertIntoMachine);
            return true;
        }
        return false;
    }

    public void ExitMachineDone()
    {
        // event from animation
        playerController.AnimationExitMachineDone();
    }

}

