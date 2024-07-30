using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefabý
    public Transform firePoint; // Merminin çýkýþ noktasý
    public float fireRate = 1f; // Ateþ etme sýklýðý
    private float fireCountdown = 0f;

    void Update()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}