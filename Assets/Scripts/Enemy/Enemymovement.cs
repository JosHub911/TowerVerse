using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int health = 3; // Enemy health

    private Transform target;
    private int PathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.Path[PathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            PathIndex++;

            if (PathIndex == LevelManager.main.Path.Length)
            {
                // Enemy bereikt het einde
                LevelManager.main.LoseLife();
                EnemySpawner.onEnemyDestroyed.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.Path[PathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

   
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EnemySpawner.onEnemyDestroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
