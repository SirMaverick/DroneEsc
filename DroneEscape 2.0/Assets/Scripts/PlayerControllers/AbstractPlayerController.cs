using UnityEngine;
[System.Serializable]
public abstract class AbstractPlayerController : MonoBehaviour
{
    [SerializeField]
    protected MeshRenderer meshRenderer;
    [SerializeField]
    protected Camera camera;

    [SerializeField]
    protected MovementController movementController;

    private void Start()
    {
        if (camera.enabled)
        {
            PlayerControllerSupervisor.GetInstance().SetCurrentPlayerController(this);
        }
    }

    public virtual void EnableController()
    {
        // meshRenderer.enabled = true;
        camera.enabled = true;
        movementController.enabled = true;
    }

    public virtual void DisableController()
    {
        // meshRenderer.enabled = false;
        camera.enabled = false;
        movementController.enabled = false;
    }

}

