using UnityEngine;
using System.Collections;
    class DronePlayerController : AbstractPlayerController
    {
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject coreCamera;
    
    [SerializeField] private float maxDistance;

    bool nearBelt;
    [SerializeField] float force;

    [SerializeField] private CorePlayerController corePlayerController;
    [SerializeField] private GameObject objectPlacement;

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
            //cameraObject.GetComponent<CoreCamera>().core = core.gameObject;

            // actual throwing
            core.GetComponent<Rigidbody>().AddForce(transform.Find("DroneCamera").TransformDirection(Vector3.forward) * force, ForceMode.Impulse);
            core.transform.parent = null;
            PlayerControllerSupervisor playerControllerSupervisor = FindObjectOfType<PlayerControllerSupervisor>();
            playerControllerSupervisor.SwitchPlayerController(corePlayerController);
            
        }
        else
        {
            core.transform.position = hit.transform.position + new Vector3(0, 1.0f, 0);
            cameraObject.transform.position = core.transform.position;
  //          cameraObject.GetComponent<CoreCamera>().core = core.gameObject;
            core.transform.parent = null;
            PlayerControllerSupervisor playerControllerSupervisor = FindObjectOfType<PlayerControllerSupervisor>();
            playerControllerSupervisor.SwitchPlayerController(corePlayerController);



        }
        
    }



}

