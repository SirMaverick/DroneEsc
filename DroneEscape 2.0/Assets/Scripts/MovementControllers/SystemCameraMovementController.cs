using UnityEngine;
class SystemCameraMovementController : CameraMovementController
{
    private SystemMovementController systemMovementController;
    private EmptyDrone lastDroneHit;
    private bool hitEmptyDrone = false;

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
        systemMovementController.LeftClick(key);
    }

    public override void Use(bool key)
    {
        systemMovementController.Use(key);
    }

    public void Update()
    {
        RaycastHit hit;

        Vector3 fwd = ownCamera.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(ownCamera.transform.position, fwd, out hit))
        {
            if (hit.collider.tag == "Drone")
            {
                hitEmptyDrone = true;
                EmptyDrone newHit = hit.collider.gameObject.GetComponent<EmptyDrone>();
                if (lastDroneHit != newHit)
                {
                    newHit.StopLookingAt();
                    lastDroneHit = newHit;
                    lastDroneHit.LookingAt();
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
}