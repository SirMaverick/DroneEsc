using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsOnElevator : MonoBehaviour {

    public List<GameObject> items = new List<GameObject>();
    private int speed = 1;
    public bool enableElevator = false;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (enableElevator) { 
            foreach (GameObject item in items)
            {
                //if (drone.transform.position.y < transform.position.y - 2f)
                item.transform.Translate(0, Time.deltaTime * speed, 0, Space.World);
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Magnet") {
            items.Add(other.gameObject);
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Magnet")
        {
            if (!items.Contains(other.gameObject))
            {
                items.Add(other.gameObject);
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
            }
            if (other.tag == "Drone")
            {
                other.GetComponent<EmptyDrone>().DisableNavMesh();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Magnet") {

            items.Remove(other.gameObject);
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Rigidbody>().isKinematic = false;
            if (other.tag == "Drone")
            {
                 other.GetComponent<EmptyDrone>().EnableNavMesh();
            }
        }
    }
}
