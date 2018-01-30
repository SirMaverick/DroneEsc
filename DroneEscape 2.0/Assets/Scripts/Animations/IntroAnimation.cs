using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;


public class IntroAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private RuntimeAnimatorController droneAnimator;
    private bool allowedToThrow = false;
    private bool walkingDone = false;

    [SerializeField] private IntroCore core;
    [SerializeField] private IntroDroneNextRoom droneNext;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void Play()
    {
        animator.SetFloat("Movement", 1.0f);
        animator.SetBool("Wave", false);
    }

    public void Pause()
    {
        animator.SetFloat("Movement", 0.0f);
        animator.SetBool("Wave", true);
    }

    // the player walked in the walk trigger
    /* public void WalkEvent()
     {

     }*/

     // Done playing the animation related to the walk event
     public void WalkAnimationEvent()
     {
         if(allowedToThrow){
            Throw();
          }
        walkingDone = true;
        //MakeEmptyShell(this.gameObject);
     }

     // the player walked in the wave trigger
     /*public void WaveEvent()
     {

     }

     // Done playing the wave animation
     public void WaveAnimationEvent()
     {

     }*/


    // the player walked in the throw trigger
    public void ThrowAllowed()
    {
        allowedToThrow = true;
        if (walkingDone)
        {
            Throw();
        }
    }

    public void Throw() {
        core.Throw();
        droneNext.Throw();
        animator.SetFloat("Movement", 1.0f);
        animator.SetBool("ShootCore", true);
        animator.SetBool("WakeUp", false);
    }

    // Done playing the throw animation
    /*public void ThrowAnimationEvent()
    {
        animator.SetFloat("Movement", 0.0f);
        director.Pause();
        // make the drones who throwed their core an empty shell

/*        foreach (GameObject drone in drones)
        {
            drone.GetComponent<EmptyDrone>().enabled = true;
            drone.tag = "Drone";// yes this is ugly as is the next few things
            drone.GetComponent<Animator>().runtimeAnimatorController = droneAnimator;
            drone.GetComponent<DronePlayerController>().enabled = true;
            drone.GetComponent<NavMeshAgent>().enabled = true;

        }*/
    //}*/

   /* public void MakeEmptyShell(GameObject drone)
    {

        // After throwing the core you can take control of the drone;

        // same as systemArm should be able to make this nicer
        drone.GetComponent<EmptyDrone>().enabled = true;
        drone.tag = "Drone";// yes this is ugly as is the next few things
        drone.GetComponent<Animator>().runtimeAnimatorController = droneAnimator;
        drone.GetComponent<DronePlayerController>().enabled = true;
        drone.GetComponent<NavMeshAgent>().enabled = true;

    }*/
}

