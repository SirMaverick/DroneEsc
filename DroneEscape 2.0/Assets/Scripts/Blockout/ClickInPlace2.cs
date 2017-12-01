using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInPlace2 : MonoBehaviour
{

    [SerializeField]
    private GameObject triggerObjectT, triggerObjectT2;
    [SerializeField]
    private MagnetMove magnetMove;
    [SerializeField]
    private GameObject object1;
    [SerializeField]
    private GameObject object2;
    private Transform triggerT;
    private Rigidbody rb;
    private Rigidbody rb2;
    private bool hasBeenSet;

    private void Start()
    {
        hasBeenSet = false;
        triggerT = gameObject.transform;
        rb = triggerObjectT.GetComponent<Rigidbody>();
        rb2 = triggerObjectT2.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggerObjectT && hasBeenSet == false)
        {
            magnetMove.ReleaseOnConveyorClick(other.gameObject);
            triggerObjectT.tag = "Untagged";
            triggerObjectT.SetActive(false);
            rb.isKinematic = true;
            other.transform.position = transform.parent.position;
            object1.SetActive(true);
            object2.SetActive(true);
            hasBeenSet = true;
        }

        if (other.gameObject == triggerObjectT2 && hasBeenSet == false)
        {
            magnetMove.ReleaseOnConveyorClick(other.gameObject);
            triggerObjectT2.tag = "Untagged";
            triggerObjectT2.SetActive(false);
            rb2.isKinematic = true;
            other.transform.position = transform.parent.position;
            object1.SetActive(true);
            object2.SetActive(true);
            hasBeenSet = true;
        }
    }
}