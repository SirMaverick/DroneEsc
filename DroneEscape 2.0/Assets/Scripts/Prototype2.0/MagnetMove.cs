using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMove : MonoBehaviour {

    public bool turnedOn;
    [SerializeField] private float speed = 2.5f;
    public bool turnedOff;
    bool on;
    public List<GameObject> listOfDrones = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(turnedOff) {
            ReleaseDrones();
        }
        foreach (GameObject drone in listOfDrones) {
            if(drone.transform.position.y < transform.position.y - 2f)
            drone.transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
        }

    }

    void ReleaseDrone(GameObject drone) {
        drone.transform.parent = null;
        drone.GetComponent<Rigidbody>().useGravity = true;
        listOfDrones.Remove(drone);
    }

    void ReleaseDrones() {
        turnedOff = false;
        on = false;
        foreach (GameObject child in listOfDrones) {
            child.transform.parent = null;
            child.GetComponent<Rigidbody>().useGravity = true;
        }
        listOfDrones.Clear();

        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Drone" && on && other.GetComponent<Rigidbody>().useGravity) {
            if (other.GetComponent<Rigidbody>().useGravity) {
                other.transform.parent = transform;
                other.GetComponent<Rigidbody>().useGravity = false;
                listOfDrones.Add(other.gameObject);
            }

        }
    }

    private void OnTriggerStay(Collider other) {
        if(turnedOn) {
            if(other.transform.tag == "Drone" && other.transform.parent != gameObject) {
                other.transform.parent = transform;
                other.GetComponent<Rigidbody>().useGravity = false;
                listOfDrones.Add(other.gameObject);
            }
            turnedOn = false;
            on = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Drone") ReleaseDrone(other.gameObject);     
    }
}
