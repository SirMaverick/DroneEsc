using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeEndingSequence : MonoBehaviour
{

    [SerializeField] private Camera playerCamera, camera1, camera2, camera3;
    [SerializeField] private float timeBetweenCamera;
    [SerializeField] private GameObject spawner1, spawner2, spawner3;



    // Use this for initialization
    void Awake()
    {
        camera1.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        spawner1.SetActive(false);
        spawner2.SetActive(false);
        spawner3.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Drone")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                print("E has been pressed");
                StartCoroutine(sacrificeSequence());
            }
        }
    }

    IEnumerator sacrificeSequence()
    {
        
        playerCamera.enabled = false;
        spawner1.SetActive(true);
        camera1.enabled = true;
        yield return new WaitForSeconds(timeBetweenCamera);
        camera1.enabled = false;
        spawner1.SetActive(false);
        spawner2.SetActive(true);
        camera2.enabled = true;
        yield return new WaitForSeconds(timeBetweenCamera);
        camera2.enabled = false;
        spawner2.SetActive(false);
        spawner3.SetActive(true);
        camera3.enabled = true;
        yield return new WaitForSeconds(timeBetweenCamera);
        FadeToWhite.Instance.CallFading();
    }
}