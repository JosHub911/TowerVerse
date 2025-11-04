using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public class EnemyType
{
    public GameObject prefab;
    public float spawnWeight = 1f; // Relative spawn chance
    public float minSpeed = 2f;
    public float maxSpeed = 3f;

    [Header("Wave Scaling")]
    public float weightIncreasePerWave = 0.2f; // Increase spawn chance each wave
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
    [SerializeField] private int maxWaves = 5;
    [SerializeField] private float spawnOffsetRadius = 18f;

    [Header("WinScreen")]
    [SerializeField] private GameObject WinScreen;

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

            // Increase spawn weight per wave
            foreach (var e in enemyTypes)
                e.spawnWeight += e.weightIncreasePerWave * (currentWave - 1);

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

            if (currentWave > maxWaves)
            {
                Debug.Log("All waves completed!");
                WinScreen.SetActive(true);
                WinScreen.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBack);
                yield break;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyTypes == null || enemyTypes.Length == 0)
            return;

        EnemyType chosen = ChooseWeightedEnemy();

        Vector2 offset = Random.insideUnitCircle * spawnOffsetRadius;
        Instantiate(chosen.prefab, (Vector2)transform.position + offset, Quaternion.identity);
    }

    private EnemyType ChooseWeightedEnemy()
    {
        float total = 0;
        foreach (var e in enemyTypes)
            total += e.spawnWeight;

        float rnd = Random.value * total;

        foreach (var e in enemyTypes)
        {
            if (rnd < e.spawnWeight)
                return e;
            rnd -= e.spawnWeight;
        }
        return enemyTypes[0];
    }

    public void EnemyDestroyed()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, scalingFactor));
    }

    public bool IsAllWavesDone()
    {
        return currentWave > maxWaves;
    }
}
