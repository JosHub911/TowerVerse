using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Setup")]
    public Transform shootingPoint;     // where the bullet originates
    public GameObject bulletPrefab;     // bullet prefab (must have Rigidbody2D)
    public float bulletSpeed = 12f;
    public float bulletCooldown = 0.5f; // seconds between shots

    [Header("Optional")]
    public bool useAxis = false; // true = use Input.GetAxis, otherwise GetKey

    // runtime timer (separate from configured cooldown)
    private float cooldownTimer = 0f;

    void Update()
    {
        // tick cooldown every frame
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        // shoot with left mouse button or space
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            if (cooldownTimer > 0f)
                return; // still cooling down

            Vector2 dir = GetShootDirection();

            // fallback if no direction: shoot to the right (adjust to your sprite)
            if (dir == Vector2.zero)
                dir = transform.right;

            Shoot(dir.normalized);

            // reset cooldown timer after shooting
            cooldownTimer = bulletCooldown;
        }
    }

    Vector2 GetShootDirection()
    {
        if (useAxis)
        {
            float h = Input.GetAxisRaw("Horizontal"); // -1..1
            float v = Input.GetAxisRaw("Vertical");
            return new Vector2(h, v);
        }
        else
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) dir += Vector2.up;
            if (Input.GetKey(KeyCode.S)) dir += Vector2.down;
            if (Input.GetKey(KeyCode.A)) dir += Vector2.left;
            if (Input.GetKey(KeyCode.D)) dir += Vector2.right;
            return dir;
        }
    }

    void Shoot(Vector2 direction)
    {
        if (bulletPrefab == null || shootingPoint == null)
            return;

        GameObject b = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // correct property is velocity on Rigidbody2D
            rb.linearVelocity = direction * bulletSpeed;
        }

        // optionally rotate the bullet to face movement direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
