using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTimerAnimation : MonoBehaviour {

    private Animator _animator;
    [SerializeField] private float timeBeforeClose;
    [SerializeField] private float timeBeforeOpen;
    [SerializeField] private float firstTimeClose;
    [SerializeField] private GameObject animObject;
    public bool Close;

    // Use this for initialization
    void Start () {

        _animator = animObject.GetComponent<Animator>();
        _animator.SetBool("Close", false);
        StartCoroutine(CloseAnim(firstTimeClose));
    }

    IEnumerator CloseAnim(float time)
    {
        yield return new WaitForSeconds(time);
        _animator.SetBool("Close", true);
        StartCoroutine(OpenAnim(timeBeforeOpen));
    }
    IEnumerator OpenAnim(float time)
    {
        yield return new WaitForSeconds(time);
        _animator.SetBool("Close", false);
        StartCoroutine(CloseAnim(timeBeforeClose));
    }

}
