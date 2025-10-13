using UnityEngine;
using System.Collections;

public class UIAnimator : MonoBehaviour
{
    public IEnumerator Fade(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    public IEnumerator Slide(RectTransform rect, Vector2 from, Vector2 to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            rect.anchoredPosition = Vector2.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        rect.anchoredPosition = to;
    }
}
