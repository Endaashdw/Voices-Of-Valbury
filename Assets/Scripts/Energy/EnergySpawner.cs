using System.Collections.Generic;
using UnityEngine;

class EnergySpawner : MonoBehaviour
{
    [SerializeField] private GameObject energyPrefab;
    [SerializeField] private float minSpawnY;
    [SerializeField] private float maxSpawnY;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private Vector3 spawnOffset = Vector3.zero;

    private float nextSpawnTime;

    private int energyPoolCount = 6;
    private int currentIndex = 0;
    private object[] energies;

    // TODO: implement
}