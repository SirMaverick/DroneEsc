using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPCDrone")
        {
            other.gameObject.GetComponent<Animator>().SetBool("PickUpCore", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPCDrone")
        {
            other.gameObject.GetComponent<Animator>().SetBool("PickUpCore", false);
            other.gameObject.GetComponent<IntroAnimation>().Pause();
        }
    }
}

