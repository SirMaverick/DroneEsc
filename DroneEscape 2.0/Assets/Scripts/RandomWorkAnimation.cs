using System.Collections;
using UnityEngine;

public class RandomWorkAnimation : MonoBehaviour
{
    [SerializeField] private int amountOfAnimations = 4;
    private Animator animator;

    public void WorkAnimationDone()
    {
        // done with the current work animation
        animator.SetInteger("anim", Random.Range(0, amountOfAnimations));
    }
}

