using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour {
    [SerializeField]
    Animator animator;
    [SerializeField]
    string stateName;


    private void OnTriggerEnter(Collider other)
    {
        animator.Play(stateName);

    }
}
