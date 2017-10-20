using UnityEngine;
class CameraMovementController : MovementController
{
    /*private Vector2 mouseLook;
    private Vector2 smoothV;
    [SerializeField] private float minClamp;
    [SerializeField] private float maxClamp;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    [SerializeField] private float selectionRange = 1.5f;*/

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
        gameObject.transform.Rotate(new Vector3(1, 0, 0), -direction, Space.Self);
    }

    public override void Horizontal(float direction)
    {
        gameObject.transform.Rotate(new Vector3(0, 1, 0), direction, Space.Self);
    }

    public override void Use(bool key)
    {
        if (key)
        {
  
                playerControllerSupervisor.SwitchPlayerControllerPrevious();

        }
    }
}

