using System.Collections;
using UnityEngine;

public class DroneEnergy : MonoBehaviour
{
    [SerializeField]
    private float minEnergy = 0;
    [SerializeField]
    private float maxEnergy = 100;
    [SerializeField]
    private float energy;


    private void Start()
    {
        energy = Random.Range(minEnergy, maxEnergy);
    }

    public float TakeEnergy()
    {
        float temp = energy;
        energy = 0;
        return energy;
    }

    public float GetEnergy()
    {
        return energy;
    }

}

