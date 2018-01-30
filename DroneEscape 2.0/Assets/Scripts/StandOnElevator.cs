using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnElevator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision) {
        if(collision.transform.tag == "Drone") {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider collision) {
        if(collision.transform.tag == "Drone") {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.transform.parent = transform.parent.parent;
        }
    }
}
