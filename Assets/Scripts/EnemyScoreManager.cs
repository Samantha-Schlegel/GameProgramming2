using UnityEngine;

public class EnemyScoreManager : MonoBehaviour
{
    public static EnemyScoreManager instance;
    public int badScore = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddEnemyPoint()
    {
        badScore++;
        GameManager.instance.AddBadScore(1);
        Debug.Log($"Bad Score: {badScore}");
    }
}
