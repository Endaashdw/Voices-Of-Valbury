
using UnityEngine;

class Energy : MonoBehaviour
{
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

        rigidbody.linearVelocity = Vector3.left * speed; // Moves directly to the left
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
}