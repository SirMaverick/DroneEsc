using UnityEngine;
using System.Collections;
    class DronePlayerController : AbstractPlayerController
    {
    [SerializeField]
    protected SkinnedMeshRenderer meshRenderer;

    private GameObject core;
    private GameObject cameraObject;
    private CorePlayerController corePlayerController;

    private GameObject lights;

    [SerializeField] private float maxDistance;

    bool nearBelt;
    [SerializeField] float force;

    [SerializeField] private GameObject objectPlacement;

    protected override void Start()
    {
        lights = GameObject.Find("LightCollection");
        corePlayerController = FindObjectOfType<CorePlayerController>();
        uiController = FindObjectOfType<DroneUIController>();
        cameraObject = corePlayerController.gameObject;
        core = FindObjectOfType<CoreObject>().gameObject;

        if (camera.enabled) { 
            GuardFOV[] guards = FindObjectsOfType<GuardFOV>();
            foreach (GuardFOV guard in guards)
            {
                // useless but prevents errors
                guard.ChangePlayer(gameObject);
            }
        }

        base.Start();
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
            SwitchLight();
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

    public Material GetMaterial()
    {
        return meshRenderer.material;
    }

    public void SwitchLight() {
        foreach(Light light in lights.GetComponentsInChildren<Light>()) {
            light.enabled = !light.enabled;
        }
    }

}

