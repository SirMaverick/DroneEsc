using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemButton : Button
{


    [SerializeField]
    private NewSystemPlayerController playerController;


    // private Camera cam;

    public override void Toggle()
    {
        playerControllerSupervisor.SwitchPlayerController(playerController);
    }


}