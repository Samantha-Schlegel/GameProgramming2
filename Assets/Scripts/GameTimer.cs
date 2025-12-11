using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float duration = 60f;
    public float remaining;
    public float elapsed;
    private bool running;

    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        GameManager.instance?.BindGameTimer(this);
    }

    private void Start()
    {
        ResetTimer();
        StartTimer();
    }

    private void Update()
    {
        if (!running) return;

        remaining -= Time.unscaledDeltaTime;
        elapsed += Time.unscaledDeltaTime;

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remaining / 60f);
            int seconds = Mathf.FloorToInt(remaining % 60f);
            timerText.text = $"TIME: {minutes:00}:{seconds:00}";
        }

        if (remaining <= 0f)
        {
            remaining = 0f;
            running = false;
            GameManager.instance?.EndGame();
        }
    }

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public void ResetTimer()
    {
        remaining = duration;
        elapsed = 0f;
        running = false;

        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remaining / 60f);
            int seconds = Mathf.FloorToInt(remaining % 60f);
            timerText.text = $"TIME: {minutes:00}:{seconds:00}";
        }
    }
}
