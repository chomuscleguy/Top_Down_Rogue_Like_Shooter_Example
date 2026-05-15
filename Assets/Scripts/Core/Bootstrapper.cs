using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private EnemySpawner enemySpawnerPrefab;

    [SerializeField]
    private InfiniteMap infiniteMapPrefab;

    [Header("Scene")]
    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private GameFlowController gameFlow;

    private Player playerInstance;

    private void Start()
    {
        RunData run = Core.Instance.Game.Run;

        InitPlayer(run);

        InitMap();

        InitSpawner();

        InitCamera();

        InitGameFlow();
    }

    private void InitPlayer(RunData run)
    {
        playerInstance = Instantiate(playerPrefab);

        playerInstance.Init(run);
    }

    private void InitMap()
    {
        InfiniteMap map = Instantiate(infiniteMapPrefab);

        map.Init(playerInstance.transform);
    }

    private void InitSpawner()
    {
        EnemySpawner spawner = Instantiate(enemySpawnerPrefab);

        spawner.Init(playerInstance.transform);
    }

    private void InitCamera()
    {
        if (cameraFollow == null)
            return;

        cameraFollow.Init(playerInstance.transform);
    }

    private void InitGameFlow()
    {
        if (gameFlow == null)
            return;

        gameFlow.Init(playerInstance.Run);
    }
}