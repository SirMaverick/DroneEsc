using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyDrone : MonoBehaviour {

    private Transform coreCamera;
    private GameObject corePickUp;
    private bool walk;
    private GameObject ownCamera;

    public float minDist;
    [SerializeField] float moveSpeed;

	// Use this for initialization
	void Start () {
        ownCamera = transform.Find("DroneCamera").gameObject;
	}

    // Update is called once per frame
    void Update() {
        if (walk) {
                transform.LookAt(new Vector3 (coreCamera.position.x, transform.position.y, coreCamera.position.z));

            if (Vector3.Distance(transform.position, coreCamera.position) >= minDist) {

                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            } else {
                walk = false;
                PickUpCore();
            }
        }
    }
    public void WalkToPlayer(Transform tempCore) {
        coreCamera = tempCore.parent;
        walk = true;

    }

    void PickUpCore() {
        coreCamera.transform.Find("CoreCamera").GetComponent<Camera>().enabled = false;
        coreCamera.transform.position = transform.position;
        corePickUp = coreCamera.GetComponent<CoreCamera>().core;
        coreCamera.GetComponent<CoreCamera>().core.GetComponent<BoxCollider>().enabled = false;
        corePickUp.GetComponent<MeshRenderer>().enabled = false;
        corePickUp.GetComponent<Rigidbody>().useGravity = false;
        corePickUp.GetComponent<MoveOnBelt>().sent = false;
        corePickUp.transform.parent = transform;
        TurnOnDrone();
    }

    void TurnOnDrone() {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<ThrowCore>().enabled = true;
        ownCamera.GetComponent<Camera>().enabled = true;
        ownCamera.GetComponent<PlayerMouseLook>().enabled = true;
        enabled = false;

    }
}
