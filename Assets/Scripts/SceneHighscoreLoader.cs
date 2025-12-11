using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHighscoreLoader : MonoBehaviour
{
    [SerializeField] private GameObject highscoreMenu; // assign your HighscoreMenu Canvas in Inspector
    [SerializeField] private string targetSceneName = "HighscoreScene"; // set the scene name you want

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            if (highscoreMenu != null)
                highscoreMenu.SetActive(true);
        }
        else
        {
            if (highscoreMenu != null)
                highscoreMenu.SetActive(false);
        }
    }
}
