using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackTimingLight : MonoBehaviour {

    [SerializeField] private GameObject[] lights;
    [SerializeField] private GameObject button;
    [SerializeField] private float time;
    [SerializeField] private Material offMaterial, onMaterial;
    [SerializeField] private int startLight;
    private int currentLight;
    private Ray ray;
    private RaycastHit hit;
    private bool buttonPressed;

	// Use this for initialization
	void Start () {
        lights[startLight].GetComponent<Renderer>().material = onMaterial;
        currentLight = startLight;
        StartCoroutine(TestHelp());
        StartCoroutine(LightSwitch());
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.collider.gameObject == button) {
                    buttonPressed = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && buttonPressed) {
            buttonPressed = false;
            StartCoroutine(LightSwitch());
        }
    }

    IEnumerator LightSwitch() {
        
        nextLight();
        yield return new WaitForSeconds(time);
        if (buttonPressed == false ) StartCoroutine(LightSwitch());
    }

    void nextLight() {
        lights[currentLight].GetComponent<Renderer>().material = offMaterial;

        currentLight++;
        if(currentLight == lights.Length) {
            currentLight = 0;
        }
        lights[currentLight].GetComponent<Renderer>().material = onMaterial;
    }

    IEnumerator TestHelp() {
        yield return new WaitForSeconds(1);
    }


}
