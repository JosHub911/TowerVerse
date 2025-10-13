using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDrag : MonoBehaviour
{
    [Header("General")]
    [Tooltip("How fast the camera follows the drag. 1 = 1:1 with finger/mouse movement.")]
    public float panSpeed = 1f;

    [Tooltip("Enable to constrain the camera to an area.")]
    public bool useBounds = false;

    [Tooltip("World-space bounds for the camera center (xMin, yMin, xMax, yMax).")]
    public Vector4 bounds = new Vector4(-10f, -10f, 10f, 10f); // xMin, yMin, xMax, yMax

    private Camera cam;
    private bool dragging = false;
    private Vector3 lastWorldPos;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        // --- Touch support (single touch) ---
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                StartDrag(t.position);
            }
            else if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
            {
                ContinueDrag(t.position);
            }
            else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                EndDrag();
            }

            // ignore additional touches here (useful for pinch/zoom later)
            return;
        }
        else if (Input.touchCount > 1)
        {
            // If multi-touch, stop dragging to allow other gestures (pinch, etc.)
            EndDrag();
            return;
        }

        // --- Mouse support ---
        if (Input.GetMouseButtonDown(1))
        {
            StartDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            ContinueDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            EndDrag();
        }
    }

    private void StartDrag(Vector2 screenPos)
    {
        dragging = true;
        lastWorldPos = ScreenToWorldOnPlane(screenPos);
    }

    private void ContinueDrag(Vector2 screenPos)
    {
        if (!dragging) return;

        Vector3 currentWorldPos = ScreenToWorldOnPlane(screenPos);
        Vector3 delta = lastWorldPos - currentWorldPos; // world difference
        Vector3 newCamPos = transform.position + delta * panSpeed;

        if (useBounds)
        {
            newCamPos = ClampCameraPosition(newCamPos);
        }

        transform.position = newCamPos;

        // Update lastWorldPos so movement is relative next frame —
        // this prevents any "jump" or residual motion when releasing.
        lastWorldPos = ScreenToWorldOnPlane(screenPos);
    }

    private void EndDrag()
    {
        // Stop dragging and do NOT apply any additional movement (no inertia).
        dragging = false;
    }

    // Convert screen position to world position on the z = 0 plane (or whatever plane camera points at).
    private Vector3 ScreenToWorldOnPlane(Vector2 screenPos)
    {
        float zDistance = -cam.transform.position.z; // e.g. camera at z = -10 => zDistance = 10
        Vector3 screenPoint = new Vector3(screenPos.x, screenPos.y, zDistance);
        return cam.ScreenToWorldPoint(screenPoint);
    }

    private Vector3 ClampCameraPosition(Vector3 camPos)
    {
        float xMin = bounds.x;
        float yMin = bounds.y;
        float xMax = bounds.z;
        float yMax = bounds.w;

        camPos.x = Mathf.Clamp(camPos.x, xMin, xMax);
        camPos.y = Mathf.Clamp(camPos.y, yMin, yMax);
        return camPos;
    }
}
