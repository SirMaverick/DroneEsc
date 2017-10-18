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
    public GameObject DoorL;
    public GameObject DoorR;
    public GameObject DoorL1;
    public GameObject DoorR1;
    public GameObject DoorL2;
    public GameObject DoorR2;
    public GameObject DoorL3;
    public GameObject DoorR3;
    public GameObject DoorL4;
    public GameObject DoorR4;
    public GameObject DoorL5;
    public GameObject DoorR5;
    public GameObject DoorL6;
    public GameObject DoorR6;

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
        if(other.tag == "Player")
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

    // Update is called once per frame
    void Update () {
		
	}
}
