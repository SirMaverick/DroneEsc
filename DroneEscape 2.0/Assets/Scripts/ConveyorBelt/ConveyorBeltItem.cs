using System;
using System.Collections;
using UnityEngine;
public class ConveyorBeltItem : MonoBehaviour
{
    public Transform nextPosition;
    public int nextPositionId;
    public bool state;

   public void NextPosition( Transform nextPosition, int id)
    {
        Transform currentNextPosition = this.nextPosition;
        // check if it didn't change (I hope this works)
        if (currentNextPosition.Equals( this.nextPosition))
        {
            this.nextPosition = nextPosition;
            nextPositionId = id;
        }
    }
}

