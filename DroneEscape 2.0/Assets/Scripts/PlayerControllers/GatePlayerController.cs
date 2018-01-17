using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class GatePlayerController : AbstractPlayerController
{
    private bool openGate = false;
   [SerializeField] private GateAnimation animation;

    public void PostFXExit() {
        movementController.DisableController();
        StartCoroutine(WaitForSwitchingCamera(0.5f));
    }

    private IEnumerator WaitForSwitchingCamera(float waitTime) {
        GenericFunctions.Instance.SetFadeInCamera(cameras[0]);
        yield return new WaitForSeconds(waitTime);
        PlayerControllerSupervisor.GetInstance().SwitchPlayerControllerPrevious();

    }

    public override void EnableController() {
        base.EnableController();
        GenericFunctions.Instance.SetFadeOutCamera(cameras[0]);
        cameras[0].GetComponent<MachinePulse>().StartPulse();
    }

    public void ToggleGate()
    {
        if (openGate)
        {
            animation.CloseGate();
        }
        else
        {
            animation.OpenGate();
        }
        openGate = !openGate;
    }
}

