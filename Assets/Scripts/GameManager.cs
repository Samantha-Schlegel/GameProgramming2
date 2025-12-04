using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverMenu gameOverMenu; 
    [SerializeField] private GameTimer gameTimer;        

    private bool gameEnded = false;

    void Start()
    {
        if (gameOverMenu != null)
            gameOverMenu.HideMenu();
    }

    public void EndGame(int score)
    {
        if (gameEnded) return; 
        gameEnded = true;

        float elapsedTime = gameTimer != null ? gameTimer.elapsed : 0f;
        float finalScore = (score * 10000f) - (elapsedTime * 6f);

        Debug.Log($"[GameManager] Game Over! Final Score = {finalScore}");

        Time.timeScale = 0f;

        if (gameOverMenu != null)
            gameOverMenu.ShowFinalScore(finalScore);
    }
        public void FailedGame(int badScore)
    {
        if (gameEnded) return; 
        gameEnded = true;

        float elapsedTime = gameTimer != null ? gameTimer.elapsed : 0f;
        float finalBadScore = 0;

        Debug.Log($"[GameManager] Game Over! Final Score = {finalBadScore}");

        Time.timeScale = 0f;

        if (gameOverMenu != null)
            gameOverMenu.ShowFinalBadScore(finalBadScore);
    }
}
