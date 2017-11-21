using UnityEngine;
using System.Collections;

// It doesn't control the animation but this player controller provides the player with controls during the animation (basically do nothing)
class AnimationPlayerController : AbstractPlayerController
    {
    [SerializeField]
    private AbstractPlayerController nextPlayerController;


    [SerializeField]
    private Animation anim;
    [SerializeField]
    private AnimationClip animClip;


    // Use this for initialization
    protected override void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        base.Start();
        anim.clip = animClip;
        anim.Play();
        StartCoroutine(AnimCheck(animClip.length));
    }


    IEnumerator AnimCheck(float animLength)
    {
        Debug.Log("AnimCheck");
        Debug.Log(animLength);
        yield return new WaitForSeconds(animLength);
        // after animation is done switch to the next playerController
        PlayerControllerSupervisor.GetInstance().SwitchPlayerController(nextPlayerController);
    }
}

