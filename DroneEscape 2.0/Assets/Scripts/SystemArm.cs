using UnityEngine;
class SystemArm : MonoBehaviour
{
    private bool reachedTarget = true;
    private bool reachedRoof = true;
    
    private Vector3 targetPosition;
    [SerializeField]
    private Vector3 roofPosition;

    [SerializeField]
    private float speed = 5;

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
            } 
        }else if (!reachedRoof)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
            targetDrone.transform.Translate(Vector3.up * Time.deltaTime * speed);
            if (gameObject.transform.position.y >= roofPosition.y)
            {
                targetDrone.GetComponent<Rigidbody>().isKinematic = false;
                //Destroy(targetDrone);
                reachedRoof = true;
            }
        }
    }


    public void MoveTo(GameObject target)
    {
        if (reachedRoof)
        {
            targetDrone = target;
            targetDrone.GetComponent<Rigidbody>().isKinematic = true;
            targetPosition = target.transform.position;
            // length of the arm, yea ...
            targetPosition.y += 6.5f;
            gameObject.transform.position = new Vector3(targetPosition.x, roofPosition.y, targetPosition.z);
            reachedTarget = false;
        }
    }
}