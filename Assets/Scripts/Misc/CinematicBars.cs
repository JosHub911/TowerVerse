using UnityEngine;

public class CinematicBars : MonoBehaviour
{
    public RectTransform topBar;
    public RectTransform bottomBar;

    [Tooltip("How far off-screen the bars start (in pixels)")]
    public float startOffset = 300f;

    [Tooltip("How long the slide animation takes (in seconds)")]
    public float slideTime = 1f;

    private Vector2 topTarget;
    private Vector2 bottomTarget;

    void Start()
    {
        // Save original positions as their final targets
        topTarget = topBar.anchoredPosition;
        bottomTarget = bottomBar.anchoredPosition;

        // Move bars off-screen vertically
        topBar.anchoredPosition = new Vector2(topTarget.x, topTarget.y + startOffset);
        bottomBar.anchoredPosition = new Vector2(bottomTarget.x, bottomTarget.y - startOffset);

        // Start animation
        StartCoroutine(SlideBarsIn());
    }

    System.Collections.IEnumerator SlideBarsIn()
    {
        float t = 0f;

        while (t < slideTime)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / slideTime);

            topBar.anchoredPosition = Vector2.Lerp(
                topBar.anchoredPosition,
                topTarget,
                p
            );

            bottomBar.anchoredPosition = Vector2.Lerp(
                bottomBar.anchoredPosition,
                bottomTarget,
                p
            );

            yield return null;
        }
    }
}
