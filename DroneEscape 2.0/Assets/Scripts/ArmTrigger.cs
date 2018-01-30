using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTrigger : MonoBehaviour
{
    private SystemArm systemArm;
    [SerializeField] private CoreDrone droneObject;
    [SerializeField] private bool alwaysTriggerOnEnter = false;
    [SerializeField] private CoreDrone[] shockedDrones;
    private bool alreadyTriggered = false;

    private void Start()
    {
        systemArm = FindObjectOfType<SystemArm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alwaysTriggerOnEnter)
        {
            systemArm.MoveTo(droneObject);
        }
        else
        {
            if (!alreadyTriggered)
            {
                systemArm.MoveTo(droneObject);
  //              systemArm.RegisterCallBack(this);
                alreadyTriggered = true;
            }
        }

    }
/*    public void ShowEffects()
    {
        droneObject.CaughtByArm();
        foreach (CoreDrone shockedDrone in shockedDrones)
        {
            shockedDrone.OtherDroneCaughtByArm();
        }
    }

    public void CallBack()
    {
        ShowEffects();
    }*/
}
