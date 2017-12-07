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

    public void WalkForwards()
    {
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

    internal void ShootReady()
    {
        if (!shootReady)
        {
            shoot = false;
            animator.SetBool("Shoot", shoot);
            shootReady = true;
            animator.SetBool("ShootReady", shootReady);
        }
    }

    internal void Shoot()
    {
        if (!shoot)
        {
            shoot = true;
            animator.SetBool("Shoot", shoot);
            shootReady = false;
            animator.SetBool("ShootReady", shootReady);
        }
    }
}

