using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsOnElevator : MonoBehaviour {

    public List<GameObject> items = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Magnet") {
            items.Add(other.gameObject);
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Magnet") {

            items.Remove(other.gameObject);
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
