using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;

    void Start()
    {
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1); // Loads scene index 1 (first level)
    }

    public void OpenCredits(GameObject creditsPanel)
    {
        creditsPanel.SetActive(true);
    }

    public void OpenSettings(GameObject settingsPanel)
    {
        settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
