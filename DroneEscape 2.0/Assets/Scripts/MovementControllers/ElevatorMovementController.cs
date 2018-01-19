using System;
using System.Collections;
using UnityEngine;
using FMODUnity;
public class ElevatorMovementController : MovementController
{
    [SerializeField] private GameObject elevator;
    [SerializeField] private Transform lowestPos;
    [SerializeField] private Transform highestPos;
    
    [SerializeField] private float speed;
    private ItemsOnElevator items;
    public FMOD.Studio.EventInstance elevatorSound;
    private FMOD.Studio.PLAYBACK_STATE playback;
    private bool moving;

    ElevatorPlayerController elevatorPlayerController;
    

    protected override void Start()
    {
        base.Start();
        items = elevator.GetComponent<ItemsOnElevator>();
        elevatorSound = RuntimeManager.CreateInstance("event:/SFX/Elevator/Elevator");
        RuntimeManager.AttachInstanceToGameObject(elevatorSound, transform, GetComponent<Rigidbody>());
        elevatorPlayerController = (ElevatorPlayerController) playerController;
        
    }

    public override void Horizontal(float direction)
    {

    }

    public override void Vertical(float direction) {
        // Do nothing
        if (direction != 0) {
            elevatorPlayerController.StopPulse();
            elevatorSound.setParameterValue("ElevatorStart", 1.0f);
            elevatorSound.setParameterValue("ElevatorLoop", 1.0f);
            elevatorSound.setParameterValue("ElevatorStop", 0.0f);
            elevatorSound.getPlaybackState(out playback);
            if ( playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

            } else {
                elevatorSound.start();
            }
            if (direction > 0) {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, highestPos.position, speed * Time.deltaTime);
                items.enableElevator = true;
            } else if (direction < 0) {

                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, lowestPos.position, speed * Time.deltaTime);
                items.enableElevator = true;
            }
        } else {
            elevatorSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            elevatorSound.setParameterValue("ElevatorStart", 0.0f);
            elevatorSound.setParameterValue("ElevatorLoop", 0.0f);
            elevatorSound.setParameterValue("ElevatorStop", 1.0f);
            elevatorSound.start();
            items.enableElevator = false;
        }
    }

    public override void Look(Vector2 md)
    {
        // Do nothing
    }

    public override void RightClick(bool key)
    {
        if (key)
        {
            items.enableElevator = false;
            elevatorSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            elevatorPlayerController.PostFXExit();

        }
    }

    IEnumerator WaitToEndAudio(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        elevatorSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }


}

