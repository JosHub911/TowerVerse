using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 


public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private CanvasGroup levelSelectPanel;

    [Header("Animation Settings")]
    [SerializeField] private float transitionDuration = 0.5f;

    private UIAnimator animator;
    private bool isTransitioning = false;

    private void Awake()
    {
        animator = GetComponent<UIAnimator>();
        ShowMainMenuInstant();
    }

    public void OnStartButton()
    {
        if (!isTransitioning)
            StartCoroutine(SwitchPanel(mainMenuPanel, levelSelectPanel));
    }

    public void OnBackButton()
    {
        if (!isTransitioning)
            StartCoroutine(SwitchPanel(levelSelectPanel, mainMenuPanel));
    }

    public void OnLevelSelect(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private IEnumerator SwitchPanel(CanvasGroup from, CanvasGroup to)
    {
        isTransitioning = true;

        // Activate target panel
        to.gameObject.SetActive(true);

        // Fade out current
        yield return StartCoroutine(animator.Fade(from, 0f, transitionDuration));
        from.gameObject.SetActive(false);

        // Fade in target
        yield return StartCoroutine(animator.Fade(to, 1f, transitionDuration));

        isTransitioning = false;
    }

    private void ShowMainMenuInstant()
    {
        mainMenuPanel.gameObject.SetActive(true);
        levelSelectPanel.gameObject.SetActive(false);
        mainMenuPanel.alpha = 1;
        levelSelectPanel.alpha = 0;
    }
}
