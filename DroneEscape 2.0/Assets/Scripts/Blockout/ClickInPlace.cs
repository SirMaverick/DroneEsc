using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInPlace : MonoBehaviour {

    [SerializeField] private GameObject triggerObjectT;
    private Transform triggerT;
    private Rigidbody rb;
    private bool hasBeenSet;

    private void Start()
    {
        hasBeenSet = false;
        triggerT = gameObject.transform;
        rb = triggerObjectT.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerObjectT && hasBeenSet == false)
        {
            triggerObjectT.tag = "Untagged";
            other.transform.position = transform.parent.position;
            rb.isKinematic = true;
            hasBeenSet = true;
        }
    }

    public bool HasBeenSet()
    {
        return hasBeenSet;
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Magnetic" && hasBeenSet == true)
        {
            rb.isKinematic = false;
            hasBeenSet = false;
        }
    }
    */
}
