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

    private float aimTimer;
    private float fireCooldownTimer;
    private int energy;

    private int currentIndex = 0;
    private Bullet[] bullets;

    private void Start()
    {
        bullets = new Bullet[maxEnergy];
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

        fireCooldownTimer += Time.fixedDeltaTime;

        if (fireCooldownTimer < fireCooldown)
        {
            return;
        }

        if (Physics.Raycast(firePoint.position, Vector3.right, out RaycastHit hit, 15f, enemyLayer))
        {
            aimTimer += Time.fixedDeltaTime;

            Debug.DrawRay(firePoint.position, hit.point - firePoint.position, Color.green);
        }
        else
        {
            aimTimer -= 0.5f * Time.fixedDeltaTime; // half reduction rate

            Debug.DrawRay(firePoint.position, Vector3.right * 15f, Color.red);
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

        energy--;
    }
}
