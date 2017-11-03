using System;
using System.Collections.Generic;
using UnityEngine;
public class MagnetMovementController : MovementController
{
    [SerializeField]
    private GameObject magnet;
    public Camera surveillanceCamera;
    public float speed = 2;
    public bool coreInside;
    public GameObject drone;

    public override void Horizontal(float direction)
    {
        magnet.transform.Translate(direction * Time.deltaTime * speed, 0, 0);
    }

    public override void Vertical(float direction)
    {
        magnet.transform.Translate(0, 0, direction * Time.deltaTime * speed);
    }

    public override void Look(Vector2 md)
    {
        // Do nothing
    }

    public override void RightClick(bool key)
    {
        if (key)
        {
            magnet.GetComponent<MagnetMove>().turnedOn = true;
        }
        else
        {
            magnet.GetComponent<MagnetMove>().turnedOn = false;
        }

    }


    public override void Use(bool key)
    {
        if (key)
        {
            playerControllerSupervisor.SwitchPlayerControllerPrevious();
        }
    }


}

