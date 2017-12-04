using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInPlace : MonoBehaviour {

    [SerializeField]
    private GameObject triggerObjectT;
    [SerializeField]
    private MagnetMove magnetMove;
    [SerializeField]
    private GameObject object1;
    [SerializeField]
    private GameObject object2;
    private Transform triggerT;
    private Rigidbody rb;
    private bool hasBeenSet;

    private void Start() {
        hasBeenSet = false;
        triggerT = gameObject.transform;
        rb = triggerObjectT.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {

        
    if (other.gameObject == triggerObjectT && hasBeenSet == false) {
            magnetMove.ReleaseOnConveyorClick(other.gameObject);
            triggerObjectT.tag = "Untagged";
            triggerObjectT.SetActive(false);
            rb.isKinematic = true;
            other.transform.position = transform.parent.position;
            object1.SetActive(true);
            object2.SetActive(true);
            hasBeenSet = true;
        }
    }

    public bool HasBeenSet() {
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
