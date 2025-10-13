using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretDragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Turret Prefab")]
    [SerializeField] private GameObject turretPrefab;

    private Image iconImage;
    private GameObject dragIcon;

    private RectTransform canvasTransform;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
        canvasTransform = GetComponentInParent<Canvas>().transform as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create a copy of the icon to drag
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(canvasTransform, false);

        Image img = dragIcon.AddComponent<Image>();
        img.sprite = iconImage.sprite;
        img.raycastTarget = false; // So it doesn’t block raycasts
        img.SetNativeSize();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
        {
            
            dragIcon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null) Destroy(dragIcon);

        // Convert screen position to world point
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);

        // 2D Raycast at mouse position
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Buildable"))
        {
            TurretBuildManager.Instance.PlaceTurret(turretPrefab, hit.point);
        }
    }
}
