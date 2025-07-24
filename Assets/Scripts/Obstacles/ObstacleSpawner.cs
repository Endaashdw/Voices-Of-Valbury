using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	[SerializeField] GameObject obstaclePrefab;
	[SerializeField] private GameObject	 player;

	[Header("Spawn Timer")]
	[SerializeField] private float spawnTimer;

	[Header("Spawn Bounds (X - Mins, Y - Maxs)")]
	[SerializeField] private bool randomPosition;
	[SerializeField] private Vector2 bounds;

	[Header("Initalizers/Random Angle Bounds")]
	[SerializeField] private Vector3 initAngles;
	[SerializeField] private bool randomAngles;
	[SerializeField] private Vector2 angleBounds;

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
			if (randomPosition)
				transform.position = new Vector3(transform.position.x, Random.Range(bounds.x, bounds.y), transform.position.z);

			GameObject obstacle;
			if (randomAngles) {
				obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.Euler(initAngles.x, initAngles.y, Random.Range(angleBounds.x, angleBounds.y)));
			}
			else {
				obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.Euler(initAngles));
			}

			obstacle.GetComponent<Obstacle>().player = player;
		}
    }
}
