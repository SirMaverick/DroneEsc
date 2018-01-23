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
    public FMOD.Studio.EventInstance elevatorStartSound, elevatorStopSound;
    private FMOD.Studio.PLAYBACK_STATE playback;
    private bool moving;
    public bool ready;

    ElevatorPlayerController elevatorPlayerController;
    

    protected override void Start()
    {
        base.Start();
        items = elevator.GetComponent<ItemsOnElevator>();
        elevatorStartSound = RuntimeManager.CreateInstance("event:/SFX/Elevator/Elevator");
        elevatorStopSound = RuntimeManager.CreateInstance("event:/SFX/Elevator/Elevator");
        RuntimeManager.AttachInstanceToGameObject(elevatorStartSound, transform, GetComponent<Rigidbody>());
        RuntimeManager.AttachInstanceToGameObject(elevatorStopSound, transform, GetComponent<Rigidbody>());
        elevatorPlayerController = (ElevatorPlayerController) playerController;
        ready = true;
    }

    public override void Horizontal(float direction)
    {

    }

    public override void Vertical(float direction) {

        print(moving);
        // Do nothing
        if (direction != 0) {
            elevatorPlayerController.StopPulse();
            elevatorStartSound.setParameterValue("ElevatorStart", 1.0f);
            elevatorStartSound.setParameterValue("ElevatorLoop", 1.0f);
            elevatorStartSound.setParameterValue("ElevatorStop", 0.0f);
            elevatorStartSound.getPlaybackState(out playback);
            print(playback);
            if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

            } else {
                elevatorStartSound.start();
            }
            if (direction > 0) {
                moving = true;
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, highestPos.position, speed * Time.deltaTime);
                items.enableElevator = true;
            } else if (direction < 0) {
                moving = true;
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, lowestPos.position, speed * Time.deltaTime);
                items.enableElevator = true;
            }
        } else {
            print("else if moving");
            elevatorStartSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            elevatorStopSound.setParameterValue("ElevatorStart", 0.0f);
            elevatorStopSound.setParameterValue("ElevatorLoop", 0.0f);
            elevatorStopSound.setParameterValue("ElevatorStop", 1.0f);
            elevatorStopSound.getPlaybackState(out playback);
            if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

            } else if(moving){
                elevatorStopSound.start();
                StartCoroutine(WaitToEndAudio(0.4f));
            }
            moving = false;
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
            ready = false;
            items.enableElevator = false;
            elevatorStartSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            elevatorPlayerController.PostFXExit();

        }
    }

    IEnumerator WaitToEndAudio(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        elevatorStopSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }


}

