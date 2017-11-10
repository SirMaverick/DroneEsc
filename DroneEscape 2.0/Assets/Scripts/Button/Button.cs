using UnityEngine;


public abstract class Button : MonoBehaviour
{
    private Material lastMaterialHit;
    [SerializeField]
    protected bool enabled = false;

    protected PlayerControllerSupervisor playerControllerSupervisor;

    private void Start()
    {
        playerControllerSupervisor = FindObjectOfType<PlayerControllerSupervisor>();
    }

    public void LookingAt()
    {
        // emision
        lastMaterialHit = gameObject.GetComponent<MeshRenderer>().material;
        lastMaterialHit.EnableKeyword("_EMISSION");
        // ugly kill me
        if (Input.GetKeyDown(KeyCode.E))
        {
            Toggle();
        }
    }

    public void StopLookingAt()
    {
        lastMaterialHit.DisableKeyword("_EMISSION");
    }

    public abstract void Toggle();
}
