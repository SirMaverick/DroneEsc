using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : ArmEventListener {


    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Renderer>().enabled = false;
        GetComponent<Animator>().SetBool("Close", true);
    }

    public override void ArmEvent()
    {
        GetComponent<Animator>().SetBool("Close", false);
    }
}
