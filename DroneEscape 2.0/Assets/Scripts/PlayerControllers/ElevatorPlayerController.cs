using UnityEngine;
using System.Collections;
class ElevatorPlayerController : AbstractPlayerController
    {
    protected override void Start()
    {
        uiController = FindObjectOfType<ElevatorUIController>();
        base.Start();
    }

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

    public override void DisableController() {
        base.DisableController();
        cameras[0].GetComponent<MachinePulse>().StopPulse();
    }
}

