using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField]
	private float speed = 5.0f;

	[SerializeField] private float exitBound = -30f;
	private Rigidbody rb;

	[Header("Colliders")]
	[SerializeField] private Collider collide;
	[SerializeField] public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
		transform.position = transform.position + Vector3.left * speed * Time.fixedDeltaTime;

		if (transform.position.x <= exitBound)
			Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider collision) {
		if (collision == player.GetComponent<BoxCollider>()) {
			KillPlayer();
		}
	}

	// If using contacts instead.
	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject == player) {
			KillPlayer();
		}
	}

	private void KillPlayer() {
		player.SetActive(false);
		Debug.Log("Player Killed!");
	}
}
