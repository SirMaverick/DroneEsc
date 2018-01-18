using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GateAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator conveyorAnimatorStart, conveyorAnimatorEnd, rollerAnimatorStart, rollerAnimatorEnd;
    [SerializeField] BoxCollider conveyorCollider;


    public void OpenGate()
    {
        conveyorCollider.enabled = true;
        animator.SetBool("Open", true);
        conveyorAnimatorStart.SetBool("Open", true);
        conveyorAnimatorEnd.SetBool("Open", true);
        rollerAnimatorStart.SetBool("StartForward", true);
        rollerAnimatorEnd.SetBool("StartBackward", true);
    }

    public void CloseGate()
    {
        conveyorCollider.enabled = false;
        animator.SetBool("Open", false);
        conveyorAnimatorStart.SetBool("Open", false);
        conveyorAnimatorEnd.SetBool("Open", false);
        rollerAnimatorStart.SetBool("StartForward", false);
        rollerAnimatorEnd.SetBool("StartBackward", false);
    }
}