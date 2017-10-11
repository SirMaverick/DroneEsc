using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private GameObject coreCamera;
    private GameObject ownCamera;
    private float xMovement;
    private float zMovement;
    public bool throwCore;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        ownCamera = transform.Find("DroneCamera").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        xMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        zMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(xMovement, 0, zMovement);


        if(throwCore) {
            coreCamera.GetComponent<CoreCamera>().SetCamera();
            TurnDroneOff();
            throwCore = false;
            

        }
    }

    void TurnDroneOff() {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<EmptyDrone>().enabled = true;
        ownCamera.GetComponent<PlayerMouseLook>().enabled = false;
        ownCamera.GetComponent<Camera>().enabled = false;
        ownCamera.GetComponent<AudioListener>().enabled = false;
        enabled = false;
    }

    void TurnMenuOn() {

    }
}
