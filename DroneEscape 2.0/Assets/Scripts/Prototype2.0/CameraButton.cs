using UnityEngine;


public class CameraButton : Button
{
    private PlayerControllerSupervisor playerControllerSupervisor;

    [SerializeField]
    private CameraPlayerController playerController = new CameraPlayerController();

    
   // private Camera cam;


    private void Start()
    {
        playerControllerSupervisor = FindObjectOfType<PlayerControllerSupervisor>();
    }

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
