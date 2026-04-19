using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public ObjectPool coinPool;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 4f;
    public float spawnX = 15f;
    public float coinY = -1.5f;     
    public int coinsPerGroup = 2;   
    public float checkRadius = 1.5f;

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
            SpawnCoins();
            timer = 0f;
            nextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinsPerGroup; i++)
        {
            Vector3 spawnPos = new Vector3(
                player.position.x + spawnX + (i * 1.2f),
                coinY,
                0
            );

            Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius);

            if (hit != null && hit.CompareTag("Obstacle"))
            {
                continue; 
            }

            GameObject coin = coinPool.GetObject();
            coin.transform.position = spawnPos;
        }
    }
}