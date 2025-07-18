using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;

    private float timer;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        rb.linearVelocity = Vector3.right * speed; // Moves directly to the right
        timer = lifetime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
			Destroy(gameObject);
        }
    }
}
