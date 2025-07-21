using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float aimTime = 1f;
    [SerializeField] private float fireCooldown = 1.5f;
    [SerializeField] private int bulletPoolSize = 20;

    private float aimTimer;
    private float fireCooldownTimer;
    private int currentIndex = 0;
    private Bullet[] bullets;

    private void Start()
    {
        bullets = new Bullet[bulletPoolSize];
        
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, firePoint, true);

            obj.SetActive(false);
            bullets[i] = obj.GetComponent<Bullet>();
        }   
    }

    private void FixedUpdate()
    {
        if (EnemySpawner.Instance.spawnedEnemies.Count > 0)
        {
            AimAndShoot();
        }
    }

    private void AimAndShoot()
    {
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

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point not set in the inspector.");

            return;
        }

        Bullet b = bullets[currentIndex];
         
        b.Activate(firePoint.position);

        currentIndex++;
        if (currentIndex >= bulletPoolSize) currentIndex = 0;
    }
}
