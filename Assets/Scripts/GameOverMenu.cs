using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button nextSceneButton;

        private void Start()
    {
        if (nextSceneButton != null)
        {
            nextSceneButton.onClick.AddListener(LoadNextScene);
            nextSceneButton.gameObject.SetActive(false);
        }
    }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideMenu();  
        Time.timeScale = 1f;  
    }
    public void ShowFinalScore(float finalScore)
    {
        Time.timeScale = 1f; 
        gameObject.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Final Score: {Mathf.RoundToInt(finalScore)}";
        if (nextSceneButton != null)
        nextSceneButton.gameObject.SetActive(true);

        Canvas.ForceUpdateCanvases(); 
        Time.timeScale = 0f; 
        Debug.Log("Game Over! Final Score: " + finalScore);
    }
        public void ShowFinalBadScore(float finalBadScore)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Final Score: {Mathf.RoundToInt(finalBadScore)}";

        if (nextSceneButton != null)
        nextSceneButton.gameObject.SetActive(true);
        Canvas.ForceUpdateCanvases();
        Debug.Log("Game Over! Final Score: " + finalBadScore);
        Time.timeScale = 0f;
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

      public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    private void OnDestroy()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}


public void LoadNextScene()
{
    HideMenu();
    Time.timeScale = 1f;

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(nextSceneIndex);

        if (GameManager.instance != null && GameManager.instance.gameTimer != null)
        {
            GameManager.instance.gameTimer.ResetTimer();
            GameManager.instance.gameTimer.StartTimer();
        }
    }
    else
    {
        Debug.Log("No next scene in the build settings.");
    }
}

}
