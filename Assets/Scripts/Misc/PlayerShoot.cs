using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Setup")]
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 12f;
    public float bulletCooldown = 0.2f;

    [Header("Gun Stats")]
    [Tooltip("Max angle deviation in degrees (e.g., 5 = small spread, 20 = shotgun)")]
    public float spreadAngle = 5f;

    private float cooldownTimer = 0f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownTimer <= 0f)
        {
            Vector2 dir = GetMouseDirection();
            if (dir == Vector2.zero)
                dir = transform.right;

            dir = ApplySpread(dir.normalized);

            Shoot(dir);
            cooldownTimer = bulletCooldown;
        }
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorld - shootingPoint.position;
        return direction;
    }

    Vector2 ApplySpread(Vector2 direction)
    {
        // pick random angle inside (-spreadAngle, +spreadAngle)
        float randomRot = Random.Range(-spreadAngle, spreadAngle);
        float rad = randomRot * Mathf.Deg2Rad;

        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        // rotate the direction vector manually
        Vector2 newDir = new Vector2(
            direction.x * cos - direction.y * sin,
            direction.x * sin + direction.y * cos
        );

        return newDir.normalized;
    }

    void Shoot(Vector2 direction)
    {
        if (bulletPrefab == null || shootingPoint == null)
            return;

        GameObject b = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();

        SoundManager.Instance?.PlayShoot();
        CamShake.Instance?.Shake(0.1f, 0.2f);

        if (rb != null)
            rb.linearVelocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
