using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
class NewSystemPlayerController : AbstractPlayerController, LiftDroneCallBack
{


    protected PlayerControllerSupervisor playerControllerSupervisor;

    private SystemEnergyController energyController;

    [SerializeField] private Transform startPosition;
    private GameObject systemArm;

    private CoreDrone coreDrone;
    private CoreDrone[] coreDrones;

    protected override void Start()
    {
        playerControllerSupervisor = PlayerControllerSupervisor.GetInstance();
        uiController = FindObjectOfType<SystemUIController>();

        systemArm = FindObjectOfType<SystemArm>().gameObject;
        energyController = GetComponent<SystemEnergyController>();

        coreDrones = FindObjectsOfType<CoreDrone>();

        base.Start();

    }

    public override void EnableController()
    {
        base.EnableController();
        energyController.EnableController();
        EnableAllCoreDrones();
        MoveArmToPosition(startPosition.position);
        camera.GetComponent<CameraSlowFollow>().enabled = true;
    }

    public override void DisableController()
    {
        // Cannot disable this controller
    }

    public void SetCoreDrone(CoreDrone coreDrone)
    {
        this.coreDrone = coreDrone;
    }

    public void GiveEnergy()
    {
        energyController.AddEnergyFromCore(coreDrone);
    }

    public void GiveEnergy(CoreDrone coreDrone)
    {
        this.coreDrone = coreDrone;
        GiveEnergy();
    }

    private void EnableAllCoreDrones()
    {
        foreach(CoreDrone cDrone in coreDrones)
        {
            cDrone.enabled = true;
        }
    }

    private void MoveArmToPosition(Vector3 position)
    {
        systemArm.transform.position = position;
    }

    public void CallBack()
    {
        coreDrone.GetComponent<CoreDroneAnimation>().CaughtByArm();
    }
}
