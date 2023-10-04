using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPathWindow : EditorWindow
{
    private LevelManager levelManager;
    private int pathIndex;
    private int pathCurveIndex;
    private void OnEnable()
    {
        
        EditorApplication.update += CalculatePaths;
    }

    private void OnDisable()
    {
        EditorApplication.update -= CalculatePaths;
    }
    [MenuItem ("Window/EnemyPathWindow")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EnemyPathWindow));
    }

    private void OnGUI()
    {
        levelManager = (LevelManager)EditorGUILayout.ObjectField(levelManager, typeof(LevelManager), true);
        GUI.enabled = false;
        if (levelManager != null)
        {
            GUI.enabled = true;
            pathIndex = EditorGUILayout.IntField("Path Index",pathIndex);
            pathCurveIndex = EditorGUILayout.IntField("Path Curve Index",pathCurveIndex);
            if (GUILayout.Button("Reset Control Point Curve"))
            {
                for (int i = 0; i < levelManager.allEnemyPaths.Length; i++)
                {
                    levelManager.allEnemyPaths[pathIndex].ResetControlPointCurve(pathCurveIndex);
                }

            }
        }
    }
    
    void CalculatePaths()
    {
        if (levelManager != null)
        {
            for (int i = 0; i < levelManager.allEnvironmentObjects.Length; i++)
            {
                levelManager.allEnvironmentObjects[i].SetPivots();
               
            }

            for (int i = 0; i < levelManager.allEnemyPaths.Length; i++)
            {
                levelManager.allEnemyPaths[i].CalculatePath(levelManager.allEnvironmentObjects);
            }
            
            
        }

    }
}
