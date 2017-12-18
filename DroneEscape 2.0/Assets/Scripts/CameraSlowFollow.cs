using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlowFollow : MonoBehaviour
{
    [SerializeField] private GameObject objectToLookAt;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private bool follow = false;
    [SerializeField] private float followSpeed = 2.0f;

    private Vector3 offsetPosition;

    public void Start()
    {
        offsetPosition = transform.position - objectToLookAt.transform.position;
    }

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

        if (follow)
        {
            transform.position = Vector3.Lerp(transform.position, objectToLookAt.transform.position + offsetPosition, followSpeed * Time.deltaTime);
        }

    }
}

