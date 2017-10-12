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


    public void ToggleEnableCamera()
    {
        if (!disabled)
        {
            cameraGuard.DisableGuard();
            disabled = true;
        }
        else
        {
            cameraGuard.EnableGuard();
            disabled = false;
        }
    }

}
