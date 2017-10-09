﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltInfo : MonoBehaviour {

    public int currentBeltPart;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (!other.GetComponent<MoveOnBelt>().sent) {
            other.GetComponent<MoveOnBelt>().currentPart = currentBeltPart;
            other.GetComponent<MoveOnBelt>().sent = true;
        }
    }

}
