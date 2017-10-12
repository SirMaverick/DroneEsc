using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCore : MonoBehaviour {

    [SerializeField] private GameObject core;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject coreCamera;
    [SerializeField] private GameObject objectPlacement;
    [SerializeField] private float maxDistance;
    public bool isThrown;
    bool nearBelt;
    Vector3 lastPos;
    Vector3 currentPos;
    [SerializeField] float force;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance)) {
            if (hit.collider.tag == "ConveyerBelt") {
                nearBelt = true;
            } else {
                nearBelt = false;
            }
        }
        if (Input.GetMouseButtonDown(1) && !isThrown && !nearBelt) {
            lastPos = transform.position;
            cameraObject.transform.position = core.transform.position;
            cameraObject.GetComponent<CoreCamera>().core = core.gameObject; 
            core.transform.parent = null;
            TurnOnCore();
            core.GetComponent<Rigidbody>().AddForce(transform.Find("DroneCamera").TransformDirection(Vector3.forward) * force, ForceMode.Impulse);
            CoreFlying();
            StartCoroutine(CheckGrounded());
        } else if (Input.GetMouseButtonDown(1) && !isThrown && nearBelt) {
            core.transform.position = hit.transform.position + new Vector3(0, 1.0f, 0);
            cameraObject.transform.position = core.transform.position;
            cameraObject.GetComponent<CoreCamera>().core = core.gameObject;
            core.transform.parent = null;
            TurnOnCore();

            
        }

        if (isThrown) {
            cameraObject.transform.position = core.transform.position;
        }

        print(isThrown);

	}

    void CoreFlying() {
        isThrown = true;
        GetComponent<PlayerMovement>().throwCore = true;
    }

    void CoreOnGround() {
        cameraObject.transform.parent = null;
        core.GetComponent<MoveOnBelt>().flying = false;
        enabled = false;
    }

    private void TurnOnCore() {
        core.transform.position = objectPlacement.transform.position;
        core.GetComponent<BoxCollider>().enabled = true;
        core.GetComponent<MeshRenderer>().enabled = true;
        core.GetComponent<Rigidbody>().useGravity = true;
        core.GetComponent<MoveOnBelt>().flying = true;
        core.GetComponent<MoveOnBelt>().pickedUp = false;

        coreCamera.GetComponent<AudioListener>().enabled = true;
        coreCamera.GetComponent<CoreMouseMovement>().enabled = true;
    }

    IEnumerator CheckGrounded() {
        lastPos = core.transform.position; 
        yield return new WaitForSeconds(0.05f);
        currentPos = core.transform.position;
        if (Vector3.Distance(lastPos, currentPos) == 0) {
            isThrown = false;
            CoreOnGround();
        }
        if ( isThrown ) {
            StartCoroutine(CheckGrounded());
        }
    }
}
