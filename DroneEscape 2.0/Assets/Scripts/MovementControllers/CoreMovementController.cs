using UnityEngine;
using System.Collections.Generic;

public class CoreMovementController : MovementController
{

    private Vector2 mouseLook;
    private Vector2 smoothV;
    [SerializeField] private float minClamp;
    [SerializeField] private float maxClamp;
    [SerializeField]
    private float maxDistance;
    private GameObject core;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;



    private GameObject character;

    private EmptyDrone lastDroneHit;
    private bool hitEmptyDrone = false;

    private bool isThrown;
    private bool nearBelt;

    private bool activate = false;

    public List<GameObject> listOfDronesInRange = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
        TurnOnPulse();
        //core = GetComponent<CorePlayerController>().GetCore();
        core = FindObjectOfType<CoreObject>().gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        character = transform.gameObject;
        

    }

    // the core cannot move on its own
    public override void Horizontal(float direction)
    {

    }

    public override void Vertical(float direction)
    {

    }


    public override void Look(Vector2 md)
    {
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minClamp, maxClamp);

        transform.GetChild(0).transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    RaycastHit hit;
    Vector3 fwd = transform.GetChild(0).transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.GetChild(0).transform.position, fwd, out hit, maxDistance + Vector3.Distance(transform.GetChild(0).position, core.transform.position)))
        {
            if (hit.collider.tag == "Drone")
                
            {
                //Debug.Log(maxDistance + Vector3.Distance(transform.GetChild(0).position, core.transform.position));    
                hitEmptyDrone = true;
                EmptyDrone newHit = hit.collider.gameObject.GetComponent<EmptyDrone>();
                if (lastDroneHit != newHit) {
                    newHit.StopLookingAt();
                    lastDroneHit = newHit;
                    lastDroneHit.LookingAt();
                }

                if (activate)
                {
                    lastDroneHit.WalkToPlayer(transform.GetChild(0).transform);
                }

            }
            else
            {
                if (hitEmptyDrone)
                {
                    NoHit();
                    
                }

            }
        }
        else
        {
            if (hitEmptyDrone)
            {
                NoHit();
            }

        }

        CHEAT(Input.GetKeyDown(KeyCode.E));
    }



    public void TurnOnPulse()
    {
        foreach (GameObject drone in listOfDronesInRange)
        {
            drone.GetComponentInChildren<DronePulse>().maxDistance = maxDistance;
            drone.GetComponentInChildren<DronePulse>().StartPulse();
        }
    }

    public void TurnOffPulse() {
        foreach (GameObject drone in listOfDronesInRange) {
            drone.GetComponentInChildren<DronePulse>().StopPulse();
        }
    }

    public void CHEAT(bool key)
    {
        if (key)
        {
            AbstractPlayerController apc = playerControllerSupervisor.GetPreviousPlayerController();
            if(Object.ReferenceEquals(apc.GetType(), typeof(DronePlayerController))) 
            {
                transform.position = apc.transform.position;
                //transform.parent = apc.transform;
                playerControllerSupervisor.SwitchPlayerControllerPrevious();

            }
            
        }    
    }

    public override void LeftClick(bool key)
    {
        activate = key;
    }
    public override void RightClick(bool key)
    {

    }


    private void OnTriggerStay(Collider other) {
        if (!listOfDronesInRange.Contains(other.gameObject) && other.tag == "Drone") {
            listOfDronesInRange.Add(other.gameObject);
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Drone") {
            listOfDronesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Drone") {
            listOfDronesInRange.Remove(other.gameObject);
            other.GetComponent<DronePlayerController>().DisableHeartBeat();
        }
    }

    private void NoHit()
    {
        lastDroneHit.StopLookingAt();
        lastDroneHit = null;
        hitEmptyDrone = false;
    }

    public override void EnableController()
    {
        base.EnableController();
        // use the current location of the camera
        mouseLook = new Vector2(transform.localEulerAngles.y, mouseLook.y);
        smoothV = new Vector2(0, 0);
    }

    public override void DisableController()
    {
        if (lastDroneHit != null)
        {
            lastDroneHit.StopLookingAt();
        }
        base.DisableController();
    }

}

