﻿using UnityEngine;
public abstract class MovementController : MonoBehaviour
{
    [SerializeField]
    protected GameObject gameObject;
    protected PlayerControllerSupervisor playerControllerSupervisor;

    protected void Start()
    {
        playerControllerSupervisor = FindObjectOfType<PlayerControllerSupervisor>();
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

    private void Update()
    {
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Look(md);
        Horizontal(Input.GetAxis("Horizontal"));
        Vertical(Input.GetAxis("Vertical"));

        Use(Input.GetKeyDown(KeyCode.E));
    }
}
