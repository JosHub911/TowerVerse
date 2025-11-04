using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Red Flash")]
    [SerializeField] private Image redPanel;
    [SerializeField] private float flashDuration = 0.2f;

    [Header("Popup")]
    [SerializeField] private GameObject popupPrefab; // "+1" prefab
    [SerializeField] private Transform playerTransform; // Where popup appears

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (redPanel != null) redPanel.color = new Color(1, 0, 0, 0); // Make transparent
    }

    public void RedFlash()
    {
        if (redPanel == null) return;

        redPanel.DOKill();
        redPanel.color = new Color(1, 0, 0, 1);
        redPanel.DOFade(0f, flashDuration);
    }

    public void ShowPopup(string text = "+1")
    {
        if (popupPrefab == null || playerTransform == null) return;

        GameObject popup = Instantiate(popupPrefab, playerTransform.position, Quaternion.identity, playerTransform);
        popup.GetComponentInChildren<TextMeshProUGUI>().text = text;

        // Animate popup
        popup.transform.DOMoveY(popup.transform.position.y + 1f, 0.8f).SetEase(Ease.OutCubic);
        popup.transform.DOScale(Vector3.zero, 0.8f).SetEase(Ease.InBack)
            .OnComplete(() => Destroy(popup));
    }
}
