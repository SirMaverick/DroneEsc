using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreCamera : MonoBehaviour {

    public GameObject core;
    [SerializeField] private GameObject drone;
    private GameObject camera;


	// Use this for initialization
	void Start () {
        camera = transform.Find("CoreCamera").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            drone.transform.Find("DroneCamera").GetComponent<Camera>().enabled = true;
            camera.GetComponent<Camera>().enabled = false;
        }
	}

    public void SetCamera () {
        camera.GetComponent<Camera>().enabled = true;
        transform.position = core.transform.position;
    }
}
