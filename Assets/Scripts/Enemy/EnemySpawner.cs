using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Vector3 spawnOffset = Vector3.zero;

    private float nextSpawnTime;
    public List<GameObject> spawnedEnemies;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not set in the inspector.");

            return;
        }
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points not set or empty in the inspector.");

            return;
        }

        nextSpawnTime = Time.time + spawnInterval;
        spawnedEnemies = new List<GameObject>(new GameObject[spawnPoints.Length]);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime = Time.time + spawnInterval;
        }

        MoveEnemies();
    }

    private void SpawnEnemy()
    {
        // Find all free spawn point indices
        var freeIndices = new List<int>();

        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] == null)
            {
                freeIndices.Add(i);
            }
        }

        if (freeIndices.Count > 0)
        {
            int randomListIndex = Random.Range(0, freeIndices.Count);
            int spawnIndex = freeIndices[randomListIndex];

            Transform spawnPoint = spawnPoints[spawnIndex];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position + spawnOffset, spawnPoint.rotation, transform);

            spawnedEnemies[spawnIndex] = enemy;
        }
        else
        {
            Debug.LogWarning("All spawn points are currently occupied. No enemy spawned.");
        }
    }

    private void MoveEnemies()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            GameObject enemy = spawnedEnemies[i];

            if (enemy == null)
            {
                continue; // Skip if the enemy has been destroyed or is not set
            }

            Enemy enemyScript = enemy.GetComponent<Enemy>();

            if (enemyScript == null)
            {
                continue;
            }
                
            if (enemyScript.IsSpawning)
            {
                enemyScript.MoveTo(spawnPoints[i].position);
            }
        }
    }
}
