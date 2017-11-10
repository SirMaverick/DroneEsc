using UnityEngine;
public class PlayerControllerSupervisor
{
    private static PlayerControllerSupervisor instance = null;

    [SerializeField]
    private static AbstractPlayerController currentPlayerController = null;
    private AbstractPlayerController previousPlayerController;

    public static PlayerControllerSupervisor GetInstance()
    {
        if(instance == null)
        {
            instance = new PlayerControllerSupervisor();
        }
        return instance;
    }

    public void SwitchPlayerController(AbstractPlayerController apc)
    {
        apc.EnableController();
        if(currentPlayerController == null)
        {
            currentPlayerController = Camera.main.GetComponentInParent<AbstractPlayerController>();
        }
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
