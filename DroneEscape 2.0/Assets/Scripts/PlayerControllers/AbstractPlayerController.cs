using UnityEngine;
[System.Serializable]
abstract class AbstractPlayerController
{
    [SerializeField]
    protected MeshCollider meshCollider;
    [SerializeField]
    protected Camera camera;

    [SerializeField]
    protected MovementController movementController;

    public virtual void EnableController()
    {
        meshCollider.enabled = true;
        camera.enabled = true;
        movementController.enabled = true;
    }

    public virtual void DisableController()
    {
        meshCollider.enabled = false;
        camera.enabled = false;
        movementController.enabled = false;
    }

}

