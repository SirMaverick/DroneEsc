using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GatePlayerController : AbstractPlayerController
{
    private bool openGate = false;
   [SerializeField] private GateAnimation animation;


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

