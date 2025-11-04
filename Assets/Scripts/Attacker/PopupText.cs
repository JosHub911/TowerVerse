using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopupText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Show(string message)
    {
        text.text = message;
        transform.localScale = Vector3.zero;
        text.alpha = 1;

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack))
        .Join(text.DOFade(1f, 0.1f))
        .AppendInterval(0.6f)
        .Append(text.DOFade(0f, 0.4f))
        .Join(transform.DOMoveY(transform.position.y + 1f, 0.4f))
        .OnComplete(() => Destroy(gameObject));
    }
}
