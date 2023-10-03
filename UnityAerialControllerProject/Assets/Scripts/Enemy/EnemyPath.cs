using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    
   [SerializeField] private Vector3[] checkPointsBezierControlPoints;
   [SerializeField] private List<Vector3> checkPointBezierPoints ;
    [SerializeField] private Color gizmosColor;
    [SerializeField] private int curveCount ;
    [SerializeField] private int segmentsPerCurve;
    public EnvironmentObjectWithPivots[] environmentObjectsWithPivotsForThePath;
    public EnvironmentObject[] allEnvironmentObjects;
    public List<Vector3> CheckPointsBezierPoints
    {
        get => checkPointBezierPoints;
    }
    public  void OnValidate()
    {
        curveCount = (checkPointsBezierControlPoints.Length+environmentObjectsWithPivotsForThePath.Length) / 3;
        checkPointBezierPoints.Clear();
        for (int j = 0; j < curveCount; j++)
        {
            int nodeIndex = j*2;
            Vector3 firstPosition =
                allEnvironmentObjects[environmentObjectsWithPivotsForThePath[j].environmentObjectIndex]
                    .GetPivotPoint(environmentObjectsWithPivotsForThePath[j].pivotDirection);
            Vector3 lastPosition =
                allEnvironmentObjects[environmentObjectsWithPivotsForThePath[j + 1].environmentObjectIndex]
                    .GetPivotPoint(environmentObjectsWithPivotsForThePath[j + 1].pivotDirection);
            for (int i = 0; i <=  segmentsPerCurve; i++)
            {
                float t = i / (float)segmentsPerCurve;
                Vector3 pixel =  CalculateCubicBezierPoint(t, 
                    firstPosition,
                checkPointsBezierControlPoints [nodeIndex], checkPointsBezierControlPoints [nodeIndex + 1],lastPosition
                ); ;
                checkPointBezierPoints.Add(pixel);
      
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        for (int i = 0; i < checkPointBezierPoints.Count-1; i++)
        {
            Gizmos.DrawLine(checkPointBezierPoints[i], checkPointBezierPoints[i+1]);
        }
      
    }
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        
        Vector3 p = uuu * p0; 
        p += 3 * uu * t * p1; 
        p += 3 * u * tt * p2; 
        p += ttt * p3; 
        
        return p;
    }
    
  
}
