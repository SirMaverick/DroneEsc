using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour {

    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject Drone;
    [SerializeField] private float TimeBetweenSpawns;
    [SerializeField] private Transform TargetA;
    [SerializeField] private Transform TargetB;

    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(TimeBetweenSpawns);
        GameObject go = Instantiate(Drone, SpawnPoint.position, SpawnPoint.rotation);
        go.GetComponent<DroneConveyorMove>().TargetA = TargetA;
        go.GetComponent<DroneConveyorMove>().TargetB = TargetB;
        StartCoroutine(Spawn());
    }
}
