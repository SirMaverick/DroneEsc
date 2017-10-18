using UnityEngine;
[System.Serializable]
abstract class AbstractPlayerController
{
    [SerializeField]
    protected MeshCollider meshCollider;
    [SerializeField]
    protected Camera camera;

    public virtual void EnableController()
    {
        meshCollider.enabled = true;
        camera.enabled = true;
    }

    public virtual void DisableController()
    {
        meshCollider.enabled = false;
        camera.enabled = false;
    }

}

