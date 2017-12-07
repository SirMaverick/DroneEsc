using UnityEngine;
[System.Serializable]
public abstract class AbstractPlayerController : MonoBehaviour
{
    protected Camera camera;

    [SerializeField]
    protected Camera[] cameras;
    protected int cameraId = 0;

    [SerializeField]
    protected MovementController movementController;

    [SerializeField]
    protected UIController uiController;

    protected virtual void Start()
    {
        camera = cameras[0];
        if (camera.enabled)
        {
            PlayerControllerSupervisor.GetInstance().SetCurrentPlayerController(this);
        }
    }

    public virtual void EnableController()
    {
        // meshRenderer.enabled = true;
        camera = cameras[cameraId];
        camera.enabled = true;
        movementController.enabled = true;
        movementController.EnableController();
        camera.GetComponent<AudioListener>().enabled = true;
        // Other possiblity was to override this function
        if (uiController != null)
        {
            uiController.EnableController();
        }
    }

    public virtual void DisableController()
    {
        // meshRenderer.enabled = false;
        camera = cameras[cameraId];
        camera.enabled = false;
        movementController.DisableController();
        movementController.enabled = false;
        camera.GetComponent<AudioListener>().enabled = false;
        if (uiController != null)
        {
            uiController.DisableController();
        }
    }

    public virtual void SwitchToNextCamera()
    {
        int previousCameraId = cameraId;
        cameraId++;
        if (cameraId >= cameras.Length)
        {
            cameraId = 0;
        }
        cameras[cameraId].enabled = true;
        if (previousCameraId != cameraId)
        {
            cameras[previousCameraId].enabled = false;
        }
    }

}

