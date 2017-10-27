using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCore : MonoBehaviour {

	// Use this for initialization
	private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPCDrone")
        {
            Destroy(this.gameObject);
        }
    }
}
