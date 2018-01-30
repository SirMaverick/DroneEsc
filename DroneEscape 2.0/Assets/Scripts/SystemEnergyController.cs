using UnityEngine;
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
    private float startTime;
    [SerializeField]
    private float badEndingTime = 60.0f;

    SystemUIController uiController;
    bool systemMode = false;

    [SerializeField] private int sceneIndexGoodEnding;
    [SerializeField] private int sceneIndexBadEnding;

    private void Start()
    {
        uiController = FindObjectOfType<SystemUIController>();
        uiController.SetMaxEnergyLevel(maxEnergy);
    }

    private void Update()
    {

        if (systemMode)
        {
            energyLevelCurrent = energyLevelTotal - Time.time;
            uiController.SetEnergyLevelBar(energyLevelCurrent);
            if (energyLevelCurrent < 0)
            {
                SceneManager.LoadScene(sceneIndexGoodEnding);
            }
            // player played long enough to choose
            if (Time.time >= startTime + badEndingTime)
            {
                SceneManager.LoadScene(sceneIndexBadEnding);
            }
        }
    }

    // start lowering the energy
    public void EnableController()
    {
        startTime = Time.time;
        systemMode = true;
        energyLevelTotal += startTime;
    }

    public void AddEnergyFromCore(CoreDrone coreDrone)
    {
        if (systemMode)
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
}