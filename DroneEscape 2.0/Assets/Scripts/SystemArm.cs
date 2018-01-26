using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

class SystemArm : MonoBehaviour
{
    private bool reachedTarget = true;
    private bool reachedRoof = true;
    private bool droneLift = true;

    private Vector3 targetPosition;
    //[SerializeField]
    private float heightRoof;

    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float liftHeight = 3;

    private CoreDrone targetDrone;

    private NewSystemPlayerController playerController;

    private List<LiftDroneCallBack> callBacksLift;
    private bool firstLift = true;

    [SerializeField] RuntimeAnimatorController droneAnimator;



    private void Start()
    {
        playerController = FindObjectOfType<NewSystemPlayerController>();
        callBacksLift = new List<LiftDroneCallBack>();
    }

    private void Update()
    {
        if (!reachedTarget)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
            // Reached the drone
            if(gameObject.transform.position.y <= targetPosition.y)
            {
                reachedTarget = true;
                reachedRoof = false;

                firstLift = true;
            } 
        }else if (!reachedRoof)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);

            // Lifting a drone
            if (droneLift)
            {
                // Reached max lifting height of the drone
                if (targetDrone.transform.position.y < liftHeight)
                {
                    targetDrone.transform.Translate(Vector3.up * Time.deltaTime * speed);
                    if (firstLift)
                    {
                        playerController.SetCoreDrone(targetDrone);
                        foreach (LiftDroneCallBack callBack in callBacksLift)
                        {
                            callBack.CallBack();
                        }
                        firstLift = false;
                        callBacksLift = new List<LiftDroneCallBack>();
                    }
                }
                else
                {

                    targetDrone.GetComponent<Rigidbody>().isKinematic = false;
                    targetDrone.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    // make the drone an empty shell
                    targetDrone.GetComponent<EmptyDrone>().enabled = true;
                    targetDrone.tag = "Drone";// yes this is ugly as is the next few things
                    targetDrone.GetComponent<Animator>().runtimeAnimatorController = droneAnimator;
                    targetDrone.GetComponent<DronePlayerController>().enabled = true;
                    targetDrone.GetComponent<NavMeshAgent>().enabled = true;
                    targetDrone.GetComponentInChildren<DronePulse>().GetComponent<BoxCollider>().enabled = true;
                    droneLift = false;
                    // remove energy
                    playerController.GiveEnergy();
                }

            }
            // Reached roof
            if (gameObject.transform.position.y >= heightRoof)
            {
                reachedRoof = true;
            }
        }
    }

    public bool PickUpDrone()
    {
        if (targetDrone != null)
        {
            // check if the drone is allowed to be picked up (has enough energy)
            if (targetDrone.IsAllowedToBePickup())
            {
                // MoveTo checks if the arm is allowed to pickup the drone
                return MoveTo(targetDrone);
            }
        }
        return false;
    }

    public bool MoveTo(CoreDrone target)
    {
        // only do something if we are not tyring to lift or are lifting a drone
        if (reachedRoof && reachedTarget)
        {
            targetDrone = target;
            targetDrone.GetComponent<Rigidbody>().isKinematic = true;
            targetPosition = target.transform.position;
            // length of the arm, yea ...
            targetPosition.y += 6.5f;
            // Let the old y position be the same as the position it will start from and will move back to
            heightRoof = gameObject.transform.position.y;
            gameObject.transform.position = new Vector3(targetPosition.x, heightRoof, targetPosition.z);
            reachedTarget = false;
            droneLift = true;
            return true;
        }
        return false;
    }

    public void RegisterCallBack(LiftDroneCallBack callBack)
    {
        callBacksLift.Add(callBack);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (reachedRoof && reachedTarget)
        {
            CoreDrone coreDrone = other.GetComponent<CoreDrone>();
            if (coreDrone != null)
            {
                targetDrone = coreDrone;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (reachedRoof && reachedTarget)
        {
            CoreDrone coreDrone = other.GetComponent<CoreDrone>();
            if (coreDrone != null)
            {
                if (targetDrone == coreDrone)
                {
                    targetDrone = null;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
    private void OnCollisionStay(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }


}