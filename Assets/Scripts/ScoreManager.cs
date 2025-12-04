using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public int maxScore = 4;

    private GameManager gameManager;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void AddPoint()
    {
        score++;
        Debug.Log("Score: " + score);

        if (score >= maxScore && gameManager != null)
        {
            gameManager.EndGame(score);
        }
    }
}
