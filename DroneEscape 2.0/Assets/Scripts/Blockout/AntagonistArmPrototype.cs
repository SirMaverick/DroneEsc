using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagonistArmPrototype : MonoBehaviour {

    [SerializeField] private GameObject antagonistArm;
   // [SerializeField] private Transform playerTransform;
    //[SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject animationCamera;
    [SerializeField] private GameObject targetPoint;
    [SerializeField] private float timeBeforeGrab;
    [SerializeField] private float yPositionArm;
    [SerializeField] private bool updateRotation;


    // Use this for initialization
    void Start ()
    {
        updateRotation = false;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drone")
        {
            StartCoroutine(ArmGrabSequence());
        }
    }*/

    public IEnumerator ArmGrabSequence(GameObject playerGameObject)
    {
        //playerGameObject.GetComponent<PlayerMovement>().enabled = false;
        AbstractPlayerController apc = playerGameObject.GetComponent<AbstractPlayerController>();
        if (playerGameObject == FindObjectOfType<CoreObject>().gameObject)
        {
            apc = FindObjectOfType<CorePlayerController>();
        }
        
        GameObject target = Instantiate(targetPoint, new Vector3(apc.GetCamera().transform.position.x, apc.GetCamera().transform.position.y + 10, apc.GetCamera().transform.position.z), Quaternion.identity);
        GameObject go = Instantiate(animationCamera, apc.GetCamera().transform.position, apc.GetCamera().transform.rotation);
        go.GetComponent<ArmCameraRotation>().target = target.transform;
        Instantiate(antagonistArm, new Vector3(go.transform.position.x, yPositionArm, go.transform.position.z), Quaternion.identity);
        playerGameObject.SetActive(false);
        yield return new WaitForSeconds(timeBeforeGrab);
        FadeToWhite.Instance.CallFading();


        //Deactivate shell, activate seperate camera, activate arm animation
    }
}
