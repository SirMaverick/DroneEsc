using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : Button {


    [SerializeField] private GameObject elevator;
    [SerializeField] private Transform lowestPos;
    [SerializeField] private Transform highestPos;
    private bool up, down;
    private float step;
    [SerializeField] private float speed;
    public bool coreInside;
    public GameObject drone;
    public Camera surveillanceCamera;

    [SerializeField]
    private ElevatorPlayerController playerController;

	
	// Update is called once per frame
	/*void Update () { 
        if(coreInside) {
            if (up) {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, highestPos.position, step);
                foreach (GameObject objectOnElevator in elevator.GetComponent<ItemsOnElevator>().items) {
                    objectOnElevator.transform.position = Vector3.MoveTowards(objectOnElevator.transform.position, new Vector3(objectOnElevator.transform.position.x, highestPos.position.y, objectOnElevator.transform.position.z), step);
                }
            } else if (down) {
                elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, lowestPos.position, step);
                foreach (GameObject objectOnElevator in elevator.GetComponent<ItemsOnElevator>().items) {
                    objectOnElevator.transform.position = Vector3.MoveTowards(objectOnElevator.transform.position, new Vector3(objectOnElevator.transform.position.x, lowestPos.position.y, objectOnElevator.transform.position.z), step);
                }
            }

            if (Input.GetKeyDown(KeyCode.W)) {
                up = true;
                down = false;
            } else if (Input.GetKeyDown(KeyCode.S)) {
                down = true;
                up = false;
            } else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
                up = false;
                down = false;
            }

            if(Input.GetKeyUp(KeyCode.E)) {
                surveillanceCamera.enabled = false;
                coreInside = false;
                drone.GetComponent<PlayerMovement>().enabled = true;
                drone.GetComponent<MeshRenderer>().enabled = false;
                drone.GetComponentInChildren<PlayerMouseLook>().enabled = true;
                drone.GetComponentInChildren<Camera>().enabled = true;
            }
        }

    }*/

    public override void Toggle()
    {
        if (!enabled)
        {
            playerControllerSupervisor.SwitchPlayerController(playerController );
            enabled = true;
        }
        else
        {
            playerControllerSupervisor.SwitchPlayerControllerPrevious();
            
            enabled = false;
        }
    }
}
