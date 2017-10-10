using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCore : MonoBehaviour {

    [SerializeField] private GameObject core;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private GameObject coreCamera;
    bool isThrown;
    Vector3 lastPos;
    Vector3 currentPos;
    [SerializeField] float force;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.T) && !isThrown) {
            lastPos = transform.position;
            cameraObject.transform.position = core.transform.position;
            cameraObject.GetComponent<CoreCamera>().core = core.gameObject; 
            core.transform.parent = null;
            TurnOnCore();
            core.GetComponent<Rigidbody>().AddForce(transform.Find("DroneCamera").TransformDirection(Vector3.forward) * force, ForceMode.Impulse);
            CoreFlying();
            StartCoroutine(CheckGrounded());
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
        core.GetComponent<BoxCollider>().enabled = true;
        core.GetComponent<MeshRenderer>().enabled = true;
        core.GetComponent<Rigidbody>().useGravity = true;
        core.GetComponent<MoveOnBelt>().flying = true;

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
