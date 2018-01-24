using System.Collections;
using UnityEngine;

public class RandomWorkAnimation : ArmEventListener
{
    [SerializeField] private int amountOfAnimations = 4;
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void WorkAnimationDone()
    {
        // done with the current work animation
        int value = Random.Range(0, amountOfAnimations);
        animator.SetInteger("anim", value);
    }

    public override void ArmEvent()
    {
        animator.Play("New State");
    }

}

