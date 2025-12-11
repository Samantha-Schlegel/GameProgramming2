using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Countdown start time in seconds")]
    public float startTime = 60f;

    public float elapsed = 0f;

    [SerializeField] private TextMeshProUGUI timerText;

    public float remaining;
    private bool timerEnded = false;
    private GameManager gameManager;

    void Start()
    {
        remaining = startTime;
        elapsed = 0f;
        gameManager = FindFirstObjectByType<GameManager>();
        UpdateDisplay();
    }

    void Update()
    {
        if (timerEnded) return;

        if (remaining > 0f)
        {
            float delta = Time.deltaTime;
            remaining -= delta;
            elapsed += delta;

            if (remaining <= 0f)
            {
                remaining = 0f;
                timerEnded = true;
                UpdateDisplay();
                EndLevelAsWin();
            }
            else
            {
                UpdateDisplay();
            }
        }
    }

    private void EndLevelAsWin()
    {
        int currentScore = 0;
        if (ScoreManager.instance != null)
            currentScore = ScoreManager.instance.score;

        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();

   //     if (gameManager != null)
     //    {
     //        gameManager.EndGame(currentScore);
      //   }
        else
        {
            Debug.LogWarning("[GameTimer] GameManager not found when timer reached 0. Pausing time.");
            Time.timeScale = 0f;
        }
    }

    private void UpdateDisplay()
    {
        if (timerText == null)
            return;

        int totalSeconds = Mathf.Max(0, Mathf.CeilToInt(remaining));
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timerText.text = $"TIME: {minutes}:{seconds:00}";
    }
}
