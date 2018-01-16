using System.Collections;
using UnityEngine;
public class MagnetPlayerController : AbstractPlayerController
{
    protected override void Start()
    {
        uiController = FindObjectOfType<MagnetUIController>();
        base.Start();
    }

    public void PostFXExit() {
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
    }
}

