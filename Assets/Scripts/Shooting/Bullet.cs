using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 2f;

    private float timer;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        rigidbody.linearVelocity = Vector3.right * speed; // Moves directly to the right
        timer = lifetime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            gameObject.SetActive(false);

            rigidbody.linearVelocity = Vector3.zero;
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
