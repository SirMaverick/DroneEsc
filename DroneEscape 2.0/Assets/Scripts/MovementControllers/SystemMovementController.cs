using UnityEngine;
class SystemMovementController : MovementController
{
    private int cameraId = -1;
    private SystemCameraPlayerController[] cameraPCS;
    private SystemPlayerController playerController;



    public override void Horizontal(float direction)
    {

    }

    public override void Vertical(float direction)
    {

    }

    public override void Look(Vector2 md)
    {

    }

    public override void RightClick(bool key) {
        cameraId++;
        if(cameraId >= cameraPCS.Length)
        {
            cameraId = 0;
        }
        cameraPCS[cameraId].SetSystemMovementController(this);
        playerControllerSupervisor.SwitchPlayerController(cameraPCS[cameraId]);
    }

    public override void LeftClick(bool key) {
        cameraId--;
        if (cameraId < 0)
        {
            cameraId = cameraPCS.Length-1;
        }
        playerControllerSupervisor.SwitchPlayerController(cameraPCS[cameraId]);
    }

    public override void Use(bool key)
    {
        if (key)
        {
            // switch to the playerController which used to be used before this one
            playerControllerSupervisor.SwitchPlayerController(playerController.GetPreviousPlayerController());
        }
    }


}

