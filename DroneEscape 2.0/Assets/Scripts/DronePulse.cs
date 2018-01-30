using UnityEngine;
using System.Collections;
using FMODUnity;
using System.Collections.Generic;

public class DronePulse : MonoBehaviour {

    private float pulseFloat = 1.0f;
    private float step;
    private float t;
    public float maxDistance;

    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    public bool startPulse;
    bool pulseActive = true;
    bool pulseDeactive = false;

    GameObject cameraCenter;

    FMOD.Studio.EventInstance droneSound;
    FMOD.Studio.PLAYBACK_STATE playback;


    // Use this for initialization
    void Start() {
        step = 1.0f / 3.0f * Time.deltaTime;
        cameraCenter = GameObject.Find("CameraCenter");
    }

    private void Update() {
        //  if (pulseFloat <= 0.5f || pulseFloat > 1.0f) {
        //      step = -step;
        //  }
        //  pulseFloat -= step;
        //  GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Fresnell", pulseFloat);
        //  GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Intensity", 1 - pulseFloat);

        if (startPulse) {
            if (!stopwatch.IsRunning) {
                stopwatch.Start();
            } else {
                t = (0.001f * stopwatch.ElapsedMilliseconds);
            }

            if ( t % 3 == 0 && !pulseActive) {
                pulseActive = true;
                pulseDeactive = false;
                
            } else if (t % 3 > 1.0f && !pulseDeactive) {
                pulseActive = false;
                pulseDeactive = true;
            }

            if (pulseActive && pulseDeactive == false) {
                pulseFloat = 0.25f + 0.25f * Mathf.Sin(Mathf.PI * (t - 0.5f));

            } else if (pulseDeactive && pulseActive == false) {
                pulseFloat = 0.25f + 0.25f * Mathf.Sin(0.5f * Mathf.PI * t);

            }
            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Fresnell", 0.5f + pulseFloat);
            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Intensity", 0.5f - pulseFloat);
        }

       
    }

    public void StartPulse() {
        StartCoroutine(WaitForPulse());
    }

    public void StartPulseHighlighted() {
        GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Heartbeat", 1);
        startPulse = true;
    }


    public IEnumerator WaitForPulse() {
        float time = Vector3.Distance(transform.position, cameraCenter.transform.position) / maxDistance * 3.0f;
        yield return new WaitForSeconds(time);
        StartCoroutine(PlayOneShotFMOD("event:/SFX/Core/CorePulseHitDrone", gameObject));
        GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Heartbeat", 1);
        startPulse = true;
        
    }

    public void StopPulse() {
        startPulse = false;
        GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Fressnel", 1);
        GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Intensity", 0);
        GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Heartbeat", 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "CameraCenter") {
            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Heartbeat", 1);
            startPulse = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.name == "CameraCenter") {
            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Heartbeat", 1);
            startPulse = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "CameraCenter") {
            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Heartbeat", 0);
            startPulse = false;
        }
    }

    IEnumerator PlayOneShotFMOD(string eventName, GameObject attachTo) {
        FMOD.Studio.EventInstance oneShot = RuntimeManager.CreateInstance(eventName);
        FMOD.Studio.EventDescription description;
        oneShot.getDescription(out description);
        int tempWaitTime;
        description.getLength(out tempWaitTime);
        RuntimeManager.AttachInstanceToGameObject(oneShot, attachTo.transform, attachTo.GetComponent<Rigidbody>());
        oneShot.start();
        yield return new WaitForSeconds(tempWaitTime / 1000.0f);
        oneShot.release();
        oneShot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
