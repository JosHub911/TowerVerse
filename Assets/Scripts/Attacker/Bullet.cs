using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 3f; // Destroy bullet after 3 seconds

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemymovement enemy = collision.GetComponent<Enemymovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            else
            {
                Enemy altEnemy = collision.GetComponent<Enemy>();
                if (altEnemy != null)
                    altEnemy.TakeDamage(damage);
            }

            Destroy(gameObject); // Bullet disappears on impact
        }
    }
}
