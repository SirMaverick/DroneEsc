using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMove : MonoBehaviour {

    public bool turnedOn;
    [SerializeField] private float speed = 2.5f;
    public List<GameObject> listOfMagneticObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(!turnedOn) {
            ReleaseObjects();
        }
        foreach (GameObject drone in listOfMagneticObjects) {
            if(drone.transform.position.y < transform.position.y - 2f)
            drone.transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
            drone.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);    
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
        // A drone is magnetic aswell but isn't tagged as magnetic
        if ((other.transform.tag == "Drone" || other.transform.tag == "Magnetic") && turnedOn && other.GetComponent<Rigidbody>().useGravity) {
            if (other.GetComponent<Rigidbody>().useGravity) {
                other.transform.parent = transform;
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                listOfMagneticObjects.Add(other.gameObject);
            }

        }
    }

    private void OnTriggerStay(Collider other) {
        if(turnedOn) {
            // A drone is magnetic aswell but isn't tagged as magnetic
            if((other.transform.tag == "Drone" || other.transform.tag == "Magnetic") && other.transform.parent != transform) {
                other.transform.parent = transform;
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                listOfMagneticObjects.Add(other.gameObject);
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
    }
}
