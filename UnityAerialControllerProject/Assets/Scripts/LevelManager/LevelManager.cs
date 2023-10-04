using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private EnemyManager[] enemyPrefabs;
    [SerializeField] private GameObject camera;
    public EnvironmentObject[] allEnvironmentObjects;
    public EnemyPath[] allEnemyPaths;

    void Start()
    {
        Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
           EnemyManager enemyManager = Instantiate(enemyPrefabs[i]);
           enemyManager.enemyPath = allEnemyPaths[enemyPrefabs[i].enemyPathIndex];
        }
    }

   
}
