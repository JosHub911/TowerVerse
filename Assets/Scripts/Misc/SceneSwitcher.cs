using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private string nextSceneName = "NextScene";

    // Update is called once per frame
    void Update()
    {
        if (spawner != null && spawner.IsAllWavesDone())
        {
            // Laad de volgende scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
