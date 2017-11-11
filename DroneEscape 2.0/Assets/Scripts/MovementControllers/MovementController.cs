using UnityEngine;
public abstract class MovementController : MonoBehaviour
{
    /*[SerializeField]
    protected GameObject gameObject;*/
    protected PlayerControllerSupervisor playerControllerSupervisor;

    protected virtual void Start()
    {
        playerControllerSupervisor = PlayerControllerSupervisor.GetInstance();
    }
    
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

    public abstract void Horizontal(float direction);


    public abstract void Vertical(float direction);

    // maybe other class
    /*public void HorizontalLook(float direction)
    {

    }

    public void VerticalLook(float direction)
    {

    }*/

    public abstract void Look(Vector2 md);

    public abstract void Use(bool key);

    public virtual void RightClick(bool key) { }
    public virtual void RightPress(bool key) { }

    public virtual void LeftClick(bool key) { }
    public virtual void LeftPress(bool key) { }

    private void Update()
    {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Look(md);
        Horizontal(Input.GetAxis("Horizontal"));
        Vertical(Input.GetAxis("Vertical"));

        Use(Input.GetKeyDown(KeyCode.E));

        RightClick(Input.GetMouseButtonUp(1));
        RightPress(Input.GetMouseButtonDown(1));
        LeftPress(Input.GetMouseButtonDown(0));
        LeftClick(Input.GetMouseButtonUp(0));

    }
}

