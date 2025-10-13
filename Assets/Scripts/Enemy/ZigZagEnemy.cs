using UnityEngine;

public class ZigZagEnemy : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float zigzagAmplitude = 0.5f;
    [SerializeField] private float zigzagFrequency = 5f;

    private Transform target;
    private int pathIndex = 0;
    private float time;

    private void Start()
    {
        target = LevelManager.main.Path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex == LevelManager.main.Path.Length)
            {
                EnemySpawner.onEnemyDestroyed.Invoke();
                Destroy(gameObject);
                return;
            }
            target = LevelManager.main.Path[pathIndex];
            time = 0f;
        }
    }

    private void FixedUpdate()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        time += Time.fixedDeltaTime;

        // Add zig-zag offset
        Vector2 perpendicular = new Vector2(-dir.y, dir.x);
        Vector2 zigzag = perpendicular * Mathf.Sin(time * zigzagFrequency) * zigzagAmplitude;

        rb.linearVelocity = (dir * moveSpeed) + zigzag;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
