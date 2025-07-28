using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration = 5f;
    [SerializeField] protected AudioClip collectSound;

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
            //AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyPowerUp(GameObject player);
}