using UnityEngine;
using System.Collections;

public class CorePlayerController : AbstractPlayerController
    {
 
    [SerializeField]
    private GameObject core;

    Vector3 lastPos;
    Vector3 currentPos;

    private bool isThrown;


    public GameObject GetCore()
    {
        return core;
    }

    public override void EnableController()
    {
        base.EnableController();
        TurnOnCore();
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
        isThrown = true;
    }

    void CoreOnGround()
    {
        camera.transform.parent = null;
        core.GetComponent<MoveOnBelt>().flying = false;
        enabled = false;
    }

    IEnumerator CheckGrounded()
    {
        lastPos = core.transform.position;
        yield return new WaitForSeconds(0.05f);
        currentPos = core.transform.position;
        if (Vector3.Distance(lastPos, currentPos) == 0)
        {
            isThrown = false;
            CoreOnGround();
        }
        if (isThrown)
        {
            StartCoroutine(CheckGrounded());
        }
    }
}
