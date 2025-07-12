using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private Vector2 minBounds; // bottom-left world position
    [SerializeField] private Vector2 maxBounds; // top-right world position

    private Vector2 _movementInput;
    public bool dead = false;
    public bool movable = true;
    public ScaleFromMicrophone microphoneData;
    public Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        microphoneData = FindAnyObjectByType<ScaleFromMicrophone>();
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Vector3 targetSpeed;

            if (microphoneData != null)
            {
                if (microphoneData.loudness > 0.01f)
                {
                    // Ascending movement
                    targetSpeed = new Vector2(0f, acceleration * microphoneData.loudness);
                }
                else
                {
                    // Passive descent
                    targetSpeed = new Vector2(0f, -deceleration * 0.5f); // adjust descent rate as needed
                }
            }
            else
            {
                if (_movementInput.y > 0.01f)
                {
                    // Ascending movement
                    targetSpeed = new Vector2(0f, acceleration);
                }
                else
                {
                    // Passive descent
                    targetSpeed = new Vector2(0f, -deceleration * 0.5f); // adjust descent rate as needed
                }
            }

            Vector2 speedDifference = targetSpeed - _rigidbody.linearVelocity;

            _rigidbody.AddForce(_rigidbody.mass * acceleration * speedDifference);
        }

        ClampPosition();
    }


    private void ClampPosition()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        transform.position = pos;
    }
}
