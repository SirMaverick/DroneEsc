﻿using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
class SystemPlayerController : AbstractPlayerController
{
    [SerializeField]
    private SystemCameraPlayerController[] cameraPCS;

    private AbstractPlayerController previousPlayerController;
    private SystemMovementController movementControllerSM;

    protected PlayerControllerSupervisor playerControllerSupervisor;

    [SerializeField]
    private SystemEnergyController energyController;

    private CoreDrone coreDrone;

    private CoreDrone[] coreDrones;

    protected override void Start()
    {
        // kind of ugly to hide it and cast it
        movementControllerSM = (SystemMovementController) movementController;
        playerControllerSupervisor = PlayerControllerSupervisor.GetInstance();
        uiController = FindObjectOfType<SystemUIController>();

        coreDrones = FindObjectsOfType<CoreDrone>();
    }

    public override void EnableController()
    {
        //camera = cameraPCS[0].GetCamera();
        previousPlayerController = playerControllerSupervisor.GetCurrentPlayerController();
        //base.EnableController();
        movementController.enabled = true;
        uiController.EnableController();
        energyController.EnableController();
        EnableAllCoreDrones();
    }

    public override void DisableController()
    {
        //base.DisableController();
        movementController.enabled = false;
        //uiController.DisableController();
    }

    public override void SwitchToNextCamera()
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

    public void SetCoreDrone(CoreDrone coreDrone)
    {
        this.coreDrone = coreDrone;
    }

    public void GiveEnergy()
    {
        energyController.AddEnergyFromCore(coreDrone);
    }

    private void EnableAllCoreDrones()
    {
        foreach(CoreDrone cDrone in coreDrones)
        {
            cDrone.enabled = true;
        }
    }
}
