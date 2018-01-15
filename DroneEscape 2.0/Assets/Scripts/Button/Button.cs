using UnityEngine;


public abstract class Button : MonoBehaviour, Selectable
{
    private Material lastMaterialHit;

    protected PlayerControllerSupervisor playerControllerSupervisor;
    [SerializeField] private Transform dronePosition;

    private void Start()
    {
        playerControllerSupervisor = PlayerControllerSupervisor.GetInstance();
    }

    public void LookingAt()
    {
        // emision
        lastMaterialHit = gameObject.GetComponent<MeshRenderer>().material;
        lastMaterialHit.EnableKeyword("_EMISSION");
    }

    public void StopLookingAt()
    {
        lastMaterialHit.DisableKeyword("_EMISSION");
    }

    public abstract void Toggle();

    public Vector3 GetDronePosition()
    {
        return dronePosition.position;
    }
}
