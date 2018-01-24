using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroDroneNextRoom : MonoBehaviour {

    private PlayableDirector director;
    private Animator animator;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.Pause();
        animator = GetComponent<Animator>();
    }
    public void Throw()
    {
        director.Play();
        animator.SetBool("Throw", true);
    }

}
