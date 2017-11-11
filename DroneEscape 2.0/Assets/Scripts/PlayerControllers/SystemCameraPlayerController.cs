using UnityEngine;

[System.Serializable]
class SystemCameraPlayerController : CameraPlayerController
{
    private new SystemCameraMovementController movementController;

    public void Start()
    {
        // kind of ugly to hide it and cast it
        movementController = (SystemCameraMovementController)base.movementController;
    }

    public void SetSystemMovementController(SystemMovementController smc)
    {

        movementController.SetSystemMovementController(smc);
    }

    public Camera GetCamera()
    {
        return camera;
    }
}
