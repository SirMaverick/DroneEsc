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
            UpdateGuardsCore();
            throwCore = false;
            

        }
    }

    void TurnDroneOff() {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<EmptyDrone>().enabled = true;
        ownCamera.GetComponent<PlayerMouseLook>().enabled = false;
        ownCamera.GetComponent<Camera>().enabled = false;
        enabled = false;
    }

    void TurnMenuOn() {

    }

    private void UpdateGuardsCore()
    {
        GuardFOV[] guards = GetComponents<GuardFOV>();
        // yes ugly
        GameObject core = coreCamera.GetComponent<CoreCamera>().core;
        foreach (GuardFOV guard in guards) {
            guard.ChangePlayer(core);
        }
    }
}
