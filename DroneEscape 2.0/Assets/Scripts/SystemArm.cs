using UnityEngine;
class SystemArm : MonoBehaviour
{
    private bool reachedTarget = true;
    private bool reachedRoof = true;
    private bool droneLift = true;

    private Vector3 targetPosition;
    [SerializeField]
    private Vector3 roofPosition;

    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float liftHeight = 3;

    private GameObject targetDrone;

    private SystemPlayerController playerController;



    private void Start()
    {
        playerController = FindObjectOfType<SystemPlayerController>();    
    }

    private void Update()
    {
        if (!reachedTarget)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
            // Reached the drone
            if(gameObject.transform.position.y <= targetPosition.y)
            {
                reachedTarget = true;
                reachedRoof = false;

                // remove energy
                //SystemEnergyController sec = FindObjectOfType<SystemEnergyController>();
                //sec.AddEnergyFromCore();
                playerController.GiveEnergy();
            } 
        }else if (!reachedRoof)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);

            // Lifting a drone
            if (droneLift)
            {
                // Reached max lifting height of the drone
                if (targetDrone.transform.position.y < liftHeight)
                {
                    targetDrone.transform.Translate(Vector3.up * Time.deltaTime * speed);
                    
                }
                else
                {
                    targetDrone.GetComponent<Rigidbody>().isKinematic = false;
                    droneLift = false;
                }

            }
            // Reached roof
            if (gameObject.transform.position.y >= roofPosition.y)
            {
                reachedRoof = true;
            }
        }
    }


    public void MoveTo(GameObject target)
    {
        // only do something if we are not tyring to lift or are lifting a drone.
        if (reachedRoof && reachedTarget)
        {
            targetDrone = target;
            targetDrone.GetComponent<Rigidbody>().isKinematic = true;
            targetPosition = target.transform.position;
            // length of the arm, yea ...
            targetPosition.y += 6.5f;
            gameObject.transform.position = new Vector3(targetPosition.x, roofPosition.y, targetPosition.z);
            reachedTarget = false;
            droneLift = true;
        }
    }
}