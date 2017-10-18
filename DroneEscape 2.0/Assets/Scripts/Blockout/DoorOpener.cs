using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {

    private Animator _animatorL;
    private Animator _animatorR;
    private Animator _animatorL1;
    private Animator _animatorR1;
    private Animator _animatorL2;
    private Animator _animatorR2;
    private Animator _animatorL3;
    private Animator _animatorR3;
    [SerializeField] private GameObject DoorL;
    [SerializeField] private GameObject DoorR;
    [SerializeField] private GameObject DoorL1;
    [SerializeField] private GameObject DoorR1;
    [SerializeField] private GameObject DoorL2;
    [SerializeField] private GameObject DoorR2;
    [SerializeField] private GameObject DoorL3;
    [SerializeField] private GameObject DoorR3;

    // Use this for initialization
    void Start () {
        _animatorL = DoorL.GetComponent<Animator>();
        _animatorR = DoorR.GetComponent<Animator>();
        _animatorL1 = DoorL1.GetComponent<Animator>();
        _animatorR1 = DoorR1.GetComponent<Animator>();
        _animatorL2 = DoorL2.GetComponent<Animator>();
        _animatorR2 = DoorR2.GetComponent<Animator>();
        _animatorL3 = DoorL3.GetComponent<Animator>();
        _animatorR3 = DoorR3.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Drone")
        {
            _animatorL.SetBool("Open", true);
            _animatorR.SetBool("Open", true);
            _animatorL1.SetBool("Open", true);
            _animatorR1.SetBool("Open", true);
            _animatorL2.SetBool("Open", true);
            _animatorR2.SetBool("Open", true);
            _animatorL3.SetBool("Open", true);
            _animatorR3.SetBool("Open", true);
        }    
    }
}
