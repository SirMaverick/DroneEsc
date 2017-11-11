﻿using UnityEngine;

[System.Serializable]
class SystemPlayerController : AbstractPlayerController
{
    private int cameraId = -1;
    private SystemCameraPlayerController[] cameraPCS;

    private AbstractPlayerController previousPlayerController;
    private new SystemMovementController movementController;

    protected PlayerControllerSupervisor playerControllerSupervisor;


    public void Start()
    {
        // kind of ugly to hide it and cast it
        movementController = (SystemMovementController) base.movementController;
        playerControllerSupervisor = PlayerControllerSupervisor.GetInstance();
    }

    public override void EnableController()
    {
        camera = cameraPCS[0].GetCamera();
        previousPlayerController = playerControllerSupervisor.GetPreviousPlayerController();
        base.EnableController();
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
        cameraPCS[cameraId].SetSystemMovementController(movementController);
        playerControllerSupervisor.SwitchPlayerController(cameraPCS[cameraId]);
    }

    public void SwitchToPreviousPlayerController()
    {
        playerControllerSupervisor.SwitchPlayerController(previousPlayerController);
    }
}
