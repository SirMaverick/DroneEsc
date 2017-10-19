using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMarchActivator : MonoBehaviour {

    [SerializeField] private GameObject droneMarch;
    [SerializeField] private bool on;
    [SerializeField] private bool off;

    // Use this for initialization
    void Start () {
        droneMarch.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drone")
        {
            if (on)
            {
                droneMarch.SetActive(true);
            }
            else if(off)
            {
                droneMarch.SetActive(false);
            }
        }
    }
}
