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


    private int[] scoreThresholds = {60, 120, 180};
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






    private string GetCallerInfo()
    {
        var st = new StackTrace(true);
        for (int i = 0; i < st.FrameCount; i++)
        {
            var frame = st.GetFrame(i);
            var method = frame.GetMethod();
            if (method == null) continue;
            var declaringType = method.DeclaringType;
            if (declaringType == null) continue;
            string typeName = declaringType.FullName;
            if (!typeName.StartsWith("System.") && !typeName.StartsWith("UnityEngine.") && !typeName.Contains("GameManager"))
            {
                return $"{typeName}.{method.Name} (at {frame.GetFileName()}:{frame.GetFileLineNumber()})";
            }
        }
        if (st.FrameCount > 2)
        {
            var f = st.GetFrame(2);
            var m = f.GetMethod();
            return $"{m.DeclaringType.FullName}.{m.Name} (at {f.GetFileName()}:{f.GetFileLineNumber()})";
        }
        return "UnknownCaller";
    }

    private void LogEndTrigger(string type, float computedScore)
    {
        string caller = GetCallerInfo();
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"[EndTrigger] Type={type} Scene=\"{sceneName}\" Time={Time.time} ComputedScore={computedScore} Caller={caller}");
        Debug.Log($"[EndTrigger StackTrace]\n{new StackTrace(true).ToString()}");
    }





public void EndGame()
{
    if (gameTimer != null && gameTimer.remaining > 0f)
    {
        Debug.Log("[GameManager] EndGame called early, ignoring because timer not finished.");
        return;
    }

    float elapsedTime = gameTimer != null ? gameTimer.elapsed : 0f;
    float finalScore = (10 + (score * 10000f));
    Debug.Log($"Game Over! Final Score = {finalScore}");

    Time.timeScale = 1f;
    if (gameOverMenu != null)
    {
        gameOverMenu.ShowFinalScore(finalScore);
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