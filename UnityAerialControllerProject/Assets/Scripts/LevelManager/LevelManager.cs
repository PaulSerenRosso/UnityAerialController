using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public EnvironmentObject[] allEnvironmentObjects;
    public EnemyPath[] allEnemyPaths;
    
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private EnemyManager[] enemyPrefabs;
    [SerializeField] private GameObject cameraFollower;
    [SerializeField] private GameObject levelUI;

    private GameObject airplane;
    private GameObject camera;
    
    void Start()
    {
        airplane = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        airplane.GetComponent<PlayerCollision>().uiManager = levelUI.GetComponent<UIManager>();
        camera = Instantiate(cameraFollower);
        airplane.GetComponent<AirplaneController>().mainCamera = camera.GetComponent<CameraFollower>();
        airplane.GetComponent<AirplaneController>().speedParticleContainer =
            camera.GetComponent<CameraFollower>().speedParticle;
        camera.GetComponent<CameraFollower>().AirplaneTransform = airplane.transform;
        levelUI.GetComponent<UIManager>().maxEnemyCount = enemyPrefabs.Length;
        levelUI.GetComponent<UIManager>().player = airplane;
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
           EnemyManager enemyManager = Instantiate(enemyPrefabs[i]);
           enemyManager.enemyPath = allEnemyPaths[enemyPrefabs[i].enemyPathIndex];
           enemyManager.uiManager = levelUI.GetComponent<UIManager>();
           levelUI.GetComponent<UIManager>().AddEnemyToList(enemyManager.enemyColor);
        }
    }
}
