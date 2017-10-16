using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PushBlock : MonoBehaviour {
    bool xAxis, yAxis, zAxis;
    float movementSpeed = 1.0f;
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
                movement.x = Mathf.Sin(Mathf.PI - transform.localRotation.y * Mathf.Deg2Rad);
                movement.z = Mathf.Cos(Mathf.PI - transform.localRotation.y * Mathf.Deg2Rad);
            }
            else if (angle >= 45  && angle < 135)
            {
                // push down
                movement.x = -Mathf.Sin(Mathf.PI / 2 - transform.localEulerAngles.y * Mathf.Deg2Rad);
                movement.z = -Mathf.Cos(Mathf.PI / 2 - transform.localEulerAngles.y * Mathf.Deg2Rad);
            }
            else if (angle >= 135 && angle < 225)
            {
                // push to right
                movement.x = Mathf.Sin(0 - transform.localRotation.y * Mathf.Deg2Rad);
                movement.z = Mathf.Cos(0 - transform.localRotation.y * Mathf.Deg2Rad);
            }
            else if (angle >= 225 && angle < 315)
            {
                // push up
                movement.x = Mathf.Sin(Mathf.PI / 2 - transform.localRotation.y * Mathf.Deg2Rad);
                movement.z = Mathf.Cos(Mathf.PI / 2 - transform.localRotation.y * Mathf.Deg2Rad);
            }
            
            transform.position += movement * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        movement = new Vector3(0, 0, 0);
    }


}
