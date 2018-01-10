using UnityEngine;
public abstract class MovementController : MonoBehaviour
{
    /*[SerializeField]
    protected GameObject gameObject;*/
    protected PlayerControllerSupervisor playerControllerSupervisor;
    [SerializeField] protected AbstractPlayerController playerController;

    public virtual void EnableController() { }
    public virtual void DisableController() { }

    protected virtual void Start()
    {
        playerControllerSupervisor = PlayerControllerSupervisor.GetInstance();
    }

    public abstract void Horizontal(float direction);

    public abstract void Vertical(float direction);


    public abstract void Look(Vector2 md);

    public virtual void RightClick(bool key) {
        if (key)
        {
            playerController.SwitchToNextCamera();
        }
    }
    public virtual void RightPress(bool key) { }
    public virtual void RightHold(bool key) { }

    public virtual void LeftClick(bool key) { }
    public virtual void LeftPress(bool key) { }
    public virtual void LeftHold(bool key) { }

    protected virtual void Update()
    {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Look(md);
        Horizontal(Input.GetAxis("Horizontal"));
        Vertical(Input.GetAxis("Vertical"));

        RightClick(Input.GetButtonUp("Fire2"));
        RightPress(Input.GetButtonDown("Fire2"));
        RightHold(Input.GetButton("Fire2"));
        LeftClick(Input.GetButtonUp("Fire1"));
        LeftPress(Input.GetButtonDown("Fire1"));
        LeftHold(Input.GetButton("Fire1"));

    }
}

