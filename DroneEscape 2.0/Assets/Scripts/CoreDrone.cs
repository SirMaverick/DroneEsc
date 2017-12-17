using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDrone : MonoBehaviour, Selectable
{
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] DroneEnergy energy;

    [SerializeField] float minEnergyToPickup = 20;

    [SerializeField] CoreDroneAnimation coreDroneAnimation;

    public void Start()
    {
        if (IsAllowedToBePickup())
        {
            meshRenderer.material.SetFloat("_Heartbeat", 1);
            StopLookingAt();
        }
    }


    public void LookingAt()
    {
        if (IsAllowedToBePickup())
        {
           // meshRenderer.material.SetFloat("_Heartbeat", 1);
            meshRenderer.material.SetColor("_ColorFresh", new Color(255.0f / 255.0f, 140.0f / 255.0f, 0));
        }
    }

    public void StopLookingAt()
    {
        //meshRenderer.material.SetFloat("_Heartbeat", 0);
        meshRenderer.material.SetColor("_ColorFresh", new Color(0, 72.0f / 255.0f, 255.0f / 255.0f));
    }

    public DroneEnergy GetDroneEnergy()
    {
        return energy;
    }

    public float GetDroneEnergyValue()
    {
        return energy.GetEnergy();
    }   

    public bool IsAllowedToBePickup()
    {
        return energy.GetEnergy() >= minEnergyToPickup;
    }

    public float TakeEnergy()
    {
        meshRenderer.material.SetFloat("_Heartbeat", 0);
        return energy.TakeEnergy();
    }

    public void CaughtByArm()
    {
        coreDroneAnimation.CaughtByArm();
    }

    public void OtherDroneCaughtByArm()
    {
        coreDroneAnimation.Shocked();
    }
}