using System;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class MagnetMovementController : MovementController
{
    [SerializeField]
    private GameObject magnet;
    public float speed = 2;
    public bool coreInside;
    public GameObject drone;
    bool ready, tutorialOn;

    private float horizontalMove, verticalMove;

    public FMOD.Studio.EventInstance magnetMoveSound, magneticSound;
    private FMOD.Studio.PLAYBACK_STATE playback;

    MagnetPlayerController magnetPlayerController;

    protected override void Start() {
        base.Start();
        magnetMoveSound = RuntimeManager.CreateInstance("event:/SFX/Magnet/Magnet");
        RuntimeManager.AttachInstanceToGameObject(magnetMoveSound, transform, GetComponent<Rigidbody>());
        magneticSound = RuntimeManager.CreateInstance("event:/SFX/Magnet/MagneticSound");
        RuntimeManager.AttachInstanceToGameObject(magneticSound, transform, GetComponent<Rigidbody>());
        magnetPlayerController = (MagnetPlayerController)playerController;
        ready = true;
    }

    public override void Horizontal(float direction) {
        // Do nothing
        if(ready) {
            horizontalMove = direction;
            magnet.transform.Translate(direction * Time.deltaTime * speed, 0, 0);

            if (direction != 0 && ready) {
                if(!tutorialOn) {
                    TriggerButtonAnimation.Instance.TurnAnimationOff("WASD");
                    TriggerButtonAnimation.Instance.moveMagnet = true;
                    tutorialOn = true;
                }
                magnetPlayerController.StopPulse();
                magnetMoveSound.setParameterValue("MagnetOn", 0.0f);
                magnetMoveSound.setParameterValue("MagnetMovement", 1.0f);
                magnetMoveSound.setParameterValue("MagnetLock", 0.0f);
                magnetMoveSound.setParameterValue("GrabSmall", 0.0f);
                magnetMoveSound.setParameterValue("GrabMedium", 0.0f);
                magnetMoveSound.setParameterValue("GrabBig", 0.0f);
                magnetMoveSound.setParameterValue("MagnetDrop", 0.0f);
                magnetMoveSound.setParameterValue("DropSmall", 0.0f);
                magnetMoveSound.setParameterValue("DropMedium", 0.0f);
                magnetMoveSound.setParameterValue("DropBig", 0.0f);
                magnetMoveSound.getPlaybackState(out playback);
                if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

                } else {
                    magnetMoveSound.start();
                    magnetMoveSound.setParameterValue("MagnetOn", 0.0f);
                }


            } else if (verticalMove == 0 && horizontalMove == 0) {
                magnetMoveSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            }
        }

    }

    public override void Vertical(float direction)
    {
        if(ready) {
            verticalMove = direction;
            magnet.transform.Translate(0, -direction * Time.deltaTime * speed, 0);
            // Do nothing
            if (direction != 0) {
                if (!tutorialOn) {
                    TriggerButtonAnimation.Instance.TurnAnimationOff("WASD");
                    TriggerButtonAnimation.Instance.moveMagnet = true;
                    tutorialOn = true;
                }
                magnetPlayerController.StopPulse();
                magnetMoveSound.setParameterValue("MagnetOn", 0.0f);
                magnetMoveSound.setParameterValue("MagnetMovement", 1.0f);
                magnetMoveSound.setParameterValue("MagnetLock", 0.0f);
                magnetMoveSound.setParameterValue("GrabSmall", 0.0f);
                magnetMoveSound.setParameterValue("GrabMedium", 0.0f);
                magnetMoveSound.setParameterValue("GrabBig", 0.0f);
                magnetMoveSound.setParameterValue("MagnetDrop", 0.0f);
                magnetMoveSound.setParameterValue("DropSmall", 0.0f);
                magnetMoveSound.setParameterValue("DropMedium", 0.0f);
                magnetMoveSound.setParameterValue("DropBig", 0.0f);
                magnetMoveSound.getPlaybackState(out playback);
                if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

                } else {
                    magnetMoveSound.start();
                    magnetMoveSound.setParameterValue("MagnetOn", 0.0f);
                }


            } else if (verticalMove == 0 && horizontalMove == 0) {
                magnetMoveSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            }
        }

    }

    public override void EnableController() {
        base.EnableController();
        if (!TriggerButtonAnimation.Instance.moveMagnet) {
            TriggerButtonAnimation.Instance.TurnAnimationOn("WASD");
        }
        ready = true;
    }

    public override void Look(Vector2 md)
    {
        // Do nothing
    }

    public override void LeftPress(bool key)
    {
        if (key)
        {
            if (!magnet.GetComponent<MagnetMove>().turnedOn) { 
                magnet.GetComponent<MagnetMove>().turnedOn = true;
                magneticSound.setParameterValue("MagneticSound", 1.0f);
                magneticSound.start();
            }
        }

    }

    public override void LeftClick(bool key)
    {
        if (key)
        {
            // released the button
            if (magnet.GetComponent<MagnetMove>().turnedOn)
                magnet.GetComponent<MagnetMove>().turnedOn = false;
            magneticSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }   
    }


    public override void RightClick(bool key)
    {
        if (key)
        {
            ready = false;
            magnetMoveSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            magnet.GetComponent<MagnetMove>().turnedOn = false;
            magnetPlayerController.PostFXExit();
            
        }
    }


}

