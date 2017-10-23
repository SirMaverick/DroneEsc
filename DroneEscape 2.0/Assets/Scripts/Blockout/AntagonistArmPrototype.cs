using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagonistArmPrototype : MonoBehaviour {

    [SerializeField] private GameObject antagonistArm;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject animationCamera;
    [SerializeField] private float timeBeforeGrab;
    [SerializeField] private float yPositionArm;
    [SerializeField] private bool updateRotation;


    // Use this for initialization
    void Start ()
    {
        updateRotation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drone")
        {
            StartCoroutine(ArmGrabSequence());
        }
    }

    IEnumerator ArmGrabSequence()
    {
        playerGameObject.GetComponent<PlayerMovement>().enabled = false;
        GameObject go = Instantiate(animationCamera, playerGameObject.GetComponentInChildren(typeof(Camera)).transform.position, playerGameObject.GetComponentInChildren(typeof(Camera)).transform.rotation);
        playerGameObject.SetActive(false);
        Instantiate(antagonistArm, new Vector3(go.transform.position.x, yPositionArm, go.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(timeBeforeGrab);
        FadeToWhite.Instance.CallFading();


        //Deactivate shell, activate seperate camera, activate arm animation
    }
}
