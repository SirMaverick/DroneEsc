using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisableButton : MonoBehaviour {

    private bool inRange = false;
    private bool disabled = false;
    [SerializeField]
    private GuardFOV cameraGuard;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (inRange)
        {
            if (!disabled)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    cameraGuard.DisableGuard();
                    disabled = true;
                    return;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    cameraGuard.EnableGuard();
                    disabled = false;
                    return;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}
