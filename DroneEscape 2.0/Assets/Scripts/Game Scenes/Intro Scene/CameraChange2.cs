using UnityEngine;
using System.Collections;

// DO NOT USE goto Jeroen for help
public class CameraChange2 : MonoBehaviour
{
    
    
    public Animation anim;
    public AnimationClip animClip;
    public Camera cam;
    public Camera coreCam;

/*
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim.clip = animClip;
        anim.Play();
        StartCoroutine(AnimCheck(animClip.length));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator AnimCheck(float animLength)
    {
        Debug.Log("AnimCheck");
        Debug.Log(animLength);
        yield return new WaitForSeconds(animLength);
        cam.enabled = false;
        coreCam.enabled = true;
    }*/
}
