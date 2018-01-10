using UnityEngine;
class SystemMovementController : MovementController
{
    // Warning do not use playerController
    [SerializeField]
    private SystemPlayerController systemPlayerController;



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
        if (key)
        {
            systemPlayerController.SwitchToNextCamera();
        }
    }

    public override void LeftClick(bool key)
    {
        if (key)
        {
            // switch to the playerController which used to be used before this one
            systemPlayerController.SwitchToPreviousPlayerController();
        }
    }


}

