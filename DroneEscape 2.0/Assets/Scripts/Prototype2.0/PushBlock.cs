using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PushBlock : MonoBehaviour {
    bool xAxis, yAxis, zAxis;
    Vector3 movement = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += movement * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Drone") {
            
            Vector3 point= transform.position - other.transform.position;
            // Debug.Log(point);
            float angle = Mathf.Atan2(point.x, point.z) * Mathf.Rad2Deg + 180.0f;
            angle -= transform.localEulerAngles.y;
            
            movement = new Vector3(0, 0, 0);
            
            if (angle >= 0 && angle < 45.0f || angle >= 315 && angle <= 360)
            {
                // push to left
                movement = -transform.forward;
            }
            else if (angle >= 45  && angle < 135)
            {
                // push down
                movement = -transform.right;
            }
            else if (angle >= 135 && angle < 225)
            {
                // push to right
                movement = transform.forward;
            }
            else if (angle >= 225 && angle < 315)
            {
                // push up
                movement = transform.right;
            }
            
            transform.position += movement * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        movement = new Vector3(0, 0, 0);
    }


}
