using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCameraRotation : MonoBehaviour {

    public Transform target;
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = target.position - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
