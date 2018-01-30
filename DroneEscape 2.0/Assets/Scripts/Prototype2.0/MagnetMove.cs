using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MagnetMove : MonoBehaviour {

    public bool turnedOn;
    [SerializeField] private float speed = 2.5f;
    [SerializeField]
    private Transform audioPlacement;
    public List<GameObject> listOfMagneticObjects = new List<GameObject>();

    GameObject lastCollided;

    public FMOD.Studio.EventInstance collisionSound;
    private FMOD.Studio.PLAYBACK_STATE playback;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(!turnedOn) {
            ReleaseObjects();
        }
        foreach (GameObject magnetic in listOfMagneticObjects) {
            if (magnetic.transform.position.y < transform.position.y - 2f)
            {
                magnetic.transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
            }
            magnetic.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);    
        }

    }

    void ReleaseObject(GameObject magneticObject) {
        magneticObject.transform.parent = null;
        magneticObject.GetComponent<Rigidbody>().useGravity = true;
        listOfMagneticObjects.Remove(magneticObject);
    }

    void ReleaseObjects() {
        turnedOn = false;
        foreach (GameObject child in listOfMagneticObjects) {
            child.transform.parent = null;
            child.GetComponent<Rigidbody>().useGravity = true;
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
        listOfMagneticObjects.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<MachinePulse>() ) other.GetComponent<MachinePulse>().StartPulse();
        if (other.GetComponent<DronePulse>()) other.GetComponent<DronePulse>().StartPulseHighlighted();


        // A drone is magnetic aswell but isn't tagged as magnetic
        if ((other.transform.tag == "Drone" || other.transform.tag == "Magnetic") && turnedOn && other.GetComponent<Rigidbody>().useGravity && listOfMagneticObjects.Count == 0) {
            RaycastHit hit;
            Vector3 direction = gameObject.transform.position - other.transform.position;
            Physics.Raycast(gameObject.transform.position, direction, out hit);
            if (hit.transform.gameObject == other.gameObject) {
                if (other.GetComponent<Rigidbody>().useGravity) {
                    other.transform.parent = transform;
                    other.GetComponent<Rigidbody>().useGravity = false;
                    other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    listOfMagneticObjects.Add(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(turnedOn) {
            // A drone is magnetic aswell but isn't tagged as magnetic
            if ((other.transform.tag == "Drone" || other.transform.tag == "Magnetic") && other.transform.parent != transform && listOfMagneticObjects.Count == 0) {
                RaycastHit hit;
                Vector3 direction = other.transform.position - gameObject.transform.position;
                Physics.Raycast(gameObject.transform.position, direction, out hit);
                if (hit.transform.gameObject == other.gameObject) {
                    if (other.GetComponent<Rigidbody>().useGravity) {
                        other.transform.parent = transform;
                        other.GetComponent<Rigidbody>().useGravity = false;
                        other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        listOfMagneticObjects.Add(other.gameObject);
                    }
                }
            }
        }
    }

    public void ReleaseOnConveyorClick(GameObject belt)
    {
        if(listOfMagneticObjects.Contains(belt))
        {
            belt.transform.parent = null;
            belt.GetComponent<Rigidbody>().useGravity = true;
            belt.GetComponent<Rigidbody>().isKinematic = false;
            listOfMagneticObjects.Remove(belt);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Drone" || other.transform.tag == "Magnetic") ReleaseObject(other.gameObject);
        if (other.GetComponent<MachinePulse>()) other.GetComponent<MachinePulse>().StopPulse();
        else if (other.GetComponent<DronePulse>()) other.GetComponent<DronePulse>().StopPulse();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (collision.transform.tag == "Magnetic") {
            collision.transform.position -= new Vector3(0, Time.deltaTime * speed * 3, 0);
            collision.rigidbody.velocity = new Vector3(0, 0, 0);
            collisionSound.getPlaybackState(out playback);
            if (playback != FMOD.Studio.PLAYBACK_STATE.PLAYING && playback != FMOD.Studio.PLAYBACK_STATE.STARTING && lastCollided != collision.gameObject) {
                lastCollided = collision.gameObject;
                collisionSound = RuntimeManager.CreateInstance("event:/SFX/Magnet/Magnet");
                RuntimeManager.AttachInstanceToGameObject(collisionSound, audioPlacement, GetComponent<Rigidbody>());
                collisionSound.setParameterValue("MagnetOn", 0.0f);
                collisionSound.setParameterValue("MagnetMovement", 0.0f);
                collisionSound.setParameterValue("MagnetLock", 1.0f);
                collisionSound.setParameterValue("GrabSmall", 0.0f);
                collisionSound.setParameterValue("GrabMedium", 0.0f);
                collisionSound.setParameterValue("GrabBig", 1.0f);
                collisionSound.setParameterValue("MagnetDrop", 0.0f);
                collisionSound.setParameterValue("DropSmall", 0.0f);
                collisionSound.setParameterValue("DropMedium", 0.0f);
                collisionSound.setParameterValue("DropBig", 0.0f);
                collisionSound.start();

                StartCoroutine(StopFMODSound(0.8f));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (collision.transform.tag == "Magnetic")
        {
            collision.rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (collision.transform.tag == "Magnetic")
        {
            collision.rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator StopFMODSound(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        collisionSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
