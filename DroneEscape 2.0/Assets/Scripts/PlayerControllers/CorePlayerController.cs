using UnityEngine;
using System.Collections;

public class CorePlayerController : AbstractPlayerController
    {
 
    [SerializeField]
    private GameObject core;
    [SerializeField]
    private GameObject cameraCenter;

    Vector3 lastPos;
    Vector3 currentPos;

    private bool isFlying;


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

        camera.GetComponent<AudioListener>().enabled = true;

        StartCoroutine(CheckGrounded());
        isFlying = true;
    }

    void CoreOnGround()
    {
        //camera.transform.parent = null;
        core.GetComponent<MoveOnBelt>().flying = false;
        //enabled = false;
    }

    private void Update()
    {
        if (isFlying)
        {
            cameraCenter.transform.position = core.transform.position;
        }
    }

    IEnumerator CheckGrounded()
    {
        lastPos = core.transform.position;
        yield return new WaitForSeconds(0.02f);
        currentPos = core.transform.position;
        
        if (Vector3.Distance(lastPos, currentPos) == 0)
        {
            isFlying = false;
            CoreOnGround();
        }
        if (isFlying)
        {
            StartCoroutine(CheckGrounded());
        }
    }
}
