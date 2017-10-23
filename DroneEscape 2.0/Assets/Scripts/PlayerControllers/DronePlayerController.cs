using UnityEngine;
    class DronePlayerController : AbstractPlayerController
    {
    public override void EnableController()
    {
        // dont see yourself 
        meshRenderer.enabled = false;
        base.EnableController();
    }

    public override void DisableController()
    {
        // when in another body you can see the drone again
         meshRenderer.enabled = true;
        base.DisableController();
    }
}

