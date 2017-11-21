using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DronePulse : MonoBehaviour {

    private float pulseFloat = 1.0f;
    private float step;
    public float maxDistance;

    public bool startPulse;

    GameObject cameraCenter;


    // Use this for initialization
    void Start() {
        step = 1.0f / 3.0f * Time.deltaTime;
        cameraCenter = GameObject.Find("CameraCenter");
    }

    private void Update() {
        if(startPulse) {
            if (pulseFloat <= 0.5f || pulseFloat > 1.0f) {
                step = -step;
            }
            pulseFloat -= step;
            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Fresnell", pulseFloat);
            GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Intensity", 1 - pulseFloat);
        }

    }

    public void StartPulse() {
        print("in it");
        StartCoroutine(WaitForPulse());
    }


    public IEnumerator WaitForPulse() {
        print("hit");
        float time = Vector3.Distance(transform.position, cameraCenter.transform.position) / maxDistance * 3.0f;
        yield return new WaitForSeconds(time);
        GetComponent<SkinnedMeshRenderer>().material.SetFloat("_ON", 1);
        startPulse = true;
        
    }
    

}
