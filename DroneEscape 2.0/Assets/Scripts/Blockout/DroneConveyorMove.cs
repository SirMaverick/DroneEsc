using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneConveyorMove : MonoBehaviour {

    public Transform TargetA;
    public Transform TargetB;
    private bool AtTarget;

    // Use this for initialization
    void Start () {
        AtTarget = false;
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(!AtTarget)
        {
            if (Vector3.Distance(TargetA.position, this.transform.position) < 0.5)
            {
                AtTarget = true;
            }

            else
            {
                Vector3 direction = TargetA.position - this.transform.position;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                MoveForward();
            }
        } else
        {
            Vector3 direction = TargetB.position - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            MoveForward();
        }

        if (Vector3.Distance(TargetB.position, this.transform.position) < 0.5)
        {
            Destroy(this.gameObject);
        }

        /* Als ik niet op de positie ben van mijn target, dan loop ik er naar toe.
         * Ben ik al bij target A geweest dan loop ik naar target B
         * Ben ik bij B geweest dan ga ik kapot
         */
    }

    void MoveForward()
    {
        this.transform.Translate(0, 0, 0.05f);
    }
}
