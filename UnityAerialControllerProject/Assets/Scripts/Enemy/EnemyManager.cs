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
    public Color enemyColor;
    public UIManager uiManager;
    private Vector3[] pathPoints;
    private int currentDestinationPointIndex;
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private float rotationTime = 0.2f;
    [SerializeField] private float rotationTimer = 0;
    private Vector3 currentDirection;
    private float currentDistanceFromDestination;
    private float currentSpeed;
    [SerializeField] private MeshRenderer[] meshRenderers;
    [SerializeField] private TrailRenderer trailRenderer;


    private void Start()
    {
        UpdateManager.Register(this);
        pathPoints = enemyPath.CheckPointsBezierPoints.ToArray();
        currentDestinationPointIndex = pathPoints.Length - 1;
        transform.position = pathPoints[0];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = enemyColor;
        }

        trailRenderer.startColor = enemyColor;
        ChooseNewDestinationPoint();
    }

    private void OnDisable()
    {
        UpdateManager.UnRegister(this);
    }

    public void OnUpdate()
    {
        currentDistanceFromDestination = (pathPoints[currentDestinationPointIndex] - transform.position).magnitude;
        currentSpeed = movementSpeed * Time.deltaTime;
        if (currentSpeed > currentDistanceFromDestination)
        {
            transform.position += currentDirection * currentDistanceFromDestination;
            ChooseNewDestinationPoint();
        }
        else
        {
            if (rotationTimer < rotationTime)
            {
                rotationTimer += Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, currentDirection, rotationTimer / rotationTime);
            }

            transform.position += currentDirection * currentSpeed;
        }
    }

    private void ChooseNewDestinationPoint()
    {
        currentDestinationPointIndex++;
        rotationTimer = 0;
        if (currentDestinationPointIndex == pathPoints.Length)
        {
            currentDestinationPointIndex = 1;
        }

        currentDirection = (pathPoints[currentDestinationPointIndex] - pathPoints[currentDestinationPointIndex - 1])
            .normalized;
    }

    public void Kill()
    {
        uiManager.UpdateEnemyCount();
        uiManager.RemoveEnemyFromList(enemyColor);
        gameObject.SetActive(false);
    }
}