using System;
using System.Collections.Generic;
using UnityEngine;
public class ElevatorMovementController : MovementController
{
    [SerializeField] private GameObject elevator;
    [SerializeField] private Transform lowestPos;
    [SerializeField] private Transform highestPos;
    
    [SerializeField] private float speed;
    private ItemsOnElevator items;

    protected override void Start()
    {
        base.Start();
        items = elevator.GetComponent<ItemsOnElevator>();
    }

    public override void Horizontal(float direction)
    {

    }

    public override void Vertical(float direction)
    {
        // Do nothing
        if (direction > 0)
        {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, highestPos.position, speed * Time.deltaTime);
            items.enableElevator = true;
        }
        else if ( direction < 0)
        {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, lowestPos.position, speed * Time.deltaTime);
            items.enableElevator = true;
        }
        else
        {
            items.enableElevator = false;
        }
    }

    public override void Look(Vector2 md)
    {
        // Do nothing
    }

    public override void Use(bool key)
    {
        if (key)
        {
            items.enableElevator = false;
            playerControllerSupervisor.SwitchPlayerControllerPrevious();
        }
    }


}

