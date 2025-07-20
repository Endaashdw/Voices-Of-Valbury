using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
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
            gameObject.SetActive(false);

            rb.linearVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0) // a little bit of bit manip
        {
            var enemyScript = collision.gameObject.GetComponent<Enemy>();

            if (enemyScript == null)
            {
                return;
            }

            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
