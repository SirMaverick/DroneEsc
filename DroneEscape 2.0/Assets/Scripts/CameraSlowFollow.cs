using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlowFollow : MonoBehaviour
{
    [SerializeField] private GameObject objectToLookAt;
    [SerializeField] private float rotationSpeed;


    public void Update()
        {
        // corePickUp = cameraObject.GetComponent<CorePlayerController>().GetCore();
        // Vector3 pointToLookAt = new Vector3(corePickUp.transform.position.x, transform.position.y, corePickUp.transform.position.z) - transform.position;

        Vector3 pointToLookAt = objectToLookAt.transform.position - transform.position;

            Quaternion previousRotation = transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(pointToLookAt), Time.deltaTime * rotationSpeed);
            if (previousRotation == transform.rotation)
            {
                // reached destination

            }

        }
}

