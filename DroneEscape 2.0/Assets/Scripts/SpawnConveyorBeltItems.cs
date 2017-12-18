using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConveyorBeltItems : MonoBehaviour {

    [SerializeField] private GameObject[] items;
    [SerializeField] private float spawnDelay = 5.0f;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float initialWaitTime = 0.0f;
    private int index = 0;

    // Use this for initialization
    void Start () {
        StartCoroutine(Wait(initialWaitTime));
    }
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject item in items)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, endPosition.position, speed * Time.deltaTime);
            if(item.transform.position == endPosition.position)
            {
                item.GetComponent<Renderer>().enabled = false;
            }
        }
	}

    IEnumerator SpawnItem()
    {
        items[index].GetComponentInChildren<Renderer>().enabled = true;
        items[index].transform.position = startPosition.position;
        yield return new WaitForSeconds(spawnDelay);
        index++;
        if(index >= items.Length)
        {
            index = 0;
        }
        StartCoroutine(SpawnItem());
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(SpawnItem());
    }
}
