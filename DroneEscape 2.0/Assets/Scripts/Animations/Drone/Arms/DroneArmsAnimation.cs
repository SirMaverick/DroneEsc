using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneArmsAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    bool forwards = false;
    bool backwards = false;

    public void WalkForwards()
    {
        if (!forwards)
        {
            forwards = true;
            backwards = false;

            animator.SetBool("WalkForwards", forwards);
            animator.SetBool("WalkBackwards", backwards);
        }
    }

    public void WalkBackwards()
    {
        if (!backwards)
        {
            forwards = false;
            backwards = true;
            animator.SetBool("WalkForwards", forwards);
            animator.SetBool("WalkBackwards", backwards);
        }
    }


    public void WalkNotVertically()
    {
        if (forwards)
        {
            forwards = false;
            animator.SetBool("WalkForwards", forwards);
        }
        if (backwards)
        {
            backwards = false;
            animator.SetBool("WalkBackwards", backwards);
        }
    }
}

