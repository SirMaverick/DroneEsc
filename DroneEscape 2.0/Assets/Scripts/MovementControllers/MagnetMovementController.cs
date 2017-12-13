using System;
using System.Collections.Generic;
using UnityEngine;
public class MagnetMovementController : MovementController
{
    [SerializeField]
    private GameObject magnet;
    public float speed = 2;
    public bool coreInside;
    public GameObject drone;

    public FMOD.Studio.EventInstance magnetSound;
    private FMOD.Studio.PLAYBACK_STATE playback;

    public override void Horizontal(float direction)
    {
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
            magnetSound.setParameterValue("ElevatorStart", 0.0f);
        }

        magnet.transform.Translate(direction * Time.deltaTime * speed, 0, 0);
    }

    public override void Vertical(float direction)
    {

        magnetSound.setParameterValue("ElevatorStart", 1.0f);
        magnetSound.setParameterValue("ElevatorLoop", 1.0f);
        magnetSound.setParameterValue("ElevatorStop", 0.0f);
        if (playback == FMOD.Studio.PLAYBACK_STATE.PLAYING) {

        } else {
            magnetSound.start();
            magnetSound.setParameterValue("ElevatorStart", 0.0f);
        }
        magnet.transform.Translate(0, 0, direction * Time.deltaTime * speed);
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


    public override void Use(bool key)
    {
        if (key)
        {
            playerControllerSupervisor.SwitchPlayerControllerPrevious();
            magnet.GetComponent<MagnetMove>().turnedOn = false;
        }
    }


}

