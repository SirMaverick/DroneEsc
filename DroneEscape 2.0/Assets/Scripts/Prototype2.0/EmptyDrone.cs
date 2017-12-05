using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyDrone : MonoBehaviour, Selectable {

    [SerializeField] private DroneAnimation droneAnimation;


    private Transform cameraObject;
    private GameObject coreCamera;
    private GameObject corePickUp;
    private bool walk;
    private GameObject ownCamera;
    [SerializeField] GameObject objectPlacement;

    [SerializeField] SkinnedMeshRenderer meshRenderer;

    public float minDist;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed = 5;

    PlayerControllerSupervisor pcs;
    [SerializeField] bool rotated = false;


    // Use this for initialization
    void Start () {
        ownCamera = transform.Find("DroneCamera").gameObject;
        pcs = PlayerControllerSupervisor.GetInstance();
    }

    // Update is called once per frame
    void Update() {

        if (walk) {
            //transform.LookAt(new Vector3 (cameraObject.position.x, transform.position.y, cameraObject.position.z));
            if (!rotated)
            {
                LookAtSlow();
                return;
            }

            if (Vector3.Distance(transform.position, cameraObject.GetComponent<CorePlayerController>().GetCore().transform.position) >= minDist) {

                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            } else {
                walk = false;
                droneAnimation.PickUp();

            }
        }
    }
    public void WalkToPlayer(Transform tempCore) {
        droneAnimation.WakeUp();
        cameraObject = tempCore.parent;
        coreCamera = tempCore.gameObject;
    }

    public void AnimWakeUpDone()
    {
        walk = true;
    }

    public void AnimPickUpDone()
    {
        //
        UpdateGuards();
        TurnOnDrone();
    }


    void TurnOnDrone() {

        cameraObject.transform.position = transform.position;

        corePickUp = cameraObject.GetComponent<CorePlayerController>().GetCore();
        corePickUp.transform.parent = transform;
        corePickUp.transform.position = objectPlacement.transform.position;

        //GetComponent<MeshRenderer>().enabled = false;
        // GetComponent<DronePlayerController>().SwitchLight();
        pcs.SwitchPlayerController(GetComponent<DronePlayerController>());
        //ownCamera.GetComponent<AudioListener>().enabled = true;
       // ownCamera.GetComponent<Camera>().enabled = true;
       

        enabled = false;
        rotated = false;

    }
    // Let the guards know which GameObject is the player (only keeping track of one object for effeciency)
    private void UpdateGuards()
    {
        GuardFOV[] guards = FindObjectsOfType<GuardFOV>();
        foreach (GuardFOV guard in guards)
        {
            guard.ChangePlayer(gameObject);
        }
    }

    public void LookingAt()
    {
        meshRenderer.material.SetColor("_ColorFresh", new Color(255.0f / 255.0f, 140.0f / 255.0f, 0));
    }

    public void StopLookingAt()
    {
        meshRenderer.material.SetColor("_ColorFresh", new Color(0, 72.0f / 255.0f, 255.0f / 255.0f));
    }


    public void LookAtSlow()
    {
        corePickUp = cameraObject.GetComponent<CorePlayerController>().GetCore();
        Vector3 pointToLookAt =  new Vector3(corePickUp.transform.position.x, transform.position.y, corePickUp.transform.position.z) - transform.position;

        
        Quaternion previousRotation = transform.rotation;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(pointToLookAt), 5);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(pointToLookAt), Time.deltaTime * rotationSpeed);
        //transform.rotation = Quaternion.LookRotation(pointToLookAt);
        if (previousRotation == transform.rotation)
        {
            rotated = true;
        }
        
    }
}
