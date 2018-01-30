﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollision : MonoBehaviour {

    public Collider[] colliders;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      /*  colliders = Physics.OverlapSphere(transform.position, transform.localScale.x);
        if(colliders != null) {
        }*/
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "DrawnLine") {
            other.transform.parent.GetComponent<DrawLine>().AtTriggerEnter();
        }
    }
}
