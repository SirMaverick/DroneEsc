using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.AI;

class SystemArm : MonoBehaviour
{
    /*private bool reachedTarget = true;
    private bool reachedRoof = true;
    private bool droneLift = true;*/
    private bool armIsReady = true;

    private Vector3 targetPosition;
    //[SerializeField]
    private float heightRoof;

    private CoreDrone targetDrone;

    private NewSystemPlayerController playerController;

    private bool liftDroneUp = false;

    [SerializeField] RuntimeAnimatorController droneAnimator;

    private SystemArmAnimation animation;

    [SerializeField] Transform targetTransform;




    private void Start()
    {
        playerController = FindObjectOfType<NewSystemPlayerController>();
        animation = GetComponent<SystemArmAnimation>();
    }

    /*private void Update()
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
    }*/

    private void Update()
    {
        if (liftDroneUp)
        {
            targetDrone.transform.position = targetTransform.position;
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
        if (armIsReady)
        {
            targetDrone = target;
            animation.Animation_PickUpDrone();
            targetDrone.GetComponent<Rigidbody>().isKinematic = true;
            targetPosition = target.transform.position;
            
            // Let the old y position be the same as the position it will start from and will move back to
            heightRoof = gameObject.transform.position.y;
            gameObject.transform.position = new Vector3(targetPosition.x, heightRoof, targetPosition.z);
            armIsReady = false;
            return true;
        }
        return false;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (armIsReady)
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
        
        if (armIsReady)
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


    // Reset collisions
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


    public void PickingUpDrone()
    {
        liftDroneUp = true;
    }

    public void DonePickingUpDrone()
    {
        liftDroneUp = false;
        targetDrone.GetComponent<CoreDroneAnimation>().CaughtByArm();

        targetDrone.GetComponent<Rigidbody>().isKinematic = false;
        targetDrone.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        // make the drone an empty shell
        targetDrone.GetComponent<EmptyDrone>().enabled = true;
        targetDrone.tag = "Drone";// yes this is ugly as is the next few things
        targetDrone.GetComponent<Animator>().runtimeAnimatorController = droneAnimator;
        targetDrone.GetComponent<DronePlayerController>().enabled = true;
        targetDrone.GetComponent<NavMeshAgent>().enabled = true;
        targetDrone.GetComponentInChildren<DronePulse>().GetComponent<BoxCollider>().enabled = true;
        // remove energy
        playerController.GiveEnergy();

    }

    public void PickUpDroneDone()
    {
        armIsReady = true;
    }
}