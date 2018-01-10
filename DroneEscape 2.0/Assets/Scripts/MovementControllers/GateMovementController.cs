using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GateMovementController : MovementController
{
    [SerializeField]
    private GatePlayerController gatePlayerController;

    public override void Horizontal(float direction)
    {

    }

    public override void Vertical(float direction)
    {

    }

    public override void Look(Vector2 md)
    {

    }

    public override void RightClick(bool key)
    {
        if (key)
        {
            playerControllerSupervisor.SwitchPlayerControllerPrevious();
        }
    }

    public override void LeftClick(bool key)
    {
        if (key)
        {
            gatePlayerController.ToggleGate();
        }
    }

}
