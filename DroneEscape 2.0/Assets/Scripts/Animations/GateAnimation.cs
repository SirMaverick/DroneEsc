using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GateAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;


    public void OpenGate()
    {
        animator.SetBool("Open", true);
    }

    public void CloseGate()
    {
        animator.SetBool("Open", false);
    }
}