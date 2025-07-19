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

    [Header("Shooting Test")]
    public ShootingTest shootingTest;
    public Shooter shooter;
    public enum TriggerCondition
    {
        Low,
        High
    }
    public TriggerCondition limit;
    private bool hasFlashed = false; // prevents repeat flashing

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        microphoneData = FindAnyObjectByType<ScaleFromMicrophone>();
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            ScoreManager.instance.AddToScore(1);
            Vector3 targetSpeed;

            if (microphoneData)
            {
                // // TriggerCondition check BEFORE anything else
                // switch (limit)
                // {
                //     case TriggerCondition.Low:
                //         if (microphoneData.normalizedLoudness <= 0.05f && !hasFlashed)
                //         {
                //             shooter.Fire();
                //             shootingTest.Flash();
                //             hasFlashed = true;
                //             return;
                //         }
                //         else if (microphoneData.normalizedLoudness > 0.05f)
                //         {
                //             hasFlashed = false;
                //         }
                //         break;
                //
                //     case TriggerCondition.High:
                //         if (microphoneData.normalizedLoudness > 0.5f && !hasFlashed)
                //         {
                //             shooter.Fire();
                //             shootingTest.Flash();
                //             hasFlashed = true;
                //             return;
                //         }
                //         else if (microphoneData.normalizedLoudness <= 0.5f)
                //         {
                //             hasFlashed = false;
                //         }
                //         break;
                // }

                // Movement logic
                if (microphoneData.loudness > 0.01f)
                {
                    targetSpeed = new Vector2(0f, acceleration * microphoneData.loudness);
                }
                else
                {
                    targetSpeed = new Vector2(0f, -deceleration * 0.5f);
                }
            }
            else
            {
                // Manual input fallback
                if (_movementInput.y > 0.01f)
                {
                    targetSpeed = new Vector2(0f, acceleration);
                }
                else
                {
                    targetSpeed = new Vector2(0f, -deceleration * 0.5f);
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
