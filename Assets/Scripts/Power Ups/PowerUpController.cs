using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private GameObject shieldVisual;
    
    private bool isMagnetActive;
    private float magnetTimer;
    private float magnetRadius;
    private float attractSpeed;
    
    private bool isShieldActive;
    private float shieldTimer;

    private void Start()
    {
        shieldVisual.SetActive(false);
    }

    private void Update()
    {
        HandleMagnet();
        HandleShield();
    }
    
    private void HandleMagnet()
    {
        if (!isMagnetActive) return;
        
        magnetTimer -= Time.deltaTime;
        if (magnetTimer <= 0)
        {
            isMagnetActive = false;
            return;
        }
        
        // Attract nearby energy
        Collider[] energyItems = Physics.OverlapSphere(
            transform.position, 
            magnetRadius, 
            LayerMask.GetMask("Energy")
        );

        foreach (var energy in energyItems)
        {
            var energyRigidbody = energy.gameObject.GetComponent<Rigidbody>();

            var toPlayer = (transform.position - energy.transform.position).normalized;
            var targetVelocity = toPlayer * attractSpeed;
            var velocityChange = targetVelocity - energyRigidbody.linearVelocity;

            energyRigidbody.AddForce(velocityChange, ForceMode.Acceleration);
        }
    }
    
    private void HandleShield()
    {
        if (!isShieldActive) return;
        
        shieldTimer -= Time.deltaTime;
        if (shieldTimer <= 0)
        {
            isShieldActive = false;
            shieldVisual.SetActive(false);
        }
    }
    
    public void ActivateMagnet(float duration, float radius, float speed)
    {
        isMagnetActive = true;
        magnetTimer = duration;
        magnetRadius = radius;
        attractSpeed = speed;
    }
    
    public void ActivateShield(float duration)
    {
        isShieldActive = true;
        shieldTimer = duration;
        shieldVisual.SetActive(true);
    }
    
    public bool HasShield()
    {
        return isShieldActive;
    }
}