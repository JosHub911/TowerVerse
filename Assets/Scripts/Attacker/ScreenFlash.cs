using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash Instance;

    private Image flashImg;

    private void Awake()
    {
        Instance = this;
        flashImg = GetComponent<Image>();
        flashImg.color = new Color(1, 1, 1, 0);
    }

    public void Flash(float intensity = 0.35f, float duration = 0.2f)
    {
        flashImg.DOKill();
        flashImg.color = new Color(1, 1, 1, 0);

        flashImg.DOFade(intensity, duration * 0.3f)
                .OnComplete(() =>
                flashImg.DOFade(0f, duration * 0.7f));
    }
}
