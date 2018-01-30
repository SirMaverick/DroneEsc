using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorEnableButton : MonoBehaviour {

    [SerializeField] private ClickInPlace[] puzzelParts;
    [SerializeField] private GameObject convBelt;
   // public GameObject convBPiece1, convBPiece2, convBPiece3, convBPiece4;

	void Start ()
    {
        //zet de convBelt gelijk uit
        convBelt.GetComponent<ConveyerBelt>().enabled = false;
    }

    void Update() { 
        foreach(ClickInPlace part in puzzelParts)
        {
            if (!part.HasBeenSet())
            {
                return;
            }
        }
        convBelt.GetComponent<ConveyerBelt>().enabled = true;
        // do something
        // enable conveyor belt
    }
}

