using UnityEngine;

[System.Serializable]
class SystemCameraPlayerController : AbstractPlayerController
{
    private SystemCameraMovementController movementControllerSC;

    public void Start()
    {
        movementControllerSC = (SystemCameraMovementController)base.movementController;
    }

    public void SetSystemMovementController(SystemMovementController smc)
    {
        // really shit :(
        movementControllerSC.SetSystemMovementController(smc);
    }

    public Camera GetCamera()
    {
        return camera;
    }
}
