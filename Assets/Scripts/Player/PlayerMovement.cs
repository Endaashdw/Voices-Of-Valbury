using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ScaleFromMicrophone microphoneData;

    [SerializeField] private float lift;
    [SerializeField] private float damping;
    [SerializeField] private float maxSpeed;

    [SerializeField] private float minBounds; // bottom
    [SerializeField] private float maxBounds; // top    

    private new Rigidbody rigidbody;
    private float input;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        microphoneData = FindAnyObjectByType<ScaleFromMicrophone>();
    }

    private void Start()
    {
        rigidbody.linearDamping = damping;
        rigidbody.maxLinearVelocity = maxSpeed;
    }

    private void Update()
    {
        input = Input.GetKey(KeyCode.Space) ? 2f : 0f;
    }

    private void FixedUpdate()
    {
        if (microphoneData && microphoneData.loudness > 0.01f)
        {
            float targetVelocity = lift * microphoneData.loudness;
            float acceleration = targetVelocity - rigidbody.linearVelocity.y;

            rigidbody.AddForce(Vector3.up * acceleration, ForceMode.Acceleration);
        }
        if (input > 0.01f)
        {
            float targetVelocity = lift * input;
            float acceleration = targetVelocity - rigidbody.linearVelocity.y;

            rigidbody.AddForce(Vector3.up * acceleration, ForceMode.Acceleration);
        }
        else if (rigidbody.linearVelocity.y > 0.01f)
        {
            float deceleration = 0.5f * lift;

            rigidbody.AddForce(Vector3.down * deceleration, ForceMode.Acceleration);
        }

        ConstrainToBounds();
    }

    private void ConstrainToBounds()
    {
        Vector3 position = transform.position;
        Vector3 velocity = rigidbody.linearVelocity;

        if (position.y <= minBounds && velocity.y < 0f)
        {
            position.y = minBounds;
            velocity.y = 0f;
        }
        if (position.y >= maxBounds && velocity.y > 0f)
        {
            position.y = maxBounds;
            velocity.y = 0f;
        }

        transform.position = position;
        rigidbody.linearVelocity = velocity;
    }

    public void TakeDamage()
    {
        var powerUpController = GetComponent<PowerUpController>();

        if (powerUpController != null && powerUpController.HasShield())
        {
            // Shield absorbs damage
            powerUpController.ActivateShield(0); // Immediately deactivate shield
            return;
        }

        GameManager.instance.GameOver();
    }
}
