using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTrigger : MonoBehaviour
{
    private SystemArm systemArm;
    [SerializeField] private GameObject droneObject;
    [SerializeField] private bool alwaysTriggerOnEnter = false;
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
                alreadyTriggered = true;
            }
        }

    }
}
