using UnityEngine;
class DroneMovementController : MovementController
{

    [SerializeField] private float speed;
    private GameObject coreCamera;
    private GameObject ownCamera;
    private float xMovement;
    private float zMovement;
    //public bool throwCore;


    private Vector2 mouseLook;
    private Vector2 smoothV;
    [SerializeField] private float minClamp;
    [SerializeField] private float maxClamp;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    [SerializeField] private float selectionRange = 1.5f;

    GameObject character;

    private Material lastMaterialHit;
    private bool hitButton = false;

    private Button button;

    
    private CorePlayerController corePlayerController;

    [SerializeField]
    private DronePlayerController dronePlayerController;
    [SerializeField]
    private DroneArmsAnimation animationDroneArms;


    // Use this for initialization
    private new void Start()
    {
        base.Start();
        character = gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        ownCamera = transform.Find("DroneCamera").gameObject;
        corePlayerController = FindObjectOfType<CorePlayerController>();
        coreCamera = corePlayerController.gameObject;
    }

    public override void Look(Vector2 md)
    {
        RaycastHit hit;
        Vector3 fwd = ownCamera.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(ownCamera.transform.position, fwd, out hit, selectionRange))
        {
            if (hit.collider.tag == "Button")
            {
                hitButton = true;
                button = hit.collider.gameObject.GetComponent<Button>();
                button.LookingAt();

            }
        }
        else
        {
            if (hitButton)
            {
                button.StopLookingAt();
                hitButton = false;
            }

        }
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minClamp, maxClamp);

        ownCamera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }

    public override void Vertical(float value)
    {
        if(value > 0)
        {
            animationDroneArms.WalkForwards();
        }
        else if(value < 0)
        {
            animationDroneArms.WalkBackwards();
        }
        else
        {
            animationDroneArms.WalkNotVertically();
        }
        gameObject.transform.Translate(0, 0, value * Time.deltaTime * speed);
    }

    public override void Horizontal(float value)
    {
        gameObject.transform.Translate(value * Time.deltaTime * speed, 0, 0);
    }

    public override void Use(bool key)
    {
        if (key)
        {
           // button.Toggle();
        }
    }

    public override void RightHold(bool key)
    {
        if (key)
        {
            GetComponentInChildren<LaunchArcMesh>().Enable();
        }
    }

    public override void RightClick(bool key)
    {
        if (key)
        {
            GetComponentInChildren<LaunchArcMesh>().Disable();
            dronePlayerController.Throw();
            playerControllerSupervisor.SwitchPlayerController(corePlayerController);
        
           // UpdateGuards();
            TurnDroneOff();
        //    throwCore = false;


        }
    }


    void TurnDroneOff()
    {
        GetComponent<EmptyDrone>().enabled = true;
        ownCamera.GetComponent<Camera>().enabled = false;
        ownCamera.GetComponent<AudioListener>().enabled = false;
        //enabled = false;
    }

    public override void EnableController()
    {
        corePlayerController = FindObjectOfType<CorePlayerController>();
        coreCamera = corePlayerController.gameObject;

        mouseLook = new Vector2(coreCamera.transform.localEulerAngles.y, mouseLook.y);
        //mouseLook = new Vector2(gameObject.transform.localRotation.eulerAngles.y, mouseLook.y );
        smoothV = new Vector2(0, 0);
        base.EnableController();
    }

    public override void DisableController()
    {
        if (button != null)
        {
            button.StopLookingAt();
        }
        base.DisableController();

    }


    // Let the guards know which GameObject is the player (only keeping track of one object for effeciency)
    private void UpdateGuards()
    {
        GuardFOV[] guards = FindObjectsOfType<GuardFOV>();
        GameObject core = coreCamera.GetComponent<CorePlayerController>().GetCore();
        foreach (GuardFOV guard in guards)
        {
            guard.ChangePlayer(core);
        }
    }
}

