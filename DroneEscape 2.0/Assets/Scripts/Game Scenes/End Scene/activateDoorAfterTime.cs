using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateDoorAfterTime : MonoBehaviour {

    [SerializeField] private float timeTilOpen;
    [SerializeField] private float timeTilFade;
    [SerializeField] private GameObject Door;
    private Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = Door.GetComponent<Animator>();
        _animator.SetBool("openDoor", false);
        StartCoroutine(StartDoor());
	}
	
    IEnumerator StartDoor()
    {
        yield return new WaitForSeconds(timeTilOpen);
        _animator.SetBool("openDoor", true);
        yield return new WaitForSeconds(timeTilFade);
        FadeToWhite.Instance.CallFading();
    }
}
