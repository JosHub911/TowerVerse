using UnityEngine;
using DG.Tweening;

public class CamShake : MonoBehaviour
{
    public static CamShake Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void Shake(float duration = 0.3f, float strength = 0.5f)
    {
        // Assumes main camera is attached to this script
        if (Camera.main != null)
            Camera.main.transform.DOShakePosition(duration, strength, 20, 90, false, true);
    }
}
