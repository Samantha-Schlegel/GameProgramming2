using UnityEngine;

public class EnemyScoreManager : MonoBehaviour
{
    public static EnemyScoreManager instance;

    public int badScore = 0;
    public int maxScore = 1;

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

    public void AddEnemyPoint()
    {
        badScore++;
        Debug.Log("Score: " + badScore);

        if (badScore >= maxScore && gameManager != null)
        {
            gameManager.FailedGame(badScore);
        }
    }
}
