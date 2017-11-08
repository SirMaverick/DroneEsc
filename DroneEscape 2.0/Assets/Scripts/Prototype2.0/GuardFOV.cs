using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

    [SerializeField]
    private Text caughtText;

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
    [SerializeField] private Color detectionColor;
    [SerializeField] private Color defaultColor;

    private void Start()
    {

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
                        //Debug.Log("spotted");
                        // ui you got caught
                        caughtText.enabled = true;
                        //@TODO fix bad habbit but just for testing
                        StartCoroutine(RestartLevel());

                    }

                    //detectionLevel++;
                    


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

        // change color
        float someScale = time + minDetectionTime - Time.time;
        someScale = Mathf.Clamp(someScale / minDetectionTime, 0, 1);

        Color c = new Color(detectionColor.r * someScale + defaultColor.r * (1 - someScale),
                             detectionColor.g * someScale + defaultColor.g * (1 - someScale),
                             detectionColor.b * someScale + defaultColor.b * (1 - someScale));

        cone.material.SetColor("_Screenlines", c);


        // spotted follow target
        if (spotted && followPlayerWhenSpotted)
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
                        nextTarget = 0;
                    }
                    done = false;
                }
                else
                {
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
                transform.root.rotation = newRotation;
            }
            else
            {
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
        isDisabled = true;
        Debug.Log("Disabled");
    }

    public void EnableGuard()
    {
        GetComponentInParent<MeshRenderer>().material.color = new Color(0, 0, 0);
        GetComponentInParent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
        cone.enabled = true;
        isDisabled = false;
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