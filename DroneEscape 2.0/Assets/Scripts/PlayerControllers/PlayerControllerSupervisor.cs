using System;
using UnityEngine;
public class PlayerControllerSupervisor
{
    private static PlayerControllerSupervisor instance = null;

    [SerializeField]
    private AbstractPlayerController currentPlayerController;
    private AbstractPlayerController previousPlayerController;

    public static PlayerControllerSupervisor GetInstance()
    {
        if(instance == null)
        {
            instance = new PlayerControllerSupervisor();
        }
        return instance;
    }

    public AbstractPlayerController GetPreviousPlayerController()
    {
        return previousPlayerController;
    }

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

    public void SetCurrentPlayerController(AbstractPlayerController apc)
    {
        currentPlayerController = apc;
    }


}
