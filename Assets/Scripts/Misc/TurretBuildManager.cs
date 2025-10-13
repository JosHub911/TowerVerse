using UnityEngine;

public class TurretBuildManager : MonoBehaviour
{
    public static TurretBuildManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void PlaceTurret(GameObject turretPrefab, Vector3 position)
    {
        // Snap to grid (optional, for tower defense)
        Vector3 buildPos = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0);
        Instantiate(turretPrefab, buildPos, Quaternion.identity);
    }
}
