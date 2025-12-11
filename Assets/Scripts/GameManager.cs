using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;   
using System.Diagnostics; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameOverMenu gameOverMenu; 
    [SerializeField] public GameTimer gameTimer;        

    public int score = 0;
    public int badScore = 0;
    public bool gameStarted = false;


    private int[] scoreThresholds = {60000000, 1200000, 1800000};
    private int[] badScoreThresholds = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};
    private bool[] scoreTriggered;
    private bool[] badScoreTriggered;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        scoreTriggered = new bool[scoreThresholds.Length];
        badScoreTriggered = new bool[badScoreThresholds.Length];
    }

    private void Start()
    {
        Time.timeScale = 1f;
        ResetGameValues();
        if (gameOverMenu != null)
            gameOverMenu.HideMenu();
    }

    public void ResetGameValues()
    {
        score = 0;
        badScore = 0;
        for (int i = 0; i < scoreTriggered.Length; i++)
            scoreTriggered[i] = false;
        for (int i = 0; i < badScoreTriggered.Length; i++)
            badScoreTriggered[i] = false;
    }

        public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"Score: {score}");
        CheckGameOver();
    }

    public void AddBadScore(int amount)
    {
        badScore += amount;
        Debug.Log($"Bad Score: {badScore}");
        CheckGameOver();
    }
        public void CheckGameOver()
    {
        for (int i = 0; i < scoreThresholds.Length; i++)
        {
            if (!scoreTriggered[i] && score >= scoreThresholds[i])
            {
                scoreTriggered[i] = true;
                EndGame();
            }
        }

        for (int i = 0; i < badScoreThresholds.Length; i++)
        {
            if (!badScoreTriggered[i] && badScore >= badScoreThresholds[i])
            {
                badScoreTriggered[i] = true;
                FailedGame();
            }
        }
    }

public void EndGame()
{
    if (gameTimer != null && gameTimer.remaining > 0f)
    {
        Debug.Log("[GameManager] EndGame called early, ignoring because timer not finished.");
        return;
    }

    float elapsedTime = gameTimer != null ? gameTimer.elapsed : 0f;
    float finalScore = (10 + (score * 100f));
    float finalBadScore = badScore; 
    float finalMergedScore = finalScore + finalBadScore;

    Debug.Log($"Game Over! Final Score = {finalScore}, BadScore = {finalBadScore}, Merged = {finalMergedScore}");

    Time.timeScale = 1f;
    if (gameOverMenu != null)
    {
        gameOverMenu.ShowFinalScore(finalMergedScore);
    }

    Time.timeScale = 0f;
}
public void FailedGame()
{
    Debug.Log($"[GameManager] FailedGame triggered! BadScore = {badScore}");

    Time.timeScale = 1f;
    if (gameOverMenu != null)
    {
        gameOverMenu.ShowFinalBadScore(badScore);
    }
    Time.timeScale = 0f;
}
public void BindGameTimer(GameTimer t)
{
    gameTimer = t;
    Debug.Log($"[GameManager] Bound GameTimer instance {t.GetInstanceID()}");
}


}

public class SceneResetter : MonoBehaviour
{
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
    if (scene.name == "MainMenu")
    {
        GameManager.instance?.ResetGameValues();
        FindObjectOfType<HighscoreMenu>()?.ResetSubmission();
    }
}

}