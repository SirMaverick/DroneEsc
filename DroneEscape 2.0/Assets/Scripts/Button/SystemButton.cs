﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemButton : Button
{


    [SerializeField]
    private SystemPlayerController playerController;


    // private Camera cam;

    public override void Toggle()
    {
        if (!enabled)
        {
            playerControllerSupervisor.SwitchPlayerController(playerController);
            enabled = true;
        }
        else
        {
            playerControllerSupervisor.SwitchPlayerControllerPrevious();
            enabled = false;
        }
    }


}