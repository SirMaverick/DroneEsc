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

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!reachedTarget)
        {
            gameObject.transform.Translate(Vector3.down * Time.deltaTime * speed);
            if(gameObject.transform.position.y <= targetPosition.y)
            {
                reachedTarget = true;
                reachedRoof = false;
                SystemEnergyController sec = FindObjectOfType<SystemEnergyController>();
                sec.AddEnergyFromCore();
            } 
        }else if (!reachedRoof)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);

            if (droneLift)
            {
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
            if (gameObject.transform.position.y >= roofPosition.y)
            {
                reachedRoof = true;
            }
        }
    }


    public void MoveTo(GameObject target)
    {
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