using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorEnableButton : Button {

    [SerializeField] private ClickInPlace[] puzzelParts;
   // public GameObject convBPiece1, convBPiece2, convBPiece3, convBPiece4;

	void Start ()
    {
        //zet de convBelt gelijk uit
       // convBelt.SetActive(false);
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
        // do something
        // enable conveyor belt
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        //Als de speler in de trigger staat van de button en op E drukt, en alle convbelts op hun plaats staan, dan word de convbelt weer op active gezet
        Debug.Log("entered");
        if (other.tag == "Drone")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Triggered");
                if(convBPiece1.GetComponent<ClickInPlace>().hasBeenSet && convBPiece2.GetComponent<ClickInPlace>().hasBeenSet && convBPiece3.GetComponent<ClickInPlace>().hasBeenSet && convBPiece4.GetComponent<ClickInPlace>().hasBeenSet)
                {
                    convBelt.SetActive(true);
                }
            }
        }
    }*/


}
