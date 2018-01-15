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
        StartCoroutine(WaitForSwitchingCamera(0.5f));
    }

    private IEnumerator WaitForSwitchingCamera(float waitTime) {
        GenericFunctions.Instance.SetFadeInCamera(camera.name);
        yield return new WaitForSeconds(waitTime);
        PlayerControllerSupervisor.GetInstance().SwitchPlayerControllerPrevious();

    }
}

