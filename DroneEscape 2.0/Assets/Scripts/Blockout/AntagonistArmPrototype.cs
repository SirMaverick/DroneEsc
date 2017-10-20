using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntagonistArmPrototype : MonoBehaviour {

    [SerializeField] private GameObject antagonistArm;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private Camera animationCamera;
    [SerializeField] private float timeBeforeGrab;
    [SerializeField] private float yPositionArm;


    // Use this for initialization
    void Start ()
    {
		
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
        yield return new WaitForSeconds(timeBeforeGrab);
        Instantiate(antagonistArm, new Vector3(playerTransform.transform.position.x, yPositionArm, playerTransform.transform.position.z), Quaternion.identity);
        playerGameObject.GetComponent<PlayerMovement>().enabled = false;
        Instantiate(animationCamera, playerGameObject.GetComponentInChildren(typeof(Camera)).transform.position, Quaternion.identity);
        playerGameObject.SetActive(false);

        
        //Deactivate shell, activate seperate camera, activate arm animation
    }
}
