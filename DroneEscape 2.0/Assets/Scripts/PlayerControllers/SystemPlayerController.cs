﻿using UnityEngine;

[System.Serializable]
class SystemPlayerController : AbstractPlayerController
{
    private int cameraId = 0;
    [SerializeField]
    private SystemCameraPlayerController[] cameraPCS;

    private AbstractPlayerController previousPlayerController;
    private SystemMovementController movementControllerSM;

    protected PlayerControllerSupervisor playerControllerSupervisor;


    public void Start()
    {
        // kind of ugly to hide it and cast it
        movementControllerSM = (SystemMovementController) movementController;
        playerControllerSupervisor = PlayerControllerSupervisor.GetInstance();
    }

    public override void EnableController()
    {
        //camera = cameraPCS[0].GetCamera();
        previousPlayerController = playerControllerSupervisor.GetCurrentPlayerController();
        //base.EnableController();
        movementController.enabled = true;
        SwitchToSystemCamera();
    }

    public override void DisableController()
    {
        base.DisableController();
    }

    public void SwitchToNextCamera()
    {
        cameraId++;
        if (cameraId >= cameraPCS.Length)
        {
            cameraId = 0;
        }
        SwitchToSystemCamera();
    }

    public void SwitchToPreviousCamera()
    {
        cameraId--;
        if (cameraId < 0)
        {
            cameraId = cameraPCS.Length - 1;
        }
        SwitchToSystemCamera();
    }

    private void SwitchToSystemCamera()
    {
        // stupid movementcontroller ;(
        cameraPCS[cameraId].SetSystemMovementController(movementControllerSM);
        playerControllerSupervisor.SwitchPlayerController(cameraPCS[cameraId]);
    }

    public void SwitchToPreviousPlayerController()
    {
        playerControllerSupervisor.SwitchPlayerController(previousPlayerController);
    }
}
