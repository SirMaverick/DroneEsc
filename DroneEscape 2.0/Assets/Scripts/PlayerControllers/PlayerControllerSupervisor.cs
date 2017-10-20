using UnityEngine;
public class PlayerControllerSupervisor : MonoBehaviour
{
    [SerializeField]
    private AbstractPlayerController currentPlayerController;
    private AbstractPlayerController previousPlayerController;

    public void SwitchPlayerController(AbstractPlayerController apc)
    {
        apc.EnableController();
        currentPlayerController.DisableController();
        previousPlayerController = currentPlayerController;
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
