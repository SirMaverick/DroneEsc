﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

using FMOD;

class GuardFOV: MonoBehaviour
{
    // max distance a player can get detected
    [SerializeField]
    private float maxDistance = 100;

    [SerializeField]
    private LayerMask layerMask;

    private bool spotted = false;

    //[SerializeField]
    //private float fieldOfView = 60.0f;

    // the actual camera used when to verify if the player is in "range" than we raytrace to check for walls and stuff (and to rotate towards)
    [SerializeField]
    private Camera guardCamera;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject cameraObject;

    // is the camera disabled
    private bool isDisabled = false;

    private int detectionLevel = 0;
    private float time = 0;

    // time it takes when the player gets spotted/detected to be caught
    [SerializeField]
    private float minDetectionTime = 3;

    // fov visualation mesh
    [SerializeField]
    private MeshRenderer cone;


    // follow the player when it has been spotted by the camera (not caught yet)
    [SerializeField]
    private bool followPlayerWhenSpotted = true;

    [SerializeField]
    private float speed = 0.25f;

    // Used to rotate between points/targets
    [SerializeField]
    private Transform[] targets;
    private int nextTarget;

    // Reached the target point
    private bool done = false;
    // at which time the camera should start rotating to the next target point
    private float nextTime;

    // Time the camera should wait after arriving at a target point
    [SerializeField]
    private float waitTime = 1;

    // Colors used for the visualition of the detection state using the cone
    [SerializeField] private Color detectionDefaultColor;
    [SerializeField] private Color detectionLineColor;
    private Color defaultColor;
    private Color lineColor;
    private string defaultColorString = "_Screenlines";
    private string lineColorString = "_Scanlines";


    private void Start()
    {
        defaultColor = cone.material.GetColor(defaultColorString);
        lineColor = cone.material.GetColor(lineColorString);

        colorNotSpotted();
    }

