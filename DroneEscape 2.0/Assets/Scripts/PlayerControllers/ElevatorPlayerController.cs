using UnityEngine;
class ElevatorPlayerController : AbstractPlayerController
    {
    protected override void Start()
    {
        uiController = FindObjectOfType<ElevatorUIController>();
        base.Start();
    }
}

