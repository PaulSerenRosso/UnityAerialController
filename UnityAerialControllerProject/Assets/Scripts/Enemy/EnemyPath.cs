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
    
    public List<Vector3> CheckPointsBezierPoints
    {
        get => checkPointBezierPoints;
    }
    public  void CalculatePath(EnvironmentObject[] allEnvironmentObjects)
    {
        curveCount = checkPointsBezierControlPoints.Length / 2;
        checkPointBezierPoints.Clear();
        
        for (int j = 0; j < curveCount; j++)
        {
            int nodeIndex = j*2;
            Vector3 firstPosition =
                allEnvironmentObjects[environmentObjectsWithPivotsForThePath[nodeIndex].environmentObjectIndex]
                    .GetPivotPoint(environmentObjectsWithPivotsForThePath[nodeIndex].pivotDirection);
            Vector3 lastPosition =
                allEnvironmentObjects[environmentObjectsWithPivotsForThePath[nodeIndex+1].environmentObjectIndex]
                    .GetPivotPoint(environmentObjectsWithPivotsForThePath[nodeIndex+1].pivotDirection);
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


    public void ResetControlPointCurve(int curveIndex)
    {
        checkPointsBezierControlPoints[curveIndex*2] = Vector3.Lerp(
            checkPointBezierPoints[curveIndex*segmentsPerCurve],
            checkPointBezierPoints[(curveIndex+1)*segmentsPerCurve], 0.5f);
        checkPointsBezierControlPoints[(curveIndex*2)+1] = Vector3.Lerp(
            checkPointBezierPoints[curveIndex*segmentsPerCurve],
            checkPointBezierPoints[(curveIndex+1)*segmentsPerCurve-1], 0.5f);
        
    }
}
