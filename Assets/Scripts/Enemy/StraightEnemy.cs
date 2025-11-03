using UnityEngine;

public class StraightEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    private Transform target;
    private int pathIndex = 0;

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
                LevelManager.main.LoseLife();
                EnemySpawner.onEnemyDestroyed.Invoke();
                Destroy(gameObject);
                return;
            }
            target = LevelManager.main.Path[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    private void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
