using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;
    private const string SaveKey = "Highscores";
    public HighscoreData data = new HighscoreData();

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
            LoadScores();
        }
    }

    public void AddScore(string initials, int score)
    {
        data.entries.Add(new HighscoreEntry { initials = initials, score = score });
        data.entries.Sort((a, b) => b.score.CompareTo(a.score));
        if (data.entries.Count > 10) data.entries.RemoveAt(10);
        SaveScores();
    }

    public void SaveScores()
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            data = JsonUtility.FromJson<HighscoreData>(json);
        }
    }
}
