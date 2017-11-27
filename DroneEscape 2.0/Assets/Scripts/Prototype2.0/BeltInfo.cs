﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltInfo : MonoBehaviour {

    public int currentBeltPart;
    public Vector3 movement;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Object" ) {
            if (!other.GetComponent<MoveOnBelt>().sent) {
                other.GetComponent<MoveOnBelt>().currentPart = currentBeltPart;
                other.GetComponent<MoveOnBelt>().sent = true;
                other.GetComponent<MoveOnBelt>().movement = movement;
            }
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Object" && other.GetComponent<MoveOnBelt>().flying) {
            other.GetComponent<MoveOnBelt>().currentPart = currentBeltPart;
           
        }
    }

}
