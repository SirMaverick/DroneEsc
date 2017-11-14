using UnityEngine;
using System.Collections;
    class DronePlayerController : AbstractPlayerController
    {
    private GameObject core;
    private GameObject cameraObject;
    private CorePlayerController corePlayerController;

    [SerializeField] private float maxDistance;

    bool nearBelt;
    [SerializeField] float force;

    [SerializeField] private GameObject objectPlacement;

    protected void Start()
    {
        corePlayerController = FindObjectOfType<CorePlayerController>();
        cameraObject = corePlayerController.gameObject;
        core = FindObjectOfType<CoreObject>().gameObject;
    }

    public override void EnableController()
    {
        // dont see yourself 
        meshRenderer.enabled = false;
        base.EnableController();
    }

    public override void DisableController()
    {
        // when in another body you can see the drone again
         meshRenderer.enabled = true;
        base.DisableController();
    }

    public void Throw()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance))
        {
            if (hit.collider.tag == "ConveyerBelt")
            {
                nearBelt = true;
            }
            else
            {
                nearBelt = false;
            }
        }
        core.transform.position = objectPlacement.transform.position;
        if (!nearBelt)
        {
            cameraObject.transform.position = core.transform.position;

            // actual throwing.
            core.GetComponent<Rigidbody>().isKinematic = false;
            core.GetComponent<Rigidbody>().velocity = Vector3.zero;
            core.GetComponent<Rigidbody>().AddForce(transform.Find("DroneCamera").TransformDirection(Vector3.forward) * force, ForceMode.Impulse);
            
            
        }
        else
        {
            core.transform.position = hit.transform.position + new Vector3(0, 1.0f, 0);
            cameraObject.transform.position = core.transform.position;
           
        }
        core.transform.parent = null;
        // Let the guards know which GameObject is the player (only keeping track of one object for effeciency)
       // private void UpdateGuards()
        {
            GuardFOV[] guards = FindObjectsOfType<GuardFOV>();
            foreach (GuardFOV guard in guards)
            {
                guard.ChangePlayer(core);
            }
        }

    }



}

