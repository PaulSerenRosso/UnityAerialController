using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{   
    [SerializeField] Vector3 upPivot;
    [SerializeField] Vector3 downPivot;
    [SerializeField] Vector3 leftPivot;
    [SerializeField] Vector3 rightPivot;
    [SerializeField] Vector3 forwardPivot;
    [SerializeField] Vector3 backwardPivot;

    public void SetPivots()
    {
        upPivot = transform.position;
        upPivot.y += transform.lossyScale.y / 2;
        downPivot = transform.position;
        downPivot.y += -transform.lossyScale.y / 2;
        leftPivot = transform.position; 
        leftPivot.x += -transform.lossyScale.x / 2;
        rightPivot = transform.position;
        rightPivot.x += transform.lossyScale.x / 2;
        forwardPivot = transform.position;
        forwardPivot.z += transform.lossyScale.z / 2;
        backwardPivot = transform.position;
        backwardPivot.z += transform.lossyScale.z / 2;
    }
    public  Vector3 GetPivotPoint(Direction pivotDirection)
    {
        switch (pivotDirection)
        {
            case Direction.Left:
            {
                return leftPivot;
        
            }
            case Direction.Down:
            {
                return downPivot;
            }
            case Direction.Right:
            {
                return rightPivot;
            }
            case Direction.Up:
            {
                return upPivot;
            }
            case Direction.Forward:
            {
                return forwardPivot;
            }
            case Direction.Backward:
            {
                return backwardPivot;
            }
        }
      return Vector3.zero;
    }
}
