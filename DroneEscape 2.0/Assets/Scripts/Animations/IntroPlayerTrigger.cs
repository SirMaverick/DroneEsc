using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IntroPlayerTrigger : MonoBehaviour
{
    [SerializeField] private IntroAnimation[] drones;

    private void OnTriggerExit(Collider other)
    {
        foreach(IntroAnimation drone in drones)
        {
            drone.Play();
        }
    }   
}

