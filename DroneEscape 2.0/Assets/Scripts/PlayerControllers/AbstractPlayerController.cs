using UnityEngine;
[System.Serializable]
public abstract class AbstractPlayerController : MonoBehaviour
{
    [SerializeField]
    protected Camera camera;

    [SerializeField]
    protected MovementController movementController;

    protected virtual void Start()
    {
        if (camera.enabled)
        {
            PlayerControllerSupervisor.GetInstance().SetCurrentPlayerController(this);
            GuardFOV[] guards = FindObjectsOfType<GuardFOV>();
            foreach (GuardFOV guard in guards)
            {
                // useless but prevents errors
                guard.ChangePlayer(gameObject);
            }
        }
    }

    public virtual void EnableController()
    {
        // meshRenderer.enabled = true;
        camera.enabled = true;
        movementController.enabled = true;
        movementController.EnableController();
        camera.GetComponent<AudioListener>().enabled = true;
    }

    public virtual void DisableController()
    {
        // meshRenderer.enabled = false;
        camera.enabled = false;
        movementController.DisableController();
        movementController.enabled = false;
        camera.GetComponent<AudioListener>().enabled = false;
    }

}

