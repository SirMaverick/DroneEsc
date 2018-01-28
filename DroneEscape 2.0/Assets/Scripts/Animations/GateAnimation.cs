using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GateAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator conveyorAnimatorStart, conveyorAnimatorEnd, rollerAnimatorStart, rollerAnimatorEnd, doorAnimator;
    [SerializeField] BoxCollider conveyorCollider;
    [SerializeField] bool conveyor;
    [SerializeField] bool door;

    public void OpenGate()
    {
        conveyorCollider.enabled = true;
        animator.SetBool("Open", true);
        if (conveyor)
        {
            conveyorAnimatorStart.SetBool("Open", true);
            conveyorAnimatorEnd.SetBool("Open", true);
            rollerAnimatorStart.SetBool("StartForward", true);
            rollerAnimatorEnd.SetBool("StartBackward", true);
        }
        if(door)
        {
            doorAnimator.SetBool("Open", true);
        }
    }

    public void CloseGate()
    {
        conveyorCollider.enabled = false;
        animator.SetBool("Open", false);
        if (conveyor)
        {
            conveyorAnimatorStart.SetBool("Open", false);
            conveyorAnimatorEnd.SetBool("Open", false);
            rollerAnimatorStart.SetBool("StartForward", false);
            rollerAnimatorEnd.SetBool("StartBackward", false);
        }
        if (door)
        {
            doorAnimator.SetBool("openDoor", false);
        }
    }
}