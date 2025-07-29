using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask energyLayer;

    [SerializeField] private float aimTime = 1f;
    [SerializeField] private float fireCooldown = 1.5f;
    [SerializeField] private int maxEnergy = 5;
    [SerializeField] private float energyDrainRate = 0.2f;
    [SerializeField] private UnityEngine.UI.Image image;

    private float aimTimer;
    private float fireCooldownTimer;
    private float energy;

    private int currentIndex = 0;
    private Bullet[] bullets;

    private void Start()
    {
        bullets = new Bullet[(int)maxEnergy];
        energy = maxEnergy;

        for (int i = 0; i < maxEnergy; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint, true);

            bullet.SetActive(false);

            bullets[i] = bullet.GetComponent<Bullet>();
        }
    }

    private void FixedUpdate()
    {
        EnergyManager.instance.SetEnergy(energy);

        if (energy > 0)
        {
            energy -= energyDrainRate * Time.fixedDeltaTime;
            energy = Mathf.Clamp(energy, 0f, maxEnergy);
            SetEnergyBar();
        }
        else
        {
            var player = GetComponent<PlayerMovement>();

            if (player)
            {
                player.TakeDamage();
            }
        }

        fireCooldownTimer += Time.fixedDeltaTime;

        if (fireCooldownTimer < fireCooldown)
        {
            return;
        }

        if (Physics.Raycast(firePoint.position, Vector3.right, out RaycastHit hit, 20f, enemyLayer))
        {
            aimTimer += Time.fixedDeltaTime;

            Debug.DrawRay(firePoint.position, hit.point - firePoint.position, Color.green);
        }
        else
        {
            aimTimer -= 0.5f * Time.fixedDeltaTime; // half reduction rate

            Debug.DrawRay(firePoint.position, Vector3.right * 20f, Color.red);
        }

        aimTimer = Mathf.Max(aimTimer, 0);

        if (aimTimer >= aimTime)
        {
            Shoot();

            aimTimer = 0f;
            fireCooldownTimer = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & energyLayer) != 0) // a little bit of bit manip
        {
            collision.gameObject.SetActive(false);

            energy++;

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }

            SetEnergyBar();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point not set in the inspector.");

            return;
        }
        if (energy <= 0)
        {
            return;
        }

        Bullet bullet = bullets[currentIndex];

        bullet.Activate(firePoint.position);

        currentIndex++;

        if (currentIndex >= maxEnergy) currentIndex = 0;

        energy-= energyDrainRate;
        SetEnergyBar();
    }

    public void SetEnergyBar()
    {
        image.fillAmount = (float) energy / maxEnergy;
        Debug.Log("Energy: " + (float) energy / maxEnergy);
    }

    public float GetCurrentEnergy()
    {
        return energy;
    }
}
