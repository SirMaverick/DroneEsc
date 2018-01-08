using System;
using System.Collections;
using UnityEngine;
public class ConveyorBeltItem : MonoBehaviour
{
    public Transform nextPosition;
    public int nextPositionId;

   public IEnumerator WaitAndGoToNextPosition(float waitTime, Transform nextPosition, int id)
    {
        Transform currentNextPosition = this.nextPosition;
        yield return new WaitForSeconds(waitTime);
        // check if it didn't change (I hope this works)
        if (currentNextPosition.Equals( this.nextPosition))
        {
            this.nextPosition = nextPosition;
            nextPositionId = id;
        }
    }
}

