using System;
using System.Collections.Generic;
using UnityEngine;

public class DroneArmsAnimation : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField]
    DroneSounds droneSounds;
    bool forwards = false;
    bool backwards = false;
    bool right = false;
    bool left = false;
    bool shoot = false;
    bool shootReady = false;

    bool insertIntoMachine = false;





    //used for call backs
    [SerializeField] DronePlayerController playerController;

    public void WalkForwards() {
        if (insertIntoMachine) {
            return;
        }
        if (!forwards) {
            if (backwards) {
                backwards = false;
                animator.SetFloat("BackwardsAnimSpeedMultiplier", -1);
                animator.SetBool("WalkBackwards", backwards);
            } else if (!(left || right)){
                droneSounds.StartMoveSound();
            }

            forwards = true;

            animator.SetFloat("ForwardsAnimSpeedMultiplier", 1);
            animator.SetBool("WalkForwards", forwards);
        }
    }

    public void WalkBackwards() {
        if (insertIntoMachine) {
            return;
        }
        if (!backwards) {
            if (forwards) {
                forwards = false;
                animator.SetFloat("ForwardsAnimSpeedMultiplier", -1);
                animator.SetBool("WalkForwards", forwards);
            } else if (!(left || right)) {
                droneSounds.StartMoveSound();
            }

            backwards = true;
            animator.SetFloat("BackwardsAnimSpeedMultiplier", 1);
            animator.SetBool("WalkBackwards", backwards);
        }
    }

    public void WalkLeft() {
        if (insertIntoMachine) {
            return;
        }
        if (!left) {
            if (right) {
                right = false;
            } else if (!(forwards || backwards)){
                droneSounds.StartMoveSound();
            }

            left = true;
        }
    }

    public void WalkRight() {
        if (insertIntoMachine) {
            return;
        }
        if (!right) {
            if (left) {
                left = false;
            } else if (!(forwards || backwards)){
                droneSounds.StartMoveSound();
            }

            right = true;
        }
    }


    public void WalkNotVertically() {
        if (forwards)
        {
            if(!(left||right))
            droneSounds.StopMoveSound();
            forwards = false;
            animator.SetFloat("ForwardsAnimSpeedMultiplier", -1);
            animator.SetBool("WalkForwards", forwards);
            
        }
        if (backwards )
        {
            if(!(left || right))
            droneSounds.StopMoveSound();
            backwards = false;
            animator.SetFloat("BackwardsAnimSpeedMultiplier", -1);
            animator.SetBool("WalkBackwards", backwards);
        }
    }

    public void WalkNotHorizontally() {
        if (left) {
            if(!(forwards || backwards))
            droneSounds.StopMoveSound();
            left = false;
        }
        if (right) {
            if(!(forwards || backwards))
            droneSounds.StopMoveSound();
            right = false;
        }
    }

    public void ShootReady()
    {
        if (!shootReady)
        {
            shoot = false;
            animator.SetBool("Shoot", shoot);
            droneSounds.StartChargeSound();
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
            droneSounds.StartShootSound();
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
            GenericFunctions.Instance.SetFadeIn("BlackFadeImage", 1.0f, 0.3f, 0.5f);
            bool test = animator.GetBool("WalkForwards");
            Debug.Log("test:" + test);
        }
    }

    public void InsertIntoMachineDone()
    {
        // event from animation
        droneSounds.InsertIntoMachineSound();
        playerController.AnimationInsertIntoMachineDone();
    }

    public bool ExitOutOfMachine()
    {
        if (insertIntoMachine)
        {
            droneSounds.ExitOutOfMachineSound();
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

