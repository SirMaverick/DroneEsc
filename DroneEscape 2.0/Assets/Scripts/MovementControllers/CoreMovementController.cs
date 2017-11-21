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
        if (Physics.Raycast(transform.GetChild(0).transform.position, fwd, out hit, maxDistance))
        {
            if (hit.collider.tag == "Drone")
            {
                print(Vector3.Distance(hit.transform.position, transform.position));    
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
                    //NoHit();
                    
                }

            }
        }
        else
        {
            if (hitEmptyDrone)
            {
                //NoHit();
                TurnOnPulse();
            }

        }
    }



    private void TurnOnPulse()
    {
        foreach (GameObject drone in listOfDronesInRange)
        {
            drone.GetComponentInChildren<DronePulse>().maxDistance = maxDistance;
            drone.GetComponentInChildren<DronePulse>().StartPulse();
        }
    }

    public override void Use(bool key)
    {
        
    }

    public override void LeftClick(bool key)
    {
        activate = key;
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
            other.GetComponent<DronePlayerController>().GetMaterial().SetInt("_ON", 0);
        }
    }

    /*
    private void NoHit()
    {
        lastDroneHit.StopLookingAt();
        lastDroneHit = null;
        hitEmptyDrone = false;
    }

    public override void DisableController()
    {
        if (lastDroneHit != null)
        {
            lastDroneHit.StopLookingAt();
        }
        base.DisableController();
    }
    */


}

