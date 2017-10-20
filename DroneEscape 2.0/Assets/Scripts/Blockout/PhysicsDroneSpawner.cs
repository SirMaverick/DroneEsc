using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDroneSpawner : MonoBehaviour {
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject Drone;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float _randomTimeBetweenSpawns = Random.Range(0.5f, 6f);
        yield return new WaitForSeconds(_randomTimeBetweenSpawns);
        GameObject go = Instantiate(Drone, SpawnPoint.position, Random.rotation);
        StartCoroutine(Spawn());
    }
}