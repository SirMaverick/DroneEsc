using UnityEngine;

[System.Serializable]
class CameraPlayerController : AbstractPlayerController
{
    [SerializeField]
    private GuardFOV guardFOV;

    public override void EnableController()
    {
        
        camera.enabled = true;
        movementController.enabled = true;
        guardFOV.DisableGuard();
    }

    public override void DisableController()
    {
        
        camera.enabled = false;
        movementController.enabled = false;
        //guardFOV.EnableGuard();
    }
}
