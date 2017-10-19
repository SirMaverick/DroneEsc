using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisableButton : Button {

    [SerializeField]
    private GuardFOV cameraGuard;

    private void Start()
    {
        
    }



    public override void Toggle()
    {
        if (!enabled)
        {
            cameraGuard.DisableGuard();
            enabled = true;
        }
        else
        {
            cameraGuard.EnableGuard();
            enabled = false;
        }
    }

}
