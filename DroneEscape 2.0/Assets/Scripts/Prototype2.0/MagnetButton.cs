using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetButton : MonoBehaviour {

    [SerializeField]
    private GameObject magnet;
    public float speed = 2;
    public bool coreInside;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(coreInside) {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            magnet.transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime * speed;
        }
	}


    public void TurnMagnetOn() {
        magnet.GetComponent<MagnetMove>().turnedOn = true;
    }

    public void TurnMagnetOff() {
        magnet.GetComponent<MagnetMove>().turnedOff = true;
    }
    

}
