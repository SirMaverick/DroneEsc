﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour {

    private Vector2 mouseLook;
    private Vector2 smoothV;
    [SerializeField] private float minClamp;
    [SerializeField] private float maxClamp;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    [SerializeField] private float selectionRange = 1.5f;

    GameObject character;

    private Material lastMaterialHit;
    private bool hitButton = false;

	// Use this for initialization
	void Start () {
        character = transform.parent.gameObject;
	}

    private Button button;
    // Update is called once per frame
    void Update () {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, selectionRange)) {

            if (hit.collider.tag == "Button") {
                hitButton = true;
                button = hit.collider.gameObject.GetComponent<Button>();
                button.LookingAt();
                if (Input.GetKeyDown(KeyCode.E)) {

            } else if (hit.collider.tag == "Elevator") {
                if (Input.GetKeyDown(KeyCode.U)) {
                    Debug.Log("Press U");
                    hit.transform.GetComponent<ElevatorButton>().MoveElevatorUp();
                } else if (Input.GetKeyDown(KeyCode.O)) {
                    hit.transform.GetComponent<ElevatorButton>().MoveElevatorDown();
                } else if (Input.GetKeyUp(KeyCode.U) || Input.GetKeyUp(KeyCode.O)) {
                    hit.transform.GetComponent<ElevatorButton>().StopElevatorMove();
                }
            } else if (hit.collider.tag == "Magnet") {
                if (Input.GetKeyDown(KeyCode.E)) {
                    hit.transform.GetComponent<MagnetButton>().TurnMagnetOn();
                } else if (Input.GetKeyUp(KeyCode.R)) {
                    hit.transform.GetComponent<MagnetButton>().TurnMagnetOff();
                } else if (Input.GetKeyUp(KeyCode.Space)) {
                    hit.transform.GetComponent<MagnetButton>().coreInside = true;
                }
            } else {
                if (hitButton) {
                    hitButton = false;
                }

            }
        }
        else
        {
            if (hitButton)
            {
                button.StopLookingAt();
                hitButton = false;
            }

        }
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minClamp, maxClamp);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
		
	}
}
