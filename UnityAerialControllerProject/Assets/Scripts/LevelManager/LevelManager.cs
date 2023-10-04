using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private EnemyManager[] enemyPrefabs;
    [SerializeField] private GameObject cameraFollower;
    public EnvironmentObject[] allEnvironmentObjects;
    public EnemyPath[] allEnemyPaths;

    private GameObject airplane;
    private GameObject camera;
    
    void Start()
    {
        airplane = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        camera = Instantiate(cameraFollower);
        camera.GetComponent<CameraFollower>().AirplaneTransform = airplane.transform; 
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
           EnemyManager enemyManager = Instantiate(enemyPrefabs[i]);
           enemyManager.enemyPath = allEnemyPaths[enemyPrefabs[i].enemyPathIndex];
        }
    }

   
}
