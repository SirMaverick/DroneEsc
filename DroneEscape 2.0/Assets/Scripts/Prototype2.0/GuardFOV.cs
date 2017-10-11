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
                    if(time + minDetectionTime < Time.time)
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

    /*private void OnTriggerStay(Collider other)
    {
       // Vector3 closestPointCollider =  other.ClosestPoint(transform.position);

        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        RaycastHit rayCastHit;
        Debug.DrawRay(transform.position, direction, new Color(0, 255, 0));
        if (angle < (fieldOfView * 0.5f))
        {
            if (Physics.Raycast(transform.position, direction, out rayCastHit, maxDistance, layerMask.value))
            {
                if (rayCastHit.collider == other)
                {
                    Debug.DrawRay(transform.position, direction, new Color(0, 255, 0));
                    Debug.Log("spotted");
                    if (!spotted)
                    {
                        GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                    }
                    spotted = true;
                }
                else
                {
                    // not spotted
                    if (spotted)
                    {
                        GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                    }
                    spotted = false;
                }

            }
        }
        else
        {
            // not spotted
            if (spotted)
            {
                GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
            }
            spotted = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (spotted)
        {
            GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
        }
        spotted = false;
    }*/

}