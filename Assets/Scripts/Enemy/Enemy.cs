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

    private Color originalColor;
    private Vector3 originalScale;

    private void Awake()
    {
        // grab sprite renderers on this object + children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        originalColor = spriteRenderers[0].color;
        originalScale = transform.localScale;
    }


    private void Start()
    {
        // Pop-in at the correct size, not Vector3.one
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
            {
                CashSystem.Instance.AddCash(CashReward);
            }
            EnemySpawner.onEnemyDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    private void FlashEffect()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0) return;

        // stop tweens so they don't stack
        foreach (var sr in spriteRenderers)
            sr.DOKill();
        transform.DOKill();

        foreach (var sr in spriteRenderers)
        {
            sr.DOColor(hitColor, 0.05f)
              .OnComplete(() => sr.DOColor(originalColor, 0.15f));
        }

        transform.DOPunchScale(originalScale * 0.15f, 0.25f, 8, 0.5f);
    }


}
