using UnityEngine;

[System.Serializable]
class CameraPlayerController : AbstractPlayerController
{

    public override void EnableController()
    {
        
        camera.enabled = true;
    }

    public override void DisableController()
    {
        
        camera.enabled = false;
    }
}
