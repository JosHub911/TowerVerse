using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyType
{
    public GameObject prefab;
    public float minSpeed = 2f;
    public float maxSpeed = 3f;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Types")]
    [SerializeField] private EnemyType[] enemyTypes;

    [Header("Wave Settings")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float scalingFactor = 0.8f;

    public static UnityEvent onEnemyDestroyed = new UnityEvent();

    private int currentWave = 1;
    private int enemiesAlive = 0;
    private int enemiesLeftToSpawn = 0;

    private void Awake()
    {
        onEnemyDestroyed.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }

    private IEnumerator WaveLoop()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            enemiesLeftToSpawn = EnemiesPerWave();
            float spawnInterval = 1f / Mathf.Max(0.0001f, enemiesPerSecond);

            while (enemiesLeftToSpawn > 0)
            {
                SpawnEnemy();
                enemiesLeftToSpawn--;
                enemiesAlive++;

                yield return new WaitForSeconds(spawnInterval);
            }

            while (enemiesAlive > 0)
                yield return null;

            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyTypes == null || enemyTypes.Length == 0) return;

        // Pick a random enemy type
        EnemyType type = enemyTypes[Random.Range(0, enemyTypes.Length)];

        // Add a small random offset
        Vector2 offset = Random.insideUnitCircle * 0.8f;

        // Spawn enemy prefab
        GameObject enemyGO = Instantiate(type.prefab, (Vector2)transform.position + offset, Quaternion.identity);

        // Try to assign a random speed (if the script supports it)
        IEnemyMovement movement = enemyGO.GetComponent<IEnemyMovement>();
        if (movement != null)
        {
            float randSpeed = Random.Range(type.minSpeed, type.maxSpeed);
            movement.SetSpeed(randSpeed);
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }

    public void NotifyEnemyKilled()
    {
        EnemyDestroyed();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, scalingFactor));
    }
}
