using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void TakeDamage(int damage)
    {
        
        health -= damage;

        if (health <= 0)
        {
            EnemySpawner.onEnemyDestroyed.Invoke(); // notify spawner
            Destroy(gameObject);
        }

    }

}
