using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateButton : Button {

    private Animator _animator;
    [SerializeField] private GameObject Door;

    // Use this for initialization
    void Start ()
    {
        _animator = Door.GetComponent<Animator>();
        _animator.SetBool("Open", false);
    }
	
    public override void Toggle()
    {
        Debug.Log("Doet ie t?");
        _animator.SetBool("Open", true);
    }
}
