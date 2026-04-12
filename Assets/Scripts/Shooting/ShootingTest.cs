using UnityEngine;

public class ShootingTest : MonoBehaviour
{
    [SerializeField] GameObject testObject; //just to test shooting
    [SerializeField] GameObject testPlayer; //just to test shooting
    public float duration = .5f; // seconds to stay red
    private SpriteRenderer sr;
    private Rigidbody rb;
    private Color originalColor;
    void Start()
    {
        sr = testObject.GetComponent<SpriteRenderer>();
        rb = testPlayer.GetComponent<Rigidbody>();
        originalColor = sr.color;
    }

    public void Flash()
    {
        StopAllCoroutines(); // cancel previous flashes if any
        StartCoroutine(FlashRoutine());
    }

    private System.Collections.IEnumerator FlashRoutine()
    {
        // Save current velocity
        Vector3 savedVelocity = rb.linearVelocity;
        Vector3 savedAngular = rb.angularVelocity;

        // Freeze
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Flash red
        sr.color = Color.red;
        yield return new WaitForSeconds(duration);

        // Unfreeze
        rb.isKinematic = false;
        rb.linearVelocity = savedVelocity;
        rb.angularVelocity = savedAngular;

        // Restore color
        sr.color = originalColor;
    }
}
