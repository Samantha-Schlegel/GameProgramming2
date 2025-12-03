using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuPanel;


    void Start()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ShowPauseMenu()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }
}