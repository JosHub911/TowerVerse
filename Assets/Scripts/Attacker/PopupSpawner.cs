using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    public static PopupSpawner Instance;
    public GameObject popupPrefab; // assign prefab
    public Transform player;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPopup(string msg)
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        // Convert world position → UI screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);

        // Spawn at UI location
        var go = Instantiate(popupPrefab, screenPos, Quaternion.identity, canvas.transform);

        // Now animate
        go.GetComponent<PopupText>().Show(msg);
    }
}
