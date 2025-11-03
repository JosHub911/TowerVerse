using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretDragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Turret Prefab")]
    [SerializeField] private int turretCost = 10;
    [SerializeField] private GameObject turretPrefab;

    private Image iconImage;
    private GameObject dragIcon;

    private Canvas parentCanvas;
    private RectTransform canvasRectTransform;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
        parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
            canvasRectTransform = parentCanvas.transform as RectTransform;
    }

    // Called when drag starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (iconImage == null || parentCanvas == null) return;

        // Create a copy of the icon to drag
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(parentCanvas.transform, false);

        Image img = dragIcon.AddComponent<Image>();
        img.sprite = iconImage.sprite;
        img.raycastTarget = false; // So it doesn’t block raycasts
        img.SetNativeSize();

        // Make sure the icon is on top
        dragIcon.transform.SetAsLastSibling();

        // Optionally add CanvasGroup to block interactions on original element while dragging
        CanvasGroup cg = dragIcon.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;
    }

    // Called during dragging
    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        {
            // Move the drag icon with the cursor
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos);
            dragIcon.transform.position = worldPos;
        }
    }

    // Called when drag ends
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            Destroy(dragIcon);

        // Convert screen position to world point
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);

        // 2D Raycast at mouse position
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Buildable"))
        {
            if (CashSystem.Instance != null)
            {
                bool canAfford = CashSystem.Instance.SpendCash(turretCost);
                if (canAfford)
                {
                    // Only place turret if we successfully spent the cash
                    if (TurretBuildManager.Instance != null && turretPrefab != null)
                    {
                        TurretBuildManager.Instance.PlaceTurret(turretPrefab, hit.point);
                    }
                }
                else
                {
                    Debug.Log("Not enough cash to place turret!");
                }
                }
            }
        }
}
