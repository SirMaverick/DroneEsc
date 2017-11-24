using System;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPlayerController : AbstractPlayerController
{
    protected override void Start()
    {
        uiController = FindObjectOfType<MagnetUIController>();
        base.Start();
    }
}

