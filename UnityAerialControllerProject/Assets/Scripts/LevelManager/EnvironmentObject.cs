using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    public Vector3 upPivot;
    public Vector3 downPivot;
    public Vector3 leftPivot;
    public Vector3 rightPivot;
    public Vector3 forwardPivot;
    public Vector3 backwardPivot;
    public Vector3 position;
    public Vector3 scale;

    private void OnValidate()
    {
        transform.position = position;
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
        Transform currentParent = transform.parent;
        transform.parent = null;
        transform.localScale = scale;
        transform.parent = currentParent;


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
