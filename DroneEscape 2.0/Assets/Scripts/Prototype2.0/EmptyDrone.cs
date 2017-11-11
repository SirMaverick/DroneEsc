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

    PlayerControllerSupervisor pcs;

    // Use this for initialization
    void Start () {
        ownCamera = transform.Find("DroneCamera").gameObject;
        pcs = PlayerControllerSupervisor.GetInstance();
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
        cameraObject.transform.position = transform.position;
        corePickUp = cameraObject.GetComponent<CorePlayerController>().GetCore();
        corePickUp.GetComponent<BoxCollider>().enabled = false;
        corePickUp.GetComponent<MeshRenderer>().enabled = false;
        corePickUp.GetComponent<Rigidbody>().useGravity = false;
        corePickUp.GetComponent<MoveOnBelt>().sent = false;
        corePickUp.GetComponent<MoveOnBelt>().pickedUp = true;
        corePickUp.GetComponent<MoveOnBelt>().StopMoving();
        corePickUp.transform.parent = transform;
        corePickUp.transform.position = objectPlacement.transform.position;
        corePickUp.GetComponent<MoveOnBelt>().currentPart = 0;

        TurnOnDrone();
    }

    void TurnOnDrone() {
        GetComponent<MeshRenderer>().enabled = false;
        
        pcs.SwitchPlayerController(GetComponent<DronePlayerController>());
        ownCamera.GetComponent<AudioListener>().enabled = true;
        ownCamera.GetComponent<Camera>().enabled = true;
       

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
