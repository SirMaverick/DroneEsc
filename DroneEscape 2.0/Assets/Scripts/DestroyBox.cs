using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : ArmEventListener {
    [SerializeField] public string tag;

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Renderer>().enabled = false;
        if (other.tag == "Core" || other.tag == tag)
        {
            GetComponent<Animator>().SetBool("Close", true);
        }
        GameObject core = FindObjectOfType<CoreObject>().gameObject;
        if (other.gameObject.Equals(core))
        {
            // core so you will die.
            StartCoroutine(GenericFunctions.Instance.RestartLevel(1, core.GetComponent<MusicController>()));

        }

    }

    public override void ArmEvent()
    {
        GetComponent<Animator>().SetBool("Close", false);
    }
}
