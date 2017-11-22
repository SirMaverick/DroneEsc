using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInPlace : MonoBehaviour {

    [SerializeField] private Transform triggerObjectT;
    private Transform triggerT;
    public bool hasBeenSet;

    private void Start()
    {
        hasBeenSet = false;
        triggerT = gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Magnetic" && hasBeenSet == false)
        {
            other.transform.position = transform.parent.position;
            hasBeenSet = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Magnetic" && hasBeenSet == true)
        {
            hasBeenSet = false;
        }
    }
}
