using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour {


    [SerializeField] private GameObject elevator;
    [SerializeField] private Transform lowestPos;
    [SerializeField] private Transform highestPos;
    private bool up, down;
    private float step;
    [SerializeField] private float speed;

    // Use this for initialization
    void Start () {
        step = speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () { 
		if (up) {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, highestPos.position, step);
        } else if (down) {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, lowestPos.position, step);
        }
	}

    public void MoveElevatorUp() {
        up = true;
    }

    public void MoveElevatorDown() {
        down = true;
    }

    public void StopElevatorMove() {
        up = false;
        down = false;
    }
}
