using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateButton : Button {
    [SerializeField] AbstractPlayerController playerController;

	
    public override void Toggle()
    {
        playerControllerSupervisor.SwitchPlayerController(playerController);
    }
}
