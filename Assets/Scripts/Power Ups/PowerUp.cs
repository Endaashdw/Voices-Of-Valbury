using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration = 5f;

    private void Start()
    {
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.linearVelocity = Vector3.left * 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);

        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyPowerUp(GameObject player);
}