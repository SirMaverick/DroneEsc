using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour {

    public int currentPart;
    [SerializeField] GameObject[] beltParts;
    GameObject obj;
        
	// Use this for initialization
	void Awake () {
        int i = transform.childCount;
        beltParts = new GameObject[i];
        int counter = 0;
        foreach (Transform child in transform) {
            beltParts[counter] = child.gameObject;
            child.GetComponent<BeltInfo>().currentBeltPart = counter;
            counter++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Object" ) {


            obj = other.gameObject;
            obj.GetComponent<MoveOnBelt>().enabled = true;
            obj.GetComponent<MoveOnBelt>().beltParts = beltParts;
            obj.GetComponent<MoveOnBelt>().currentCoroutine = StartCoroutine(obj.GetComponent<MoveOnBelt>().MoveObject());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Object" && other.GetComponent<MoveOnBelt>().isActiveAndEnabled) {

            StopCoroutine(obj.GetComponent<MoveOnBelt>().MoveObject());
            obj.GetComponent<MoveOnBelt>().enabled = false;
        }
    }
}
