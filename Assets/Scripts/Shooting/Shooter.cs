using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public int poolSize = 20;
    public Transform firePoint;

    private Bullet[] bullets;
    private int currentIndex = 0;

    private void Start()
    {
        bullets = new Bullet[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, firePoint, true);
            
            obj.SetActive(false);
            bullets[i] = obj.GetComponent<Bullet>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        Bullet b = bullets[currentIndex];
         
        b.Activate(firePoint.position);

        currentIndex++;
        if (currentIndex >= poolSize) currentIndex = 0;
    }
}
