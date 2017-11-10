using UnityEngine;

[System.Serializable]
class SystemPlayerController : AbstractPlayerController
{
    private AbstractPlayerController previousPlayerController;

    public override void EnableController()
    {
        previousPlayerController = PlayerControllerSupervisor.GetInstance().GetPreviousPlayerController();
        base.EnableController();
    }

    public override void DisableController()
    {
        base.DisableController();
    }

    public AbstractPlayerController GetPreviousPlayerController()
    {
        return previousPlayerController;
    }
}
