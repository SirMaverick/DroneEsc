using UnityEngine;
using System.Collections;
    class DronePlayerController : AbstractPlayerController
    {
    [SerializeField]
    protected SkinnedMeshRenderer droneMeshRenderer;
    [SerializeField]
    protected SkinnedMeshRenderer armsMeshRenderer;

    private GameObject core;
    private GameObject cameraObject;
    private CorePlayerController corePlayerController;

    [SerializeField] private float maxDistance;

    bool nearBelt;
    [SerializeField] private bool startAsCore;
    [SerializeField] float force;

    [SerializeField] private GameObject objectPlacement;

    [SerializeField]
    private DroneArmsAnimation animationDroneArms;
    [SerializeField]
    private DroneAnimation animationDrone;



    [SerializeField] private float speed;
    [SerializeField] private bool freezeDroneButton = true;

    private EmptyDrone emptyDrone;

    protected override void Start()
    {

        base.Start();

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
        emptyDrone = GetComponent<EmptyDrone>();

    }

    public override void EnableController()
    {
        // dont see yourself 
        droneMeshRenderer.enabled = false;
        armsMeshRenderer.enabled = true;
        emptyDrone.Disable();
        emptyDrone.enabled = false;

        base.EnableController();
        if (ExitMachine())
        {
            // dont move when displaying exiting machine animation
            movementController.enabled = false;
        }
    }

    public override void DisableController()
    {
        // when in another body you can see the drone again
        armsMeshRenderer.enabled = false;
        droneMeshRenderer.enabled = true;
        emptyDrone.enabled = true;
        base.DisableController();
    }

    public void ShootReady()
    {
        animationDroneArms.ShootReady();
    }
    public void Throw()
    {
        animationDroneArms.Shoot();
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
            cameraObject.transform.rotation = transform.rotation;

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
        animationDrone.HasShot();
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

    

    public void DisableHeartBeat()
    {
        droneMeshRenderer.material.SetFloat("_Heartbeat", 0);
    }

    public void MoveVertically(float value)
    {
        if (value > 0)
        {
            animationDroneArms.WalkForwards();
        }
        else if (value < 0)
        {
            animationDroneArms.WalkBackwards();
        }
        else
        {
            animationDroneArms.WalkNotVertically();
        }
        gameObject.transform.Translate(0, 0, value * Time.deltaTime * speed);
    }

    public void MoveHorizontally(float value)
    {
        gameObject.transform.Translate(value * Time.deltaTime * speed, 0, 0);
    }
    // button just for temp action
    private Button button;
    public void ActivateButton(Button button)
    {
        if (freezeDroneButton)
        {
            movementController.DisableController();
            movementController.enabled = false;
        }
        animationDroneArms.InsertIntoMachine();
        this.button = button;
    }

    public void AnimationInsertIntoMachineDone()
    {
        if (freezeDroneButton)
        {
            movementController.EnableController();
            movementController.enabled = true;
        }
        button.Toggle();

    }

    public bool ExitMachine()
    {
        // if it is not in the machine nothing will happen
        return animationDroneArms.ExitOutOfMachine();
    }

    public void AnimationExitMachineDone()
    {
        // can move again
        movementController.enabled = true;
    }
}

