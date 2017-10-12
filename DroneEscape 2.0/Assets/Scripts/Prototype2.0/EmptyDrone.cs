using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyDrone : MonoBehaviour {

    private Transform cameraObject;
    private GameObject coreCamera;
    private GameObject corePickUp;
    private bool walk;
    private GameObject ownCamera;
    [SerializeField] GameObject objectPlacement;

    public float minDist;
    [SerializeField] float moveSpeed;

	// Use this for initialization
	void Start () {
        ownCamera = transform.Find("DroneCamera").gameObject;
	}

    // Update is called once per frame
    void Update() {
        if (walk) {
                transform.LookAt(new Vector3 (cameraObject.position.x, transform.position.y, cameraObject.position.z));

            if (Vector3.Distance(transform.position, cameraObject.position) >= minDist) {

                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            } else {
                walk = false;
                PickUpCore();
                UpdateGuards();
            }
        }
    }
    public void WalkToPlayer(Transform tempCore) {
        cameraObject = tempCore.parent;
        coreCamera = tempCore.gameObject;
        walk = true;

    }

    void PickUpCore() {
        coreCamera.GetComponent<Camera>().enabled = false;
        coreCamera.GetComponent<AudioListener>().enabled = false;
        coreCamera.GetComponent<CoreMouseMovement>().enabled = false;
        cameraObject.transform.position = transform.position;
        corePickUp = cameraObject.GetComponent<CoreCamera>().core;
        cameraObject.GetComponent<CoreCamera>().core.GetComponent<BoxCollider>().enabled = false;
        corePickUp.GetComponent<MeshRenderer>().enabled = false;
        corePickUp.GetComponent<Rigidbody>().useGravity = false;
        corePickUp.GetComponent<MoveOnBelt>().sent = false;
        corePickUp.GetComponent<MoveOnBelt>().pickedUp = true;
        corePickUp.transform.parent = transform;
        corePickUp.transform.position = objectPlacement.transform.position;
        corePickUp.GetComponent<MoveOnBelt>().currentPart = 0;

        TurnOnDrone();
    }

    void TurnOnDrone() {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<ThrowCore>().enabled = true;
        GetComponent<ThrowCore>().isThrown = false;
        ownCamera.GetComponent<AudioListener>().enabled = true;
        ownCamera.GetComponent<Camera>().enabled = true;
        ownCamera.GetComponent<PlayerMouseLook>().enabled = true;

        enabled = false;

    }
    // Let the guards know which GameObject is the player (only keeping track of one object for effeciency)
    private void UpdateGuards()
    {
        GuardFOV[] guards = FindObjectsOfType<GuardFOV>();
        foreach (GuardFOV guard in guards)
        {
            guard.ChangePlayer(gameObject);
        }
    }
}
