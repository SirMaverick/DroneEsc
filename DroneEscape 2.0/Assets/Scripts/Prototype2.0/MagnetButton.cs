using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetButton : MonoBehaviour {

    [SerializeField]
    private GameObject magnet;
    public Camera surveillanceCamera;
    public float speed = 2;
    public bool coreInside;
    public GameObject drone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(coreInside) {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //magnet.transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime * speed;
            magnet.transform.Translate(direction.x * Time.deltaTime * speed, 0, direction.y * Time.deltaTime * speed);
            if (Input.GetMouseButton(0)) {
                magnet.GetComponent<MagnetMove>().turnedOn = true;
            } else if (Input.GetMouseButtonUp(0)) {
                magnet.GetComponent<MagnetMove>().turnedOff = true;
            }

            if (Input.GetMouseButtonDown(1)) {
                surveillanceCamera.enabled = false;
                coreInside = false;
                drone.GetComponent<PlayerMovement>().enabled = true;
                drone.GetComponent<MeshRenderer>().enabled = false;
                drone.GetComponentInChildren<PlayerMouseLook>().enabled = true;
                drone.GetComponentInChildren<Camera>().enabled = true;
            }
        }
	}


    public void TurnMagnetOn() {
        magnet.GetComponent<MagnetMove>().turnedOn = true;
    }

    public void TurnMagnetOff() {
        magnet.GetComponent<MagnetMove>().turnedOff = true;
    }
    

}
