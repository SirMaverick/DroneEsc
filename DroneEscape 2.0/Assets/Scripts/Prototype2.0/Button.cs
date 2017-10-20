using UnityEngine;


public abstract class Button : MonoBehaviour
{
    private Material lastMaterialHit;
    [SerializeField]
    protected bool enabled = false;

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

    public virtual void Toggle()
    {
        // on / off
    }
}
