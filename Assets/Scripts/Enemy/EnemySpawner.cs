using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Vector3 spawnOffset = Vector3.zero;

    private float nextSpawnTime;
    public List<Enemy> spawnedEnemies;

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
        spawnedEnemies = new List<Enemy>(new Enemy[spawnPoints.Length]);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime = Time.time + spawnInterval;
        }
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
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            if (enemyScript)
            {
                enemyScript.StartSpawning(spawnPoint.position);
            }

            spawnedEnemies[spawnIndex] = enemyScript;
        }
        else
        {
            Debug.LogWarning("All spawn points are currently occupied. No enemy spawned.");
        }
    }
}
