using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] private float range = 10f; // How far the turret can see/shoot
    [SerializeField] private float fireRate = 1f; // Bullets per second
    [SerializeField] private GameObject bulletPrefab; // The bullet to shoot
    [SerializeField] private Transform firePoint; // Where bullets spawn
    [SerializeField] private float bulletSpeed = 10f; // How fast bullets travel
    [SerializeField] private float turnSpeed = 5f; // How fast the turret rotates

    private Transform target; // The current enemy we’re aiming at
    private float fireCountdown = 0f; // Timer to control fire rate

    void Update()
    {
        // Step 1: Find a target if we don’t have one or it’s out of range
        if (target == null || target.Equals(null) || Vector3.Distance(transform.position, target.position) > range)
        {
            EnemyLockOn();
            Debug.Log("Locking on to target: " + (target != null ? target.name : "None"));
        }

        // If there’s no valid target, stop here
        if (target == null || target.Equals(null)) return;

        // Step 2: Rotate turret to face target (2D friendly version)
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        // Step 3: Handle shooting
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    // Finds the nearest enemy within range
    private void EnemyLockOn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Searching for enemies, found: " + enemies.Length);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                Debug.Log("Enemy in range: " + enemy.name);
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Spawns a bullet and makes it fly forward
    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bulletGO.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * bulletSpeed; //  correct property for Rigidbody2D
        }
    }

    // Visualize turret range in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
