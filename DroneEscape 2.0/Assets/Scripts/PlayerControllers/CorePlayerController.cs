using UnityEngine;
using System.Collections;
using FMODUnity;

public class CorePlayerController : AbstractPlayerController
    {
 
    [SerializeField]
    private GameObject core;
    [SerializeField]
    private GameObject cameraCenter;
    private GameObject pulse1, pulse2;

    FMOD.Studio.EventInstance pulseSound;

    Vector3 lastPos;
    Vector3 currentPos;

    private bool isFlying;

    private GameObject lights;
    private bool coreAmbient;
    [SerializeField] private Color inCoreAmbient;
    [SerializeField] private Color inDroneAmbient;


    private void Awake() {
        pulseSound = RuntimeManager.CreateInstance("event:/Core/CorePulse");
        RuntimeManager.AttachInstanceToGameObject(pulseSound, core.transform, core.GetComponent<Rigidbody>());
        
    }

    protected override void Start()
    {
        base.Start();

        lights = GameObject.Find("LightCollection");
        core = FindObjectOfType<CoreObject>().gameObject;
        uiController = FindObjectOfType<CoreUIController>();
 
        if (camera.enabled)
        {
            GuardFOV[] guards = FindObjectsOfType<GuardFOV>();

            foreach (GuardFOV guard in guards)
            {
                guard.ChangePlayer(core);
            }
        }
        pulse1 = GameObject.Find("Pulse1");
        pulse2 = GameObject.Find("Pulse2");
        StartCoroutine(TurnOnPulses());
    }


    public GameObject GetCore()
    {
        return core;
    }

    public override void EnableController()
    {
        base.EnableController();
        GetComponentInChildren<CameraCollision>().enabled = true;
        TurnOnCore();
        TurnLightsOff();

    }

    public override void DisableController()
    {
        base.DisableController();
        GetComponentInChildren<CameraCollision>().enabled = false;
        TurnOffCore();
        TurnLightsOn();
    }


    

    private void TurnOnCore()
    {
        core.GetComponent<BoxCollider>().enabled = true;
        core.GetComponent<MeshRenderer>().enabled = true;
        core.GetComponent<Rigidbody>().useGravity = true;
        core.GetComponent<MoveOnBelt>().flying = true;
        core.GetComponent<MoveOnBelt>().pickedUp = false;
        core.GetComponent<MoveOnBelt>().start = false;


        //   StartCoroutine(CheckGrounded());
        isFlying = true;
    }

    private void TurnOffCore()
    {
        //coreCamera.GetComponent<Camera>().enabled = false;
        //coreCamera.GetComponent<AudioListener>().enabled = false;

        TurnOffPulses();
        CoreMovementController cmc = (CoreMovementController)movementController;
        cmc.TurnOffPulse();
       

        core.GetComponent<BoxCollider>().enabled = false;
        core.GetComponent<MeshRenderer>().enabled = false;
        core.GetComponent<Rigidbody>().useGravity = false;

        core.GetComponent<MoveOnBelt>().sent = false;
        core.GetComponent<MoveOnBelt>().pickedUp = true;
        core.GetComponent<MoveOnBelt>().StopMoving();
        core.GetComponent<MoveOnBelt>().currentPart = 0;
     
    }

    void CoreOnGround()
    {
        //camera.transform.parent = null;
        StartCoroutine(TurnOnPulses());
        if (core.GetComponent<MoveOnBelt>().start)
        {
            core.GetComponent<MoveOnBelt>().currentCoroutine = StartCoroutine(core.GetComponent<MoveOnBelt>().MoveObject());
        }
        core.GetComponent<MoveOnBelt>().flying = false;
        //enabled = false;
    }

    private void FixedUpdate()
    {
        if (isFlying)
        {
            currentPos = core.transform.position;

            if (Vector3.Distance(lastPos, currentPos) == 0)
            {
                isFlying = false;
                CoreOnGround();
            }


            cameraCenter.transform.position = core.transform.position;

           
        }
        lastPos = core.transform.position;
    }

    public Camera GetCamera()
    {
        return camera;
    }

    public IEnumerator TurnOnPulses() {
        RuntimeManager.AttachInstanceToGameObject(pulseSound, core.transform, core.GetComponent<Rigidbody>());
        pulseSound.setParameterValue("Pulse", 1.0f);
        pulseSound.start();
        GetComponent<CoreMovementController>().TurnOnPulse();
        pulse1.GetComponent<Menu_Button_Pulse>().TurnOnPulse();
        yield return new WaitForSeconds(3.0f);
        pulse2.GetComponent<Menu_Button_Pulse>().TurnOnPulse();
    }

    public void TurnOffPulses() {
        pulseSound.setParameterValue("Pulse", 0.0f);
        pulseSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);    
        pulse1.GetComponent<Menu_Button_Pulse>().TurnOffPulse();
        pulse2.GetComponent<Menu_Button_Pulse>().TurnOffPulse();
    }


    private void TurnLightsOn()
    {
        foreach (Light light in lights.GetComponentsInChildren<Light>())
        {
            light.enabled = true;
        }
        coreAmbient = false;
        RenderSettings.ambientLight = inDroneAmbient;
    }

    void TurnLightsOff()
    {
        foreach (Light light in lights.GetComponentsInChildren<Light>())
        {
            light.enabled = false;
        }
        coreAmbient = true;
        RenderSettings.ambientLight = inCoreAmbient;
    }
}