    private void Update()
    {
        if (isDisabled)
        {
            return;
        }
        //http://answers.unity3d.com/questions/720447/if-game-object-is-in-cameras-field-of-view.html
        Vector3 screenPoint = guardCamera.WorldToViewportPoint(player.transform.position);
        bool inFOV = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (inFOV)
        {
            Vector3 direction = player.transform.position - transform.position;
            RaycastHit rayCastHit;
            if (Physics.Raycast(transform.position, direction, out rayCastHit, maxDistance, layerMask.value))
            {
                //Debug.Log(rayCastHit.collider.gameObject.name);
                if (rayCastHit.collider.gameObject == player)
                {
                    //Debug.DrawRay(transform.position, direction, new Color(0, 255, 0));

                    if (!spotted)
                    {
                        // first time spotted so change color
                        colorSpotted();

                        time = Time.time - (time - minDetectionTime) + Time.time;
                        //time = Time.time;
                        if (time <= Time.time || time > Time.time + minDetectionTime)
                        {
                            time = Time.time + minDetectionTime;
                        }
                    }

                    if (time <= Time.time)
                    {
                        // you are caught
                        AntagonistArmPrototype arm = FindObjectOfType<AntagonistArmPrototype>();
                        StartCoroutine(arm.ArmGrabSequence(player));
                        //@TODO fix bad habbit but just for testing
                        StartCoroutine(RestartLevel());

                    }

                    // change color
                    float someScale = time - Time.time;
                    someScale = Mathf.Clamp(someScale / minDetectionTime, 0, 1);

                    Color defaultMix = new Color(detectionDefaultColor.r * (1 - someScale) + defaultColor.r * (someScale),
                                                detectionDefaultColor.g * (1 - someScale) + defaultColor.g * (someScale),
                                                detectionDefaultColor.b * (1 - someScale) + defaultColor.b * (someScale));
                    Color lineMix = new Color(detectionLineColor.r * (1 - someScale) + lineColor.r * (someScale),
                            detectionLineColor.g * (1 - someScale) + lineColor.g * (someScale),
                            detectionLineColor.b * (1 - someScale) + lineColor.b * (someScale));

                    cone.material.SetColor(defaultColorString, defaultMix);
                    cone.material.SetColor(lineColorString, lineMix);



                    spotted = true;
                }
                else
                {
                    notSpotted();
                }

            }
            else
            {
                notSpotted();
            }
        }
        else
        {
            notSpotted();
        }



        /// For the rotation between target points
        // spotted follow target
        if (spotted && followPlayerWhenSpotted)
        {
            // direction based on the 
            Vector3 direction = player.transform.position - cameraObject.transform.position;
            cameraObject.transform.rotation = Quaternion.Slerp(cameraObject.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
        }   
        else
        {
            if (done)
            {
                if (Time.time >= nextTime)
                {
                    nextTarget++;
                    if (nextTarget >= targets.Length)
                    {
                        nextTarget = 0;
                    }
                    done = false;
                }
                else
                {
                    return;
                }

            }
            Vector3 direction = targets[nextTarget].position - cameraObject.transform.position;

            Quaternion newRotation = Quaternion.RotateTowards(cameraObject.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
            float offset = 0.00001f;
            if ((newRotation.w >= cameraObject.transform.rotation.w + offset || newRotation.w <= cameraObject.transform.rotation.w - offset)
                || (newRotation.x >= cameraObject.transform.rotation.x + offset || newRotation.x <= cameraObject.transform.rotation.x - offset)
                || (newRotation.y >= cameraObject.transform.rotation.y + offset || newRotation.y <= cameraObject.transform.rotation.y - offset)
                || (newRotation.z >= cameraObject.transform.rotation.z + offset || newRotation.z <= cameraObject.transform.rotation.z - offset))
            //if (newRotation != cameraObject.transform.rotation)
            {
                cameraObject.transform.rotation = newRotation;
            }
            else
            {
                done = true;
                nextTime = Time.time + waitTime;

            }
        }


    }

    private void colorSpotted()
    {
        GetComponentInParent<MeshRenderer>().material.color = detectionDefaultColor;
        GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", detectionDefaultColor);
    }

    private void colorNotSpotted()
    {
        GetComponentInParent<MeshRenderer>().material.color = defaultColor;
        GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", defaultColor);
    }

    public void DisableGuard()
    {
        colorNotSpotted();
        cone.enabled = false;
        isDisabled = true;
       // Debug.Log("Disabled");
    }

    public void EnableGuard()
    {
        colorNotSpotted();
        cone.enabled = true;
        isDisabled = false;
       // Debug.Log("enabled");
    }

    public void notSpotted()
    {
        if (spotted)
        {
            // first time here after detecting 
            colorNotSpotted();
            time = Time.time - (time - minDetectionTime) + Time.time;
            // fully detected (shouln't happen)
            if (time <= Time.time)
            {
                time = Time.time + minDetectionTime;
            }
            spotted = false;
        }

        if (time >= Time.time)
        {
            // change color
            float someScale = time  - Time.time;
            someScale = Mathf.Clamp(someScale / minDetectionTime, 0, 1);

            Color defaultMix = new Color(detectionDefaultColor.r * ( someScale) + defaultColor.r * (1 - someScale),
                                        detectionDefaultColor.g * ( someScale) + defaultColor.g * (1 - someScale),
                                        detectionDefaultColor.b * (someScale) + defaultColor.b * (1 - someScale));
            Color lineMix = new Color(detectionLineColor.r * (someScale) + lineColor.r * (1 - someScale),
                    detectionLineColor.g * (someScale) + lineColor.g * (1 - someScale),
                    detectionLineColor.b * (someScale) + lineColor.b * (1- someScale));

            cone.material.SetColor(defaultColorString, defaultMix);
            cone.material.SetColor(lineColorString, lineMix);

        }



        
    }

    // Switch the GameObject which is being tracked
    // Let the guards know which GameObject is the player (only keeping track of one object for effeciency)
    public void ChangePlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }


    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}