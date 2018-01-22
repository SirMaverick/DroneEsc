using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimNextRoom : MonoBehaviour {

    [SerializeField] private IntroAnimation[] drones;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Drone")
        {
            foreach (IntroAnimation drone in drones)
            {
                drone.ThrowAllowed();
            }
        }
    }
}
