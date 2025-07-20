using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float smoothTime = 1f;

    private Vector3 smoothVelocity = Vector3.zero;
    private Vector3 targetPosition;

    public bool IsSpawning { get; private set; } = true;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, smoothTime);

        if (transform.position == targetPosition && IsSpawning)
        {
            IsSpawning = false;
        }
    }

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
    }
}
