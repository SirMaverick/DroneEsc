using UnityEngine;
public abstract class MovementController : MonoBehaviour
{
    [SerializeField]
    protected GameObject gameObject;
    /*public void Forward(float speed)
    {

    }

    public void Backward(float speed)
    {

    }

    public void Left(float speed)
    {

    }

    public void Right(float speed)
    {

    }*/

    public void Horizontal(float direction)
    {

    }

    public void Vertical(float direction)
    {

    }

    // maybe other class
    /*public void HorizontalLook(float direction)
    {

    }

    public void VerticalLook(float direction)
    {

    }*/

    public virtual void Look(Vector2 md)
    {

    }

    private void Update()
    {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Look(md);
    }
}

