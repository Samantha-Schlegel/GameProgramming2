using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighscoreMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField initialsInput;
    [SerializeField] private TextMeshProUGUI boardText;

    private bool scoreSubmitted = false;

public void SubmitScore()
{
    if (scoreSubmitted) return;

    string initials = initialsInput.text.ToUpper();
    int finalScore = Mathf.RoundToInt(GameManager.instance.score);

    HighscoreManager.instance.AddScore(initials, finalScore);
    UpdateBoard();

    scoreSubmitted = true;

    GameManager.instance?.ResetGameValues();
}


    public void UpdateBoard()
    {
        boardText.text = "HIGHSCORES\n";
        int rank = 1;
        foreach (var entry in HighscoreManager.instance.data.entries)
        {
            boardText.text += $"{rank}. {entry.initials} - {entry.score}\n";
            rank++;
        }
    }

    public void ResetSubmission()
    {
        scoreSubmitted = false;
    }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            GameManager.instance?.ResetGameValues();
        }
    }
}
