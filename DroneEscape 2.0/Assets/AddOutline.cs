using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOutline : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Drone" && other.tag != "Object" && other.tag != "CoreDrone"){

            other.GetComponent<MeshRenderer>().material.SetFloat("_Outline", 1);
            other.GetComponent<MeshRenderer>().material.SetFloat("_Outline1", 1);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Drone" && other.tag != "Object" && other.tag != "CoreDrone") {
            other.GetComponent<MeshRenderer>().material.SetFloat("_Outline", 0);
            other.GetComponent<MeshRenderer>().material.SetFloat("_Outline1", 0);
        }
    }
}
