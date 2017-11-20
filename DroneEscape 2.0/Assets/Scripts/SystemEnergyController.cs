using UnityEngine;
using System.Collections;


class SystemEnergyController : MonoBehaviour
{
    [SerializeField]
    private float energyLevelTotal = 10.0f;

    [SerializeField]
    private float droneEnergyValue = 20.0f;

    private float energyLevelCurrent;
   /* [SerializeField]
    private float energyTimeLost = 1.0f;*/

    private void Update()
    {
        energyLevelCurrent = energyLevelTotal - Time.time;
        Debug.Log(energyLevelCurrent);
        if(energyLevelCurrent < 0)
        {
            Debug.LogError("you lost");
        }
    }

    public void AddEnergyFromCore()
    {
        energyLevelTotal += droneEnergyValue;
    }
}

