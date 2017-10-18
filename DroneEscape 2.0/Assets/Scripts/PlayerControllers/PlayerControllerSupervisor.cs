using UnityEngine;
class PlayerControllerSupervisor : MonoBehaviour
{
    private AbstractPlayerController currentPlayerController;
    private AbstractPlayerController previousPlayerController;

    public void SwitchPlayerController(AbstractPlayerController apc)
    {
        apc.EnableController();
        currentPlayerController.DisableController();
        currentPlayerController = apc;
    }

    public void SwitchPlayerControllerPrevious()
    {
        SwitchPlayerController(previousPlayerController);
    }

    public AbstractPlayerController GetCurrentPlayerController()
    {
        return currentPlayerController;
    }
}
