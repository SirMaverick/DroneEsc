﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


class SystemEnergyController : MonoBehaviour
{
    [SerializeField]
    private float maxEnergy = 100.0f;
    [SerializeField]
    private float energyLevelTotal = 10.0f;

    private float energyLevelCurrent;
    /* [SerializeField]
     private float energyTimeLost = 1.0f;*/

    SystemUIController uiController;
    bool systemMode = false;

    [SerializeField] private int sceneIndexGoodEnding;

    private void Start()
    {
        uiController = FindObjectOfType<SystemUIController>();
        uiController.SetMaxEnergyLevel(energyLevelTotal);
    }

    private void Update()
    {

        if (systemMode)
        {
            energyLevelCurrent = energyLevelTotal - Time.time;
            uiController.SetEnergyLevelBar(energyLevelCurrent);
            //Debug.Log(energyLevelCurrent);
            if (energyLevelCurrent < 0)
            {
                Debug.LogError("you lost");
                SceneManager.LoadScene(sceneIndexGoodEnding);
            }
        }
    }
    
    // start lowering the energy
    public void EnableController()
    {
        systemMode = true;
        energyLevelTotal += Time.time;
    }

    public void AddEnergyFromCore(CoreDrone coreDrone)
    {
        
        energyLevelTotal += coreDrone.TakeEnergy(); 
        energyLevelCurrent = energyLevelTotal - Time.time;
        // limit the energy
        if (energyLevelCurrent > maxEnergy)
        {
            energyLevelTotal -= energyLevelCurrent - maxEnergy;
            energyLevelCurrent -= energyLevelCurrent - maxEnergy;
        }
    }
}

