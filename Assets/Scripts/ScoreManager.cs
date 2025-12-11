using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddPoint(GameObject waldo)
    {
        score++;
        GameManager.instance.AddScore(1); 
        Destroy(waldo);
        Debug.Log($"Score: {score}");
    }
}
