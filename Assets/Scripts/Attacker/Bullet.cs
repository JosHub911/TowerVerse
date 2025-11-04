using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 3f; // Destroy bullet after 3 seconds

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
           
            Enemymovement enemyMovement = collision.GetComponent<Enemymovement>();
            if (enemyMovement != null)
            {
                enemyMovement.TakeDamage(damage);
            }
            else
            {
                Enemy altEnemy = collision.GetComponent<Enemy>();
                if (altEnemy != null)
                    altEnemy.TakeDamage(damage);
            }

            Destroy(gameObject); // Bullet disappears on impact
        }
        else
        {
            // Optionally destroy on other collisions to avoid stray bullets
            // Destroy(gameObject);
        }
    }
}
