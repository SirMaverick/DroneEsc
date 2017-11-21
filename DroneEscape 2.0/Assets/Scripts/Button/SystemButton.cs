using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemButton : Button
{


    [SerializeField]
    private SystemPlayerController playerController;


    // private Camera cam;

    public override void Toggle()
    {
        /* if (!enabled)
         {
             playerControllerSupervisor.SwitchPlayerController(playerController);
             playerController.SwitchToNextCamera();
             enabled = true;
         }
         else
         {
             playerControllerSupervisor.SwitchPlayerControllerPrevious();
             enabled = false;
         }*/

        playerControllerSupervisor.SwitchPlayerController(playerController);
        // be sure to load the same one ;) yes ugly :D
        playerController.SwitchToPreviousCamera();
        playerController.SwitchToNextCamera();
    }


}