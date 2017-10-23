using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

class GuardFOV: MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 100;

    [SerializeField]
    private LayerMask layerMask;

    private bool spotted = false;

    //[SerializeField]
    //private float fieldOfView = 60.0f;

    [SerializeField]
    private Camera guardCamera;

    [SerializeField]
    private GameObject player;

    private bool disabled = false;

    private int detectionLevel = 0;
    private float time = 0;
    [SerializeField]
    private float minDetectionTime = 3;

    [SerializeField]
    private MeshRenderer cone;

    [SerializeField]
    private Text caughtText;

    [SerializeField]
    private bool followTargetWhenSpotted = true;

    [SerializeField]
    private float speed = 0.25f;

    [SerializeField]
    private Transform[] targets;
    private int nextTarget;

    private bool done = false;
    private float nextTime;
    [SerializeField]
    private float waitTime = 1;

    private void Start()
    {

    }

    private void Update()
    {
        if (disabled)
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
                Debug.Log(rayCastHit.collider.gameObject.name);
                if (rayCastHit.collider.gameObject == player)
                {
                    Debug.DrawRay(transform.position, direction, new Color(0, 255, 0));

                    if (!spotted)
                    {
                        GetComponentInParent<MeshRenderer>().material.color = new Color(255, 0, 0);
                        GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(255, 0, 0));
                        time = Time.time;
                    }
                    /*ParticleSystem sm = GetComponent<ParticleSystem>();
                    //ParticleSystem.MinMaxGradient colorGradient = sm.main.startColor;
                    ParticleSystem.MainModule mainPS = sm.main;
                    //mainPS.startColor = new ParticleSystem.MinMaxGradient(new Color(detectionLevel, 0, 0));
                    mainPS.startColor = new Color(detectionLevel, 0, 0);*/
                    if (time + minDetectionTime < Time.time)
                    {
                        // you are caught
                        Debug.Log("spotted");
                        // ui you got caught
                        caughtText.enabled = true;
                        //@TODO fix bad habbit but just for testing
                        StartCoroutine(RestartLevel());

                    }
                    detectionLevel++;

                    spotted = true;
                }
                else
                {
                    // not spotted
                    if (spotted)
                    {
                        GetComponentInParent<MeshRenderer>().material.color = new Color(0, 0, 0);
                        GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
                    }
                    detectionLevel = 0;
                    time = 0;
                    spotted = false;
                }

            }
            else
            {
                if (spotted)
                {
                    GetComponentInParent<MeshRenderer>().material.color = new Color(0, 0, 0);
                    GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
                }
                spotted = false;
            }
        }
        else
        {
            if (spotted)
            {
                GetComponentInParent<MeshRenderer>().material.color = new Color(0, 0, 0);
                GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
            }
            spotted = false;
        }


        // spotted follow target
        if (spotted && followTargetWhenSpotted)
        {
            // direction based on the 
            Vector3 direction = player.transform.position - transform.root.position;
            transform.root.rotation = Quaternion.Slerp(transform.root.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
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
                        Debug.Log("0");
                        nextTarget = 0;
                    }
                    done = false;
                    Debug.Log("Time");
                }
                else
                {
                    Debug.Log("Return");
                    return;
                }

            }
            Vector3 direction = targets[nextTarget].position - transform.root.position;

            Quaternion newRotation = Quaternion.RotateTowards(transform.root.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
            float offset = 0.00001f;
            if ((newRotation.w >= transform.root.rotation.w + offset || newRotation.w <= transform.root.rotation.w - offset)
                || (newRotation.x >= transform.root.rotation.x + offset || newRotation.x <= transform.root.rotation.x - offset)
                || (newRotation.y >= transform.root.rotation.y + offset || newRotation.y <= transform.root.rotation.y - offset)
                || (newRotation.z >= transform.root.rotation.z + offset || newRotation.z <= transform.root.rotation.z - offset))
            //if (newRotation != transform.root.rotation)
            {
                Debug.Log("rotate");
                transform.root.rotation = newRotation;
            }
            else
            {
                Debug.Log("Done");
                done = true;
                nextTime = Time.time + waitTime;

            }
        }


    }

    public void DisableGuard()
    {
        GetComponentInParent<MeshRenderer>().material.color = new Color(255, 255, 255);
        GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(255, 255, 255));
        cone.enabled = false;
        disabled = true;
        Debug.Log("Disabled");
    }

    public void EnableGuard()
    {
        GetComponentInParent<MeshRenderer>().material.color = new Color(0, 0, 0);
        GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
        cone.enabled = true;
        disabled = false;
        Debug.Log("enabled");
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