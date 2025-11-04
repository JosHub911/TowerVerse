using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform[] Path;
    [SerializeField]private int lives = 10;
    [SerializeField] private GameObject Healthbar;
    
    [SerializeField] private GameObject gameOverUI; // Assign your GameOverUI Panel here

    private void Awake()
    {
        main = this;
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Enemy reached the end! Lives left: " + lives);
        SoundManager.Instance?.PlayPlayerDamage();

        Healthbar.transform.DOScaleX((float)lives / 10f, 0.2f);
        CamShake.Instance?.Shake(0.25f, 0.6f);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!");

        // Stop time (optional)
        Time.timeScale = 0f;

        // Show the GameOver UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOver UI not assigned in LevelManager!");
        }
    }

    // Called from the Restart Button in your UI
    private void RestartLevel()
    {
        Time.timeScale = 1f; // resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Called from Main Menu Button
    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
