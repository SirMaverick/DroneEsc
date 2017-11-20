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
            if (gameObject.transform.position.y >= roofPosition.y)
            {
                reachedRoof = true;
            }
        }
    }


    public void MoveTo(Vector3 targetPos)
    {
        if (reachedRoof)
        {
            targetPosition = targetPos;
            targetPosition.y += 6.5f;
            gameObject.transform.position = new Vector3(targetPosition.x, roofPosition.y, targetPosition.z);
            reachedTarget = false;
        }
    }
}