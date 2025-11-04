using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int CashReward = 5;
    [SerializeField] private int health = 3;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private Transform visuals; // drag the child here

    private Color originalColor;
    private Vector3 originalScale;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        originalColor = spriteRenderers[0].color;
        originalScale = transform.localScale;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale, 0.35f).SetEase(Ease.OutBack);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashEffect();

        if (health <= 0)
        {
            if (CashSystem.Instance != null)
                CashSystem.Instance.AddCash(CashReward);

            EnemySpawner.onEnemyDestroyed.Invoke();
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        // Stop movement / behavior scripts if needed (optional)
        GetComponent<Collider2D>().enabled = false;

        // Fade + shrink
        foreach (var sr in spriteRenderers)
            sr.DOFade(0f, 0.4f);

        transform.DOScale(0f, 0.4f).SetEase(Ease.InBack)
        .OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    

    private void FlashEffect()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0) return;

        foreach (var sr in spriteRenderers)
            sr.DOKill();
        transform.DOKill();

        foreach (var sr in spriteRenderers)
        {
            sr.DOColor(hitColor, 0.05f)
              .OnComplete(() => sr.DOColor(originalColor, 0.15f));
        }

        visuals.DOShakePosition(1f, 0.1f);
    }
}
