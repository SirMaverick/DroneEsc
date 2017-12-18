using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    [SerializeField] String nameOfVariable;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Drone")
        {
            foreach (Animator animator in animators)
            {
                animator.SetBool(nameOfVariable, true);
            }
        }
    }
}