using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Pools")]
    public ObjectPool smallObstaclePool;
    public ObjectPool largeObstaclePool;
    public ObjectPool flyingObstaclePool;

    [Header("Spawn Settings")]
    public float minSpawnTime = 1.5f;
    public float maxSpawnTime = 3f;
    public float spawnX = 15f;      
    public float groundY = -2.5f;   
    public float flyingY = -1f;     

    private Transform player;
    private float timer;
    private float nextSpawn;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        nextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= nextSpawn)
        {
            SpawnObstacle();
            timer = 0f;
            nextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnObstacle()
    {
        int type = Random.Range(0, 3);
        GameObject obstacle;

        if (type == 0)
        {
            obstacle = smallObstaclePool.GetObject();
            obstacle.transform.position = new Vector3(player.position.x + spawnX, groundY, 0);
        }
        else if (type == 1)
        {
            obstacle = largeObstaclePool.GetObject();
            obstacle.transform.position = new Vector3(player.position.x + spawnX, groundY, 0);
        }
        else
        {
            obstacle = flyingObstaclePool.GetObject();
            obstacle.transform.position = new Vector3(player.position.x + spawnX, flyingY, 0);
        }
    }
}