using UnityEngine;
class SystemCameraMovementController : CameraMovementController
{
    private SystemMovementController systemMovementController;
    private CoreDrone lastDroneHit;
    private bool hitEmptyDrone = false;
    private bool pickupDrone = false;
    private bool pickingUpDrone = false;

    [SerializeField] private SystemArm systemArm;

    [SerializeField]
    private Camera ownCamera;

    public void SetSystemMovementController(SystemMovementController smc)
    {
        systemMovementController = smc;
    }

    public override void RightClick(bool key)
    {
        systemMovementController.RightClick(key);
    }

    public override void LeftClick(bool key)
    {
        pickupDrone = key;
    }

    public override void Use(bool key)
    {
        systemMovementController.Use(key);
    }

    protected override void Update()
    {
        RaycastHit hit;

        Vector3 fwd = ownCamera.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(ownCamera.transform.position, fwd, out hit))
        {
            if (hit.collider.tag == "CoreDrone")
            {
                hitEmptyDrone = true;
                CoreDrone newHit = hit.collider.gameObject.GetComponent<CoreDrone>();
                if (lastDroneHit != newHit)
                {
                    newHit.StopLookingAt();
                    lastDroneHit = newHit;
                    lastDroneHit.LookingAt();
                }

                if (pickupDrone)
                {
                    PickUpDrone(newHit);
                    SystemPlayerController spc = FindObjectOfType<SystemPlayerController>();
                    spc.SetCoreDrone(newHit);
                }

            }
            else
            {
                if (hitEmptyDrone)
                {
                    NoHit();
                }

            }
        }
        else
        {
            if (hitEmptyDrone)
            {
                NoHit();
            }

        }
        base.Update();
    }

    private void NoHit()
    {
        lastDroneHit.StopLookingAt();
        lastDroneHit = null;
        hitEmptyDrone = false;
    }

    public override void DisableController()
    {
        if (lastDroneHit != null)
        {
            lastDroneHit.StopLookingAt();
        }
        base.DisableController();
    }

    private void PickUpDrone(CoreDrone drone)
    {
        if (drone.IsAllowedToBePickup())
        {
            systemArm.MoveTo(drone);
        }

    }
}