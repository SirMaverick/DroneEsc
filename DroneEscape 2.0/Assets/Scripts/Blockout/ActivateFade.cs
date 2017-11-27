using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FadeToWhite.Instance.CallFading();
    }
}
