using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab�
    public Transform firePoint; // Merminin ��k�� noktas�
    public float fireRate = 1f; // Ate� etme s�kl���
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