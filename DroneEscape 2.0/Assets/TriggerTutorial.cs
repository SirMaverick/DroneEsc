using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour {

    public string boolName;
    public bool insertMachine;
    public bool throwCore;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Drone" && !TriggerButtonAnimation.Instance.insertMachine && insertMachine)
            TriggerButtonAnimation.Instance.TurnAnimationOn(boolName);
        else if (other.tag == "Drone" && !TriggerButtonAnimation.Instance.throwCore && throwCore)
            TriggerButtonAnimation.Instance.TurnAnimationOn(boolName);
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Drone" && !TriggerButtonAnimation.Instance.insertMachine && insertMachine)
            TriggerButtonAnimation.Instance.TurnAnimationOff(boolName);
        else if (other.tag == "Drone" && !TriggerButtonAnimation.Instance.throwCore && throwCore)
            TriggerButtonAnimation.Instance.TurnAnimationOff(boolName);
    }
}
