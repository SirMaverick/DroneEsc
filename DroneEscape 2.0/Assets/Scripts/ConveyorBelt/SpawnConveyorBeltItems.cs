using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConveyorBeltItems : ArmEventListener {

    [SerializeField] private ConveyorBeltItem[] items;
                     private Transform startPosition;
                     private Transform endPosition;
    [SerializeField] private Transform[] positions;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private ArmEventListener[] armEventListeners;
    [SerializeField] private bool alreadyRunning = true;
    [SerializeField] private float distanceToMove = 2.25f;
    [SerializeField] private bool coreOnConveyor = false;

    private ConveyorBeltItem coreItem;

    private int index = 0;

    private bool state = false;

    // Use this for initialization
    void Start () {
        //StartCoroutine(Wait(initialWaitTime));
        startPosition = positions[0];
        endPosition = positions[positions.Length - 1];

        foreach (ConveyorBeltItem item in items)
        {
            Renderer renderer = item.GetComponent<Renderer>();
            if (renderer == null)
            {
                renderer = item.GetComponentInChildren<Renderer>();
            }


            if (alreadyRunning)
            {
                if (index >= positions.Length)
                {
                    // already reached end so
                    item.transform.position = endPosition.position;
                    renderer.enabled = false;
                }
                else
                {
                    item.transform.position = positions[positions.Length -1 - index].position;

                    renderer.enabled = true;
                    index++;
                }

                if (index >= positions.Length)
                {
                    // already reached end so
                    item.nextPosition = endPosition;
                    item.nextPositionId = -1;
                }
                else
                {
                    item.nextPosition = positions[positions.Length - index];
                    item.nextPositionId = positions.Length - index;
                }
            }
            else
            {
                item.nextPosition = endPosition;
                item.nextPositionId = -1;
                renderer.enabled = false;
            }

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
                // Destroy box does this
               // item.GetComponent<Renderer>().enabled = false;
                //SendEndEvent();
            }
            else if (item.transform.position.Equals(item.nextPosition.position))
            {
                // don't schedule multiple coroutines to do the same thing 
                if (item.state != state)
                {
                    item.NextPosition(positions[item.nextPositionId + 1], item.nextPositionId + 1);
                    item.state = state;
                    if (coreOnConveyor)
                    {
                        coreItem.state = true;
                    }
                }
                else if (coreOnConveyor)
                    {
                        coreItem.state = false;
                    }
                
                //item.nextPosition = positions[item.nextPositionId + 1];
                //item.nextPositionId++;
            }
        }
        if (coreOnConveyor)
        {
            if (coreItem.state)
            {
                coreItem.transform.position = Vector3.MoveTowards(coreItem.transform.position, endPosition.position, speed * Time.deltaTime);
            }
        }
	}
    
    // Start event to spawn an item (this is done by an animation as name suggests)
    public void AnimationArmDone()
    {
        SendArmEvent();
        state = !state;
        if (index >= items.Length)
        {
            index = 0;
        }
        // arm is done so "spawn" item at the start position
        items[index].GetComponentInChildren<Renderer>().enabled = true;
        items[index].transform.position = startPosition.position;
        items[index].nextPosition = positions[1]; // position after the startPosition ;)
        items[index].nextPositionId = 1;
        items[index].state = state;
        index++;
    }

    // Used to send an end event when an item has reached the endpoint which can be used for starting/synchronizing an animation
    private void SendArmEvent()
    {
        foreach(ArmEventListener armEventListener in armEventListeners)
        {
            armEventListener.ArmEvent();
        }
    }

    public override void ArmEvent()
    {
        AnimationArmDone();
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject coreGameObject = other.gameObject;
        if (coreGameObject == FindObjectOfType<CoreObject>().gameObject)
        {
            Vector3 xyz = coreGameObject.transform.position;
            xyz.x = transform.position.x;
            
            coreItem = coreGameObject.GetComponent<ConveyorBeltItem>();
            coreItem.transform.position = xyz;
            coreOnConveyor = true;
        }
    }

    /*public void AddEndEventListener(EndEventListener endEventListener)
    {
        endEventListeners.Add(endEventListener);
    }*/

}
