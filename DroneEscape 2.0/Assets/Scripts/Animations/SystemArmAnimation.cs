using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SystemArmAnimation : MonoBehaviour
{
    private Animator animator;
    private List<LiftDroneCallBack> callBacksLift;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PickUpDrone(LiftDroneCallBack callback = null)
    {
        if(callback != null)
        {
            callBacksLift.Add(callback);
        }
        animator.SetBool("PickUp", true);
    }

    public void PickUpDroneDone()
    {
        animator.SetBool("PickUp", false);

        foreach(LiftDroneCallBack callback in callBacksLift)
        {
            callback.CallBack();
        }
        callBacksLift.Clear();

    }

    public void RegisterCallBack(LiftDroneCallBack callBack)
    {
        callBacksLift.Add(callBack);
    }
}

