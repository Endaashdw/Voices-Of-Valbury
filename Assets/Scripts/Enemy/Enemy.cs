using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        Spawning,
        Charging
    }

    [SerializeField] private LayerMask playerLayer;
    
    [SerializeField] private float smoothTime = 1f;
    [SerializeField] private float chargeDelay = 5f;
    [SerializeField] private float chargeSpeed = 20f;
    [SerializeField] private float chargeTime = 5f;

    private Vector3 smoothVelocity = Vector3.zero;
    private Vector3 targetPosition;
    private new Rigidbody rigidbody;

    private State state;
    private float spawnTime;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Spawning:
                HandleSpawning();

                break;
            case State.Charging:
                HandleCharging();

                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) // a little bit of bit manip
        {
            GameManager.instance.GameOver();
            Destroy(collision.gameObject);
        }
    }

    private void HandleSpawning()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, smoothTime);

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        float timeSinceSpawn = Time.time - spawnTime;

        if (distanceToTarget < 0.01f && timeSinceSpawn >= chargeDelay)
        {
            state = State.Charging;
            chargeTime = Time.time;
        }
    }

    private void HandleCharging()
    {
        float targetVelocity = chargeSpeed - rigidbody.linearVelocity.x;
        float timeSinceCharge = Time.time - chargeTime;

        rigidbody.AddForce(Vector3.left * targetVelocity, ForceMode.Acceleration);

        if (timeSinceCharge >= chargeTime)
        {
            Destroy(gameObject);
        }
    }

    public void StartSpawning(Vector3 position)
    {
        targetPosition = position;
        state = State.Spawning;
        spawnTime = Time.time;
    }
}
