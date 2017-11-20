using UnityEngine;
class SystemCameraMovementController : CameraMovementController
{
    private SystemMovementController systemMovementController;
    private EmptyDrone lastDroneHit;
    private bool hitEmptyDrone = false;
    private bool pickupDrone = false;
    private bool pickingUpDrone = false;

    [SerializeField] private GameObject antagonistArm;
    [SerializeField] private GameObject targetPoint;
    [SerializeField] private float yPositionArm;

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

                if (pickupDrone)
                {
                    if (!pickingUpDrone)
                    {
                        PickUpDrone(newHit);
                        pickingUpDrone = true;
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

    private void PickUpDrone(EmptyDrone drone)
    {
        GameObject target = Instantiate(targetPoint, new Vector3(drone.transform.position.x, drone.transform.position.y + 10, drone.transform.position.z), Quaternion.identity);
        //GameObject go = Instantiate(animationCamera, playerGameObject.GetComponentInChildren(typeof(Camera)).transform.position, playerGameObject.GetComponentInChildren(typeof(Camera)).transform.rotation);
        //go.GetComponent<ArmCameraRotation>().target = target.transform;
        Instantiate(antagonistArm, new Vector3(drone.transform.position.x, yPositionArm, drone.transform.position.z), Quaternion.identity);
    }
}