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

    private float horizontalMove, verticalMove;

    public FMOD.Studio.EventInstance magnetSound;
    private FMOD.Studio.PLAYBACK_STATE playback;

    MagnetPlayerController magnetPlayerController;

    protected override void Start() {
        base.Start();
        magnetSound = RuntimeManager.CreateInstance("event:/SFX/Magnet/Magnet");
        RuntimeManager.AttachInstanceToGameObject(magnetSound, transform, GetComponent<Rigidbody>());
        magnetPlayerController = (MagnetPlayerController)playerController;
    }

    public override void Horizontal(float direction) {
        // Do nothing
        horizontalMove = direction;
        magnet.transform.Translate(direction * Time.deltaTime * speed, 0, 0);

        if (direction != 0) {
            magnetSound.setParameterValue("MagnetOn", 0.0f);
            magnetSound.setParameterValue("MagnetMovement", 1.0f);
            magnetSound.setParameterValue("MagnetLock", 0.0f);
            magnetSound.setParameterValue("GrabSmall", 0.0f);
            magnetSound.setParameterValue("GrabMedium", 0.0f);
            magnetSound.setParameterValue("GrabBig", 0.0f);
            magnetSound.setParameterValue("MagnetDrop", 0.0f);
            magnetSound.setParameterValue("DropSmall", 0.0f);
            magnetSound.setParameterValue("DropMedium", 0.0f);
            magnetSound.setParameterValue("DropBig", 0.0f);
            magnetSound.getPlaybackState(out playback);
            if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

            } else {
                magnetSound.start();
                magnetSound.setParameterValue("MagnetOn", 0.0f);
            }
            

        } else if (verticalMove == 0 && horizontalMove == 0) {
            magnetSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        }
    }

    public override void Vertical(float direction)
    {
        verticalMove = direction;
        magnet.transform.Translate(0, 0, direction * Time.deltaTime * speed);
        // Do nothing
        if (direction != 0) {
            magnetSound.setParameterValue("MagnetOn", 1.0f);
            magnetSound.setParameterValue("MagnetMovement", 1.0f);
            magnetSound.setParameterValue("MagnetLock", 0.0f);
            magnetSound.setParameterValue("GrabSmall", 0.0f);
            magnetSound.setParameterValue("GrabMedium", 0.0f);
            magnetSound.setParameterValue("GrabBig", 0.0f);
            magnetSound.setParameterValue("MagnetDrop", 0.0f);
            magnetSound.setParameterValue("DropSmall", 0.0f);
            magnetSound.setParameterValue("DropMedium", 0.0f);
            magnetSound.setParameterValue("DropBig", 0.0f);
            if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

            } else {
                magnetSound.start();
                magnetSound.setParameterValue("MagnetOn", 0.0f);
            }
            

        } else if (verticalMove == 0 && horizontalMove == 0) {
            magnetSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            
        }
    }

    public override void Look(Vector2 md)
    {
        // Do nothing
    }

    public override void LeftPress(bool key)
    {
        if (key)
        {
            if(!magnet.GetComponent<MagnetMove>().turnedOn)
                magnet.GetComponent<MagnetMove>().turnedOn = true;
        }

    }

    public override void LeftClick(bool key)
    {
        if (key)
        {
            // released the button
            if (magnet.GetComponent<MagnetMove>().turnedOn)
                magnet.GetComponent<MagnetMove>().turnedOn = false;
        }
    }


    public override void RightClick(bool key)
    {
        if (key)
        {
            magnet.GetComponent<MagnetMove>().turnedOn = false;
            magnetPlayerController.PostFXExit();
            magnetSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }


}

