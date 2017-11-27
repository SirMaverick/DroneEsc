using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {

    private Animator _animator;
    [SerializeField] private float timeBeforeClose;
    [SerializeField] private GameObject Door;

    // Use this for initialization
    void Start () {
        _animator = Door.GetComponent<Animator>();
        _animator.SetBool("openDoor", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Drone")
        {
            _animator.SetBool("openDoor", true);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(DoorCloser());
    }

    IEnumerator DoorCloser()
    {
        yield return new WaitForSeconds(timeBeforeClose);
        _animator.SetBool("openDoor", false);
    }
}
