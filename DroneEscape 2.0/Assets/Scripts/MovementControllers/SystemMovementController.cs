using UnityEngine;
class SystemMovementController : MovementController
{
    [SerializeField]
    private SystemPlayerController playerController;



    public override void Horizontal(float direction)
    {

    }

    public override void Vertical(float direction)
    {

    }

    public override void Look(Vector2 md)
    {

    }

    public override void RightClick(bool key) {
        playerController.SwitchToNextCamera();
    }

    public override void LeftClick(bool key) {
        playerController.SwitchToPreviousCamera();
    }

    public override void Use(bool key)
    {
        if (key)
        {
            // switch to the playerController which used to be used before this one
            playerController.SwitchToPreviousPlayerController();
        }
    }


}

