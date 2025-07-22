using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	[SerializeField] GameObject obstaclePrefab;
	[SerializeField] private BoxCollider playerCollider;

	[Header("Spawn Timer")]
	[SerializeField] private float spawnTimer;

	[Header("Spawn Bounds (X - Mins, Y - Maxs)")]
	[SerializeField] private Vector2 bounds;

	private float spawnTimerLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		spawnTimerLeft = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
		spawnTimerLeft -= Time.deltaTime;

		if (spawnTimerLeft <= 0) {
			spawnTimerLeft = spawnTimer;
			transform.position = new Vector3(transform.position.x, Random.Range(bounds.x, bounds.y), transform.position.z);

			GameObject obstacle = Instantiate(obstaclePrefab, transform.position, transform.rotation);

			obstacle.GetComponent<Obstacle>().playerCollider = playerCollider;
		}
    }
}
