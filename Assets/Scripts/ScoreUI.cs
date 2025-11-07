using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        if (scoreText == null)
            scoreText = GetComponent<TMP_Text>();

        UpdateScoreDisplay();
    }

    void Update()
    {
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + ScoreManager.instance.score;
    }
}
