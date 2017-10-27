using System;
using System.Collections.Generic;
using UnityEngine;
public class MagnetMovementController : MovementController
{
   

    public override void Horizontal(float speed)
    {

    }

    public override void Vertical(float direction)
    {
       // Do nothing
    }

    public override void Look(Vector2 md)
    {
        // Do nothing
    }

    public override void Use(bool key)
    {
        if (key)
        {
            playerControllerSupervisor.SwitchPlayerControllerPrevious();
        }
    }


}

