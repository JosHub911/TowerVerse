using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform[] Path;
    public int lives = 10;

    
    [SerializeField] private GameObject gameOverUI; // Assign your GameOverUI Panel here

    private void Awake()
    {
        main = this;
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Enemy reached the end! Lives left: " + lives);

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
    public void RestartLevel()
    {
        Time.timeScale = 1f; // resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Called from Main Menu Button
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
