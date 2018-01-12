using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConveyorBeltItems : MonoBehaviour {

    [SerializeField] private ConveyorBeltItem[] items;
                     private Transform startPosition;
                     private Transform endPosition;
    [SerializeField] private Transform[] positions;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float waitTime = 0.0f;
    [SerializeField] private EndEventListener[] endEventListeners;
    [SerializeField] private bool alreadyRunning;
    private int index = 0;

    private bool state = false;

    // Use this for initialization
    /*void Start () {
        //StartCoroutine(Wait(initialWaitTime));
        startPosition = positions[0];
        endPosition = positions[positions.Length - 1];

        foreach (ConveyorBeltItem item in items)
        {
            item.nextPosition = endPosition;
            item.nextPositionId = -1;
        }
        //endEventListeners = new List<EndEventListener>();

    }
	
	// Update is called once per frame
	void Update () {
		foreach(ConveyorBeltItem item in items)
        {

            // @ToDo add lerp?
            item.transform.position = Vector3.MoveTowards(item.transform.position, item.nextPosition.position, speed * Time.deltaTime);

            if (item.transform.position == endPosition.position)
            {
                item.GetComponent<Renderer>().enabled = false;
                SendEndEvent();
            }
            else if (item.transform.position.Equals(item.nextPosition.position))
            {
                // don't schedule multiple coroutines to do the same thing 
                if (item.state != state)
                {
                    item.NextPosition(positions[item.nextPositionId + 1], item.nextPositionId + 1);
                    item.state = state;
                }


            
                //item.nextPosition = positions[item.nextPositionId + 1];
                //item.nextPositionId++;
            }
        }
	}
    
    // Start event to spawn an item (this is done by an animation as name suggests)
    public void AnimationArmDone()
    {
        state = !state;
        // arm is done so "spawn" item at the start position
        items[index].GetComponentInChildren<Renderer>().enabled = true;
        items[index].transform.position = startPosition.position;
        items[index].nextPosition = positions[1]; // position after the startPosition ;)
        items[index].nextPositionId = 1;
        items[index].state = state;
        index++;
        if (index >= items.Length)
        {
            index = 0;
        }
    }

    // Used to send an end event when an item has reached the endpoint which can be used for starting/synchronizing an animation
    private void SendEndEvent()
    {
        foreach(EndEventListener endEventListener in endEventListeners)
        {
            endEventListener.EndEvent();
        }
    }

    /*public void AddEndEventListener(EndEventListener endEventListener)
    {
        endEventListeners.Add(endEventListener);
    }*/

}
