using System.Collections.Generic;
using UnityEngine;

class EnergySpawner : MonoBehaviour
{
    [SerializeField] private GameObject energyPrefab;
    [SerializeField] private float minSpawnY;
    [SerializeField] private float maxSpawnY;
    [SerializeField] private float spawnInterval = 1f;

    private float nextSpawnTime;

    private int energyPoolCount = 8;
    private int currentIndex = 0;
    private Energy[] energies;

    private void Start()
    {
        energies = new Energy[energyPoolCount];

        for (int i = 0; i < energyPoolCount; i++)
        {
            GameObject energy = Instantiate(energyPrefab, gameObject.transform);

            energy.SetActive(false);

            energies[i] = energy.GetComponent<Energy>();
        }

        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnergy();

            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnergy()
    {
        float randomY = Random.Range(minSpawnY, maxSpawnY);
        Vector3 spawnPostition = transform.position + Vector3.up * randomY;
        Energy energy = energies[currentIndex];

        energy.Activate(spawnPostition);

        currentIndex++;

        if (currentIndex >= energyPoolCount) currentIndex = 0;
    }
}