using UnityEngine;
class CameraMovementController : MovementController
{
    [SerializeField] private float clampX;
    [SerializeField] private float clampY;
    [SerializeField] private float speed;

    private Vector3 initAngles;

    private Vector3 currentRotation; //intended

    protected override void Start()
    {
        base.Start();
        initAngles = gameObject.transform.localRotation.eulerAngles;
        currentRotation = initAngles;
    }

    // no movement only looking

    // maybe other class
    public void HorizontalLook(float direction)
    {
       // gameObject.
    }

    public void VerticalLook(float direction)
    {

    }

    public override void Look(Vector2 md)
    {
        /*md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minClamp, maxClamp);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        gameObject.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, gameObject.transform.up);
        */
    }

    public override void Vertical(float direction)
    {
        //Vector3 rotation = gameObject.transform.localRotation.eulerAngles;
      
        float xValue = Mathf.Clamp(currentRotation.x + (-direction * speed * Time.deltaTime), initAngles.x - clampX, initAngles.x + clampX);
        currentRotation.x = xValue;

        gameObject.transform.eulerAngles = currentRotation;
        //gameObject.transform.Rotate(new Vector3(1, 0, 0), -direction, Space.Self);
    }

    public override void Horizontal(float direction)
    {
        float yValue = Mathf.Clamp(currentRotation.y + (direction * speed * Time.deltaTime), initAngles.y - clampY, initAngles.y + clampY);
        currentRotation.y= yValue;

        gameObject.transform.eulerAngles = currentRotation;
        //gameObject.transform.Rotate(new Vector3(0, 1, 0), direction, Space.Self);
    }

    public override void Use(bool key)
    {
        if (key)
        {
  
                playerControllerSupervisor.SwitchPlayerControllerPrevious();

        }
    }
    public override void RightClick(bool key)
    {
        // do nothing
    }
}

