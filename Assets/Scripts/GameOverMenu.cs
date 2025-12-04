using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void ShowFinalScore(float finalScore)
    {
        gameObject.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Final Score: {Mathf.RoundToInt(finalScore)}";

        Debug.Log("Game Over! Final Score: " + finalScore);
    }
        public void ShowFinalBadScore(float finalBadScore)
    {
        gameObject.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = $"Final Score: {Mathf.RoundToInt(finalBadScore)}";

        Debug.Log("Game Over! Final Score: " + finalBadScore);
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
}
