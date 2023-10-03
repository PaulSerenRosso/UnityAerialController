using System;
using System.Collections;
using System.Collections.Generic;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour, IUpdatable
{
    public int enemyPathIndex;
     public EnemyPath enemyPath;
    private Vector3[] pathPoints;
    private int currentDestinationPointIndex;
    [SerializeField] private float speed;
    private Vector3 currentDirection;
    private float currentDistanceFromDestination;
    private float currentSpeed;
    private void Start()
    {
     UpdateManager.Register(this);
     pathPoints = enemyPath.CheckPointsBezierPoints.ToArray();
     currentDestinationPointIndex = pathPoints.Length-1;
     transform.position = pathPoints[0];
     ChooseNewDestinationPoint();
    }

    public void OnUpdate()
    {
        currentDistanceFromDestination = (pathPoints[currentDestinationPointIndex] - transform.position).magnitude;
        currentSpeed = speed * Time.deltaTime;
        if (currentSpeed >currentDistanceFromDestination )
        {
            transform.position += currentDirection*currentDistanceFromDestination;
            ChooseNewDestinationPoint();
        }
        else
        {
            transform.position += currentDirection * currentSpeed;
        }
       
    }

    private void ChooseNewDestinationPoint()
    {  
        currentDestinationPointIndex++;
      
            if (currentDestinationPointIndex == pathPoints.Length) 
            {
                     currentDestinationPointIndex = 1;
            }
            currentDirection= (pathPoints[currentDestinationPointIndex] - pathPoints[currentDestinationPointIndex - 1]).normalized;
             transform.forward = currentDirection;
    }
}
    

 
