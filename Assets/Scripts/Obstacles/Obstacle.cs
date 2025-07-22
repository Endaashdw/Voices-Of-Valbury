using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField]
	private float speed = 5.0f;

	private float exitBound = -15f;
	private Rigidbody rb;

	[Header("Colliders")]
	[SerializeField] private BoxCollider boxCollider;
	[SerializeField] public BoxCollider playerCollider;

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
		if (collision == playerCollider) {
			Debug.Log("Hit!");
		}
	}
}
