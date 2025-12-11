using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStartMenu : MonoBehaviour
{
    public CanvasGroup menuGroup;
    public Button startButton;

    private static bool hasOpenedOnce = false;

    private void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (hasOpenedOnce)
        {
            gameObject.SetActive(false);
            return;
        }

        if (currentScene != 1)
        {
            gameObject.SetActive(false);
            return;
        }

        hasOpenedOnce = true;

        ShowMenu(true);
       Time.timeScale = 0f;

        startButton.onClick.AddListener(CloseMenu);
    }

void CloseMenu()
{
    ShowMenu(false);
    Time.timeScale = 1f;

    if (GameManager.instance != null && GameManager.instance.gameTimer != null)
    {
        GameManager.instance.gameTimer.StartTimer();
    }
}


    void ShowMenu(bool visible)
    {
        menuGroup.alpha = visible ? 1 : 0;
        menuGroup.interactable = visible;
        menuGroup.blocksRaycasts = visible;
    }
}
