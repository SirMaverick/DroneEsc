﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorEnableButton : Button {

    [SerializeField] private ClickInPlace[] puzzelParts;
    [SerializeField] private GameObject convBelt;
   // public GameObject convBPiece1, convBPiece2, convBPiece3, convBPiece4;

	void Start ()
    {
        //zet de convBelt gelijk uit
        convBelt.GetComponent<ConveyerBelt>().enabled = false;
    }

    public override void Toggle()
    {
        foreach(ClickInPlace part in puzzelParts)
        {
            if (!part.HasBeenSet())
            {
                Debug.Log("NOt done");
                return;
            }
        }
        Debug.Log("Enable conveyor");
        convBelt.GetComponent<ConveyerBelt>().enabled = true;
        // do something
        // enable conveyor belt
    }
}

