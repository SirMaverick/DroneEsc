using UnityEngine;


public class CameraButton : Button
{
    

    [SerializeField]
    private CameraPlayerController playerController;

    [SerializeField]
    private GuardFOV cameraGuard;

    
   // private Camera cam;

    public override void Toggle()
    {
        /* if (!enabled)
         {
             playerControllerSupervisor.SwitchPlayerController(playerController);
             cameraGuard.enabled = false;
             enabled = true;
         }
         else
         {
             playerControllerSupervisor.SwitchPlayerControllerPrevious();
             cameraGuard.enabled = true;
             enabled = false;
         }*/

        playerControllerSupervisor.SwitchPlayerController(playerController);
    }


}
