using UnityEngine;
using System.Collections;

public class CorePlayerController : AbstractPlayerController
    {
 
    [SerializeField]
    private GameObject core;
    [SerializeField]
    private GameObject cameraCenter;
    private GameObject pulse1, pulse2;

    Vector3 lastPos;
    Vector3 currentPos;

    private bool isFlying;

    protected override void Start()
    {
        if (camera.enabled)
        {
            GuardFOV[] guards = FindObjectsOfType<GuardFOV>();

            foreach (GuardFOV guard in guards)
            {
                guard.ChangePlayer(core);
            }
        }
        base.Start();
        pulse1 = GameObject.Find("Pulse1");
        pulse2 = GameObject.Find("Pulse2");
        StartCoroutine(TurnOnPulses());
    }


    public GameObject GetCore()
    {
        return core;
    }

    public override void EnableController()
    {
        base.EnableController();
        GetComponentInChildren<CameraCollision>().enabled = true;
        TurnOnCore();
    }

    public override void DisableController()
    {
        base.DisableController();
        GetComponentInChildren<CameraCollision>().enabled = false;
    }


    

    private void TurnOnCore()
    {
        core.GetComponent<BoxCollider>().enabled = true;
        core.GetComponent<MeshRenderer>().enabled = true;
        core.GetComponent<Rigidbody>().useGravity = true;
        core.GetComponent<MoveOnBelt>().flying = true;
        core.GetComponent<MoveOnBelt>().pickedUp = false;

        

     //   StartCoroutine(CheckGrounded());
        isFlying = true;
    }

    void CoreOnGround()
    {
        //camera.transform.parent = null;
        StartCoroutine(TurnOnPulses());
        core.GetComponent<MoveOnBelt>().flying = false;
        //enabled = false;
    }

    private void FixedUpdate()
    {
        if (isFlying)
        {
            currentPos = core.transform.position;

            if (Vector3.Distance(lastPos, currentPos) == 0)
            {
                isFlying = false;
                CoreOnGround();
            }


            cameraCenter.transform.position = core.transform.position;

           
        }
        lastPos = core.transform.position;
    }

    public IEnumerator TurnOnPulses() {
        GetComponent<CoreMovementController>().TurnOnPulse();
        pulse1.GetComponent<Menu_Button_Pulse>().TurnOnPulse();
        yield return new WaitForSeconds(3.0f);
        pulse2.GetComponent<Menu_Button_Pulse>().TurnOnPulse();
    }

    public void TurnOffPulses() {
        pulse1.GetComponent<Menu_Button_Pulse>().TurnOffPulse();
        pulse2.GetComponent<Menu_Button_Pulse>().TurnOffPulse();
    }
}
