﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMove : MonoBehaviour {

    public bool turnedOn;
    [SerializeField] private float speed = 2.5f;
    public List<GameObject> listOfDrones = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(!turnedOn) {
            ReleaseDrones();
        }
        foreach (GameObject drone in listOfDrones) {
            if(drone.transform.position.y < transform.position.y - 2f)
            drone.transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
            drone.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);    
        }

    }

    void ReleaseDrone(GameObject drone) {
        drone.transform.parent = null;
        drone.GetComponent<Rigidbody>().useGravity = true;
        listOfDrones.Remove(drone);
    }

    void ReleaseDrones() {
        turnedOn = false;
        foreach (GameObject child in listOfDrones) {
            child.transform.parent = null;
            child.GetComponent<Rigidbody>().useGravity = true;
            child.GetComponent<Rigidbody>().isKinematic = false;
        }
        listOfDrones.Clear();

        
    }

    private void OnTriggerEnter(Collider other) {
        if ((other.transform.tag == "Drone" || other.transform.tag == "Magnetic") && turnedOn && other.GetComponent<Rigidbody>().useGravity) {
            if (other.GetComponent<Rigidbody>().useGravity) {
                other.transform.parent = transform;
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                listOfDrones.Add(other.gameObject);
            }

        }
    }

    private void OnTriggerStay(Collider other) {
        if(turnedOn) {
            if((other.transform.tag == "Drone" || other.transform.tag == "Magnetic") && other.transform.parent != transform) {
                other.transform.parent = transform;
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                listOfDrones.Add(other.gameObject);
            }

        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.transform.tag == "Drone" || other.transform.tag == "Magnetic") ReleaseDrone(other.gameObject);     
    }
}
