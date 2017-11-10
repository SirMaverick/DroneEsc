using UnityEngine;
class SystemCameraMovementController : CameraMovementController
{
    private SystemMovementController systemMovementController;

    public void SetSystemMovementController(SystemMovementController smc)
    {
        systemMovementController = smc;
    }

    public override void RightClick(bool key)
    {
        systemMovementController.RightClick(key);
    }

    public override void LeftClick(bool key)
    {
        systemMovementController.LeftClick(key);
    }
}