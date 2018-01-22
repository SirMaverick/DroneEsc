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
                // doesn't work for some reason
                //if (coreItem.transform.position.Equals(coreItem.nextPosition.position))
                Vector3 corePos = coreItem.transform.position;
                Vector3 coreNextPos = coreItem.nextPosition.position;
                if ((corePos-coreNextPos).sqrMagnitude < 0.01)
                {
                    if (coreItem.nextPositionId < positions.Length - 1){
                        coreItem.NextPosition(positions[coreItem.nextPositionId + 1], coreItem.nextPositionId + 1);
                    }
                }

                coreItem.transform.position = Vector3.MoveTowards(coreItem.transform.position, coreItem.nextPosition.position, speed * Time.deltaTime);
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
            BoxCollider boxCollider = GetComponent<BoxCollider>() ;
            if (boxCollider.center.x > boxCollider.center.z)
            {
                xyz.z = transform.position.z + boxCollider.center.z;
            }
            else
            {
                xyz.x = transform.position.x;// + boxCollider.center.x;
            }
            
            coreItem = coreGameObject.GetComponent<ConveyorBeltItem>();
            coreItem.transform.position = xyz;
            int id = FindNextPosition(xyz);
            coreItem.NextPosition(positions[id], id);
            coreOnConveyor = true;
        }
    }

    public int FindNextPosition(Vector3 position)
    {
        int id = -2;
        float distance = 999999;
        float distancePrev = 0;
        for(int i = 0; i < positions.Length; i++)
        {
            Vector3 pos = positions[i].position;

            float distanceTemp = Mathf.Abs(position.x - pos.x) + Mathf.Abs(position.y - pos.y) + Mathf.Abs(position.z - pos.z);
            if(distanceTemp < distance)
            {
                distance = distanceTemp;
                id = i;
                if (i > 0)
                {
                    distancePrev = Mathf.Abs(position.x - positions[i - 1].position.x) + Mathf.Abs(position.y - positions[i - 1].position.y) + Mathf.Abs(position.z - positions[i - 1].position.z);
                }
            }
            if(id == i - 1)
            {
                if(distancePrev > distanceTemp)
                {
                    id = i;
                    distancePrev = distanceTemp;
                }
            }

        }
        return id;
    }

    /*public void AddEndEventListener(EndEventListener endEventListener)
    {
        endEventListeners.Add(endEventListener);
    }*/

}
