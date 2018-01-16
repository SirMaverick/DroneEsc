using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePulse : MonoBehaviour {

    private float pulseFloat = 1.0f;
    private float step;
    private float t;
    private const float pi = 3.14159274F;

    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    public bool startPulse;
    public GameObject machine;
    bool pulseActive = true;
    bool pulseDeactive = false;



    // Use this for initialization
    void Start() {
        step = 1.0f / 3.0f * Time.deltaTime;
    }

	// Update is called once per frame
	void Update () {
        if (startPulse) {
            if (!stopwatch.IsRunning) {
                stopwatch.Start();
            } else {
                t = (0.001f * stopwatch.ElapsedMilliseconds);
            }

            if (t % 3 == 0 && !pulseActive) {
                pulseActive = true;
                pulseDeactive = false;

            } else if (t % 3 > 1.0f && !pulseDeactive) {
                pulseActive = false;
                pulseDeactive = true;
            }

            if (pulseActive && pulseDeactive == false) {
                pulseFloat = 0.25f + 0.25f * Mathf.Sin(pi * (t - 0.5f));

            } else if (pulseDeactive && pulseActive == false) {
                pulseFloat = 0.25f + 0.25f * Mathf.Sin(0.5f * pi * t);

            }
            machine.GetComponent<MeshRenderer>().material.SetFloat("_Fresnell", 0.5f + pulseFloat);
            machine.GetComponent<MeshRenderer>().material.SetFloat("_Intensity", 0.5f - pulseFloat);
        }
    }

    public void StartPulse() {
        machine.GetComponent<MeshRenderer>().material.SetFloat("_Heartbeat", 1);
        startPulse = true;

    }

    public void StopPulse() {
        startPulse = false;
        machine.GetComponent<MeshRenderer>().material.SetFloat("_Fressnel", 1);
        machine.GetComponent<MeshRenderer>().material.SetFloat("_Intensity", 0);
        machine.GetComponent<MeshRenderer>().material.SetFloat("_Heartbeat", 0);
    }
}
